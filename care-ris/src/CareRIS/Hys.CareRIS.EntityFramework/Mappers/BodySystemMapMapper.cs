using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class BodySystemMapMapper : EntityTypeConfiguration<BodySystemMap>
    {
        public BodySystemMapMapper()
        {
            this.ToTable("dbo.tbBodySystemMap");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasMaxLength(36);
            this.Property(u => u.ModalityType).IsRequired().HasMaxLength(128);
            this.Property(u => u.BodyPart).IsRequired().HasColumnName("Bodypart").HasMaxLength(512);
            this.Property(u => u.ExamSystem).IsRequired().HasMaxLength(128);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
        }
    }
}
