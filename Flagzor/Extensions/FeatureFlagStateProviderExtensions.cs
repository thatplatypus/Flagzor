using Flagzor;
using Flagzor.Components;

namespace Flagzor.Extensions
{
    public static class FeatureFlagStateProviderExtensions
    {
        public static async Task RegisterFeatureFlagAsync(this FeatureFlagStateProvider provider, IFeatureFlagService service, string feature)
        {
            await service.RegisterFeatureFlag(feature);
            await provider.GetFeatureFlagState();
        }

        public static async Task UnregisterFeatureFlagAsync(this FeatureFlagStateProvider provider, IFeatureFlagService service, string feature)
        {
            await service.UnregisterFeatureFlag(feature);
            await provider.GetFeatureFlagState();
        }

        public static async Task<bool> IsFeatureFlagActive(this FeatureFlagStateProvider provider, string feature)
        {
            var state = await provider.GetFeatureFlagState();

            foreach(var flag in state.FeatureFlags)
            {
                Console.WriteLine(flag);
                Console.WriteLine(state.FeatureFlags.Any(x => x.Equals(feature, StringComparison.OrdinalIgnoreCase)).ToString());
            }

            if(state.FeatureFlags.Count == 0)
            {
                Console.WriteLine("No State");
            }

            return state.FeatureFlags.Any(x => x.Equals(feature, StringComparison.OrdinalIgnoreCase));
        }
    }
}