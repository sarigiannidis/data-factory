﻿// --------------------------------------------------------------------------------
// <copyright file="RandomSqlGeographyConfiguration.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories.Spatial
{
    using Df.Extensibility;
    using System.Collections.Generic;
    using System.Diagnostics;
    using static Constants;

    [DebuggerDisplay("MinLatitude = {MinLatitude}, MaxLatitude = {MaxLatitude}, MinLongitude = {MinLongitude}, MaxLongitude = {MaxLongitude}, Srid = {Srid}")]
    public sealed class RandomSqlGeographyConfiguration
        : ValueFactoryConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double MaxLatitude => GetValue<double>(PROPERTY_MAXLATITUDE);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double MaxLongitude => GetValue<double>(PROPERTY_MAXLONGITUDE);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double MinLatitude => GetValue<double>(PROPERTY_MINLATITUDE);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double MinLongitude => GetValue<double>(PROPERTY_MINLONGITUDE);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Srid => GetValue<int>(PROPERTY_SRID);

        public RandomSqlGeographyConfiguration(double minLatitude, double maxLatitude, double minLongitude, double maxLongitude, int srid)
        {
            SetValue(PROPERTY_MINLATITUDE, minLatitude);
            SetValue(PROPERTY_MAXLATITUDE, Check.GreaterThanOrEqual(nameof(maxLatitude), maxLatitude, minLatitude));
            SetValue(PROPERTY_MINLONGITUDE, minLongitude);
            SetValue(PROPERTY_MAXLONGITUDE, Check.GreaterThanOrEqual(nameof(maxLongitude), maxLongitude, minLongitude));
            SetValue(PROPERTY_SRID, srid);
        }

        public RandomSqlGeographyConfiguration(IDictionary<string, object> properties)
            : base(properties)
        {
        }

        public RandomSqlGeographyConfiguration()
        {
        }
    }
}