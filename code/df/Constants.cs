// --------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df
{
    internal static class Constants
    {
        public const string APP_SETTINGS_FILE = "settings.json";

        public const int BUFFERSIZE = 4096;

        public const string EXTENSION = ".json";

        public const string SECTION_EXTENSIBILITY = "ValueFactoryManagerOptions";

        public const string SECTION_PREFERENCES = "Preferences";

        public const string SQL_COUNT_TABLES = "SELECT COUNT(*) FROM sys.tables";

        public const string SQL_LIST_DATABASES = "SELECT name FROM master.sys.databases";

        public const string SQL_LIST_TABLES = "SELECT name FROM sys.tables";
    }
}