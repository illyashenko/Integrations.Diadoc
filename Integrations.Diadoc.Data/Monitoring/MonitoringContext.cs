using Data.Models.MonitoringContext;
using Microsoft.EntityFrameworkCore;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Monitoring;

public class MonitoringContext : AptContext<MonitoringContext>
{
    public DbSet<Jobs> Jobs { get; set; }
    public MonitoringContext(DbContextOptions<MonitoringContext> context) : base(context)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new JobsConfiguration());
    }
}
