// --------------------------------------------------------------------------------
// <copyright file="MetaDbContext.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta
{
    using Df.Data.Meta.Configurations;
    using Microsoft.EntityFrameworkCore;

    public sealed class MetaDbContext
        : DbContext
    {
        public DbSet<Column> Columns { get; set; }

        public DbSet<ForeignKeyColumn> ForeignKeyColumns { get; set; }

        public DbSet<ForeignKey> ForeignKeys { get; set; }

        public DbSet<IdentityColumn> IdentityColumns { get; set; }

        public DbSet<Table> Tables { get; set; }

        public MetaDbContext(DbContextOptions options)
            : base(options)
        {
        }

#pragma warning disable CA1801 // Review unused parameters
#pragma warning disable IDE0060 // Remove unused parameter

        /// <summary>
        /// Stand-in for OBJECT_DEFINITION.
        /// </summary>
        /// <param name="id">The id of an object.</param>
        /// <returns>The definition of the object with the given id.</returns>
        public static string ObjectDefinition(int id) =>
            ThrowLinqOnly();

        /// <summary>
        /// Stand-in for OBJECT_ID.
        /// </summary>
        /// <param name="name">The name of an object.</param>
        /// <returns>The id of the named object.</returns>
        public static int ObjectId(string name) =>
            ThrowLinqOnly();

        /// <summary>
        /// Stand-in for OBJECT_NAME.
        /// </summary>
        /// <param name="id">The id of an object.</param>
        /// <returns>The name of the object with the given id.</returns>
        public static string ObjectName(int id) =>
            ThrowLinqOnly();

        /// <summary>
        /// Stand-in for SCHEMA_NAME.
        /// </summary>
        /// <param name="id">The id of a schema.</param>
        /// <returns>The schema with the given id.</returns>
        public static string SchemaName(int id) =>
            ThrowLinqOnly();

        /// <summary>
        /// Stand-in for TYPE_NAME.
        /// </summary>
        /// <param name="id">The id of a database type.</param>
        /// <returns>The name of the database type with the given id.</returns>
        public static string TypeName(int id) =>
            ThrowLinqOnly();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfiguration(new ColumnConfiguration());
            _ = modelBuilder.ApplyConfiguration(new ForeignKeyColumnConfiguration());
            _ = modelBuilder.ApplyConfiguration(new ForeignKeyConfiguration());
            _ = modelBuilder.ApplyConfiguration(new IdentityColumnConfiguration());
            _ = modelBuilder.ApplyConfiguration(new TableConfiguration());

            _ = modelBuilder.HasDbFunction(() => ObjectDefinition(default)).HasName("OBJECT_DEFINITION").HasSchema(string.Empty);
            _ = modelBuilder.HasDbFunction(() => ObjectId(default)).HasName("OBJECT_ID").HasSchema(string.Empty);
            _ = modelBuilder.HasDbFunction(() => ObjectName(default)).HasName("OBJECT_NAME").HasSchema(string.Empty);
            _ = modelBuilder.HasDbFunction(() => SchemaName(default)).HasName("SCHEMA_NAME").HasSchema(string.Empty);
            _ = modelBuilder.HasDbFunction(() => TypeName(default)).HasName("TYPE_NAME").HasSchema(string.Empty);
        }

        private static dynamic ThrowLinqOnly() =>
            throw new DbFunctionException("Please use this function only in LINQ statements.");

#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore CA1801 // Review unused parameters
    }
}