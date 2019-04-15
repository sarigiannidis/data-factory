// --------------------------------------------------------------------------------
// <copyright file="JsonSerializerExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Io
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal static class JsonSerializerExtensions
    {
        public static T Deserialize<T>(this JsonSerializer serializer, JToken jToken)
        {
            using (var reader = jToken.CreateReader())
            {
                return serializer.Deserialize<T>(reader);
            }
        }
    }
}