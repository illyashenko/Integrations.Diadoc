using Integrations.Diadoc.Data.Monitoring.Models;
using Integrations.Diadoc.Infrastructure.SubServices.AptServices;
using Integrations.Diadoc.Infrastructure.SubServices.DocumentBuilders;
using Integrations.Diadoc.Infrastructure.SubServices.Pushers;

namespace Integrations.Diadoc.Infrastructure.SubServices.DiadocService;

public class DiadocService
{
    private readonly IBuildUserData _buildUserData;
    private readonly IDiadocPusher _diadocPusher;
    private readonly IAptService _aptService;

    public DiadocService(IBuildUserData buildUserData, IDiadocPusher diadocPusher, IAptService aptService)
    {
        this._buildUserData = buildUserData;
        this._diadocPusher = diadocPusher;
        this._aptService = aptService;
    }
    public async Task SendDocuments(RequestIdData data)
    {
        var userData = await this._buildUserData.BuildMessageToPost(data);
        await this._diadocPusher.PushPostMessageAsync(userData.Item1, userData.Item2);
    }

    public async Task SendClients(RequestIdData data)
    {
        var acquireData = await _buildUserData.BuildAcquireCounteragentRequest(data);
        var counteragent = await this._diadocPusher.PushAcquireCounteragentAsync(acquireData.acquireCounteragentRequest, acquireData.EmployeeSettings, acquireData.OrgId);
        await this._aptService.DiadocSaveCounteragent(counteragent);

    }
}
