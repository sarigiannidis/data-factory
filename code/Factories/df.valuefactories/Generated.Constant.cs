// --------------------------------------------------------------------------------
// <copyright file="Generated.Constant.cs" company="Michalis Sarigiannidis">
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
    using System.CodeDom.Compiler;

    [GeneratedCode("df", "")]
    [ValueFactory("datetime-constant", "Generates constant DateTime values", typeof(DateTime), typeof(ConstantDateTimeFactory))]
    public sealed class ConstantDateTimeFactory
        : ConstantFactory<DateTime>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<DateTime>(default(DateTime));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("datetimeoffset-constant", "Generates constant DateTimeOffset values", typeof(DateTimeOffset), typeof(ConstantDateTimeOffsetFactory))]
    public sealed class ConstantDateTimeOffsetFactory
        : ConstantFactory<DateTimeOffset>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<DateTimeOffset>(default(DateTimeOffset));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("timespan-constant", "Generates constant TimeSpan values", typeof(TimeSpan), typeof(ConstantTimeSpanFactory))]
    public sealed class ConstantTimeSpanFactory
        : ConstantFactory<TimeSpan>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<TimeSpan>(default(TimeSpan));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("bool-constant", "Generates constant bool values", typeof(bool), typeof(ConstantBoolFactory))]
    public sealed class ConstantBoolFactory
        : ConstantFactory<bool>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<bool>(default(bool));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("byte-constant", "Generates constant byte values", typeof(byte), typeof(ConstantByteFactory))]
    public sealed class ConstantByteFactory
        : ConstantFactory<byte>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<byte>(default(byte));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("char-constant", "Generates constant char values", typeof(char), typeof(ConstantCharFactory))]
    public sealed class ConstantCharFactory
        : ConstantFactory<char>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<char>(default(char));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("decimal-constant", "Generates constant decimal values", typeof(decimal), typeof(ConstantDecimalFactory))]
    public sealed class ConstantDecimalFactory
        : ConstantFactory<decimal>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<decimal>(default(decimal));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("double-constant", "Generates constant double values", typeof(double), typeof(ConstantDoubleFactory))]
    public sealed class ConstantDoubleFactory
        : ConstantFactory<double>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<double>(default(double));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("float-constant", "Generates constant float values", typeof(float), typeof(ConstantFloatFactory))]
    public sealed class ConstantFloatFactory
        : ConstantFactory<float>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<float>(default(float));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("int-constant", "Generates constant int values", typeof(int), typeof(ConstantIntFactory))]
    public sealed class ConstantIntFactory
        : ConstantFactory<int>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<int>(default(int));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("long-constant", "Generates constant long values", typeof(long), typeof(ConstantLongFactory))]
    public sealed class ConstantLongFactory
        : ConstantFactory<long>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<long>(default(long));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("sbyte-constant", "Generates constant sbyte values", typeof(sbyte), typeof(ConstantSbyteFactory))]
    public sealed class ConstantSbyteFactory
        : ConstantFactory<sbyte>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<sbyte>(default(sbyte));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("short-constant", "Generates constant short values", typeof(short), typeof(ConstantShortFactory))]
    public sealed class ConstantShortFactory
        : ConstantFactory<short>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<short>(default(short));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("string-constant", "Generates constant string values", typeof(string), typeof(ConstantStringFactory))]
    public sealed class ConstantStringFactory
        : ConstantFactory<string>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<string>(default(string));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("uint-constant", "Generates constant uint values", typeof(uint), typeof(ConstantUintFactory))]
    public sealed class ConstantUintFactory
        : ConstantFactory<uint>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<uint>(default(uint));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("ulong-constant", "Generates constant ulong values", typeof(ulong), typeof(ConstantUlongFactory))]
    public sealed class ConstantUlongFactory
        : ConstantFactory<ulong>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<ulong>(default(ulong));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("ushort-constant", "Generates constant ushort values", typeof(ushort), typeof(ConstantUshortFactory))]
    public sealed class ConstantUshortFactory
        : ConstantFactory<ushort>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ConstantConfiguration<ushort>(default(ushort));
    }
}
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1649 // File name should match first type name