﻿using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;
using Integrations.Diadoc.Infrastructure.Settings;

namespace Integrations.Diadoc.Infrastructure.SubServices.Pushers;

public interface IDiadocPusher
{
    Task<Message> PushPostMessageAsync(MessageToPost messageToPost, EmployeeSettings employee);
    Task<Counteragent> PushAcquireCounteragentAsync(AcquireCounteragentRequest acquireCounteragentRequest, EmployeeSettings employee, string? orgId);
}
