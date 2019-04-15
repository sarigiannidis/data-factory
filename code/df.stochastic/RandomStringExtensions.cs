// --------------------------------------------------------------------------------
// <copyright file="RandomStringExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Stochastic
{
    using Df.Stochastic.Fare;
    using System.Collections.Generic;

    public static class RandomStringExtensions
    {
        public static IEnumerable<string> NextStrings(this IRandom random, string expression)
        {
            var xeger = new Xeger(expression, random);
            while (true)
            {
                yield return xeger.Generate();
            }
        }
    }
}