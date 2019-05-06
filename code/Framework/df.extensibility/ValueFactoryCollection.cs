// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryCollection.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System.Collections.Generic;

    internal sealed class ValueFactoryCollection
        : List<IValueFactoryInfo>
    {
        public ValueFactoryCollection()
        {
        }

        public ValueFactoryCollection(IEnumerable<IValueFactoryInfo> collection)
            : base(collection)
        {
        }
    }
}