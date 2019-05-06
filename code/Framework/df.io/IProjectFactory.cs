// --------------------------------------------------------------------------------
// <copyright file="IProjectFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io
{
    public interface IProjectFactory
    {
        Project CreateNew(string connectionString);
    }
}