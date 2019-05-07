// --------------------------------------------------------------------------------
// <copyright file="GenerateHandler.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.OptionHandlers
{
    using Df.Io;
    using Df.Options;
    using Df.Production;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using static Constants;

    internal sealed class GenerateHandler
        : IHandler<GenerateOptions>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IDatasetGeneratorFactory _DatasetGeneratorFactory;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IProjectManager _ProjectManager;

        public GenerateHandler(IProjectManager projectManager, IDatasetGeneratorFactory generatorFactory)
        {
            _ProjectManager = Check.NotNull(nameof(projectManager), projectManager);
            _DatasetGeneratorFactory = Check.NotNull(nameof(generatorFactory), generatorFactory);
        }

        public void Handle(GenerateOptions options)
        {
            var project = _ProjectManager.LoadFromFile(options.Project);
            switch (options.Subject)
            {
                case GenerateSubject.File:
                    GenerateFile(project, options.DisableTriggers, options.DryRun, options.Output);
                    break;

                case GenerateSubject.Database:
                    GenerateDatabase(project, options.DisableTriggers, options.DryRun);
                    break;
            }
        }

        private void GenerateDatabase(Project project, bool disableTriggers, bool dryRun) =>
            _DatasetGeneratorFactory.Create(project).GenerateDatabase(disableTriggers, dryRun);

        private void GenerateFile(Project project, bool disableTriggers, bool dryRun, string path)
        {
            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, BUFFERSIZE);
            var datasetGenerator = _DatasetGeneratorFactory.Create(project);
            if (path.EndsWith(".gz", StringComparison.InvariantCultureIgnoreCase))
            {
                using var compressionStream = new GZipStream(stream, CompressionMode.Compress);
                datasetGenerator.GenerateStream(compressionStream, disableTriggers, dryRun);
            }
            else
            {
                datasetGenerator.GenerateStream(stream, disableTriggers, dryRun);
            }
        }
    }
}