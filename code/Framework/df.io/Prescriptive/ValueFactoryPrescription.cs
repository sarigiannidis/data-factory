﻿// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryPrescription.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------
namespace Df.Io.Prescriptive
{
    using Df.Extensibility;
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{Name,nq}")]
    public sealed class ValueFactoryPrescription
        : IEquatable<ValueFactoryPrescription>
    {
        [JsonProperty(Order = 2, IsReference = false)]
        public IValueFactoryConfiguration Configuration { get; }

        [JsonIgnore]
        public string Name { get; set; }

        [JsonProperty(Order = 1, IsReference = false)]
        public string Factory { get; }

        public ValueFactoryPrescription(string name, string factory, IValueFactoryConfiguration configuration)
        {
            Name = Check.NotNull(nameof(name), name);
            Factory = Check.NotNull(nameof(factory), factory);
            Configuration = Check.NotNull(nameof(configuration), configuration);
        }

        public override bool Equals(object obj) =>
            obj is ValueFactoryPrescription o && Equals(o);

        public bool Equals(ValueFactoryPrescription other) =>
            !(other is null)
                && (ReferenceEquals(this, other)
                    || (Name == other.Name
                    && Factory == other.Factory
                    && Configuration == other.Configuration));

        public override int GetHashCode() =>
            HashCode.Combine(Name, Factory, Configuration);
    }
}