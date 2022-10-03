using Data.Models.MonitoringContext;
using Microsoft.EntityFrameworkCore;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Monitoring;

public class MonitoringContext : AptContext<MonitoringContext>
{
    public DbSet<Job> Jobs { get; set; }
    public DbSet<DiadocSendingDocuments> DiadocSendingDocuments { get; set; }
    public MonitoringContext(DbContextOptions<MonitoringContext> context) : base(context)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new JobsConfiguration());
        modelBuilder.ApplyConfiguration(new DiadocSendingDocumentsConfiguration());
    }
}
