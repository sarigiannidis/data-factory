// --------------------------------------------------------------------------------
// <copyright file="RandomScalarFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using Df.Stochastic;
    using System;
    using System.Globalization;

    public abstract class RandomScalarFactory<TValue>
        : RandomFactory<TValue, IRangeFactoryConfiguration<TValue>>
        where TValue : struct, IComparable, IComparable<TValue>, IEquatable<TValue>
    {
        public override TValue CreateValue()
        {
            var formatProvider = CultureInfo.InvariantCulture;
            var minValue = (decimal)Convert.ChangeType(Configuration.MinValue, typeof(decimal), formatProvider);
            var maxValue = (decimal)Convert.ChangeType(Configuration.MaxValue, typeof(decimal), formatProvider);
            var result = ((decimal)Random.NextPercentage() * (maxValue - minValue)) + minValue;
            return (TValue)Convert.ChangeType(result, typeof(TValue), formatProvider);
        }
    }
}