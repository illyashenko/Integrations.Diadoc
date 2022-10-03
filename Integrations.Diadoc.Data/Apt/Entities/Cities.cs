using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class Cities : LegacyTable
    {
        public int RegionId { get; set; }
        public int RegionOwnerId { get; set; }
        public string CityName { get; set; }
    }
    public class CitiesConfiguration : LegacyTableConfiguration<Cities>
    {
        public override void Configure(EntityTypeBuilder<Cities> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("Cities");
            builder.Property(p => p.RegionId).HasColumnName("Region_ID");
            builder.Property(p => p.RegionOwnerId).HasColumnName("Region_Owner_ID");
            builder.Property(p => p.CityName).HasColumnName("CityName");
        }
    }
}
