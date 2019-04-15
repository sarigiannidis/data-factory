// --------------------------------------------------------------------------------
// <copyright file="IValueFactoryInfoExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using Df.Io.Descriptive;

    public static class IValueFactoryInfoExtensions
    {
        public static IValueFactoryConfiguration ConfigureForColumn(this IValueFactoryInfo valueFactoryInfo, ColumnDescription columnDescription)
        =>
            valueFactoryInfo.Configurator switch
        {
            IConstrainableConfigurator constrainableConfigurator => constrainableConfigurator.CreateConfiguration(columnDescription.CreateConstraints()),
            _ => valueFactoryInfo.Configurator.CreateConfiguration(),
        };
    }
}