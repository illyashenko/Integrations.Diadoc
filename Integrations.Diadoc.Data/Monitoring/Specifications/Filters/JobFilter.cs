﻿using Integrations.Diadoc.Domain.Models.Enums;

namespace Integrations.Diadoc.Data.Monitoring.Specifications.Filters;

public class JobFilter
{
    public JobStatus Status { get; set; } = JobStatus.Default;
    public OperationId OperationId { get; set; } = OperationId.Default;
    public DateTime DateFrom { get; set; } = DateTime.MinValue;
    public ServerId ServerId { get; set; } = 0; 
}
