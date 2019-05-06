// --------------------------------------------------------------------------------
// <copyright file="TablePrescription.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Prescriptive
{
    using Df.Io.Descriptive;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [JsonObject(IsReference = false)]
    [DebuggerDisplay("{ColumnPrescriptions.Count} columns")]
    public sealed class TablePrescription
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonProperty(nameof(ColumnPrescriptions), Order = 2, ItemIsReference = false)]
        private readonly List<ColumnPrescription> _ColumnPrescriptions = new List<ColumnPrescription>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonIgnore]
        private WeakReference<TableDescription> _TableDescription;

        [JsonIgnore]
        public IReadOnlyList<ColumnPrescription> ColumnPrescriptions => _ColumnPrescriptions;

        [JsonProperty(Order = 1)]
        public TableDescription TableDescription
        {
            get => _TableDescription.TryGetTarget(out var target) ? target : null;

            internal set => _TableDescription = new WeakReference<TableDescription>(Check.NotNull(nameof(value), value));
        }

        [JsonConstructor]
        public TablePrescription(TableDescription tableDescription) =>
            TableDescription = Check.NotNull(nameof(tableDescription), tableDescription);

        public void AddColumn(ColumnPrescription columnPrescription)
        {
            Check.NotNull(nameof(columnPrescription), columnPrescription);
            Check.IfNotThrow<ArgumentException>(() => columnPrescription.ColumnDescription.Parent == TableDescription, "The column does not belong to this table.");
            _ColumnPrescriptions.Add(columnPrescription);
        }
    }
}