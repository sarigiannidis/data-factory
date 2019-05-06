// --------------------------------------------------------------------------------
// <copyright file="IDatasetGenerator.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Production
{
    using System.IO;

    public interface IDatasetGenerator
    {
        void GenerateDatabase(bool disableTriggers, bool dryRun);

        void GenerateStream(Stream stream, bool disableTriggers, bool dryRun);
    }
}