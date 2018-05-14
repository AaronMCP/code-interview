using System.Data.Entity.ModelConfiguration;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class User2DomainMapper : EntityTypeConfiguration<User2Domain>
    {
        public User2DomainMapper()
        {
            this.ToTable("dbo.tbUser2Domain");
            this.HasKey(u => new { u.UniqueID, u.Domain });

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("UserGuid").HasMaxLength(64);
            this.Property(u => u.Department).HasMaxLength(64);
            this.Property(u => u.DomainLoginName).HasMaxLength(64);
            this.Property(u => u.Telephone).HasMaxLength(64);
            this.Property(u => u.IsSetExpireDate).IsOptional();
            this.Property(u => u.StartDate);
            this.Property(u => u.EndDate);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.Mobile).HasMaxLength(128);
            this.Property(u => u.Email).HasMaxLength(128);
        }
    }
}
