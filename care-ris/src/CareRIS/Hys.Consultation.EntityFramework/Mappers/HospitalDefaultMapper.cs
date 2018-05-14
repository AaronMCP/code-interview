using Hys.Consultation.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class HospitalDefaultMapper : EntityTypeConfiguration<HospitalDefault>
    {
        public HospitalDefaultMapper()
        {
            this.ToTable("dbo.tbHospitalDefault");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("HospitalDefaultID").HasMaxLength(128);
            this.Property(u => u.RequestType).IsRequired();
            this.Property(u => u.RequestID).IsRequired().HasMaxLength(128);
            this.Property(u => u.ResponseType).IsRequired();
            this.Property(u => u.ResponseID).IsRequired().HasMaxLength(128);
            this.Property(u => u.Order).IsRequired();
            this.Property(u => u.Description).IsOptional();
            this.Property(u => u.IsDeleted).IsRequired();
            this.Property(u => u.Owner).IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();
        }
    }
}
