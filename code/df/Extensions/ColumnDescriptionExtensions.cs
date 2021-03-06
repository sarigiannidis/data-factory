﻿// --------------------------------------------------------------------------------
// <copyright file="ColumnDescriptionExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Descriptive
{
    using Df.Data;
    using Df.Extensibility;
    using System;
    using static Df.Data.Constants;

    public static class ColumnDescriptionExtensions
    {
        public static ConfiguratorConstraints CreateConstraints(this ColumnDescription columnDescription)
        {
            _ = Check.NotNull(nameof(columnDescription), columnDescription);
            return new ConfiguratorConstraints
            {
#pragma warning disable CA1062 // Validate arguments of public methods
                MaxLength = GetMaxCharLength(columnDescription),
#pragma warning restore CA1062 // Validate arguments of public methods
                Type = columnDescription.ResolveUserType(),
                IncrementValue = columnDescription.Identity?.IncrementValue,
                SeedValue = columnDescription.Identity?.SeedValue,
            };
        }

        public static Type ResolveUserType(this ColumnDescription columnDescription)
        {
            _ = Check.NotNull(nameof(columnDescription), columnDescription);
            return columnDescription.UserType switch
            {
                "sql_variant" => typeof(int),
                _ => SqlTypeUtility.GetDataType(columnDescription.UserType, columnDescription.MaxLength),
            };
        }

        private static int GetMaxCharLength(ColumnDescription columnDescription) => columnDescription.UserType switch
            {
                SQL_TYPE_NCHAR => columnDescription.MaxLength / 2,
                SQL_TYPE_NTEXT => columnDescription.MaxLength / 2,
                SQL_TYPE_NVARCHAR => columnDescription.MaxLength / 2,
                _ => columnDescription.MaxLength
            };
    }
}