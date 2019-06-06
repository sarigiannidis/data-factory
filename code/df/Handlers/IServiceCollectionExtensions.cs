// --------------------------------------------------------------------------------
// <copyright file="IServiceCollectionExtensions.cs" company="Michalis Sarigiannidis">
// Copyright 2019 © Michalis Sarigiannidis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// --------------------------------------------------------------------------------

namespace Microsoft.Extensions.DependencyInjection
{
    using Df;
    using Df.OptionHandlers;
    using Df.Options;

    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDfOptionHandlers(this IServiceCollection services) => Check.NotNull(nameof(services), services)
            .AddTransient<IHandler<AddOptions>, AddHandler>()
            .AddTransient<IHandler<GenerateOptions>, GenerateHandler>()
            .AddTransient<IHandler<ListOptions>, ListHandler>()
            .AddTransient<IHandler<NewOptions>, NewHandler>()
            .AddTransient<IHandler<TestOptions>, TestHandler>();
    }
}