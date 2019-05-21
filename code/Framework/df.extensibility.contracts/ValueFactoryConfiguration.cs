// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;

    public abstract class ValueFactoryConfiguration
        : IValueFactoryConfiguration,
        IEquatable<ValueFactoryConfiguration>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IDictionary<string, object> _Properties;

        public static IValueFactoryConfiguration Empty =>
                new EmptyConfiguration();

        public ICollection<string> Keys => _Properties.Keys;

        public ICollection<object> Values => _Properties.Values;

        public int Count => _Properties.Count;

        public bool IsReadOnly => _Properties.IsReadOnly;

        public object this[string key] { get => _Properties[key]; set => _Properties[key] = value; }

        protected ValueFactoryConfiguration(IDictionary<string, object> properties) =>
            _Properties = new Dictionary<string, object>(properties);

        protected ValueFactoryConfiguration()
            : this(new Dictionary<string, object>())
        {
        }

        public override bool Equals(object obj) =>
            obj is ValueFactoryConfiguration o && Equals(o);

        public bool Equals(ValueFactoryConfiguration other)
        {
            if (other is null)
            {
                return false;
            }
            else if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (_Properties.Count != other._Properties.Count)
            {
                return false;
            }

            foreach (var key in _Properties.Keys)
            {
                if (!other._Properties.TryGetValue(key, out var otherValue)
                    || _Properties[key] != otherValue)
                {
                    return false;
                }
            }

            return true;
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() =>
            _Properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            _Properties.GetEnumerator();

        public override int GetHashCode() =>
            HashCode.Combine(_Properties);

        // Serialization may return a type other than the original.
        protected virtual T GetValue<T>(string key) =>
            _Properties[key] switch
        {
            T t => t,
            DateTimeOffset d when typeof(T) == typeof(DateTime) => (T)(object)d.DateTime,
            string s when typeof(T) == typeof(TimeSpan) => (T)(object)TimeSpan.Parse(s, CultureInfo.InvariantCulture),
            _ => (T)Convert.ChangeType(_Properties[key], typeof(T), CultureInfo.InvariantCulture)
        };

        protected void SetValue(string key, object value) =>
            _Properties[key] = value;
        public void Add(string key, object value) => _Properties.Add(key, value);
        public bool ContainsKey(string key) => _Properties.ContainsKey(key);
        public bool Remove(string key) => _Properties.Remove(key);
        public bool TryGetValue(string key, out object value) => _Properties.TryGetValue(key, out value);
        public void Add(KeyValuePair<string, object> item) => _Properties.Add(item);
        public void Clear() => _Properties.Clear();
        public bool Contains(KeyValuePair<string, object> item) => _Properties.Contains(item);
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => _Properties.CopyTo(array, arrayIndex);
        public bool Remove(KeyValuePair<string, object> item) => _Properties.Remove(item);
    }
}