// --------------------------------------------------------------------------------
// <copyright file="Incremental.cs" company="Michalis Sarigiannidis">
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
    using System.Globalization;
    using static Constants;

    [GeneratedCode("df", "")]
    [ValueFactory("byte-incremental", "Generates incremental byte values", typeof(byte), typeof(IncrementalByteFactory))]
    public sealed class IncrementalByteFactory
        : IncrementalScalarFactory<byte>,
        IConstrainableConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ScalarFactoryConfiguration<byte>(BYTE_MIN, BYTE_MAX, BYTE_STEP);

        IValueFactoryConfiguration IConstrainableConfigurator.CreateConfiguration(ConfiguratorConstraints configuratorConstraints)
        {
            if (configuratorConstraints.SeedValue is null || configuratorConstraints.IncrementValue is null)
            {
                return ((IConfigurator)this).CreateConfiguration();
            }
            else
            {
                var seed = (byte)Convert.ChangeType(configuratorConstraints.SeedValue, typeof(byte), CultureInfo.InvariantCulture);
                var increment = (byte)Convert.ChangeType(configuratorConstraints.IncrementValue, typeof(byte), CultureInfo.InvariantCulture);
                return new ScalarFactoryConfiguration<byte>(seed, byte.MaxValue, increment);
            }
        }
    }

    [GeneratedCode("df", "")]
    [ValueFactory("decimal-incremental", "Generates incremental decimal values", typeof(decimal), typeof(IncrementalDecimalFactory))]
    public sealed class IncrementalDecimalFactory
        : IncrementalScalarFactory<decimal>,
        IConstrainableConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ScalarFactoryConfiguration<decimal>(DECIMAL_MIN, DECIMAL_MAX, DECIMAL_STEP);

        IValueFactoryConfiguration IConstrainableConfigurator.CreateConfiguration(ConfiguratorConstraints configuratorConstraints)
        {
            if (configuratorConstraints.SeedValue is null || configuratorConstraints.IncrementValue is null)
            {
                return ((IConfigurator)this).CreateConfiguration();
            }
            else
            {
                var seed = (decimal)Convert.ChangeType(configuratorConstraints.SeedValue, typeof(decimal), CultureInfo.InvariantCulture);
                var increment = (decimal)Convert.ChangeType(configuratorConstraints.IncrementValue, typeof(decimal), CultureInfo.InvariantCulture);
                return new ScalarFactoryConfiguration<decimal>(seed, decimal.MaxValue, increment);
            }
        }
    }

    [GeneratedCode("df", "")]
    [ValueFactory("double-incremental", "Generates incremental double values", typeof(double), typeof(IncrementalDoubleFactory))]
    public sealed class IncrementalDoubleFactory
        : IncrementalScalarFactory<double>,
        IConstrainableConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ScalarFactoryConfiguration<double>(DOUBLE_MIN, DOUBLE_MAX, DOUBLE_STEP);

        IValueFactoryConfiguration IConstrainableConfigurator.CreateConfiguration(ConfiguratorConstraints configuratorConstraints)
        {
            if (configuratorConstraints.SeedValue is null || configuratorConstraints.IncrementValue is null)
            {
                return ((IConfigurator)this).CreateConfiguration();
            }
            else
            {
                var seed = (double)Convert.ChangeType(configuratorConstraints.SeedValue, typeof(double), CultureInfo.InvariantCulture);
                var increment = (double)Convert.ChangeType(configuratorConstraints.IncrementValue, typeof(double), CultureInfo.InvariantCulture);
                return new ScalarFactoryConfiguration<double>(seed, double.MaxValue, increment);
            }
        }
    }

    [GeneratedCode("df", "")]
    [ValueFactory("float-incremental", "Generates incremental float values", typeof(float), typeof(IncrementalFloatFactory))]
    public sealed class IncrementalFloatFactory
        : IncrementalScalarFactory<float>,
        IConstrainableConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ScalarFactoryConfiguration<float>(FLOAT_MIN, FLOAT_MAX, FLOAT_STEP);

        IValueFactoryConfiguration IConstrainableConfigurator.CreateConfiguration(ConfiguratorConstraints configuratorConstraints)
        {
            if (configuratorConstraints.SeedValue is null || configuratorConstraints.IncrementValue is null)
            {
                return ((IConfigurator)this).CreateConfiguration();
            }
            else
            {
                var seed = (float)Convert.ChangeType(configuratorConstraints.SeedValue, typeof(float), CultureInfo.InvariantCulture);
                var increment = (float)Convert.ChangeType(configuratorConstraints.IncrementValue, typeof(float), CultureInfo.InvariantCulture);
                return new ScalarFactoryConfiguration<float>(seed, float.MaxValue, increment);
            }
        }
    }

    [GeneratedCode("df", "")]
    [ValueFactory("int-incremental", "Generates incremental int values", typeof(int), typeof(IncrementalIntFactory))]
    public sealed class IncrementalIntFactory
        : IncrementalScalarFactory<int>,
        IConstrainableConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ScalarFactoryConfiguration<int>(INT_MIN, INT_MAX, INT_STEP);

        IValueFactoryConfiguration IConstrainableConfigurator.CreateConfiguration(ConfiguratorConstraints configuratorConstraints)
        {
            if (configuratorConstraints.SeedValue is null || configuratorConstraints.IncrementValue is null)
            {
                return ((IConfigurator)this).CreateConfiguration();
            }
            else
            {
                var seed = (int)Convert.ChangeType(configuratorConstraints.SeedValue, typeof(int), CultureInfo.InvariantCulture);
                var increment = (int)Convert.ChangeType(configuratorConstraints.IncrementValue, typeof(int), CultureInfo.InvariantCulture);
                return new ScalarFactoryConfiguration<int>(seed, int.MaxValue, increment);
            }
        }
    }

    [GeneratedCode("df", "")]
    [ValueFactory("long-incremental", "Generates incremental long values", typeof(long), typeof(IncrementalLongFactory))]
    public sealed class IncrementalLongFactory
        : IncrementalScalarFactory<long>,
        IConstrainableConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ScalarFactoryConfiguration<long>(LONG_MIN, LONG_MAX, LONG_STEP);

        IValueFactoryConfiguration IConstrainableConfigurator.CreateConfiguration(ConfiguratorConstraints configuratorConstraints)
        {
            if (configuratorConstraints.SeedValue is null || configuratorConstraints.IncrementValue is null)
            {
                return ((IConfigurator)this).CreateConfiguration();
            }
            else
            {
                var seed = (long)Convert.ChangeType(configuratorConstraints.SeedValue, typeof(long), CultureInfo.InvariantCulture);
                var increment = (long)Convert.ChangeType(configuratorConstraints.IncrementValue, typeof(long), CultureInfo.InvariantCulture);
                return new ScalarFactoryConfiguration<long>(seed, long.MaxValue, increment);
            }
        }
    }

    [GeneratedCode("df", "")]
    [ValueFactory("short-incremental", "Generates incremental short values", typeof(short), typeof(IncrementalShortFactory))]
    public sealed class IncrementalShortFactory
        : IncrementalScalarFactory<short>,
        IConstrainableConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ScalarFactoryConfiguration<short>(SHORT_MIN, SHORT_MAX, SHORT_STEP);

        IValueFactoryConfiguration IConstrainableConfigurator.CreateConfiguration(ConfiguratorConstraints configuratorConstraints)
        {
            if (configuratorConstraints.SeedValue is null || configuratorConstraints.IncrementValue is null)
            {
                return ((IConfigurator)this).CreateConfiguration();
            }
            else
            {
                var seed = (short)Convert.ChangeType(configuratorConstraints.SeedValue, typeof(short), CultureInfo.InvariantCulture);
                var increment = (short)Convert.ChangeType(configuratorConstraints.IncrementValue, typeof(short), CultureInfo.InvariantCulture);
                return new ScalarFactoryConfiguration<short>(seed, short.MaxValue, increment);
            }
        }
    }

    [GeneratedCode("df", "")]
    [ValueFactory("timespan-incremental", "Generates incremental TimeSpan values", typeof(TimeSpan), typeof(IncrementalTimeSpanFactory))]
    public sealed class IncrementalTimeSpanFactory
        : IncrementalScalarFactory<TimeSpan>,
        IConstrainableConfigurator
    {
        IValueFactoryConfiguration IConfigurator.CreateConfiguration() =>
            new ScalarFactoryConfiguration<TimeSpan>(TIMESPAN_MIN, TIMESPAN_MAX, TIMESPAN_STEP);

        IValueFactoryConfiguration IConstrainableConfigurator.CreateConfiguration(ConfiguratorConstraints configuratorConstraints)
        {
            if (configuratorConstraints.SeedValue is null || configuratorConstraints.IncrementValue is null)
            {
                return ((IConfigurator)this).CreateConfiguration();
            }
            else
            {
                var seed = (TimeSpan)Convert.ChangeType(configuratorConstraints.SeedValue, typeof(TimeSpan), CultureInfo.InvariantCulture);
                var increment = (TimeSpan)Convert.ChangeType(configuratorConstraints.IncrementValue, typeof(TimeSpan), CultureInfo.InvariantCulture);
                return new ScalarFactoryConfiguration<TimeSpan>(seed, TimeSpan.MaxValue, increment);
            }
        }
    }
}
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1649 // File name should match first type name