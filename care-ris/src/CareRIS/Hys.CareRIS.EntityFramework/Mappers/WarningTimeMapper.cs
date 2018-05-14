using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class WarningTimeMapper : EntityTypeConfiguration<WarningTime>
    {
        public WarningTimeMapper()
        {
            this.ToTable("dbo.tbWarningTime");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasMaxLength(128);
            this.Property(u => u.ModalityType).IsRequired().HasMaxLength(128);
            this.Property(u => u.Type).IsRequired().HasMaxLength(128);
            this.Property(u => u.PatientType).IsRequired().HasMaxLength(128);
            this.Property(u => u.WarningTimeValue).HasColumnName("WarningTime").IsOptional();
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
        }
    }
}
