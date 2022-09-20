using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class CrmContracts : LegacyTable
    {
        public int ContractId { get; set; }
        public int ContractOwnerId { get; set; }
        public DateTime? ContractDate { get; set; }
        public int Flags { get; set; }
        public Contracts Contracts { get; set; }
    }
    public class CrmContractsConfiguration : LegacyTableConfiguration<CrmContracts> 
    {
        public override void Configure(EntityTypeBuilder<CrmContracts> builder)
        {
            builder.ToTable("CRM_Contracts");
            builder.Property(p => p.ContractId).HasColumnName("contract_id");
            builder.Property(p => p.ContractOwnerId).HasColumnName("contract_owner_id");
            builder.Property(p => p.ContractDate).HasColumnName("ContractDate");
            builder.Property(p => p.Flags).HasColumnName("Flags");
        }
    }
}
