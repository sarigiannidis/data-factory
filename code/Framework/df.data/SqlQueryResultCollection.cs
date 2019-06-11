// --------------------------------------------------------------------------------
// <copyright file="SqlQueryResultCollection.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Diagnostics;

    internal sealed class SqlQueryResultCollection<TResult>
        : ISqlQueryResultCollection<TResult>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Func<IDataRecord, TResult> _Convert;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DbCommand _Command;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DbConnection _Connection;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Disposed;

        public SqlQueryResultCollection(DbConnection connection, DbCommand command, Func<IDataRecord, TResult> convert)
        {
            _Connection = Check.NotNull(nameof(connection), connection);
            _Command = Check.NotNull(nameof(command), command);
            _Convert = Check.NotNull(nameof(convert), convert);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerator<TResult> GetEnumerator()
        {
            if (_Connection.State == ConnectionState.Closed)
            {
                _Connection.Open();
            }

            return new SqlQueryResultCollectionEnumerator<TResult>(_Command.ExecuteReader(), _Convert);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                return;
            }

            if (disposing)
            {
                _Command?.Dispose();
                _Command = null;
                _Connection?.Dispose();
                _Connection = null;
            }

            _Disposed = true;
        }
    }
}