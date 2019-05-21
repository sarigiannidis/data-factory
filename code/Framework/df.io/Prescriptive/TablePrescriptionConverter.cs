// --------------------------------------------------------------------------------
// <copyright file="TablePrescriptionConverter.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Prescriptive
{
    using Df.Io.Descriptive;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;

    internal class TablePrescriptionConverter
        : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType) =>
            typeof(TablePrescription).Equals(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            var tableDescription = (TableDescription)serializer.ReferenceResolver.ResolveReference(serializer, (string)obj["$ref"]);
            var result = new TablePrescription(tableDescription);
            foreach (var item in obj["ColumnPrescriptions"])
            {
                var columnDescriptionName = item.Value<string>("Column");
                var valueFactoryPrescriptionName = item.Value<string>("Prescription");
                var nullPercentage = item.Value<float?>("NULL");

                var columnDescription = tableDescription.ColumnDescriptions.First(c => c.Name == columnDescriptionName);
                var valueFactoryPrescription = (ValueFactoryPrescription)serializer.ReferenceResolver.ResolveReference(serializer, valueFactoryPrescriptionName);
                var columnPrescription = new ColumnPrescription(columnDescription, valueFactoryPrescription, nullPercentage);
                result.AddColumn(columnPrescription);
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var tablePrescription = Check.NotNull(nameof(value), value as TablePrescription);

            writer.WriteStartObject();
            writer.WritePropertyName("$ref");
            writer.WriteValue("[{0}].[{1}]".FormatInvariant(tablePrescription.TableDescription.Schema, tablePrescription.TableDescription.Name));
            writer.WritePropertyName("Rows");
            writer.WriteValue(tablePrescription.Rows);

            writer.WritePropertyName("ColumnPrescriptions");
            writer.WriteStartArray();
            foreach (var item in tablePrescription.ColumnPrescriptions)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Column");
                writer.WriteValue(item.ColumnDescription.Name);
                writer.WritePropertyName("Prescription");
                writer.WriteValue(item.ValueFactoryPrescription.Name);
                if (item.NullPercentage != null)
                {
                    writer.WritePropertyName("NULL");
                    writer.WriteValue(item.NullPercentage.Value);
                }

                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}