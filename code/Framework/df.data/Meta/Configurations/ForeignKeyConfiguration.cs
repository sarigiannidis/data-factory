// --------------------------------------------------------------------------------
// <copyright file="ForeignKeyConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public sealed class ForeignKeyConfiguration
        : IEntityTypeConfiguration<ForeignKey>
    {
        public void Configure(EntityTypeBuilder<ForeignKey> builder)
        {
            builder.ToTable("foreign_keys", "sys");
            builder.Property(_ => _.Name).HasColumnName("name");
            builder.Property(_ => _.ObjectId).HasColumnName("object_id");
            builder.Property(_ => _.ParentObjectId).HasColumnName("parent_object_id");
            builder.Property(_ => _.CreateDate).HasColumnName("create_date");
            builder.Property(_ => _.ModifyDate).HasColumnName("modify_date");
            builder.Property(_ => _.ReferencedObjectId).HasColumnName("referenced_object_id");
            builder.HasKey(_ => _.ObjectId);
            builder.HasOne(_ => _.Parent).WithMany(_ => _.ForeignKeys).HasForeignKey(_ => _.ParentObjectId);
            builder.HasOne(_ => _.Referenced).WithMany(_ => _.ReferringForeignKeys).HasForeignKey(_ => _.ReferencedObjectId);

            // builder.Property(_ => _.DeleteReferentialAction).HasColumnName("delete_referential_action");
            // builder.Property(_ => _.DeleteReferentialActionDesc).HasColumnName("delete_referential_action_desc");
            // builder.Property(_ => _.IsDisabled).HasColumnName("is_disabled");
            // builder.Property(_ => _.IsMsShipped).HasColumnName("is_ms_shipped");
            // builder.Property(_ => _.IsNotForReplication).HasColumnName("is_not_for_replication");
            // builder.Property(_ => _.IsNotTrusted).HasColumnName("is_not_trusted");
            // builder.Property(_ => _.IsPublished).HasColumnName("is_published");
            // builder.Property(_ => _.IsSchemaPublished).HasColumnName("is_schema_published");
            // builder.Property(_ => _.IsSystemNamed).HasColumnName("is_system_named");
            // builder.Property(_ => _.KeyIndexId).HasColumnName("key_index_id");
            // builder.Property(_ => _.PrincipalId).HasColumnName("principal_id");
            // builder.Property(_ => _.SchemaId).HasColumnName("schema_id");
            // builder.Property(_ => _.Type).HasColumnName("type");
            // builder.Property(_ => _.TypeDesc).HasColumnName("type_desc");
            // builder.Property(_ => _.UpdateReferentialAction).HasColumnName("update_referential_action");
            // builder.Property(_ => _.UpdateReferentialActionDesc).HasColumnName("update_referential_action_desc");
        }
    }
}