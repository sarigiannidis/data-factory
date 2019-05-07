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
            _ = builder.ToTable("tables", "sys");
            _ = builder.Property(_ => _.CreateDate).HasColumnName("create_date");
            _ = builder.Property(_ => _.ModifyDate).HasColumnName("modify_date");
            _ = builder.Property(_ => _.Name).HasColumnName("name");
            _ = builder.Property(_ => _.ObjectId).HasColumnName("object_id");
            _ = builder.Property(_ => _.SchemaId).HasColumnName("schema_id");
            _ = builder.HasKey(_ => _.ObjectId);
        }
    }
}