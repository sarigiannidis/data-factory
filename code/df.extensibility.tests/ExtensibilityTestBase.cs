// --------------------------------------------------------------------------------
// <copyright file="ExtensibilityTestBase.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility.Tests
{
    using Xunit;
    using Xunit.Abstractions;

    public abstract class ExtensibilityTestBase
        : IClassFixture<ExtensibilityFixture>
    {
        public ExtensibilityFixture Fixture { get; }

        public ITestOutputHelper Output { get; }

        public ExtensibilityTestBase(ITestOutputHelper output, ExtensibilityFixture fixture)
        {
            Output = output;
            Fixture = fixture;
        }
    }
}