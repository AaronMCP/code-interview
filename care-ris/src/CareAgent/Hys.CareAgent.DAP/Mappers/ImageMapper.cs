
namespace Hys.CareAgent.DAP.Mappers
{
    using System.Data.Entity.ModelConfiguration;
    using Hys.CareAgent.DAP.Entity;

    class ImageMapper : EntityTypeConfiguration<Image>
    {
        public ImageMapper()
        {
            this.ToTable("dbo.tImage");
            this.HasKey(u => u.SOPInstanceUID);
            this.Property(u => u.SOPInstanceUID).IsRequired().HasMaxLength(256);
            this.Property(u => u.SeriesInstanceUID).IsRequired().HasMaxLength(256);
            this.Property(u => u.ImageNo).IsOptional();
            this.Property(u => u.FilePath).IsRequired().HasMaxLength(512);
            this.Property(u => u.CreateTime).IsRequired();
        }
    }
}



