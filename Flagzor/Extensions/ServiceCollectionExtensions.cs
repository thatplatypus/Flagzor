using Flagzor.Components;
using Flagzor.Configuration;
using Flagzor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flagzor.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="IServiceCollection"/> interface to add Flagzor services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private const string _configSectionName = "Flagzor";

        /// <summary>
        /// Adds Flagzor services to the specified <see cref="IServiceCollection"/> with a custom configuration.
        /// </summary>
        public static IServiceCollection AddFeatureFlagzor(this IServiceCollection services, Action<FeatureFlagConfiguration> configure)
        {
            var configuration = new FeatureFlagConfiguration();
            configure(configuration);

            services.AddSingleton(configuration);
            services.AddScoped<IFeatureFlagService, FeatureFlagService>();
            services.AddScoped<FeatureFlagStateProvider, DefaultFeatureFlagStateProvider>();

            return services;
        }

        /// <summary>
        /// Adds Flagzor services to the specified <see cref="IServiceCollection"/> with a custom configuration and a custom feature flag service.
        /// </summary>
        public static IServiceCollection AddFeatureFlagzor<T>(this IServiceCollection services, Action<FeatureFlagConfiguration> configure)
            where T : class, IFeatureFlagService
        {
            var configuration = new FeatureFlagConfiguration();
            configure(configuration);

            services.AddSingleton(configuration);
            services.AddScoped<IFeatureFlagService, T>();
            services.AddScoped<FeatureFlagStateProvider, DefaultFeatureFlagStateProvider>();

            return services;
        }

        /// <summary>
        /// Adds Flagzor services to the specified <see cref="IServiceCollection"/> with a custom configuration, a custom feature flag service, and a custom feature flag state provider.
        /// </summary>
        public static IServiceCollection AddFeatureFlagzor<TService, TProvider>(this IServiceCollection services, Action<FeatureFlagConfiguration> configure)
            where TService : class, IFeatureFlagService
            where TProvider : FeatureFlagStateProvider
        {
            var configuration = new FeatureFlagConfiguration();
            configure(configuration);

            services.AddSingleton(configuration);
            services.AddScoped<IFeatureFlagService, TService>();
            services.AddScoped<FeatureFlagStateProvider, TProvider>();

            return services;
        }

        /// <summary>
        /// Adds Flagzor services to the specified <see cref="IServiceCollection"/> with a configuration from an <see cref="IConfiguration"/> object.
        /// </summary>
        public static IServiceCollection AddFeatureFlagzor(this IServiceCollection services, IConfiguration configuration)
        {
            var featureFlagConfiguration = GetFeatureFlagConfiguration(configuration);

            services.AddSingleton(featureFlagConfiguration);
            services.AddScoped<IFeatureFlagService, FeatureFlagService>();
            services.AddScoped<FeatureFlagStateProvider, DefaultFeatureFlagStateProvider>();

            return services;
        }

        /// <summary>
        /// Adds Flagzor services to the specified <see cref="IServiceCollection"/> with a configuration from an <see cref="IConfiguration"/> object and a custom feature flag service.
        /// </summary>
        public static IServiceCollection AddFeatureFlagzor<T>(this IServiceCollection services, IConfiguration configuration)
            where T : class, IFeatureFlagService
        {
            var featureFlagConfiguration = GetFeatureFlagConfiguration(configuration);

            services.AddSingleton(featureFlagConfiguration);
            services.AddScoped<IFeatureFlagService, T>();
            services.AddScoped<FeatureFlagStateProvider, DefaultFeatureFlagStateProvider>();

            return services;
        }

        /// <summary>
        /// Adds Flagzor services to the specified <see cref="IServiceCollection"/> with a configuration from an <see cref="IConfiguration"/> object and a custom feature flag service.
        /// </summary>
        public static IServiceCollection AddFeatureFlagzor<TService, TProvider>(this IServiceCollection services, IConfiguration configuration)
            where TService : class, IFeatureFlagService
            where TProvider : FeatureFlagStateProvider
        {
            var featureFlagConfiguration = GetFeatureFlagConfiguration(configuration);

            services.AddSingleton(featureFlagConfiguration);
            services.AddScoped<IFeatureFlagService, TService>();
            services.AddScoped<FeatureFlagStateProvider, TProvider>();

            return services;
        }

        private static FeatureFlagConfiguration GetFeatureFlagConfiguration(IConfiguration configuration)
        {
            return configuration.GetSection(_configSectionName).Get<FeatureFlagConfiguration>()
                ?? throw new InvalidOperationException($"Missing '{_configSectionName}' section in configuration.");
        }
    }
}
