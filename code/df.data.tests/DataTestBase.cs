// --------------------------------------------------------------------------------
// <copyright file="DataTestBase.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Tests
{
    using Xunit;
    using Xunit.Abstractions;

    public abstract class DataTestBase
        : IClassFixture<DataFixture>
    {
        public DataFixture Fixture { get; }

        public ITestOutputHelper Output { get; }

        public DataTestBase(ITestOutputHelper output, DataFixture fixture)
        {
            Output = output;
            Fixture = fixture;
        }
    }
}