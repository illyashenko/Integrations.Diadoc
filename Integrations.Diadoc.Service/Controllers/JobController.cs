using Integrations.Diadoc.Data.Monitoring.Enums;
using Integrations.Diadoc.Data.Monitoring.Models;
using Integrations.Diadoc.Data.Monitoring.Specifications.Filters;
using Integrations.Diadoc.Infrastructure.Settings;
using Integrations.Diadoc.Infrastructure.Stores;
using Integrations.Diadoc.Infrastructure.SubServices.DiadocService;
using Integrations.Diadoc.Infrastructure.SubServices.ExternalExchangeDocumentsService;
using MassTransitRMQExtensions.Attributes.JobAttributes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Integrations.Diadoc.Service.Controllers;

public class JobController
{
    private readonly MonitoringStore _store;
    private readonly DiadocExecutor _executor;
    private readonly JobSettings _settings;
    private readonly ExternalExchangeDocumentsService _externalExchangeDocumentsService;
    private DiadocSettings _diadocSettings;

    public JobController(MonitoringStore monitoringStore
        , DiadocExecutor executor
        , IOptions<JobSettings> options
        , ExternalExchangeDocumentsService externalExchangeDocumentsService
        , IOptions<DiadocSettings> diadocOptions)
    {
        this._store = monitoringStore;
        this._executor = executor;
        this._settings = options.Value;
        this._externalExchangeDocumentsService = externalExchangeDocumentsService;
        this._diadocSettings = diadocOptions.Value;
    }

    [RunJob("0 41 * * * ?")]
    public async Task ProcessSendDocumentsAndAcquireClientsJobs()
    {
        var filter = new JobFilter
        {
            DateFrom = DateTime.Now.AddDays(-30),
            Status = JobStatus.Prepared,
            ServerId = _settings.TargetServerId,
            OperationIds = new []
            {
                OperationId.SendDocuments,
                OperationId.SendClients
            }
        };

        await ProcessJobs(filter);
    }

    //[RunJob("0 03 * * * ?")]
    public async Task AddCheckAcquireClientsJob()
    {
        var job = new JobCandidate()
        {
            OperationId = OperationId.CheckClients,
            Status = JobStatus.Prepared,
            ServerId = _settings.TargetServerId,
            CreateDate = DateTime.Now,
            StartDate = DateTime.Now,
            Data = JsonConvert.SerializeObject(new RequestIdData()
            {
                RequestId = new Guid("F66856BB-A5DA-468B-B4AB-01306A223BD1")
            })
        };

        await this._store.AddJobAsync(job);
    }

    private async Task ProcessJobs(JobFilter filter)
    {
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
            await this._externalExchangeDocumentsService.UpdateExternalExchangeDocuments(jobs);
        }
    }
}
