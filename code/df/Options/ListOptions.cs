// --------------------------------------------------------------------------------
// <copyright file="ListOptions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Options
{
    using CommandLine;
    using CommandLine.Text;
    using System.Collections.Generic;

    [Verb("list", HelpText = "Enumerates items.")]
    public sealed class ListOptions
    {
        [Usage]
        public static IEnumerable<Example> Examples =>
           new List<Example>
           {
                new Example("List the databases on a server.", new ListOptions { Subject = ListSubject.Databases, ConnectionString = "demo_server" }),
                new Example("List the registered value factories.", new ListOptions { Subject = ListSubject.Factories }),
                new Example("List the tables in a database.", new ListOptions { Subject = ListSubject.Tables, ConnectionString = "demo_server" }),
           };

        [Option('c', "connection-string", Required = false, HelpText = "The connection string for this project.")]
        public string ConnectionString { get; set; }

        [Value(0, MetaName = "subject", Required = true, HelpText = "What to list.")]
        public ListSubject Subject { get; set; }
    }
}