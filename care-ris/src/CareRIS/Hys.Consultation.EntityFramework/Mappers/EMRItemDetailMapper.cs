using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class EMRItemDetailMapper : EntityTypeConfiguration<EMRItemDetail>
    {
        public EMRItemDetailMapper()
        {
            this.ToTable("dbo.tbEmrItemDetail");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("EMRItemDetailID").HasMaxLength(128);
            this.Property(u => u.EMRItemID).IsRequired().HasMaxLength(128);
            this.Property(u => u.DetailID).IsRequired().HasMaxLength(128);
            this.Property(u => u.DamID).IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();
        }
    }
}
