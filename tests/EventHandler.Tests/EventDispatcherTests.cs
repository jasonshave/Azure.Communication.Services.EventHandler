using AutoFixture;
using FluentAssertions;
using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;
using JasonShave.Azure.Communication.Service.EventHandler.CallingServer;

namespace EventHandler.Tests;

public class EventDispatcherTests
{
    [Fact(DisplayName = "Dispatch should invoke event handler")]
    public void Should_Invoke_Handler()
    {
        // arrange
        var testId = Guid.NewGuid().ToString();
        var fixture = new Fixture();
        var callConnectedEvent = fixture.Create<CallConnectedEvent>();
        var callDisconnectedEvent = fixture.Create<CallDisconnectedEvent>();
        var callConnectionStateChangedEvent = fixture.Create<CallConnectionStateChanged>();
        var incomingCall = fixture.Create<IncomingCall>();

        var subject = new CallingServerEventDispatcher();

        subject.OnCallConnected += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallConnectedEvent>();
            @event.Should().BeEquivalentTo(callConnectedEvent);
            contextId.Should().BeSameAs(testId);
            contextId.Should().NotBeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnCallDisconnected += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallDisconnectedEvent>();
            @event.Should().BeEquivalentTo(callDisconnectedEvent);
            contextId.Should().BeSameAs(testId);
            contextId.Should().NotBeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnCallConnectionStateChanged += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallConnectionStateChanged>();
            @event.Should().BeEquivalentTo(callConnectionStateChangedEvent);
            contextId.Should().BeSameAs(testId);
            contextId.Should().NotBeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnIncomingCall += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<IncomingCall>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        // act
        subject.Dispatch(callConnectedEvent, typeof(CallConnectedEvent), testId);
        subject.Dispatch(callDisconnectedEvent, typeof(CallDisconnectedEvent), testId);
        subject.Dispatch(callConnectionStateChangedEvent, typeof(CallConnectionStateChanged), testId);
        subject.Dispatch(incomingCall, typeof(IncomingCall));
    }

    [Fact(DisplayName = "Null context should invoke")]
    public void EventDispatcher_NullContext_ShouldInvoke()
    {
        // arrange
        var fixture = new Fixture();
        var incomingCall = fixture.Create<IncomingCall>();
        var subject = new CallingServerEventDispatcher();

        // act
        subject.OnIncomingCall += HandleIncomingCall;

        ValueTask HandleIncomingCall(IncomingCall @event, string? contextId)
        {
            @event.Should().NotBeNull();
            contextId.Should().BeNull();
            return ValueTask.CompletedTask;
        }

        // assert
        subject.Dispatch(incomingCall, typeof(IncomingCall));

        // clean up
        subject.OnIncomingCall -= HandleIncomingCall;
    }

    [Fact(DisplayName = "EventDispatcher with no subscriber should not throw nullref")]
    public void EventDispatcher_NullSubscriber_DoesNotThrow()
    {
        // arrange
        var fixture = new Fixture();
        var incomingCall = fixture.Create<IncomingCall>();
        var subject = new CallingServerEventDispatcher();

        // act
        subject.OnIncomingCall += HandleIncomingCall;
        
        ValueTask HandleIncomingCall(IncomingCall @event, string? contextId)
        {
            return ValueTask.CompletedTask;
        }

        // assert
        subject.Invoking(x => x.Dispatch(incomingCall, typeof(IncomingCall))).Should().NotThrow();

        // clean up
        subject.OnIncomingCall -= HandleIncomingCall;
    }
}