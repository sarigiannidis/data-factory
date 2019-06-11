// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryInfoTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility.Tests
{
    using Df.IO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using Xunit;
    using Xunit.Abstractions;

    public sealed class ValueFactoryInfoTest
    {
        private const int REPETITIONS = 100;

        public ITestOutputHelper Output { get; }

        public ValueFactoryInfoTest(ITestOutputHelper output) => Output = output;

        public static IEnumerable<object[]> GetValueFactoryInfos()
        {
            const string APP_SETTINGS_FILE = "settings.json";
            const string SECTION_EXTENSIBILITY = "ValueFactoryManagerOptions";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(PathUtility.CurrentDirectory)
                .AddJsonFile(APP_SETTINGS_FILE, false, true)
                .Build();
            var services = new ServiceCollection()
                .AddDfExtensibility(configuration.GetSection(SECTION_EXTENSIBILITY))
                .AddLogging(ConfigureLogging);
            using var serviceProvider = services.BuildServiceProvider();
            var manager = serviceProvider.GetService<IValueFactoryManager>();
            manager.Initialize();
            foreach (var item in manager.ValueFactoryInfos)
            {
                yield return new object[] { item };
            }
        }

        [Theory]
        [MemberData(nameof(GetValueFactoryInfos))]
        public void GenerateValuesTest(IValueFactoryInfo factoryInfo)
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
                    Output.WriteLine("min: {0}, max: {1}, result: {2}", min, max, result);
                    Assert.InRange(result, min, max);
                }
                else if (result is null)
                {
                    Output.WriteLine("<NULL>");
                }
                else
                {
                    Output.WriteLine("{0}", result);
                }
            }
        }

        private static void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            _ = loggingBuilder.AddDebug().SetMinimumLevel(LogLevel.Trace);
            _ = loggingBuilder.AddEventSourceLogger();
        }
    }
}