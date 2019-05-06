// --------------------------------------------------------------------------------
// <copyright file="SqlAuthenticationUtil.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Security
{
    using System;
    using static Constants;

    public static class SqlAuthenticationUtil
    {
        public static SqlAuthentication FromString(string str) =>
        str switch
        {
            SQL_AUTHENTICATION_WINDOWS => SqlAuthentication.Windows,
            SQL_AUTHENTICATION_SQL_SERVER => SqlAuthentication.SqlServer,
            SQL_AUTHENTICATION_AD_UNIVERSAL => SqlAuthentication.ActiveDirectoryUniversal,
            SQL_AUTHENTICATION_AD_PASSWORD => SqlAuthentication.ActiveDirectoryPassword,
            SQL_AUTHENTICATION_AD_INTEGRATED => SqlAuthentication.ActiveDirectoryIntegrated,
            _ => SqlAuthentication.None,
        };

        public static bool NeedsPassword(SqlAuthentication sqlAuthentication) =>
            sqlAuthentication == SqlAuthentication.SqlServer
            || sqlAuthentication == SqlAuthentication.ActiveDirectoryPassword;

        public static bool NeedsUserName(SqlAuthentication sqlAuthentication) =>
            sqlAuthentication == SqlAuthentication.SqlServer
            || sqlAuthentication == SqlAuthentication.ActiveDirectoryUniversal
            || sqlAuthentication == SqlAuthentication.ActiveDirectoryPassword;

        public static string ToString(SqlAuthentication sqlAuthentication) =>
        sqlAuthentication switch
        {
            SqlAuthentication.Windows => SQL_AUTHENTICATION_WINDOWS,
            SqlAuthentication.SqlServer => SQL_AUTHENTICATION_SQL_SERVER,
            SqlAuthentication.ActiveDirectoryUniversal => SQL_AUTHENTICATION_AD_UNIVERSAL,
            SqlAuthentication.ActiveDirectoryPassword => SQL_AUTHENTICATION_AD_PASSWORD,
            SqlAuthentication.ActiveDirectoryIntegrated => SQL_AUTHENTICATION_AD_INTEGRATED,
            _ => throw new InvalidOperationException(),
        };
    }
}