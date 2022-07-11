// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using AutoFixture;
using FluentAssertions;
using System.Text.Json;

namespace JasonShave.Azure.Communication.Service.EventHandler.Tests;

public class EventConverterTests
{
    [Fact(DisplayName = "JSON converter should deserialize")]
    public void Should_Deserialize()
    {
        // arrange
        var fixture = new Fixture();
        var startEvent = fixture.Create<StartEvent>();
        var startEventJson = JsonSerializer.Serialize(startEvent);

        var subject = new JsonEventConverter(new JsonSerializerOptions());

        // act
        var result = subject.Convert(startEventJson, typeof(StartEvent));

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(startEvent);
    }
}