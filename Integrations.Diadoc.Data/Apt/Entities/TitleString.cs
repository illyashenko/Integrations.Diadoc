using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Integrations.Diadoc.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class TitleString
    {
        public int TitleId { get; set; }
        public int TitleOwnerId { get; set; }
        public int DocumentTitleId { get; set; }
        public int DocumentTitleOwnerId { get; set; }
        public string? StringValue { get; set; }
        public decimal? RealValue { get; set; }
        public int? IntValue { get; set; }
        public int? IntValue2 { get; set; }
        public DateTime? DateTimeValue { get; set; }
        public ParameterTypes? ParameterType { get; set; }
        public DocumentTitle? DocumentTitle { get; set; }
        public Contracts? Contracts { get; set; }
        public virtual ICollection<Contracts>? ContractsRef { get; set; } 
    }
    public class TitleStringConfiguration : IEntityTypeConfiguration<TitleString> 
    {
        public void Configure(EntityTypeBuilder<TitleString> builder)
        {
            
            builder.ToTable("TitleString");
            builder.HasKey(m => m.TitleId);
            builder.Property(p => p.TitleId).HasColumnName("id");
            builder.Property(p => p.TitleOwnerId).HasColumnName("Owner_id");
            builder.Property(p => p.DocumentTitleId).HasColumnName("DocumentTitle_id");
            builder.Property(p => p.DocumentTitleOwnerId).HasColumnName("DocumentTitle_owner_id");
            builder.Property(p => p.ParameterType).HasColumnName("ParametrType");
            builder.Property(p => p.StringValue).HasColumnName("StringValue");
            builder.Property(p => p.DateTimeValue).HasColumnName("DateTimeValue");
            builder.Property(p => p.IntValue).HasColumnName("IntValue");
            builder.Property(p => p.IntValue2).HasColumnName("IntValue_1");
            builder.Property(p => p.RealValue).HasColumnName("RealValue").HasConversion(new CastingConverter<decimal,double>());
            builder.HasOne(p => p.Contracts)
                .WithOne(c => c.TitleString)
                .HasPrincipalKey<Contracts>(c => new {c.Id, c.OwnerId})
                .HasForeignKey<TitleString>(ts => new {ts.IntValue, ts.IntValue2});
        }
    }
}
