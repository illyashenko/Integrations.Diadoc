using Integrations.Diadoc.Data.Monitoring.Enums;

namespace Integrations.Diadoc.Data.Monitoring.Models;

public class JobModel
{
    public int Id { get; set; }
    public OperationId OperationId { get; set; }
    public JobStatus Status { get; set; }
    public RequestIdData? Data { get; set; }
    public ExecuteCodes ExecuteCode { get; set; }
    public string? ExecuteMessage { get; set; }
}
