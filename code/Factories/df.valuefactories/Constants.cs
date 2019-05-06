// --------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories
{
    using System;

    internal static class Constants
    {
        public const int BINARY_MAX = 32;

        public const int BINARY_MIN = 64;

        public const float BOOL_FALSEWEIGHT = 1f;

        public const float BOOL_TRUEWEIGHT = 1f;

        public const byte BYTE_MAX = byte.MaxValue;

        public const byte BYTE_MIN = byte.MinValue;

        public const byte BYTE_STEP = 1;

        public const decimal DECIMAL_MAX = 100000m;

        public const decimal DECIMAL_MIN = -100000m;

        public const decimal DECIMAL_STEP = 1000m;

        public const string DEFAULT_CHAR_PATTERN = "[a-zA-Z0-9]{1}";

        public const string DEFAULT_RANDOM_STRING_PATTERN_FORMAT = "[a-zA-Z0-9]{{{0}}}";

        public const double DOUBLE_MAX = 10000d;

        public const double DOUBLE_MIN = -10000d;

        public const double DOUBLE_STEP = 100d;

        public const float FLOAT_MAX = 1000f;

        public const float FLOAT_MIN = -1000f;

        public const float FLOAT_STEP = 100f;

        public const int INT_MAX = 1000;

        public const int INT_MIN = -1000;

        public const int INT_STEP = 100;

        public const long LONG_MAX = 10000;

        public const long LONG_MIN = -10000;

        public const long LONG_STEP = 1000;

        public const string PROPERTY_FALSE = "false";

        public const string PROPERTY_MAXLENGTH = "maxLength";

        public const string PROPERTY_MINLENGTH = "minLength";

        public const string PROPERTY_REGEX = "Regex";

        public const string PROPERTY_TRUE = "true";

        public const int REGEX_LENGTH = 32;

        public const short SHORT_MAX = 1000;

        public const short SHORT_MIN = -1000;

        public const short SHORT_STEP = 100;

        public static readonly DateTime DATETIME_MAX = DateTime.Now.AddDays(3650);

        public static readonly DateTime DATETIME_MIN = DateTime.Now.AddDays(-3650);

        public static readonly DateTimeOffset DATETIMEOFFSET_MAX = DateTimeOffset.Now.AddDays(36500);

        public static readonly DateTimeOffset DATETIMEOFFSET_MIN = DateTimeOffset.Now.AddDays(-36500);

        public static readonly TimeSpan TIMESPAN_MAX = TimeSpan.FromDays(1);

        public static readonly TimeSpan TIMESPAN_MIN = TimeSpan.FromMinutes(0);

        public static readonly TimeSpan TIMESPAN_STEP = TimeSpan.FromMinutes(5);
    }
}