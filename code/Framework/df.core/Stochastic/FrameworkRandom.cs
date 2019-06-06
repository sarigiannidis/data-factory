// --------------------------------------------------------------------------------
// <copyright file="FrameworkRandom.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Stochastic
{
    using System;

    public sealed class FrameworkRandom
        : IRandom
    {
        private Random Random { get; }

        public FrameworkRandom() => Random = new Random(); /* defaults to new Random(Environment.TickCount) */

        public void NextBytes(byte[] bytes) => Random.NextBytes(bytes);
    }
}