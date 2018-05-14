using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class ModalityTypeMapper : EntityTypeConfiguration<ModalityType>
    {
        public ModalityTypeMapper()
        {
            this.ToTable("dbo.tbModalityType");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.Modalitytype).IsRequired().HasMaxLength(128);
            this.Property(u => u.Sopclass).IsRequired().HasMaxLength(512);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
        }
    }
}
