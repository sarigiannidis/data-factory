// --------------------------------------------------------------------------------
// <copyright file="DatasetGeneratorFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Production
{
    using Df.Data;
    using Df.Io;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;

    internal class DatasetGeneratorFactory
        : IDatasetGeneratorFactory
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILogger<DatasetGenerator> _Logger;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IRecordGeneratorFactory _RecordGeneratorFactory;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ISqlFactory _SqlFactory;

        public DatasetGeneratorFactory(IRecordGeneratorFactory recordGeneratorFactory, ISqlFactory sqlFactory, ILogger<DatasetGenerator> logger)
        {
            _RecordGeneratorFactory = Check.NotNull(nameof(recordGeneratorFactory), recordGeneratorFactory);
            _SqlFactory = Check.NotNull(nameof(sqlFactory), sqlFactory);
            _Logger = Check.NotNull(nameof(logger), logger);
        }

        public IDatasetGenerator Create(Project project) => new DatasetGenerator(_RecordGeneratorFactory, _SqlFactory, _Logger, project);
    }
}