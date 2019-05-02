#pragma warning disable IDE1006 // Naming Styles

namespace M1.Data
{
    internal static class Constants
    {
        public static class Columns
        {
            public const string COLUMN_ALLOW_DB_NULL = "allow_db_null";
            public const string COLUMN_COLUMN_NAME = "column_name";
            public const string COLUMN_COLUMN_ORDINAL = "column_ordinal";
            public const string COLUMN_COLUMN_SIZE = "column_size";
            public const string COLUMN_DATA_TYPE_NAME = "data_type_name";
            public const string COLUMN_FULL_NAME = "full_name";
            public const string COLUMN_IS_SYSTEM = "is_system";
            public const string COLUMN_NUMERICOLUMN_PRECISION = "numeriCOLUMN_precision";
            public const string COLUMN_NUMERICOLUMN_SCALE = "numeriCOLUMN_scale";
            public const string COLUMN_SCHEMA_NAME = "schema_name";
            public const string COLUMN_VIEW_ID = "view_id";
            public const string COLUMN_VIEW_NAME = "view_name";
        }

        public static class Parameters
        {
            public const string BUILDER_PARAMETER_NAME = "builder";
        }

        public static class SqlStatements
        {
            public const string SQL_SELECT_COLUMNS = "SELECT c.object_id view_id, c.name column_name, column_id column_ordinal, TYPE_NAME(system_type_id) data_type_name, precision numeriCOLUMN_precision, scale numeriCOLUMN_scale, max_length column_size, is_nullable allow_db_null FROM sys.all_columns c INNER JOIN sys.all_views v ON c.object_id = v.object_id;";
            public const string SQL_SELECT_VIEWS = "SELECT object_id view_id, SCHEMA_NAME(schema_id) schema_name, name view_name, CONCAT(SCHEMA_NAME(schema_id), '.', name) full_name, is_ms_shipped is_system FROM sys.all_views ORDER BY is_ms_shipped, schema_name, name;";
        }

        public static class SqlTypes
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
        }

        public static class Tables
        {
            public const string TABLE_COLS = "COLUMNS";
            public const string TABLE_VIEWS = "VIEWS";
        }
    }
}

#pragma warning restore IDE1006 // Naming Styles