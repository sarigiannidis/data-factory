// --------------------------------------------------------------------------------
// <copyright file="GuidFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories
{
    using Df.Extensibility;
    using System;

    [ValueFactory("guid", "Generates GUIDs", typeof(Guid), typeof(GuidFactory))]
    public sealed class GuidFactory
        : IValueFactory,
        IConfigurator
    {
        public IValueFactoryConfiguration Configuration { get; set; }

        public bool IsRandom =>
            true;

        public IValueFactoryConfiguration CreateConfiguration() =>
            ValueFactoryConfiguration.Empty;

        public object CreateValue() =>
            Guid.NewGuid();
    }
}