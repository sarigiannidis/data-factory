// --------------------------------------------------------------------------------
// <copyright file="Prescriptor.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Prescriptive
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using static Constants;

    [JsonObject(IsReference = false)]
    [DebuggerDisplay("{TablePrescriptions.Count} tables, {ValueFactoryPrescriptions.Count} value factories")]
    public sealed class Prescriptor
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonProperty(nameof(TablePrescriptions), Order = 3, ItemIsReference = false)]
        private readonly List<TablePrescription> _TablePrescriptions = new List<TablePrescription>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [JsonProperty(nameof(ValueFactoryPrescriptions), Order = 2)]
        private readonly List<ValueFactoryPrescription> _ValueFactoryPrescriptions = new List<ValueFactoryPrescription>();

        [JsonProperty(Order = 1)]
        public int RowsPerTable { get; set; } = DEFAULTROWSPERTABLE;

        [JsonIgnore]
        public IReadOnlyList<TablePrescription> TablePrescriptions => _TablePrescriptions;

        [JsonIgnore]
        public IReadOnlyList<ValueFactoryPrescription> ValueFactoryPrescriptions => _ValueFactoryPrescriptions;

        public void AddTable(TablePrescription tablePrescription) =>
            _TablePrescriptions.Add(Check.NotNull(nameof(tablePrescription), tablePrescription));

        public void AddValueFactory(ValueFactoryPrescription valueFactoryPrescription)
        {
            Check.NotNull(nameof(valueFactoryPrescription), valueFactoryPrescription);

            if (_ValueFactoryPrescriptions.Any(_ => _.Name == valueFactoryPrescription.Name))
            {
                throw new ArgumentException("The name must be unique.");
            }

            _ValueFactoryPrescriptions.Add(valueFactoryPrescription);
        }
    }
}