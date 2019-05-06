// --------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace Df.Data
{
    public static class Constants
    {
        public const string SQL_TYPE_BIGINT = "bigint";

        public const string SQL_TYPE_BINARY = "binary";

        public const string SQL_TYPE_BIT = "bit";

        public const string SQL_TYPE_CHAR = "char";

        public const string SQL_TYPE_DATE = "date";

        public const string SQL_TYPE_DATETIME = "datetime";

        public const string SQL_TYPE_DATETIME2 = "datetime2";

        public const string SQL_TYPE_DATETIMEOFFSET = "datetimeoffset";

        public const string SQL_TYPE_DECIMAL = "decimal";

        public const string SQL_TYPE_FLOAT = "float";

        public const string SQL_TYPE_GEOGRAPHY = "geography";

        public const string SQL_TYPE_GEOMETRY = "geometry";

        public const string SQL_TYPE_HIERARCHYID = "hierarchyid";

        public const string SQL_TYPE_IMAGE = "image";

        public const string SQL_TYPE_INT = "int";

        public const string SQL_TYPE_MONEY = "money";

        public const string SQL_TYPE_NCHAR = "nchar";

        public const string SQL_TYPE_NTEXT = "ntext";

        public const string SQL_TYPE_NUMERIC = "numeric";

        public const string SQL_TYPE_NVARCHAR = "nvarchar";

        public const string SQL_TYPE_REAL = "real";

        public const string SQL_TYPE_SMALLDATETIME = "smalldatetime";

        public const string SQL_TYPE_SMALLINT = "smallint";

        public const string SQL_TYPE_SMALLMONEY = "smallmoney";

        public const string SQL_TYPE_SQL_VARIANT = "sql_variant";

        public const string SQL_TYPE_SYSNAME = "sysname";

        public const string SQL_TYPE_TEXT = "text";

        public const string SQL_TYPE_TIME = "time";

        public const string SQL_TYPE_TIMESTAMP = "timestamp";

        public const string SQL_TYPE_TINYINT = "tinyint";

        public const string SQL_TYPE_UNIQUEIDENTIFIER = "uniqueidentifier";

        public const string SQL_TYPE_VARBINARY = "varbinary";

        public const string SQL_TYPE_VARCHAR = "varchar";

        public const string SQL_TYPE_XML = "xml";

        internal const string EXTENSION_TMP_LDF = ".tmp.ldf";

        internal const string EXTENSION_TMP_MDF = ".tmp.mdf";

        internal const string SQL_CREATE_DATABASE_FORMAT = "CREATE DATABASE [{0}] ON PRIMARY(NAME= {0}_DATA, FILENAME = '{1}', FILEGROWTH = 512MB) LOG ON (NAME= {0}_LOG, FILENAME = '{2}', FILEGROWTH = 512MB)";
    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE1006 // Naming Styles