// --------------------------------------------------------------------------------
// <copyright file="OptionsHandlerTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Tests
{
    using Df.OptionHandlers;
    using Df.Options;
    using Df.Stochastic;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using Xunit.Abstractions;

    public abstract class OptionsHandlerTest<TOptions>
        : DfTestBase
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<string> _Files = new List<string>();

        internal IHandler<TOptions> Handler { get; }

        public OptionsHandlerTest(ITestOutputHelper output, DfFixture fixture)
                    : base(output, fixture) =>
            Handler = Check.NotNull(nameof(Handler), Fixture.ServiceProvider.GetService<IHandler<TOptions>>());

        protected static string CreateFileName(string prefix)
        {
            var random = new HardRandom().NextStrings(@"([a-z]|\d){32}").First();
            return "{0}{1}.json".FormatInvariant(prefix, random);
        }

        protected void CreateProjectFile(string fileName)
        {
            _Files.Add(fileName);
            var options = new NewOptions
            {
                ConnectionString = Fixture.ConnectionString,
                Name = fileName,
            };

            Fixture.ServiceProvider.GetService<IHandler<NewOptions>>().Handle(options);

            Output.WriteLine("Created file {0}.", fileName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var fileName in _Files)
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }

                _Files.Clear();
            }

            base.Dispose(disposing);
        }
    }
}