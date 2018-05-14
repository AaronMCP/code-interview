using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class IDMaxValueMapper : EntityTypeConfiguration<IDMaxValue>
    {
        public IDMaxValueMapper()
        {
            this.ToTable("dbo.tbIdMaxValue");
            this.HasKey(u => new { u.Tag, u.Domain });

            this.Property(u => u.Tag).IsRequired();
            this.Property(u => u.Value).IsOptional();
            this.Property(u => u.CreateDate).HasColumnName("CreateDt").IsOptional();
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.ModalityType).IsOptional().HasMaxLength(32);
            this.Property(u => u.LocationAccNoPrefix).IsOptional().HasMaxLength(32);
        }
    }
}
