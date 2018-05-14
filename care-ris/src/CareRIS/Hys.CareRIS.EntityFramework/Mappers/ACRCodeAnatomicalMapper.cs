using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ACRCodeAnatomicalMapper : EntityTypeConfiguration<ACRCodeAnatomical>
    {
        public ACRCodeAnatomicalMapper()
        {
            this.ToTable("dbo.tbAcrCodeAnatomical");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("UniqueID").HasMaxLength(36);
            this.Property(u => u.AID).IsRequired().HasColumnName("AID").HasMaxLength(128);
            this.Property(u => u.Description).IsOptional().HasMaxLength(512);
            this.Property(u => u.DescriptionEn).IsOptional().HasMaxLength(512);
            this.Property(u => u.Domain).IsRequired().IsOptional().HasMaxLength(128);
        }
    }
}
