using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ModalityMapper : EntityTypeConfiguration<Modality>
    {
        public ModalityMapper()
        {
            this.ToTable("dbo.tbModality");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ModalityGuid").HasMaxLength(128);
            this.Property(u => u.ModalityType).IsRequired().HasMaxLength(128);
            this.Property(u => u.ModalityName).IsOptional().HasColumnName("Modality").HasMaxLength(512);
            this.Property(u => u.Room).IsOptional().HasMaxLength(256);
            this.Property(u => u.IPAddress).IsOptional().HasMaxLength(128);
            this.Property(u => u.Description).IsOptional().HasMaxLength(512);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
            //
            this.Property(u => u.MaxLoad).IsOptional();
            this.Property(u => u.BookingShowMode).IsRequired();
            this.Property(u => u.ApplyHaltPeriod).IsOptional();
            this.Property(u => u.StartDt).IsOptional();
            this.Property(u => u.EndDt).IsOptional();
            this.Property(u => u.WorkStationIP).IsOptional().HasMaxLength(128);
        }
    }
}
