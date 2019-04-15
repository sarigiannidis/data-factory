// --------------------------------------------------------------------------------
// <copyright file="AddHandler.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.OptionHandlers
{
    using Df.Collections;
    using Df.Extensibility;
    using Df.Io;
    using Df.Io.Descriptive;
    using Df.Io.Prescriptive;
    using Df.Options;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class AddHandler
        : IHandler<AddOptions>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IProjectManager _ProjectManager;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IValueFactoryManager _ValueFactoryManager;

        public AddHandler(IProjectManager projectManager, IValueFactoryManager valueFactoryManager)
        {
            _ProjectManager = Check.NotNull(nameof(projectManager), projectManager);
            _ValueFactoryManager = Check.NotNull(nameof(valueFactoryManager), valueFactoryManager);
        }

        public void AddFactory(AddOptions options)
        {
            var project = _ProjectManager.LoadFromFile(options.Project);
            var valueFactoryInfo = _ValueFactoryManager.ValueFactoryInfos.FirstOrDefault(_ => _.Name == options.Name);
            var valueFactoryPrescription = project.CreateValueFactoryPrescription(valueFactoryInfo, _ => _.Configurator.CreateConfiguration());
            project.Prescriptor.AddValueFactory(valueFactoryPrescription);
            _ProjectManager.SaveToFile(project, options.Project);
        }

        public void Handle(AddOptions options)
        {
            _ValueFactoryManager.Initialize();
            switch (options.Subject)
            {
                case AddSubject.AllFactories: AddAllFactories(options); return;
                case AddSubject.Factory: AddFactory(options); return;
                case AddSubject.AllTables: AddAllTables(options); return;
                case AddSubject.Table: AddTable(options); return;
            }
        }

        private void AddAllFactories(AddOptions options)
        {
            var project = _ProjectManager.LoadFromFile(options.Project);
            foreach (var valueFactoryInfo in _ValueFactoryManager.ValueFactoryInfos)
            {
                var valueFactoryPrescription = project.CreateValueFactoryPrescription(valueFactoryInfo, _ => _.Configurator.CreateConfiguration());
                project.Prescriptor.AddValueFactory(valueFactoryPrescription);
            }

            _ProjectManager.SaveToFile(project, options.Project);
        }

        private void AddAllTables(AddOptions options)
        {
            var project = _ProjectManager.LoadFromFile(options.Project);
            var orderer = new Orderer<TableDescription>(tableDescription => tableDescription.ForeignKeyDescriptions.Select(foreignKeyDescription => foreignKeyDescription.Referenced));
            var ordered = orderer.Order(project.Descriptor.TableDescriptions);

            foreach (var tableDescription in ordered.Select(_ => _.Node))
            {
                var tablePrescription = CreateDefaultTablePrescription(project, tableDescription);
                if (tablePrescription.ColumnPrescriptions.Count > 0)
                {
                    project.Prescriptor.AddTable(tablePrescription);
                }
            }

            _ProjectManager.SaveToFile(project, options.Project);
        }

        private void AddTable(AddOptions options)
        {
            var project = _ProjectManager.LoadFromFile(options.Project);
            var tableDescription = project.Descriptor.TableDescriptions.SingleOrDefault(_ => _.Name == options.Name);
            var tablePrescription = CreateDefaultTablePrescription(project, tableDescription);
            if (tablePrescription.ColumnPrescriptions.Count > 0)
            {
                project.Prescriptor.AddTable(tablePrescription);
            }

            _ProjectManager.SaveToFile(project, options.Project);
        }

        private TablePrescription CreateDefaultTablePrescription(Project project, TableDescription tableDescription)
        {
            var tablePrescription = new TablePrescription(tableDescription);
            foreach (var columnDescription in tableDescription.ColumnDescriptions.Where(_ => _.IsWritable()))
            {
                var valueFactory = GetValueFactoryPrescription(project, columnDescription);
                var columnPrescription = new ColumnPrescription(columnDescription, valueFactory, columnDescription.Nullable ? 0.25f : 0f);
                tablePrescription.AddColumn(columnPrescription);
            }

            return tablePrescription;
        }

        private ValueFactoryPrescription CreateNewValueFactoryPrescription(Project project, ColumnDescription columnDescription)
        {
            var valueFactoryInfos = _ValueFactoryManager.ValueFactoryInfos.FilterByType(columnDescription.ResolveUserType()).ToArray();
            var valueFactoryInfo = valueFactoryInfos.Length switch
            {
                0 => throw null,
                1 => valueFactoryInfos[0],
                _ when columnDescription.Identity != null => valueFactoryInfos.First(_ => !_.ValueFactory.IsRandom),
                _ => valueFactoryInfos.FirstOrDefault(_ => _.ValueFactory.IsRandom) ?? valueFactoryInfos[0]
            };

            return project.CreateValueFactoryPrescription(valueFactoryInfo, _ => _.ConfigureForColumn(columnDescription));
        }

        private ValueFactoryPrescription GetValueFactoryPrescription(Project project, ColumnDescription columnDescription)
        {
            var tentative = CreateNewValueFactoryPrescription(project, columnDescription);
            var valueFactoryPrescription = project.Prescriptor.ValueFactoryPrescriptions.FirstOrDefault(_ => _ == tentative);
            if (valueFactoryPrescription is null)
            {
                project.Prescriptor.AddValueFactory(tentative);
                return tentative;
            }
            else
            {
                return valueFactoryPrescription;
            }
        }
    }
}