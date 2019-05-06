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

            // builder.Property(_ => _.CollationName).HasColumnName("collation_name");
            // builder.Property(_ => _.ColumnEncryptionKeyDatabaseName).HasColumnName("column_encryption_key_database_name");
            // builder.Property(_ => _.ColumnEncryptionKeyId).HasColumnName("column_encryption_key_id");
            // builder.Property(_ => _.DefaultObjectId).HasColumnName("default_object_id");
            // builder.Property(_ => _.EncryptionAlgorithmName).HasColumnName("encryption_algorithm_name");
            // builder.Property(_ => _.EncryptionType).HasColumnName("encryption_type");
            // builder.Property(_ => _.EncryptionTypeDesc).HasColumnName("encryption_type_desc");
            // builder.Property(_ => _.GeneratedAlwaysType).HasColumnName("generated_always_type");
            // builder.Property(_ => _.GeneratedAlwaysTypeDesc).HasColumnName("generated_always_type_desc");
            // builder.Property(_ => _.GraphType).HasColumnName("graph_type");
            // builder.Property(_ => _.GraphTypeDesc).HasColumnName("graph_type_desc");
            // builder.Property(_ => _.IsAnsiPadded).HasColumnName("is_ansi_padded");
            // builder.Property(_ => _.IsColumnSet).HasColumnName("is_column_set");
            // builder.Property(_ => _.IsDtsReplicated).HasColumnName("is_dts_replicated");
            // builder.Property(_ => _.IsFilestream).HasColumnName("is_filestream");
            // builder.Property(_ => _.IsHidden).HasColumnName("is_hidden");
            // builder.Property(_ => _.IsMasked).HasColumnName("is_masked");
            // builder.Property(_ => _.IsMergePublished).HasColumnName("is_merge_published");
            // builder.Property(_ => _.IsNonSqlSubscribed).HasColumnName("is_non_sql_subscribed");
            // builder.Property(_ => _.IsReplicated).HasColumnName("is_replicated");
            // builder.Property(_ => _.IsRowguidcol).HasColumnName("is_rowguidcol");
            // builder.Property(_ => _.IsSparse).HasColumnName("is_sparse");
            // builder.Property(_ => _.IsXmlDocument).HasColumnName("is_xml_document");
            // builder.Property(_ => _.RuleObjectId).HasColumnName("rule_object_id");
            // builder.Property(_ => _.SystemTypeId).HasColumnName("system_type_id");
            // builder.Property(_ => _.XmlCollectionId).HasColumnName("xml_collection_id");
        }
    }
}