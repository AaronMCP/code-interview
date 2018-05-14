using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ScanningTechMapper : EntityTypeConfiguration<ScanningTech>
    {
        public ScanningTechMapper()
        {
            this.ToTable("dbo.tbScanningTech");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ScanningTechGuid").HasMaxLength(128);
            this.Property(u => u.ScanningTechName).IsRequired().HasColumnName("ScanningTech").HasMaxLength(512); 
            this.Property(u => u.ModalityType).IsRequired().HasMaxLength(128);
            this.Property(u => u.ParentId).IsRequired().HasColumnName("ParentScanningTechGuid").HasMaxLength(64);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
        }
    }
}
