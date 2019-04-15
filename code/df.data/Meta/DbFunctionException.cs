// --------------------------------------------------------------------------------
// <copyright file="DbFunctionException.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta
{
    using System;

    [Serializable]
    internal class DbFunctionException
        : NotImplementedException
    {
        public DbFunctionException(string message)
            : base(message)
        {
        }

        public DbFunctionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DbFunctionException()
        {
        }
    }
}