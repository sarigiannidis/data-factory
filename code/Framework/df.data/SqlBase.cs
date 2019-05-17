// --------------------------------------------------------------------------------
// <copyright file="SqlBase.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Diagnostics;

    [DebuggerDisplay("{ConnectionString}")]
    public abstract class SqlBase
        : ISql
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Disposed;

        public abstract SqlConnection CreateConnection();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void NonQuery(string commandText)
        {
            _ = Check.NotNull(nameof(commandText), commandText);
            ExecuteNonQuery(null, commandText);
        }

        public void NonQuery(SqlConnection connection, string commandText)
        {
            _ = Check.NotNull(nameof(connection), connection);
            _ = Check.NotNull(nameof(commandText), commandText);
            ExecuteNonQuery(connection, null, commandText);
        }

        public void NonQuery(SqlConnection connection, SqlTransaction transaction, string commandText)
        {
            _ = Check.NotNull(nameof(connection), connection);
            _ = Check.NotNull(nameof(commandText), commandText);
            _ = Check.NotNull(nameof(transaction), transaction);
            ExecuteNonQuery(connection, transaction, commandText);
        }

        public ISqlQueryResultCollection<T> Query<T>(string query, Func<IDataRecord, T> convert)
        {
            _ = Check.NotNull(nameof(query), query);
            _ = Check.NotNull(nameof(convert), convert);
            return ExecuteQuery(query, convert);
        }

        protected virtual DbCommand CreateCommand(SqlConnection connection, string commandText)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            return command;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                return;
            }

            if (disposing)
            {
            }

            _Disposed = true;
        }

        private void ExecuteNonQuery(SqlTransaction transaction, string query)
        {
            using var connection = CreateConnection();
            connection.Open();
            ExecuteNonQuery(connection, transaction, query);
        }

        private void ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, string commandText)
        {
            using var command = CreateCommand(connection, commandText);
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            _ = command.ExecuteNonQuery();
        }

        private ISqlQueryResultCollection<TResult> ExecuteQuery<TResult>(string query, Func<IDataRecord, TResult> convert)
        {
            var connection = CreateConnection();
            var command = CreateCommand(connection, query);
            return new SqlQueryResultCollection<TResult>(connection, command, convert);
        }
    }
}