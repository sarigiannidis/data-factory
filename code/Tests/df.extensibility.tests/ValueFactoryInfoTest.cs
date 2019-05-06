// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryInfoTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.SqlServer.Types;
    using System;
    using Xunit;
    using Xunit.Abstractions;

    public sealed class ValueFactoryInfoTest
        : ExtensibilityTestBase
    {
        private const int REPETITIONS = 100;

        public ValueFactoryInfoTest(ITestOutputHelper output, ExtensibilityFixture fixture)
            : base(output, fixture)
        {
        }

        [Theory]
        [InlineData(typeof(byte))]
        [InlineData(typeof(byte[]))]
        [InlineData(typeof(DateTime))]
        [InlineData(typeof(DateTimeOffset))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(double))]
        [InlineData(typeof(float))]
        [InlineData(typeof(Guid))]
        [InlineData(typeof(int))]
        [InlineData(typeof(long))]
        [InlineData(typeof(short))]
        [InlineData(typeof(string))]
        [InlineData(typeof(TimeSpan))]
        [InlineData(typeof(SqlGeometry))]
        [InlineData(typeof(SqlGeography))]
        [InlineData(typeof(SqlHierarchyId))]
        public void GenerateValues(Type type)
        {
            var manager = Fixture.ServiceProvider.GetService<IValueFactoryManager>();
            manager.Initialize();
            Assert.NotNull(manager.ValueFactoryInfos);
            Output.WriteLine("There are now {0} {1}.", manager.ValueFactoryInfos.Count, nameof(manager.ValueFactoryInfos));
            Assert.NotEmpty(manager.ValueFactoryInfos);

            var filtered = manager.ValueFactoryInfos.FilterByType(type);
            Output.WriteLine("{0} of these create values of type {1}.", filtered.Count, type.Name);

            Assert.NotNull(filtered);
            Assert.NotEmpty(filtered);

            foreach (var factoryInfo in filtered)
            {
                Output.WriteLine("Generating with: {0}", factoryInfo.Name);
                Assert.NotNull(factoryInfo);
                var valueFactory = factoryInfo.ValueFactory;
                Assert.NotNull(valueFactory);
                var configuration = factoryInfo.Configurator.CreateConfiguration();
                Assert.NotNull(configuration);
                valueFactory.Configuration = configuration;

                for (var i = 0; i < REPETITIONS; i++)
                {
                    dynamic result = valueFactory.CreateValue();
                    if (configuration.ContainsKey("MinValue"))
                    {
                        dynamic min = configuration["MinValue"];
                        dynamic max = configuration["MaxValue"];
                        _ = Output.WriteLine("min: {0}, max: {1}, result: {2}", min, max, result);
                        _ = Assert.InRange(result, min, max);
                    }
                    else
                    {
                        _ = Output.WriteLine("{0}", result);
                    }
                }
            }
        }
    }
}