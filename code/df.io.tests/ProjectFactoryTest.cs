// --------------------------------------------------------------------------------
// <copyright file="ProjectFactoryTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Tests
{
    using Xunit;
    using Xunit.Abstractions;

    public sealed class ProjectFactoryTest
        : IoTestBase
    {
        public ProjectFactoryTest(ITestOutputHelper output, IoFixture fixture)
            : base(output, fixture)
        {
        }

        [Fact]
        public void CreateAProject()
        {
            var project = CreateProject();
            IoAssert.AsExpected(project);
        }
    }
}