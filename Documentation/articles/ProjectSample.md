# Project sample
This article demonstrates the process of how to setup Unity projects to be able to use the supplement packages.

## Adding the eventbus package
After creating the project we can add one of our packages to the project with OpenUPM. For example to add the eventbus package open a terminal in the new project root and type the following command:

```PowerShell
openupm add com.autsoft.unitysupplements.eventbus
```

After adding this you will see multiple errors appear in your console, this is because some dependencies from NuGet still need to be added.
## Adding NuGet scope and packages
One way to add NuGet packages inside Unity is to add the following scope to the **scopedRegistries** array inside the *Packages/manifest.json* file.

```json
{
      "name": "Unity NuGet",
      "url": "https://unitynuget-registry.azurewebsites.net",
      "scopes": [
        "org.nuget"
      ]
}
```

After that you can add NuGet packages in the package manager window according to the dependencies described [**here**](xref:Dependencies.md).

![PackageManager](~/images/ProjectSample/PackageManager.png)

Adding these dependencies fixes all the errors we had.

## Adding the Injecter DI
To make it possible to inject the eventbus into `MonoBehaviour` classes we are going to use [Injecter](https://openupm.com/packages/com.injecter.unity/) and [Injecter.Hosting](https://openupm.com/packages/com.injecter.hosting.unity/?subPage=deps) from OpenUPM with the following command.

```PowerShell
openupm add com.injecter.hosting.unity
```

Once again we need to add:
- [Injecter](https://www.nuget.org/packages/Injecter/)  
- [Hosting](https://www.nuget.org/packages/Microsoft.Extensions.Hosting)
- [Serilog.Hosting](https://www.nuget.org/packages/Serilog.Extensions.Hosting/)
  
NuGet packages from the package manager to get rid of the errors.

> [!NOTE]
> If you receive Assembly Version Validation errors you can turn these off in: `Project Settings -> Player -> Other Settings -> Configuration -> Assembly Version Validation`

To easily setup the DI container add the AppInstaller file with right clicking in one of your folders and selecting `Injecter->AppInstallerCompositionRoot`

![AddAppInstaller](~/images/ProjectSample/AppInstallerCreate.png)

After that add the following UPM package: [Serilog.Sinks.Unity3D](https://openupm.com/packages/com.serilog.sinks.unity3d/)

```PowerShell
openupm add com.serilog.sinks.unity3d
```

Create an Assembly definition and add the following references:

![AsmdefSetup](~/images/ProjectSample/AsmdefSetup.png)

Now you can add the following line to the **ConfigureServices** method inside the *AppInstallerCompositionRoot.cs*

```csharp
    services.AddEventBus();
```

## Adding the UiComponents package
Adding more packages becomes more simple after the initial setup,we just need to add the UPM package

```PowerShell
openupm add com.autsoft.unitysupplements.uicomponents
```

And the dependencies found [here](xref:Dependencies.md). Since injecter is already added we can skip that. So we just add  `AutSoft.UnitySupplements.UiComponents` to the .asmdef references. And add the following line to **ConfigureServices** method inside the `AppInstallerCompositionRoot.cs` like before:

```csharp
    services.AddTimeline();
```

Now the timeline packages is ready to use, in the right click menu we can create a basic timeline player, it can be found under `AutSoft->Timeline.`

## Testing
To test everything we are going to create a component which uses an injected **eventbus** to subscribe to the **CurrentTimeChanged** event of the timeline. So it prints the current state of the timeline to the console.

```csharp
#nullable enable
using System;
using AutSoft.UnitySupplements.EventBus;
using AutSoft.UnitySupplements.UiComponents.Timeline;
using AutSoft.UnitySupplements.Vitamins;
using Injecter;
using Injecter.Unity;
using UnityEngine;

public class EventSubscriber : MonoBehaviourScoped
{
    [Inject] private readonly IEventBus _eventBus = default!;

    [SerializeField] private BasicTimelinePlayer _timeline = default!;

    protected override void Awake()
    {
        base.Awake();
        this.CheckSerializedField(_timeline, nameof(_timeline));

        _eventBus.SubscribeWeak<CurrentTimeChanged>(this, message => Debug.Log($"Timeline time changed to: {message.CurrentTime.ToLocalTime()}"));
    }

    private void Start() => _timeline.Initialize(DateTimeOffset.Now, DateTimeOffset.Now.AddHours(1));
}
```