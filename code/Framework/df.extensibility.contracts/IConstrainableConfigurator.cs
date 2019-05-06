// --------------------------------------------------------------------------------
// <copyright file="IConstrainableConfigurator.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    public interface IConstrainableConfigurator
        : IConfigurator
    {
        IValueFactoryConfiguration CreateConfiguration(ConfiguratorConstraints configuratorConstraints);
    }
}