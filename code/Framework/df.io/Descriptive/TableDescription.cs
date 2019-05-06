// --------------------------------------------------------------------------------
// <copyright file="TableDescription.cs" company="Michalis Sarigiannidis">
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

    [DebuggerDisplay("{Schema,nq}.{Name,nq} ({ColumnDescriptions.Count} columns, {ForeignKeyDescriptions.Count} foreign keys)")]
    public sealed class TableDescription
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonIgnore]
        private IReadOnlyList<ColumnDescription> _ColumnDescriptions;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonIgnore]
        private IReadOnlyList<ForeignKeyDescription> _ForeignKeyDescriptions = new List<ForeignKeyDescription>();

        [JsonProperty(Order = 6)]
        public IReadOnlyList<ColumnDescription> ColumnDescriptions
        {
            get => _ColumnDescriptions;

            internal set
            {
                _ColumnDescriptions = Check.NotNull(nameof(value), value);
                foreach (var columnDescription in _ColumnDescriptions)
                {
                    columnDescription.Parent = this;
                }
            }
        }

        [JsonProperty(Order = 4)]
        public DateTimeOffset Created { get; }

        [JsonIgnore]
        public IReadOnlyList<ForeignKeyDescription> ForeignKeyDescriptions
        {
            get => _ForeignKeyDescriptions;
            internal set => _ForeignKeyDescriptions = Check.NotNull(nameof(value), value);
        }

        [JsonProperty(Order = 5)]
        public DateTimeOffset Modified { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonProperty(Order = 3)]
        public string Name { get; }

        [JsonProperty(Order = 1)]
        public int ObjectId { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonProperty(Order = 2)]
        public string Schema { get; }

        [JsonConstructor]
        internal TableDescription(int objectId, string schema, string name, DateTimeOffset created, DateTimeOffset modified)
        {
            ObjectId = Check.GreaterThanOrEqual(nameof(objectId), objectId, 0);
            Schema = Check.NotNull(nameof(schema), schema);
            Name = Check.NotNull(nameof(name), name);
            Created = Check.InOpenInterval(nameof(created), created, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            Modified = Check.InOpenInterval(nameof(modified), modified, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            Check.GreaterThanOrEqual(nameof(modified), modified, created);
        }
    }
}