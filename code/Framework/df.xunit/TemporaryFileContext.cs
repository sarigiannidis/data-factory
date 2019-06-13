// --------------------------------------------------------------------------------
// <copyright file="TemporaryFileContext.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Xunit
{
    using Df.Stochastic;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using static Constants;

    internal class TemporaryFileContext
        : IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly ConcurrentDictionary<MemberInfo, TemporaryFileContext> _CreatedTempFilePaths = new ConcurrentDictionary<MemberInfo, TemporaryFileContext>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _Extension;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ConcurrentQueue<string> _Paths = new ConcurrentQueue<string>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _Prefix;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _Disposed;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HardRandom _Random;

        public TemporaryFileContext(string prefix, string extension)
        {
            _Random = new HardRandom();
            _Prefix = prefix;
            _Extension = extension;
        }

        ~TemporaryFileContext() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public string GetFilePath(string extension)
        {
            var path = GenerateFilePath(extension ?? _Extension);
            _Paths.Enqueue(path);
            return path;
        }

        internal static void AddContext(MemberInfo memberInfo, string prefix, string extension)
        {
            var value = new TemporaryFileContext(prefix, extension);
            if (!_CreatedTempFilePaths.TryAdd(memberInfo, value))
            {
                value.Dispose();
                throw new InvalidOperationException("The context has already been added.");
            }
        }

        internal static TemporaryFileContext GetContext(MemberInfo memberInfo) => _CreatedTempFilePaths.TryGetValue(memberInfo, out var value)
                ? value
                : null;

        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                return;
            }

            var exceptions = new List<Exception>();
            if (disposing)
            {
                _Random.Dispose();
                _Random = null;

                while (_Paths.TryDequeue(out var path))
                {
                    try
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }
                    catch (Exception exception)
                    {
                        exceptions.Add(exception);
                    }
                }
            }

            _Disposed = true;
            if (exceptions.Count == 1)
            {
                throw exceptions[0];
            }
            else if (exceptions.Count > 1)
            {
                throw new AggregateException(exceptions);
            }
        }

        private string GenerateFilePath(string extension) => (from str in _Random.NextStrings(FILENAMEREGEX)
             let path = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), $"{_Prefix}-{str}"), extension)
             where !File.Exists(path)
             select path).First();
    }
}