using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ReportListMapper : EntityTypeConfiguration<ReportList>
    {
        public ReportListMapper()
        {
            this.ToTable("dbo.tbReportList");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ReportListGuid").HasMaxLength(128);
            this.Property(u => u.ReportID).IsRequired().HasColumnName("ReportGuid").HasMaxLength(128);
            this.Property(u => u.ReportName).IsRequired().HasMaxLength(512);
            this.Property(u => u.WYS).IsOptional();
            this.Property(u => u.WYG).IsOptional();
            this.Property(u => u.AppendInfo).IsOptional();
            this.Property(u => u.ReportText).IsOptional();
            this.Property(u => u.DoctorAdvice).IsOptional();
            this.Property(u => u.IsPositive).IsOptional();
            this.Property(u => u.AcrCode).IsOptional().HasMaxLength(128);
            this.Property(u => u.AcrAnatomic).IsOptional().HasMaxLength(128);
            this.Property(u => u.AcrPathologic).IsOptional().HasMaxLength(128);
            this.Property(u => u.Creater).IsOptional().HasMaxLength(128);
            this.Property(u => u.CreateTime).HasColumnName("CreateDt").IsOptional();
            this.Property(u => u.Submitter).IsOptional().HasMaxLength(128);
            this.Property(u => u.SubmitTime).HasColumnName("SubmitDt").IsOptional();
            this.Property(u => u.FirstApprover).IsOptional().HasMaxLength(128);
            this.Property(u => u.FirstApproveTime).HasColumnName("FirstApproveDt").IsOptional();
            this.Property(u => u.SecondApprover).IsOptional().HasMaxLength(128);
            this.Property(u => u.SecondApproveTime).IsOptional().HasColumnName("SecondApproveDt");
            this.Property(u => u.IsDiagnosisRight).IsOptional();
            this.Property(u => u.KeyWord).IsOptional().HasMaxLength(128);
            this.Property(u => u.ReportQuality).IsOptional().HasMaxLength(128);
            this.Property(u => u.RejectToObject).IsOptional().HasMaxLength(128);
            this.Property(u => u.Rejecter).IsOptional().HasMaxLength(128);
            this.Property(u => u.RejectTime).IsOptional().HasColumnName("RejectDt");
            this.Property(u => u.Status).IsOptional();
            this.Property(u => u.Comments).IsOptional();
            this.Property(u => u.Mender).IsOptional().HasMaxLength(128);
            this.Property(u => u.ModifyTime).IsOptional().HasColumnName("ModifyDt");
            this.Property(u => u.IsPrint).IsOptional();
            this.Property(u => u.OperationTime).IsOptional();
            
            this.Property(u => u.WYSText).IsOptional();
            this.Property(u => u.WYGText).IsOptional();
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.SubmitterName).IsOptional().HasMaxLength(32);
            this.Property(u => u.CreaterName).IsOptional().HasMaxLength(32);
            this.Property(u => u.MenderName).IsOptional().HasMaxLength(32);
            this.Property(u => u.TechInfo).IsOptional();

        }
    }
}
