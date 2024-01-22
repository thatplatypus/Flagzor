namespace Flagzor.Configuration
{
    public class FeatureFlagConfiguration
    {
        public List<FeatureFlag> FeatureFlags { get; set; } = [];

        public bool ReadOnly = false;
    }
}
