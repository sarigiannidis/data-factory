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
            Check.IfNotThrow<ArgumentException>(() => typeof(T).IsEnum, "The value {0} is not a member of an enumerator.", value);
            return ((dynamic)value & member) == member;
        }
    }
}