using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class Addresses : LegacyTable
    {
        public string PostCode { get; set; }

        public string Address { get; set; }

        public int CitiesId { get; set; }

        public int CitiesOwnerId { get; set; }
    }

    public class AddressesConfiguration : LegacyTableConfiguration<Addresses>
    {
        public override void Configure(EntityTypeBuilder<Addresses> builder)
        {
            builder.ToTable("Addresses");
            builder.Property(p => p.Address).HasColumnName("Address");
            builder.Property(p => p.PostCode).HasColumnName("PostCode");
            builder.Property(p => p.CitiesId).HasColumnName("Cities_Id");
            builder.Property(p => p.CitiesOwnerId).HasColumnName("Cities_Owner_Id");
        }
    }
}
