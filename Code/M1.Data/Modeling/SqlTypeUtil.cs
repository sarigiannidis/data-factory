namespace M1.Data.Modeling
{
    using System;
    using static Constants.SqlTypes;

    internal static class SqlTypeUtil
    {
        public static Type GetDataType(string dataTypeName, short columnSize, bool allowDbNull)
        {
            var dataType = GetRawDataType(dataTypeName, columnSize);
            if (allowDbNull && dataType.IsValueType)
                dataType = typeof(Nullable<>).MakeGenericType(dataType);
            return dataType;
        }

        private static Type GetRawDataType(string dataTypeName, short columnSize)
        {
            switch (dataTypeName)
            {
                case SQL_TYPE_DATE:
                case SQL_TYPE_DATETIME:
                case SQL_TYPE_DATETIME2:
                case SQL_TYPE_SMALLDATETIME:
                    return typeof(DateTime);

                case SQL_TYPE_DATETIMEOFFSET:
                    return typeof(DateTimeOffset);

                case SQL_TYPE_TIME:
                    return typeof(TimeSpan);

                case SQL_TYPE_CHAR:
                case SQL_TYPE_NCHAR:
                case SQL_TYPE_VARCHAR:
                case SQL_TYPE_NVARCHAR:
                    return (columnSize == 1) ? typeof(char) : typeof(string);

                case SQL_TYPE_SYSNAME:
                case SQL_TYPE_NTEXT:
                case SQL_TYPE_TEXT:
                case SQL_TYPE_XML:
                    return typeof(string);

                case SQL_TYPE_DECIMAL:
                case SQL_TYPE_NUMERIC:
                case SQL_TYPE_SMALLMONEY:
                case SQL_TYPE_MONEY:
                    return typeof(decimal);

                case SQL_TYPE_BIT:
                    return typeof(bool);

                case SQL_TYPE_TINYINT:
                    return typeof(byte);

                case SQL_TYPE_SMALLINT:
                    return typeof(short);

                case SQL_TYPE_INT:
                    return typeof(int);

                case SQL_TYPE_BIGINT:
                    return typeof(long);

                case SQL_TYPE_REAL:
                    return typeof(float);

                case SQL_TYPE_FLOAT:
                    return typeof(double);

                case SQL_TYPE_UNIQUEIDENTIFIER:
                    return typeof(Guid);

                case SQL_TYPE_BINARY:
                case SQL_TYPE_VARBINARY:
                case SQL_TYPE_IMAGE:
                case SQL_TYPE_TIMESTAMP:
                    return typeof(byte[]);

                case SQL_TYPE_HIERARCHYID:
                    return typeof(Microsoft.SqlServer.Types.SqlHierarchyId);

                case SQL_TYPE_GEOMETRY:
                    return typeof(Microsoft.SqlServer.Types.SqlGeometry);

                case SQL_TYPE_GEOGRAPHY:
                    return typeof(Microsoft.SqlServer.Types.SqlGeography);

                case SQL_TYPE_SQL_VARIANT:
                default:
                    return typeof(object);
            }
        }
    }
}