using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;
using JasonShave.Azure.Communication.Service.EventHandler.Abstractions.Interfaces;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

internal class CallingServerEventDispatcher : IEventDispatcher<CallingServer>, ICallingServerEventSubscriber
{
    public event Func<IncomingCall, string, Task>? OnIncomingCall;
    public event Func<CallConnectedEvent, string, Task>? OnCallConnected;
    public event Func<CallDisconnectedEvent, string, Task>? OnCallDisconnected;
    public event Func<CallConnectionStateChanged, string, Task>? OnCallConnectionStateChanged;

    private readonly Dictionary<Type, Func<object, string, Task>> _eventDictionary = new();

    public CallingServerEventDispatcher()
    {
        _eventDictionary = new Dictionary<Type, Func<object, string, Task>>
        {
            [typeof(IncomingCall)] = async (@event, contextId) => await OnIncomingCall?.Invoke((IncomingCall)@event, contextId),
            [typeof(CallConnectedEvent)] = async (@event, contextId) => await OnCallConnected?.Invoke((CallConnectedEvent)@event, contextId),
            [typeof(CallDisconnectedEvent)] = async (@event, contextId) => await OnCallDisconnected?.Invoke((CallDisconnectedEvent)@event, contextId),
            [typeof(CallConnectionStateChanged)] = async (@event, contextId) => await OnCallConnectionStateChanged?.Invoke((CallConnectionStateChanged)@event, contextId),
        };
    }

    public void Dispatch(object @event, Type eventType, string contextId = default!)
    {
        _eventDictionary[eventType](@event, contextId);
    }
}