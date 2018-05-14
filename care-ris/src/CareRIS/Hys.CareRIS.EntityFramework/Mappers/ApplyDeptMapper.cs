using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ApplyDeptMapper : EntityTypeConfiguration<ApplyDept>
    {
        public ApplyDeptMapper()
        {
            this.ToTable("dbo.tbApplyDept");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ID").HasMaxLength(128);
            this.Property(u => u.DeptName).IsRequired().HasColumnName("ApplyDept").HasMaxLength(128);
            this.Property(u => u.Telephone).IsOptional().HasMaxLength(128);
            this.Property(u => u.ShortcutCode).IsOptional().HasMaxLength(512);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
        }
    }
}
