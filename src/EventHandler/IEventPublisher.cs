// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventPublisher<TPrimitive>
    where TPrimitive : IPrimitive
{
    void Publish(string data, string eventName, string contextId = default!);
}