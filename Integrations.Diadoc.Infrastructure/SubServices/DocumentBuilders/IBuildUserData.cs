using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;
using Integrations.Diadoc.Data.Monitoring.Models;
using Integrations.Diadoc.Infrastructure.Settings;

namespace Integrations.Diadoc.Infrastructure.SubServices.DocumentBuilders;

public interface IBuildUserData
{
    Task<(MessageToPost, EmployeeSettings)> BuildMessageToPost(RequestIdData requestId);
    Task<(AcquireCounteragentRequest acquireCounteragentRequest, EmployeeSettings EmployeeSettings, string? OrgId)> BuildAcquireCounteragentRequest(RequestIdData requestId);
}
