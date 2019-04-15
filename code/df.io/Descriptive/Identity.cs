// --------------------------------------------------------------------------------
// <copyright file="Identity.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io.Descriptive
{
    using Newtonsoft.Json;
    using System.Diagnostics;

    [JsonObject(IsReference = false)]
    [DebuggerDisplay("IDENTITY({SeedValue}, {IncrementValue})")]
    public sealed class Identity
    {
        [JsonProperty(Order = 2)]
        public object IncrementValue { get; }

        [JsonProperty(Order = 1)]
        public object SeedValue { get; }

        [JsonConstructor]
        internal Identity(object seedValue, object incrementValue)
        {
            SeedValue = Check.NotNull(nameof(seedValue), seedValue);
            IncrementValue = Check.NotNull(nameof(incrementValue), incrementValue);
        }
    }
}