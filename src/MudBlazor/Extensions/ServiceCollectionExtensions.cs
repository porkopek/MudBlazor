﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MudBlazor
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMudBlazorDialog(this IServiceCollection services)
        {
            return services.AddScoped<IDialogService, DialogService>();
        }

        public static IServiceCollection AddMudBlazorSnackbar(this IServiceCollection services, SnackbarConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            services.TryAddScoped<ISnackbar>(builder => new Snackbars(configuration));
            return services;
        }

        public static IServiceCollection AddMudBlazorSnackbar(this IServiceCollection services)
        {
            return AddMudBlazorSnackbar(services, new SnackbarConfiguration());
        }

        public static IServiceCollection AddMudBlazorSnackbar(this IServiceCollection services, Action<SnackbarConfiguration> configure)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            var options = new SnackbarConfiguration();
            configure(options);

            return AddMudBlazorSnackbar(services, options);
        }

        
        /// <summary>
        /// Injects the services that allow to manage scroll
        /// </summary>
       /// <returns></returns>
        public static IServiceCollection AddMudBlazorScrollServices(this IServiceCollection services)
        {
            services.AddTransient<IScrollListener, ScrollListener>();
            services.AddTransient<IScrollManager, ScrollManager>();
            return services;
        }

    }
}
