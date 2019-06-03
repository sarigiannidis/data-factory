// --------------------------------------------------------------------------------
// <copyright file="HashCodeExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace System
{
    using Df;
    using System.Collections.Generic;

    public static class HashCodeExtensions
    {
        public static void AddRange<T>(this HashCode hashCode, IEnumerable<T> collection)
        {
            foreach (var value in Check.NotNull(nameof(collection), collection))
            {
                hashCode.Add(value);
            }
        }
    }
}