using Hys.Consultation.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class HospitalProfileMapper : EntityTypeConfiguration<HospitalProfile>
    {
        public HospitalProfileMapper()
        {
            this.ToTable("dbo.tbHospitalProfile");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("OrganizationID").HasMaxLength(128);
            this.Property(u => u.HospitalName).HasColumnName("HospitalName").IsRequired().HasMaxLength(100);

            this.Property(u => u.Province).IsOptional().HasMaxLength(200);
            this.Property(u => u.City).IsOptional().HasMaxLength(200);
            this.Property(u => u.Area).IsOptional().HasMaxLength(200);
            this.Property(u => u.Address).HasColumnName("Address").IsOptional().HasMaxLength(200);

            this.Property(u => u.TelePhone).HasColumnName("TelePhone").IsOptional().HasMaxLength(100);
            this.Property(u => u.Website).HasColumnName("Website").IsOptional().HasMaxLength(200);
            this.Property(u => u.Introduction).HasColumnName("Introduction").IsOptional().IsMaxLength();
            this.Property(u => u.HospitalType).HasColumnName("HospitalType").IsOptional().HasMaxLength(100);
            this.Property(u => u.HospitalLevel).HasColumnName("HospitalLevel").IsOptional().HasMaxLength(100);
            this.Property(u => u.DicomPrefix).HasColumnName("DicomPrefix").IsOptional().HasMaxLength(10);
            this.Property(u => u.HospitalImage).HasColumnName("HospitalImage").IsOptional();
            this.Property(u => u.Status).IsRequired();
            this.Property(u => u.IsConsultation).IsOptional();
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();

            this.HasOptional(u => u.Dam1).WithMany().HasForeignKey(l => l.Dam1ID).WillCascadeOnDelete(false);
        }
    }
}
