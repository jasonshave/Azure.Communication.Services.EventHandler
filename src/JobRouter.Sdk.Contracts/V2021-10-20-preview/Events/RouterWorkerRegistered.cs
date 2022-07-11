// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

using JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Models;

namespace JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events
{
    [Serializable]
    public class RouterWorkerRegistered
    {
        public string WorkerId { get; init; }

        public int TotalCapacity { get; set; }

        public ICollection<QueueInfo> QueueAssignments { get; set; }

        public Dictionary<string, object>? Labels { get; set; }

        public ICollection<ChannelConfiguration> ChannelConfigurations { get; set; }
    }
}
