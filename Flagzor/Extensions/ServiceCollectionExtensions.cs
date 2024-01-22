using Flagzor.Components;
using Flagzor.Configuration;
using Flagzor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Flagzor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureFlagzor(this IServiceCollection services, Action<FeatureFlagConfiguration> configure)
        {
            var configuration = new FeatureFlagConfiguration();
            configure(configuration);

            services.AddSingleton(configuration);

            services.AddSingleton<IFeatureFlagService, FeatureFlagService>();
            services.AddSingleton<FeatureFlagStateProvider, DefaultFeatureFlagStateProvider>();

            return services;
        }
    }
}
