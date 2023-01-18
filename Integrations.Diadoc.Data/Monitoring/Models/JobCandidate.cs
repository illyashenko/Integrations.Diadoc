using Integrations.Diadoc.Data.Monitoring.Enums;

namespace Integrations.Diadoc.Data.Monitoring.Models;

public class JobCandidate
{
    public OperationId OperationId { get; set; }
    public JobStatus Status { get; set; }
    public string? Data { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public int ServerId { get; set; }
}
