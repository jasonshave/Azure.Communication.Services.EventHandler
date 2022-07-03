using AutoFixture;
using JasonShave.Azure.Communication.Service.EventHandler;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;

namespace EventHandler.Tests;

public class CallingServerEventPublisherTests
{
    [Fact(DisplayName = "Interaction publisher works")]
    public void Should_Send()
    {
        // arrange
        var fixture = new Fixture();
        var startEvent = fixture.Create<StartEvent>();
        var startEventJson = JsonSerializer.Serialize(startEvent);

        var mockEventCatalog = new Mock<IEventCatalog<CallingServer>>();
        var mockEventConverter = new Mock<IEventConverter>();
        var mockEventDispatcher = new Mock<IEventDispatcher<CallingServer>>();
        var mockLogger = new Mock<ILogger<EventPublisher<CallingServer>>>();

        mockEventCatalog.Setup(c => c.Get(It.IsAny<string>())).Returns(typeof(StartEvent));
        mockEventConverter.Setup(c => c.Convert(It.IsAny<BinaryData>(), It.IsAny<Type>())).Returns(startEvent);
        mockEventDispatcher.Setup(d => d.Dispatch(It.IsAny<object>(), It.IsAny<Type>(), It.IsAny<string>()));

        var subject = new EventPublisher<CallingServer>(
            mockLogger.Object,
            mockEventCatalog.Object,
            mockEventDispatcher.Object,
            mockEventConverter.Object);

        // act
        subject.Publish(new BinaryData(startEventJson), nameof(StartEvent), "test");

        // assert
        mockEventCatalog.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
        mockEventConverter.Verify(x => x.Convert(It.IsAny<BinaryData>(), It.IsAny<Type>()), Times.Once);
        mockEventDispatcher.Verify(x => x.Dispatch(It.IsAny<object>(), It.IsAny<Type>(), It.IsAny<string>()), Times.Once);
    }
}