using Diadoc.Api.Proto.Events;
using Integrations.Diadoc.Domain.Models;

namespace Integrations.Diadoc.Infrastructure.SubServices.DocumentBuilders;

public interface IBuildUserData
{
    Task<MessageToPost> BuildMessageToPost(RequestIdData requestId);
}
