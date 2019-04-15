﻿// --------------------------------------------------------------------------------
// <copyright file="Project.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io
{
    using Df.Io.Descriptive;
    using Df.Io.Prescriptive;
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;

    [JsonObject(IsReference = false)]
    [DebuggerDisplay("{Descriptor} Created = {Created}, Modified = {Modified}")]
    public sealed class Project
    {
        [JsonProperty(Order = 1)]
        public DateTimeOffset Created { get; }

        [JsonProperty(Order = 3)]
        public Descriptor Descriptor { get; }

        [JsonProperty(Order = 2)]
        public DateTimeOffset Modified { get; internal set; }

        [JsonProperty(Order = 4)]
        public Prescriptor Prescriptor { get; }

        [JsonConstructor]
        internal Project(Descriptor descriptor, Prescriptor prescriptor, DateTimeOffset created, DateTimeOffset modified)
        {
            Descriptor = Check.NotNull(nameof(descriptor), descriptor);
            Prescriptor = Check.NotNull(nameof(prescriptor), prescriptor);
            Created = Check.InOpenInterval(nameof(created), created, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            Modified = Check.InOpenInterval(nameof(modified), modified, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            Check.GreaterThanOrEqual(nameof(modified), modified, created);
        }
    }
}