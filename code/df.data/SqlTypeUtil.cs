// --------------------------------------------------------------------------------
// <copyright file="SqlTypeUtil.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data
{
    using System;
    using System.Text;
    using static Constants;

    public static class SqlTypeUtil
    {
        public static Type GetDataType(string dataTypeName, short columnSize, bool allowDbNull)
        {
            var dataType = GetDataType(dataTypeName, columnSize);
            if (allowDbNull && dataType.IsValueType)
            {
                dataType = typeof(Nullable<>).MakeGenericType(dataType);
            }

            return dataType;
        }

        public static Type GetDataType(string dataTypeName) =>
            GetDataType(dataTypeName, 2);

        public static Type GetDataType(string dataTypeName, short columnSize)
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
                default:
                    return typeof(object);
            }
        }

        public static string GetSqlColumnDefinition(string name, string userType, bool nullable, short maxLength, short precision, short scale, object seedValue, object incrementValue)
        {
            var sb = new StringBuilder()
                .AppendFormatInvariant("[{0}]", name)
                .Append(' ')
                .Append(GetSqlType(userType, maxLength, precision, scale));

            if (seedValue != null && incrementValue != null)
            {
                sb.AppendFormatInvariant(" IDENTITY({0}, {1})", seedValue, incrementValue);
            }

            sb.Append(' ');
            if (!nullable)
            {
                sb.Append("NOT ");
            }

            sb.Append("NULL");
            return sb.ToString();
        }

        private static string GetSqlType(string userType, short maxLength, short precision, short scale)
        {
            var sb = new StringBuilder(userType.ToUpperInvariant());
            switch (userType)
            {
                case SQL_TYPE_NCHAR:
                case SQL_TYPE_NVARCHAR:
                    {
                        maxLength /= 2;
                        if (maxLength > 0)
                        {
                            sb.AppendFormatInvariant("({0})", maxLength);
                        }
                        else if (maxLength == -1)
                        {
                            sb.Append("(MAX)");
                        }

                        break;
                    }

                case SQL_TYPE_CHAR:
                case SQL_TYPE_VARCHAR:
                case SQL_TYPE_BINARY:
                case SQL_TYPE_VARBINARY:
                    {
                        if (maxLength > 0)
                        {
                            sb.AppendFormatInvariant("({0})", maxLength);
                        }
                        else if (maxLength == -1)
                        {
                            sb.Append("(MAX)");
                        }

                        break;
                    }

                case SQL_TYPE_FLOAT:
                    sb.AppendFormatInvariant("({0})", precision);
                    break;

                case SQL_TYPE_DATETIME2:
                case SQL_TYPE_DATETIMEOFFSET:
                case SQL_TYPE_TIME:
                    sb.AppendFormatInvariant("({0})", scale);
                    break;

                case SQL_TYPE_DECIMAL:
                case SQL_TYPE_NUMERIC:
                    sb.AppendFormatInvariant("({0},{1})", precision, scale);
                    break;
            }

            return sb.ToString();
        }
    }
}