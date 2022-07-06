namespace JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

public record RemoveParticipantSucceeded(string CallConnectionId, string ServerCallId, string CorrelationId);