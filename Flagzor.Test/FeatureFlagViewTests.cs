using Flagzor.Configuration;
using Flagzor.Extensions;

namespace Flagzor.Test
{
    public class FeatureFlagViewTests : TestContext
    {
        public FeatureFlagViewTests()
        {
            Services.AddFeatureFlagzor(config =>
            {
                config.FeatureFlags.Add(new FeatureFlag
                {
                    Feature = "Beta",
                    Enabled = true
                });
            });
        }

        [Fact]
        public void FeatureFlagView_ShouldRenderChildContent_WhenFeatureEnabled()
        {
            // Act
            var cut = RenderComponent<SimpleBetaFeature>();

            // Assert
            cut.MarkupMatches("Enabled Beta Feature");
        }

        [Fact]
        public void FeatureFlagView_ShouldRenderEmptyyChildContent_WhenFeatureNotEnabled()
        {
            // Arrange
            var config = Services.GetService<FeatureFlagConfiguration>();
            var feature = config.FeatureFlags.FirstOrDefault(x => x.Feature == "Beta");
            feature.Enabled = false;

            // Act
            var cut = RenderComponent<SimpleBetaFeature>();

            // Assert
            cut.MarkupMatches("");
        }

        [Fact]
        public void FeatureFlagView_ShouldRenderActiveContent_WhenFeatureEnabled()
        {
            //Arrange
            Services.AddFeatureFlagzor(config =>
            {
                config.FeatureFlags.Add(new FeatureFlag
                {
                    Feature = "Test",
                    Enabled = true
                });
            });

            // Act
            var cut = RenderComponent<TestFeatureComponent>();

            // Assert
            cut.MarkupMatches("This is shown when the feature flag is enabled.");
        }

        [Fact]
        public void FeatureFlagView_ShouldRenderInactiveContent_WhenFeatureNotEnabled()
        {
            // Act
            var cut = RenderComponent<TestFeatureComponent>();

            // Assert
            cut.MarkupMatches("This is shown when the feature flag is disabled.");
        }
    }
}
