using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

public static class EventHandlerBuilderExtensions
{
    public static EventHandlerBuilder AddCallingServerEventHandling(this EventHandlerBuilder eventHandlerBuilder)
    {
        eventHandlerBuilder.Services.AddSingleton<IEventPublisher<CallingServer>, EventPublisher<CallingServer>>();

        var catalog = new EventCatalogService<CallingServer>();

        catalog
            .Register<IncomingCall>()
            .Register<CallConnectedEvent>()
            .Register<CallDisconnectedEvent>()
            .Register<CallConnectionStateChanged>();

        eventHandlerBuilder.Services.AddSingleton<IEventCatalog<CallingServer>>(catalog);

        var dispatcher = new CallingServerEventDispatcher();
        eventHandlerBuilder.Services.AddSingleton<IEventDispatcher<CallingServer>>(dispatcher);
        eventHandlerBuilder.Services.AddSingleton<ICallingServerEventSubscriber>(dispatcher);

        return eventHandlerBuilder;
    }
}