﻿// --------------------------------------------------------------------------------
// <copyright file="IValueFactoryConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;
    using System.Collections.Generic;

    public interface IValueFactoryConfiguration
        : IDictionary<string, object>,
        IEquatable<IValueFactoryConfiguration>
    {
    }
}