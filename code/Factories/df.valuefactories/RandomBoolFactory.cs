// --------------------------------------------------------------------------------
// <copyright file="RandomBoolFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories
{
    using Df.Extensibility;
    using Df.Stochastic;
    using static Constants;

    [ValueFactory("bool", "Generates random bool(ean) values", typeof(bool), typeof(RandomBoolFactory))]
    public sealed class RandomBoolFactory
        : RandomFactory<bool, RandomBoolConfiguration>,
        IConfigurator
    {
        public IValueFactoryConfiguration CreateConfiguration() => new RandomBoolConfiguration(BOOL_TRUEWEIGHT, BOOL_FALSEWEIGHT);

        public override bool CreateValue() => Random.NextBoolean(Configuration.TrueWeight, Configuration.FalseWeight);
    }
}