// --------------------------------------------------------------------------------
// <copyright file="RandomTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Stochastic.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class RandomTest<T>
        : IClassFixture<RandomFixture<T>>
        where T : IRandom, new()
    {
        private const double REPETITIONS = 1e5;

        public RandomFixture<T> Fixture { get; }

        public ITestOutputHelper Output { get; }

        protected RandomTest(ITestOutputHelper output, RandomFixture<T> fixture)
        {
            Output = output;
            Fixture = fixture;
        }

        public static IEnumerable<object[]> GetRanges() =>
            new object[][]
            {
                new object[] { 0, 1 },
                new object[] { -1, 1 },
                new object[] { 0, 10 },
                new object[] { 10, 20 },
                new object[] { -10, 0 },
                new object[] { -20, -10 },
            };

        [Theory]
        [MemberData(nameof(GetRanges))]
        public void HitAllTheValuesInTheRange(int min, int max)
        {
            var random = Fixture.Random;
            for (var i = min; i < max; i++)
            {
                var found = false;
                for (var j = 0; j < REPETITIONS; j++)
                {
                    var result = random.NextInt32(min, max);
                    Output.WriteLine($"[{min}, {max}): expecting {i}, received {result}.");
                    if (result == i)
                    {
                        found = true;
                        break;
                    }
                }

                Assert.True(found, $"Failed to find value {i} in the range [{min}, {max}).");
            }
        }

        [Theory]
        [MemberData(nameof(GetRanges))]
        public void MaxInclusive(int min, int max)
        {
            var minResult = int.MaxValue;
            var maxResult = int.MinValue;
            var random = Fixture.Random;
            for (var i = 0; i < REPETITIONS; i++)
            {
                var result = random.NextInt32(min, max);
                Assert.NotEqual(max, result);
                minResult = Math.Min(minResult, result);
                maxResult = Math.Max(maxResult, result);
            }

            Output.WriteLine("{0}-{1}: {2}-{3}", min, max, minResult, maxResult);
        }

        [Theory]
        [MemberData(nameof(GetRanges))]
        public void MinInclusive(int min, int max)
        {
            var minResult = int.MaxValue;
            var maxResult = int.MinValue;
            var random = Fixture.Random;
            for (var i = 0; i < REPETITIONS; i++)
            {
                var result = random.NextInt32(min, max);
                if (result == min)
                {
                    return;
                }

                minResult = Math.Min(minResult, result);
                maxResult = Math.Max(maxResult, result);
            }

            Output.WriteLine("{0}-{1}: {2}-{3}", min, max, minResult, maxResult);
            Assert.False(true);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 5)]
        [InlineData(5, 1)]
        [InlineData(1, 50)]
        [InlineData(50, 1)]
        [InlineData(1, 1000)]
        [InlineData(1000, 1)]
        public void NextBoolean(float trueWeight, float falseWeight)
        {
            var random = Fixture.Random;
            var countTrue = 0d;
            for (var i = 0; i < REPETITIONS; i++)
            {
                countTrue += random.NextBoolean(trueWeight, falseWeight) ? 1 : 0;
            }

            var expectedTrue = trueWeight == falseWeight ? 0.5 : trueWeight / (trueWeight + falseWeight);
            var actualTrue = countTrue / REPETITIONS;
            var expectedFalse = 1 - expectedTrue;
            var actualFalse = 1 - actualTrue;
            Output.WriteLine(
                "TRUE: {0:0.000} ({1:0.000}) => {2:0.000}; FALSE: {3:0.000} ({4:0.000}) => {5:0.000}",
                trueWeight,
                expectedTrue,
                actualTrue,
                falseWeight,
                expectedFalse,
                actualFalse);
            Assert.True(Math.Round(expectedTrue - actualTrue, 2) == 0);
            Assert.True(Math.Round(expectedFalse - actualFalse, 2) == 0);
        }

        [Fact]
        public void NextBytes()
        {
            var random = Fixture.Random;
            var b0 = new byte[256];
            random.NextBytes(b0);
            _ = random.NextBytes(256);
        }

        [Fact]
        public void NextPercentage()
        {
            var minResult = 1d;
            var maxResult = 0d;
            var random = Fixture.Random;
            for (var i = 0; i < REPETITIONS; i++)
            {
                var result = random.NextPercentage();
                minResult = Math.Min(minResult, result);
                maxResult = Math.Max(maxResult, result);
            }

            Assert.InRange(minResult, 0, 1);
            Assert.InRange(maxResult, 0, 1);
        }

        [Fact]
        public void PercentageIsNeverEqualToOne()
        {
            var random = Fixture.Random;
            for (var i = 0; i < REPETITIONS; i++)
            {
                var result = random.NextPercentage();
                Assert.NotEqual(1f, result);
            }
        }
    }
}