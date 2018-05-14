using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class RoleToUserMapper : EntityTypeConfiguration<RoleToUser>
    {
        public RoleToUserMapper()
        {
            this.ToTable("dbo.tbRole2User");
            this.HasKey(u => new { u.RoleName, u.UserID, u.Domain });

            this.Property(u => u.RoleName).IsRequired().HasMaxLength(128);
            this.Property(u => u.UserID).IsRequired().HasColumnName("UserGuid").HasMaxLength(128);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
        }
    }
}
