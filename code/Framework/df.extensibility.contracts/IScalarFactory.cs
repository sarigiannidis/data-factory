// --------------------------------------------------------------------------------
// <copyright file="IScalarFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;

    public interface IScalarFactory<TValue>
        : IValueFactory
        where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue>
    {
        new IScalarFactoryConfiguration<TValue> Configuration { get; set; }

        new TValue CreateValue();
    }
}