// --------------------------------------------------------------------------------
// <copyright file="PrescriptorTest.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Tests
{
    using Df.Data;
    using Df.Extensibility;
    using Df.Io.Prescriptive;
    using System.IO;
    using System.Linq;
    using Xunit;
    using Xunit.Abstractions;

    public sealed class PrescriptorTest
        : IoTestBase
    {
        public PrescriptorTest(ITestOutputHelper output, IoFixture fixture)
            : base(output, fixture)
        {
        }

        [Fact]
        public void AddAllValueFactories()
        {
            var fileName = CreateFileName("addallvaluefactories_");
            var project = CreateProject();
            ValueFactoryManager.Initialize();

            foreach (var factory in ValueFactoryManager.ValueFactoryInfos)
            {
                var configuration = factory.Configurator.CreateConfiguration();
                var valueFactoryPrescription = new ValueFactoryPrescription("default-" + factory.Name, factory.Name, configuration);
                project.Prescriptor.AddValueFactory(valueFactoryPrescription);
            }

            ProjectManager.SaveToFile(project, fileName);

            Output.WriteLine(File.ReadAllText(fileName));
            var project2 = ProjectManager.LoadFromFile(fileName);
            IoAssert.AsExpected(project2);
        }

        [Fact]
        public void AddColumnPrescriptions()
        {
            var fileName = CreateFileName("addColumnprescriptions_");
            var project = CreateProject();
            ValueFactoryManager.Initialize();

            var i = 0;
            foreach (var tableDescription in project.Descriptor.TableDescriptions)
            {
                var tablePrescription = new TablePrescription(tableDescription);

                foreach (var columnDescription in tableDescription.ColumnDescriptions)
                {
                    if (columnDescription.Identity != null || columnDescription.Computed)
                    {
                        continue;
                    }

                    var type = columnDescription.UserType switch
                    {
                        "sql_variant" => typeof(int),
                        _ => SqlTypeUtil.GetDataType(columnDescription.UserType, columnDescription.MaxLength),
                    };

                    var factory = ValueFactoryManager.ValueFactoryInfos.FilterByType(type).FirstOrDefault();
                    Assert.NotNull(factory);
                    var configuration = factory.Configurator.CreateConfiguration();
                    var valueFactoryPrescription = new ValueFactoryPrescription(factory.Name + i++, factory.Name, configuration);
                    project.Prescriptor.AddValueFactory(valueFactoryPrescription);

                    var columnPrescription = new ColumnPrescription(columnDescription, valueFactoryPrescription, null);
                    tablePrescription.AddColumn(columnPrescription);
                }

                project.Prescriptor.AddTable(tablePrescription);
            }

            ProjectManager.SaveToFile(project, fileName);

            Output.WriteLine(File.ReadAllText(fileName));
            var project2 = ProjectManager.LoadFromFile(fileName);
            IoAssert.AsExpected(project2);
        }
    }
}