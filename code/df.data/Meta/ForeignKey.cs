// --------------------------------------------------------------------------------
// <copyright file="ForeignKey.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta
{
    using System;
    using System.Collections.Generic;

    public sealed class ForeignKey
    {
        public List<ForeignKeyColumn> Columns { get; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string Name { get; set; }

        public int ObjectId { get; set; }

        public Table Parent { get; set; }

        public int ParentObjectId { get; set; }

        public Table Referenced { get; set; }

        public int? ReferencedObjectId { get; set; }

        // public bool IsDisabled { get; set; }
        // public bool IsMsShipped { get; set; }
        // public bool IsNotForReplication { get; set; }
        // public bool IsNotTrusted { get; set; }
        // public bool IsPublished { get; set; }
        // public bool IsSchemaPublished { get; set; }
        // public bool IsSystemNamed { get; set; }
        // public byte? DeleteReferentialAction { get; set; }
        // public byte? UpdateReferentialAction { get; set; }
        // public int SchemaId { get; set; }
        // public int? KeyIndexId { get; set; }
        // public int? PrincipalId { get; set; }
        // public string DeleteReferentialActionDesc { get; set; }
        // public string Type { get; set; }
        // public string TypeDesc { get; set; }
        // public string UpdateReferentialActionDesc { get; set; }
    }
}