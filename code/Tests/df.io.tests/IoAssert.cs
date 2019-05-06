// --------------------------------------------------------------------------------
// <copyright file="IoAssert.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Tests
{
    using Df.Extensibility;
    using Df.Io.Descriptive;
    using Df.Io.Prescriptive;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Xunit;

    internal static class IoAssert
    {
        public static void AsExpected(Project project)
        {
            Assert.NotNull(project);
            DfAssert.Past(project.Created);
            DfAssert.Past(project.Modified);
            DfAssert.GreaterThanOrEqual(project.Modified, project.Created);
            AsExpected(project.Descriptor);
            AsExpected(project.Prescriptor);

            // Checking the references
            var tableDescriptions = project.Descriptor.TableDescriptions;
            var foreignKeyDescriptions = tableDescriptions.SelectMany(_ => _.ForeignKeyDescriptions);

            foreach (var tableDescription in tableDescriptions)
            {
                foreach (var columnDescription in tableDescription.ColumnDescriptions)
                {
                    Assert.Same(columnDescription.Parent, tableDescription);
                }

                foreach (var fk in tableDescription.ForeignKeyDescriptions)
                {
                    Assert.Same(fk.Parent, tableDescription);
                    Assert.Same(fk.Referenced, tableDescriptions.Single(_ => _.ObjectId == fk.Referenced.ObjectId));
                    foreach (var cr in fk.ColumnRelationshipDescriptions)
                    {
                        Assert.Same(cr.Parent, tableDescription.ColumnDescriptions.Single(_ => _.Order == cr.Parent.Order));
                        Assert.Same(cr.Referenced, fk.Referenced.ColumnDescriptions.Single(_ => _.Order == cr.Referenced.Order));
                    }
                }
            }
        }

        private static void AsExpected(Prescriptor prescriptor)
        {
            Assert.NotNull(prescriptor);
            AsExpected(prescriptor.TablePrescriptions);
            AsExpected(prescriptor.ValueFactoryPrescriptions);
        }

        private static void AsExpected(IReadOnlyList<TablePrescription> tablePrescriptions)
        {
            Assert.NotNull(tablePrescriptions);
            Assert.All(tablePrescriptions, AsExpected);
        }

        private static void AsExpected(TablePrescription tablePrescription)
        {
            Assert.NotNull(tablePrescription);
            AsExpected(tablePrescription.ColumnPrescriptions);
        }

        private static void AsExpected(IReadOnlyList<ValueFactoryPrescription> valueFactoryPrescriptions)
        {
            Assert.NotNull(valueFactoryPrescriptions);
            Assert.All(valueFactoryPrescriptions, AsExpected);
        }

        private static void AsExpected(ValueFactoryPrescription valueFactoryPrescription)
        {
            Assert.NotNull(valueFactoryPrescription.Name);
            Assert.NotNull(valueFactoryPrescription.Reference);
            AsExpected(valueFactoryPrescription.Configuration);
        }

        private static void AsExpected(IValueFactoryConfiguration configuration)
        {
            Assert.NotNull(configuration);

            // Testing we can access all properties
            var acessors = configuration
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(_ => _.CanRead && _.GetIndexParameters().Length == 0)
                .Select((Func<PropertyInfo, Func<object>>)(_ => () => _.GetValue(configuration)));

            foreach (var item in acessors)
            {
                _ = item();
            }
        }

        private static void AsExpected(IReadOnlyList<ColumnPrescription> columnPrescriptions)
        {
            Assert.NotNull(columnPrescriptions);
            Assert.All(columnPrescriptions, AsExpected);
        }

        private static void AsExpected(ColumnPrescription columnPrescription)
        {
            Assert.NotNull(columnPrescription);
            Assert.NotNull(columnPrescription.ColumnDescription);
            Assert.NotNull(columnPrescription.ValueFactoryPrescription);
        }

        private static void AsExpected(Descriptor descriptor)
        {
            Assert.NotNull(descriptor);
            DfAssert.NotEmpty(descriptor.Checksum);
            DfAssert.NotEmpty(descriptor.ConnectionString);
            AsExpected(descriptor.TableDescriptions);
        }

        private static void AsExpected(ColumnDescription columnDescription)
        {
            Assert.NotNull(columnDescription);
            DfAssert.NotEmpty(columnDescription.Name);
            DfAssert.GreaterThanOrEqual(columnDescription.Order, 0);

            AsExpected(columnDescription.Identity);
        }

        private static void AsExpected(Identity identity)
        {
            if (identity == null)
            {
                return;
            }

            Assert.NotNull(identity.SeedValue);
            Assert.NotNull(identity.IncrementValue);
        }

        private static void AsExpected(TableDescription tableDescription)
        {
            Assert.NotNull(tableDescription);
            DfAssert.NotEmpty(tableDescription.Name);
            DfAssert.Past(tableDescription.Created);
            DfAssert.Past(tableDescription.Modified);
            DfAssert.GreaterThanOrEqual(tableDescription.Modified, tableDescription.Created);
            DfAssert.GoodId(tableDescription.ObjectId);
            DfAssert.NotEmpty(tableDescription.Schema);
            AsExpected(tableDescription.ColumnDescriptions);
            AsExpected(tableDescription.ForeignKeyDescriptions);
        }

        private static void AsExpected(ForeignKeyDescription foreignKeyDescription)
        {
            Assert.NotNull(foreignKeyDescription);
            DfAssert.NotEmpty(foreignKeyDescription.Name);
            DfAssert.Past(foreignKeyDescription.Created);
            DfAssert.Past(foreignKeyDescription.Modified);
            Assert.NotNull(foreignKeyDescription.Parent);
            Assert.NotNull(foreignKeyDescription.Referenced);
            DfAssert.GreaterThanOrEqual(foreignKeyDescription.Modified, foreignKeyDescription.Created);
            AsExpected(foreignKeyDescription.ColumnRelationshipDescriptions);
        }

        private static void AsExpected(ColumnRelationshipDescription columnRelationshipDescription)
        {
            Assert.NotNull(columnRelationshipDescription);
            Assert.NotNull(columnRelationshipDescription.Parent);
            Assert.NotNull(columnRelationshipDescription.Referenced);
            Assert.NotSame(columnRelationshipDescription.Parent, columnRelationshipDescription.Referenced);
        }

        private static void AsExpected(IReadOnlyList<ColumnRelationshipDescription> columnRelationshipDescriptions)
        {
            Assert.NotNull(columnRelationshipDescriptions);
            DfAssert.GreaterThan(columnRelationshipDescriptions.Count, 0);
            Assert.All(columnRelationshipDescriptions, AsExpected);
        }

        private static void AsExpected(IReadOnlyList<ForeignKeyDescription> foreignKeyDescriptions)
        {
            Assert.NotNull(foreignKeyDescriptions);
            Assert.All(foreignKeyDescriptions, AsExpected);
        }

        private static void AsExpected(IReadOnlyList<TableDescription> tableDescriptions)
        {
            Assert.NotNull(tableDescriptions);
            DfAssert.GreaterThan(tableDescriptions.Count, 0);
            Assert.All(tableDescriptions, AsExpected);
        }

        private static void AsExpected(IReadOnlyList<ColumnDescription> columnDescriptions)
        {
            Assert.NotNull(columnDescriptions);
            DfAssert.GreaterThan(columnDescriptions.Count, 0);
            Assert.All(columnDescriptions, AsExpected);
            var indexCount = columnDescriptions.Select(_ => _.Order).Count();
            Assert.Equal(columnDescriptions.Count, indexCount);
        }
    }
}