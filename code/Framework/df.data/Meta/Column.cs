// --------------------------------------------------------------------------------
// <copyright file="Column.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("{Name} - {ColumnId}")]
    public sealed class Column
    {
        public int ColumnId { get; set; }

        public List<ForeignKeyColumn> ForeignKeyColumns { get; }

        public IdentityColumn Identity { get; set; }

        public bool IsComputed { get; set; }

        public bool IsIdentity { get; set; }

        public bool? IsNullable { get; set; }

        public short MaxLength { get; set; }

        public string Name { get; set; }

        public int ObjectId { get; set; }

        public byte Precision { get; set; }

        public List<ForeignKeyColumn> ReferringForeignKeyColumns { get; }

        public byte Scale { get; set; }

        public Table Table { get; set; }

        public int UserTypeId { get; set; }
    }
}