// --------------------------------------------------------------------------------
// <copyright file="DatasetGenerator.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Production
{
    using Df.Collections;
    using Df.Data;
    using Df.Io;
    using Df.Io.Descriptive;
    using Microsoft.Extensions.Logging;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using static Constants;
    using Check = Df.Check;

    internal sealed partial class DatasetGenerator
        : IDatasetGenerator
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILogger<DatasetGenerator> _Logger;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Project _Project;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IRecordGeneratorFactory _RecordGeneratorFactory;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ISqlFactory _SqlFactory;

        public DatasetGenerator(IRecordGeneratorFactory recordGeneratorFactory, ISqlFactory sqlFactory, ILogger<DatasetGenerator> logger, Project project)
        {
            _RecordGeneratorFactory = Check.NotNull(nameof(recordGeneratorFactory), recordGeneratorFactory);
            _SqlFactory = Check.NotNull(nameof(sqlFactory), sqlFactory);
            _Logger = Check.NotNull(nameof(logger), logger);
            _Project = Check.NotNull(nameof(project), project);
        }

        public void GenerateDatabase(bool disableTriggers, bool dryRun)
        {
            var tablePrescriptions = GetOrderedTableDescriptions()
                .Select(_ => _Project.Prescriptor.TablePrescriptions.SingleOrDefault(tablePrescription => tablePrescription.TableDescription == _))
                .ToArray();

            using var source = new InternalGenerator(this).Generate();
            using var destination = _SqlFactory.Open(_Project.Descriptor.ConnectionString);
            using var sourceConnection = source.CreateConnection();
            using var destinationConnection = destination.CreateConnection();
            sourceConnection.Open();
            destinationConnection.Open();
            using var transaction = destinationConnection.BeginTransaction("DATAFACTORY");
            using var deleteCommand = destinationConnection.CreateCommand();
            var deletions = tablePrescriptions
                .Select(t => t.TableName())
                .Reverse()
                .Select(tableName => "DELETE FROM {0};".FormatInvariant(tableName));
            deleteCommand.CommandText = string.Concat(deletions);
            deleteCommand.Transaction = transaction;
            _ = deleteCommand.ExecuteNonQuery();
            var copyOptions = CreateCopyOptions(disableTriggers);
            foreach (var tablePrescription in tablePrescriptions)
            {
                var tableName = tablePrescription.TableName();
                var columnNames = tablePrescription.ColumnPrescriptions.Select(c => c.ColumnDescription.Name);
                ExecuteSqlBulkCopy(transaction, sourceConnection, destinationConnection, copyOptions, tableName, columnNames);
            }

            if (dryRun)
            {
                transaction.Rollback();
            }
            else
            {
                transaction.Commit();
            }
        }

        public void GenerateStream(Stream stream, bool disableTriggers, bool dryRun)
        {
            _ = Check.NotNull(nameof(stream), stream);
            using var sql = new InternalGenerator(this).Generate();
            var tableDescriptions = GetOrderedTableDescriptions().ToArray();
            WriteStartTransaction(stream);
            WriteDisableTriggers(stream, disableTriggers);
            WriteDeletions(stream, tableDescriptions);
            WriteInsertions(stream, sql, tableDescriptions);
            WriteFinishTranscation(stream, dryRun);
        }

        private static SqlBulkCopyOptions CreateCopyOptions(bool disableTriggers)
        {
            var copyOptions = SqlBulkCopyOptions.TableLock
                | SqlBulkCopyOptions.KeepIdentity;
            if (!disableTriggers)
            {
                copyOptions |= SqlBulkCopyOptions.FireTriggers;
            }

            return copyOptions;
        }

        private static void ExecuteSqlBulkCopy(SqlTransaction transaction, SqlConnection sourceConnection, SqlConnection destinationConnection, SqlBulkCopyOptions copyOptions, string tableName, IEnumerable<string> columnNames)
        {
            using var sourceCommand = sourceConnection.CreateCommand();
            sourceCommand.CommandText = SQL_SELECT.FormatInvariant(tableName);
            var sourceTableReader = sourceCommand.ExecuteReader();
            using var sqlBulkCopy = new SqlBulkCopy(destinationConnection, copyOptions, transaction)
            {
                BatchSize = BULKCOPY_BATCHSIZE,
                BulkCopyTimeout = 0,
                DestinationTableName = tableName,
                EnableStreaming = true,
            };
            foreach (var columnName in columnNames)
            {
                _ = sqlBulkCopy.ColumnMappings.Add(columnName, columnName);
            }

            sqlBulkCopy.WriteToServer(sourceTableReader);
            sourceTableReader.Close();
        }

        private static void WriteDeletions(Stream stream, TableDescription[] tableDescriptions)
        {
            var deletions = tableDescriptions.Reverse().Select(tableDescription => SQL_DELETE.FormatInvariant(tableDescription.TableName()));
            stream.WriteLines(deletions);
        }

        private static void WriteDisableTriggers(Stream stream, bool disableTriggers)
        {
            if (disableTriggers)
            {
                stream.WriteLine("DISABLE TRIGGER ALL ON DATABASE;");
            }
        }

        private static void WriteFinishTranscation(Stream stream, bool dryRun)
        {
            if (dryRun)
            {
                stream.WriteLine("ROLLBACK TRANSACTION DATAFACTORY;");
            }
            else
            {
                stream.WriteLine("COMMIT TRANSACTION DATAFACTORY;");
            }
        }

        private static void WriteInsertions(Stream stream, ISql sql, TableDescription[] tableDescriptions)
        {
            using var connection = sql.CreateConnection();
            var server = new Server(new ServerConnection(connection));
            var database = server.Databases[DEFAULT_DATABASENAME];
            foreach (var tableDescription in tableDescriptions)
            {
                bool Match(Table table) => table.Schema == tableDescription.Schema
                    && table.Name == tableDescription.Name;

                var table = database
                    .Tables
                    .Cast<Table>()
                    .First(Match);

                var tableName = tableDescription.TableName();
                stream.WriteLine("SET IDENTITY_INSERT {0} ON;".FormatInvariant(tableName));
                WriteInsertions(stream, table);
                stream.WriteLine("SET IDENTITY_INSERT {0} OFF;".FormatInvariant(tableName));
            }
        }

        private static void WriteInsertions(Stream stream, Table table)
        {
            var tempFileName = Path.GetTempFileName();
            try
            {
                var options = new ScriptingOptions
                {
                    AppendToFile = true,
                    EnforceScriptingOptions = true,
                    FileName = tempFileName,
                    ScriptData = true,
                    ScriptSchema = false,
                    Encoding = DEFAULT_ENCODING,
                    NoCommandTerminator = true,
                };
                _ = table.EnumScript(options);
                using var tempFileStream = File.OpenRead(tempFileName);

                // Skipping encoding preamble bytes.
                tempFileStream.Position = DEFAULT_ENCODING.GetPreamble().Length;
                tempFileStream.CopyTo(stream);
            }
            finally
            {
                if (File.Exists(tempFileName))
                {
                    File.Delete(tempFileName);
                }
            }
        }

        private static void WriteStartTransaction(Stream stream) => stream.WriteLine("BEGIN TRANSACTION DATAFACTORY WITH MARK N'DATAFACTORY';");

        private IEnumerable<TableDescription> GetOrderedTableDescriptions() => new Orderer<TableDescription>(_ => _.ForeignKeyDescriptions.Select(foreignKeyDescription => foreignKeyDescription.Referenced))
                .Order(_Project.Prescriptor.TablePrescriptions.Select(_ => _.TableDescription))
                .Select(_ => _.Node);
    }
}