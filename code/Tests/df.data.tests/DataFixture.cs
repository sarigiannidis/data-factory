// --------------------------------------------------------------------------------
// <copyright file="DataFixture.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Tests
{
    using Df.Data.Meta;
    using Df.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Debug;
    using System;
    using System.Globalization;

    public sealed class DataFixture
    {
        private const string APP_SETTINGS_FILE = "settings.json";

        private static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });

        private readonly string _ConnectionString;

        public IServiceProvider ServiceProvider { get; }

        public DataFixture()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(PathUtility.CurrentDirectory)
            .AddJsonFile(APP_SETTINGS_FILE, false, true);
            var configuration = builder.Build();
            _ConnectionString = configuration
                .GetConnectionString("TestDb")
                .Replace("|DataDirectory|", PathUtility.CurrentDirectory + "\\", false, CultureInfo.InvariantCulture);

            ServiceProvider = ConfigureServices().BuildServiceProvider();
        }

        public MetaDbContext CreateMetaDbContext()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder()
                .UseLoggerFactory(LoggerFactory)
                .UseSqlServer(_ConnectionString);
            return new MetaDbContext(dbContextOptionsBuilder.Options);
        }

        private IServiceCollection ConfigureServices() => new ServiceCollection()
            .AddDfData();
    }
}