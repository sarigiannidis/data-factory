// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryPrescriptionConverter.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------
namespace Df.Io.Prescriptive
{
    using Df.Extensibility;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;

    internal sealed class ValueFactoryPrescriptionConverter
        : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;

        private IValueFactoryManager ValueFactoryManager { get; }

        public ValueFactoryPrescriptionConverter(IValueFactoryManager valueFactoryManager) =>
            ValueFactoryManager = Check.NotNull(nameof(valueFactoryManager), valueFactoryManager);

        public override bool CanConvert(Type objectType) =>
            typeof(ValueFactoryPrescription).Equals(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            var id = obj.Value<string>("$id");
            if (string.IsNullOrEmpty(id))
            {
                return serializer.ReferenceResolver.ResolveReference(serializer, (string)obj["$ref"]);
            }
            else
            {
                var reference = obj.Value<string>("Reference");
                var valueFactoryInfo = ValueFactoryManager.ValueFactoryInfos.First(f => f.Name == reference);
                var configuration = valueFactoryInfo.Configurator.CreateConfiguration();
                serializer.Populate(obj["Configuration"].CreateReader(), configuration);
                var result = new ValueFactoryPrescription(id, reference, configuration);
                serializer.ReferenceResolver.AddReference(serializer, id, result);
                return result;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotSupportedException();
    }
}