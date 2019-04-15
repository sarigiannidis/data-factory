// --------------------------------------------------------------------------------
// <copyright file="RandomBoolConfiguration.cs" company="Michalis Sarigiannidis">
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

    [DebuggerDisplay("True = {TrueWeight}, False = {FalseWeight}")]
    public sealed class RandomBoolConfiguration
        : ValueFactoryConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public float FalseWeight =>
            Get<float>(PROPERTY_FALSE);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public float TrueWeight =>
            Get<float>(PROPERTY_TRUE);

        public RandomBoolConfiguration(float trueWeight, float falseWeight)
        {
            Set(PROPERTY_TRUE, Check.GreaterThanOrEqual(nameof(trueWeight), trueWeight, 0));
            Set(PROPERTY_FALSE, Check.GreaterThanOrEqual(nameof(falseWeight), falseWeight, 0));
        }

        public RandomBoolConfiguration(IDictionary<string, object> properties)
            : base(properties)
        {
        }

        public RandomBoolConfiguration()
            : base()
        {
        }
    }
}