// --------------------------------------------------------------------------------
// <copyright file="AddHandlerTest.cs" company="Michalis Sarigiannidis">
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

    public sealed class AddHandlerTest
        : OptionsHandlerTest<AddOptions>
    {
        public AddHandlerTest(ITestOutputHelper output, DfFixture fixture)
            : base(output, fixture)
        {
        }

        [Fact]
        public void AddAllFactories()
        {
            var fileName = CreateFileName("add_");
            CreateProjectFile(fileName);

            var options = new AddOptions
            {
                Project = fileName,
                Subject = AddSubject.AllFactories,
            };
            Handler.Handle(options);
            Output.WriteLine(File.ReadAllText(fileName));
        }

        [Fact]
        public void AddAllTables()
        {
            var fileName = CreateFileName("add_");
            CreateProjectFile(fileName);

            var options = new AddOptions
            {
                Project = fileName,
                Subject = AddSubject.AllTables,
            };
            Handler.Handle(options);
            Output.WriteLine(File.ReadAllText(fileName));
        }

        [Fact]
        public void AddFactory()
        {
            var fileName = CreateFileName("add_");
            CreateProjectFile(fileName);

            var options = new AddOptions
            {
                Name = "double-random",
                Project = fileName,
                Subject = AddSubject.Factory,
            };
            Handler.Handle(options);
            Output.WriteLine(File.ReadAllText(fileName));
        }

        [Fact]
        public void AddTable()
        {
            var fileName = CreateFileName("add_");
            CreateProjectFile(fileName);

            var options = new AddOptions
            {
                Name = "SQLTYPE_TABLE_1",
                Project = fileName,
                Subject = AddSubject.Table,
            };
            Handler.Handle(options);
            Output.WriteLine(File.ReadAllText(fileName));
        }
    }
}