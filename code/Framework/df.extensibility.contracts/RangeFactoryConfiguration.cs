// --------------------------------------------------------------------------------
// <copyright file="RangeFactoryConfiguration.cs" company="Michalis Sarigiannidis">
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
    public class RangeFactoryConfiguration<TValue>
        : ValueFactoryConfiguration, IRangeFactoryConfiguration<TValue>
        where TValue : struct, IComparable, IComparable<TValue>, IEquatable<TValue>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TValue MaxValue => GetValue<TValue>(PROPERTY_MAX);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TValue MinValue => GetValue<TValue>(PROPERTY_MIN);

        public RangeFactoryConfiguration(TValue min, TValue max)
        {
            _ = Check.GreaterThanOrEqual(nameof(max), max, min);
            SetValue(PROPERTY_MIN, min);
            SetValue(PROPERTY_MAX, max);
        }

        public RangeFactoryConfiguration(IDictionary<string, object> properties)
            : base(properties)
        {
        }

        public RangeFactoryConfiguration()
        {
        }
    }
}