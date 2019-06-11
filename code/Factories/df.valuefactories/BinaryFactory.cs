// --------------------------------------------------------------------------------
// <copyright file="BinaryFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories
{
    using Df.Extensibility;
    using Df.Stochastic;
    using static Constants;

    [ValueFactory("binary", "Generates random byte arrays", typeof(byte[]), typeof(BinaryFactory))]
    public sealed class BinaryFactory
    : RandomFactory<byte[], BinaryConfiguration>,
        IConstrainableConfigurator
    {
        public IValueFactoryConfiguration CreateConfiguration() => new BinaryConfiguration(BINARY_MIN, BINARY_MAX);

        public IValueFactoryConfiguration CreateConfiguration(ConfiguratorConstraints configuratorConstraints)
        {
            var maxLength = configuratorConstraints?.MaxLength ?? 0;
            var length = maxLength >= 0 ? maxLength : REGEX_LENGTH;
            return new BinaryConfiguration(length, length);
        }

        public override byte[] CreateValue() => Random.NextBytes(Random.NextInt64(Configuration.MinLength, Configuration.MaxLength));
    }
}