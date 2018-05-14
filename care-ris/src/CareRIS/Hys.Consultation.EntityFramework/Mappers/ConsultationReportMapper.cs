using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class ConsultationReportMapper : EntityTypeConfiguration<ConsultationReport>
    {
        public ConsultationReportMapper()
        {
            this.ToTable("dbo.tbConsultationReport");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ConsultationReportID").HasMaxLength(128);
            this.Property(u => u.RequestID).IsRequired().HasMaxLength(128).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("ConsultationReport_RequestID_Index", 1) { IsUnique = false }));
            this.Property(u => u.EditorID).IsOptional().HasMaxLength(128);
            this.Property(u => u.Advice).IsOptional();
            this.Property(u => u.Description).IsOptional();
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();
        }
    }
}
