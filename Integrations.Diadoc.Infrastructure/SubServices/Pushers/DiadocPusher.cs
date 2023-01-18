using Diadoc.Api;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;
using Integrations.Diadoc.Infrastructure.Settings;
using Integrations.Diadoc.Infrastructure.SubServices.TokenService;

namespace Integrations.Diadoc.Infrastructure.SubServices.Pushers;

public class DiadocPusher : IDiadocPusher
{
    private IDiadocApi api { get; set; }
    private IAuthToken authToken { get; set; }

    public DiadocPusher(IDiadocApi api, IAuthToken authToken)
    {
        this.api = api;
        this.authToken = authToken;
    }

    public async Task<Message> PushPostMessageAsync(MessageToPost messageToPost, EmployeeSettings employee)
    {
        var operationId = Guid.NewGuid().ToString();
        var token = await this.authToken.GetAccessToken(employee);
        return await api.PostMessageAsync(token, messageToPost, operationId);
    }
    public async Task<Counteragent> PushAcquireCounteragentAsync(AcquireCounteragentRequest acquireCounteragentRequest, EmployeeSettings employee, string? orgId)
    {
        var token = await this.authToken.GetAccessToken(employee);
        var asyncMethodResult = await this.api.AcquireCounteragentAsync(token, orgId, acquireCounteragentRequest);
        var acquireCounteragentResult = await this.api.WaitAcquireCounteragentResultAsync(token, asyncMethodResult?.TaskId);
        return await this.api.GetCounteragentAsync(token, orgId, acquireCounteragentResult?.OrgId);
    }
}
