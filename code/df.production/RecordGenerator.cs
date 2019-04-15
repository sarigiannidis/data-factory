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

        public int Depth => 0;

        public int FieldCount =>
            _Factories.Count;

        public bool IsClosed =>
            _Closed;

        public int RecordsAffected { get; private set; }

        public object this[string name] =>
            throw new NotImplementedException();

        public object this[int i] =>
            GetValue(i);

        public RecordGenerator(int recordsToCreate, IEnumerable<(IValueFactory factory, Type type, float nullPercentage)> factories)
        {
            _RecordsToCreate = Check.GreaterThan(nameof(recordsToCreate), recordsToCreate, 0);
            _Factories = new List<(IValueFactory factory, Type type, float nullPercentage)>(Check.NotNull(nameof(factories), factories));
        }

        public void Close() =>
            _Closed = true;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool GetBoolean(int i) =>
            throw new NotImplementedException();

        public byte GetByte(int i) =>
            throw new NotImplementedException();

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) =>
            throw new NotImplementedException();

        public char GetChar(int i) =>
            throw new NotImplementedException();

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) =>
            throw new NotImplementedException();

        public IDataReader GetData(int i) =>
            throw new NotImplementedException();

        public string GetDataTypeName(int i) =>
            throw new NotImplementedException();

        public DateTime GetDateTime(int i) =>
            throw new NotImplementedException();

        public decimal GetDecimal(int i) =>
            throw new NotImplementedException();

        public double GetDouble(int i) =>
            throw new NotImplementedException();

        public Type GetFieldType(int i) =>
            _Factories[i].type;

        public float GetFloat(int i) =>
            throw new NotImplementedException();

        public Guid GetGuid(int i) =>
            throw new NotImplementedException();

        public short GetInt16(int i) =>
            throw new NotImplementedException();

        public int GetInt32(int i) =>
            throw new NotImplementedException();

        public long GetInt64(int i) =>
            throw new NotImplementedException();

        public string GetName(int i) =>
            throw new NotImplementedException();

        public int GetOrdinal(string name) =>
            throw new NotImplementedException();

        public DataTable GetSchemaTable() =>
            throw new NotImplementedException();

        public string GetString(int i) =>
            throw new NotImplementedException();

        public object GetValue(int i) =>
            _Factories[i].factory.CreateValue();

        public int GetValues(object[] values) =>
            throw new NotImplementedException();

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

        public bool Read() =>
            NextResult();

        private void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
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
}