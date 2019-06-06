// --------------------------------------------------------------------------------
// <copyright file="IServiceCollectionExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Microsoft.Extensions.DependencyInjection
{
    using Df;
    using Df.Production;

    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDfProduction(this IServiceCollection services) => Check.NotNull(nameof(services), services)
            .AddTransient<IDatasetGeneratorFactory, DatasetGeneratorFactory>()
            .AddTransient<IRecordGeneratorFactory, RecordGeneratorFactory>();
    }
}