using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class OnlineClientMapper : EntityTypeConfiguration<OnlineClient>
    {
        public OnlineClientMapper()
        {
            this.ToTable("dbo.tbOnlineClient");
            this.HasKey(u => new { u.UniqueID, u.MachineIP, u.Domain });

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("UserGuid").HasMaxLength(128);
            this.Property(u => u.MachineIP).IsRequired().HasMaxLength(128);
            this.Property(u => u.RoleName).IsRequired().HasMaxLength(128);
            this.Property(u => u.IISUrl).IsOptional().HasMaxLength(128);
            this.Property(u => u.LoginTime).IsOptional();
            this.Property(u => u.Comments).IsOptional().HasMaxLength(512);
            this.Property(u => u.SessionID).IsOptional().HasMaxLength(128);
            this.Property(u => u.Comments).IsOptional().HasMaxLength(512);
            this.Property(u => u.IsOnline).IsOptional();
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.MachineName).IsOptional().HasMaxLength(512);
            this.Property(u => u.MACAddress).IsOptional().HasMaxLength(512);
            this.Property(u => u.Location).IsOptional().HasMaxLength(512);
        }
    }
}
