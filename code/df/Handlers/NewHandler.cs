// --------------------------------------------------------------------------------
// <copyright file="NewHandler.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Handlers
{
    using Df.Io;
    using Df.Options;
    using System;
    using System.Diagnostics;
    using System.IO;
    using static Constants;

    internal sealed class NewHandler
        : IHandler<NewOptions>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IProjectFactory _ProjectFactory;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IProjectManager _ProjectManager;

        public NewHandler(IProjectFactory projectFactory, IProjectManager projectManager)
        {
            _ProjectFactory = Check.NotNull(nameof(projectFactory), projectFactory);
            _ProjectManager = Check.NotNull(nameof(projectManager), projectManager);
        }

        public void Handle(NewOptions options)
        {
            var project = _ProjectFactory.CreateNew(options.ConnectionString);
            _ProjectManager.SaveToFile(project, EnsureExtension(options.Name));
        }

        private static string EnsureExtension(string path) => string.Equals(EXTENSION, Path.GetExtension(path), StringComparison.InvariantCultureIgnoreCase)
                ? path
                : Path.ChangeExtension(path, EXTENSION);
    }
}