using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class InitialDataHistoryMapper : EntityTypeConfiguration<InitialDataHistory>
    {
        public InitialDataHistoryMapper()
        {
            this.ToTable("dbo.tbInitialDataHistory");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ConsultationReportHistoryID").HasMaxLength(128);
            this.Property(u => u.Version).IsRequired().HasMaxLength(128);
            this.Property(u => u.IsUpdated).IsRequired();
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsOptional().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsOptional();
        }
    }
}
