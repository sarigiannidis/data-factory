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
            Get<double>(PROPERTY_MAXX);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double MaxY =>
            Get<double>(PROPERTY_MAXY);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double MinX =>
            Get<double>(PROPERTY_MINX);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double MinY =>
            Get<double>(PROPERTY_MINY);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Srid =>
            Get<int>(PROPERTY_SRID);

        public RandomSqlGeometryConfiguration(double minX, double maxX, double minY, double maxY, int srid)
        {
            Set(PROPERTY_MINX, minX);
            Set(PROPERTY_MAXX, Check.GreaterThanOrEqual(nameof(maxX), maxX, minX));
            Set(PROPERTY_MINY, minY);
            Set(PROPERTY_MAXY, Check.GreaterThanOrEqual(nameof(maxY), maxY, minY));
            Set(PROPERTY_SRID, srid);
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