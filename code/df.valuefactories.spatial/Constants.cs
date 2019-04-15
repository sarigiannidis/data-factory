// --------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories.Spatial
{
    internal static class Constants
    {
        public const string PROPERTY_MAXLATITUDE = "maxLatitude";

        public const string PROPERTY_MAXLONGITUDE = "maxLongitude";

        public const string PROPERTY_MAXX = "maxX";

        public const string PROPERTY_MAXY = "maxY";

        public const string PROPERTY_MINLATITUDE = "minLatitude";

        public const string PROPERTY_MINLONGITUDE = "minLongitude";

        public const string PROPERTY_MINX = "minX";

        public const string PROPERTY_MINY = "minY";

        public const string PROPERTY_SRID = "srid";

        public const double SQLGEOGRAPHY_MAXLATITUDE = 10d;

        public const double SQLGEOGRAPHY_MAXLONGITUDE = 10d;

        public const double SQLGEOGRAPHY_MINLATITUDE = -10d;

        public const double SQLGEOGRAPHY_MINLONGITUDE = -10d;

        /// <summary>
        /// GEOGCS["Amersfoort", DATUM["Amersfoort", ELLIPSOID["Bessel 1841", 6377397.155, 299.1528128]], PRIMEM["Greenwich", 0], UNIT["Degree", 0.0174532925199433]].
        /// </summary>
        public const int SQLGEOGRAPHY_SRID = 4289;

        public const double SQLGEOMETRY_MAXX = 10d;

        public const double SQLGEOMETRY_MAXY = 10d;

        public const double SQLGEOMETRY_MINX = -10d;

        public const double SQLGEOMETRY_MINY = -10d;

        public const int SQLGEOMETRY_SRID = 0;
    }
}