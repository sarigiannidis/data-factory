// --------------------------------------------------------------------------------
// <copyright file="HashUtility.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    internal static class HashUtility
    {
        public static string ComputeHash(Stream stream) => ComputeSHA512Hash(stream);

        public static string ComputeSHA1Hash(Stream stream) => ComputeHash(stream, SHA1.Create);

        public static string ComputeSHA256Hash(Stream stream) => ComputeHash(stream, SHA256.Create);

        public static string ComputeSHA384Hash(Stream stream) => ComputeHash(stream, SHA384.Create);

        public static string ComputeSHA512Hash(Stream stream) => ComputeHash(stream, SHA512.Create);

        private static string ComputeHash(Stream stream, Func<HashAlgorithm> hashAlgorithmFactory)
        {
            using var hashAlgorithm = hashAlgorithmFactory();
            var bytes = hashAlgorithm.ComputeHash(stream);
            var chars = new char[bytes.Length * 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                chars[i * 2] = GetHexValue(b / 16);
                chars[(i * 2) + 1] = GetHexValue(b % 16);
            }

            return new string(chars, 0, chars.Length);
        }

        private static char GetHexValue(int i) => (char)(i < 10 ? i + 48 : i - 10 + 65);
    }
}