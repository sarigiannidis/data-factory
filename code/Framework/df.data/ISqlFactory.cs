// --------------------------------------------------------------------------------
// <copyright file="ISqlFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data
{
    public interface ISqlFactory
    {
        ISql CreateTemporaryDatabase(string databaseName);

        ISql Open(string connectionString);
    }
}