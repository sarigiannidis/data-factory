// --------------------------------------------------------------------------------
// <copyright file="RandomStringConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories
{
    using Df.Extensibility;
    using System.Collections.Generic;
    using System.Diagnostics;
    using static Constants;

    [DebuggerDisplay("RegexPattern: {RegexPattern}")]
    public sealed class RandomStringConfiguration
        : ValueFactoryConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string RegexPattern =>
            Get<string>(PROPERTY_REGEX);

        public RandomStringConfiguration(string regexPattern) =>
            Set(PROPERTY_REGEX, regexPattern);

        public RandomStringConfiguration(IDictionary<string, object> properties)
            : base(properties)
        {
        }

        public RandomStringConfiguration()
        {
        }
    }
}