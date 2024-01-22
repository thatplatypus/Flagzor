namespace Flagzor.Components
{
    /// <summary>
    /// Provides the default implementation of <see cref="FeatureFlagStateProvider"/>.
    /// </summary>
    /// <param name="featureFlagService">The feature flag service.</param>
    public class DefaultFeatureFlagStateProvider(IFeatureFlagService featureFlagService) : FeatureFlagStateProvider
    {
        /// <summary>
        /// Gets the feature flag state.
        /// </summary>
        public override async Task<FeatureFlagState> GetFeatureFlagState()
        {
            var features = await featureFlagService.GetFeatureFlagsAsync();

            var state = new FeatureFlagState(features.Select(x => x.Feature).ToList());
            NotifyFeatureFlagStateChanged(Task.FromResult(state));
            return state;
        }
    }
}
