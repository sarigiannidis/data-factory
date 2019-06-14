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
            _ = Check.NotNull(nameof(builder), builder);
            _ = builder.ToTable("foreign_keys", "sys");
            _ = builder.Property(_ => _.Name).HasColumnName("name");
            _ = builder.Property(_ => _.ObjectId).HasColumnName("object_id");
            _ = builder.Property(_ => _.ParentObjectId).HasColumnName("parent_object_id");
            _ = builder.Property(_ => _.CreateDate).HasColumnName("create_date");
            _ = builder.Property(_ => _.ModifyDate).HasColumnName("modify_date");
            _ = builder.Property(_ => _.ReferencedObjectId).HasColumnName("referenced_object_id");
            _ = builder.HasKey(_ => _.ObjectId);
            _ = builder.HasOne(_ => _.Parent).WithMany(_ => _.ForeignKeys).HasForeignKey(_ => _.ParentObjectId);
            _ = builder.HasOne(_ => _.Referenced).WithMany(_ => _.ReferringForeignKeys).HasForeignKey(_ => _.ReferencedObjectId);
        }
    }
}