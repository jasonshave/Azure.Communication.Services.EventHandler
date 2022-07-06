namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventCatalog<TPrimitive>
    where TPrimitive : IPrimitivePublisher
{
    IEventCatalog<TPrimitive> Register<TEvent>();

    Type? Get(string eventName);
}