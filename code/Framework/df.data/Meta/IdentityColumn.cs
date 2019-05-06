// --------------------------------------------------------------------------------
// <copyright file="IdentityColumn.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta
{
    public sealed class IdentityColumn
    {
        public Column Column { get; set; }

        public int ColumnId { get; set; }

        public object IncrementValue { get; set; }

        public object LastValue { get; set; }

        public int ObjectId { get; set; }

        public object SeedValue { get; set; }

        public Table Table { get; set; }
    }
}