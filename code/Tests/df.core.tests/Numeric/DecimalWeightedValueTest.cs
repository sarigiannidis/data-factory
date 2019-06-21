// --------------------------------------------------------------------------------
// <copyright file="DecimalWeightedValueTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Numeric.Tests
{
    using System.Collections.Generic;

    public class DecimalWeightedValueTest
        : WeightedValueTest<decimal>
    {
        public override IEnumerable<decimal> Range() => new[] { decimal.Zero, decimal.MinValue, decimal.MaxValue, decimal.One, decimal.MinusOne };
    }
}