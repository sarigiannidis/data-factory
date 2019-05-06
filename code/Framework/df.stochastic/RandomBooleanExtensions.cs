// --------------------------------------------------------------------------------
// <copyright file="RandomBooleanExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Stochastic
{
    public static class RandomBooleanExtensions
    {
        public static bool NextBoolean(this IRandom random) =>
            NextBoolean(random, 1f, 1f);

        public static bool NextBoolean(this IRandom random, float trueWeight, float falseWeight)
        {
            Check.GreaterThanOrEqual(nameof(trueWeight), trueWeight, 0);
            Check.GreaterThanOrEqual(nameof(falseWeight), falseWeight, 0);

            return random.NextDouble() > CalculateThreshold(trueWeight, falseWeight);
        }

        private static float CalculateThreshold(float trueWeight, float falseWeight) =>
            falseWeight == trueWeight
                ? 0.5f
                : falseWeight / (falseWeight + trueWeight);
    }
}