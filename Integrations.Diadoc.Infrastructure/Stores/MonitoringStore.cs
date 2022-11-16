using Data.Models.MonitoringContext;
using Integrations.Diadoc.Data.Monitoring;
using Integrations.Diadoc.Data.Monitoring.Models;
using Integrations.Diadoc.Data.Monitoring.Specifications.Filters;
using Integrations.Diadoc.Data.Monitoring.Specifications.ForJobs;
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
            .Select(j => new JobModel
            {
                Id = j.Id,
                OperationId = j.OperationId,
                Status = j.Status,
                Data = JsonConvert.DeserializeObject<RequestIdData>(j.Data)
            })
            .OrderBy(j=>j.Id).Take(50).ToListAsync();
    }

    public async Task UpdateJobsStatus(IEnumerable<JobModel> jobs)
    {
        if (jobs.Any())
        {
            var idS = jobs.Select(el=>el.Id).ToArray();

            var jobsDb = await this.Context.Jobs.Where(el => idS.Contains(el.Id)).ToListAsync();

            foreach (var jobModel in jobs)
            {
                var jobDb = jobsDb.Single(el=>el.Id == jobModel.Id);
                
                jobDb.Status = jobModel.Status;
                jobDb.ExecuteCode = jobModel.ExecuteCode;
                jobDb.ExecuteMessage = jobModel.ExecuteMessage;
                jobDb.ProcessedDate = DateTime.Now;
            }
            
            await this.Context.SaveChangesAsync();
        }
    }
}
