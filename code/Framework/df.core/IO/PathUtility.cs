// --------------------------------------------------------------------------------
// <copyright file="PathUtility.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.IO
{
    using System.IO;
    using System.Reflection;

    public static class PathUtility
    {
        public static string CurrentDirectory => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static string GetFullPath(string path) =>
            Path.IsPathFullyQualified(path) ? path : Path.GetFullPath(path, CurrentDirectory);

        public static string GetTempFileName() =>
                            Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
    }
}