// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.JobRouter.Sdk.Contracts.V2021_10_20_preview.Models
{
    public class WorkerSelector
    {
        public string Key { get; set; }

        public LabelOperator LabelOperator { get; set; }

        public object Value { get; set; }

        public double? TTLSeconds { get; set; }

        public WorkerSelectorState State { get; set; }

        public DateTimeOffset? ExpireTime { get; set; }
    }
}
