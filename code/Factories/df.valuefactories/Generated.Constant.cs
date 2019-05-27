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
    [ValueFactory("byte-constant", "Generates constant byte values", typeof(byte), typeof(ConstantByteFactory))]
    public sealed class ConstantByteFactory
        : ConstantFactory<byte>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ConstantConfiguration<byte>(default(byte));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("decimal-constant", "Generates constant decimal values", typeof(decimal), typeof(ConstantDecimalFactory))]
    public sealed class ConstantDecimalFactory
        : ConstantFactory<decimal>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ConstantConfiguration<decimal>(default(decimal));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("double-constant", "Generates constant double values", typeof(double), typeof(ConstantDoubleFactory))]
    public sealed class ConstantDoubleFactory
        : ConstantFactory<double>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ConstantConfiguration<double>(default(double));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("float-constant", "Generates constant float values", typeof(float), typeof(ConstantFloatFactory))]
    public sealed class ConstantFloatFactory
        : ConstantFactory<float>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ConstantConfiguration<float>(default(float));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("int-constant", "Generates constant int values", typeof(int), typeof(ConstantIntFactory))]
    public sealed class ConstantIntFactory
        : ConstantFactory<int>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ConstantConfiguration<int>(default(int));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("long-constant", "Generates constant long values", typeof(long), typeof(ConstantLongFactory))]
    public sealed class ConstantLongFactory
        : ConstantFactory<long>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ConstantConfiguration<long>(default(long));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("short-constant", "Generates constant short values", typeof(short), typeof(ConstantShortFactory))]
    public sealed class ConstantShortFactory
        : ConstantFactory<short>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ConstantConfiguration<short>(default(short));
    }

    [GeneratedCode("df", "")]
    [ValueFactory("timespan-constant", "Generates constant TimeSpan values", typeof(TimeSpan), typeof(ConstantTimeSpanFactory))]
    public sealed class ConstantTimeSpanFactory
        : ConstantFactory<TimeSpan>
        , IConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ConstantConfiguration<TimeSpan>(default(TimeSpan));
    }
}
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1649 // File name should match first type name