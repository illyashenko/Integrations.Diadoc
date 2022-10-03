using Integrations.Diadoc.Data.Monitoring.Specifications.Filters;
using Integrations.Diadoc.Domain.Models;
using Integrations.Diadoc.Domain.Models.Enums;
using Integrations.Diadoc.Infrastructure.Stores;
using Integrations.Diadoc.Service.Helpers;
using MassTransitRMQExtensions.Attributes.JobAttributes;
using Newtonsoft.Json;

namespace Integrations.Diadoc.Service.Controllers;

public class JobController
{
    private readonly MonitoringStore _store;
    private readonly DiadocExecutors _executors;

    public JobController(MonitoringStore monitoringStore, DiadocExecutors executors)
    {
        this._store = monitoringStore;
        this._executors = executors;

    }
    
    [RunJob("1/30 * * * * ?")]
    public async Task ProcessJobs()
    {
        var filter = new JobFilter
        {
            DateFrom = DateTime.Now.AddDays(-30),
            Status = JobStatus.Prepared,
            ServerId = ServerId.Sending
        };

        var jobs = await this._store.GetJobs(filter);

        if (jobs.Any())
        {
            foreach (var job in jobs)
            {
                try
                {
                    await OperationExecutor(job);
                    job.Status = JobStatus.Processed;
                }
                catch (Exception exception)
                {
                    job.Status = JobStatus.Failed;
                    job.ExecuteMessage =JsonConvert.SerializeObject(new {message = exception.Message});
                }   
            }
            await this._store.UpdateJobsStatus(jobs);
        }
        
    }

    private async Task OperationExecutor(JobModel job)
    {
        switch (job.OperationId)
        {
            case OperationId.SendingDocuments:
                await _executors.SendingDocuments(job.Data);
                break;
            case OperationId.SendingClients:
                await _executors.SendingClients();
                break;
            case OperationId.Default:
                break;
        }
    }
}
