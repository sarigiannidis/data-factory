// --------------------------------------------------------------------------------
// <copyright file="ColumnRelationshipDescription.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Descriptive
{
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;

    [JsonObject(IsReference = false)]
    [DebuggerDisplay("{Order}. {Parent.Name,nq} → {Referenced.Name,nq}")]
    public sealed class ColumnRelationshipDescription
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonIgnore]
        private WeakReference<ColumnDescription> _Parent;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonIgnore]
        private WeakReference<ColumnDescription> _Referenced;

        [JsonProperty(Order = 1)]
        public int Order { get; internal set; }

        [JsonProperty(Order = 2)]
        public ColumnDescription Parent
        {
            get => _Parent.TryGetTarget(out var target) ? target : null;

            internal set => _Parent = new WeakReference<ColumnDescription>(Check.NotNull(nameof(value), value));
        }

        [JsonProperty(Order = 3)]
        public ColumnDescription Referenced
        {
            get => _Referenced.TryGetTarget(out var target) ? target : null;

            internal set => _Referenced = new WeakReference<ColumnDescription>(Check.NotNull(nameof(value), value));
        }

        [JsonConstructor]
        internal ColumnRelationshipDescription(int order) => Order = Check.GreaterThanOrEqual(nameof(order), order, 0);
    }
}