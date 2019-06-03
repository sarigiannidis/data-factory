// --------------------------------------------------------------------------------
// <copyright file="ColumnDescription.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Descriptive
{
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{Order}. {Name,nq}")]
    public sealed class ColumnDescription
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonIgnore]
        private WeakReference<TableDescription> _Parent;

        [JsonProperty(Order = 7)]
        public bool Computed { get; }

        [JsonProperty(Order = 9, NullValueHandling = NullValueHandling.Ignore)]
        public Identity Identity { get; }

        [JsonProperty(Order = 4)]
        public short MaxLength { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonProperty(Order = 2)]
        public string Name { get; }

        [JsonProperty(Order = 8)]
        public bool Nullable { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonProperty(Order = 1)]
        public int Order { get; }

        [JsonIgnore]
        public TableDescription Parent
        {
            get => _Parent.TryGetTarget(out var target) ? target : null;
            internal set => _Parent = new WeakReference<TableDescription>(Check.NotNull(nameof(value), value));
        }

        [JsonProperty(Order = 5)]
        public short Precision { get; }

        [JsonProperty(Order = 6)]
        public short Scale { get; }

        [JsonProperty(Order = 3)]
        public string UserType { get; }

        [JsonConstructor]
        internal ColumnDescription(int order, string name, string userType, bool nullable, bool computed, short maxLength, short precision, short scale, Identity identity)
        {
            Order = Check.GreaterThanOrEqual(nameof(order), order, 0);
            Name = Check.NotNull(nameof(name), name);
            UserType = Check.NotNull(nameof(userType), userType);
            Nullable = nullable;
            Identity = identity;
            Computed = computed;
            MaxLength = Check.GreaterThanOrEqual(nameof(maxLength), maxLength, (short)-1);
            Precision = Check.GreaterThanOrEqual(nameof(precision), precision, (short)0);
            Scale = Check.GreaterThanOrEqual(nameof(scale), scale, (short)0);
            _ = Check.LessThanOrEqual(nameof(scale), scale, precision);
        }

        // @TODO: Also Identity == null.
        public bool IsWritable() =>
            !(Computed || UserType == Data.Constants.SQL_TYPE_TIMESTAMP);
    }
}