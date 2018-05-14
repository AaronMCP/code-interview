using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class DAMHospitalMapper : EntityTypeConfiguration<DAMHospital>
    {
        public DAMHospitalMapper()
        {
            this.ToTable("dbo.tbDamHospital");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("DamHospitalID").HasMaxLength(128);
            this.Property(u => u.DamID).IsRequired().HasMaxLength(128);
            this.Property(u => u.HospitalID).IsRequired().HasMaxLength(128);
            this.Property(u => u.Order).IsOptional();
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();
        }
    }
}
