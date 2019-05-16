// --------------------------------------------------------------------------------
// <copyright file="Temporary.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Xunit
{
    using System.Diagnostics;

    public static class Temporary
    {
        public static string GetTempFilePath(string extension = null)
        {
            var memberInfo = new StackTrace()
                .GetFrame(1)
                .GetMethod();

            return TemporaryFileContext
                .GetContext(memberInfo)
                .GetFilePath(extension);
        }
    }
}