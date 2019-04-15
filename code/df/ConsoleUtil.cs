// --------------------------------------------------------------------------------
// <copyright file="ConsoleUtil.cs" company="Michalis Sarigiannidis">
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

    internal static class ConsoleUtil
    {
        public static string ToConsoleTable<TEntity>(IEnumerable<TEntity> entities, params (Func<TEntity, object> property, string name, int length)[] columns)
        {
            object[] GetValues(TEntity entity) =>
                columns.Select(_ => _.property(entity)).ToArray();

            string ColumnFormat(int index, int length) =>
                "{{{0},-{1}}}".FormatInvariant(index, length);

            var entityFormatBuilder = new StringBuilder();
            var sb = new StringBuilder();
            for (var index = 0; index < columns.Length; index++)
            {
                var (_, name, length) = columns[index];
                sb.AppendFormatInvariant(ColumnFormat(0, length), name);
                entityFormatBuilder.Append(ColumnFormat(index, length));
            }

            sb.AppendLine();
            sb.AppendLine(new string('-', columns.Sum(_ => _.length)));
            var entityFormat = entityFormatBuilder.ToString();

            foreach (var entity in entities)
            {
                sb.AppendFormatInvariant(entityFormat, GetValues(entity));
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}