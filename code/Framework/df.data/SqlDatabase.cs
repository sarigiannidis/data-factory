// --------------------------------------------------------------------------------
// <copyright file="SqlDatabase.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data
{
    using System.Data.SqlClient;

    internal sealed class SqlDatabase
        : SqlBase
    {
        public string ConnectionString { get; }

        public SqlDatabase(string connectionString) => ConnectionString = Check.NotNull(nameof(connectionString), connectionString);

        public override SqlConnection CreateConnection() => new SqlConnection(ConnectionString);
    }
}