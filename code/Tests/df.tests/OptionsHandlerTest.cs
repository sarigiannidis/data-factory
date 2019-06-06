// --------------------------------------------------------------------------------
// <copyright file="OptionsHandlerTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Tests
{
    using Df.Handlers;
    using Df.Options;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit.Abstractions;

    public abstract class OptionsHandlerTest<TOptions>
        : DfTestBase
    {
        private IHandler<TOptions> Handler { get; }

        private IHandler<NewOptions> NewOptionsHandler { get; }

        protected OptionsHandlerTest(ITestOutputHelper output, DfFixture fixture)
                    : base(output, fixture)
        {
            Handler = Check.NotNull(nameof(Handler), Fixture.ServiceProvider.GetService<IHandler<TOptions>>());
            NewOptionsHandler = Fixture.ServiceProvider.GetService<IHandler<NewOptions>>();
        }

        protected void Handle(TOptions options) => Handler.Handle(options);

        protected void CreateProjectFile(string fileName)
        {
            var options = new NewOptions
            {
                ConnectionString = Fixture.ConnectionString,
                Name = fileName,
            };
            NewOptionsHandler.Handle(options);
        }
    }
}