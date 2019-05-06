// --------------------------------------------------------------------------------
// <copyright file="ValueFactoryAttributeException.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Extensibility
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ValueFactoryAttributeException
        : Exception
    {
        public ValueFactoryAttributeException()
        {
        }

        public ValueFactoryAttributeException(string message)
            : base(message)
        {
        }

        public ValueFactoryAttributeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ValueFactoryAttributeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}