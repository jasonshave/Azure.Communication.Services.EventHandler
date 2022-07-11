// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.EventHandler;

public interface IEventCatalog<TPrimitive>
    where TPrimitive : IPrimitive
{
    IEventCatalog<TPrimitive> Register<TEvent>();

    Type? Get(string eventName);
}