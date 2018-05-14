using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class RequestMapper : EntityTypeConfiguration<Request>
    {
        public RequestMapper()
        {
            this.ToTable("dbo.tbRequest");
            //[EAcquisition]
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("RequestID").HasMaxLength(64);
            this.Property(u => u.ErNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.RequestType).IsOptional().HasMaxLength(16);
            this.Property(u => u.EventCode).IsOptional();
            this.Property(u => u.PatientID).IsOptional().HasMaxLength(64);
            this.Property(u => u.LocalName).IsOptional().HasMaxLength(128);
            this.Property(u => u.EnglishName).IsOptional().HasMaxLength(512);
            this.Property(u => u.Gender).IsOptional().HasMaxLength(128);
            this.Property(u => u.InhospitalRegion).IsOptional().HasMaxLength(64);
            this.Property(u => u.InhospitalNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.ClinicNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.BedNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.Birthday).IsOptional();
            this.Property(u => u.Telephone).IsOptional().HasMaxLength(64);
            this.Property(u => u.Address).IsOptional().HasMaxLength(64);
            this.Property(u => u.ApplyDept).IsOptional().HasMaxLength(64);
            this.Property(u => u.ApplyDoctor).IsOptional().HasMaxLength(64);
            this.Property(u => u.Observation).IsOptional().HasMaxLength(512);
            this.Property(u => u.HealthHistory).IsOptional().HasMaxLength(512);
            this.Property(u => u.IdentityNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.SocialSecurityNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.HisID).HasColumnName("Hisid").IsOptional().HasMaxLength(64);
            this.Property(u => u.CardNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.ChargeType).IsOptional().HasMaxLength(32);
            this.Property(u => u.PatientType).IsOptional().HasMaxLength(64);
            this.Property(u => u.Priority).IsOptional();
            this.Property(u => u.Reason).IsOptional().HasMaxLength(512);
            this.Property(u => u.GlobalID).IsOptional().HasMaxLength(64);
            this.Property(u => u.MedicareNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.WebAcquisitionURL).IsOptional().HasMaxLength(512);
            this.Property(u => u.RequestTime).HasColumnName("RequestDt").IsOptional();
            this.Property(u => u.Domain).IsOptional().HasMaxLength(64);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.RISPatientID).IsOptional().HasMaxLength(64);
            this.Property(u => u.EAcquisition).IsOptional();
        }
    }
}
