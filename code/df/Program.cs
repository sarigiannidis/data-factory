// --------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df
{
    using CommandLine;
    using CommandLine.Text;
    using Df.Handlers;
    using Df.IO;
    using Df.Options;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using static Constants;

    internal static class Program
    {
        public static IConfiguration Configuration { get; }

        public static IServiceProvider ServiceProvider { get; }

        static Program()
        {
            Configuration = CreateConfiguration();
            ServiceProvider = ConfigureServices(Configuration).BuildServiceProvider();
        }

        private static void ConfigureParser(ParserSettings s)
        {
            s.CaseInsensitiveEnumValues = true;
            s.AutoVersion = true;
            s.AutoHelp = true;
        }

        private static IServiceCollection ConfigureServices(IConfiguration configuration) => new ServiceCollection()
            .AddLogging(_ => _.AddDebug().AddEventSourceLogger().SetMinimumLevel(LogLevel.Debug))
            .AddDfData()
            .AddDfExtensibility(configuration.GetSection(SECTION_EXTENSIBILITY))
            .AddDfIo()
            .AddDfProduction()
            .AddDfOptionHandlers(configuration.GetSection(SECTION_PREFERENCES));

        private static IConfiguration CreateConfiguration() => new ConfigurationBuilder()
                .SetBasePath(PathUtility.CurrentDirectory)
                .AddJsonFile(APP_SETTINGS_FILE, false, true)
                .Build();

        private static Action<TOptions> GetHandler<TOptions>() => ServiceProvider.GetService<IHandler<TOptions>>().Handle;

        private static void Main(string[] args)
        {
            var parserResult = new Parser(ConfigureParser)
                .ParseArguments<AddOptions, GenerateOptions, ListOptions, NewOptions, TestOptions>(args);

            void HandleNotParsed(IEnumerable<Error> errors)
            {
                var helpText = HelpText.AutoBuild(parserResult, _ => _, _ => _, true, 100);
                Console.WriteLine(helpText);
            }

            _ = parserResult
                .WithParsed(GetHandler<AddOptions>())
                .WithParsed(GetHandler<GenerateOptions>())
                .WithParsed(GetHandler<ListOptions>())
                .WithParsed(GetHandler<NewOptions>())
                .WithParsed(GetHandler<TestOptions>())
                .WithNotParsed(HandleNotParsed);
        }
    }
}