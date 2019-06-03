// --------------------------------------------------------------------------------
// <copyright file="WeightedValueCollection.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Numeric
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [JsonArray(IsReference = false)]
    public sealed class WeightedValueCollection<TValue>
        : IList<WeightedValue<TValue>>,
        IEquatable<WeightedValueCollection<TValue>>
        where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonIgnore]
        private readonly List<WeightedValue<TValue>> _WeightedValues = new List<WeightedValue<TValue>>();

        [JsonIgnore]
        public int Count =>
            _WeightedValues.Count;

        [JsonIgnore]
        public bool IsReadOnly =>
            ((IList<WeightedValue<TValue>>)_WeightedValues).IsReadOnly;

        [JsonIgnore]
        public float SumOfWeights { get; private set; }

        public WeightedValue<TValue> this[int index]
        {
            get => _WeightedValues[index];
            set
            {
                _WeightedValues[index] = value;
                Reset();
            }
        }

        public WeightedValueCollection(IEnumerable<WeightedValue<TValue>> collection)
        {
            _WeightedValues.AddRange(collection);
            Reset();
        }

        public WeightedValueCollection()
        {
        }

        public static WeightedValueCollection<TValue> FromJArray(JArray jArray) =>
            new WeightedValueCollection<TValue>(Check.NotNull(nameof(jArray), jArray).Select(t => (WeightedValue<TValue>)t));

        public static implicit operator WeightedValueCollection<TValue>(JArray jArray) =>
            FromJArray(jArray);

        public void Add(WeightedValue<TValue> item)
        {
            _WeightedValues.Add(item);
            Reset();
        }

        public void AddRange(IEnumerable<WeightedValue<TValue>> collection)
        {
            _WeightedValues.AddRange(collection);
            Reset();
        }

        public void Clear() =>
            _WeightedValues.Clear();

        public bool Contains(WeightedValue<TValue> item) =>
            _WeightedValues.Contains(item);

        public void CopyTo(WeightedValue<TValue>[] array, int arrayIndex) =>
            _WeightedValues.CopyTo(array, arrayIndex);

        public bool Equals(WeightedValueCollection<TValue> other)
        {
            if (other is null)
            {
                return false;
            }
            else if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (_WeightedValues.Count != other.Count)
            {
                return false;
            }

            foreach (var key in _WeightedValues)
            {
                if (!other._WeightedValues.Contains(key))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj) =>
            obj is WeightedValueCollection<TValue> o && Equals(o);

        public IEnumerator<WeightedValue<TValue>> GetEnumerator() =>
            _WeightedValues.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            _WeightedValues.GetEnumerator();

        public override int GetHashCode()
        {
            var hashCode = default(HashCode);
            foreach (var value in _WeightedValues)
            {
                hashCode.Add(value);
            }

            return hashCode.ToHashCode();
        }

        public int IndexOf(WeightedValue<TValue> item) =>
                    _WeightedValues.IndexOf(item);

        public void Insert(int index, WeightedValue<TValue> item) =>
            _WeightedValues.Insert(index, item);

        public bool Remove(WeightedValue<TValue> item) =>
            _WeightedValues.Remove(item);

        public void RemoveAt(int index) =>
            _WeightedValues.RemoveAt(index);

        private void Reset()
        {
            _WeightedValues.Sort();
            SumOfWeights = this.Sum(w => w.Weight);
        }
    }
}