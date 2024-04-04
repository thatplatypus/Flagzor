# Table of Contents
1. [Getting Started](#getting-started)
2. [Configuration](#configuration)
3. [Usage](#usage)
4. [Extending Flagzor](#extending-flagzor)


# Flagzor

Flagzor is a feature flagging library for Blazor. It provides a declarative API similar to Microsoft's authentication state. It's quick to set up, but also flexible, allowing different providers to implement an `IFeatureFlagService` to communicate with different feature flag backends. Alternatively, you can manage everything with configuration and environment variables.

## Getting Started

To use Flagzor, you need to add a `Flagzor` section in your configuration file (like `appsettings.json`). Here's an example:

```json
{
    "Flagzor": {
        "FeatureFlags": [
            {
                "Feature": "NewHomePage",
                "Enabled": true,
                "Attributes": {
                    "Color": "Blue",
                    "Size": "Large"
                }
            },
            {
                "Feature": "BetaFeature",
                "Enabled": false,
                "Attributes": {
                    "StartDate": "2022-01-01",
                    "EndDate": "2022-12-31"
                }
            }
        ],
        "ReadOnly": false
    }
}
```
## Configuration
The Flagzor section contains an array of feature flags. Each feature flag has the following properties:

Feature: The name of the feature.
Enabled: A boolean indicating whether the feature is enabled.
Attributes: A dictionary of additional attributes for the feature.
The ReadOnly property at the root of the Flagzor section indicates whether the feature flags can be changed at runtime.

## Usage
To use the feature flags in your application, you need to add the Flagzor services in your `Program.cs`

```csharp
services.AddFeatureFlagzor(Configuration);
```

You can manually configure features as well.

```
builder.Services.AddFeatureFlagzor(config =>
{
    config.FeatureFlags.Add(new FeatureFlag
    {
        Feature = "BetaFeature",
        Enabled = true
    });
});
```

In your Razor components, you use can use it very similarly to Microsoft's AuthorizeView. You will need to add the `CascadingFeatureFlagState` as a wrapper around components you want to be aware of active feature flags, such as around your content in MainLayout:

```razor
<CascadingFeatureFlagState>
...
...
...
</CascadingFeatureFlagState>
```

When the component renders, components wrappedd in `FeatureFlagView` will conditionally render content depending on the State of the active Feature Flags.

The most basic usage simply takes Child Content and displays it when the named FeatureFlag is active.

```razor
<FeatureFlagView Feature="BetaFeature">
    This is shown when the feature flag is enabled.
</FeatureFlagView>
```

You can also show different sets of content to indicate between active feature flags, or have a fallback.

```razor
<FeatureFlagView Feature="Test2">
    <Active>
        This is shown when the feature flag is enabled.
    </Active>
    <Inactive>
        This is shown when the feature flag is disabled.
    </Inactive>
</FeatureFlagView>
```

Another example with changing logic by rendering a different button:
```razor
<FeatureFlagView Feature="BetaFeature">
    <Active>
        <button onclick=(BetaFunction)>Feature Button</button>
    </Active>
    <Inactive>
       <button onclick=(NormalFunction)>Button</button>
    </Inactive>
</FeatureFlagView>
```

## Extending Flagzor
Flagzor is designed to be flexible. You can implement your own `IFeatureFlagService` to communicate with different feature flag backends. This allows you to use Flagzor in a variety of different environments and configurations. You can also implement the base class `FeatureFlagStateProvider` to change the functionality of the `DefaultFeatureFlagStateProvider`

