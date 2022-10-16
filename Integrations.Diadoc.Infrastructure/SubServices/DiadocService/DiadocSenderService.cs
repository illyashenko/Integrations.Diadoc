using Integrations.Diadoc.Domain.Models;
using Integrations.Diadoc.Domain.Models.Enums;
using Integrations.Diadoc.Domain.Models.Settings;
using Integrations.Diadoc.Infrastructure.SubServices.DocumentBuilders;
using Integrations.Diadoc.Infrastructure.SubServices.Pusher;
using Microsoft.Extensions.Options;

namespace Integrations.Diadoc.Infrastructure.SubServices.DiadocService;

public class DiadocSenderService
{
    private readonly IBuildUserData _buildUserData;
    private readonly IDiadocPusher _diadocPusher;
    private readonly EmployeeSettings _employeeSettings;

    public DiadocSenderService(IBuildUserData buildUserData, IDiadocPusher diadocPusher, IOptions<CommonSettings> options)
    {
        this._buildUserData = buildUserData;
        this._diadocPusher = diadocPusher;
        this._employeeSettings = options.Value.EmployeeSettings.First(el => el.Position == EmployeePosition.AccountManager);
    }
    public async Task SendDocuments(RequestIdData data)
    {
        var messageToPost = await this._buildUserData.BuildMessageToPost(data);
        await this._diadocPusher.PushPostMessage(messageToPost, _employeeSettings);
    }

    public async Task SendClients()
    {
    }
}
