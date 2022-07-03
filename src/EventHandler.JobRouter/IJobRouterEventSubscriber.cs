using JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events;

namespace JasonShave.Azure.Communication.Service.EventHandler.JobRouter;

public interface IJobRouterEventSubscriber
{
    event Func<RouterJobCancelled, string, Task>? OnJobCancelled;
    event Func<RouterJobClassificationFailed, string, Task>? OnJobClassificationFailed;
    event Func<RouterJobClassified, string, Task>? OnJobClassified;
    event Func<RouterJobClosed, string, Task>? OnJobClosed;
    event Func<RouterJobCompleted, string, Task>? OnJobCompleted;
    event Func<RouterJobExceptionTriggered, string, Task>? OnJobExceptionTriggered;
    event Func<RouterJobQueued, string, Task>? OnJobQueued;
    event Func<RouterJobReceived, string, Task>? OnJobReceived;
    event Func<RouterJobWorkerSelectorsExpired, string, Task>? OnJobWorkerSelectorsExpired;
    event Func<RouterWorkerDeregistered, string, Task>? OnWorkerDeregistered;
    event Func<RouterWorkerRegistered, string, Task>? OnWorkerRegistered;
    event Func<RouterWorkerOfferAccepted, string, Task>? OnWorkerOfferAccepted;
    event Func<RouterWorkerOfferDeclined, string, Task>? OnWorkerOfferDeclined;
    event Func<RouterWorkerOfferExpired, string, Task>? OnWorkerOfferExpired;
    event Func<RouterWorkerOfferIssued, string, Task>? OnWorkerOfferIssued;
    event Func<RouterWorkerOfferRevoked, string, Task>? OnWorkerOfferRevoked;
}