// --------------------------------------------------------------------------------
// <copyright file="TableConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public sealed class TableConfiguration
        : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("tables", "sys");
            builder.Property(_ => _.CreateDate).HasColumnName("create_date");
            builder.Property(_ => _.ModifyDate).HasColumnName("modify_date");
            builder.Property(_ => _.Name).HasColumnName("name");
            builder.Property(_ => _.ObjectId).HasColumnName("object_id");
            builder.Property(_ => _.SchemaId).HasColumnName("schema_id");
            builder.HasKey(_ => _.ObjectId);

            // builder.Property(_ => _.Durability).HasColumnName("durability");
            // builder.Property(_ => _.DurabilityDesc).HasColumnName("durability_desc");
            // builder.Property(_ => _.FilestreamDataSpaceId).HasColumnName("filestream_data_space_id");
            // builder.Property(_ => _.HasReplicationFilter).HasColumnName("has_replication_filter");
            // builder.Property(_ => _.HasUncheckedAssemblyData).HasColumnName("has_unchecked_assembly_data");
            // builder.Property(_ => _.HistoryRetentionPeriod).HasColumnName("history_retention_period");
            // builder.Property(_ => _.HistoryTableId).HasColumnName("history_table_id");
            // builder.Property(_ => _.IsExternal).HasColumnName("is_external");
            // builder.Property(_ => _.IsFiletable).HasColumnName("is_filetable");
            // builder.Property(_ => _.IsMemoryOptimized).HasColumnName("is_memory_optimized");
            // builder.Property(_ => _.IsMergePublished).HasColumnName("is_merge_published");
            // builder.Property(_ => _.IsMsShipped).HasColumnName("is_ms_shipped");
            // builder.Property(_ => _.IsPublished).HasColumnName("is_published");
            // builder.Property(_ => _.IsRemoteDataArchiveEnabled).HasColumnName("is_remote_data_archive_enabled");
            // builder.Property(_ => _.IsReplicated).HasColumnName("is_replicated");
            // builder.Property(_ => _.IsSchemaPublished).HasColumnName("is_schema_published");
            // builder.Property(_ => _.IsSyncTranSubscribed).HasColumnName("is_sync_tran_subscribed");
            // builder.Property(_ => _.IsTrackedByCdc).HasColumnName("is_tracked_by_cdc");
            // builder.Property(_ => _.LargeValueTypesOutOfRow).HasColumnName("large_value_types_out_of_row");
            // builder.Property(_ => _.LobDataSpaceId).HasColumnName("lob_data_space_id");
            // builder.Property(_ => _.LockEscalation).HasColumnName("lock_escalation");
            // builder.Property(_ => _.LockEscalationDesc).HasColumnName("lock_escalation_desc");
            // builder.Property(_ => _.LockOnBulkLoad).HasColumnName("lock_on_bulk_load");
            // builder.Property(_ => _.MaxColumnIdUsed).HasColumnName("max_column_id_used");
            // builder.Property(_ => _.ParentObjectId).HasColumnName("parent_object_id");
            // builder.Property(_ => _.PrincipalId).HasColumnName("principal_id");
            // builder.Property(_ => _.TemporalType).HasColumnName("temporal_type");
            // builder.Property(_ => _.TemporalTypeDesc).HasColumnName("temporal_type_desc");
            // builder.Property(_ => _.TextInRowLimit).HasColumnName("text_in_row_limit");
            // builder.Property(_ => _.Type).HasColumnName("type");
            // builder.Property(_ => _.TypeDesc).HasColumnName("type_desc");
            // builder.Property(_ => _.UsesAnsiNulls).HasColumnName("uses_ansi_nulls");
            // The following columns are SQL2017+
            // builder.Property(_ => _.HistoryRetentionPeriodUnit).HasColumnName("history_retention_period_unit");
            // builder.Property(_ => _.HistoryRetentionPeriodUnitDesc).HasColumnName("history_retention_period_unit_desc");
            // builder.Property(_ => _.IsNode).HasColumnName("is_node");
            // builder.Property(_ => _.IsEdge).HasColumnName("is_edge");
        }
    }
}