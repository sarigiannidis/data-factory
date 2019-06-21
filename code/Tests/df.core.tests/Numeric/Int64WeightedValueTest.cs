// --------------------------------------------------------------------------------
// <copyright file="Int64WeightedValueTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Numeric.Tests
{
    using System.Collections.Generic;

    public class Int64WeightedValueTest
        : WeightedValueTest<long>
    {
        public override IEnumerable<long> Range() => new[] { long.MinValue, 0L, long.MaxValue };
    }
}