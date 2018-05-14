using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ReportFileMapper : EntityTypeConfiguration<ReportFile>
    {
        public ReportFileMapper()
        {
            this.ToTable("dbo.tbReportFile");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("FileGuid").HasMaxLength(128);
            this.Property(u => u.ReportID).IsRequired().HasColumnName("ReportGuid").HasMaxLength(128);
            this.Property(u => u.fileType).IsOptional();
            this.Property(u => u.RelativePath).IsOptional().HasMaxLength(256);
            this.Property(u => u.FileName).IsOptional().HasMaxLength(128);
            this.Property(u => u.BackupMark).IsOptional().HasMaxLength(128);
            this.Property(u => u.BackupComment).IsOptional().HasMaxLength(512);
            this.Property(u => u.ShowWidth).IsOptional();
            this.Property(u => u.ShowHeight).IsOptional();
            this.Property(u => u.ImagePosition).IsOptional();
            this.Property(u => u.CreateTime).IsOptional().HasColumnName("CreateDt");
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
        }
    }
}
