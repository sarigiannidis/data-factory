// --------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Production
{
    using System.Text;

    internal static class Constants
    {
        public const int BULKCOPY_BATCHSIZE = 10000;
        public const string DEFAULT_DATABASENAME = "DF";
        public const string SQL_DELETE = "DELETE FROM {0};";
        public const string SQL_SELECT = "SELECT * FROM {0}";
        public static readonly Encoding DEFAULT_ENCODING = Encoding.Unicode;
    }
}