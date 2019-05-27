// --------------------------------------------------------------------------------
// <copyright file="ConstantConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using static Constants;

    [DebuggerDisplay("[{Value}]")]
    public class ConstantConfiguration<TValue>
        : ValueFactoryConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public TValue Value => GetValue<TValue>(PROPERTY_CONSTANT);

        public ConstantConfiguration(TValue value) =>
            SetValue(PROPERTY_CONSTANT, value);

        public ConstantConfiguration(IDictionary<string, object> properties)
            : base(properties)
        {
        }

        public ConstantConfiguration()
        {
        }
    }
}