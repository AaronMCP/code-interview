using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class ConsultationRequestMapper : EntityTypeConfiguration<ConsultationRequest>
    {
        public ConsultationRequestMapper()
        {
            this.ToTable("dbo.tbConsultationRequest");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ConsultationRequestID").HasMaxLength(128);
            this.Property(u => u.RequestUserID).IsRequired().HasColumnName("RequestUserID").HasMaxLength(128).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("ConsultationRequest_RequestUserID_Index", 1) { IsUnique = false }));
            this.Property(u => u.RequestHospitalID).IsRequired().HasColumnName("RequestHospitalID").HasMaxLength(128).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("ConsultationRequest_RequestHospitalID_Index", 1) { IsUnique = false }));
            this.Property(u => u.PatientCaseID).IsRequired().HasColumnName("PatientCaseID").HasMaxLength(128).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("ConsultationRequest_PatientCaseID_Index", 1) { IsUnique = false }));
            this.Property(u => u.ServiceTypeID).IsRequired().HasColumnName("ServiceTypeID").HasMaxLength(128);
            this.Property(u => u.ConsultantType).IsRequired().HasColumnName("ConsultantType").HasColumnType("int");

            this.Property(u => u.ExpectedDate).HasColumnName("ExpectedDate").IsOptional().HasColumnType("datetime");
            this.Property(u => u.ExpectedTimeRange).HasColumnName("ExpectedTimeRange").IsOptional().HasMaxLength(128);
            this.Property(u => u.RequestDescription).HasColumnName("RequestDescription").IsOptional();
            this.Property(u => u.RequestRequirement).HasColumnName("RequestRequirement").IsOptional();
            this.Property(u => u.Status).IsRequired().HasColumnName("Status").HasColumnType("int");
            this.Property(u => u.StatusUpdateTime).IsRequired().HasColumnName("StatusUpdateTime").HasColumnType("datetime");

            this.Property(u => u.OtherReason).HasColumnName("OtherReason").IsOptional();
            this.Property(u => u.ChangeReasonType).HasColumnName("ChangeReasonType").IsOptional();
            this.Property(u => u.AssignedBy).HasColumnName("AssignedBy").IsOptional().HasMaxLength(128);
            this.Property(u => u.AssignedDate).HasColumnName("AssignedDate").IsOptional().HasColumnType("datetime");
            this.Property(u => u.ConsultationDate).HasColumnName("ConsultationDate").IsOptional().HasColumnType("datetime");
            this.Property(u => u.ConsultationStartTime).HasColumnName("ConsultationStartTime").IsOptional().HasMaxLength(128);
            this.Property(u => u.ConsultationEndTime).HasColumnName("ConsultationEndTime").IsOptional().HasMaxLength(128);
            this.Property(u => u.MeetingRoom).HasColumnName("MeetingRoom").IsOptional().HasMaxLength(128);
            this.Property(u => u.AssignedDescription).HasColumnName("AssignedDescription").IsOptional();
            this.Property(u => u.SpecialComment).HasColumnName("SpecialComment").IsOptional();

            this.Property(u => u.ReceiveHospitalID).HasColumnName("ReceiveHospitalID").IsRequired().HasMaxLength(128).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("ConsultationRequest_ReceiveHospitalID_Index", 1) { IsUnique = false }));

            this.Property(u => u.RequestCreateDate).HasColumnName("RequestCreateDate").IsRequired().HasColumnType("datetime");
            this.Property(u => u.RequestCompleteDate).HasColumnName("RequestCompleteDate").IsOptional().HasColumnType("datetime");
            this.Property(u => u.ConsultationReportID).HasColumnName("ConsultationReportID").IsOptional().HasMaxLength(128).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("ConsultationRequest_ReportID_Index", 1) { IsUnique = false }));
            this.Property(u => u.RequestUserName).HasColumnName("RequestUserName").IsOptional().HasMaxLength(128);
            this.Property(u => u.LastEditUser).HasColumnName("LastEditUser").IsRequired().HasMaxLength(128);
            this.Property(u => u.LastEditUserName).HasColumnName("LastEditUserName").IsOptional().HasMaxLength(128);
            this.Property(u => u.LastEditTime).HasColumnName("LastEditTime").IsRequired().HasColumnType("datetime");
            this.Property(u => u.IsDeleted).HasColumnName("IsDeleted").IsRequired();
            this.Property(u => u.DeleteTime).HasColumnName("DeleteTime").IsOptional();
            this.Property(u => u.DeleteReason).HasColumnName("DeleteReason").IsOptional();
            this.Property(u => u.DeleteUser).HasColumnName("DeleteUser").IsOptional().HasMaxLength(128);
        }
    }
}
