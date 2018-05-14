using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class DAMInfoMapper : EntityTypeConfiguration<DAMInfo>
    {
        public DAMInfoMapper()
        {
            this.ToTable("dbo.tbDamInfo");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("DamID").HasMaxLength(128);
            this.Property(u => u.Name).IsRequired().HasMaxLength(64);
            this.Property(u => u.IpAddress).IsRequired().HasMaxLength(64);
            this.Property(u => u.WebApiUrl).IsRequired().HasMaxLength(128);
            this.Property(u => u.Description).IsRequired().HasMaxLength(512);
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();
        }
    }
}
