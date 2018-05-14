using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class SysConfigMapper : EntityTypeConfiguration<SysConfig>
    {
        public SysConfigMapper()
        {
            this.ToTable("dbo.tbSysConfig");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("SysConfigID").HasMaxLength(128);
            this.Property(u => u.Module).IsRequired();
            this.Property(u => u.GroupName).IsOptional().HasMaxLength(128);
            this.Property(u => u.ConfigOwner).IsOptional().HasMaxLength(128);
            this.Property(u => u.ConfigKey).IsRequired().HasMaxLength(100);
            this.Property(u => u.ConfigValue).IsOptional().HasMaxLength(512);
            this.Property(u => u.ConfigDescription).IsOptional();
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();
        }
    }
}
