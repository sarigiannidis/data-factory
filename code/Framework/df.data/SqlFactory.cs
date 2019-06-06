// --------------------------------------------------------------------------------
// <copyright file="SqlFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data
{
    using System.Data.SqlClient;

    internal sealed class SqlFactory
        : ISqlFactory
    {
        public ISql CreateTemporaryDatabase(string databaseName)
            => new LocalDbServer(Check.NotNull(nameof(databaseName), databaseName));

        public ISql Open(string connectionString) => new SqlDatabase(connectionString);
    }
}