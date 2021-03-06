﻿// --------------------------------------------------------------------------------
// <copyright file="TestHandlerTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Tests
{
    using Df.Options;
    using Xunit;
    using Xunit.Abstractions;

    public sealed class TestHandlerTest
        : OptionsHandlerTest<TestOptions>
    {
        public TestHandlerTest(ITestOutputHelper output, DfFixture fixture)
            : base(output, fixture)
        {
        }

        [Fact]
        public void GoodConnection()
        {
            var options = new TestOptions
            {
                ConnectionString = Fixture.ConnectionString,
            };
            Handle(options);
        }
    }
}