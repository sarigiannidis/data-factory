﻿// --------------------------------------------------------------------------------
// <copyright file="RandomStringFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories
{
    using Df.Extensibility;
    using Df.Stochastic.Fare;
    using System;
    using System.Diagnostics;
    using static Constants;

    [ValueFactory("string-random", "Generates random string values based on a regex", typeof(string), typeof(RandomStringFactory))]
    public sealed class RandomStringFactory
        : RandomFactory<string, RandomStringConfiguration>,
        IConstrainableConfigurator
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly object _SyncRoot = new object();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Xeger _Xeger;

        public RandomStringFactory() =>
            ConfigurationChanged += RandomStringFactory_ConfigurationChanged;

        public IValueFactoryConfiguration CreateConfiguration() =>
            CreateConfiguration(ConfiguratorConstraints.Empty);

        public IValueFactoryConfiguration CreateConfiguration(ConfiguratorConstraints configuratorConstraints)
        {
            var maxLength = configuratorConstraints?.MaxLength ?? 0;
            var length = maxLength >= 0 ? maxLength : REGEX_LENGTH;
            var pattern = "[a-zA-Z0-9]{{{0}}}".FormatInvariant(length);
            return new RandomStringConfiguration(pattern);
        }

        public override string CreateValue()
        {
            lock (_SyncRoot)
            {
                return _Xeger.Generate();
            }
        }

        private void RandomStringFactory_ConfigurationChanged(object sender, EventArgs e) =>
            _Xeger = new Xeger(Configuration.RegexPattern, Random);
    }
}