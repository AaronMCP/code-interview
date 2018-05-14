using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class GWOrderMapper : EntityTypeConfiguration<GWOrder>
    {
        public GWOrderMapper()
        {
            this.ToTable("dbo.tbGwOrder");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("DATA_ID").HasMaxLength(128);
            this.Property(u => u.DataTime).IsRequired().HasColumnName("DATA_DT");
            this.Property(u => u.OrderNo).IsRequired().HasColumnName("ORDER_NO").HasMaxLength(128);
            this.Property(u => u.PlacerNo).IsOptional().HasColumnName("PLACER_NO").HasMaxLength(128);
            this.Property(u => u.FillerNo).IsOptional().HasColumnName("FILLER_NO").HasMaxLength(128);
            this.Property(u => u.SeriesNo).IsOptional().HasColumnName("SERIES_NO").HasMaxLength(128);
            this.Property(u => u.PatientID).IsRequired().HasColumnName("PATIENT_ID").HasMaxLength(128);
            this.Property(u => u.ExamStatus).IsRequired().HasColumnName("EXAM_STATUS").HasMaxLength(128);
            this.Property(u => u.PlacerDepartment).IsOptional().HasColumnName("PLACER_DEPARTMENT").HasMaxLength(128);
            this.Property(u => u.Placer).IsOptional().HasColumnName("PLACER").HasMaxLength(128);
            this.Property(u => u.PlacerContact).IsOptional().HasColumnName("PLACER_CONTACT").HasMaxLength(128);
            this.Property(u => u.FillerDepartment).IsOptional().HasColumnName("FILLER_DEPARTMENT").HasMaxLength(128);
            this.Property(u => u.Filler).IsOptional().HasColumnName("FILLER").HasMaxLength(128);
            this.Property(u => u.FillerContact).IsOptional().HasColumnName("FILLER_CONTACT").HasMaxLength(128);
            this.Property(u => u.RefOrganization).IsOptional().HasColumnName("REF_ORGANIZATION").HasMaxLength(128);
            this.Property(u => u.RefPhysician).IsOptional().HasColumnName("REF_PHYSICIAN").HasMaxLength(128);
            this.Property(u => u.RefContact).IsOptional().HasColumnName("REF_CONTACT").HasMaxLength(128);
            this.Property(u => u.RequestReason).IsOptional().HasColumnName("REQUEST_REASON").HasMaxLength(128);
            this.Property(u => u.ReuqestComments).IsOptional().HasColumnName("REUQEST_COMMENTS").HasMaxLength(128);
            this.Property(u => u.ExamRequirement).IsOptional().HasColumnName("EXAM_REQUIREMENT").HasMaxLength(128);
            this.Property(u => u.ScheduledTime).IsOptional().HasColumnName("SCHEDULED_DT").HasMaxLength(128);
            this.Property(u => u.Modality).IsOptional().HasColumnName("MODALITY").HasMaxLength(128);
            this.Property(u => u.StationName).IsOptional().HasColumnName("STATION_NAME").HasMaxLength(128);
            this.Property(u => u.StationAETitle).IsOptional().HasColumnName("STATION_AETITLE").HasMaxLength(128);
            this.Property(u => u.ExamLocation).IsOptional().HasColumnName("EXAM_LOCATION").HasMaxLength(128);
            this.Property(u => u.ExamVolume).IsOptional().HasColumnName("EXAM_VOLUME").HasMaxLength(128);
            this.Property(u => u.ExamTime).IsOptional().HasColumnName("EXAM_DT").HasMaxLength(128);
            this.Property(u => u.Duration).IsOptional().HasColumnName("DURATION").HasMaxLength(128);
            this.Property(u => u.TransportArrange).IsOptional().HasColumnName("TRANSPORT_ARRANGE").HasMaxLength(128);
            this.Property(u => u.Technician).IsOptional().HasColumnName("TECHNICIAN").HasMaxLength(128);
            this.Property(u => u.BodyPart).IsOptional().HasColumnName("BODY_PART").HasMaxLength(256);
            this.Property(u => u.ProcedureName).IsOptional().HasColumnName("PROCEDURE_NAME").HasMaxLength(128);
            this.Property(u => u.ProcedureCode).IsOptional().HasColumnName("PROCEDURE_CODE").HasMaxLength(128);
            this.Property(u => u.ProcedureDesc).IsOptional().HasColumnName("PROCEDURE_DESC").HasMaxLength(128);
            this.Property(u => u.StudyInstanceUID).IsOptional().HasColumnName("STUDY_INSTANCE_UID").HasMaxLength(128);
            this.Property(u => u.StudyID).IsOptional().HasColumnName("STUDY_ID").HasMaxLength(128);
            this.Property(u => u.RefClassUID).IsOptional().HasColumnName("REF_CLASS_UID").HasMaxLength(128);
            this.Property(u => u.ExamComment).IsOptional().HasColumnName("EXAM_COMMENT").HasMaxLength(128);
            this.Property(u => u.CNTAgent).IsOptional().HasColumnName("CNT_AGENT").HasMaxLength(128);
            this.Property(u => u.ChargeStatus).IsOptional().HasColumnName("CHARGE_STATUS").HasMaxLength(128);
            this.Property(u => u.ChargeAmount).IsOptional().HasColumnName("CHARGE_AMOUNT").HasMaxLength(128);
           
            this.Property(u => u.Customer1).IsOptional().HasColumnName("CUSTOMER_1");
            this.Property(u => u.Customer2).IsOptional().HasColumnName("CUSTOMER_2");
            this.Property(u => u.Customer3).IsOptional().HasColumnName("CUSTOMER_3");
            this.Property(u => u.Customer4).IsOptional().HasColumnName("CUSTOMER_4");
            

        }
    }
}
