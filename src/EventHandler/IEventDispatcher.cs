namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventDispatcher<TPrimitive>
    where TPrimitive : IPrimitivePublisher
{
    void Dispatch(object @event, Type eventType, string contextId = default!);
}