// --------------------------------------------------------------------------------
// <copyright file="HardRandom.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Stochastic
{
    using System;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using System.Threading;

    public sealed class HardRandom
        : IRandom,
        IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly object _Sync = new object();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static int _Count;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static RandomNumberGenerator _RandomNumberGenerator;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Disposed;

        public void Dispose() => Dispose(true);

        public HardRandom() => _ = Interlocked.Increment(ref _Count);

        private static RandomNumberGenerator GetProvider()
        {
            if (_RandomNumberGenerator is null)
            {
                lock (_Sync)
                {
                    _RandomNumberGenerator ??= new RNGCryptoServiceProvider();
                }
            }

            return _RandomNumberGenerator;
        }

        public void NextBytes(byte[] bytes) => GetProvider().GetBytes(bytes);

        private void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                return;
            }

            if (disposing && Interlocked.Decrement(ref _Count) == 0)
            {
                lock (_Sync)
                {
                    if (_Count == 0)
                    {
                        _RandomNumberGenerator?.Dispose();
                        _RandomNumberGenerator = null;
                    }
                }
            }

            _Disposed = true;
        }
    }
}