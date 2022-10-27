using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class BillSend : LegacyTable
    {
        public int BillId { get; set; }
        public int BillOwnerId { get; set; }
        public int? FileExt { get; set; } 
    }
    public class BillSendConfiguration : LegacyTableConfiguration<BillSend>
    {
        public override void Configure(EntityTypeBuilder<BillSend> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("BillSend");
            builder.Property(p => p.BillId).HasColumnName("bill_id");
            builder.Property(p => p.BillOwnerId).HasColumnName("bill_owner_id");
            builder.Property(p => p.FileExt).HasColumnName("file_type");
        }
    }
}
