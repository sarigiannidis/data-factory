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
        : IValueFactoryConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IDictionary<string, object> _Properties;

        public static IValueFactoryConfiguration Empty => new EmptyConfiguration();

        public int Count => _Properties.Count;

        public bool IsReadOnly => _Properties.IsReadOnly;

        public ICollection<string> Keys => _Properties.Keys;

        public object this[string key] { get => _Properties[key]; set => _Properties[key] = value; }

        public ICollection<object> Values => _Properties.Values;

        protected ValueFactoryConfiguration(IDictionary<string, object> properties) => _Properties = new Dictionary<string, object>(properties);

        protected ValueFactoryConfiguration()
            : this(new Dictionary<string, object>())
        {
        }

        public void Add(string key, object value) => _Properties.Add(key, value);

        public void Add(KeyValuePair<string, object> item) => _Properties.Add(item);

        public void Clear() => _Properties.Clear();

        public bool Contains(KeyValuePair<string, object> item) => _Properties.Contains(item);

        public bool ContainsKey(string key) => _Properties.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) => _Properties.CopyTo(array, arrayIndex);

        public override bool Equals(object obj) => obj is IValueFactoryConfiguration o && Equals(o);

        public bool Equals(IValueFactoryConfiguration other)
        {
            if (other is null)
            {
                return false;
            }
            else if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (_Properties.Count != other.Count)
            {
                return false;
            }

            foreach (var key in _Properties.Keys)
            {
                if (!other.TryGetValue(key, out var otherValue)
                    || !_Properties[key].Equals(otherValue))
                {
                    return false;
                }
            }

            return true;
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() => _Properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _Properties.GetEnumerator();

        public override int GetHashCode() => HashCode.Combine(_Properties);

        public bool Remove(string key) => _Properties.Remove(key);

        public bool Remove(KeyValuePair<string, object> item) => _Properties.Remove(item);

        public bool TryGetValue(string key, out object value) => _Properties.TryGetValue(key, out value);

        // Serialization may return a type other than the original.
        protected virtual T GetValue<T>(string key) => _Properties[key] switch
            {
                T t => t,
                DateTimeOffset d when typeof(T) == typeof(DateTime) => (T)(object)d.DateTime,
                string s when typeof(T) == typeof(TimeSpan) => (T)(object)TimeSpan.Parse(s, CultureInfo.InvariantCulture),
                _ => (T)Convert.ChangeType(_Properties[key], typeof(T), CultureInfo.InvariantCulture)
            };

        protected void SetValue(string key, object value) => _Properties[key] = value;
    }
}