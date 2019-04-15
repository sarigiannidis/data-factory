// --------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Security
{
    internal static class Constants
    {
        public const string SQL_AUTHENTICATION_AD_INTEGRATED = "Active Directory - Integrated";

        public const string SQL_AUTHENTICATION_AD_PASSWORD = "Active Directory - Password";

        public const string SQL_AUTHENTICATION_AD_UNIVERSAL = "Active Directory - Universal wtih MFA Support";

        public const string SQL_AUTHENTICATION_SQL_SERVER = "SQL Server Authentication";

        public const string SQL_AUTHENTICATION_WINDOWS = "Windows Authentication";
    }
}