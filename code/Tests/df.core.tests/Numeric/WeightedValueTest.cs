// --------------------------------------------------------------------------------
// <copyright file="WeightedValueTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Numeric.Tests
{
    using Df.Stochastic;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public abstract class WeightedValueTest<TValue>
        where TValue : IComparable, IComparable<TValue>, IEquatable<TValue>
    {
        [Fact]
        public void WeightedValueDefaultTest()
        {
            var value = default(WeightedValue<TValue>);
            Assert.Equal(default, value.Value);
            Assert.Equal(0, value.Weight);
        }

        [Fact]
        public void ConstructorTest()
        {
            foreach (var value in Range())
            {
                var weightedValue = new WeightedValue<TValue>(value);
                Assert.Equal(value, weightedValue.Value);
                Assert.Equal(WeightedValue<TValue>.DEFAULTWEIGHT, weightedValue.Weight);
            }
        }

        [Fact]
        public void EqualityTest()
        {
            foreach (var value1 in Range())
            {
                var weightedValue1 = new WeightedValue<TValue>(value1);
                foreach (var value2 in Range())
                {
                    var weightedValue2 = new WeightedValue<TValue>(value2);

                    Assert.Equal(WeightedValue<TValue>.DEFAULTWEIGHT, weightedValue1.Weight);
                    Assert.Equal(WeightedValue<TValue>.DEFAULTWEIGHT, weightedValue2.Weight);
                    var equalValues = EqualityComparer<TValue>.Default.Equals(value1, value2);
                    Assert.True(weightedValue1 == weightedValue2 || !equalValues);
                    Assert.True(weightedValue1.Equals(weightedValue2) || !equalValues);
                    Assert.True(weightedValue1 != weightedValue2 || equalValues);
                    Assert.True(!weightedValue1.Equals(weightedValue2) || equalValues);
                }
            }
        }

        [Fact]
        public void ComparisonTest()
        {
            using var random = new HardRandom();
            var weightComparer = Comparer<float>.Default;
            var comparer = Comparer<TValue>.Default;
            foreach (var value1 in Range())
            {
                var weight1 = random.NextSingle();
                var weightedValue1 = new WeightedValue<TValue>(value1, weight1);
                foreach (var value2 in Range())
                {
                    var weight2 = random.NextSingle();
                    var weightedValue2 = new WeightedValue<TValue>(value2, weight2);
                    if (Equals(value1, value2))
                    {
                        Assert.True(weightedValue1.CompareTo(weightedValue2) == weightComparer.Compare(weight1, weight2));
                    }
                    else
                    {
                        Assert.True(weightedValue1.CompareTo(weightedValue2) == comparer.Compare(value1, value2));
                    }
                }
            }
        }

        public abstract IEnumerable<TValue> Range();
    }
}