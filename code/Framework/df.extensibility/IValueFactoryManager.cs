// --------------------------------------------------------------------------------
// <copyright file="IValueFactoryManager.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System.Collections.Generic;

    public interface IValueFactoryManager
    {
        ValueFactoryManagerOptions Options { get; }

        IReadOnlyCollection<IValueFactoryInfo> ValueFactoryInfos { get; }

        /// <summary>
        /// Call once to make sure the collection is loaded.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Call to refresh the collection.
        /// </summary>
        void Refresh();
    }
}