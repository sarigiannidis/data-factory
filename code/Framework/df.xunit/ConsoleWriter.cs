// --------------------------------------------------------------------------------
// <copyright file="ConsoleWriter.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Xunit
{
    using Df;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Xunit.Abstractions;

    public sealed class ConsoleWriter
        : TextWriter
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ITestOutputHelper _Output;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly StringBuilder _StringBuilder = new StringBuilder();

        public override Encoding Encoding =>
            Encoding.Unicode;

        public ConsoleWriter(ITestOutputHelper output) =>
            _Output = Check.NotNull(nameof(output), output);

        public override void Flush()
        {
            _Output.WriteLine(_StringBuilder.ToString());
            _ = _StringBuilder.Clear();
        }

        public override void Write(char value) =>
                    _StringBuilder.Append(value);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Flush();
            }

            base.Dispose(disposing);
        }
    }
}