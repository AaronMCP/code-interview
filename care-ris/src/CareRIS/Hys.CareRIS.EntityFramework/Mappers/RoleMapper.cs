using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class RoleMapper : EntityTypeConfiguration<Role>
    {
        public RoleMapper()
        {
            this.ToTable("dbo.tbRole");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("RoleID").HasMaxLength(36);
            this.Property(u => u.RoleName).IsRequired().HasMaxLength(128);
            this.Property(u => u.Description).IsOptional().HasMaxLength(512);
            this.Property(u => u.IsSystem).IsOptional();
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
        }
    }
}
