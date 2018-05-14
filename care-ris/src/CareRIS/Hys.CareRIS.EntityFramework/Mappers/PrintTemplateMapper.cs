using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class PrintTemplateMapper : EntityTypeConfiguration<PrintTemplate>
    {
        public PrintTemplateMapper()
        {
            this.ToTable("dbo.tbPrintTemplate");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("TemplateGuid").HasMaxLength(128);
            this.Property(u => u.Type).IsOptional();
            this.Property(u => u.TemplateName).IsRequired().HasMaxLength(128);
            this.Property(u => u.TemplateInfo).IsOptional();
            this.Property(u => u.IsDefaultByType).IsOptional();
            this.Property(u => u.Version).IsOptional();
            this.Property(u => u.ModalityType).IsOptional().HasMaxLength(128);
            this.Property(u => u.IsDefaultByModality).IsOptional();
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.PropertyTag).IsOptional().HasMaxLength(1024);
            this.Property(u => u.Site).IsRequired().HasMaxLength(64);
        }
    }
}
