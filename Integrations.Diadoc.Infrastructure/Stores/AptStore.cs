using Integrations.Diadoc.Data.Apt;
using Integrations.Diadoc.Data.Apt.Entities;
using Integrations.Diadoc.Data.Apt.Models;
using Microsoft.EntityFrameworkCore;

namespace Integrations.Diadoc.Infrastructure.Stores;

public class AptStore
{
    private AptContext _aptContext;

    public AptStore(AptContext context)
    {
        this._aptContext = context;
    }

    public async Task SaveCounteragent(CounteragentCandidate candidate)
    {
        await AddOrganizationsBoxId(candidate);
        
        var crm = await this._aptContext.CrmClients
            .Where(el => el.Clients != null && el.Clients.INN == candidate.InnKpp)
            .FirstOrDefaultAsync();

        if (crm is not null)
        {
            crm.OrgId = candidate.OrgIg;
        }

        await this._aptContext.SaveChangesAsync();
    }
    private async Task AddOrganizationsBoxId(CounteragentCandidate candidate)
    {
        var boxId = await this._aptContext.DiadocOrganizationsBoxId
            .Where(el => el.BoxId == candidate.BoxId && el.OrgId == candidate.OrgIg)
            .FirstOrDefaultAsync();
        
        if (boxId is null)
        {
            var newBoxId = new DiadocOrganizationsBoxId
            {
                BoxId = candidate.BoxId,
                OrgId = candidate.OrgIg,
                Title = candidate.Title
            };
            await this._aptContext.DiadocOrganizationsBoxId.AddAsync(newBoxId);
            await this._aptContext.SaveChangesAsync();
        }
    }
}
