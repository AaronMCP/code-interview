using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class EMRItemMapper : EntityTypeConfiguration<EMRItem>
    {
        public EMRItemMapper()
        {
            this.ToTable("dbo.tbEmrItem");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("EMRItemID").HasMaxLength(128);
            this.Property(u => u.PatientCaseID).IsRequired().HasMaxLength(128);
            this.Property(u => u.PatientNo).IsOptional().HasMaxLength(128);
            this.Property(u => u.EMRItemType).IsOptional().HasMaxLength(64);
            this.Property(u => u.EMRName).IsOptional().HasMaxLength(128);
            this.Property(u => u.ExamDate).IsOptional().HasMaxLength(64);
            this.Property(u => u.ReportDate).IsOptional().HasMaxLength(64);
            this.Property(u => u.BodyPart).IsOptional().HasMaxLength(128);
            this.Property(u => u.ExamModality).IsOptional().HasMaxLength(64);
            this.Property(u => u.AccessionNo).IsOptional().HasMaxLength(256);
            this.Property(u => u.ProcedureCode).IsOptional().HasMaxLength(128);
            this.Property(u => u.ExamSection).IsOptional().HasMaxLength(128);
            this.Property(u => u.ExamDescription).IsOptional().HasMaxLength(512);
            this.Property(u => u.Observation).IsOptional();
            this.Property(u => u.Conclusion).IsOptional();
            this.Property(u => u.AuthorDoctor).IsOptional().HasMaxLength(128);
            this.Property(u => u.Authenticator).IsOptional().HasMaxLength(128);
            this.Property(u => u.Progress).IsOptional();
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();
        }
    }
}
