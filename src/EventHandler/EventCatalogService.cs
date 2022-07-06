﻿namespace JasonShave.Azure.Communication.Service.EventHandler;

public class EventCatalogService<TPrimitive> : IEventCatalog<TPrimitive>
    where TPrimitive : IPrimitivePublisher
{
    private const string _eventPrefix = "Microsoft.Communication.";
    private readonly Dictionary<string, Type> _eventCatalog = new();

    public IEventCatalog<TPrimitive> Register<TEvent>()
    {
        _eventCatalog.Add(typeof(TEvent).Name, typeof(TEvent));
        return this;
    }

    public Type? Get(string eventName)
    {
        _eventCatalog.TryGetValue(eventName.Replace(_eventPrefix, ""), out var eventType);
        return eventType;
    }
}