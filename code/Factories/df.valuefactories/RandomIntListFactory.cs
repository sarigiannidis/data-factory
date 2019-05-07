// --------------------------------------------------------------------------------
// <copyright file="RandomIntListFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories
{
    using Df.Extensibility;

    [ValueFactory("int-list", "Picks randomly from a weighted list of integers", typeof(int), typeof(RandomIntListFactory))]
    public sealed class RandomIntListFactory
        : RandomListFactory<int>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ListFactoryConfiguration<int>(new[]
            {
                new WeightedValue<int>(5, 0.1f),
                new WeightedValue<int>(10, 0.2f),
                new WeightedValue<int>(15, 0.3f),
            });
    }
}