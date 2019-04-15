// --------------------------------------------------------------------------------
// <copyright file="Descriptor.cs" company="Michalis Sarigiannidis">
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
    using System.Linq;

    [JsonObject(IsReference = false)]
    [DebuggerDisplay(@"{ConnectionInfo.DataSource,nq}\\{ConnectionInfo.InitialCatalog,nq} ({TableDescriptions.Count} tables)")]
    public sealed class Descriptor
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonProperty(nameof(TableDescriptions), Order = 4)]
        private IReadOnlyList<TableDescription> _TableDescriptions;

        [JsonProperty(Order = 1)]
        public string Checksum { get; }

        [JsonProperty(Order = 3)]
        public string ConnectionString { get; }

        [JsonProperty(Order = 2)]
        public DateTimeOffset Created { get; }

        [JsonIgnore]
        public IReadOnlyList<TableDescription> TableDescriptions
        {
            get => _TableDescriptions;
            internal set => _TableDescriptions = Check.NotNull(nameof(value), value);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonProperty(Order = 5)]
        internal IEnumerable<ForeignKeyDescription> ForeignKeyDescriptions
        {
            get => _TableDescriptions.SelectMany(t => t.ForeignKeyDescriptions);

            set
            {
                foreach (var foreignKeyDescriptionGroup in value.GroupBy(_ => _.Parent))
                {
                    foreignKeyDescriptionGroup.Key.ForeignKeyDescriptions = new List<ForeignKeyDescription>(foreignKeyDescriptionGroup);
                }
            }
        }

        [JsonConstructor]
        internal Descriptor(string connectionString, string checksum, DateTimeOffset created)
        {
            ConnectionString = Check.NotNull(nameof(connectionString), connectionString);
            Checksum = Check.NotNull(nameof(checksum), checksum);
            Created = Check.InOpenInterval(nameof(created), created, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
        }
    }
}