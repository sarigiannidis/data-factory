// --------------------------------------------------------------------------------
// <copyright file="ConstantFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    public abstract class ConstantFactory<TValue>
        : ValueFactory<TValue, ConstantConfiguration<TValue>>
    {
        public override ValueFactoryKinds Kind => ValueFactoryKinds.Constant;

        public override TValue CreateValue() => Configuration.Value;
    }
}