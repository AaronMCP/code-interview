using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class ConsultationPatientNoMapper : EntityTypeConfiguration<ConsultationPatientNo>
    {
        public ConsultationPatientNoMapper()
        {
            this.ToTable("dbo.tbConsultationPatientNo");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ConsultationPatientNoID").HasMaxLength(128);
            this.Property(u => u.HospitalID).IsOptional().HasColumnName("HospitalID").HasMaxLength(128);
            this.Property(u => u.Prefix).IsOptional().HasColumnName("Prefix").HasMaxLength(128);
            this.Property(u => u.MaxLength).IsRequired().HasColumnName("MaxLength");
            this.Property(u => u.CurrentValue).IsRequired().HasColumnName("CurrentValue");
            
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired().HasColumnType("datetime");
        }
    }
}
