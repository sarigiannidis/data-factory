// --------------------------------------------------------------------------------
// <copyright file="ConfiguratorConstraints.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;

    public class ConfiguratorConstraints
    {
        public static ConfiguratorConstraints Empty => null;

        public object IncrementValue { get; set; }

        public int MaxLength { get; set; }

        public object SeedValue { get; set; }

        public Type Type { get; set; }
    }
}