using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Models.MonitoringContext;

public class DiadocSendingDocuments
{
    public int? DocumentId { get; set; }

    public int? DocumentOwnerId { get; set; }

    public string? FileName { get; set; }

    public byte[]? Content { get; set; }

    public Guid RequestId { get; set; }

    public bool? Bill { get; set; }
}

public class DiadocSendingDocumentsConfiguration : IEntityTypeConfiguration<DiadocSendingDocuments>
{
    public void Configure(EntityTypeBuilder<DiadocSendingDocuments> builder)
    {
        builder.ToTable("DiadocSendingDocuments");
        builder.HasKey(p => p.RequestId);
        builder.Property(p => p.DocumentId).HasColumnName("DocumentId");
        builder.Property(p => p.DocumentOwnerId).HasColumnName("DocumentOwnerId");
        builder.Property(p => p.FileName).HasColumnName("FileName");
        builder.Property(p => p.Content).HasColumnName("Content");
        builder.Property(p => p.RequestId).HasColumnName("RequestId");
        builder.Property(p => p.Bill).HasColumnName("Bill");
    }
}
