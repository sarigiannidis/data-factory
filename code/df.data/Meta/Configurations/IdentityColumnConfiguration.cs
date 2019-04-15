// --------------------------------------------------------------------------------
// <copyright file="IdentityColumnConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public sealed class IdentityColumnConfiguration
        : IEntityTypeConfiguration<IdentityColumn>
    {
        public void Configure(EntityTypeBuilder<IdentityColumn> builder)
        {
            builder.ToTable("identity_columns", "sys");
            builder.Property(_ => _.ColumnId).HasColumnName("column_id");
            builder.Property(_ => _.IncrementValue).HasColumnName("increment_value").HasColumnType("sql_variant");
            builder.Property(_ => _.LastValue).HasColumnName("last_value").HasColumnType("sql_variant");
            builder.Property(_ => _.ObjectId).HasColumnName("object_id");
            builder.Property(_ => _.SeedValue).HasColumnName("seed_value").HasColumnType("sql_variant");
            builder.HasKey(_ => new { _.ObjectId, _.ColumnId });
            builder.HasOne(_ => _.Table).WithMany(_ => _.IdentityColumns).HasForeignKey(_ => _.ObjectId);
            builder.HasOne(_ => _.Column).WithOne(_ => _.Identity).HasForeignKey<Column>(_ => new { _.ObjectId, _.ColumnId });
        }
    }
}