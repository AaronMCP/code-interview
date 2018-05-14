using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class RoleDirMapper : EntityTypeConfiguration<RoleDir>
    {
        public RoleDirMapper()
        {
            this.ToTable("dbo.tbRoleDir");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("UniqueID").HasMaxLength(36);
            this.Property(u => u.Name).IsRequired().HasMaxLength(128);
            this.Property(u => u.ParentID).IsOptional().HasMaxLength(36);
            this.Property(u => u.RoleID).IsOptional().HasMaxLength(36);
            this.Property(u => u.Leaf).IsOptional();
            this.Property(u => u.OrderID).IsOptional();
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
        }
    }
}
