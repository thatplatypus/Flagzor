using Flagzor;
using Flagzor.Components;

namespace Flagzor.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="FeatureFlagStateProvider"/> class.
    /// </summary>
    public static class FeatureFlagStateProviderExtensions
    {
        /// <summary>
        /// Registers a feature flag with the provided service and updates the feature flag state.
        /// </summary>
        /// <param name="provider">The feature flag state provider.</param>
        /// <param name="service">The feature flag service.</param>
        /// <param name="feature">The feature to register.</param>
        public static async Task RegisterFeatureFlagAsync(this FeatureFlagStateProvider provider, IFeatureFlagService service, string feature)
        {
            await service.RegisterFeatureFlag(feature);
            await provider.GetFeatureFlagState();
        }

        /// <summary>
        /// Unregisters a feature flag with the provided service and updates the feature flag state.
        /// </summary>
        /// <param name="provider">The feature flag state provider.</param>
        /// <param name="service">The feature flag service.</param>
        /// <param name="feature">The feature to unregister.</param>
        public static async Task UnregisterFeatureFlagAsync(this FeatureFlagStateProvider provider, IFeatureFlagService service, string feature)
        {
            await service.UnregisterFeatureFlag(feature);
            await provider.GetFeatureFlagState();
        }

        /// <summary>
        /// Checks if a feature flag is active.
        /// </summary>
        /// <param name="provider">The feature flag state provider.</param>
        /// <param name="feature">The feature to check.</param>
        public static async Task<bool> IsFeatureFlagActive(this FeatureFlagStateProvider provider, string feature)
        {
            var state = await provider.GetFeatureFlagState();

            return state.FeatureFlags.Any(x => x.Equals(feature, StringComparison.OrdinalIgnoreCase));
        }
    }
}