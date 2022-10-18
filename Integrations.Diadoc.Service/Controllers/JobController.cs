using Integrations.Diadoc.Data.Monitoring.Specifications.Filters;
using Integrations.Diadoc.Domain.Models;
using Integrations.Diadoc.Domain.Models.Enums;
using Integrations.Diadoc.Domain.Models.Settings;
using Integrations.Diadoc.Infrastructure;
using Integrations.Diadoc.Infrastructure.Stores;
using Integrations.Diadoc.Infrastructure.SubServices.DiadocService;
using MassTransitRMQExtensions.Attributes.JobAttributes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Integrations.Diadoc.Service.Controllers;

public class JobController
{
    private readonly MonitoringStore _store;
    private readonly DiadocExecutor _executor;
    private JobSettings _settings;

        public JobController(MonitoringStore monitoringStore, DiadocExecutor executor, IOptions<JobSettings> options)
    {
        this._store = monitoringStore;
        this._executor = executor;
        this._settings = options.Value;
    }

    [RunJob("1/30 * * * * ?")]
    public async Task ProcessJobs()
    {
        var filter = new JobFilter
        {
            DateFrom = DateTime.Now.AddDays(-30),
            Status = JobStatus.Prepared,
            ServerId = _settings.TargetServerId
        };

        var jobs = await this._store.GetJobs(filter);
        
        if (jobs.Any())
        {
            foreach (var job in jobs)
            {
                try
                {
                    await _executor.ExecuteAsync(job.OperationId, job.Data);
                    job.Status = JobStatus.Processed;
                    job.ExecuteCode = ExecuteCodes.Ok;
                }
                catch (Exception exception)
                {
                    job.Status = JobStatus.Failed;
                    job.ExecuteMessage = JsonConvert.SerializeObject(new { message = exception.Message });
                }
            }
            await this._store.UpdateJobsStatus(jobs);
        }
    }
}
