using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class PatientCaseHistoryMapper : EntityTypeConfiguration<PatientCaseHistory>
    {
        public PatientCaseHistoryMapper()
        {
            this.ToTable("dbo.tbPatientCaseHistory");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.PatientCaseID).IsRequired().HasColumnName("PatientCaseID").HasMaxLength(128);
            this.Property(u => u.PatientNo).IsRequired().HasColumnName("PatientID").HasMaxLength(128);

            this.Property(u => u.HospitalId).HasColumnName("HospitalID").HasMaxLength(128).IsOptional();
            this.Property(u => u.PatientName).HasColumnName("Name").HasMaxLength(128).IsOptional();
            this.Property(u => u.PatientNamePy).HasColumnName("PYName").HasMaxLength(128).IsOptional();
            this.Property(u => u.IdentityCard).HasColumnName("IdentityCard").HasMaxLength(64).IsOptional();
            this.Property(u => u.InsuranceNumber).HasColumnName("InsuranceNumber").HasMaxLength(128).IsOptional();

            this.Property(u => u.Gender).HasColumnName("Gender").HasMaxLength(16).IsOptional();
            this.Property(u => u.Birthday).HasColumnName("Birthday").IsOptional();
            this.Property(u => u.Address).HasColumnName("Address").HasMaxLength(64).IsOptional();
            this.Property(u => u.Telephone).HasColumnName("Telephone").HasMaxLength(64).IsOptional();
            this.Property(u => u.InsuranceNumber).HasColumnName("InsuranceNumber").HasMaxLength(128).IsOptional();
            this.Property(u => u.ClinicalDiagnosis).HasColumnName("ClinicalDiagnosis").IsMaxLength().IsOptional();
            this.Property(u => u.History).HasColumnName("History").IsMaxLength().IsOptional();

            this.Property(u => u.Progress).HasColumnName("Progress").IsOptional();

            this.Property(u => u.Creator).HasColumnName("Creator").IsRequired().HasMaxLength(128);
            this.Property(u => u.CreatorName).HasColumnName("CreatorName").IsOptional().HasMaxLength(128);
            this.Property(u => u.CreateTime).HasColumnName("CreateTime").IsRequired();
            this.Property(u => u.Status).HasColumnName("Status").IsRequired();

            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired();

            this.Property(u => u.IsDeleted).HasColumnName("IsDeleted").IsRequired();
            this.Property(u => u.DeleteTime).HasColumnName("DeleteTime").IsOptional();
            this.Property(u => u.DeleteReason).HasColumnName("DeleteReason").IsOptional();
            this.Property(u => u.DeleteUser).HasColumnName("DeleteUser").IsOptional().HasMaxLength(128);
            this.Property(u => u.DeletePublicAccountName).HasColumnName("DeletePublicAccountName").IsOptional().HasMaxLength(128);
        }
    }
}
