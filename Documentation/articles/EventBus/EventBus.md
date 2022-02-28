# EventBus [![openupm](https://img.shields.io/npm/v/com.autsoft.unitysupplements.eventbus?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.autsoft.unitysupplements.eventbus/)

This package contains an implementation of a event bus.

## Installation

Use [OpenUPM](https://openupm.com/) to install the package.

```
openupm add com.autsoft.unitysupplements.eventbus
```

## Getting started
To make it easier to use the event bus, you may want to use a dependency injection container. We have several options for this when using Unity. For *Microsoft.Extensions.DependencyInjection* an extension method you can use to register our event bus implementation like this:

```csharp
public static void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
{
    var assemblies = new[] { typeof(AppInstaller).Assembly };

    services.AddEventBus(assemblies);
    .
    .
    .
    // other registrations
}
```

After that you can inject *IEventBus* into your monobehaviours. This interfaces contains three main functions:

- **Subsribe**: Method to register a handler to an event
- **UnSubscribe**: Method to remove the previously registered handler
- **Invoke**: Method to invoke an event

We can create as many events as we want, but all event must implement from the **IEvent** interface. Below is an example of a sample event.

### Example
```csharp
public sealed class SampleEvent : IEvent
{
    public SampleEvent(int eventData) => EventData = eventData;
    public int EventData { get; }
}
```

And you can invoke and subscribe to an event like so:

```csharp
public class InvokingClass : MonoBehaviour
{
    [Inject] private readonly IEventBus _eventBus = default!;

    public void SomeMethod()
    {
        _eventBus.Invoke(new SampleEvent(12));
    }
}
```

```csharp
public class SubscriberClass : MonoBehaviour
{
    [Inject] private readonly IEventBus _eventBus = default!;

    private void Start()
    {
        _eventBus.Subscribe<SampleEvent>(OnSampleEvent);
    }

    private void OnDestroy()
    {
        _eventBus.UnSubscribe<SampleEvent>(OnSampleEvent);
    }

    private void OnSampleEvent(SampleEvent event)
    {
        Debug.Log(event.EventData); // Prints "12" to unity log when SampleEvent is invoked
    }
}
```

> [!WARNING]
> Dont forget to UnSubscribe your events in the OnDestroy method

You can also use the *SubscribeWeak* extension method, which uses the [DestroyDetector](xref:AutSoft.UnitySupplements.Vitamins.DestroyDetector) component to unsubscribe automatically when the object is destroyed.

```csharp
public class SubscriberClass : MonoBehaviour
{
    [Inject] private readonly IEventBus _eventBus = default!;

    private void Start() => _eventBus.SubscribeWeak(this, OnSampleEvent);

    private void OnSampleEvent(SampleEvent event) => Debug.Log(event.EventData); // Prints "12" to unity log when SampleEvent is invoked
}
```

### EventHandler
Another way to call a method when an event gets invoked is to implement the **IEventHandler** interface in a separate class. 

```csharp
public class HandlerClass : IEventHandler<SampleEvent>
{
    public void Handle(SampleEvent message)
    {
        Debug.Log(message.EventData);
    }
}
```

If we used the *AddEventBus* extension method to register the eventbus to the DI container, all the classes that implement the *IEventHandler* will also be registered.

### EventBus
You can also create your own eventbus implementation with the **IEventBus** interface.
