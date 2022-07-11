// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Models
{
    [Serializable]
    public class QueueInfo
    {
        public string Id { get; set; } = default!;

        public string Name { get; set; } = default!;

        public Dictionary<string, string>? Labels { get; set; }
    }
}
