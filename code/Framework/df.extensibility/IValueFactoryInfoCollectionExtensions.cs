// --------------------------------------------------------------------------------
// <copyright file="IValueFactoryInfoCollectionExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IValueFactoryInfoCollectionExtensions
    {
        public static IReadOnlyCollection<IValueFactoryInfo> FilterByType(this IReadOnlyCollection<IValueFactoryInfo> valueFactoryInfos, Type type) => valueFactoryInfos?.Where(_ => _.ValueType == type).ToList();
    }
}