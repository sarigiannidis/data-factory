// --------------------------------------------------------------------------------
// <copyright file="IoFixture.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Tests
{
    using Df.IO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Globalization;

    public sealed class IoFixture
        : IDisposable
    {
        private const string APP_SETTINGS_FILE = "settings.json";

        private const string SECTION_EXTENSIBILITY = "ValueFactoryManagerOptions";

        public string ConnectionString { get; }

        public ServiceProvider ServiceProvider { get; private set; }

        public IoFixture()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(PathUtil.CurrentDirectory)
            .AddJsonFile(APP_SETTINGS_FILE, false, true);
            var configuration = builder.Build();
            ConnectionString = configuration
                .GetConnectionString("TestDb")
                .Replace("|DataDirectory|", PathUtil.CurrentDirectory + "\\", false, CultureInfo.InvariantCulture);
            ServiceProvider = ConfigureServices(configuration)
                .BuildServiceProvider();
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

        private IServiceCollection ConfigureServices(IConfiguration configuration) =>
                    new ServiceCollection()
            .AddDfData()
            .AddDfIo()
            .AddDfExtensibility(configuration.GetSection(SECTION_EXTENSIBILITY))
            .AddLogging(ConfigureLogging);
    }
}