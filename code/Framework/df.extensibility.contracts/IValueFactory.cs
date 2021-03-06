﻿// --------------------------------------------------------------------------------
// <copyright file="IValueFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    public interface IValueFactory
    {
        IValueFactoryConfiguration Configuration { get; set; }

        ValueFactoryKinds Kind { get; }

        object CreateValue();
    }
}