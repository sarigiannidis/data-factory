﻿// --------------------------------------------------------------------------------
// <copyright file="ProjectManager.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io
{
    using System.IO;

    internal sealed class ProjectManager
        : IProjectManager
    {
        private JsonUtility JsonUtility { get; }

        public ProjectManager(JsonUtility jsonUtility) => JsonUtility = Check.NotNull(nameof(jsonUtility), jsonUtility);

        public Project LoadFromFile(string path)
        {
            _ = Check.NotNull(nameof(path), path);
            Check.IfNotThrow<FileNotFoundException>(() => File.Exists(path), Messages.CHECK_NOT_FOUND, nameof(path));
            return JsonUtility.Read<Project>(path);
        }

        public void SaveToFile(Project project, string path) => JsonUtility.Write(Check.NotNull(nameof(project), project), path);
    }
}