// --------------------------------------------------------------------------------
// <copyright file="ExtensibilityFixture.cs" company="Michalis Sarigiannidis">
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
    using System;

    public sealed class ExtensibilityFixture
        : IDisposable
    {
        private const string APP_SETTINGS_FILE = "settings.json";

        private const string SECTION_EXTENSIBILITY = "ValueFactoryManagerOptions";

        public IConfigurationRoot Configuration { get; }

        public ServiceProvider ServiceProvider { get; private set; }

        public ExtensibilityFixture()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(PathUtil.CurrentDirectory)
            .AddJsonFile(APP_SETTINGS_FILE, false, true);
            Configuration = builder.Build();

            var services = new ServiceCollection();
            _ = services
                .AddDfExtensibility(Configuration.GetSection(SECTION_EXTENSIBILITY))
                .AddLogging(ConfigureLogging);
            ServiceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            ServiceProvider?.Dispose();
            ServiceProvider = null;
        }

        private void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            _ = loggingBuilder.AddDebug().SetMinimumLevel(LogLevel.Trace);
            _ = loggingBuilder.AddEventSourceLogger();
        }
    }
}