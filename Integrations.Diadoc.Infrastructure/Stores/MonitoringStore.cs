using Data.Models.MonitoringContext;
using Integrations.Diadoc.Data.Monitoring;
using Integrations.Diadoc.Data.Monitoring.Specifications.Filters;
using Integrations.Diadoc.Data.Monitoring.Specifications.ForJobs;
using Integrations.Diadoc.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Integrations.Diadoc.Infrastructure.Stores;

public class MonitoringStore
{
    private MonitoringContext db { get; }

    public MonitoringStore(MonitoringContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<Job>> GetJobs(JobFilter filter)
    {
        var listJobs = await this.db.Jobs.Where(filter.ToExpression())
            .Select(j => new Job()
            {
                Id = j.Id,
                OperationId = j.OperationId,
                Status = j.Status,
                Data = JsonConvert.DeserializeObject<RequestIdData>(j.Data)
            }).ToListAsync();
        return listJobs;
    }

    public async Task UpdateJobsStatus(IEnumerable<Job> jobs)
    {
        List<Jobs> jobsListForUpdate = new();

        foreach (var jobModel in jobs)
        {
            var jobDb = await this.db.Jobs.FindAsync(jobModel.Id);
                
            jobDb.Status = jobModel.Status;
            jobDb.ExecuteCode = jobModel.ExecuteCode;
            jobDb.ExecuteMessage = jobModel.ExecuteMessage;
            jobDb.ProcessedDate = DateTime.Now;
                 
            jobsListForUpdate.Add(jobDb);
        }

        this.db.Jobs.UpdateRange(jobsListForUpdate);
        await this.db.SaveChangesAsync();
    }
    public async Task GetJob()
    {
        var job = await this.db.Jobs.FirstOrDefaultAsync();
    }
}
