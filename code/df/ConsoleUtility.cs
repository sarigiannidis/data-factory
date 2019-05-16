﻿// --------------------------------------------------------------------------------
// <copyright file="ConsoleUtility.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal static class ConsoleUtility
    {
        public static string ToConsoleTable<TEntity>(IEnumerable<TEntity> entities, params (Func<TEntity, object> property, string name, int length)[] columns)
        {
            object[] GetValues(TEntity entity) =>
                columns.Select(_ => _.property(entity)).ToArray();

            var entityFormatBuilder = new StringBuilder();
            var sb = new StringBuilder();
            for (var index = 0; index < columns.Length; index++)
            {
                var (_, name, length) = columns[index];
                _ = sb.AppendFormatInvariant(ColumnFormat(0, length), name);
                _ = entityFormatBuilder.Append(ColumnFormat(index, length));
            }

            _ = sb.AppendLine();
            _ = sb.AppendLine(new string('-', columns.Sum(_ => _.length)));
            var entityFormat = entityFormatBuilder.ToString();

            foreach (var entity in entities)
            {
                _ = sb.AppendFormatInvariant(entityFormat, GetValues(entity));
                _ = sb.AppendLine();
            }

            return sb.ToString();

            static string ColumnFormat(int index, int length) =>
                "{{{0},-{1}}}".FormatInvariant(index, length);
        }
    }
}