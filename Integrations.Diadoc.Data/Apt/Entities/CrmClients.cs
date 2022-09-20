using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class CrmClients : LegacyTable
    {
        public string OrgId { get; set; }
        public int ClientId { get; set; }
        public int ClientOwnerId { get; set; }
        public DateTime ContractDate { get; set; }
        public DiadocOrganizationsBoxId OrganizationsBoxId { get; set; }
        public Contracts Contracts { get; set; }
    }
    public class CrmClientsConfiguration : LegacyTableConfiguration<CrmClients>
    {
        public override void Configure(EntityTypeBuilder<CrmClients> builder)
        {
            builder.ToTable("CRM_Clients");
            builder.Property(p => p.ClientId).HasColumnName("Client_Id");
            builder.Property(p => p.OrgId).HasColumnName("DiadocOrgId");
            builder.Property(p => p.ClientOwnerId).HasColumnName("Client_Owner_Id");
            builder.Property(p => p.ContractDate).HasColumnName("ContractDate");
        }
    }
}