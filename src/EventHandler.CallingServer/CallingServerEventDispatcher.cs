using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

namespace JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

internal class CallingServerEventDispatcher : IEventDispatcher<CallingServer>, ICallingServerEventSubscriber
{
    public event Func<IncomingCall, string?, ValueTask>? OnIncomingCall;
    public event Func<CallConnectedEvent, string?, ValueTask>? OnCallConnected;
    public event Func<CallDisconnectedEvent, string?, ValueTask>? OnCallDisconnected;
    public event Func<CallConnectionStateChanged, string?, ValueTask>? OnCallConnectionStateChanged;

    private readonly Dictionary<Type, Func<object, string?, ValueTask>> _eventDictionary = new();

    public CallingServerEventDispatcher()
    {
        _eventDictionary = new Dictionary<Type, Func<object, string?, ValueTask>>
        {
            [typeof(IncomingCall)] = async (@event, contextId) =>
            {
                if (OnIncomingCall is null) return;
                await OnIncomingCall.Invoke((IncomingCall)@event, contextId);
            },
            [typeof(CallConnectedEvent)] = async (@event, contextId) =>
            {
                if (OnCallConnected is null) return;
                await OnCallConnected.Invoke((CallConnectedEvent)@event, contextId);
            },
            [typeof(CallDisconnectedEvent)] = async (@event, contextId) =>
            {
                if (OnCallDisconnected is null) return;
                await OnCallDisconnected.Invoke((CallDisconnectedEvent)@event, contextId);
            },
            [typeof(CallConnectionStateChanged)] = async (@event, contextId) =>
            {
                if (OnCallConnectionStateChanged is null) return;
                await OnCallConnectionStateChanged.Invoke((CallConnectionStateChanged)@event, contextId);
            }
        };
    }

    public void Dispatch(object @event, Type eventType, string contextId = default!)
    {
        _eventDictionary[eventType](@event, contextId);
    }
}