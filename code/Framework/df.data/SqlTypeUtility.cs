// --------------------------------------------------------------------------------
// <copyright file="SqlTypeUtility.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using static Constants;

    public static class SqlTypeUtility
    {
        private static readonly Dictionary<string, Type> _Types = new Dictionary<string, Type>
        {
            { SQL_TYPE_DATE, typeof(DateTime) },
            { SQL_TYPE_DATETIME, typeof(DateTime) },
            { SQL_TYPE_DATETIME2, typeof(DateTime) },
            { SQL_TYPE_SMALLDATETIME, typeof(DateTime) },
            { SQL_TYPE_DATETIMEOFFSET, typeof(DateTimeOffset) },
            { SQL_TYPE_TIME, typeof(TimeSpan) },
            { SQL_TYPE_CHAR, typeof(char) },
            { SQL_TYPE_NCHAR, typeof(char) },
            { SQL_TYPE_VARCHAR, typeof(char) },
            { SQL_TYPE_NVARCHAR, typeof(char) },
            { SQL_TYPE_SYSNAME, typeof(string) },
            { SQL_TYPE_NTEXT, typeof(string) },
            { SQL_TYPE_TEXT, typeof(string) },
            { SQL_TYPE_XML, typeof(string) },
            { SQL_TYPE_DECIMAL, typeof(decimal) },
            { SQL_TYPE_NUMERIC, typeof(decimal) },
            { SQL_TYPE_SMALLMONEY, typeof(decimal) },
            { SQL_TYPE_MONEY, typeof(decimal) },
            { SQL_TYPE_BIT, typeof(bool) },
            { SQL_TYPE_TINYINT, typeof(byte) },
            { SQL_TYPE_SMALLINT, typeof(short) },
            { SQL_TYPE_INT, typeof(int) },
            { SQL_TYPE_BIGINT, typeof(long) },
            { SQL_TYPE_REAL, typeof(float) },
            { SQL_TYPE_FLOAT, typeof(double) },
            { SQL_TYPE_UNIQUEIDENTIFIER, typeof(Guid) },
            { SQL_TYPE_BINARY, typeof(byte[]) },
            { SQL_TYPE_VARBINARY, typeof(byte[]) },
            { SQL_TYPE_IMAGE, typeof(byte[]) },
            { SQL_TYPE_TIMESTAMP, typeof(byte[]) },
            { SQL_TYPE_HIERARCHYID, typeof(Microsoft.SqlServer.Types.SqlHierarchyId) },
            { SQL_TYPE_GEOMETRY, typeof(Microsoft.SqlServer.Types.SqlGeometry) },
            { SQL_TYPE_GEOGRAPHY, typeof(Microsoft.SqlServer.Types.SqlGeography) },
        };

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
            var baseType = GetBaseType(dataTypeName);
            return columnSize > 1 && baseType == typeof(char) ? typeof(string) : baseType;
        }

        public static string GetSqlColumnDefinition(string name, string userType, bool nullable, short maxLength, short precision, short scale, object seedValue, object incrementValue)
        {
            var sb = new StringBuilder()
                .AppendFormatInvariant("[{0}]", name)
                .Append(' ')
                .Append(GetSqlType(userType, maxLength, precision, scale));

            if (seedValue != null && incrementValue != null)
            {
                _ = sb.AppendFormatInvariant(" IDENTITY({0}, {1})", seedValue, incrementValue);
            }

            _ = sb.Append(' ');
            if (!nullable)
            {
                _ = sb.Append("NOT ");
            }

            _ = sb.Append("NULL");
            return sb.ToString();
        }

        private static Type GetBaseType(string dataTypeName) =>
                    _Types.TryGetValue(dataTypeName, out var value) ? value : typeof(object);

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
                        _ = sb.AppendFormatInvariant("({0})", maxLength);
                    }
                    else if (maxLength == -1)
                    {
                        _ = sb.Append("(MAX)");
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
                        _ = sb.AppendFormatInvariant("({0})", maxLength);
                    }
                    else if (maxLength == -1)
                    {
                        _ = sb.Append("(MAX)");
                    }

                    break;
                }

                case SQL_TYPE_FLOAT:
                    _ = sb.AppendFormatInvariant("({0})", precision);
                    break;

                case SQL_TYPE_DATETIME2:
                case SQL_TYPE_DATETIMEOFFSET:
                case SQL_TYPE_TIME:
                    _ = sb.AppendFormatInvariant("({0})", scale);
                    break;

                case SQL_TYPE_DECIMAL:
                case SQL_TYPE_NUMERIC:
                    _ = sb.AppendFormatInvariant("({0},{1})", precision, scale);
                    break;
            }

            return sb.ToString();
        }
    }
}