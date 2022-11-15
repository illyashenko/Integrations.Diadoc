using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integrations.Diadoc.Data.Apt.Entities;

public class ExternalExchangeDocuments
{
    public int Id { get; set; }
    public int DocumentTitleId { get; set; }
    public int DocumentTitleOwnerId { get; set; }
    public int Type { get; set; }
    public int Action { get; set; }
    public int Status { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public string? ProcessedBy { get; set; }
    public int? JobId { get; set; }
    public string? JobData { get; set; }
    public int? Mode { get; set; }
}

public class ExternalExchangeDocumentsConfiguration : IEntityTypeConfiguration<ExternalExchangeDocuments>
{
    public void Configure(EntityTypeBuilder<ExternalExchangeDocuments> builder)
    {
        builder.ToTable("ExternalExchangeDocuments");

        builder.HasKey(e => e.Id);
    }
}
