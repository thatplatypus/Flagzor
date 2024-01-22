using Microsoft.AspNetCore.Components;

namespace Flagzor.Components
{
    /// <summary>
    /// Represents a view that is controlled by a feature flag.
    /// </summary>
    public class FeatureFlagView : FeatureFlagCore
    {
        private string[]? _featureFlagData;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureFlagView"/> class.
        /// </summary>
        public FeatureFlagView()
        {
        }

        /// <summary>
        /// Gets or sets the feature that determines access to the content.
        /// If the feature is enabled, the content is accessible; otherwise, it is not.
        /// </summary>
        [Parameter] public string? Feature { get; set; }

        /// <summary>
        /// Called when the component is initialized.
        /// Sets the feature flag data based on the value of the Feature property.
        /// </summary>
        protected override void OnInitialized()
        {
            _featureFlagData = Feature != null ? [Feature] : [];
        }

        /// <summary>
        /// Gets the data used for feature flag checking.
        /// If the Feature property is not null, returns an array containing the Feature;
        /// otherwise, returns an empty array.
        /// </summary>
        protected override string[] GetFeatureFlagData()
            => _featureFlagData ?? [];
    }
}