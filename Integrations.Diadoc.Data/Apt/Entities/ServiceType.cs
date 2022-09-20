using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class ServiceType
    {
        [Key]
        public int ServiceTypeId { get; set; }
        public int ServiceTypeOwnerId { get; set; } 
        public string TypeName { get; set; }
        public string TypeNamePrint { get; set; }
        public string Measure { get; set; }
        public BillSums BillSums { get; set; }
        
        public string Res { get; set; } 
    }

    public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.ToTable("ServiceType");
            builder.Property(p => p.TypeName).HasColumnName("TypeName");
            builder.Property(p => p.TypeNamePrint).HasColumnName("TypeNamePrint");
            builder.Property(p => p.Measure).HasColumnName("Measure");
            builder.Property(p => p.ServiceTypeId).HasColumnName("id");
            builder.Property(p => p.ServiceTypeOwnerId).HasColumnName("owner_id");
        }
    }
}