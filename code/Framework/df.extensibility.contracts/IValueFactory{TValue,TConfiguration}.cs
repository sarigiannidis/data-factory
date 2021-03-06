﻿// --------------------------------------------------------------------------------
// <copyright file="IValueFactory{TValue,TConfiguration}.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    public interface IValueFactory<TValue, TConfiguration>
        : IValueFactory
        where TConfiguration : IValueFactoryConfiguration
    {
        new TConfiguration Configuration { get; set; }

        new TValue CreateValue();
    }
}