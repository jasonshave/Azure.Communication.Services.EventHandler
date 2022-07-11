﻿// Copyright (c) 2022 Jason Shave. All rights reserved.
// Licensed under the MIT License.

namespace JasonShave.Azure.Communication.Service.EventHandler;

internal interface IEventDispatcher<TPrimitive>
    where TPrimitive : IPrimitive
{
    void Dispatch(object @event, Type eventType, string contextId = default!);
}