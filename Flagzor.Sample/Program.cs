using Flagzor;
using Flagzor.Components;
using Flagzor.Extensions;
using Flagzor.Sample;
using Flagzor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddFeatureFlagzor(config =>
{
    config.FeatureFlags.Add(new FeatureFlag
    {
        Feature = "Navigation",
        Enabled = false
    });
});

await builder.Build().RunAsync();
