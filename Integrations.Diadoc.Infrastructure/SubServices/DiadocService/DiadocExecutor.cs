using Integrations.Diadoc.Data.Monitoring.Enums;

namespace Integrations.Diadoc.Infrastructure.SubServices.DiadocService;

public class DiadocExecutor
{
    private readonly DiadocService _diadocService;
    
    public DiadocExecutor(DiadocService service)
    {
        this._diadocService = service;
    }
    public async Task ExecuteAsync(OperationId type, params object[] jobParams)
    {
        var typeController = typeof(DiadocService);
        var methodInfo = typeController.GetMethod(type.ToString());
        await (Task)methodInfo?.Invoke(_diadocService, jobParams)!;
    }
}
