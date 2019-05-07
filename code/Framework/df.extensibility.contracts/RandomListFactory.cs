// --------------------------------------------------------------------------------
// <copyright file="RandomListFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using Df.Stochastic;
    using System;
    using System.Linq;

    public abstract class RandomListFactory<TValue>
        : RandomFactory<TValue, IListFactoryConfiguration<TValue>>
        where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue>
    {
        protected float SumOfWeights { get; private set; }

        protected RandomListFactory() =>
            ConfigurationChanged += (sender, e) => SumOfWeights = Configuration.WeightedValues.Sum(w => w.Weight);

        public override TValue CreateValue()
        {
            var value = Random.NextDouble() * SumOfWeights;
            foreach (var weightedValue in Configuration.WeightedValues)
            {
                if (value < weightedValue.Weight)
                {
                    return weightedValue.Value;
                }

                value -= weightedValue.Weight;
            }

            throw null;
        }
    }
}