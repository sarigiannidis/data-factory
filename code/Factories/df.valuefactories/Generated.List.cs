// --------------------------------------------------------------------------------
// <copyright file="Generated.List.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1649 // File name should match first type name
namespace Df.ValueFactories
{
    using Df.Extensibility;
    using Df.Numeric;
    using System;
    using System.CodeDom.Compiler;

    [GeneratedCode("df", "")]
    [ValueFactory("datetime-list", "Picks randomly from a weighted list of DateTime values.", typeof(DateTime), typeof(DateTimeListFactory))]
    public sealed class DateTimeListFactory
        : RandomListFactory<DateTime>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<DateTime>(new WeightedValueCollection<DateTime>
            {
                new WeightedValue<DateTime>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("datetimeoffset-list", "Picks randomly from a weighted list of DateTimeOffset values.", typeof(DateTimeOffset), typeof(DateTimeOffsetListFactory))]
    public sealed class DateTimeOffsetListFactory
        : RandomListFactory<DateTimeOffset>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<DateTimeOffset>(new WeightedValueCollection<DateTimeOffset>
            {
                new WeightedValue<DateTimeOffset>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("bool-list", "Picks randomly from a weighted list of bool values.", typeof(bool), typeof(BoolListFactory))]
    public sealed class BoolListFactory
        : RandomListFactory<bool>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<bool>(new WeightedValueCollection<bool>
            {
                new WeightedValue<bool>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("byte-list", "Picks randomly from a weighted list of byte values.", typeof(byte), typeof(ByteListFactory))]
    public sealed class ByteListFactory
        : RandomListFactory<byte>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<byte>(new WeightedValueCollection<byte>
            {
                new WeightedValue<byte>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("char-list", "Picks randomly from a weighted list of char values.", typeof(char), typeof(CharListFactory))]
    public sealed class CharListFactory
        : RandomListFactory<char>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<char>(new WeightedValueCollection<char>
            {
                new WeightedValue<char>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("decimal-list", "Picks randomly from a weighted list of decimal values.", typeof(decimal), typeof(DecimalListFactory))]
    public sealed class DecimalListFactory
        : RandomListFactory<decimal>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<decimal>(new WeightedValueCollection<decimal>
            {
                new WeightedValue<decimal>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("double-list", "Picks randomly from a weighted list of double values.", typeof(double), typeof(DoubleListFactory))]
    public sealed class DoubleListFactory
        : RandomListFactory<double>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<double>(new WeightedValueCollection<double>
            {
                new WeightedValue<double>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("float-list", "Picks randomly from a weighted list of float values.", typeof(float), typeof(FloatListFactory))]
    public sealed class FloatListFactory
        : RandomListFactory<float>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<float>(new WeightedValueCollection<float>
            {
                new WeightedValue<float>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("int-list", "Picks randomly from a weighted list of int values.", typeof(int), typeof(IntListFactory))]
    public sealed class IntListFactory
        : RandomListFactory<int>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<int>(new WeightedValueCollection<int>
            {
                new WeightedValue<int>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("long-list", "Picks randomly from a weighted list of long values.", typeof(long), typeof(LongListFactory))]
    public sealed class LongListFactory
        : RandomListFactory<long>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<long>(new WeightedValueCollection<long>
            {
                new WeightedValue<long>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("sbyte-list", "Picks randomly from a weighted list of sbyte values.", typeof(sbyte), typeof(SbyteListFactory))]
    public sealed class SbyteListFactory
        : RandomListFactory<sbyte>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<sbyte>(new WeightedValueCollection<sbyte>
            {
                new WeightedValue<sbyte>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("short-list", "Picks randomly from a weighted list of short values.", typeof(short), typeof(ShortListFactory))]
    public sealed class ShortListFactory
        : RandomListFactory<short>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<short>(new WeightedValueCollection<short>
            {
                new WeightedValue<short>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("string-list", "Picks randomly from a weighted list of string values.", typeof(string), typeof(StringListFactory))]
    public sealed class StringListFactory
        : RandomListFactory<string>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<string>(new WeightedValueCollection<string>
            {
                new WeightedValue<string>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("uint-list", "Picks randomly from a weighted list of uint values.", typeof(uint), typeof(UintListFactory))]
    public sealed class UintListFactory
        : RandomListFactory<uint>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<uint>(new WeightedValueCollection<uint>
            {
                new WeightedValue<uint>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("ulong-list", "Picks randomly from a weighted list of ulong values.", typeof(ulong), typeof(UlongListFactory))]
    public sealed class UlongListFactory
        : RandomListFactory<ulong>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<ulong>(new WeightedValueCollection<ulong>
            {
                new WeightedValue<ulong>(default, 0.1f),
            });
    }
    [GeneratedCode("df", "")]
    [ValueFactory("ushort-list", "Picks randomly from a weighted list of ushort values.", typeof(ushort), typeof(UshortListFactory))]
    public sealed class UshortListFactory
        : RandomListFactory<ushort>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>new ListFactoryConfiguration<ushort>(new WeightedValueCollection<ushort>
            {
                new WeightedValue<ushort>(default, 0.1f),
            });
    }
}
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1649 // File name should match first type name