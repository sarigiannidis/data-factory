// --------------------------------------------------------------------------------
// <copyright file="CharWeightedValueTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Numeric.Tests
{
    using System.Collections.Generic;

    public class CharWeightedValueTest
        : WeightedValueTest<char>
    {
        public override IEnumerable<char> Range() => new char[] { char.MinValue, char.MaxValue };
    }
}