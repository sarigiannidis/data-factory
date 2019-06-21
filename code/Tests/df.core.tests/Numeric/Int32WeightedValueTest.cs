// --------------------------------------------------------------------------------
// <copyright file="Int32WeightedValueTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Numeric.Tests
{
    using System.Collections.Generic;

    public class Int32WeightedValueTest
        : WeightedValueTest<int>
    {
        public override IEnumerable<int> Range() => new[] { int.MinValue, 0, int.MaxValue };
    }
}