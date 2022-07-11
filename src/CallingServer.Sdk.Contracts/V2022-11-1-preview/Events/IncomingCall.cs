// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.V2022_11_1_preview.Events;

[Serializable]
public class IncomingCall
{
    public Identifier To { get; set; } = default!;

    public Identifier From { get; set; } = default!;

    public bool HasIncomingVideo { get; set; }

    public string CallerDisplayName { get; set; } = default!;

    public string IncomingCallContext { get; set; } = default!;

    public string CorrelationId { get; set; } = default!;
}

[Serializable]
public class Identifier
{
    public string RawId { get; set; } = default!;

    public PhoneNumber PhoneNumber { get; set; } = default!;
}

[Serializable]
public class PhoneNumber
{
    public string Value { get; set; } = default!;
}