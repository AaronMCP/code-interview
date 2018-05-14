using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class ConsultationReportHistoryMapper : EntityTypeConfiguration<ConsultationReportHistory>
    {
        public ConsultationReportHistoryMapper()
        {
            this.ToTable("dbo.tbConsultationReportHistory");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ConsultationReportHistoryID").HasMaxLength(128);
            this.Property(u => u.ConsultationReportID).IsRequired().HasMaxLength(128);
            this.Property(u => u.RequestID).IsRequired().HasMaxLength(128);
            this.Property(u => u.EditorID).IsOptional().HasMaxLength(128);
            this.Property(u => u.Advice).IsOptional();
            this.Property(u => u.Description).IsOptional();
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();
        }
    }
}
