using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Integrations.Diadoc.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickPoint.Models.Data;

namespace Integrations.Diadoc.Data.Apt.Entities
{
    public class DocumentTitle
    {
        public int DocumentId { get; set; }
        public int DocumentOwnerId { get; set; }
        public DocumentTypes? DocumentType { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime? DocumentDate { get; set; }
        public TitleString? TitleString { get; set; }
        
        //public BillSum? BillSums { get; set; }
        
        public virtual ICollection<TitleString>? TitleStrings { get; set; }
        public virtual ICollection<BillSum>? BillSumsCollection { get; set; }

    }
    public class DocumentTitleConfiguration : IEntityTypeConfiguration<DocumentTitle> 
    {
        public void Configure(EntityTypeBuilder<DocumentTitle> builder)
        {
            builder.ToTable("DocumentTitle");
            builder.HasKey(p => p.DocumentId);
            builder.Property(p => p.DocumentId).HasColumnName("id");
            builder.Property(p => p.DocumentOwnerId).HasColumnName("Owner_id");
            builder.Property(p => p.DocumentType).HasColumnName("DocumentType");
            builder.Property(p => p.DocumentNumber).HasColumnName("DocumentNumber");
            builder.Property(p => p.DocumentDate).HasColumnName("DateOfCreate");
            builder.HasOne(dt => dt.TitleString)
                .WithOne(ts => ts.DocumentTitle)
                .HasPrincipalKey<TitleString>(ts => new {ts.DocumentTitleId, ts.DocumentTitleOwnerId})
                .HasForeignKey<DocumentTitle>(dt => new {dt.DocumentId, dt.DocumentOwnerId});
        }
    }
}
