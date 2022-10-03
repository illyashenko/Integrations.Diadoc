using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class Contracts : LegacyTable
    {
        public string? DogovorNumber { get; set; }
        public int ClientId { get; set; }
        public int ClientOwnerId { get; set; }
        public int? DocumentId { get; set; }
        public int? DocumentOwnerId { get; set; }
        public string? NumberContract { get; set; }
        public TitleString? TitleString { get; set; }
        public CrmClients? CrmClientsRef { get; set; }
        public Clients? ClientsRef { get; set; }
        public CrmContracts? CrmContractsRef { get; set; }
        
        public virtual ICollection<TitleString>? TitleStringRef { get; set; }
    }

    public class ContractsConfiguration : LegacyTableConfiguration<Contracts>
    {
        public override void Configure(EntityTypeBuilder<Contracts> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("Contracts");
            builder.Property(p => p.DogovorNumber).HasColumnName("contract_num");
            builder.Property(p => p.ClientId).HasColumnName("client_id");
            builder.Property(p => p.ClientOwnerId).HasColumnName("client_owner_id");
            builder.Property(p => p.DocumentId).HasColumnName("document_id");
            builder.Property(p => p.DocumentOwnerId).HasColumnName("document_owner_id");
            builder.Property(p => p.NumberContract).HasColumnName("NumberContract");
            builder.HasOne(p => p.CrmClientsRef)
                .WithOne(cr => cr.Contracts)
                .HasPrincipalKey<CrmClients>(cl => new {cl.ClientId, cl.ClientOwnerId})
                .HasForeignKey<Contracts>(c => new {c.ClientId, c.ClientOwnerId});
            builder.HasOne(cl => cl.ClientsRef)
                .WithOne(cr => cr.Contracts)
                .HasPrincipalKey<Clients>(cl => new {cl.Id, cl.OwnerId})
                .HasForeignKey<Contracts>(cr => new {cr.ClientId, cr.ClientOwnerId});
            builder.HasOne(cr => cr.CrmContractsRef)
                .WithOne(co => co.Contracts)
                .HasPrincipalKey<CrmContracts>(cr => new {cr.ContractId, cr.ContractOwnerId})
                .HasForeignKey<Contracts>(co => new {co.Id, co.OwnerId});
        }
    }
}
