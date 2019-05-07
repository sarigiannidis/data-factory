// --------------------------------------------------------------------------------
// <copyright file="IListFactoryConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using Df.Numeric;
    using System;

    public interface IListFactoryConfiguration<TValue>
       : IValueFactoryConfiguration
   where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue>
    {
        WeightedValueCollection<TValue> WeightedValues { get; }
    }
}