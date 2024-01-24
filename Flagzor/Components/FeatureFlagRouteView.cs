using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Threading.Tasks;

namespace Flagzor.Components
{
    /// <summary>
    /// A component that combines the behaviors of <see cref="RouteView"/>, but only displays the page
    /// if the specified feature flag is enabled.
    /// </summary>
    public class FeatureFlagRouteView : RouteView
    {
        /// <summary>
        /// The injected feature flag service.
        /// </summary>
        [Inject]
        private IFeatureFlagService? FeatureFlagService { get; set; } = default!;

        /// <summary>
        /// The feature flag that controls access to the route.
        /// </summary>
        [Parameter]
        public string Feature { get; set; } = "";

        /// <summary>
        /// The content that will be displayed if the feature flag is enabled.
        /// </summary>
        [Parameter]
        public RenderFragment? Active { get; set; }

        /// <summary>
        /// The content that will be displayed if the feature flag is not enabled.
        /// </summary>
        [Parameter]
        public RenderFragment? NotActive { get; set; }

        private static readonly RenderFragment _defaultNotActiveContent
            = builder => builder.AddContent(0, "Feature not enabled");

        private readonly RenderFragment _renderFeatureFlagRouteViewCoreDelegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureFlagRouteView"/> class.
        /// </summary>
        public FeatureFlagRouteView()
        {
            RenderFragment renderBaseRouteViewDelegate = base.Render;
            _renderFeatureFlagRouteViewCoreDelegate = builder => RenderFeatureFlagRouteViewCore(builder, renderBaseRouteViewDelegate);
        }

        /// <summary>
        /// Renders the component.
        /// </summary>
        /// <param name="builder">The <see cref="RenderTreeBuilder"/> to receive the component's render output.</param>
        protected override void Render(RenderTreeBuilder builder)
        {
            _renderFeatureFlagRouteViewCoreDelegate(builder);
        }

        private async void RenderFeatureFlagRouteViewCore(RenderTreeBuilder builder, RenderFragment baseRenderDelegate)
        {
            if (await FeatureFlagService!.IsFeatureFlagEnabled(Feature))
            {
                (Active ?? baseRenderDelegate)(builder);
            }
            else
            {
                (NotActive ?? _defaultNotActiveContent)(builder);
            }
        }
    }
}