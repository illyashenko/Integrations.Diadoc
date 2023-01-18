using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Integrations.Diadoc.Data.Monitoring.Entities;

public class BufferDiadocCounteragentsOrganizationBoxes
{
    public int Id { get; set; }
    public string OrgId { get; set; }
    public string? BoxId { get; set; }
    public string? Title { get; set; }
    public bool? EncryptedDocumentsAllowed { get; set; }
    public int? InvoiceFormatVersion { get; set; }
}

public class BufferDiadocCounteragentsOrganizationBoxesConfiguratin : IEntityTypeConfiguration<BufferDiadocCounteragentsOrganizationBoxes>
{

    public void Configure(EntityTypeBuilder<BufferDiadocCounteragentsOrganizationBoxes> builder)
    {
        builder.ToTable("BufferDiadocCounteragentsOrganizationBoxes");
        builder.HasKey(p => p.Id);
    }
}
