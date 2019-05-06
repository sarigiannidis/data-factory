// --------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace System
{
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;

    public static class StringExtensions
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly Encoding DefaultEncoding = Encoding.UTF32;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly IFormatProvider InvariantFormatProvider = CultureInfo.InvariantCulture;

        public static string FormatInvariant(this string format, params object[] args) =>
            string.Format(InvariantFormatProvider, format, args);

        public static string FromBase64(this string value) =>
            DefaultEncoding.GetString(Convert.FromBase64String(value));

        public static string ToBase64(this string value) =>
            Convert.ToBase64String(DefaultEncoding.GetBytes(value));
    }
}