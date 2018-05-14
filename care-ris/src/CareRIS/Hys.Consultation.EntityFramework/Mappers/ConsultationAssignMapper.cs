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
    public class ConsultationAssignMapper : EntityTypeConfiguration<ConsultationAssign>
    {
        public ConsultationAssignMapper()
        {
            this.ToTable("dbo.tbConsultationAssign");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ConsultationAssignID").HasMaxLength(128);
            this.Property(u => u.ConsultationRequestID).IsRequired().HasMaxLength(128).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("ConsultationAssign_RequestID_Index", 1) { IsUnique = false }));
            this.Property(u => u.AssignedUserID).IsRequired().HasMaxLength(128);
            this.Property(u => u.AssignedTime).IsRequired();
            this.Property(u => u.IsHost).IsRequired();
            this.Property(u => u.Comments).IsOptional();
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();
        }
    }
}
