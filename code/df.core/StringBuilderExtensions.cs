// --------------------------------------------------------------------------------
// <copyright file="StringBuilderExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace System.Text
{
    using System.Globalization;

    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendFormatInvariant(this StringBuilder sb, string format, params object[] args) =>
            sb.AppendFormat(CultureInfo.InvariantCulture, format, args);
    }
}