// --------------------------------------------------------------------------------
// <copyright file="SqlAuthentication.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Security
{
    public enum SqlAuthentication
    {
        None = 0,

        Windows = 1,

        SqlServer = 2,

        ActiveDirectoryUniversal = 3,

        ActiveDirectoryPassword = 4,

        ActiveDirectoryIntegrated = 5,
    }
}