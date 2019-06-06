// --------------------------------------------------------------------------------
// <copyright file="NewOptions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Options
{
    using CommandLine;
    using CommandLine.Text;
    using System.Collections.Generic;

    [Verb("new", HelpText = "Creates a new project.")]
    public sealed class NewOptions
    {
        [Usage]
        public static IEnumerable<Example> Examples => new List<Example>
          {
                new Example("Create a new project.", new NewOptions { ConnectionString = "TESTDB" }),
          };

        [Option('c', "connection-string", Required = true, HelpText = "The connection string for this project.")]
        public string ConnectionString { get; set; }

        [Option('n', "name", Required = true, HelpText = "The name of the project.")]
        public string Name { get; set; }
    }
}