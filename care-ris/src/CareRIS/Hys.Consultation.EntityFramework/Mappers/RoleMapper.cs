using Hys.Consultation.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class RoleMapper : EntityTypeConfiguration<Role>
    {
        public RoleMapper()
        {
            this.ToTable("dbo.tbRole");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.RoleName).IsRequired();
            this.Property(u => u.Status).IsRequired();
            this.Property(u => u.LastEditTime).IsRequired();
            this.Property(u => u.IsDeleted).IsRequired();
            this.Property(u => u.IsSystem).IsRequired();
            this.Property(u => u.Description).IsOptional();
            this.Property(u => u.Permissions).IsOptional();
        }
    }
}
