namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventPublisher<TPrimitive>
    where TPrimitive : IPrimitivePublisher
{
    void Publish(BinaryData data, string eventName, string contextId = default!);

    void Publish(string data, string eventName, string contextId = default!);
}