using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class RegionsCode : LegacyTable
    {
        public int? CityId { get; set; }
        public int? CityOwnerId { get; set; }
        public int? RegionId { get; set; }
        public int? RegionOwnerId { get; set; }
        public int? RegionCode { get; set; }
    }

    public class RegionsCodeConfiguration : LegacyTableConfiguration<RegionsCode>
    {
        public override void Configure(EntityTypeBuilder<RegionsCode> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("RegionsCode");
            builder.Property(p => p.CityId).HasColumnName("Cities_id");
            builder.Property(p => p.CityOwnerId).HasColumnName("Cities_Owner_id");
            builder.Property(p => p.RegionId).HasColumnName("Region_id");
            builder.Property(p => p.RegionOwnerId).HasColumnName("Region_Owner_id");
            builder.Property(p => p.RegionCode).HasColumnName("RegionCode");
        }
    }
}
