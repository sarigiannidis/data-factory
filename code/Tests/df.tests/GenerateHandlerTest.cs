﻿// --------------------------------------------------------------------------------
// <copyright file="GenerateHandlerTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Tests
{
    using Df.Options;
    using System.IO;
    using Xunit;
    using Xunit.Abstractions;

    public sealed class GenerateHandlerTest
        : OptionsHandlerTest<GenerateOptions>
    {
        public GenerateHandlerTest(ITestOutputHelper output, DfFixture fixture)
            : base(output, fixture)
        {
        }

        [Fact]
        public void GenerateDatabase()
        {
            var options = new GenerateOptions
            {
                Subject = GenerateSubject.Database,
                Project = "TESTDB.json",
                DryRun = false,
                DisableTriggers = true,
            };
            Handle(options);
        }

        [Fact]
        [TemporaryFiles]
        public void GenerateFile()
        {
            var outputFileName = Temporary.GetTempFilePath();
            var options = new GenerateOptions
            {
                Subject = GenerateSubject.File,
                Project = "TESTDB.json",
                Output = outputFileName,
            };
            Handle(options);
            Output.WriteLine(File.ReadAllText(outputFileName));
        }
    }
}