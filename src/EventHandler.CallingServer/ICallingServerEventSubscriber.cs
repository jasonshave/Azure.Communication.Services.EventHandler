// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

public interface ICallingServerEventSubscriber
{
    event Func<IncomingCall, string?, ValueTask>? OnIncomingCall;
    event Func<CallConnectedEvent, string?, ValueTask>? OnCallConnected;
    event Func<CallDisconnectedEvent, string?, ValueTask>? OnCallDisconnected;
    event Func<AddParticipantSucceeded, string?, ValueTask>? OnAddParticipantSucceeded;
    event Func<AddParticipantFailed, string?, ValueTask>? OnAddParticipantFailed;
    event Func<CallTransferAccepted, string?, ValueTask>? OnCallTransferAccepted;
    event Func<CallTransferFailed, string?, ValueTask>? OnCallTransferFailed;
    event Func<RemoveParticipantSucceeded, string?, ValueTask>? OnRemoveParticipantSucceeded;
    event Func<RemoveParticipantFailed, string?, ValueTask>? OnRemoveParticipantFailed;
    event Func<ParticipantUpdated, string?, ValueTask>? OnParticipantUpdated;
}