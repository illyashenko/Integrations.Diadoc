using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class ClientsInfo : LegacyTable
    {
        public string? FullCompanyName { get; set; }
    }

    public class ClientsInfoConfiguration : LegacyTableConfiguration<ClientsInfo>
    {
        public override void Configure(EntityTypeBuilder<ClientsInfo> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("ClientsInfo");
            builder.Property(p => p.FullCompanyName).HasColumnName("Full_Company_name");
        }
    }
}
