// --------------------------------------------------------------------------------
// <copyright file="DfTestBase.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Tests
{
    using System;
    using System.Diagnostics;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class DfTestBase
        : IClassFixture<DfFixture>,
        IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ConsoleWriter _ConsoleWriter;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Disposed;

        public DfFixture Fixture { get; }

        public ITestOutputHelper Output { get; }

        protected DfTestBase(ITestOutputHelper output, DfFixture fixture)
        {
            Output = Check.NotNull(nameof(output), output);
            Fixture = Check.NotNull(nameof(fixture), fixture);

            _ConsoleWriter = new ConsoleWriter(output);
            Console.SetOut(_ConsoleWriter);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                return;
            }

            if (disposing)
            {
                _ConsoleWriter?.Dispose();
                _ConsoleWriter = null;
            }

            _Disposed = true;
        }
    }
}