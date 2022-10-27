namespace Integrations.Diadoc.Data.Monitoring.Enums;

public enum JobStatus
{
    Default = -3,
    Ignored = -2,
    Failed = -1,
    Prepared = 0,
    Running = 1,
    Processed = 2
}
