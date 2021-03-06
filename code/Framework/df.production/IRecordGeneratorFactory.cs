﻿// --------------------------------------------------------------------------------
// <copyright file="IRecordGeneratorFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Production
{
    using Df.Io.Prescriptive;
    using System.Data;

    internal interface IRecordGeneratorFactory
    {
        IDataReader Create(TablePrescription tablePrescription, int rows);
    }
}