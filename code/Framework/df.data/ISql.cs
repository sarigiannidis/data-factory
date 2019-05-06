// --------------------------------------------------------------------------------
// <copyright file="ISql.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public interface ISql
        : IDisposable
    {
        SqlConnection CreateConnection();

        void NonQuery(string commandText);

        void NonQuery(SqlConnection connection, string commandText);

        void NonQuery(SqlConnection connection, SqlTransaction transaction, string commandText);

        ISqlQueryResultCollection<T> Query<T>(string query, Func<IDataRecord, T> convert);
    }
}