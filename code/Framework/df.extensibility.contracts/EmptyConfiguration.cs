// --------------------------------------------------------------------------------
// <copyright file="EmptyConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("<Empty>")]
    public sealed class EmptyConfiguration
        : ValueFactoryConfiguration
    {
        public EmptyConfiguration()
        {
        }

        public EmptyConfiguration(IDictionary<string, object> properties)
            : base(properties)
        {
        }
    }
}