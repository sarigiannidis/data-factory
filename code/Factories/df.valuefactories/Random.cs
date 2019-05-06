// --------------------------------------------------------------------------------
// <copyright file="Random.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1649 // File name should match first type name
namespace Df.ValueFactories
{
    using Df.Extensibility;
    using System;
    using static Constants;

    [ValueFactory("byte-random", "Generates random byte values", typeof(byte), typeof(RandomByteFactory))]
    public sealed partial class RandomByteFactory
        : RandomScalarFactory<byte>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new RangeFactoryConfiguration<byte>(BYTE_MIN, BYTE_MAX);
    }

    [ValueFactory("datetime-random", "Generates random DateTime values", typeof(DateTime), typeof(RandomDateTimeFactory))]
    public sealed partial class RandomDateTimeFactory
        : RandomScalarFactory<DateTime>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new RangeFactoryConfiguration<DateTime>(DATETIME_MIN, DATETIME_MAX);
    }

    [ValueFactory("datetimeoffset-random", "Generates random DateTimeOffset values", typeof(DateTimeOffset), typeof(RandomDateTimeOffsetFactory))]
    public sealed partial class RandomDateTimeOffsetFactory
        : RandomScalarFactory<DateTimeOffset>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new RangeFactoryConfiguration<DateTimeOffset>(DATETIMEOFFSET_MIN, DATETIMEOFFSET_MAX);
    }

    [ValueFactory("decimal-random", "Generates random decimal values", typeof(decimal), typeof(RandomDecimalFactory))]
    public sealed partial class RandomDecimalFactory
        : RandomScalarFactory<decimal>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new RangeFactoryConfiguration<decimal>(DECIMAL_MIN, DECIMAL_MAX);
    }

    [ValueFactory("double-random", "Generates random double values", typeof(double), typeof(RandomDoubleFactory))]
    public sealed partial class RandomDoubleFactory
        : RandomScalarFactory<double>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new RangeFactoryConfiguration<double>(DOUBLE_MIN, DOUBLE_MAX);
    }

    [ValueFactory("float-random", "Generates random float values", typeof(float), typeof(RandomFloatFactory))]
    public sealed partial class RandomFloatFactory
        : RandomScalarFactory<float>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new RangeFactoryConfiguration<float>(FLOAT_MIN, FLOAT_MAX);
    }

    [ValueFactory("int-random", "Generates random int values", typeof(int), typeof(RandomIntFactory))]
    public sealed partial class RandomIntFactory
        : RandomScalarFactory<int>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new RangeFactoryConfiguration<int>(INT_MIN, INT_MAX);
    }

    [ValueFactory("long-random", "Generates random long values", typeof(long), typeof(RandomLongFactory))]
    public sealed partial class RandomLongFactory
        : RandomScalarFactory<long>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new RangeFactoryConfiguration<long>(LONG_MIN, LONG_MAX);
    }

    [ValueFactory("short-random", "Generates random short values", typeof(short), typeof(RandomShortFactory))]
    public sealed partial class RandomShortFactory
        : RandomScalarFactory<short>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new RangeFactoryConfiguration<short>(SHORT_MIN, SHORT_MAX);
    }

    [ValueFactory("timespan-random", "Generates random TimeSpan values", typeof(TimeSpan), typeof(RandomTimeSpanFactory))]
    public sealed partial class RandomTimeSpanFactory
        : RandomScalarFactory<TimeSpan>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new RangeFactoryConfiguration<TimeSpan>(TIMESPAN_MIN, TIMESPAN_MAX);
    }
}
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1649 // File name should match first type name