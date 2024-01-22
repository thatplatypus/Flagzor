namespace Flagzor.Components
{
    public class DefaultFeatureFlagStateProvider(IFeatureFlagService featureFlagService) : FeatureFlagStateProvider
    {
        public override async Task<FeatureFlagState> GetFeatureFlagState()
        {
            var features = await featureFlagService.GetFeatureFlagsAsync();

            var state = new FeatureFlagState(features.Select(x => x.Feature).ToList());
            NotifyFeatureFlagStateChanged(Task.FromResult(state));
            return state;
        }
    }
}
