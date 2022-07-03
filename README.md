# Azure Communication Service Event Handler Library

[![.NET](https://github.com/jasonshave/Azure.Communication.Service/actions/workflows/dotnet.yml/badge.svg)](https://github.com/jasonshave/Azure.Communication.Service/actions/workflows/dotnet.yml)

This .NET library provides a set of convenience layer services and extensions to the `Azure.Communication.Service.CallingServer` and `Azure.Communication.Service.JobRouter` libraries currently in Public/Private preview.

## Common event handling and orchestration challenges

A common task developers must undertake with an event-driven platform is to deal with a common event payload which wraps a variation of models often denoted with a type identifier. Consider the following event, `CallConnectionStateChanged` which is 'wrapped' in an `Azure.Messaging.CloudEvent` type:

### Azure.Messaging.CloudEvent

```json
{
    "id": "7dec6eed-129c-43f3-a2bf-134ac1978168",
    "source": "calling/callConnections/441f1200-fd54-422e-9566-a867d187dca7/callState",
    "type": "Microsoft.Communication.CallConnectionStateChanged",
    "data": {
        "callConnectionId": "441f1200-fd54-422e-9566-a867d187dca7",
        "callConnectionState": "connected"
    },
    "time": "2022-06-24T15:12:41.5556858+00:00",
    "specversion": "1.0",
    "datacontenttype": "application/json",
    "subject": "calling/callConnections/441f1200-fd54-422e-9566-a867d187dca7/callState"
}
```

Since this event is triggered by a web hook callback, the API controller consuming this `CloudEvent` type must extract the body by reading the `HttpRequest` object, typically asynchronously, then deserialize it to it's proper type as follows:

```csharp
string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
CloudEvent[] cloudEvents = JsonSerializer.Deserialize<CloudEvent[]>(requestBody);
```

You then need to invoke conditional logic with "magic strings" against the `type` property to understand what payload exists in the `data` object, then deserialize that into something useful as follows:

```csharp
foreach(var cloudEvent in cloudEvents)
{
    if (cloudEvent.Type == "Microsoft.Communication.CallConnectionStateChanged")
    {
        CallConnectionStateChanged @event = JsonSerializer.Deserialize<CallConnectionStateChanged>(cloudEvent.Data);        
        // now you can invoke your action based on this event    
    }
}
```

Unfortunately this conditional logic handling needs to be done by every customer for every possible event type which turns the focus of the developer away from their business problem and concerns them with the non-functional challenges.

## Event handling dependency injection configuration

1. Add the following to your .NET 6 or higher `Program.cs` file:

    ```csharp
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddEventHandlerServices() //<--adds common event handling services
        .AddCallingServerEventHandling() //<--adds support for CallingServer SDK events
        .AddJobRouterEventHandling(); //<--adds support for Job Router events
    
    var app = builder.Build();

    app.Run();
    ```

## Publishing CloudEvents and EventGridEvents

Using .NET constructor injection, add the `IEventPublisher<CallingServer>` or `IEventPublisher<JobRouter>` to push `Azure.Messaging.CloudEvent` and `Azure.Messaging.EventGrid` messages into the services where their types are automatically cast, deserialized, and the correct event handler is invoked.

```csharp
public class SomeEventHandler : 
    IEventHandler<CloudEvent>, 
    IEventHandler<EventGridEvent>
{
    private readonly IEventPublisher<CallingServer> _callingServerPublisher;
    private readonly IEventPublisher<JobRouter> _jobRouterPublisher;
    
    public SomeEventHandler(
        IEventPublisher<CallingServer> callingServerPublisher,
        IEventPublisher<JobRouter> jobRouterPublisher)
    {
        _callingServerPublisher = callingServerPublisher;
        _jobRouterPublisher = jobRouterPublisher;
    }

    public void Handle(CloudEvent cloudEvent) =>
        _callingServerPublisher.Publish(cloudEvent.Data, cloudEvent.Type, "myContextId");

    public void Handle(EventGridEvent eventGridEvent) =>
        _jobRouterPublisher.Publish(eventGridEvent.Data, eventGridEvent.EventType);
}
```

## Subscribing to Calling Server and Job Router events

As mentioned above, the `CloudEvent` or `EventGridEvent` is pushed and the corresponding C# event is invoked and subscribed to. For the Calling Server SDK, incoming calls are delivered using Event Grid and mid-call events are delivered through web hook callbacks. Job Router uses Event Grid to deliver all events. The example below shows a .NET background service wiring up the event handler as follows:

```csharp
public class MyService : BackgroundService
{
    private readonly ICallingServerEventSubscriber _callingServerSubscriber;
    private readonly IJobRouterEventSubscriber _jobRouterSubscriber;

    public MyService(
        ICallingServerEventSubscriber callingServerSubscriber,
        IJobRouterEventSubscriber jobRouterSubscriber)
    {
        _callingServerSubscriber = callingServerSubscriber;
        _jobRouterSubscriber = jobRouterSubscriber
    }
    

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // subscribe to Calling Server and Job Router events
        _callingServerSubscriber.OnCallConnectionStateChanged += HandleOnCallConnectionStateChanged;

        _jobRouterSubscriber.OnJobQueued += HandleOnJobQueued;

        while (!cancellationToken.IsCancellationRequested)
        {
            Task.Delay(1000, cancellationToken);
        }

        return Task.CompletedTask;
    }

    private Task HandleOnCallConnectionStateChanged(CallConnectionStateChanged args, string contextId)
    {
        _logger.LogInformation($"Call connection ID: {args.CallConnectionId} | Context: {contextId}");
        return Task.CompletedTask;
    }

    private Task HandleOnJobQueued(RouterJobQueued jobQueued, string contextId)
    {
        _logger.LogInformation($"Job {jobQueued.JobId} in queue {jobQueued.QueueId}")
        return Task.CompletedTask;
    }
}
```

## License

This project is licensed under the MIT License - see the [LICENSE.md](license.md) file for details.
