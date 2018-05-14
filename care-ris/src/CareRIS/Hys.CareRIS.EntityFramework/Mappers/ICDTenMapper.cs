using System.Data.Entity.ModelConfiguration;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ICDTenMapper : EntityTypeConfiguration<ICDTen>
    {
        public ICDTenMapper()
        {
            this.ToTable("dbo.tbIcd10");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.Domain).IsRequired();
            this.Property(u => u.ID).IsRequired();
            this.Property(u => u.Name).IsRequired().HasColumnName("INAME");
            this.Property(u => u.PY).IsOptional();
            this.Property(u => u.WB).IsOptional();
            this.Property(u => u.TJM).IsOptional();
            this.Property(u => u.BZLB).IsOptional();
            this.Property(u => u.ZLBM).IsOptional();
            this.Property(u => u.JLZT).IsOptional();
            this.Property(u => u.Memo).IsOptional().HasColumnName("MEMO");
        }
    }
}
