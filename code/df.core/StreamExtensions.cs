// --------------------------------------------------------------------------------
// <copyright file="StreamExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace System.IO
{
    using System.Collections.Generic;
    using System.Text;

    internal static class StreamExtensions
    {
        private static readonly Encoding DEFAULTENCODING = Encoding.Unicode;

        public static void WriteLine(this Stream stream, string line) =>
            WriteLine(stream, line, DEFAULTENCODING);

        public static void WriteLine(this Stream stream, string line, Encoding encoding)
        {
            using (var writer = new StreamWriter(stream, encoding, 4096, true))
            {
                writer.WriteLine(line);
            }
        }

        public static void WriteLines(this Stream stream, IEnumerable<string> lines) =>
            WriteLines(stream, lines, DEFAULTENCODING);

        public static void WriteLines(this Stream stream, IEnumerable<string> lines, Encoding encoding)
        {
            using (var writer = new StreamWriter(stream, encoding, 4096, true))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}