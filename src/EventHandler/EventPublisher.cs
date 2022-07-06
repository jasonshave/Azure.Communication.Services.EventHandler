using Microsoft.Extensions.Logging;

namespace JasonShave.Azure.Communication.Service.EventHandler;

public class EventPublisher<TPrimitive> : IEventPublisher<TPrimitive>
    where TPrimitive : IPrimitivePublisher
{
    private readonly ILogger<EventPublisher<TPrimitive>> _logger;
    private readonly IEventCatalog<TPrimitive> _eventCatalog;
    private readonly IEventDispatcher<TPrimitive> _eventDispatcher;
    private readonly IEventConverter _eventConverter;

    public EventPublisher(
        ILogger<EventPublisher<TPrimitive>> logger,
        IEventCatalog<TPrimitive> eventCatalog,
        IEventDispatcher<TPrimitive> eventDispatcher,
        IEventConverter eventConverter)
    {
        _logger = logger;
        _eventCatalog = eventCatalog;
        _eventDispatcher = eventDispatcher;
        _eventConverter = eventConverter;
    }

    public void Publish(BinaryData data, string eventName, string contextId)
    {
        Publish(data.ToString(), eventName, contextId);
    }

    public void Publish(string data, string eventName, string contextId)
    {
        _logger.LogDebug($"Interaction event publisher handling: {eventName}");
        var eventType = _eventCatalog.Get(eventName);
        if (eventType is null) return;

        var convertedEvent = _eventConverter.Convert(data, eventType);

        if (convertedEvent is null) return;
        _eventDispatcher.Dispatch(convertedEvent, eventType, contextId);
    }
}