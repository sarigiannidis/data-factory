// --------------------------------------------------------------------------------
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
        public Project LoadFromFile(string path)
        {
            Check.NotNull(nameof(path), path);
            Check.IfNotThrow<FileNotFoundException>(() => File.Exists(path), "{0} not found.", nameof(path));
            return JsonUtil.Read<Project>(path);
        }

        public void SaveToFile(Project project, string path) =>
            JsonUtil.Write(Check.NotNull(nameof(project), project), path);
    }
}