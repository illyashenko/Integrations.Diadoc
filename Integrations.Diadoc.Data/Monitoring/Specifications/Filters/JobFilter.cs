using Integrations.Diadoc.Data.Monitoring.Enums;

namespace Integrations.Diadoc.Data.Monitoring.Specifications.Filters;

public class JobFilter
{
    public JobStatus Status { get; set; } = JobStatus.Default;
    public OperationId OperationId { get; set; } = OperationId.Default;
    public DateTime DateFrom { get; set; } = DateTime.MinValue;
    public int ServerId { get; set; } = 0;
    public IEnumerable<OperationId>? OperationIds { get; set; }
}
