// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using JasonShave.Azure.Communication.Service.EventHandler;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;
using Microsoft.Extensions.Logging;
using Moq;

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

        var mockEventCatalog = new Mock<IEventCatalog<Calling>>();
        var mockEventConverter = new Mock<IEventConverter>();
        var mockEventDispatcher = new Mock<IEventDispatcher<Calling>>();
        var mockLogger = new Mock<ILogger<EventPublisher<Calling>>>();

        mockEventCatalog.Setup(c => c.Get(It.IsAny<string>())).Returns(typeof(StartEvent));
        mockEventConverter.Setup(c => c.Convert(It.IsAny<string>(), It.IsAny<Type>())).Returns(startEvent);
        mockEventDispatcher.Setup(d => d.Dispatch(It.IsAny<object>(), It.IsAny<Type>(), It.IsAny<string>()));

        var subject = new EventPublisher<Calling>(
            mockLogger.Object,
            mockEventCatalog.Object,
            mockEventDispatcher.Object,
            mockEventConverter.Object);

        // act
        subject.Publish(startEventJson, nameof(StartEvent), "test");

        // assert
        mockEventCatalog.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
        mockEventConverter.Verify(x => x.Convert(It.IsAny<string>(), It.IsAny<Type>()), Times.Once);
        mockEventDispatcher.Verify(x => x.Dispatch(It.IsAny<object>(), It.IsAny<Type>(), It.IsAny<string>()), Times.Once);
    }

    [Fact(DisplayName = "No event in catalog throws")]
    public void EventPublisher_MissingEvent_Throws()
    {
        // arrange
        var mockEventCatalog = new Mock<IEventCatalog<Calling>>();
        var mockEventConverter = new Mock<IEventConverter>();
        var mockEventDispatcher = new Mock<IEventDispatcher<Calling>>();
        var mockLogger = new Mock<ILogger<EventPublisher<Calling>>>();

        mockEventCatalog.Setup(c => c.Get(It.IsAny<string>())).Returns(It.IsAny<Type>());

        var subject = new EventPublisher<Calling>(
            mockLogger.Object,
            mockEventCatalog.Object,
            mockEventDispatcher.Object,
            mockEventConverter.Object);

        // act/assert
        subject.Invoking(x => x.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Should().Throw<InvalidOperationException>();
    }

    [Fact(DisplayName = "Invalid conversion throws")]
    public void EventPublisher_NullConversion_Throws()
    {
        // arrange
        var mockEventCatalog = new Mock<IEventCatalog<Calling>>();
        var mockEventConverter = new Mock<IEventConverter>();
        var mockEventDispatcher = new Mock<IEventDispatcher<Calling>>();
        var mockLogger = new Mock<ILogger<EventPublisher<Calling>>>();

        mockEventCatalog.Setup(c => c.Get(It.IsAny<string>())).Returns(typeof(StartEvent));
        mockEventConverter.Setup(c => c.Convert(It.IsAny<string>(), It.IsAny<Type>())).Returns(It.IsAny<object>());

        var subject = new EventPublisher<Calling>(
            mockLogger.Object,
            mockEventCatalog.Object,
            mockEventDispatcher.Object,
            mockEventConverter.Object);

        // act/assert
        subject.Invoking(x => x.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Should().Throw<InvalidOperationException>();
    }
}