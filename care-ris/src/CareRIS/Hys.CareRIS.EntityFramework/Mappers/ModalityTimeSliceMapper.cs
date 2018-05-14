using Hys.CareRIS.Domain;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ModalityTimeSliceMapper : EntityTypeConfiguration<ModalityTimeSlice>
    {
        public ModalityTimeSliceMapper()
        {
            this.ToTable("dbo.tbModalityTimeSlice");
            this.HasKey(u => u.TimeSliceGuid);

            this.Property(u => u.TimeSliceGuid).IsRequired().HasMaxLength(128);
            this.Property(u => u.ModalityType).IsOptional().HasMaxLength(128);
            this.Property(u => u.Modality).IsOptional().HasMaxLength(128);
            this.Property(u => u.StartDt).IsOptional();
            this.Property(u => u.EndDt).IsOptional();
            this.Property(u => u.Description).IsOptional().HasMaxLength(128);
            this.Property(u => u.MaxNumber).IsOptional();
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.DateType).IsOptional();
            this.Property(u => u.AvailableDate).IsOptional();
        }
    }
}
