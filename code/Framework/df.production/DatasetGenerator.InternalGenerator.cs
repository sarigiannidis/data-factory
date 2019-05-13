// --------------------------------------------------------------------------------
// <copyright file="DatasetGenerator.InternalGenerator.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Production
{
    using Df.Data;
    using Df.Io.Descriptive;
    using Df.Io.Prescriptive;
    using Microsoft.Extensions.Logging;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using static Constants;
    using Index = Microsoft.SqlServer.Management.Smo.Index;

    internal sealed partial class DatasetGenerator
    {
        internal sealed class InternalGenerator
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly DatasetGenerator _Owner;

            internal InternalGenerator(DatasetGenerator generator) =>
                _Owner = generator;

            /// <summary>
            /// Generate the data.
            /// </summary>
            /// <remarks>
            /// 1. Add columns @DF1 and @DF2 to the table.
            ///      @DF1 is a primary key.
            ///      @DF2 contains the same values as @DF1, but scrambled.
            /// 2. For each primary key relationship, update the fk by binding @DF2 of the parent table to the @DF1 of the referenced table.
            /// 3. Drop the primary keys.
            /// </remarks>
            public ISql Generate()
            {
                ISql sql = null;

                _Owner._Logger.LogInformation("{0} - Started", nameof(Generate));
                try
                {
                    sql = _Owner._SqlFactory.CreateTemporaryDatabase(DEFAULT_DATABASENAME);
                    var tablePrescriptions = _Owner._Project.Prescriptor.TablePrescriptions
                        .AsParallel()
                        .WithDegreeOfParallelism(2);
                    var tableDescriptions = tablePrescriptions.Select(t => t.TableDescription);
                    var foreignKeyDescriptions = tableDescriptions.SelectMany(tableDescription => tableDescription.ForeignKeyDescriptions);

                    tablePrescriptions.ForAll(_ => CreateTable(sql, _));
                    tablePrescriptions.ForAll(_ => FillTable(sql, _));
                    tableDescriptions.ForAll(_ => FillAuxiliaryColumns(sql, _));
                    foreignKeyDescriptions.ForAll(_ => UpdateForeignKeys(sql, _));

                    DropAuxiliaryColumns(sql);
                    return sql;
                }
                catch (Exception exception)
                {
                    _Owner._Logger.LogError(exception, "{0} - Failed.", nameof(Generate));
                    sql?.Dispose();
                    throw;
                }
                finally
                {
                    _Owner._Logger.LogInformation("{0} - Finished.", nameof(Generate));
                }
            }

            private static void CreateTable(ISql sql, TablePrescription tablePrescription) =>
                sql.NonQuery(CreateTableDefinition(tablePrescription));

            private static string CreateTableDefinition(TablePrescription tablePrescription) =>
                        new StringBuilder()
                .AppendFormatInvariant("CREATE TABLE {0} (", tablePrescription.TableName())
                .AppendFormatInvariant("[@DF1] [BIGINT] IDENTITY(1, 1) NOT NULL PRIMARY KEY, ")
                .AppendFormatInvariant("[@DF2] [BIGINT] NULL, ")
                .AppendJoin(", ", tablePrescription
                    .ColumnPrescriptions
                    .Select(columnPrescription => columnPrescription.ColumnDescription)
                    .Where(columnDescription => columnDescription.IsWritable())
                    .Select(columnDescription => columnDescription.SqlDefinition()))
                .AppendFormatInvariant(");")
                .ToString();

            private static void DropAuxiliaryColumns(ISql sql)
            {
                using var connection = sql.CreateConnection();
                connection.Open();
                var server = new Server(new ServerConnection(connection));
                foreach (Table table in server.Databases[DEFAULT_DATABASENAME].Tables)
                {
                    var indexes = table.Indexes.Cast<Index>();
                    var columns = table.Columns.Cast<Column>();
                    indexes.Single().Drop();
                    columns.Single(c => c.Name == "@DF1").Drop();
                    columns.Single(c => c.Name == "@DF2").Drop();
                }
            }

            private static void FillAuxiliaryColumns(ISql sql, TableDescription tableDescription)
            {
                using var connection = sql.CreateConnection();
                connection.Open();
                var tableName = tableDescription.TableName();
                var command = "WITH CTE AS (SELECT ROW_NUMBER() OVER(ORDER BY newid() ASC) AS ROWNUMBER, [@DF1] FROM {0}) UPDATE T2 SET T2.[@DF2] = T1.[@DF1] FROM {0} T2, CTE T1 WHERE T1.ROWNUMBER = T2.[@DF1]".FormatInvariant(tableName);
                sql.NonQuery(connection, command);
            }

            private static void UpdateForeignKeys(ISql sql, ForeignKeyDescription foreignKeyDescription)
            {
                var parentTableName = foreignKeyDescription.Parent.TableName();
                var referencedTableName = foreignKeyDescription.Referenced.TableName();
                var columnRelationshipDescriptions = foreignKeyDescription.ColumnRelationshipDescriptions;
                var referencedColumnNames = columnRelationshipDescriptions.Select(_ => _.Referenced.Name);
                var parentColumnNames = columnRelationshipDescriptions.Select(_ => _.Parent.Name);
                var updatePairs = parentColumnNames.Zip(referencedColumnNames, (p, r) => $"T1.{p} = T2.{r}");
                var commandText = new StringBuilder("UPDATE T1 SET ")
                    .AppendJoin(", ", updatePairs)
                    .AppendFormatInvariant(" FROM {0} T1, {1} T2 WHERE T2.[@DF1] = T1.[@DF2];", parentTableName, referencedTableName)
                    .ToString();
                sql.NonQuery(commandText);
            }

            private void FillTable(ISql sql, TablePrescription tablePrescription)
            {
                _Owner._Logger.LogInformation("{0}({1}) - Started.", nameof(FillTable), tablePrescription.TableDescription.Name);

                var columns = tablePrescription.ColumnPrescriptions.Select(_ => _.ColumnDescription.Name).ToArray();
                var tableName = tablePrescription.TableName();
                using var recordGenerator = _Owner._RecordGeneratorFactory.Create(tablePrescription, tablePrescription.Rows);
                using var connection = sql.CreateConnection();
                connection.Open();
                using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, null)
                {
                    BatchSize = BULKCOPY_BATCHSIZE,
                    BulkCopyTimeout = 0,
                    DestinationTableName = tableName,
                    EnableStreaming = true,
                };

                // skipping DF1, DF2.
                for (var i = 0; i < recordGenerator.FieldCount; i++)
                {
                    _ = bulkCopy.ColumnMappings.Add(i, i + 2);
                }

                bulkCopy.WriteToServer(recordGenerator);
                _Owner._Logger.LogInformation("{0}({1}) - Finished.", nameof(FillTable), tablePrescription.TableDescription.Name);
            }
        }
    }
}