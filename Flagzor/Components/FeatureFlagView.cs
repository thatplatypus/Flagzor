using Microsoft.AspNetCore.Components;

namespace Flagzor.Components
{
    public class FeatureFlagView : FeatureFlagCore
    {
        private string[]? _featureFlagData;

        /// <summary>
        /// Constructs an instance of <see cref="FeatureFlagView"/>.
        /// </summary>
        public FeatureFlagView()
        {
        }

        /// <summary>
        /// The feature that determine access to the content.
        /// </summary>
        [Parameter] public string? Feature { get; set; }

        protected override void OnInitialized()
        {
            _featureFlagData = Feature != null ? [Feature] : [];

        }

        /// <summary>
        /// Gets the data used for feature flag checking.
        /// </summary>
        protected override string[] GetFeatureFlagData()
            => _featureFlagData ?? [];
    }
}
