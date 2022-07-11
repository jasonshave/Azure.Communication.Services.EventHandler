// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;

namespace JasonShave.Azure.Communication.Service.EventHandler;

internal class JsonEventConverter : IEventConverter
{
    private readonly JsonSerializerOptions _serializerOptions;

    internal JsonEventConverter(JsonSerializerOptions serializerOptions)
    {
        _serializerOptions = serializerOptions;
    }

    public object? Convert(string eventPayload, Type eventType)
    {
        var result = JsonSerializer.Deserialize(eventPayload, eventType, _serializerOptions);
        return result;
    }
}