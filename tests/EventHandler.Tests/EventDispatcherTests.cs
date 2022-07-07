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
        var addParticipantSucceededEvent = fixture.Create<AddParticipantSucceeded>();
        var addParticipantFailedEvent = fixture.Create<AddParticipantFailed>();
        var callTransferAcceptedEvent = fixture.Create<CallTransferAccepted>();
        var callTransferFailedEvent = fixture.Create<CallTransferFailed>();
        var removeParticipantSucceededEvent = fixture.Create<RemoveParticipantSucceeded>();
        var removeParticipantFailedEvent = fixture.Create<RemoveParticipantFailed>();
        var participantUpdatedEvent = fixture.Create<ParticipantUpdated>();

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

        subject.OnAddParticipantSucceeded += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<AddParticipantSucceeded>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnAddParticipantFailed += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<AddParticipantFailed>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnCallTransferAccepted += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallTransferAccepted>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnCallTransferFailed += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<CallTransferFailed>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnRemoveParticipantSucceeded += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<RemoveParticipantSucceeded>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnRemoveParticipantFailed += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<RemoveParticipantFailed>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        subject.OnParticipantUpdated += (@event, contextId) =>
        {
            @event.Should().NotBeNull();
            @event.Should().BeOfType<ParticipantUpdated>();
            contextId.Should().BeNullOrEmpty();
            return ValueTask.CompletedTask;
        };

        // act
        subject.Dispatch(callConnectedEvent, typeof(CallConnectedEvent), testId);
        subject.Dispatch(callDisconnectedEvent, typeof(CallDisconnectedEvent), testId);
        subject.Dispatch(callConnectionStateChangedEvent, typeof(CallConnectionStateChanged), testId);
        subject.Dispatch(incomingCall, typeof(IncomingCall));
        subject.Dispatch(addParticipantSucceededEvent, typeof(AddParticipantSucceeded));
        subject.Dispatch(addParticipantFailedEvent, typeof(AddParticipantFailed));
        subject.Dispatch(removeParticipantSucceededEvent, typeof(RemoveParticipantSucceeded));
        subject.Dispatch(removeParticipantFailedEvent, typeof(RemoveParticipantFailed));
        subject.Dispatch(callTransferAcceptedEvent, typeof(CallTransferAccepted));
        subject.Dispatch(callTransferFailedEvent, typeof(CallTransferFailed));
        subject.Dispatch(participantUpdatedEvent, typeof(ParticipantUpdated));
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