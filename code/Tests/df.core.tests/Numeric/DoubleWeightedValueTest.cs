// --------------------------------------------------------------------------------
// <copyright file="DoubleWeightedValueTest.cs" company="Michalis Sarigiannidis">
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

    public class DoubleWeightedValueTest
        : WeightedValueTest<double>
    {
        public override IEnumerable<double> Range() => new[] { double.NaN, double.PositiveInfinity, double.NegativeInfinity, double.Epsilon, double.MinValue, double.MaxValue, 0 };
    }
}