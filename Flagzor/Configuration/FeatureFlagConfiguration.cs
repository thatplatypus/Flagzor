namespace Flagzor.Configuration
{
    /// <summary>
    /// Represents the configuration for feature flags.
    /// </summary>
    public class FeatureFlagConfiguration
    {
        /// <summary>
        /// Gets or sets the list of feature flags.
        /// </summary>
        public List<FeatureFlag> FeatureFlags { get; set; } = [];

        /// <summary>
        /// Gets or sets a value indicating whether the configuration is read-only.
        /// If true, the configuration cannot be modified; otherwise, it can be modified.
        /// </summary>
        public bool ReadOnly = false;
    }
}