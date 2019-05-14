// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryManagerTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;
    using Xunit.Abstractions;

    public sealed class ValueFactoryManagerTest
        : ExtensibilityTestBase
    {
        public ValueFactoryManagerTest(ITestOutputHelper output, ExtensibilityFixture fixture)
            : base(output, fixture)
        {
        }

        [Fact]
        public void GetService()
        {
            var manager = Fixture.ServiceProvider.GetService<IValueFactoryManager>();
            Assert.NotNull(manager);
        }

        [Fact]
        public void Initialize()
        {
            var manager = Fixture.ServiceProvider.GetService<IValueFactoryManager>();
            manager.Initialize();
        }

        [Fact]
        public void ValueFactoryInfosAreLoaded()
        {
            var manager = Fixture.ServiceProvider.GetService<IValueFactoryManager>();
            manager.Initialize();
            Assert.NotEmpty(manager.ValueFactoryInfos);
            Assert.True(manager.ValueFactoryInfos.Count > 0);
            Assert.All(manager.ValueFactoryInfos, Evaluate);

            static void Evaluate(IValueFactoryInfo valueFactoryInfo)
            {
                Assert.NotNull(valueFactoryInfo);
                Assert.NotNull(valueFactoryInfo.Configurator);
                Assert.NotNull(valueFactoryInfo.ValueFactory);
                Assert.NotNull(valueFactoryInfo.Description);
                Assert.NotNull(valueFactoryInfo.Name);
            }
        }
    }
}