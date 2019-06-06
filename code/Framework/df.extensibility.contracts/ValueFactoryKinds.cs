// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryKinds.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;

    [Flags]
    public enum ValueFactoryKinds
    {
        None = 0b_00000,
        Constant = 0b_00001,
        Incremental = 0b_00010,
        List = 0b_00100,
        Random = 0b_01000,
        Pattern = 0b_10000,
    }
}