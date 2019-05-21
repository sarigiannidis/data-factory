// --------------------------------------------------------------------------------
// <copyright file="JsonUtility.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io
{
    using Df.Extensibility;
    using Df.Io.Prescriptive;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Collections.Generic;
    using System.IO;

    internal sealed class JsonUtility
    {
        private IValueFactoryManager ValueFactoryManager { get; }

        public JsonUtility(IValueFactoryManager valueFactoryManager)
        {
            ValueFactoryManager = Check.NotNull(nameof(valueFactoryManager), valueFactoryManager);
            ValueFactoryManager.Refresh();
        }

        public T Read<T>(string path)
        {
            var serializer = CreateJsonSerializer();

            using var file = File.OpenText(path);
            using var reader = new JsonTextReader(file);
            return serializer.Deserialize<T>(reader);
        }

        public string Serialize<T>(T t)
        {
            _ = Check.NotNull(nameof(t), t);
            var serializer = CreateJsonSerializer();
            using var stringWriter = new StringWriter();
            using var writer = new JsonTextWriter(stringWriter);
            serializer.Serialize(writer, t);
            return stringWriter.ToString();
        }

        public void Write<T>(T t, string path)
        {
            _ = Check.NotNull(nameof(t), t);
            var serializer = CreateJsonSerializer();
            using var file = File.CreateText(path);
            serializer.Serialize(file, t);
        }

        private JsonSerializerSettings CreateDefaultJsonSerializerSettings() =>
            new JsonSerializerSettings
            {
                DateParseHandling = DateParseHandling.DateTimeOffset,
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.None,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ObjectCreationHandling = ObjectCreationHandling.Reuse,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Converters = new List<JsonConverter> { new StringEnumConverter(), new ValueFactoryPrescriptionConverter(ValueFactoryManager) },
                ReferenceResolverProvider = () => new DfReferenceResolver(),
            };

        private JsonSerializer CreateJsonSerializer() =>
            JsonSerializer.CreateDefault(CreateDefaultJsonSerializerSettings());
    }
}