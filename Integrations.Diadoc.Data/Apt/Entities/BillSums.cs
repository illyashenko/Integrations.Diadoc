﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class BillSums
    {
        [Key]
        public int BillSumId { get; set; }
        public int BillSumOwnerId { get; set; }
        public int BillId { get; set; }
        public int BillOwnerId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Measure { get; set; }
        public decimal Tariff { get; set; }
        public decimal Vat { get; set; }
        public int ServiceTypeId { get; set; }
        public int ServiceTypeOwnerId { get; set; }
        public int HasInvoice { get; set; }
        public DocumentTitle DocumentTitle { get; set; }
        public ServiceType ServiceType { get; set; }
        public decimal VatPercent { get; set; }
    }

    public class BillSumConfiguration : IEntityTypeConfiguration<BillSums>
    {
        public void Configure(EntityTypeBuilder<BillSums> builder)
        {
            builder.ToTable("BillSum");
            builder.Property(p => p.BillSumId).HasColumnName("id");
            builder.Property(p => p.BillSumOwnerId).HasColumnName("Owner_Id");
            builder.Property(p => p.BillId).HasColumnName("Bill_id");
            builder.Property(p => p.BillOwnerId).HasColumnName("Bill_Owner_Id");
            builder.Property(p => p.Description).HasColumnName("Description");
            builder.Property(p => p.Quantity).HasColumnName("Quantity");
            builder.Property(p => p.Measure).HasColumnName("Measure");
            builder.Property(p => p.HasInvoice).HasColumnName("HasInvoice");
            builder.Property(p => p.Tariff).HasColumnName("Tariff").HasConversion(new CastingConverter<decimal, double>());
            builder.Property(p => p.Vat).HasColumnName("NDS").HasConversion(new CastingConverter<decimal, double>());
            builder.Property(p => p.VatPercent).HasColumnName("beNDS").HasConversion(new CastingConverter<decimal, double>());
            builder.Property(p => p.ServiceTypeId).HasColumnName("ServiceType_id");
            builder.Property(p => p.ServiceTypeOwnerId).HasColumnName("ServiceType_Owner_id");
            builder.HasOne(st => st.ServiceType)
                .WithOne(b => b.BillSums)
                .HasPrincipalKey<ServiceType>(st => new {st.ServiceTypeId, st.ServiceTypeOwnerId})
                .HasForeignKey<BillSums>(b => new {b.ServiceTypeId, b.ServiceTypeOwnerId});
            builder.HasOne(dt => dt.DocumentTitle)
                .WithMany(b=>b.BillSumsCollection)
                .HasPrincipalKey(dt => new {dt.DocumentId, dt.DocumentOwnerId})
                .HasForeignKey(b => new {b.BillId, b.BillOwnerId});

        }
    }
}
