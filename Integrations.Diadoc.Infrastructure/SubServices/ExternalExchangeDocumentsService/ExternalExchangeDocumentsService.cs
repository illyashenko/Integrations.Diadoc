using Integrations.Diadoc.Data.Monitoring.Models;
using Integrations.Diadoc.Infrastructure.Stores;

namespace Integrations.Diadoc.Infrastructure.SubServices.ExternalExchangeDocumentsService;

public class ExternalExchangeDocumentsService
{
    private DiadocStore _store;

    public ExternalExchangeDocumentsService(DiadocStore store)
    {
        this._store = store;
    }

    public async Task UpdateExternalExchangeDocuments(IEnumerable<JobModel> jobs)
    {
        await this._store.UpdateExternalExchangeDocuments(jobs);
    } 
}
