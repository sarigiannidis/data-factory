// --------------------------------------------------------------------------------
// <copyright file="BinaryConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories
{
    using Df.Extensibility;
    using System.Collections.Generic;
    using System.Diagnostics;
    using static Constants;

    [DebuggerDisplay("[{MinLength}, {MaxLength}]")]
    public sealed class BinaryConfiguration
        : ValueFactoryConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int MaxLength =>
            GetValue<int>(PROPERTY_MAXLENGTH);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int MinLength =>
            GetValue<int>(PROPERTY_MINLENGTH);

        public BinaryConfiguration(int minLength, int maxLength)
        {
            SetValue(PROPERTY_MINLENGTH, Check.GreaterThanOrEqual(nameof(minLength), minLength, 0));
            SetValue(PROPERTY_MAXLENGTH, Check.GreaterThanOrEqual(nameof(maxLength), maxLength, 0));
        }

        public BinaryConfiguration(IDictionary<string, object> properties)
            : base(properties)
        {
        }

        public BinaryConfiguration()
        {
        }
    }
}