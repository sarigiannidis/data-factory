// --------------------------------------------------------------------------------
// <copyright file="IServiceCollectionExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Microsoft.Extensions.DependencyInjection
{
    using Df;
    using Df.Handlers;
    using Df.Options;
    using Microsoft.Extensions.Configuration;

    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDfOptionHandlers(this IServiceCollection services, IConfigurationSection configurationSection) => Check.NotNull(nameof(services), services)
            .Configure<Preferences>(configurationSection)
            .AddTransient<IHandler<AddOptions>, AddHandler>()
            .AddTransient<IHandler<GenerateOptions>, GenerateHandler>()
            .AddTransient<IHandler<ListOptions>, ListHandler>()
            .AddTransient<IHandler<NewOptions>, NewHandler>()
            .AddTransient<IHandler<TestOptions>, TestHandler>();
    }
}