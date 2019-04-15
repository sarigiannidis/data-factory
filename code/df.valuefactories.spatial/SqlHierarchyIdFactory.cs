// --------------------------------------------------------------------------------
// <copyright file="SqlHierarchyIdFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.ValueFactories.Spatial
{
    using Df.Extensibility;
    using Df.Stochastic;
    using Microsoft.SqlServer.Types;
    using System;

    [ValueFactory("sqlhierarchyid", "Generates random SqlHierarchyId values", typeof(SqlHierarchyId), typeof(SqlHierarchyIdFactory))]
    public sealed class SqlHierarchyIdFactory
        : RandomFactory<SqlHierarchyId, EmptyConfiguration>,
        IConfigurator
    {
        public IValueFactoryConfiguration CreateConfiguration() =>
            ValueFactoryConfiguration.Empty;

        public override SqlHierarchyId CreateValue() =>
            SqlHierarchyId.Parse("/{0}/".FormatInvariant(Random.NextInt32(0, 11)));
    }
}