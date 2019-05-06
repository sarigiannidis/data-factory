// --------------------------------------------------------------------------------
// <copyright file="ColumnPrescription.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Prescriptive
{
    using Df.Io.Descriptive;
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;

    [JsonObject(IsReference = false)]
    [DebuggerDisplay("{ColumnDescription.Name,nq} → {ValueFactoryPrescription,nq}")]
    public sealed class ColumnPrescription
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonIgnore]
        private WeakReference<ColumnDescription> _ColumnDescription;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonIgnore]
        private float? _NullPercentage;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonIgnore]
        private WeakReference<ValueFactoryPrescription> _ValueFactoryPrescription;

        [JsonProperty(Order = 1)]
        public ColumnDescription ColumnDescription
        {
            get => _ColumnDescription.TryGetTarget(out var target) ? target : null;

            internal set => _ColumnDescription = new WeakReference<ColumnDescription>(Check.NotNull(nameof(value), value));
        }

        [JsonProperty(Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public float? NullPercentage
        {
            get => _NullPercentage;
            set => _NullPercentage = ((value ?? 0) == 0) ? null : value;
        }

        [JsonProperty(Order = 2)]
        public ValueFactoryPrescription ValueFactoryPrescription
        {
            get => _ValueFactoryPrescription.TryGetTarget(out var target) ? target : null;

            internal set => _ValueFactoryPrescription = new WeakReference<ValueFactoryPrescription>(Check.NotNull(nameof(value), value));
        }

        [JsonConstructor]
        public ColumnPrescription(ColumnDescription columnDescription, ValueFactoryPrescription valueFactoryPrescription, float? nullPercentage)
        {
            ColumnDescription = Check.NotNull(nameof(columnDescription), columnDescription);
            ValueFactoryPrescription = Check.NotNull(nameof(valueFactoryPrescription), valueFactoryPrescription);
            NullPercentage = nullPercentage;
        }
    }
}