// --------------------------------------------------------------------------------
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
            object[] GetValues(TEntity entity) => columns.Select(_ => _.property(entity)).ToArray();

            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            for (var i = 0; i < columns.Length; i++)
            {
                var (_, name, length) = columns[i];
                _ = sb2.AppendFormatInvariant(ColumnFormat(0, length), name);
                _ = sb1.Append(ColumnFormat(i, length));
            }

            _ = sb2.AppendLine();
            _ = sb2.AppendLine(new string('-', columns.Sum(_ => _.length)));
            var entityFormat = sb1.ToString();

            foreach (var entity in entities)
            {
                _ = sb2.AppendFormatInvariant(entityFormat, GetValues(entity));
                _ = sb2.AppendLine();
            }

            return sb2.ToString();

            static string ColumnFormat(int index, int length) => "{{{0},-{1}}}".FormatInvariant(index, length);
        }
    }
}