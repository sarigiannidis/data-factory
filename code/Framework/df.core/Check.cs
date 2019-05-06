// --------------------------------------------------------------------------------
// <copyright file="Check.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df
{
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerStepThrough]
    internal static class Check
    {
        public static T Equals<T>(string paramName, T value, T operand)
        {
            if (Comparer<T>.Default.Compare(value, operand) != 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"The value must be equal to {operand}.");
            }

            return value;
        }

        public static T GreaterThan<T>(string paramName, T value, T operand)
        {
            if (Comparer<T>.Default.Compare(value, operand) <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"The value must be in the range ({operand}, +∞).");
            }

            return value;
        }

        public static T GreaterThanOrEqual<T>(string paramName, T value, T operand)
        {
            if (Comparer<T>.Default.Compare(value, operand) < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"The value must be in the range [{operand}, +∞).");
            }

            return value;
        }

        public static void IfNotThrow<T>(Func<bool> predicate, string message, params object[] args)
                    where T : Exception
        {
            if (!predicate())
            {
                throw (T)Activator.CreateInstance(typeof(T), new object[] { message.FormatInvariant(args) });
            }
        }

        /// <summary>
        /// Checks that the value of the parameter is in the closed interval [<paramref name="min"/>, <paramref name="max"/>].
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="min">The inclusive minimum value.</param>
        /// <param name="max">The inclusive maximum value.</param>
        /// <returns>The value of  <paramref name="value"/>.</returns>
        public static T InClosedInterval<T>(string paramName, T value, T min, T max)
        {
            if (Comparer<T>.Default.Compare(value, min) < 0 || Comparer<T>.Default.Compare(value, max) > 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"The value must be in the range [{min}, {max}].");
            }

            return value;
        }

        /// <summary>
        /// Checks that the value of the parameter is in the open interval (<paramref name="min"/>, <paramref name="max"/>).
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="min">The inclusive minimum value.</param>
        /// <param name="max">The inclusive maximum value.</param>
        /// <returns>The value of  <paramref name="value"/>.</returns>
        public static T InOpenInterval<T>(string paramName, T value, T min, T max)
        {
            if (Comparer<T>.Default.Compare(value, min) <= 0 || Comparer<T>.Default.Compare(value, max) >= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"The value must be in the range ({min}, {max}).");
            }

            return value;
        }

        public static T LessThan<T>(string paramName, T value, T operand)
        {
            if (Comparer<T>.Default.Compare(value, operand) >= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"The value must be in the range (-∞, {operand}).");
            }

            return value;
        }

        public static T LessThanOrEqual<T>(string paramName, T value, T operand)
        {
            if (Comparer<T>.Default.Compare(value, operand) > 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"The value must be in the range (-∞, {operand}].");
            }

            return value;
        }

        public static T NotNull<T>(string paramName, T value)
        {
            if (EqualityComparer<T>.Default.Equals(value, default))
            {
                throw new ArgumentNullException(paramName, $"The value of \"{paramName}\" may not be null (or default).");
            }

            return value;
        }

        public static string NotNull(string paramName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(paramName, $"The value of \"{paramName}\" may not be null or white space.");
            }

            return value;
        }

        public static T NotNull<T>(string paramName, IOptions<T> options)
            where T : class, new()
        {
            if (options == default)
            {
                throw new ArgumentNullException(paramName, $"\"{paramName}\" may not be null.");
            }

            if (options.Value == default)
            {
                throw new ArgumentNullException(paramName, $"\"{paramName}.Value\" may not be null.");
            }

            return options.Value;
        }
    }
}