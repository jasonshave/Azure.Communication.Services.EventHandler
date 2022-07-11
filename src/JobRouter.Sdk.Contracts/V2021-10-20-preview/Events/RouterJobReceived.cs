// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Models;

namespace JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events
{
    [Serializable]
    public class RouterJobReceived
    {
        public string JobId { get; set; }

        public string ChannelReference { get; set; }

        public string JobStatus { get; set; }

        public string ChannelId { get; set; }

        public string? ClassificationPolicyId { get; set; }

        public string? QueueId { get; set; }

        public List<WorkerSelector>? RequestedWorkerSelectors { get; set; } = new();

        public Dictionary<string, object> Labels { get; set; }

        public Dictionary<string, object> Tags { get; set; }

        public int? Priority { get; set; }
    }
}
