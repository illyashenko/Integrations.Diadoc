using Integrations.Diadoc.Data.Monitoring.Enums;

namespace Integrations.Diadoc.Infrastructure.SubServices.DiadocService;

public class DiadocExecutor
{
    private readonly DiadocSenderService _diadocSenderService;
    
    public DiadocExecutor(DiadocSenderService senderService)
    {
        this._diadocSenderService = senderService;
    }
    public async Task ExecuteAsync(OperationId type, params object[] jobParams)
    {
        var typeController = typeof(DiadocSenderService);
        var methodInfo = typeController.GetMethod(type.ToString());
        await (Task)methodInfo?.Invoke(_diadocSenderService, jobParams)!;
    }
}
