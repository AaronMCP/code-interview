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
    public class ConsultationReceiverXRefMapper : EntityTypeConfiguration<ConsultationReceiverXRef>
    {
        public ConsultationReceiverXRefMapper()
        {
            this.ToTable("dbo.tbConsultationReceiverXRef");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ConsultationReceiverXRefID").HasMaxLength(128);
            this.Property(u => u.ConsultationRequestID).IsRequired().HasMaxLength(128).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("ConsultationReceiver_RequestID_Index", 1) { IsUnique = false }));
            this.Property(u => u.ConsultationRequestID).IsRequired().HasMaxLength(128);
            this.Property(u => u.ReceiverID).IsRequired().HasMaxLength(128);
       
        }
    }
}
