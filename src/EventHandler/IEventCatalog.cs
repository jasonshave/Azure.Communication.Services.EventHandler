namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventCatalog<TPrimitive>
{
    IEventCatalog<TPrimitive> Register<TEvent>();

    Type? Get(string eventName);
}