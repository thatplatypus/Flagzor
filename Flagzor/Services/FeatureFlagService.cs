using Flagzor.Configuration;

namespace Flagzor.Services
{
    public class FeatureFlagService : IFeatureFlagService
    {
        private readonly List<FeatureFlag> _features = [];
        private readonly FeatureFlagConfiguration _config;

        public FeatureFlagService(FeatureFlagConfiguration config)
        {
            _config = config;
            if(_config.FeatureFlags != null)
            {
                _features = _config.FeatureFlags;
            }
        }

        public Task DisableFeatureFlag(string feature)
        {
            _features.Where(x => x.Feature == feature).ToList().ForEach(x => x.Enabled = false);

            return Task.CompletedTask;
        }

        public Task<bool> IsFeatureFlagEnabled(string feature)
        {
            if (_features.Where(x => x.Enabled).Select(x => x.Feature).Contains(feature))
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task RegisterFeatureFlag(string feature, bool enabled = true, Dictionary<string, string>? attributes = null)
        {
            if (!_features.Select(x => x.Feature).Contains(feature))
            {
                _features.Add(new FeatureFlag
                {
                    Feature = feature,
                    Enabled = true,
                });
            }
            else
            {
                _features.SingleOrDefault(x => x.Feature == feature)!.Enabled = enabled;
            }

            return Task.CompletedTask;
        }

        public Task UnregisterFeatureFlag(string feature)
        {
            _features.Where(x => x.Feature == feature).ToList().ForEach(x => _features.Remove(x));

            return Task.CompletedTask;
        }

        public Task<List<FeatureFlag>> GetFeatureFlagsAsync(bool enabledOnly = true)
        {
            return Task.FromResult(_features.Where(x =>  x.Enabled).ToList());
        }
    }
}
