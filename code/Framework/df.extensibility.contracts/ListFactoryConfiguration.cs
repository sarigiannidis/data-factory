// --------------------------------------------------------------------------------
// <copyright file="ListFactoryConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using Df.Numeric;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using static Constants;

    [DebuggerDisplay("[{MinValue}, {MaxValue}]")]
    public class ListFactoryConfiguration<TValue>
        : ValueFactoryConfiguration, IListFactoryConfiguration<TValue>
        where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public WeightedValueCollection<TValue> WeightedValues =>
            GetValue<WeightedValueCollection<TValue>>(PROPERTY_VALUES);

        public ListFactoryConfiguration(WeightedValueCollection<TValue> weightedValues) =>
            SetValue(PROPERTY_VALUES, Check.NotNull(nameof(weightedValues), weightedValues));

        public ListFactoryConfiguration(IDictionary<string, object> properties)
            : base(properties)
        {
        }

        public ListFactoryConfiguration()
        {
        }

        protected override T GetValue<T>(string key)
        {
            var value = ((IReadOnlyDictionary<string, object>)this)[key];
            return typeof(T) == typeof(WeightedValueCollection<TValue>) && value.GetType() == typeof(JArray)
                ? (T)(dynamic)(WeightedValueCollection<TValue>)(JArray)value
                : base.GetValue<T>(key);
        }
    }
}