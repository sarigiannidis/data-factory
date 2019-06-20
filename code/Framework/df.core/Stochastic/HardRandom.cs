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

    public sealed class HardRandom
           : IRandom,
           IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly object _SyncObject = new object();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static RandomNumberGenerator _RandomNumberGenerator;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static volatile int _ReferenceCount;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Disposed;

        public HardRandom()
        {
            lock (_SyncObject)
            {
                _ReferenceCount++;
            }
        }

        public void Dispose()
        {
            if (_Disposed)
            {
                return;
            }

            lock (_SyncObject)
            {
                if (_Disposed)
                {
                    return;
                }

                if (--_ReferenceCount == 0)
                {
                    _RandomNumberGenerator?.Dispose();
                    _RandomNumberGenerator = null;
                }

                _Disposed = true;
            }
        }

        public void NextBytes(byte[] bytes) => GetProvider().GetBytes(bytes);

        private static RandomNumberGenerator GetProvider()
        {
            if (_RandomNumberGenerator is null)
            {
                lock (_SyncObject)
                {
                    if (_RandomNumberGenerator is null && _ReferenceCount > 0)
                    {
                        _RandomNumberGenerator = new RNGCryptoServiceProvider();
                    }
                }
            }

            return _RandomNumberGenerator;
        }
    }
}