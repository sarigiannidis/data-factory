// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryInfo.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;
    using System.Diagnostics;

    internal sealed class ValueFactoryInfo
        : IValueFactoryInfo
    {
        // @TODO: Are the IConfigurator instances disposed properly?
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Func<IConfigurator> _ConfiguratorFactory;

        // @TODO: Are the IValueFactory instances disposed properly?
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Func<IValueFactory> _ValueFactoryFactory;

        public IConfigurator Configurator => _ConfiguratorFactory();

        public string Description { get; }

        public string Name { get; }

        public string Path { get; }

        public IValueFactory ValueFactory => _ValueFactoryFactory();

        public Type ValueType { get; }

        public ValueFactoryInfo(
            string name,
            string description,
            string path,
            Type valueType,
            Func<IValueFactory> valueFactoryFactory,
            Func<IConfigurator> configuratorFactory)
        {
            _ConfiguratorFactory = Check.NotNull(nameof(configuratorFactory), configuratorFactory);
            _ValueFactoryFactory = Check.NotNull(nameof(valueFactoryFactory), valueFactoryFactory);
            Description = Check.NotNull(nameof(description), description);
            Name = Check.NotNull(nameof(name), name);
            Path = Check.NotNull(nameof(path), path);
            ValueType = Check.NotNull(nameof(valueType), valueType);
        }
    }
}