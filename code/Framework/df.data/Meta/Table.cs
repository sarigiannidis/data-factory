// --------------------------------------------------------------------------------
// <copyright file="Table.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("{Name} ({ObjectId})")]
    public sealed class Table
    {
        public List<Column> Columns { get; }

        public DateTime CreateDate { get; set; }

        public List<ForeignKey> ForeignKeys { get; }

        public List<IdentityColumn> IdentityColumns { get; }

        public DateTime ModifyDate { get; set; }

        public string Name { get; set; }

        public int ObjectId { get; set; }

        public List<ForeignKey> ReferringForeignKeys { get; }

        public int SchemaId { get; set; }
    }
}