// --------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Production
{
    internal static class Constants
    {
        public const int BULKCOPY_BATCHSIZE = 10000;

        public const string DEFAULT_DATABASENAME = "DF";

        public const string SQL_DELETE = "DELETE FROM {0};";

        public const string SQL_SELECT = "SELECT * FROM {0}";

        public const string SQL_UPDATE_DF2 = "WITH CTE AS (SELECT ROW_NUMBER() OVER(ORDER BY newid() ASC) AS ROWNUMBER, [@DF1] FROM {0}) UPDATE T2 SET T2.[@DF2] = T1.[@DF1] FROM {0} T2, CTE T1 WHERE T1.ROWNUMBER = T2.[@DF1]";
    }
}