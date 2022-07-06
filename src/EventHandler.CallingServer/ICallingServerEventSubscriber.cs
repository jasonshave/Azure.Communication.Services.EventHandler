using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

public interface ICallingServerEventSubscriber
{
    event Func<IncomingCall, string?, ValueTask>? OnIncomingCall;
    event Func<CallConnectedEvent, string?, ValueTask>? OnCallConnected;
    event Func<CallDisconnectedEvent, string?, ValueTask>? OnCallDisconnected;
    event Func<CallConnectionStateChanged, string?, ValueTask>? OnCallConnectionStateChanged;
}