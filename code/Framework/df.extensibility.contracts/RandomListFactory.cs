// --------------------------------------------------------------------------------
// <copyright file="RandomListFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;

    public abstract class RandomListFactory<TValue>
                : RandomFactory<TValue, IListFactoryConfiguration<TValue>>
            where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue>
    {
        public override TValue CreateValue()
        {
            throw new NotImplementedException();
        }
    }
}