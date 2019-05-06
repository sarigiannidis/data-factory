// --------------------------------------------------------------------------------
// <copyright file="Partitioner.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Numeric
{
    using System;
    using System.Collections.Generic;

    public static class Partitioner
    {
        public static IEnumerable<Range> Partitions(int partitionSize, int count)
        {
            for (var partitionIndex = 0; partitionIndex < Math.Ceiling(count / (double)partitionSize); partitionIndex++)
            {
                yield return new Range(partitionIndex * partitionSize, Math.Min((partitionIndex + 1) * partitionSize, count));
            }
        }
    }
}