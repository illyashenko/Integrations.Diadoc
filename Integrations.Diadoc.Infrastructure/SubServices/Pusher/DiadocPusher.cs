using Diadoc.Api;
using Diadoc.Api.Proto.Events;
using Integrations.Diadoc.Domain.Models.Settings;
using Integrations.Diadoc.Infrastructure.SubServices.TokenService;

namespace Integrations.Diadoc.Infrastructure.SubServices.Pusher;

public class DiadocPusher : IDiadocPusher
{
    private IDiadocApi api { get; set; }
    private IAuthToken authToken { get; set; }

    public DiadocPusher(IDiadocApi api, IAuthToken authToken)
    {
        this.api = api;
        this.authToken = authToken;
    }

    public async Task<Message> PushPostMessage(MessageToPost messageToPost, EmployeeSettings employee)
    {
        var operationId = Guid.NewGuid().ToString();
        var token = await this.authToken.GetAccessToken(employee);
        return await api.PostMessageAsync(token, messageToPost, operationId);
    }
}
