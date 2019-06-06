// --------------------------------------------------------------------------------
// <copyright file="ScalarFactoryConfiguration.cs" company="Michalis Sarigiannidis">
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

    [DebuggerDisplay("[{MinValue}, {MaxValue}] + {Increment}")]
    public class ScalarFactoryConfiguration<TValue>
    : RangeFactoryConfiguration<TValue>, IScalarFactoryConfiguration<TValue>
    where TValue : struct, IComparable, IComparable<TValue>, IEquatable<TValue>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TValue Increment => GetValue<TValue>(PROPERTY_INCREMENT);

        public ScalarFactoryConfiguration(TValue min, TValue max, TValue increment)
            : base(min, max)
        {
            _ = Check.GreaterThanOrEqual(nameof(increment), increment, default);
            _ = Check.LessThanOrEqual(nameof(increment), increment, (dynamic)max - min);
            SetValue(PROPERTY_INCREMENT, increment);
        }

        public ScalarFactoryConfiguration(IDictionary<string, object> properties)
            : base(properties)
        {
        }

        public ScalarFactoryConfiguration()
        {
        }
    }
}