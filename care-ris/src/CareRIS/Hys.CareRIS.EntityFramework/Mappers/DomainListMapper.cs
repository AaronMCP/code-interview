using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class DomainListMapper : EntityTypeConfiguration<DomainList>
    {
        public DomainListMapper()
        {
            this.ToTable("dbo.tbDomainList");
            this.HasKey(u => u.DomainName);
            this.Property(u => u.DomainName).IsRequired().HasColumnName("Domain").HasMaxLength(64);
            this.Property(u => u.DomainPrefix).IsOptional().HasMaxLength(64);
            this.Property(u => u.Connstring).IsOptional().HasMaxLength(512);
            this.Property(u => u.FtpServer).IsOptional().HasMaxLength(64);
            this.Property(u => u.FtpPort).IsOptional();
            this.Property(u => u.FtpUser).IsOptional().HasMaxLength(64);
            this.Property(u => u.FtpPassword).IsOptional().HasMaxLength(64);
            this.Property(u => u.PacsAETitle).IsOptional().HasMaxLength(64);
            this.Property(u => u.Telephone).IsOptional().HasMaxLength(64);
            this.Property(u => u.Address).IsOptional().HasMaxLength(64);
            this.Property(u => u.PacsServer).IsOptional().HasMaxLength(512);
            this.Property(u => u.PacsWebServer).IsOptional().HasMaxLength(512);
            this.Property(u => u.Tab).IsOptional();
            this.Property(u => u.Alias).IsOptional().HasMaxLength(16);
            this.Property(u => u.IISUrl).IsOptional().HasMaxLength(256);
        }
    }
}
