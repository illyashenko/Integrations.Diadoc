using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class Regions : LegacyTable
    {
        public string RegionName { get; set; }
    }

    public class RegionsConfiguration : LegacyTableConfiguration<Regions>
    {
        public override void Configure(EntityTypeBuilder<Regions> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("Regions");
            builder.Property(p => p.RegionName).HasColumnName("RegionName");
        }
    }
}
