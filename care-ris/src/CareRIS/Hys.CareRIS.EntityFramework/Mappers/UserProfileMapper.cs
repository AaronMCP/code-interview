using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class UserProfileMapper : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileMapper()
        {
            this.ToTable("dbo.tbUserProfile");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasMaxLength(36);
            this.Property(u => u.Name).IsRequired().HasMaxLength(128);
            this.Property(u => u.Value).IsOptional().IsMaxLength();
            this.Property(u => u.ModuleID).IsRequired().HasMaxLength(128);
            this.Property(u => u.RoleName).IsRequired().HasMaxLength(128);
            this.Property(u => u.PropertyDesc).IsOptional().HasMaxLength(512);
            this.Property(u => u.PropertyOptions).IsOptional().IsMaxLength();
            this.Property(u => u.PropertyType).IsRequired();
            this.Property(u => u.IsExportable).IsRequired().HasColumnName("Exportable");
            this.Property(u => u.CanBeInherited).IsRequired().HasColumnName("Inheritance");
            this.Property(u => u.IsHidden).IsRequired();
            this.Property(u => u.OrderingPos).IsRequired().HasMaxLength(128);
            this.Property(u => u.UserID).IsRequired().HasMaxLength(128).HasColumnName("UserGuid");
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
        }
    }
}
