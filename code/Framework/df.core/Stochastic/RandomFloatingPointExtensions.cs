// --------------------------------------------------------------------------------
// <copyright file="RandomFloatingPointExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Stochastic
{
    using System;
    using System.Diagnostics;

    public static class RandomFloatingPointExtensions
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [ThreadStatic]
        private static int _Inext = 0;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [ThreadStatic]
        private static int _Inextp = 0;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [ThreadStatic]
        private static int[] _SeedArray;

        public static double NextDouble(this IRandom random) =>
            random.NextPercentage();

        public static double NextPercentage(this IRandom random) =>
            InternalSample(random) * 4.6566128752457969E-10;

        public static float NextSingle(this IRandom random) =>
            (float)random.NextPercentage();

        private static void Initialize(IRandom random)
        {
            if (_SeedArray != null)
            {
                return;
            }

            var seedArray = new int[56];
            var seed = random.NextInt32();
            var num2 = 161803398 - ((seed == -2147483648) ? 2147483647 : Math.Abs(seed));
            seedArray[55] = num2;
            var num3 = 1;
            for (var i = 1; i < 55; i++)
            {
                var num4 = (21 * i) % 55;
                seedArray[num4] = num3;
                num3 = num2 - num3;
                if (num3 < 0)
                {
                    num3 += 2147483647;
                }

                num2 = seedArray[num4];
            }

            for (var j = 1; j < 5; j++)
            {
                for (var k = 1; k < 56; k++)
                {
                    seedArray[k] -= seedArray[1 + ((k + 30) % 55)];
                    if (seedArray[k] < 0)
                    {
                        seedArray[k] += 2147483647;
                    }
                }
            }

            _Inext = 0;
            _Inextp = 21;
            _SeedArray = seedArray;
        }

        private static int InternalSample(IRandom random)
        {
            Initialize(random);
            var num = _Inext;
            var num2 = _Inextp;
            if (++num >= 56)
            {
                num = 1;
            }

            if (++num2 >= 56)
            {
                num2 = 1;
            }

            var num3 = _SeedArray[num] - _SeedArray[num2];
            if (num3 == 2147483647)
            {
                num3--;
            }

            if (num3 < 0)
            {
                num3 += 2147483647;
            }

            _SeedArray[num] = num3;
            _Inext = num;
            _Inextp = num2;
            return num3;
        }
    }
}