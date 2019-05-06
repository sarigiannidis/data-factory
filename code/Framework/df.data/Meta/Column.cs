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

        // public bool IsAnsiPadded { get; set; }
        // public bool IsFilestream { get; set; }
        // public bool IsRowguidcol { get; set; }
        // public bool IsXmlDocument { get; set; }
        // public bool? IsColumnSet { get; set; }
        // public bool? IsDtsReplicated { get; set; }
        // public bool? IsHidden { get; set; }
        // public bool? IsMasked { get; set; }
        // public bool? IsMergePublished { get; set; }
        // public bool? IsNonSqlSubscribed { get; set; }
        // public bool? IsReplicated { get; set; }
        // public bool? IsSparse { get; set; }
        // public byte SystemTypeId { get; set; }
        // public byte? GeneratedAlwaysType { get; set; }
        // public int DefaultObjectId { get; set; }
        // public int RuleObjectId { get; set; }
        // public int XmlCollectionId { get; set; }
        // public int? ColumnEncryptionKeyId { get; set; }
        // public int? EncryptionType { get; set; }
        // public int? GraphType { get; set; }
        // public string CollationName { get; set; }
        // public string ColumnEncryptionKeyDatabaseName { get; set; }
        // public string EncryptionAlgorithmName { get; set; }
        // public string EncryptionTypeDesc { get; set; }
        // public string GeneratedAlwaysTypeDesc { get; set; }
        // public string GraphTypeDesc { get; set; }
    }
}