// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Events
{
    [Serializable]

    public class RouterJobCancelled
    {
        public string? Note { get; set; }

        public string DispositionCode { get; set; }

        public string JobId { get; set; }

        public string ChannelReference { get; set; }

        public string ChannelId { get; set; }

        public Dictionary<string, object>? Labels { get; set; }

        public Dictionary<string, object> Tags { get; set; }

        public string? QueueId { get; set; }
    }
}