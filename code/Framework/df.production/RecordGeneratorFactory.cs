// --------------------------------------------------------------------------------
// <copyright file="RecordGeneratorFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Production
{
    using Df.Extensibility;
    using Df.Io.Prescriptive;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;

    internal sealed class RecordGeneratorFactory
        : IRecordGeneratorFactory
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IValueFactoryManager _ValueFactoryManager;

        public RecordGeneratorFactory(IValueFactoryManager valueFactoryManager)
        {
            _ValueFactoryManager = Check.NotNull(nameof(valueFactoryManager), valueFactoryManager);
            _ValueFactoryManager.Initialize();
        }

        public IDataReader Create(TablePrescription tablePrescription, int rows) => new RecordGenerator(rows, GetFactories(tablePrescription));

        private IEnumerable<(IValueFactory factory, Type type, float nullPercentage)> GetFactories(TablePrescription tablePrescription)
        {
            foreach (var columnPrescription in tablePrescription.ColumnPrescriptions)
            {
                var factoryInfo = _ValueFactoryManager.Resolve(columnPrescription.ValueFactoryPrescription.Factory);
                var factory = factoryInfo.ValueFactory;
                factory.Configuration = columnPrescription.ValueFactoryPrescription.Configuration;
                yield return (factory, factoryInfo.ValueType, columnPrescription.NullPercentage ?? 0);
            }
        }
    }
}