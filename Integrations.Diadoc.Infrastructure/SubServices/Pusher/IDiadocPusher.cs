using Diadoc.Api.Proto.Employees;
using Diadoc.Api.Proto.Events;
using Integrations.Diadoc.Domain.Models.Enums;
using Integrations.Diadoc.Domain.Models.Settings;

namespace Integrations.Diadoc.Infrastructure.SubServices.Pusher;

public interface IDiadocPusher
{
    Task<Message> PushPostMessage(MessageToPost messageToPost, EmployeeSettings employee);
}
