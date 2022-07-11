// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events;

namespace JasonShave.Azure.Communication.Service.EventHandler.JobRouter;

public interface IJobRouterEventSubscriber
{
    event Func<RouterJobCancelled, string?, ValueTask>? OnJobCancelled;
    event Func<RouterJobClassificationFailed, string?, ValueTask>? OnJobClassificationFailed;
    event Func<RouterJobClassified, string?, ValueTask>? OnJobClassified;
    event Func<RouterJobClosed, string?, ValueTask>? OnJobClosed;
    event Func<RouterJobCompleted, string?, ValueTask>? OnJobCompleted;
    event Func<RouterJobExceptionTriggered, string?, ValueTask>? OnJobExceptionTriggered;
    event Func<RouterJobQueued, string?, ValueTask>? OnJobQueued;
    event Func<RouterJobReceived, string?, ValueTask>? OnJobReceived;
    event Func<RouterJobWorkerSelectorsExpired, string?, ValueTask>? OnJobWorkerSelectorsExpired;
    event Func<RouterWorkerDeregistered, string?, ValueTask>? OnWorkerDeregistered;
    event Func<RouterWorkerRegistered, string?, ValueTask>? OnWorkerRegistered;
    event Func<RouterWorkerOfferAccepted, string?, ValueTask>? OnWorkerOfferAccepted;
    event Func<RouterWorkerOfferDeclined, string?, ValueTask>? OnWorkerOfferDeclined;
    event Func<RouterWorkerOfferExpired, string?, ValueTask>? OnWorkerOfferExpired;
    event Func<RouterWorkerOfferIssued, string?, ValueTask>? OnWorkerOfferIssued;
    event Func<RouterWorkerOfferRevoked, string?, ValueTask>? OnWorkerOfferRevoked;
}