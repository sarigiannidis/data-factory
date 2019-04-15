// --------------------------------------------------------------------------------
// <copyright file="OrderTraitDiscoverer.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Xunit
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Xunit.Abstractions;
    using Xunit.Sdk;
    using static Constants;

    internal sealed class OrderTraitDiscoverer
            : ITraitDiscoverer
    {
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var orderValue = (int)traitAttribute.GetConstructorArguments().First();
            yield return new KeyValuePair<string, string>(KEY_ORDER, orderValue.ToString(CultureInfo.InvariantCulture));
        }
    }
}