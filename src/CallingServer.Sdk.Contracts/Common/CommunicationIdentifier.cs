// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using Azure.Communication;

namespace JasonShave.Azure.Communication.Service.CallingServer.Sdk.Contracts.Common;

public class CommunicationIdentifier
{
    public string RawId { get; set; }

    public CommunicationIdentifierKind Kind { get; set; }

    public CommunicationUserIdentifier CommunicationUser { get; set; }

    public PhoneNumberIdentifier PhoneNumber { get; set; }

    public MicrosoftTeamsUserIdentifier MicrosoftTeamsUser { get; set; }
}