using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class GWReportMapper : EntityTypeConfiguration<GWReport>
    {
        public GWReportMapper()
        {
            this.ToTable("dbo.tbGwReport");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("DATA_ID").HasMaxLength(128);
            this.Property(u => u.DataTime).IsRequired().HasColumnName("DATA_DT");
            this.Property(u => u.ReportNo).IsRequired().HasColumnName("REPORT_NO").HasMaxLength(128);
            this.Property(u => u.AccessionNumber).IsRequired().HasColumnName("ACCESSION_NUMBER").HasMaxLength(128);
            this.Property(u => u.PatientID).IsRequired().HasColumnName("PATIENT_ID").HasMaxLength(128);
            this.Property(u => u.ReportStatus).IsRequired().HasColumnName("REPORT_STATUS").HasMaxLength(128);
            this.Property(u => u.Modality).IsOptional().HasColumnName("MODALITY").HasMaxLength(128);
            this.Property(u => u.ReportType).IsRequired().HasColumnName("REPORT_TYPE");
            this.Property(u => u.ReportFile).IsOptional().HasColumnName("REPORT_FILE").HasMaxLength(512);
            this.Property(u => u.Diagnose).IsOptional().HasColumnName("DIAGNOSE");
            this.Property(u => u.Comments).IsOptional().HasColumnName("COMMENTS");
            this.Property(u => u.ReportWriter).IsOptional().HasColumnName("REPORT_WRITER").HasMaxLength(128);
            this.Property(u => u.ReportIntepreter).IsOptional().HasColumnName("REPORT_INTEPRETER").HasMaxLength(128);
            this.Property(u => u.ReportApprover).IsOptional().HasColumnName("REPORT_APPROVER").HasMaxLength(128);
            this.Property(u => u.ReportTime).IsOptional().HasColumnName("REPORTDT").HasMaxLength(128);
            this.Property(u => u.ObservationMethod).IsOptional().HasColumnName("OBSERVATIONMETHOD");
            this.Property(u => u.ReportTime).IsOptional().HasColumnName("REPORTDT").HasMaxLength(128);
            this.Property(u => u.Customer1).IsOptional().HasColumnName("CUSTOMER_1");
            this.Property(u => u.Customer2).IsOptional().HasColumnName("CUSTOMER_2");
            this.Property(u => u.Customer3).IsOptional().HasColumnName("CUSTOMER_3");
            this.Property(u => u.Customer4).IsOptional().HasColumnName("CUSTOMER_4");

        }
    }
}
