// --------------------------------------------------------------------------------
// <copyright file="ListFactoryConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
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
        public IEnumerable<WeightedValue<TValue>> WeightedValues =>
            Get<IEnumerable<WeightedValue<TValue>>>(PROPERTY_VALUES);

        public ListFactoryConfiguration(IEnumerable<WeightedValue<TValue>> weightedValues) =>
            Set(PROPERTY_VALUES, Check.NotNull(nameof(weightedValues), weightedValues));

        public ListFactoryConfiguration(IDictionary<string, object> properties)
            : base(properties)
        {
        }

        public ListFactoryConfiguration()
        {
        }
    }
}