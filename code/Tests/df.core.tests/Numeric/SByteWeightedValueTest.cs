// --------------------------------------------------------------------------------
// <copyright file="SByteWeightedValueTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Numeric.Tests
{
    using Df.Stochastic;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class SByteWeightedValueTest
        : WeightedValueTest<sbyte>
    {
        public override IEnumerable<sbyte> Range() => new sbyte[] { sbyte.MinValue, sbyte.MaxValue, 0 };
    }
}