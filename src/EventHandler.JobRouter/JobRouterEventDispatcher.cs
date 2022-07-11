// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events;

namespace JasonShave.Azure.Communication.Service.EventHandler.JobRouter;

internal class JobRouterEventDispatcher : IEventDispatcher<Router>, IJobRouterEventSubscriber
{
    public event Func<RouterJobCancelled, string?, ValueTask>? OnJobCancelled;
    public event Func<RouterJobClassificationFailed, string?, ValueTask>? OnJobClassificationFailed;
    public event Func<RouterJobClassified, string?, ValueTask>? OnJobClassified;
    public event Func<RouterJobClosed, string?, ValueTask>? OnJobClosed;
    public event Func<RouterJobCompleted, string?, ValueTask>? OnJobCompleted;
    public event Func<RouterJobExceptionTriggered, string?, ValueTask>? OnJobExceptionTriggered;
    public event Func<RouterJobQueued, string?, ValueTask>? OnJobQueued;
    public event Func<RouterJobReceived, string?, ValueTask>? OnJobReceived;
    public event Func<RouterJobWorkerSelectorsExpired, string?, ValueTask>? OnJobWorkerSelectorsExpired;
    public event Func<RouterWorkerDeregistered, string?, ValueTask>? OnWorkerDeregistered;
    public event Func<RouterWorkerRegistered, string?, ValueTask>? OnWorkerRegistered;
    public event Func<RouterWorkerOfferAccepted, string?, ValueTask>? OnWorkerOfferAccepted;
    public event Func<RouterWorkerOfferDeclined, string?, ValueTask>? OnWorkerOfferDeclined;
    public event Func<RouterWorkerOfferExpired, string?, ValueTask>? OnWorkerOfferExpired;
    public event Func<RouterWorkerOfferIssued, string?, ValueTask>? OnWorkerOfferIssued;
    public event Func<RouterWorkerOfferRevoked, string?, ValueTask>? OnWorkerOfferRevoked;

    private readonly Dictionary<Type, Func<object, string?, ValueTask>> _eventDictionary;

    public JobRouterEventDispatcher()
    {
        _eventDictionary = new Dictionary<Type, Func<object, string?, ValueTask>>
        {
            [typeof(RouterJobCancelled)] = async (@event, contextId) =>
            {
                if (OnJobCancelled is null) return;
                await OnJobCancelled.Invoke((RouterJobCancelled)@event, contextId);
            },
            [typeof(RouterJobClassificationFailed)] = async (@event, contextId) =>
            {
                if (OnJobClassificationFailed is null) return;
                await OnJobClassificationFailed.Invoke((RouterJobClassificationFailed)@event, contextId);
            },
            [typeof(RouterJobClassified)] = async (@event, contextId) =>
            {
                if (OnJobClassified is null) return;
                await OnJobClassified.Invoke((RouterJobClassified)@event, contextId);
            },
            [typeof(RouterJobClosed)] = async (@event, contextId) =>
            {
                if (OnJobClosed is null) return;
                await OnJobClosed.Invoke((RouterJobClosed)@event, contextId);
            },
            [typeof(RouterJobCompleted)] = async (@event, contextId) =>
            {
                if (OnJobCompleted is null) return;
                await OnJobCompleted.Invoke((RouterJobCompleted)@event, contextId);
            },
            [typeof(RouterJobExceptionTriggered)] = async (@event, contextId) =>
            {
                if (OnJobExceptionTriggered is null) return;
                await OnJobExceptionTriggered.Invoke((RouterJobExceptionTriggered)@event, contextId);
            },
            [typeof(RouterJobQueued)] = async (@event, contextId) =>
            {
                if (OnJobQueued is null) return;
                await OnJobQueued.Invoke((RouterJobQueued)@event, contextId);
            },
            [typeof(RouterJobReceived)] = async (@event, contextId) =>
            {
                if (OnJobReceived is null) return;
                await OnJobReceived.Invoke((RouterJobReceived)@event, contextId);
            },
            [typeof(RouterJobWorkerSelectorsExpired)] = async (@event, contextId) =>
            {
                if (OnJobWorkerSelectorsExpired is null) return;
                await OnJobWorkerSelectorsExpired.Invoke((RouterJobWorkerSelectorsExpired)@event, contextId);
            },
            [typeof(RouterWorkerDeregistered)] = async (@event, contextId) =>
            {
                if (OnWorkerDeregistered is null) return;
                await OnWorkerDeregistered.Invoke((RouterWorkerDeregistered)@event, contextId);
            },
            [typeof(RouterWorkerRegistered)] = async (@event, contextId) =>
            {
                if (OnWorkerRegistered is null) return;
                await OnWorkerRegistered.Invoke((RouterWorkerRegistered)@event, contextId);
            },
            [typeof(RouterWorkerOfferAccepted)] = async (@event, contextId) =>
            {
                if (OnWorkerOfferAccepted is null) return;
                await OnWorkerOfferAccepted.Invoke((RouterWorkerOfferAccepted)@event, contextId);
            },
            [typeof(RouterWorkerOfferDeclined)] = async (@event, contextId) =>
            {
                if (OnWorkerOfferDeclined is null) return;
                await OnWorkerOfferDeclined.Invoke((RouterWorkerOfferDeclined)@event, contextId);
            },
            [typeof(RouterWorkerOfferExpired)] = async (@event, contextId) =>
            {
                if (OnWorkerOfferExpired is null) return;
                await OnWorkerOfferExpired.Invoke((RouterWorkerOfferExpired)@event, contextId);
            },
            [typeof(RouterWorkerOfferIssued)] = async (@event, contextId) =>
            {
                if (OnWorkerOfferIssued is null) return;
                await OnWorkerOfferIssued.Invoke((RouterWorkerOfferIssued)@event, contextId);
            },
            [typeof(RouterWorkerOfferRevoked)] = async (@event, contextId) =>
            {
                if (OnWorkerOfferRevoked is null) return;
                await OnWorkerOfferRevoked.Invoke((RouterWorkerOfferRevoked)@event, contextId);
            },

        };
    }

    public void Dispatch(object @event, Type eventType, string contextId = default!)
    {
        _eventDictionary[eventType](@event, contextId);
    }
}