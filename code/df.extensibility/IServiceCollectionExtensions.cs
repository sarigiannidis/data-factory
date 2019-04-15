// --------------------------------------------------------------------------------
// <copyright file="IServiceCollectionExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Microsoft.Extensions.DependencyInjection
{
    using Df;
    using Df.Extensibility;
    using Microsoft.Extensions.Configuration;

    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDfExtensibility(this IServiceCollection services, IConfigurationSection configurationSection) =>
            Check.NotNull(nameof(services), services)
                .Configure<ValueFactoryManagerOptions>(configurationSection)
                .AddSingleton<IValueFactoryManager, ValueFactoryManager>();
    }
}