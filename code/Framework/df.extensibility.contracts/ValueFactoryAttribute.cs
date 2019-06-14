// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryAttribute.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ValueFactoryAttribute
        : Attribute
    {
        public Type DefaultConfigurationFactoryType { get; }

        public string Description { get; }

        public string Name { get; }

        public Type ValueType { get; }

        public ValueFactoryAttribute(string name, string description, Type valueType, Type defaultConfigurationFactoryType)
        {
            ValueType = Check.NotNull(nameof(valueType), valueType);
            Description = Check.NotNull(nameof(description), description);
            Name = Check.NotNull(nameof(name), name);
            DefaultConfigurationFactoryType = Check.NotNull(nameof(defaultConfigurationFactoryType), defaultConfigurationFactoryType);
            Check.IfNotThrow<ArgumentException>(() => typeof(IValueFactoryConfiguration).IsAssignableFrom(defaultConfigurationFactoryType), Messages.EX_DOES_NOT_IMPLEMENT, nameof(defaultConfigurationFactoryType), nameof(IValueFactoryConfiguration));
        }
    }
}