// --------------------------------------------------------------------------------
// <copyright file="RandomFixture.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Stochastic.Tests
{
    using System;
    using System.Diagnostics;

    public sealed class RandomFixture<T>
        : IDisposable
        where T : IRandom, new()
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Disposed;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T _Random = new T();

        public IRandom Random =>
            _Random;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                return;
            }

            if (disposing)
            {
                var disposable = _Random as IDisposable;
                disposable?.Dispose();
                _Random = default;
            }

            _Disposed = true;
        }
    }
}