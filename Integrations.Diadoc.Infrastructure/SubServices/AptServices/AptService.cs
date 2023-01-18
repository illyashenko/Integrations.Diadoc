using Diadoc.Api.Proto;
using Integrations.Diadoc.Data.Apt.Models;
using Integrations.Diadoc.Infrastructure.Stores;

namespace Integrations.Diadoc.Infrastructure.SubServices.AptServices;

public class AptService : IAptService
{
    private AptStore _aptStore;

    public AptService(AptStore aptStore)
    {
        this._aptStore = aptStore;
    }

    public async Task DiadocSaveCounteragent(Counteragent counteragent)
    {
        var candidate = new CounteragentCandidate
        {
            OrgIg = counteragent.Organization.OrgId,
            BoxId = counteragent.Organization.Boxes.FirstOrDefault()?.BoxId,
            Title = counteragent.Organization.ShortName,
            Inn = counteragent.Organization.Inn,
            Kpp = counteragent.Organization.Kpp
        };
        await this._aptStore.SaveCounteragent(candidate);
    }
}
