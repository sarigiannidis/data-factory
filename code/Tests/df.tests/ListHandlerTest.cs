// --------------------------------------------------------------------------------
// <copyright file="ListHandlerTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Tests
{
    using Df.Options;
    using Xunit;
    using Xunit.Abstractions;

    public sealed class ListHandlerTest
        : OptionsHandlerTest<ListOptions>
    {
        public ListHandlerTest(ITestOutputHelper output, DfFixture fixture)
            : base(output, fixture)
        {
        }

        [Fact]
        public void ListDatabases()
        {
            var options = new ListOptions
            {
                ConnectionString = Fixture.ConnectionString,
                Subject = ListSubject.Databases,
            };
            Handle(options);
        }

        [Fact]
        public void ListFactories() => Handle(new ListOptions { Subject = ListSubject.Factories });

        [Fact]
        public void ListTables()
        {
            var options = new ListOptions
            {
                ConnectionString = Fixture.ConnectionString,
                Subject = ListSubject.Tables,
            };
            Handle(options);
        }
    }
}