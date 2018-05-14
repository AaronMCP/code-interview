using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            this.ToTable("dbo.tbUser");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("UserGuid").HasMaxLength(128);
            this.Property(u => u.LoginName).IsRequired().HasMaxLength(128);
            this.Property(u => u.LocalName).IsRequired().HasMaxLength(128);
            this.Property(u => u.EnglishName).IsOptional().HasMaxLength(128);
            this.Property(u => u.Password).IsOptional().HasMaxLength(128);
            this.Property(u => u.Title).IsOptional().HasMaxLength(128);
            this.Property(u => u.Address).IsOptional().HasMaxLength(128);
            this.Property(u => u.Comments).IsOptional().HasMaxLength(512);
            this.Property(u => u.DeleteMark).IsOptional().HasColumnName("DeleteMark");
            this.Property(u => u.SignImage).IsOptional();
            this.Property(u => u.PrivateKey).IsOptional();
            this.Property(u => u.PublicKey).IsOptional();
            this.Property(u => u.IkeySn).IsOptional().HasColumnType("nchar").HasMaxLength(128);
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
            this.Property(u => u.DisplayName).IsRequired().HasMaxLength(128);
            this.Property(u => u.InvalidLoginCount).IsOptional().HasColumnName("InvalidLoginCount");
            this.Property(u => u.IsLocked).IsOptional().HasColumnName("IsLocked");
        }
    }
}
