// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.Common;

namespace JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

[Serializable]
public class IncomingCall
{
    public CommunicationIdentifier To { get; set; } = default!;

    public CommunicationIdentifier From { get; set; } = default!;

    public bool HasIncomingVideo { get; set; }

    public string CallerDisplayName { get; set; } = default!;

    public string IncomingCallContext { get; set; } = default!;

    public string CorrelationId { get; set; } = default!;
}