// --------------------------------------------------------------------------------
// <copyright file="AddOptions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Options
{
    using CommandLine;
    using CommandLine.Text;
    using System.Collections.Generic;

    [Verb("add", HelpText = "Add a value factory to a project file.")]
    public sealed class AddOptions
    {
        [Usage]
        public static IEnumerable<Example> Examples => new List<Example>
            {
                new Example("Add a random-double value factory to the project", new AddOptions { Subject = AddSubject.Factory, Name = "double-random", Project = "testdb.json" }),
            };

        [Value(1, MetaName = "name", Required = false, HelpText = "The name of either the factory or the table to add.")]
        public string Name { get; set; }

        [Option('p', "project", Required = false, HelpText = "The path to the project file.")]
        public string Project { get; set; }

        [Value(0, MetaName = "subject", Required = true, HelpText = "What to add. (factory, allfactories, table, alltables)")]
        public AddSubject Subject { get; set; }
    }
}