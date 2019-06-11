// --------------------------------------------------------------------------------
// <copyright file="RandomDateTimeOffsetFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories
{
    using Df.Stochastic;
    using System;

    public sealed partial class RandomDateTimeOffsetFactory
    {
        public override DateTimeOffset CreateValue() => Configuration.MinValue + TimeSpan.FromTicks((long)(Random.NextPercentage() * (Configuration.MaxValue - Configuration.MinValue).Ticks));
    }
}