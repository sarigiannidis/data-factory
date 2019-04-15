// --------------------------------------------------------------------------------
// <copyright file="TestOptions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Options
{
    using CommandLine;
    using CommandLine.Text;
    using System.Collections.Generic;

    [Verb("test", HelpText = "Test whether a connection is possible.")]
    public sealed class TestOptions
    {
        [Usage]
        public static IEnumerable<Example> Examples =>
            new List<Example>
            {
                new Example("Test the conection", new TestOptions { ConnectionString = "demo_server" }),
            };

        [Option('c', "connection-string", Required = true, HelpText = "The connection string for this project.")]
        public string ConnectionString { get; set; }
    }
}