using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ApplyDoctorMapper : EntityTypeConfiguration<ApplyDoctor>
    {
        public ApplyDoctorMapper()
        {
            this.ToTable("dbo.tbApplyDoctor");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ID").HasMaxLength(128);
            this.Property(u => u.ApplyDeptID).IsOptional().HasMaxLength(128);
            this.Property(u => u.DoctorName).IsRequired().HasColumnName("ApplyDoctor").HasMaxLength(128);
            this.Property(u => u.Gender).IsOptional().HasMaxLength(128);
            this.Property(u => u.Telephone).IsOptional().HasMaxLength(128);
            this.Property(u => u.Mobile).IsOptional().HasMaxLength(128);
            this.Property(u => u.StaffNo).IsOptional().HasColumnName("StaffID").HasMaxLength(128);
            this.Property(u => u.Email).IsOptional().HasColumnName("EMail").HasMaxLength(128);
            this.Property(u => u.ShortcutCode).IsOptional().HasMaxLength(512);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
        }
    }
}
