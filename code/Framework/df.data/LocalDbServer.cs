// --------------------------------------------------------------------------------
// <copyright file="LocalDbServer.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data
{
    using Df.IO;
    using MartinCostello.SqlLocalDb;
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using static Constants;

    internal sealed class LocalDbServer
        : SqlBase
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TemporarySqlLocalDbInstance _Instance;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private SqlLocalDbApi _SqlLocalDbApi;

        public string DatabaseName { get; }

        public string LdfFileName { get; }

        public string MdfFileName { get; }

        public LocalDbServer(string databaseName)
        {
            DatabaseName = Check.NotNull(nameof(databaseName), databaseName);
            var tmp = PathUtil.GetTempFileName();
            MdfFileName = Path.ChangeExtension(tmp, EXTENSION_TMP_MDF);
            LdfFileName = Path.ChangeExtension(tmp, EXTENSION_TMP_LDF);
            var sqlCreateDb = SQL_CREATE_DATABASE_FORMAT.FormatInvariant(databaseName, MdfFileName, LdfFileName);

            _SqlLocalDbApi = new SqlLocalDbApi { AutomaticallyDeleteInstanceFiles = true };
            _Instance = _SqlLocalDbApi.CreateTemporaryInstance(true);

            using var connection = _Instance.GetInstanceInfo().CreateConnection();
            connection.Open();
            using var createDbComand = connection.CreateCommand();
            createDbComand.CommandText = sqlCreateDb;
            _ = createDbComand.ExecuteNonQuery();
        }

        // TODO: Make this protected.
        public override SqlConnection CreateConnection()
        {
            var sb = _Instance.GetInstanceInfo().CreateConnectionStringBuilder();
            sb.InitialCatalog = DatabaseName;
            sb.ConnectTimeout = 180;
            sb.MaxPoolSize = Environment.ProcessorCount * 4;
            sb.Pooling = true;

            return new SqlConnection(sb.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Instance?.Manage()?.Stop();
                Cleanup(ref _Instance);
                Cleanup(ref _SqlLocalDbApi);
                Cleanup(MdfFileName);
                Cleanup(LdfFileName);
            }
        }

        private static void Cleanup<TDisposable>(ref TDisposable tDisposable)
                    where TDisposable : IDisposable
        {
            tDisposable?.Dispose();
            tDisposable = default;
        }

        private static void Cleanup(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}