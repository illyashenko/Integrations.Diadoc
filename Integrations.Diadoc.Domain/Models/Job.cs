using Integrations.Diadoc.Domain.Models.Enums;

namespace Integrations.Diadoc.Domain.Models;

public class Job
{
    public int Id { get; set; }
    public OperationId OperationId { get; set; }
    public JobStatus Status { get; set; }
    public RequestIdData Data { get; set; }
    public ExecuteCodes ExecuteCode { get; set; }
    public string ExecuteMessage { get; set; }
}
