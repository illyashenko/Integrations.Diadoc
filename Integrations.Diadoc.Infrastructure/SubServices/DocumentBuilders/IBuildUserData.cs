using Diadoc.Api.Proto.Events;
using Integrations.Diadoc.Data.Monitoring.Models;

namespace Integrations.Diadoc.Infrastructure.SubServices.DocumentBuilders;

public interface IBuildUserData
{
    Task<MessageToPost> BuildMessageToPost(RequestIdData requestId);
    Task BuildTest(RequestIdData requestId);
}
