using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace JasonShave.Azure.Communication.Service.EventHandler;

public static class ServiceCollectionExtensions
{
    public static EventHandlerBuilder AddEventHandlerServices(this IServiceCollection services)
    {
        services.AddSingleton<IEventConverter>(new JsonEventConverter(new JsonSerializerOptions()));

        return new(services);
    }

    public static EventHandlerBuilder AddEventHandlerServices(this IServiceCollection services, Action<JsonSerializerOptions> jsonSerializerOptions)
    {
        JsonSerializerOptions options = new();
        jsonSerializerOptions(options);
        services.AddSingleton<IEventConverter>(new JsonEventConverter(options));

        return new(services);
    }
}