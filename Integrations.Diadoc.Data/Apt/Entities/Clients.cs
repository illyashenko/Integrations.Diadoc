using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class Clients : LegacyTable
    { 
        public int ClientId { get; set; }
        public int ClientOwnerId { get; set; }
        public string CompanyName { get; set; }
        public string INN { get; set; }
        public int PhisAddressId { get; set; }
        public int PhisAddressOwnerId { get; set; }
        public int JurAddressId { get; set; }
        public int JurAddressOwnerId { get; set; }
        
        public Contracts Contracts { get; set; }
    }

    public class ClientsConfiguration : LegacyTableConfiguration<Clients>
    {
        public override void Configure(EntityTypeBuilder<Clients> builder)
        {
            builder.ToTable("Clients");
            builder.Property(p => p.ClientId).HasColumnName("ID");
            builder.Property(p => p.ClientOwnerId).HasColumnName("Owner_ID");
            builder.Property(p => p.CompanyName).HasColumnName("Company_name");
            builder.Property(p => p.INN).HasColumnName("INN");
            builder.Property(p => p.PhisAddressId).HasColumnName("Phis_Address_id");
            builder.Property(p => p.PhisAddressOwnerId).HasColumnName("Phis_Address_Owner_id");
            builder.Property(p => p.JurAddressId).HasColumnName("Jur_Address_id");
            builder.Property(p => p.JurAddressOwnerId).HasColumnName("Jur_Address_Owner_id");
        }
    }
}
