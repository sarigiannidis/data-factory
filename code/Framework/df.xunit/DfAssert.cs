// --------------------------------------------------------------------------------
// <copyright file="DfAssert.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Xunit
{
    using System;
    using System.Collections.Generic;

    public static class DfAssert
    {
        public static void GoodId(int id)
        {
            GreaterThan(id, 0);
            LessThan(id, int.MaxValue);
        }

        public static void GreaterThan<T>(T actualValue, T operand) => Assert.True(Comparer<T>.Default.Compare(actualValue, operand) > 0);

        public static void GreaterThanOrEqual<T>(T actualValue, T operand) => Assert.True(Comparer<T>.Default.Compare(actualValue, operand) >= 0);

        public static void InClosedInterval<T>(T actualValue, T min, T max) => Assert.True(Comparer<T>.Default.Compare(actualValue, min) >= 0
                && Comparer<T>.Default.Compare(actualValue, max) <= 0);

        public static void InOpenInterval<T>(T actualValue, T min, T max) => Assert.True(Comparer<T>.Default.Compare(actualValue, min) > 0
                && Comparer<T>.Default.Compare(actualValue, max) < 0);

        public static void LessThan<T>(T actualValue, T operand) => Assert.True(Comparer<T>.Default.Compare(actualValue, operand) < 0);

        public static void LessThanOrEqual<T>(T actualValue, T operand) => Assert.True(Comparer<T>.Default.Compare(actualValue, operand) <= 0);

        public static void NotEmpty(string str) => Assert.False(string.IsNullOrWhiteSpace(str));

        public static void Past(DateTimeOffset dateTimeOffset)
        {
            Assert.True(DateTimeOffset.Now >= dateTimeOffset);
            Assert.True(dateTimeOffset > DateTimeOffset.MinValue);
        }
    }
}