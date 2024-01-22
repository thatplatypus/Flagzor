using Flagzor.Components;
using Flagzor.Configuration;
using Flagzor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flagzor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string _configSectionName = "Flagzor";
        public static IServiceCollection AddFeatureFlagzor(this IServiceCollection services, Action<FeatureFlagConfiguration> configure)
        {
            var configuration = new FeatureFlagConfiguration();
            configure(configuration);

            services.AddSingleton(configuration);
            services.AddScoped<IFeatureFlagService, FeatureFlagService>();
            services.AddScoped<FeatureFlagStateProvider, DefaultFeatureFlagStateProvider>();

            return services;
        }

        public static IServiceCollection AddFeatureFlagzor(this IServiceCollection services, IConfiguration configuration)
        {
            var featureFlagConfiguration = configuration.GetSection(_configSectionName).Get<FeatureFlagConfiguration>() 
                ?? throw new InvalidOperationException($"Missing '{_configSectionName}' section in configuration.");
            
            services.AddSingleton(featureFlagConfiguration);
            services.AddScoped<IFeatureFlagService, FeatureFlagService>();
            services.AddScoped<FeatureFlagStateProvider, DefaultFeatureFlagStateProvider>();

            return services;
        }
    }
}
