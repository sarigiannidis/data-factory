// --------------------------------------------------------------------------------
// <copyright file="ListHandler.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.OptionHandlers
{
    using Df.Data;
    using Df.Extensibility;
    using Df.Options;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using static Constants;

    internal sealed class ListHandler
        : IHandler<ListOptions>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ISqlFactory _SqlFactory;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IValueFactoryManager _ValueFactoryManager;

        public ListHandler(IValueFactoryManager valueFactoryManager, ISqlFactory sqlFactory)
        {
            _ValueFactoryManager = Check.NotNull(nameof(valueFactoryManager), valueFactoryManager);
            _SqlFactory = Check.NotNull(nameof(sqlFactory), sqlFactory);
        }

        public void Handle(ListOptions options)
        {
            switch (options.Subject)
            {
                case ListSubject.Factories: ListFactories(); return;
                case ListSubject.Databases: ListDatabases(options.ConnectionString); return;
                case ListSubject.Tables: ListTables(options.ConnectionString); return;
            }
        }

        private static string ToConsoleTable(IReadOnlyCollection<IValueFactoryInfo> valueFactoryInfos) => ConsoleUtility.ToConsoleTable(
                        valueFactoryInfos,
                        (_ => _.Name, "Name", 24),
                        (_ => _.Description, "Description", 64),
                        (_ => _.ValueType.Name, "Value", 16),
                        (_ => _.ValueFactory.GetType().Name, "Type", 32),
                        (_ => Path.GetFileName(_.Path), "Assembly", 32));

        private void ListDatabases(string connectionString)
        {
            using var sql = _SqlFactory.Open(connectionString);
            using var databases = sql.Query(SQL_LIST_DATABASES, _ => (string)_[0]);
            Console.WriteLine(ConsoleUtility.ToConsoleTable(databases, (_ => _, "Database", 64)));
        }

        private void ListFactories()
        {
            _ValueFactoryManager.Initialize();
            Console.WriteLine(ToConsoleTable(_ValueFactoryManager.ValueFactoryInfos));
        }

        private void ListTables(string connectionString)
        {
            try
            {
                using var sql = _SqlFactory.Open(connectionString);
                using var tables = sql.Query(SQL_LIST_TABLES, _ => (string)_[0]);
                Console.WriteLine(ConsoleUtility.ToConsoleTable(tables, (_ => _, "Table", 64)));
            }
            catch
            {
                Console.WriteLine("Failed to connect.");
                throw;
            }
        }
    }
}