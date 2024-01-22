namespace Flagzor.Components
{
    /// <summary>
    /// Provides information about the currently active feature flags, if any.
    /// </summary>
    public class FeatureFlagState
    {
        private readonly List<string> _features = [];

        /// <summary>
        /// Constructs an instance of <see cref="FeatureFlagState"/>.
        /// </summary>
        public FeatureFlagState(string feature)
        {
            if(!_features.Contains(feature))
            {
                _features.Add(feature);
            }
        }

        public FeatureFlagState(List<string> features)
        {
            _features = features;
        }   

        public List<string> FeatureFlags => _features;
    }
}

