// --------------------------------------------------------------------------------
// <copyright file="ForeignKeyColumnConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public sealed class ForeignKeyColumnConfiguration
        : IEntityTypeConfiguration<ForeignKeyColumn>
    {
        public void Configure(EntityTypeBuilder<ForeignKeyColumn> builder)
        {
            _ = builder.ToTable("foreign_key_columns", "sys");
            _ = builder.Property(_ => _.ConstraintObjectId).HasColumnName("constraint_object_id");
            _ = builder.Property(_ => _.ConstraintColumnId).HasColumnName("constraint_column_id");
            _ = builder.Property(_ => _.ParentObjectId).HasColumnName("parent_object_id");
            _ = builder.Property(_ => _.ParentColumnId).HasColumnName("parent_column_id");
            _ = builder.Property(_ => _.ReferencedObjectId).HasColumnName("referenced_object_id");
            _ = builder.Property(_ => _.ReferencedColumnId).HasColumnName("referenced_column_id");
            _ = builder.HasKey(_ => new { _.ConstraintObjectId, _.ConstraintColumnId });
            _ = builder.HasOne(_ => _.ForeignKey).WithMany(_ => _.Columns).HasForeignKey(_ => _.ConstraintObjectId);
            _ = builder.HasOne(_ => _.Parent).WithMany(_ => _.ForeignKeyColumns).HasForeignKey(_ => new { _.ParentObjectId, _.ParentColumnId });
            _ = builder.HasOne(_ => _.Referenced).WithMany(_ => _.ReferringForeignKeyColumns).HasForeignKey(_ => new { _.ReferencedObjectId, _.ReferencedColumnId });
        }
    }
}