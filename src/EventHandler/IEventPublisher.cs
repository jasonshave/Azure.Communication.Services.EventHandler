namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventPublisher<TPrimitive>
    where TPrimitive : IPrimitive
{
    void Publish(BinaryData data, string eventName, string contextId = default!);
}