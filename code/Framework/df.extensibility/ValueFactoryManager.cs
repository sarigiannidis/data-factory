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

        public IReadOnlyCollection<IValueFactoryInfo> ValueFactoryInfos =>
            _ValueFactoryInfos;

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
                Logger.LogError(exception, "Refresh failed.");
                throw;
            }
        }

        private Func<T> CreateActivator<T>(Type type)
        {
            var func = ReflectionUtility.CreateDefaultInstance<T>(type);
            if (func == null)
            {
                Logger.LogWarning("The type {0} does not have a default constructor.", type.FullName);
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

            if (result == default)
            {
                Logger.LogWarning("Missing argument {0}.", key);
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
                Logger.LogError(exception, "LoadAssembly failed.");
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

            Logger.LogInformation("The type {0} has the attribute {1}.", type.FullName, nameof(ValueFactoryAttribute));
            var args = ReflectionUtility.GetConstructorArguments(attribute);
            if (args == null)
            {
                Logger.LogError("Failed to get the constructor arguments on {0}", nameof(ValueFactoryAttribute));
                throw new ValueFactoryAttributeException("Failed to get the constructor arguments on {0}".FormatInvariant(nameof(ValueFactoryAttribute)));
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
                Logger.LogWarning("{0} should not be null.", nameof(assembly));
                return result;
            }

            try
            {
                Logger.LogInformation("Reading exported types from {0}", assembly.FullName);
                Logger.LogInformation("{0} has {1} exported types.", assembly.FullName, assembly.GetExportedTypes().Length);
                var items = assembly.GetExportedTypes().Select(Selector_LoadValueFactory).Where(_ => _ != null);
                result = new ValueFactoryCollection(items);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "Failed to read types from assembly {0}", assembly.FullName);
                throw;
            }

            return result;
        }
    }
}