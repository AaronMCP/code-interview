using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class PrintTemplateFieldsMapper : EntityTypeConfiguration<PrintTemplateFields>
    {
        public PrintTemplateFieldsMapper()
        {
            this.ToTable("dbo.tbPrintTemplateFields");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasMaxLength(36);
            this.Property(u => u.FieldName).IsRequired().HasMaxLength(128);
            this.Property(u => u.Type).IsOptional();
            this.Property(u => u.SubType).IsOptional().HasMaxLength(128);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
        }
    }
}
