﻿using Data.Models.MonitoringContext;
using Integrations.Diadoc.Data.Monitoring;
using Integrations.Diadoc.Data.Monitoring.Specifications.Filters;
using Integrations.Diadoc.Data.Monitoring.Specifications.ForJobs;
using Integrations.Diadoc.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Integrations.Diadoc.Infrastructure.Stores;

public class MonitoringStore
{
    private MonitoringContext Context { get; }

    public MonitoringStore(MonitoringContext context)
    {
        this.Context = context;
    }

    public async Task<IEnumerable<JobModel>> GetJobs(JobFilter filter)
    {
        return await this.Context.Jobs.Where(filter.ToExpression())
            .Select(j => new JobModel()
            {
                Id = j.Id,
                OperationId = j.OperationId,
                Status = j.Status,
                Data = JsonConvert.DeserializeObject<RequestIdData>(j.Data)
            }).Take(100).ToListAsync();
    }

    public async Task UpdateJobsStatus(IEnumerable<JobModel> jobs)
    {
        List<Job> jobsListForUpdate = new();

        foreach (var jobModel in jobs)
        {
            var jobDb = await this.Context.Jobs.SingleAsync(el=>el.Id == jobModel.Id);
                
            jobDb.Status = jobModel.Status;
            jobDb.ExecuteCode = jobModel.ExecuteCode;
            jobDb.ExecuteMessage = jobModel.ExecuteMessage;
            jobDb.ProcessedDate = DateTime.Now;
                 
            jobsListForUpdate.Add(jobDb);
        }
        this.Context.Jobs.UpdateRange(jobsListForUpdate);
        await this.Context.SaveChangesAsync();
    }
}
