using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ReportTemplateMapper : EntityTypeConfiguration<ReportTemplate>
    {
        public ReportTemplateMapper()
        {
            this.ToTable("dbo.tbReportTemplate");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("TemplateGuid").HasMaxLength(128);
            this.Property(u => u.TemplateName).IsRequired().HasMaxLength(128);
            this.Property(u => u.ModalityType).IsRequired().HasMaxLength(128);
            this.Property(u => u.BodyPart).IsRequired().HasMaxLength(512);
            this.Property(u => u.WYS).IsOptional();
            this.Property(u => u.WYG).IsOptional();
            this.Property(u => u.AppendInfo).IsOptional();
            this.Property(u => u.TechInfo).IsOptional();
            this.Property(u => u.CheckItemName).IsOptional().HasMaxLength(128);
            this.Property(u => u.DoctorAdvice).IsOptional().HasMaxLength(512);
            this.Property(u => u.ShortcutCode).IsOptional().HasMaxLength(128);
            this.Property(u => u.ACRCode).IsOptional().HasMaxLength(128);
            this.Property(u => u.ACRAnatomicDesc).IsOptional().HasMaxLength(512);
            this.Property(u => u.ACRPathologicDesc).IsOptional().HasMaxLength(512);
            this.Property(u => u.BodyCategory).IsOptional().HasMaxLength(128);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.Gender).HasMaxLength(32).IsOptional();
            this.Property(u => u.IsPositive).IsOptional().HasColumnName("Positive");
        }
    }
}
