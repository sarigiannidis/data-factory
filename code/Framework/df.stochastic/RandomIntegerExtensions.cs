// --------------------------------------------------------------------------------
// <copyright file="RandomIntegerExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Stochastic
{
    using System;

    public static class RandomIntegerExtensions
    {
        public static byte NextByte(this IRandom random) =>
            random.NextBytes(1)[0];

        public static byte[] NextBytes(this IRandom random, long byteCount)
        {
            var bytes = new byte[byteCount];
            random.NextBytes(bytes);
            return bytes;
        }

        public static short NextInt16(this IRandom random) =>
            BitConverter.ToInt16(random.NextBytes(2), 0);

        public static int NextInt32(this IRandom random) =>
            BitConverter.ToInt32(random.NextBytes(4), 0);

        public static int NextInt32(this IRandom random, int min, int max) =>
            min + (int)((max - min) * random.NextPercentage());

        public static long NextInt64(this IRandom random) =>
            BitConverter.ToInt64(random.NextBytes(8), 0);

        public static long NextInt64(this IRandom random, long min, long max) =>
            min + (long)((max - min) * random.NextPercentage());

        public static sbyte NextSByte(this IRandom random) =>
            (sbyte)random.NextByte();

        public static ushort NextUInt16(this IRandom random) =>
            BitConverter.ToUInt16(random.NextBytes(2), 0);

        public static uint NextUInt32(this IRandom random) =>
            BitConverter.ToUInt32(random.NextBytes(4), 0);

        public static ulong NextUInt64(this IRandom random) =>
            BitConverter.ToUInt64(random.NextBytes(8), 0);
    }
}