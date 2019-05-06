// --------------------------------------------------------------------------------
// <copyright file="IoExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Production
{
    using Df.Data;
    using Df.Io.Descriptive;
    using Df.Io.Prescriptive;
    using System;

    internal static class IoExtensions
    {
        public static string SqlDefinition(this ColumnDescription columnDescription) =>
            SqlTypeUtil.GetSqlColumnDefinition(
                    columnDescription.Name,
                    columnDescription.UserType,
                    columnDescription.Nullable,
                    columnDescription.MaxLength,
                    columnDescription.Precision,
                    columnDescription.Scale,
                    null,
                    null);

        public static string TableName(this TableDescription tableDescription) =>
            "[{0}].[{1}]".FormatInvariant(tableDescription.Schema, tableDescription.Name);

        public static string TableName(this TablePrescription tablePrescription) =>
            TableName(tablePrescription.TableDescription);
    }
}