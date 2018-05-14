using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ReportPrintLogMapper : EntityTypeConfiguration<ReportPrintLog>
    {
        public ReportPrintLogMapper()
        {
            this.ToTable("dbo.tbReportPrintLog");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("FileGuid").HasMaxLength(128);
            this.Property(u => u.ReportID).IsOptional().HasColumnName("ReportGuid").HasMaxLength(128);
            this.Property(u => u.TemplateID).IsOptional().HasColumnName("PrintTemplateGuid").HasMaxLength(128);
            this.Property(u => u.Printer).IsOptional().HasMaxLength(128);
            this.Property(u => u.PrintDate).IsOptional().HasColumnName("PrintDt");
            this.Property(u => u.Type).IsOptional().HasMaxLength(128);
            this.Property(u => u.Counts).IsOptional();
            this.Property(u => u.Comments).IsOptional().HasMaxLength(1024);
            this.Property(u => u.BackupMark).IsOptional().HasMaxLength(128);
            this.Property(u => u.BackupComment).IsOptional().HasMaxLength(512);
            this.Property(u => u.SnapShotSrvPath).IsOptional().HasMaxLength(256);
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
        }
    }
}
