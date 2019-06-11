// --------------------------------------------------------------------------------
// <copyright file="GenerateOptions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Options
{
    using CommandLine;
    using CommandLine.Text;
    using System.Collections.Generic;

    [Verb("generate", HelpText = "Generate data based on a project file.")]
    public sealed class GenerateOptions
    {
        [Usage]
        public static IEnumerable<Example> Examples => new List<Example>
            {
                new Example("Generate a file containing INSERT statements.", new GenerateOptions { Subject = GenerateSubject.File, Project = "testdb.json", Output = "INSERTS.SQL" }),
                new Example("Generate data in a database.", new GenerateOptions { Subject = GenerateSubject.Database, Project = "testdb.json", DisableTriggers = true }),
            };

        [Option('d', "disable-triggers", Default = false, Required = false, HelpText = "Disable triggers to prevent them from executing while inserting data.")]
        public bool DisableTriggers { get; set; }

        [Option("dry-run", Default = false, Required = false, HelpText = "Test the creation of data without actually populating a database.")]
        public bool DryRun { get; set; }

        [Option('o', "output", Required = false, HelpText = "The path to the file where the INSERT statements will be saved.")]
        public string Output { get; set; }

        [Option('n', "project", Required = false, HelpText = "The path to the project file.")]
        public string Project { get; set; }

        [Value(0, MetaName = "subject", Default = "file", Required = true, HelpText = "What to generate.")]
        public GenerateSubject Subject { get; set; }
    }
}