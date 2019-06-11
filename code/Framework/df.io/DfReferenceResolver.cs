// --------------------------------------------------------------------------------
// <copyright file="DfReferenceResolver.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io
{
    using Df.Io.Descriptive;
    using Df.Io.Prescriptive;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal sealed class DfReferenceResolver
        : IReferenceResolver
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IDictionary<string, object> _Referenced = new Dictionary<string, object>();

        public void AddReference(object context, string reference, object value) => _Referenced[reference] = value;

        public string GetReference(object context, object value)
        {
            var reference = ObjectReference(value);
            _Referenced[reference] = value;
            return reference;
        }

        public bool IsReferenced(object context, object value) => _Referenced.ContainsKey(ObjectReference(value));

        public object ResolveReference(object context, string reference) => _Referenced.TryGetValue(reference, out var value) ? value : null;

        private static string ObjectReference(object value) => value switch
        {
            ColumnDescription columnDescription => "{0}.[{1}]".FormatInvariant(ObjectReference(columnDescription.Parent), columnDescription.Name),
            TableDescription tableDescription => "[{0}].[{1}]".FormatInvariant(tableDescription.Schema, tableDescription.Name),
            ValueFactoryPrescription valueFactoryPrescription => valueFactoryPrescription.Name,
            _ => "{0}".FormatInvariant(value),
        };
    }
}