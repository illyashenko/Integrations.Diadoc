using Integrations.Diadoc.Data.Apt.Entities;
using Microsoft.EntityFrameworkCore;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt;

public class AptContext : AptContext<AptContext>
{
    public DbSet<BillSend> BillSends { get; set; }
    public DbSet<DocumentTitle> DocumentTitles { get; set; }
    public DbSet<TitleString> TitleStrings { get; set; }
    public DbSet<BillSum> BillSums { get; set; }
    public DbSet<Cities> Cities { get; set; }
    public DbSet<Clients> Clients { get; set; }
    public DbSet<ClientsInfo> ClientsInfo { get; set; }
    public DbSet<Contracts> Contracts { get; set; }
    public DbSet<CrmContracts> CrmContracts { get; set; }
    public DbSet<CrmClients> CrmClients { get; set; }
    public DbSet<DiadocOrganizationsBoxId> DiadocOrganizationsBoxId { get; set; }
    public DbSet<Regions> Regions { get; set; }
    public DbSet<RegionsCode> RegionsCodes { get; set; }
    public DbSet<Addresses> Addresses { get; set; }
    public DbSet<ServiceType> ServiceTypes { get; set; }
    public DbSet<ScalarString> ScalarStrings { get; set; }
    public DbSet<ExternalExchangeDocuments> ExternalExchangeDocuments { get; set; }

    public AptContext(DbContextOptions<AptContext> context) : base(context)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new BillSendConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentTitleConfiguration());
        modelBuilder.ApplyConfiguration(new TitleStringConfiguration());
        modelBuilder.ApplyConfiguration(new BillSumConfiguration());
        modelBuilder.ApplyConfiguration(new CitiesConfiguration());
        modelBuilder.ApplyConfiguration(new ClientsConfiguration());
        modelBuilder.ApplyConfiguration(new ClientsInfoConfiguration());
        modelBuilder.ApplyConfiguration(new ContractsConfiguration());
        modelBuilder.ApplyConfiguration(new CrmContractsConfiguration());
        modelBuilder.ApplyConfiguration(new CrmClientsConfiguration());
        modelBuilder.ApplyConfiguration(new DiadocOrganizationsBoxIdConfiguration());
        modelBuilder.ApplyConfiguration(new RegionsConfiguration());
        modelBuilder.ApplyConfiguration(new RegionsCodeConfiguration());
        modelBuilder.ApplyConfiguration(new AddressesConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ExternalExchangeDocumentsConfiguration());

        modelBuilder.Entity<ScalarString>().HasNoKey();
        modelBuilder.HasDbFunction(() => fn_bc_GetServiceTypeNameContract(default, default, default, default));
    }
    
    public IQueryable<ScalarString> fn_bc_GetServiceTypeNameContract(int cId, int cOwnerId, int stId, int stOwnerId)
    {
        return FromExpression(() => fn_bc_GetServiceTypeNameContract(cId, cOwnerId, stId, stOwnerId)); 
    }
}
