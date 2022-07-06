namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventConverter
{
    object? Convert(string stringPayload, Type eventType);
}