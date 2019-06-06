// --------------------------------------------------------------------------------
// <copyright file="IoTestBase.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Tests
{
    using Df.Extensibility;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class IoTestBase
        : IClassFixture<IoFixture>
    {
        public IoFixture Fixture { get; }

        public ITestOutputHelper Output { get; }

        public IProjectFactory ProjectFactory => Fixture.ServiceProvider.GetService<IProjectFactory>();

        public IProjectManager ProjectManager => Fixture.ServiceProvider.GetService<IProjectManager>();

        public IValueFactoryManager ValueFactoryManager => Fixture.ServiceProvider.GetService<IValueFactoryManager>();

        protected IoTestBase(ITestOutputHelper output, IoFixture fixture)
        {
            Output = output;
            Fixture = fixture;
        }

        protected Project CreateProject() => ProjectFactory.CreateNew(Fixture.ConnectionString);
    }
}