using Integrations.Diadoc.Data.Monitoring.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Models.MonitoringContext;

public class DiadocAcquireCounteragent
{
    public Guid RequestId { get; set; }
    public string? Inn { get; set; }
    public string? Kpp { get; set; }
    public string? OrgId { get; set; }
    public string? MessageToCouneragent { get; set; }
    public int? Organization { get; set; }
}
public class DiadocAcquireCounteragentConfiguration : IEntityTypeConfiguration<DiadocAcquireCounteragent>
{
    public void Configure(EntityTypeBuilder<DiadocAcquireCounteragent> builder) 
    {
        builder.ToTable("BufferDiadocAcquireCounteragent");
        builder.HasKey(p => p.RequestId);
    }
}
