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

        // public bool HasUncheckedAssemblyData { get; set; }
        // public bool IsExternal { get; set; }
        // public bool IsMsShipped { get; set; }
        // public bool IsPublished { get; set; }
        // public bool IsSchemaPublished { get; set; }
        // public bool LockOnBulkLoad { get; set; }
        // public bool? HasReplicationFilter { get; set; }
        // public bool? IsEdge { get; set; }
        // public bool? IsFiletable { get; set; }
        // public bool? IsMemoryOptimized { get; set; }
        // public bool? IsMergePublished { get; set; }
        // public bool? IsNode { get; set; }
        // public bool? IsRemoteDataArchiveEnabled { get; set; }
        // public bool? IsReplicated { get; set; }
        // public bool? IsSyncTranSubscribed { get; set; }
        // public bool? IsTrackedByCdc { get; set; }
        // public bool? LargeValueTypesOutOfRow { get; set; }
        // public bool? UsesAnsiNulls { get; set; }
        // public byte? Durability { get; set; }
        // public byte? LockEscalation { get; set; }
        // public byte? TemporalType { get; set; }
        // public int LobDataSpaceId { get; set; }
        // public int MaxColumnIdUsed { get; set; }
        // public int ParentObjectId { get; set; }
        // public int? FilestreamDataSpaceId { get; set; }
        // public int? HistoryRetentionPeriod { get; set; }
        // public int? HistoryRetentionPeriodUnit { get; set; }
        // public int? HistoryTableId { get; set; }
        // public int? PrincipalId { get; set; }
        // public int? TextInRowLimit { get; set; }
        // public string DurabilityDesc { get; set; }
        // public string HistoryRetentionPeriodUnitDesc { get; set; }
        // public string LockEscalationDesc { get; set; }
        // public string TemporalTypeDesc { get; set; }
        // public string Type { get; set; }
        // public string TypeDesc { get; set; }
    }
}