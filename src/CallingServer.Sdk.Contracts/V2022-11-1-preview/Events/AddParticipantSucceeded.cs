namespace JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

public record AddParticipantSucceeded(string CallConnectionId, string ServerCallId, string CorrelationId);