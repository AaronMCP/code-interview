
namespace Hys.CareAgent.DAP.Mappers
{
    using System.Data.Entity.ModelConfiguration;
    using Hys.CareAgent.DAP.Entity;

    class SeriesMapper: EntityTypeConfiguration<Series>
    {
      public SeriesMapper()
        {
            this.ToTable("dbo.tSeries");
            this.HasKey(u => u.SeriesInstanceUID);
            this.Property(u => u.SeriesInstanceUID).IsRequired().HasMaxLength(256);
            this.Property(u => u.StudyInstanceUID).IsRequired().HasMaxLength(256);
            this.Property(u => u.SeriesNo).IsOptional();
            this.Property(u => u.BodyPart).IsOptional().HasMaxLength(64);
            this.Property(u => u.Modality).IsOptional().HasMaxLength(64);
        }
    }
}



