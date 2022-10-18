﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class DiadocOrganizationsBoxId
    {
        public int Id { get; set; }
        public string OrgId { get; set; }
        public string BoxId { get; set; }
        public string? Title { get; set; }
        public bool ServiceCode { get; set; }
        public CrmClients? CrmClients { get; set; }
    }
    public class DiadocOrganizationsBoxIdConfiguration : IEntityTypeConfiguration<DiadocOrganizationsBoxId>
    {
        public void Configure(EntityTypeBuilder<DiadocOrganizationsBoxId> builder)
        {
            builder.ToTable("DiadocOrganizationsBoxId");
            builder.HasKey(p => p.Id);
            // builder.Property(p => p.Id).HasColumnName("Id");
            // builder.Property(p => p.OrgId).HasColumnName("OrgId");
            // builder.Property(p => p.BoxId).HasColumnName("BoxId");
            // builder.Property(p => p.Title).HasColumnName("Title");
            // builder.Property(p => p.ServiceCode).HasColumnName("ServiceCode");
            builder.HasOne(p => p.CrmClients)
                .WithOne(c => c.OrganizationsBoxId)
                .HasPrincipalKey<CrmClients>(c => c.OrgId)
                .HasForeignKey<DiadocOrganizationsBoxId>(p => p.OrgId);
        }
    }
}