using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class ServiceType : LegacyTable
    {
        public string TypeName { get; set; }
        public string? TypeNamePrint { get; set; }
        public string? Measure { get; set; }
        public BillSum? BillSum { get; set; }
    }

    public class ServiceTypeConfiguration : LegacyTableConfiguration<ServiceType>
    {
        public override void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("ServiceType");
            builder.Property(p => p.TypeName).HasColumnName("TypeName");
            builder.Property(p => p.TypeNamePrint).HasColumnName("TypeNamePrint");
            builder.Property(p => p.Measure).HasColumnName("Measure");
        }
    }
}