using System.Text.Json;

namespace JasonShave.Azure.Communication.Service.EventHandler;

public class JsonEventConverter : IEventConverter
{
    private readonly JsonSerializerOptions _serializerOptions;

    public JsonEventConverter(JsonSerializerOptions serializerOptions)
    {
        _serializerOptions = serializerOptions;
    }

    public object? Convert(string eventPayload, Type eventType)
    {
        var result = JsonSerializer.Deserialize(eventPayload, eventType, _serializerOptions);
        return result;
    }
}