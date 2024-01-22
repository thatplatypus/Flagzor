using Flagzor.Configuration;
using Microsoft.Extensions.Logging;

namespace Flagzor.Services
{
    /// <summary>
    /// Represents a service for managing feature flags. This is the default implementation provided by FLagzor.
    /// </summary>
    public class FeatureFlagService : IFeatureFlagService
    {
        private readonly List<FeatureFlag> _features = [];
        private readonly FeatureFlagConfiguration _config;
        private readonly ILogger<FeatureFlagService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureFlagService"/> class.
        /// </summary>
        public FeatureFlagService(FeatureFlagConfiguration config, ILogger<FeatureFlagService> logger)
        {
            _config = config;
            _logger = logger;
            if (_config.FeatureFlags != null)
            {
                _features = _config.FeatureFlags;
            }
        }

        ///<inheritdoc/>
        public Task DisableFeatureFlag(string feature)
        {
            if(_config.ReadOnly)
            {
                _logger.LogWarning("Cannot disable feature flag {feature} because the configuration is read-only.", feature);
                return Task.CompletedTask;
            }

            _features.Where(x => x.Feature == feature).ToList().ForEach(x => x.Enabled = false);

            return Task.CompletedTask;
        }

        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public Task RegisterFeatureFlag(string feature, bool enabled = true, Dictionary<string, string>? attributes = null)
        {
            if(_config.ReadOnly)
            {
                _logger.LogWarning("Cannot register feature flag {feature} because the configuration is read-only.", feature);
                return Task.CompletedTask;
            }   

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

        ///<inheritdoc/>
        public Task UnregisterFeatureFlag(string feature)
        {
            if(_config.ReadOnly)
            {
                _logger.LogWarning("Cannot unregister feature flag {feature} because the configuration is read-only.", feature);
                return Task.CompletedTask;
            }

            _features.Where(x => x.Feature == feature).ToList().ForEach(x => _features.Remove(x));

            return Task.CompletedTask;
        }

        ///<inheritdoc/>
        public Task<List<FeatureFlag>> GetFeatureFlagsAsync(bool enabledOnly = true)
        {
            return Task.FromResult(_features.Where(x =>  x.Enabled).ToList());
        }
    }
}
