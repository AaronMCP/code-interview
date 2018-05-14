using System.Data.Entity.ModelConfiguration;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class UserExtentionMapper : EntityTypeConfiguration<UserExtention>
    {
        public UserExtentionMapper()
        {
            this.ToTable("dbo.tbUserExtention");

            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("UserID").HasMaxLength(128);

            this.Property(u => u.Description);
            this.Property(u => u.Avatar);
            this.Property(u => u.LastStatus).HasMaxLength(200); ;
            this.Property(u => u.ExpertLevel);
            this.Property(u => u.ResearchDomain);
            this.Property(u => u.Introduction);
            this.Property(u => u.DefaultRoleID).HasMaxLength(128);
            this.HasOptional(u => u.Hospital).WithMany().HasForeignKey(l => l.HospitalID).WillCascadeOnDelete(false);
            this.HasOptional(u => u.Department).WithMany().HasForeignKey(l => l.DepartmentID).WillCascadeOnDelete(false);
            this.HasMany(u => u.Roles).WithMany().Map(cs => cs.MapLeftKey("UserID").MapRightKey("RoleID").ToTable("tUserRoleLink"));
        }
    }
}


