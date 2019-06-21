// --------------------------------------------------------------------------------
// <copyright file="SingleWeightedValueTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Numeric.Tests
{
    using System.Collections.Generic;

    public class SingleWeightedValueTest
        : WeightedValueTest<float>
    {
        public override IEnumerable<float> Range() => new[] { float.NaN, float.MinValue, float.MaxValue, float.PositiveInfinity, float.NegativeInfinity, 0 };
    }
}