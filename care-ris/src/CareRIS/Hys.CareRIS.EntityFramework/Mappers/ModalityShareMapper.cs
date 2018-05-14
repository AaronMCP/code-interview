using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ModalityShareMapper : EntityTypeConfiguration<ModalityShare>
    {
        public ModalityShareMapper()
        {
            this.ToTable("dbo.tbModalityShare");
            this.HasKey(u => u.Guid);

            this.Property(u => u.Guid).IsRequired().HasMaxLength(128);
            this.Property(u => u.TimeSliceGuid).IsRequired().HasMaxLength(128);
            this.Property(u => u.ShareTarget).IsRequired().HasMaxLength(64);
            this.Property(u => u.TargetType).IsRequired();
            this.Property(u => u.MaxCount).IsRequired();
            this.Property(u => u.AvailableCount).IsRequired();
            this.Property(u => u.GroupId).IsRequired().HasMaxLength(128);
            this.Property(u => u.Date).IsOptional();

            this.Ignore<ModalityTimeSlice>(u => u.TimeSlice);
        }
    }
}
