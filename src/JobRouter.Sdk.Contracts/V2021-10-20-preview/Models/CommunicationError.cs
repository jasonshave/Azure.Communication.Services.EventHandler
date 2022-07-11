// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Models
{
    [Serializable]
    public class CommunicationError
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public string? Target { get; set; }

        public CommunicationError? InnerError { get; set; }

        public IEnumerable<CommunicationError>? Details { get; set; }
    }
}