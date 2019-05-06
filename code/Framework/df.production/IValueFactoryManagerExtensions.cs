// --------------------------------------------------------------------------------
// <copyright file="IValueFactoryManagerExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System.Linq;

    public static class IValueFactoryManagerExtensions
    {
        public static IValueFactoryInfo Resolve(this IValueFactoryManager valueFactoryManager, string valueFactoryReference) =>
            Check.NotNull(nameof(valueFactoryManager), valueFactoryManager).ValueFactoryInfos.FirstOrDefault(_ => _.Name == valueFactoryReference);
    }
}