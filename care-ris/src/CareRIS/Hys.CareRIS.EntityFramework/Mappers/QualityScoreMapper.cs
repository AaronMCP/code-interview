using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class QualityScoreMapper : EntityTypeConfiguration<QualityScore>
    {
        public QualityScoreMapper()
        {
            this.ToTable("dbo.tbQualityScoring");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("Guid").HasMaxLength(36);
            this.Property(u => u.AppraiseObject).HasMaxLength(128);
            this.Property(u => u.OrderID).IsOptional().HasColumnName("OrderGuid").HasMaxLength(512);
            this.Property(u => u.ExaminateTime).IsOptional().HasColumnName("ExaminateDt");
            this.Property(u => u.ScoreType).IsOptional().HasColumnName("Type");
            this.Property(u => u.Result).IsOptional().HasMaxLength(128);
            this.Property(u => u.Appraisee).IsOptional().HasMaxLength(512);
            this.Property(u => u.Appraiser).IsOptional().HasMaxLength(128);
            this.Property(u => u.AppraiseDate).IsOptional();
            this.Property(u => u.Comment).IsOptional().IsMaxLength();
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.Result2).IsOptional().HasMaxLength(128);
            this.Property(u => u.Result3).IsOptional().HasMaxLength(128);
        }
    }
}
