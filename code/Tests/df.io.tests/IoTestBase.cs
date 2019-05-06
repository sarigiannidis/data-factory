// --------------------------------------------------------------------------------
// <copyright file="IoTestBase.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Tests
{
    using Df.Extensibility;
    using Df.Stochastic;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class IoTestBase
        : IClassFixture<IoFixture>,
        IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<string> _Files = new List<string>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Disposed;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected string CreateFileName(string prefix)
        {
            var random = new HardRandom().NextStrings(@"([a-z]|\d){32}").First();
            var fileName = "{0}{1}.json".FormatInvariant(prefix, random);
            _Files.Add(fileName);
            return fileName;
        }

        protected Project CreateProject() =>
            ProjectFactory.CreateNew(Fixture.ConnectionString);

        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                foreach (var fileName in _Files)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }

                _Files.Clear();

                _Disposed = true;
            }
        }
    }
}