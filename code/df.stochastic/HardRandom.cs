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
        private RNGCryptoServiceProvider _CryptoServiceProvider = new RNGCryptoServiceProvider();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Disposed;

        public void Dispose() =>
            Dispose(true);

        public void NextBytes(byte[] bytes) =>
            _CryptoServiceProvider.GetBytes(bytes);

        private void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    _CryptoServiceProvider?.Dispose();
                    _CryptoServiceProvider = null;
                }

                _Disposed = true;
            }
        }
    }
}