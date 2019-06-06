// --------------------------------------------------------------------------------
// <copyright file="RandomFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using Df.Stochastic;
    using System;
    using System.Diagnostics;

    public abstract class RandomFactory<TValue, TConfiguration>
        : ValueFactory<TValue, TConfiguration>,
        IDisposable
        where TConfiguration : IValueFactoryConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Disposed;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HardRandom _Random = new HardRandom();

        public override bool IsRandom => true;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected IRandom Random => _Random;

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
                _Random?.Dispose();
                _Random = null;
            }

            _Disposed = true;
        }
    }
}