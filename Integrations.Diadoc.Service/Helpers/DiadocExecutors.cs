using Integrations.Diadoc.Domain.Models;
using Integrations.Diadoc.Domain.Models.Enums;
using Integrations.Diadoc.Domain.Models.Settings;
using Integrations.Diadoc.Infrastructure.SubServices.DocumentBuilders;
using Integrations.Diadoc.Infrastructure.SubServices.Pusher;
using Microsoft.Extensions.Options;

namespace Integrations.Diadoc.Service.Helpers;

public class DiadocExecutors
{
    private IBuildUserData _buildUserData;
    private IDiadocPusher _diadocPusher;
    private EmployeeSettings _employeeSettings;

    public DiadocExecutors(IBuildUserData buildUserData, IDiadocPusher diadocPusher, IOptions<CommonSettings> options)
    {
        this._buildUserData = buildUserData;
        this._diadocPusher = diadocPusher;
        this._employeeSettings = options.Value.EmployeeSettings.First(el=>el.Position == EmployeePosition.AccountManager);
    }
    public async Task SendingDocuments(RequestIdData data)
    {
        var messageToPost = await this._buildUserData.BuildMessageToPost(data);
        await this._diadocPusher.PushPostMessage(messageToPost, _employeeSettings);

    }

    public async Task SendingClients()
    {
    }
}
