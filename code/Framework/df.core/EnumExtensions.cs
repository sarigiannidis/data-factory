// --------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df
{
    using System;

    public static class EnumExtensions
    {
        public static bool Contains<T>(this T value, T member)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            Check.IfNotThrow<ArgumentException>(() => typeof(T).IsEnum, Messages.CHECK_VALUE, value);
            return ((dynamic)value & member) == member;
        }
    }
}