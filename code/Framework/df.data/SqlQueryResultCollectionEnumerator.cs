// --------------------------------------------------------------------------------
// <copyright file="SqlQueryResultCollectionEnumerator.cs" company="Michalis Sarigiannidis">
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

    internal sealed class SqlQueryResultCollectionEnumerator<TResult>
        : IEnumerator<TResult>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Func<IDataRecord, TResult> _Convert;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DbDataReader _Reader;

        public TResult Current => _Convert(_Reader);

        object IEnumerator.Current => Current;

        public SqlQueryResultCollectionEnumerator(DbDataReader reader, Func<IDataRecord, TResult> convert)
        {
            _Reader = Check.NotNull(nameof(reader), reader);
            _Convert = Check.NotNull(nameof(convert), convert);
        }

        public void Dispose()
        {
            _Reader?.Close();
            _Reader?.Dispose();
            _Reader = null;
        }

        public bool MoveNext() => _Reader.Read();

        public void Reset() => throw new NotSupportedException();
    }
}