// --------------------------------------------------------------------------------
// <copyright file="JsonUtil.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Collections.Generic;
    using System.IO;

    internal static class JsonUtil
    {
        public static T Read<T>(string path)
        {
            var serializer = CreateJsonSerializer();

            using (var file = File.OpenText(path))
            using (var reader = new JsonTextReader(file))
            {
                return serializer.Deserialize<T>(reader);
            }
        }

        public static string Serialize<T>(T t)
        {
            Check.NotNull(nameof(t), t);
            var serializer = CreateJsonSerializer();
            using (var stringWriter = new StringWriter())
            {
                using (var writer = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(writer, t);
                }

                return stringWriter.ToString();
            }
        }

        public static void Write<T>(T t, string path)
        {
            Check.NotNull(nameof(t), t);
            var serializer = CreateJsonSerializer();

            using (var file = File.CreateText(path))
            {
                serializer.Serialize(file, t);
            }
        }

        private static JsonSerializerSettings CreateDefaultJsonSerializerSettings() =>
            new JsonSerializerSettings
            {
                DateParseHandling = DateParseHandling.DateTimeOffset,
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.None,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ObjectCreationHandling = ObjectCreationHandling.Reuse,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Converters = new List<JsonConverter> { new StringEnumConverter() },
                ReferenceResolverProvider = () => new DfReferenceResolver(),
            };

        private static JsonSerializer CreateJsonSerializer() =>
            JsonSerializer.CreateDefault(CreateDefaultJsonSerializerSettings());
    }
}