// --------------------------------------------------------------------------------
// <copyright file="RandomSqlGeometryFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories.Spatial
{
    using Df.Extensibility;
    using Df.Stochastic;
    using Microsoft.SqlServer.Types;
    using static Constants;

    [ValueFactory("sqlgeometry", "Generates random SqlGeometry values", typeof(SqlGeometry), typeof(RandomSqlGeometryFactory))]
    public sealed class RandomSqlGeometryFactory
        : RandomFactory<SqlGeometry, RandomSqlGeometryConfiguration>,
        IConfigurator
    {
        public IValueFactoryConfiguration CreateConfiguration() => new RandomSqlGeometryConfiguration(SQLGEOMETRY_MINX, SQLGEOMETRY_MAXX, SQLGEOMETRY_MINY, SQLGEOMETRY_MAXY, SQLGEOMETRY_SRID);

        public override SqlGeometry CreateValue()
        {
            var x = (Random.NextPercentage() * (Configuration.MaxX - Configuration.MinX)) + Configuration.MinX;
            var y = (Random.NextPercentage() * (Configuration.MaxY - Configuration.MinY)) + Configuration.MinY;
            return SqlGeometry.Point(x, y, Configuration.Srid);
        }
    }
}