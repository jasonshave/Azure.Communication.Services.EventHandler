using JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events;
using Microsoft.Extensions.DependencyInjection;

namespace JasonShave.Azure.Communication.Service.EventHandler.JobRouter;

public static class EventHandlerBuilderExtensions
{
    public static EventHandlerBuilder AddJobRouterEventHandling(this EventHandlerBuilder eventHandlerBuilder)
    {
        eventHandlerBuilder.Services.AddSingleton<IEventPublisher<JobRouter>, EventPublisher<JobRouter>>();

        var catalog = new EventCatalogService<JobRouter>();

        catalog
            .Register<RouterJobCancelled>()
            .Register<RouterJobClassificationFailed>()
            .Register<RouterJobClassified>()
            .Register<RouterJobClosed>()
            .Register<RouterJobCompleted>()
            .Register<RouterJobExceptionTriggered>()
            .Register<RouterJobQueued>()
            .Register<RouterJobReceived>()
            .Register<RouterJobWorkerSelectorsExpired>()
            .Register<RouterWorkerDeregistered>()
            .Register<RouterWorkerRegistered>()
            .Register<RouterWorkerOfferAccepted>()
            .Register<RouterWorkerOfferDeclined>()
            .Register<RouterWorkerOfferExpired>()
            .Register<RouterWorkerOfferIssued>()
            .Register<RouterWorkerOfferRevoked>();

        eventHandlerBuilder.Services.AddSingleton<IEventCatalog<JobRouter>>(catalog);

        var dispatcher = new JobRouterEventDispatcher();
        eventHandlerBuilder.Services.AddSingleton<IEventDispatcher<JobRouter>>(dispatcher);
        eventHandlerBuilder.Services.AddSingleton<IJobRouterEventSubscriber>(dispatcher);

        return eventHandlerBuilder;
    }
}