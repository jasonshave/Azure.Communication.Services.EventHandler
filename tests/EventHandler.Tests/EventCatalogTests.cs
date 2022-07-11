// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using FluentAssertions;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

namespace JasonShave.Azure.Communication.Service.EventHandler.Tests
{
    public class EventCatalogTests
    {
        [Fact(DisplayName = "Registering event type returns same type by name")]
        public void Register_Type_Returns_Same_Type()
        {
            // arrange
            var subject = new EventCatalogService<Calling>();

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