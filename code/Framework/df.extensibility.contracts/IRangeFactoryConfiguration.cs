// --------------------------------------------------------------------------------
// <copyright file="IRangeFactoryConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;

    public interface IRangeFactoryConfiguration<TValue>
    : IValueFactoryConfiguration
    where TValue : struct, IComparable, IComparable<TValue>, IEquatable<TValue>
    {
        TValue MaxValue { get; }

        TValue MinValue { get; }
    }
}