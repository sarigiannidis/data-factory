// --------------------------------------------------------------------------------
// <copyright file="ProjectFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io
{
    using Df.Data.Meta;
    using Df.Io.Descriptive;
    using Df.Io.Prescriptive;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal sealed class ProjectFactory
        : IProjectFactory
    {
        private DateTimeOffset CreationTime { get; } = DateTimeOffset.Now;

        private JsonUtility JsonUtility { get; }

        private IMetaDbContextFactory MetaDbContextFactory { get; }

        public ProjectFactory(IMetaDbContextFactory metaDbContextFactory, JsonUtility jsonUtility)
        {
            MetaDbContextFactory = Check.NotNull(nameof(metaDbContextFactory), metaDbContextFactory);
            JsonUtility = Check.NotNull(nameof(jsonUtility), jsonUtility);
        }

        public Project CreateNew(string connectionString)
        {
            _ = Check.NotNull(nameof(connectionString), connectionString);
            var descriptor = CreateDescriptor(connectionString);
            var prescriptor = new Prescriptor();
            return new Project(descriptor, prescriptor, CreationTime, CreationTime);
        }

        private static void ReadAllForeignKeys(MetaDbContext context, IReadOnlyList<TableDescription> tableDescriptions)
        {
            foreach (var parent in tableDescriptions)
            {
                parent.ForeignKeyDescriptions = ReadForeignKeysForParent(context, tableDescriptions, parent);
            }
        }

        private static IQueryable<ColumnDescription> ReadColumnDescriptions(MetaDbContext context, TableDescription tableDescription)
        {
            var withIdentity = from column in context.Columns.Include(_ => _.Identity)
                               where column.IsIdentity && column.ObjectId == tableDescription.ObjectId
                               select new ColumnDescription(
                                   column.ColumnId,
                                   column.Name,
                                   MetaDbContext.TypeName(column.UserTypeId),
                                   column.IsNullable ?? false,
                                   column.IsComputed,
                                   column.MaxLength,
                                   column.Precision,
                                   column.Scale,
                                   new Identity(column.Identity.SeedValue, column.Identity.IncrementValue))
                               {
                                   Parent = tableDescription,
                               };
            var withoutIdentity = from column in context.Columns
                                  where !column.IsIdentity && column.ObjectId == tableDescription.ObjectId
                                  select new ColumnDescription(
                                      column.ColumnId,
                                      column.Name,
                                      MetaDbContext.TypeName(column.UserTypeId),
                                      column.IsNullable ?? false,
                                      column.IsComputed,
                                      column.MaxLength,
                                      column.Precision,
                                      column.Scale,
                                      null)
                                  {
                                      Parent = tableDescription,
                                  };

            return withIdentity.Concat(withoutIdentity).OrderBy(_ => _.Order);
        }

        private static IReadOnlyList<ForeignKeyDescription> ReadForeignKeysForParent(MetaDbContext context, IReadOnlyList<TableDescription> tables, TableDescription parent)
        {
            var foreignKeyGroups = from fkc in context.ForeignKeyColumns
                                   where fkc.ParentObjectId == parent.ObjectId
                                   group new
                                   {
                                       fkc.ConstraintColumnId,
                                       fkc.ParentColumnId,
                                       fkc.ReferencedColumnId,
                                   }
                                   by fkc.ConstraintObjectId;

            var foreignKeys = new List<ForeignKeyDescription>();
            foreach (var foreignKeyColumnGroup in foreignKeyGroups)
            {
                var foreignKey = context.ForeignKeys.Single(_ => _.ObjectId == foreignKeyColumnGroup.Key);
                var referenced = tables.Single(_ => _.ObjectId == foreignKey.ReferencedObjectId);
                var columnRelationships = new List<ColumnRelationshipDescription>();
                foreach (var foreignKeyColumn in foreignKeyColumnGroup)
                {
                    var parentColumn = parent.ColumnDescriptions.Single(_ => _.Order == foreignKeyColumn.ParentColumnId);
                    var referencedColumn = referenced.ColumnDescriptions.Single(c => c.Order == foreignKeyColumn.ReferencedColumnId);
                    var columnRelationship = new ColumnRelationshipDescription(foreignKeyColumn.ConstraintColumnId)
                    {
                        Parent = parentColumn,
                        Referenced = referencedColumn,
                    };
                    columnRelationships.Add(columnRelationship);
                }

                var foreignKeyDescription = new ForeignKeyDescription(foreignKey.Name, foreignKey.CreateDate, foreignKey.ModifyDate)
                {
                    ColumnRelationshipDescriptions = columnRelationships,
                };

                foreignKeys.Add(foreignKeyDescription);
            }

            return foreignKeys;
        }

        private static IEnumerable<TableDescription> ReadTableDescriptions(MetaDbContext context)
        {
            var tableDescriptions = from tableDescription in context.Tables
                                    select new TableDescription(
                                        tableDescription.ObjectId,
                                        MetaDbContext.SchemaName(tableDescription.SchemaId),
                                        tableDescription.Name,
                                        new DateTimeOffset(tableDescription.CreateDate),
                                        new DateTimeOffset(tableDescription.ModifyDate));

            foreach (var tableDescription in tableDescriptions)
            {
                tableDescription.ColumnDescriptions = ReadColumnDescriptions(context, tableDescription).ToList();
                yield return tableDescription;
            }
        }

        private string CalculateChecksum(IReadOnlyList<TableDescription> tableDescriptions)
        {
            using var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream, Encoding.Unicode, 1024, true);
            var str = JsonUtility.Serialize(tableDescriptions);
            streamWriter.WriteLine(str);
            _ = stream.Seek(0, SeekOrigin.Begin);
            return HashUtility.ComputeHash(stream);
        }

        private Descriptor CreateDescriptor(string connectionString)
        {
            var tables = CreateTableDescriptions(connectionString);
            var checksum = CalculateChecksum(tables);
            return new Descriptor(connectionString, checksum, CreationTime)
            {
                TableDescriptions = tables,
            };
        }

        private IReadOnlyList<TableDescription> CreateTableDescriptions(string connectionString)
        {
            using var context = MetaDbContextFactory.Create(connectionString);
            var tables = ReadTableDescriptions(context).ToList();
            ReadAllForeignKeys(context, tables);
            return tables;
        }
    }
}