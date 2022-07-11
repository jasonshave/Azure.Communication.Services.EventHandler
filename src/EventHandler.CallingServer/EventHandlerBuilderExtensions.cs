// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

public static class EventHandlerBuilderExtensions
{
    public static EventHandlerBuilder AddCallingServerEventHandling(this EventHandlerBuilder eventHandlerBuilder)
    {
        eventHandlerBuilder.Services.AddSingleton<IEventPublisher<Calling>, EventPublisher<Calling>>();

        var catalog = new EventCatalogService<Calling>();

        catalog
            .Register<IncomingCall>()
            .Register<CallConnectedEvent>()
            .Register<CallDisconnectedEvent>()
            .Register<CallConnectionStateChanged>();

        eventHandlerBuilder.Services.AddSingleton<IEventCatalog<Calling>>(catalog);

        var dispatcher = new CallingServerEventDispatcher();
        eventHandlerBuilder.Services.AddSingleton<IEventDispatcher<Calling>>(dispatcher);
        eventHandlerBuilder.Services.AddSingleton<ICallingServerEventSubscriber>(dispatcher);

        return eventHandlerBuilder;
    }
}