using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.EntityFramework.Mappers
{
    class ServiceTypeMappper : EntityTypeConfiguration<ServiceType>
    {
        public ServiceTypeMappper()
        {
            this.ToTable("dbo.tbServiceType");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ServiceTypeID").HasMaxLength(128);
            this.Property(u => u.Name).IsRequired().HasMaxLength(64);
            this.Property(u => u.Description).IsOptional().IsMaxLength();
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();
        }
    }
}
