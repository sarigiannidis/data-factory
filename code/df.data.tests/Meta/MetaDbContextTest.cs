// --------------------------------------------------------------------------------
// <copyright file="MetaDbContextTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta.Tests
{
    using Df.Data.Tests;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using Xunit;
    using Xunit.Abstractions;

    public sealed class MetaDbContextTest
        : DataTestBase
    {
        public MetaDbContextTest(ITestOutputHelper output, DataFixture fixture)
            : base(output, fixture)
        {
        }

        [Fact]
        public void CallTheConstructor()
        {
            using (var mc = Fixture.CreateMetaDbContext())
            {
                var c = mc.Tables.Count();
                DfAssert.GreaterThan(c, 0);
            }
        }

        [Fact]
        public void ColumnsHaveForeignKeys()
        {
            using (var mc = Fixture.CreateMetaDbContext())
            {
                var columns = mc.Columns.Include(_ => _.ForeignKeyColumns);
                foreach (var column in columns)
                {
                    Assert.NotNull(column.ForeignKeyColumns);
                    foreach (var foreignKeyColumn in column.ForeignKeyColumns)
                    {
                        Assert.NotNull(foreignKeyColumn.Parent);
                        Assert.Same(foreignKeyColumn.Parent, column);
                    }
                }
            }
        }

        [Fact]
        public void ColumnsHaveReferringForeignKeys()
        {
            using (var mc = Fixture.CreateMetaDbContext())
            {
                var columns = mc.Columns.Include(_ => _.ReferringForeignKeyColumns);
                foreach (var column in columns)
                {
                    Assert.NotNull(column.ReferringForeignKeyColumns);
                    foreach (var foreignKeyColumn in column.ReferringForeignKeyColumns)
                    {
                        Assert.NotNull(foreignKeyColumn.Referenced);
                        Assert.Same(foreignKeyColumn.Referenced, column);
                    }
                }
            }
        }

        [Fact]
        public void ForeignKeyColumnsHaveParent()
        {
            using (var mc = Fixture.CreateMetaDbContext())
            {
                var foreignKeyColumns = mc.ForeignKeyColumns.Include(_ => _.Parent).ThenInclude(_ => _.Table).Include(_ => _.Referenced);
                foreach (var foreignKeyColumn in foreignKeyColumns)
                {
                    Assert.NotNull(foreignKeyColumn.Parent);
                    Assert.NotNull(foreignKeyColumn.Referenced);
                }
            }
        }

        [Fact]
        public void ForeignKeysHaveForeignKeyColumns()
        {
            using (var mc = Fixture.CreateMetaDbContext())
            {
                foreach (var foreignKey in mc.ForeignKeys.Include(_ => _.Columns))
                {
                    Assert.NotNull(foreignKey.Columns);
                    foreach (var foreignKeyColumn in foreignKey.Columns)
                    {
                        Assert.NotNull(foreignKeyColumn.ForeignKey);
                        Assert.Same(foreignKeyColumn.ForeignKey, foreignKey);
                    }
                }
            }
        }

        [Fact]
        public void TablesHaveColumns()
        {
            using (var mc = Fixture.CreateMetaDbContext())
            {
                var tables = mc.Tables.Include(_ => _.Columns);
                foreach (var table in tables)
                {
                    Assert.NotNull(table.Columns);
                    foreach (var column in table.Columns)
                    {
                        Assert.NotNull(column.Table);
                    }
                }
            }
        }

        [Fact]
        public void TablesHaveForeignKeys()
        {
            using (var mc = Fixture.CreateMetaDbContext())
            {
                var tables = mc.Tables.Include(_ => _.ForeignKeys);
                foreach (var table in tables)
                {
                    Assert.NotNull(table.ForeignKeys);
                    foreach (var foreignKey in table.ForeignKeys)
                    {
                        Assert.NotNull(foreignKey.Parent);
                        Assert.Same(foreignKey.Parent, table);
                    }
                }
            }
        }

        [Fact]
        public void TablesHaveIdentityColumns()
        {
            using (var mc = Fixture.CreateMetaDbContext())
            {
                var tables = mc.Tables.Include(_ => _.IdentityColumns).ThenInclude(_ => _.Column);
                foreach (var table in tables)
                {
                    Assert.NotNull(table.IdentityColumns);
                    foreach (var identityColumn in table.IdentityColumns)
                    {
                        Assert.NotNull(identityColumn.Table);
                        Assert.Same(identityColumn.Table, table);
                        Assert.NotNull(identityColumn.SeedValue);
                        Assert.NotNull(identityColumn.IncrementValue);
                        Assert.NotNull(identityColumn.Column);
                        Assert.NotNull(identityColumn.Column.Identity);
                        Assert.Same(identityColumn, identityColumn.Column.Identity);
                        Output.WriteLine("{0}.{1} IDENTITY({2}, {3}) => ", table.Name, identityColumn.Column.Name, identityColumn.SeedValue, identityColumn.IncrementValue, identityColumn.LastValue);
                    }
                }
            }
        }

        [Fact]
        public void TablesHaveReferringForeignKeys()
        {
            using (var mc = Fixture.CreateMetaDbContext())
            {
                var tables = mc.Tables.Include(_ => _.ReferringForeignKeys);
                foreach (var table in tables)
                {
                    Assert.NotNull(table.ReferringForeignKeys);
                    foreach (var foreignKey in table.ReferringForeignKeys)
                    {
                        Assert.NotNull(foreignKey.Referenced);
                        Assert.Same(foreignKey.Referenced, table);
                    }
                }
            }
        }
    }
}