using FluentAssertions;
using JasonShave.Azure.Communication.Service.EventHandler.Abstractions;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

namespace EventHandler.Tests
{
    public class EventCatalogTests
    {
        [Fact(DisplayName = "Registering event type returns same type by name")]
        public void Register_Type_Returns_Same_Type()
        {
            // arrange
            var subject = new EventCatalogService<CallingServer>();

            // act
            subject
                .Register<StartEvent>()
                .Register<StopEvent>();

            var startEvent = subject.Get(nameof(StartEvent));
            var stopEvent = subject.Get(nameof(StopEvent));

            // assert
            startEvent.Should().NotBeNull();
            startEvent.Should().BeAssignableTo<StartEvent>();

            stopEvent.Should().NotBeNull();
            stopEvent.Should().BeAssignableTo<StopEvent>();
        }
    }
}