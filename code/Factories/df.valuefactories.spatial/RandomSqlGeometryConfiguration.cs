// --------------------------------------------------------------------------------
// <copyright file="RandomSqlGeometryConfiguration.cs" company="Michalis Sarigiannidis">
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

    [DebuggerDisplay("MinX = {MinX}, MaxX = {MaxX}, MinY = {MinY}, MaxY = {MaxY}, Srid = {Srid}")]
    public sealed class RandomSqlGeometryConfiguration
        : ValueFactoryConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double MaxX =>
            GetValue<double>(PROPERTY_MAXX);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double MaxY =>
            GetValue<double>(PROPERTY_MAXY);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double MinX =>
            GetValue<double>(PROPERTY_MINX);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double MinY =>
            GetValue<double>(PROPERTY_MINY);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Srid =>
            GetValue<int>(PROPERTY_SRID);

        public RandomSqlGeometryConfiguration(double minX, double maxX, double minY, double maxY, int srid)
        {
            SetValue(PROPERTY_MINX, minX);
            SetValue(PROPERTY_MAXX, Check.GreaterThanOrEqual(nameof(maxX), maxX, minX));
            SetValue(PROPERTY_MINY, minY);
            SetValue(PROPERTY_MAXY, Check.GreaterThanOrEqual(nameof(maxY), maxY, minY));
            SetValue(PROPERTY_SRID, srid);
        }

        public RandomSqlGeometryConfiguration(IDictionary<string, object> properties)
            : base(properties)
        {
        }

        public RandomSqlGeometryConfiguration()
        {
        }
    }
}