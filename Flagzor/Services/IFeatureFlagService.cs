using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flagzor
{
    public interface IFeatureFlagService
    {
        Task<List<FeatureFlag>> GetFeatureFlagsAsync(bool enabledOnly = true);

        Task RegisterFeatureFlag(string feature, bool enabled = true, Dictionary<string, string>? attributes = default);

        Task<bool> IsFeatureFlagEnabled(string feature);

        Task DisableFeatureFlag(string feature);

        Task UnregisterFeatureFlag(string feature);
    }
}
