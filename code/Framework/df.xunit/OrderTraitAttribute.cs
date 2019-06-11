// --------------------------------------------------------------------------------
// <copyright file="OrderTraitAttribute.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Xunit
{
    using System;
    using Xunit.Sdk;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [TraitDiscoverer("Xunit.OrderTraitDiscoverer", "df.xunit")]
    public sealed class OrderTraitAttribute
        : Attribute, ITraitAttribute
    {
        public int Order { get; }

        public OrderTraitAttribute(int order) => Order = order;
    }
}