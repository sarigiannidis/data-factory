// --------------------------------------------------------------------------------
// <copyright file="IValueFactoryInfo.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;

    public interface IValueFactoryInfo
    {
        IConfigurator Configurator { get; }

        string Description { get; }

        string Name { get; }

        string Path { get; }

        IValueFactory ValueFactory { get; }

        Type ValueType { get; }
    }
}