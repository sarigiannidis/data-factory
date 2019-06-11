// --------------------------------------------------------------------------------
// <copyright file="RecordGenerator.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Production
{
    using Df.Extensibility;
    using Df.Stochastic;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;

    internal sealed class RecordGenerator
        : IDataReader
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<(IValueFactory factory, Type type, float nullPercentage)> _Factories;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly int _RecordsToCreate;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Closed;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Disposed;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HardRandom _Random = new HardRandom();

        public RecordGenerator(int recordsToCreate, IEnumerable<(IValueFactory factory, Type type, float nullPercentage)> factories)
        {
            _RecordsToCreate = Check.GreaterThan(nameof(recordsToCreate), recordsToCreate, 0);
            _Factories = new List<(IValueFactory factory, Type type, float nullPercentage)>(Check.NotNull(nameof(factories), factories));
        }

        public int Depth => 0;

        public int FieldCount => _Factories.Count;

        public bool IsClosed => _Closed;

        public int RecordsAffected { get; private set; }

        public object this[string name] => ThrowNotImplemented();

        public object this[int i] => GetValue(i);

        public void Close() => _Closed = true;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool GetBoolean(int i) => ThrowNotImplemented();

        public byte GetByte(int i) => ThrowNotImplemented();

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => ThrowNotImplemented();

        public char GetChar(int i) => ThrowNotImplemented();

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => ThrowNotImplemented();

        public IDataReader GetData(int i) => ThrowNotImplemented();

        public string GetDataTypeName(int i) => ThrowNotImplemented();

        public DateTime GetDateTime(int i) => ThrowNotImplemented();

        public decimal GetDecimal(int i) => ThrowNotImplemented();

        public double GetDouble(int i) => ThrowNotImplemented();

        public Type GetFieldType(int i) => _Factories[i].type;

        public float GetFloat(int i) => ThrowNotImplemented();

        public Guid GetGuid(int i) => ThrowNotImplemented();

        public short GetInt16(int i) => ThrowNotImplemented();

        public int GetInt32(int i) => ThrowNotImplemented();

        public long GetInt64(int i) => ThrowNotImplemented();

        public string GetName(int i) => ThrowNotImplemented();

        public int GetOrdinal(string name) => ThrowNotImplemented();

        public DataTable GetSchemaTable() => ThrowNotImplemented();

        public string GetString(int i) => ThrowNotImplemented();

        public object GetValue(int i) => _Factories[i].factory.CreateValue();

        public int GetValues(object[] values) => ThrowNotImplemented();

        public bool IsDBNull(int i)
        {
            var (_, _, nullPercentage) = _Factories[i];
            return nullPercentage > 0 && _Random.NextPercentage() <= nullPercentage;
        }

        public bool NextResult()
        {
            if (!_Closed && RecordsAffected < _RecordsToCreate)
            {
                RecordsAffected++;
                return true;
            }
            else
            {
                _Closed = true;
                return false;
            }
        }

        public bool Read() => NextResult();

        private static dynamic ThrowNotImplemented() => throw new NotImplementedException();

        private void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var (factory, _, _) in _Factories)
                {
                    if (factory is IDisposable d)
                    {
                        d.Dispose();
                    }
                }

                _Factories.Clear();
                _Random?.Dispose();
                _Random = null;
            }

            _Disposed = true;
        }
    }
}