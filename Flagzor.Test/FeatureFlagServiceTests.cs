using Flagzor.Services;
using Flagzor.Configuration;
using Microsoft.Extensions.Logging;

namespace Flagzor.Test
{
    public class FeatureFlagServiceTests
    {
        private readonly FeatureFlagService _service;
        private readonly FeatureFlagConfiguration _config;
        private readonly ILogger<FeatureFlagService> _logger;

        public FeatureFlagServiceTests()
        {
            _config = new FeatureFlagConfiguration
            {
                FeatureFlags =
                [
                    new FeatureFlag
                    {
                        Feature = "TestFeature",
                        Enabled = true
                    }
                ],
                ReadOnly = false
            };
            _logger = Substitute.For<ILogger<FeatureFlagService>>();
            _service = new FeatureFlagService(_config, _logger);
        }

        [Fact]
        public async Task RegisterFeatureFlag_ShouldAddNewFeature_WhenFeatureDoesNotExist()
        {
            // Arrange
            string feature = "TestFeature2";

            // Act
            await _service.RegisterFeatureFlag(feature);

            // Assert
            var result = await _service.IsFeatureFlagEnabled(feature);
            Assert.True(result);
        }

        [Fact]
        public async Task RegisterFeatureFlag_ShouldLogWarning_WhenConfigIsReadOnly()
        {
            // Arrange
            var config = new FeatureFlagConfiguration
            {
                FeatureFlags =
                [
                    new FeatureFlag
                    {
                        Feature = "TestFeature",
                        Enabled = true
                    }
                ],
                ReadOnly = true
            };
            string feature = "TestFeature2";
            var service = new FeatureFlagService(config, _logger);

            // Act
            await service.RegisterFeatureFlag(feature);

            // Assert
            _logger.ReceivedWithAnyArgs().LogWarning("");
        }

        [Fact]
        public async Task DisableFeatureFlag_ShouldDisableFeature_WhenFeatureExistsAndConfigIsNotReadOnly()
        {
            // Arrange
            string feature = "TestFeature";
            await _service.RegisterFeatureFlag(feature);

            // Act
            await _service.DisableFeatureFlag(feature);

            // Assert
            var result = await _service.IsFeatureFlagEnabled(feature);
            Assert.False(result);
        }

        [Fact]
        public async Task IsFeatureFlagEnabled_ShouldReturnFalse_WhenFeatureDoesNotExist()
        {
            // Arrange
            string feature = "NonExistentFeature";

            // Act
            var result = await _service.IsFeatureFlagEnabled(feature);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UnregisterFeatureFlag_ShouldRemoveFeature_WhenFeatureExistsAndConfigIsNotReadOnly()
        {
            // Arrange
            string feature = "TestFeature";
            await _service.RegisterFeatureFlag(feature);

            // Act
            await _service.UnregisterFeatureFlag(feature);

            // Assert
            var result = await _service.IsFeatureFlagEnabled(feature);
            Assert.False(result);
        }

        [Fact]
        public async Task GetFeatureFlagsAsync_ShouldReturnOnlyEnabledFeatures_WhenEnabledOnlyIsTrue()
        {
            // Arrange
            string feature2 = "TestFeature2";
            await _service.RegisterFeatureFlag(feature2);
            await _service.DisableFeatureFlag(feature2);

            // Act
            var result = await _service.GetFeatureFlagsAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("TestFeature", result[0].Feature);
        }
    }
}