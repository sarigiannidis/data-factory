// --------------------------------------------------------------------------------
// <copyright file="RandomCharFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories
{
    using Df.Extensibility;
    using static Constants;

    [ValueFactory("string-random", "Generates random string values based on a regex", typeof(char), typeof(RandomCharFactory))]
    public sealed class RandomCharFactory
        : RandomStringFactory
    {
        public override IValueFactoryConfiguration CreateConfiguration(ConfiguratorConstraints configuratorConstraints) =>
            new RandomStringConfiguration(DEFAULT_CHAR_PATTERN);
    }
}