// --------------------------------------------------------------------------------
// <copyright file="WeightedValueTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1649 // File name should match first type name

namespace Df.Numeric.Tests
{
    using Df.Stochastic;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class BoolWeightedValueTest
        : WeightedValueTest<bool>
    {
        public override IEnumerable<bool> Range() => new[] { true, false };
    }

    public class ByteWeightedValueTest
        : WeightedValueTest<byte>
    {
        public override IEnumerable<byte> Range() => new byte[] { byte.MinValue, byte.MaxValue, 0 };
    }

    public class CharWeightedValueTest
        : WeightedValueTest<char>
    {
        public override IEnumerable<char> Range() => new char[] { char.MinValue, char.MaxValue };
    }

    public class DoubleWeightedValueTest
        : WeightedValueTest<double>
    {
        public override IEnumerable<double> Range() => new[] { double.NaN, double.PositiveInfinity, double.NegativeInfinity, double.Epsilon, double.MinValue, double.MaxValue, 0 };
    }

    public class DecimalWeightedValueTest
        : WeightedValueTest<decimal>
    {
        public override IEnumerable<decimal> Range() => new[] { decimal.Zero, decimal.MinValue, decimal.MaxValue, decimal.One, decimal.MinusOne };
    }

    public class Int32WeightedValueTest
        : WeightedValueTest<int>
    {
        public override IEnumerable<int> Range() => new[] { int.MinValue, 0, int.MaxValue };
    }

    public class Int64WeightedValueTest
        : WeightedValueTest<long>
    {
        public override IEnumerable<long> Range() => new[] { long.MinValue, 0L, long.MaxValue };
    }

    public class SByteWeightedValueTest
        : WeightedValueTest<sbyte>
    {
        public override IEnumerable<sbyte> Range() => new sbyte[] { sbyte.MinValue, sbyte.MaxValue, 0 };
    }

    public class SingleWeightedValueTest
        : WeightedValueTest<float>
    {
        public override IEnumerable<float> Range() => new[] { float.NaN, float.MinValue, float.MaxValue, float.PositiveInfinity, float.NegativeInfinity, 0 };
    }

    public class StringWeightedValueTest
        : WeightedValueTest<string>
    {
        public override IEnumerable<string> Range() => new[] { string.Empty, null, "aa", "zz", "AA", "ZZ" };
    }

    public class UInt32WeightedValueTest
        : WeightedValueTest<uint>
    {
        public override IEnumerable<uint> Range() => new[] { uint.MinValue, uint.MaxValue, 0U };
    }

    public class UInt64WeightedValueTest
        : WeightedValueTest<ulong>
    {
        public override IEnumerable<ulong> Range() => new[] { ulong.MinValue, 0UL, ulong.MaxValue };
    }

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
            var random = new HardRandom();
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

#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type