// --------------------------------------------------------------------------------
// <copyright file="MetaDbContextFactory.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Df.Data.Meta
{
    using Microsoft.EntityFrameworkCore;

    internal sealed class MetaDbContextFactory
        : IMetaDbContextFactory
    {
        public MetaDbContext Create(string connectionString)
        {
            Check.NotNull(nameof(connectionString), connectionString);
            var dbContextOptionsBuilder = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString);
            return new MetaDbContext(dbContextOptionsBuilder.Options);
        }
    }
}