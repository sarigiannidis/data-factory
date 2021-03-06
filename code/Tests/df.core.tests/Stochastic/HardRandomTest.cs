﻿// --------------------------------------------------------------------------------
// <copyright file="HardRandomTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Stochastic.Tests
{
    using Xunit.Abstractions;

    public sealed class HardRandomTest
    : RandomTest<HardRandom>
    {
        public HardRandomTest(ITestOutputHelper output, RandomFixture<HardRandom> fixture)
            : base(output, fixture)
        {
        }
    }
}