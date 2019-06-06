// --------------------------------------------------------------------------------
// <copyright file="TestHandler.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.OptionHandlers
{
    using Df.Data;
    using Df.Options;
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using static Constants;

    internal sealed class TestHandler
        : IHandler<TestOptions>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ISqlFactory _SqlFactory;

        public TestHandler(ISqlFactory sqlFactory) => _SqlFactory = Check.NotNull(nameof(sqlFactory), sqlFactory);

        public void Handle(TestOptions options)
        {
            try
            {
                using var sql = _SqlFactory.Open(options.ConnectionString);
                using var result = sql.Query(SQL_COUNT_TABLES, dataRecord => (int)dataRecord[0]);
                var databaseName = new SqlConnectionStringBuilder(options.ConnectionString).InitialCatalog;
                Console.WriteLine("{0} contains {1} tables.".FormatInvariant(databaseName, result.Single()));
                Console.WriteLine("Good connection.");
            }
            catch
            {
                Console.WriteLine("Failed to connect.");
                throw;
            }
        }
    }
}