// --------------------------------------------------------------------------------
// <copyright file="List.cs" company="Michalis Sarigiannidis">
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

    [ValueFactory("byte-list", "Picks randomly from a weighted list of byte values.", typeof(byte), typeof(ByteListFactory))]
    public sealed class ByteListFactory
        : RandomListFactory<byte>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ListFactoryConfiguration<byte>(new WeightedValueCollection<byte>
            {
                new WeightedValue<byte>(default, 0.1f),
            });
    }

    [ValueFactory("decimal-list", "Picks randomly from a weighted list of decimal values.", typeof(decimal), typeof(DecimalListFactory))]
    public sealed class DecimalListFactory
        : RandomListFactory<decimal>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ListFactoryConfiguration<decimal>(new WeightedValueCollection<decimal>
            {
                new WeightedValue<decimal>(default, 0.1f),
            });
    }

    [ValueFactory("double-list", "Picks randomly from a weighted list of double values.", typeof(double), typeof(DoubleListFactory))]
    public sealed class DoubleListFactory
        : RandomListFactory<double>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ListFactoryConfiguration<double>(new WeightedValueCollection<double>
            {
                new WeightedValue<double>(default, 0.1f),
            });
    }

    [ValueFactory("float-list", "Picks randomly from a weighted list of float values.", typeof(float), typeof(FloatListFactory))]
    public sealed class FloatListFactory
        : RandomListFactory<float>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ListFactoryConfiguration<float>(new WeightedValueCollection<float>
            {
                new WeightedValue<float>(default, 0.1f),
            });
    }

    [ValueFactory("int-list", "Picks randomly from a weighted list of int values.", typeof(int), typeof(IntListFactory))]
    public sealed class IntListFactory
        : RandomListFactory<int>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ListFactoryConfiguration<int>(new WeightedValueCollection<int>
            {
                new WeightedValue<int>(default, 0.1f),
            });
    }

    [ValueFactory("long-list", "Picks randomly from a weighted list of long values.", typeof(long), typeof(LongListFactory))]
    public sealed class LongListFactory
        : RandomListFactory<long>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ListFactoryConfiguration<long>(new WeightedValueCollection<long>
            {
                new WeightedValue<long>(default, 0.1f),
            });
    }

    [ValueFactory("short-list", "Picks randomly from a weighted list of short values.", typeof(short), typeof(ShortListFactory))]
    public sealed class ShortListFactory
        : RandomListFactory<short>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ListFactoryConfiguration<short>(new WeightedValueCollection<short>
            {
                new WeightedValue<short>(default, 0.1f),
            });
    }

    [ValueFactory("string-list", "Picks randomly from a weighted list of string values.", typeof(string), typeof(StringListFactory))]
    public sealed class StringListFactory
        : RandomListFactory<string>,
        IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ListFactoryConfiguration<string>(new WeightedValueCollection<string>
            {
                new WeightedValue<string>(default, 0.1f),
            });
    }
}
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1649 // File name should match first type name