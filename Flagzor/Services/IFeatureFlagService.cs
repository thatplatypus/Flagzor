namespace Flagzor
{
    /// <summary>
    /// Defines a service for managing feature flags.
    /// </summary>
    public interface IFeatureFlagService
    {
        /// <summary>
        /// Gets a list of feature flags.
        /// </summary>
        /// <param name="enabledOnly">If true, only enabled feature flags are returned; otherwise, all feature flags are returned.</param>
        /// <returns>A list of feature flags.</returns>
        Task<List<FeatureFlag>> GetFeatureFlagsAsync(bool enabledOnly = true);

        /// <summary>
        /// Registers a feature flag.
        /// </summary>
        /// <param name="feature">The name of the feature.</param>
        /// <param name="enabled">A value indicating whether the feature is enabled.</param>
        /// <param name="attributes">The attributes associated with the feature.</param>
        Task RegisterFeatureFlag(string feature, bool enabled = true, Dictionary<string, string>? attributes = default);

        /// <summary>
        /// Checks if a feature flag is enabled.
        /// </summary>
        /// <param name="feature">The name of the feature.</param>
        /// <returns>True if the feature flag is enabled; otherwise, false.</returns>
        Task<bool> IsFeatureFlagEnabled(string feature);

        /// <summary>
        /// Disables a feature flag.
        /// </summary>
        /// <param name="feature">The name of the feature.</param>
        Task DisableFeatureFlag(string feature);

        /// <summary>
        /// Unregisters a feature flag.
        /// </summary>
        /// <param name="feature">The name of the feature.</param>
        Task UnregisterFeatureFlag(string feature);
    }
}