using JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events;

namespace JasonShave.Azure.Communication.Service.EventHandler.JobRouter;

internal class JobRouterEventDispatcher : IEventDispatcher<JobRouter>, IJobRouterEventSubscriber
{
    public event Func<RouterJobCancelled, string, Task>? OnJobCancelled;
    public event Func<RouterJobClassificationFailed, string, Task>? OnJobClassificationFailed;
    public event Func<RouterJobClassified, string, Task>? OnJobClassified;
    public event Func<RouterJobClosed, string, Task>? OnJobClosed;
    public event Func<RouterJobCompleted, string, Task>? OnJobCompleted;
    public event Func<RouterJobExceptionTriggered, string, Task>? OnJobExceptionTriggered;
    public event Func<RouterJobQueued, string, Task>? OnJobQueued;
    public event Func<RouterJobReceived, string, Task>? OnJobReceived;
    public event Func<RouterJobWorkerSelectorsExpired, string, Task>? OnJobWorkerSelectorsExpired;
    public event Func<RouterWorkerDeregistered, string, Task>? OnWorkerDeregistered;
    public event Func<RouterWorkerRegistered, string, Task>? OnWorkerRegistered;
    public event Func<RouterWorkerOfferAccepted, string, Task>? OnWorkerOfferAccepted;
    public event Func<RouterWorkerOfferDeclined, string, Task>? OnWorkerOfferDeclined;
    public event Func<RouterWorkerOfferExpired, string, Task>? OnWorkerOfferExpired;
    public event Func<RouterWorkerOfferIssued, string, Task>? OnWorkerOfferIssued;
    public event Func<RouterWorkerOfferRevoked, string, Task>? OnWorkerOfferRevoked;

    private readonly Dictionary<Type, Func<object, string, Task>> _eventDictionary = new();

    public JobRouterEventDispatcher()
    {
        _eventDictionary = new Dictionary<Type, Func<object, string, Task>>
        {
            [typeof(RouterJobCancelled)] = async (@event, contextId) => await OnJobCancelled?.Invoke((RouterJobCancelled)@event, contextId),
            [typeof(RouterJobClassificationFailed)] = async (@event, contextId) => await OnJobClassificationFailed?.Invoke((RouterJobClassificationFailed)@event, contextId),
            [typeof(RouterJobClassified)] = async (@event, contextId) => await OnJobClassified?.Invoke((RouterJobClassified)@event, contextId),
            [typeof(RouterJobClosed)] = async (@event, contextId) => await OnJobClosed?.Invoke((RouterJobClosed)@event, contextId),
            [typeof(RouterJobCompleted)] = async (@event, contextId) => await OnJobCompleted?.Invoke((RouterJobCompleted)@event, contextId),
            [typeof(RouterJobExceptionTriggered)] = async (@event, contextId) => await OnJobExceptionTriggered?.Invoke((RouterJobExceptionTriggered)@event, contextId),
            [typeof(RouterJobQueued)] = async (@event, contextId) => await OnJobQueued?.Invoke((RouterJobQueued)@event, contextId),
            [typeof(RouterJobReceived)] = async (@event, contextId) => await OnJobReceived?.Invoke((RouterJobReceived)@event, contextId),
            [typeof(RouterJobWorkerSelectorsExpired)] = async (@event, contextId) => await OnJobWorkerSelectorsExpired?.Invoke((RouterJobWorkerSelectorsExpired)@event, contextId),
            [typeof(RouterWorkerDeregistered)] = async (@event, contextId) => await OnWorkerDeregistered?.Invoke((RouterWorkerDeregistered)@event, contextId),
            [typeof(RouterWorkerRegistered)] = async (@event, contextId) => await OnWorkerRegistered?.Invoke((RouterWorkerRegistered)@event, contextId),
            [typeof(RouterWorkerOfferAccepted)] = async (@event, contextId) => await OnWorkerOfferAccepted?.Invoke((RouterWorkerOfferAccepted)@event, contextId),
            [typeof(RouterWorkerOfferDeclined)] = async (@event, contextId) => await OnWorkerOfferDeclined?.Invoke((RouterWorkerOfferDeclined)@event, contextId),
            [typeof(RouterWorkerOfferExpired)] = async (@event, contextId) => await OnWorkerOfferExpired?.Invoke((RouterWorkerOfferExpired)@event, contextId),
            [typeof(RouterWorkerOfferIssued)] = async (@event, contextId) => await OnWorkerOfferIssued?.Invoke((RouterWorkerOfferIssued)@event, contextId),
            [typeof(RouterWorkerOfferRevoked)] = async (@event, contextId) => await OnWorkerOfferRevoked?.Invoke((RouterWorkerOfferRevoked)@event, contextId),

        };
    }

    public void Dispatch(object @event, Type eventType, string contextId = default)
    {
        _eventDictionary[eventType](@event, contextId);
    }
}