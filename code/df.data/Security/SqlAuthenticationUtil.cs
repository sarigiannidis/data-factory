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
        public static SqlAuthentication FromString(string str)
        {
            switch (str)
            {
                case SQL_AUTHENTICATION_WINDOWS:
                    return SqlAuthentication.Windows;

                case SQL_AUTHENTICATION_SQL_SERVER:
                    return SqlAuthentication.SqlServer;

                case SQL_AUTHENTICATION_AD_UNIVERSAL:
                    return SqlAuthentication.ActiveDirectoryUniversal;

                case SQL_AUTHENTICATION_AD_PASSWORD:
                    return SqlAuthentication.ActiveDirectoryPassword;

                case SQL_AUTHENTICATION_AD_INTEGRATED:
                    return SqlAuthentication.ActiveDirectoryIntegrated;

                default:
                    return SqlAuthentication.None;
            }
        }

        public static bool NeedsPassword(SqlAuthentication sqlAuthentication) =>
                    sqlAuthentication == SqlAuthentication.SqlServer
            || sqlAuthentication == SqlAuthentication.ActiveDirectoryPassword;

        public static bool NeedsUserName(SqlAuthentication sqlAuthentication) =>
            sqlAuthentication == SqlAuthentication.SqlServer
                    || sqlAuthentication == SqlAuthentication.ActiveDirectoryUniversal
                    || sqlAuthentication == SqlAuthentication.ActiveDirectoryPassword;

        public static string ToString(SqlAuthentication sqlAuthentication)
        {
            switch (sqlAuthentication)
            {
                case SqlAuthentication.Windows:
                    return SQL_AUTHENTICATION_WINDOWS;

                case SqlAuthentication.SqlServer:
                    return SQL_AUTHENTICATION_SQL_SERVER;

                case SqlAuthentication.ActiveDirectoryUniversal:
                    return SQL_AUTHENTICATION_AD_UNIVERSAL;

                case SqlAuthentication.ActiveDirectoryPassword:
                    return SQL_AUTHENTICATION_AD_PASSWORD;

                case SqlAuthentication.ActiveDirectoryIntegrated:
                    return SQL_AUTHENTICATION_AD_INTEGRATED;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}