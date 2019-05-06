// --------------------------------------------------------------------------------
// <copyright file="ForeignKeyDescription.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Descriptive
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [JsonObject(IsReference = false)]
    [DebuggerDisplay("{Name,nq}: {Parent.Name,nq} → {Referenced.Name,nq}")]
    public sealed class ForeignKeyDescription
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonProperty(nameof(ColumnRelationshipDescriptions), Order = 4)]
        private IReadOnlyList<ColumnRelationshipDescription> _ColumnRelationshipDescriptions;

        [JsonIgnore]
        public IReadOnlyList<ColumnRelationshipDescription> ColumnRelationshipDescriptions
        {
            get => _ColumnRelationshipDescriptions;

            internal set => _ColumnRelationshipDescriptions = Check.NotNull(nameof(value), value);
        }

        [JsonProperty(Order = 2)]
        public DateTimeOffset Created { get; }

        [JsonProperty(Order = 3)]
        public DateTimeOffset Modified { get; }

        [JsonProperty(Order = 1)]
        public string Name { get; }

        [JsonIgnore]
        public TableDescription Parent => _ColumnRelationshipDescriptions[0].Parent.Parent;

        [JsonIgnore]
        public TableDescription Referenced => _ColumnRelationshipDescriptions[0].Referenced.Parent;

        [JsonConstructor]
        internal ForeignKeyDescription(string name, DateTime created, DateTime modified)
        {
            Created = Check.InOpenInterval(nameof(created), created, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            Modified = Check.InOpenInterval(nameof(modified), modified, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            Check.GreaterThanOrEqual(nameof(modified), modified, created);
            Name = Check.NotNull(nameof(name), name);
        }
    }
}