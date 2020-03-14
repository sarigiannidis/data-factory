// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryManager.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using Df.IO;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using static Constants;

    internal sealed class ValueFactoryManager
        : IValueFactoryManager
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly object _SyncObject = new object();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Initialized;

        private ValueFactoryCollection _ValueFactoryInfos = new ValueFactoryCollection();

        public ILogger<ValueFactoryManager> Logger { get; }

        public ValueFactoryManagerOptions Options { get; }

        public IReadOnlyCollection<IValueFactoryInfo> ValueFactoryInfos => _ValueFactoryInfos;

        public ValueFactoryManager(IOptions<ValueFactoryManagerOptions> options, ILogger<ValueFactoryManager> logger)
        {
            Options = Check.NotNull(nameof(options), options);
            Logger = Check.NotNull(nameof(logger), logger);
        }

        public void Initialize()
        {
            if (!_Initialized)
            {
                lock (_SyncObject)
                {
                    if (!_Initialized)
                    {
                        Refresh();
                        _Initialized = true;
                    }
                }
            }
        }

        public void Refresh()
        {
            try
            {
                var root = PathUtility.GetFullPath(Options.Path);
                var items = Directory.EnumerateFiles(root, SEARCHPATTERN_ASSEMBLIES)
                    .Select(Selector_LoadAssembly)
                    .SelectMany(Selector_LoadValueFactoryInfos);
                _ValueFactoryInfos = new ValueFactoryCollection(items);
                Thread.MemoryBarrier();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, Messages.LOG_ERR_FAILED, nameof(Refresh));
                throw;
            }
        }

        private Func<T> CreateActivator<T>(Type type)
        {
            var func = ReflectionUtility.CreateDefaultInstance<T>(type);
            if (func == null)
            {
                Logger.LogWarning(Messages.LOG_WRN_NO_DEFAULT_CONSTRUCTOR, type.FullName);
            }

            return func;
        }

        private bool Predicate_IsValueFactoryAttribute(CustomAttributeData customAttributeData)
        {
            var type = typeof(ValueFactoryAttribute);
            if (customAttributeData.AttributeType == type)
            {
                return true;
            }

            if (customAttributeData.AttributeType.FullName == type.FullName)
            {
                var sb = new StringBuilder("A type ambiguity has occured.")
                    .AppendFormatInvariant("The expected type {0} is defined in the assembly at {1}.", type.AssemblyQualifiedName, type.AssemblyQualifiedName)
                    .AppendFormatInvariant("The actual type {0} is defined in the assembly at {1}.", customAttributeData.AttributeType.AssemblyQualifiedName, customAttributeData.AttributeType.Assembly.Location)
                    .ToString();
                Logger.LogWarning(sb);
            }

            return false;
        }

        private T ReadArgument<T>(IDictionary<string, object> dictionary, string key)
        {
            T result = default;
            if (dictionary != null && dictionary.TryGetValue(key, out var value))
            {
                result = (T)(value ?? default(T));
            }

            if (EqualityComparer<T>.Default.Equals(result, default))
            {
                Logger.LogWarning(Messages.LOG_WRN_MISSING_ARG, key);
            }

            return result;
        }

        private Assembly Selector_LoadAssembly(string path)
        {
            try
            {
                return Assembly.LoadFrom(path);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, Messages.LOG_ERR_FAILED, nameof(Selector_LoadAssembly));
                throw;
            }
        }

        private IValueFactoryInfo Selector_LoadValueFactory(Type type)
        {
            using var scope = Logger.BeginScope("{0}({1})", nameof(Selector_LoadValueFactory), type.FullName);
            var attribute = type.CustomAttributes.FirstOrDefault(Predicate_IsValueFactoryAttribute);
            if (attribute == null)
            {
                return null;
            }

            Logger.LogInformation(Messages.LOG_INF_FOUND_ATTRIBUTE, type.FullName, nameof(ValueFactoryAttribute));
            var args = ReflectionUtility.GetConstructorArguments(attribute);
            if (args == null)
            {
                Logger.LogError(Messages.LOG_ERR_FAILED_TO_GET_ARGS, nameof(ValueFactoryAttribute));
                throw new ValueFactoryAttributeException(Messages.EX_NO_ARGS.FormatInvariant(nameof(ValueFactoryAttribute)));
            }

            var name = ReadArgument<string>(args, ARG_NAME);
            var description = ReadArgument<string>(args, ARG_DESCRIPTION);
            var valueType = ReadArgument<Type>(args, ARG_VALUETYPE);
            var defaultConfigurationFactoryType = ReadArgument<Type>(args, ARG_DEFAULTCONFIGURATIONFACTORY);

            var invalid = false;
            invalid |= string.IsNullOrWhiteSpace(name);
            invalid |= string.IsNullOrWhiteSpace(description);
            invalid |= defaultConfigurationFactoryType == null;
            if (invalid)
            {
                return null;
            }

            var valueFactoryActivator = CreateActivator<IValueFactory>(type);
            invalid |= valueFactoryActivator == null;
            var configuratorActivator = CreateActivator<IConfigurator>(defaultConfigurationFactoryType);
            invalid |= configuratorActivator == null;

            return invalid
                ? null
                : new ValueFactoryInfo(
                name,
                description,
                Path.GetFileName(type.Assembly.Location),
                valueType,
                valueFactoryActivator,
                configuratorActivator);
        }

        private IEnumerable<IValueFactoryInfo> Selector_LoadValueFactoryInfos(Assembly assembly)
        {
            IEnumerable<IValueFactoryInfo> result = Array.Empty<IValueFactoryInfo>();
            if (assembly == null)
            {
                Logger.LogWarning(Messages.LOG_WRN_NULL_ENCOUNTERED, nameof(assembly));
                return result;
            }

            try
            {
                Logger.LogInformation(Messages.LOG_INF_READING_ASSEMBLY, assembly.FullName);
                Logger.LogInformation(Messages.LOG_INF_TYPES_EXPORTED, assembly.FullName, assembly.GetExportedTypes().Length);
                var items = assembly.GetExportedTypes().Select(Selector_LoadValueFactory).Where(_ => _ != null);
                result = new ValueFactoryCollection(items);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, Messages.LOG_ERR_FAILED_TO_READ_TYPES, assembly.FullName);
                throw;
            }

            return result;
        }
    }
}