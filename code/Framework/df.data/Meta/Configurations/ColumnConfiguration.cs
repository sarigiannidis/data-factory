// --------------------------------------------------------------------------------
// <copyright file="ColumnConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public sealed class ColumnConfiguration
        : IEntityTypeConfiguration<Column>
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            _ = Check.NotNull(nameof(builder), builder);
            _ = builder.ToTable("columns", "sys");
            _ = builder.Property(_ => _.ObjectId).HasColumnName("object_id");
            _ = builder.Property(_ => _.Name).HasColumnName("name");
            _ = builder.Property(_ => _.ColumnId).HasColumnName("column_id");
            _ = builder.Property(_ => _.UserTypeId).HasColumnName("user_type_id");
            _ = builder.Property(_ => _.MaxLength).HasColumnName("max_length");
            _ = builder.Property(_ => _.Precision).HasColumnName("precision");
            _ = builder.Property(_ => _.Scale).HasColumnName("scale");
            _ = builder.Property(_ => _.IsNullable).HasColumnName("is_nullable");
            _ = builder.Property(_ => _.IsIdentity).HasColumnName("is_identity");
            _ = builder.Property(_ => _.IsComputed).HasColumnName("is_computed");
            _ = builder.HasKey(_ => new { _.ObjectId, _.ColumnId });
            _ = builder.HasOne(_ => _.Table).WithMany(_ => _.Columns).HasForeignKey(_ => _.ObjectId);
        }
    }
}