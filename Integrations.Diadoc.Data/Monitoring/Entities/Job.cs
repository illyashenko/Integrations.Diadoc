using Integrations.Diadoc.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Models.MonitoringContext
{
    public class Job
    {
        public int Id { get; set; }
        public OperationId OperationId { get; set; }
        public JobStatus Status { get; set; }
        public string? Data { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public ServerId ServerId { get; set; }
        public ExecuteCodes? ExecuteCode { get; set; }
        public string? ExecuteMessage { get; set; }
        public int? AttemptIndex { get; set; }
    }
    public class JobsConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.OperationId).HasColumnName("OperationId");
            builder.Property(p => p.Status).HasColumnName("Status");
            builder.Property(p => p.Data).HasColumnName("Data");
            builder.Property(p => p.CreateDate).HasColumnName("CreateDate");
            builder.Property(p => p.StartDate).HasColumnName("StartDate");
            builder.Property(p => p.ProcessedDate).HasColumnName("ProcessedDate");
            builder.Property(p => p.ServerId).HasColumnName("ServerId");
            builder.Property(p => p.ExecuteCode).HasColumnName("ExecuteCode");
            builder.Property(p => p.ExecuteMessage).HasColumnName("ExecuteMessage");
            builder.Property(p => p.AttemptIndex).HasColumnName("AttemptIndex");
        }
    }
}