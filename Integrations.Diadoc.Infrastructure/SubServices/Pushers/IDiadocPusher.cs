using Diadoc.Api.Proto.Events;
using Integrations.Diadoc.Infrastructure.Settings;

namespace Integrations.Diadoc.Infrastructure.SubServices.Pushers;

public interface IDiadocPusher
{
    Task<Message> PushPostMessage(MessageToPost messageToPost, EmployeeSettings employee);
}
