// --------------------------------------------------------------------------------
// <copyright file="RandomSqlGeographyFactory.cs" company="Michalis Sarigiannidis">
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

    [ValueFactory("sqlgeography", "Generates random SqlGeography values", typeof(SqlGeography), typeof(RandomSqlGeographyFactory))]
    public sealed class RandomSqlGeographyFactory
        : RandomFactory<SqlGeography, RandomSqlGeographyConfiguration>,
        IConfigurator
    {
        public IValueFactoryConfiguration CreateConfiguration() => new RandomSqlGeographyConfiguration(SQLGEOGRAPHY_MINLATITUDE, SQLGEOGRAPHY_MAXLATITUDE, SQLGEOGRAPHY_MINLONGITUDE, SQLGEOGRAPHY_MAXLONGITUDE, SQLGEOGRAPHY_SRID);

        public override SqlGeography CreateValue()
        {
            var x = (Random.NextPercentage() * (Configuration.MaxLatitude - Configuration.MinLatitude)) + Configuration.MinLatitude;
            var y = (Random.NextPercentage() * (Configuration.MaxLongitude - Configuration.MinLongitude)) + Configuration.MinLongitude;
            return SqlGeography.Point(x, y, Configuration.Srid);
        }
    }
}