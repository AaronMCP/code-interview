using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ScoringResultMapper : EntityTypeConfiguration<ScoringResult>
    {
        public ScoringResultMapper()
        {
            this.ToTable("dbo.tbScoringResult");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("Guid").HasMaxLength(36);
            this.Property(u => u.ObjectGuid).IsRequired().HasMaxLength(128);
            this.Property(u => u.CreateDate).IsOptional();
            this.Property(u => u.Type).IsOptional().HasColumnName("Type");
            this.Property(u => u.Result).IsOptional().HasMaxLength(128);
            this.Property(u => u.AccordRate).IsOptional().HasMaxLength(512);
            this.Property(u => u.Appraiser).IsOptional().HasMaxLength(128);
            this.Property(u => u.Comment).IsOptional().IsMaxLength();
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.Result2).IsOptional().HasMaxLength(128);
            this.Property(u => u.IsFinalVersion).IsOptional();
        }
    }
}
