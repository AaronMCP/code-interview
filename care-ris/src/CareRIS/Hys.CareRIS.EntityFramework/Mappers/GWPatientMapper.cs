using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class GWPatientMapper : EntityTypeConfiguration<GWPatient>
    {
        public GWPatientMapper()
        {
            this.ToTable("dbo.tbGwPatient");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("DATA_ID").HasMaxLength(128);
            this.Property(u => u.DataTime).IsRequired().HasColumnName("DATA_DT");
            this.Property(u => u.PatientID).IsRequired().HasColumnName("PATIENTID").HasMaxLength(128);
            this.Property(u => u.PriorPatientID).IsOptional().HasColumnName("PRIOR_PATIENT_ID").HasMaxLength(128);
            this.Property(u => u.OtherPID).IsOptional().HasColumnName("OTHER_PID").HasMaxLength(128);
            this.Property(u => u.PatientName).IsOptional().HasColumnName("PATIENT_NAME").HasMaxLength(128);
            this.Property(u => u.PatientLocalName).IsRequired().HasColumnName("PATIENT_LOCAL_NAME").HasMaxLength(128);
            this.Property(u => u.MotherMaidenName).IsOptional().HasColumnName("MOTHER_MAIDEN_NAME").HasMaxLength(128);
            this.Property(u => u.BirthDate).IsRequired().HasColumnName("BIRTHDATE").HasMaxLength(128);
            this.Property(u => u.Sex).IsRequired().HasColumnName("SEX").HasMaxLength(128);
            this.Property(u => u.PatientAlias).IsOptional().HasColumnName("PATIENT_ALIAS").HasMaxLength(128);
            this.Property(u => u.Race).IsOptional().HasColumnName("RACE").HasMaxLength(128);
            this.Property(u => u.Address).IsOptional().HasColumnName("ADDRESS");
            this.Property(u => u.CountryCode).IsOptional().HasColumnName("COUNTRY_CODE").HasMaxLength(128);
            this.Property(u => u.PhoneNumberHome).IsOptional().HasColumnName("PHONENUMBER_HOME").HasMaxLength(128);
            this.Property(u => u.PhoneNumberBusiness).IsOptional().HasColumnName("PHONENUMBER_BUSINESS").HasMaxLength(128);
            this.Property(u => u.PrimaryLanguage).IsOptional().HasColumnName("PRIMARY_LANGUAGE").HasMaxLength(128);
            this.Property(u => u.MaritalStatus).IsOptional().HasColumnName("MARITAL_STATUS").HasMaxLength(128);
            this.Property(u => u.Religion).IsOptional().HasColumnName("RELIGION").HasMaxLength(128);
            this.Property(u => u.AccountNumber).IsOptional().HasColumnName("ACCOUNT_NUMBER").HasMaxLength(128);
            this.Property(u => u.SSNBumber).IsOptional().HasColumnName("SSN_NUMBER").HasMaxLength(128);
            this.Property(u => u.DriverLicNumber).IsOptional().HasColumnName("DRIVERLIC_NUMBER").HasMaxLength(128);
            this.Property(u => u.EthnicGroup).IsOptional().HasColumnName("ETHNIC_GROUP").HasMaxLength(128);
            this.Property(u => u.BirthPlace).IsOptional().HasColumnName("BIRTH_PLACE").HasMaxLength(128);
            this.Property(u => u.CitizenShip).IsOptional().HasColumnName("CITIZENSHIP").HasMaxLength(128);
            this.Property(u => u.VeteransMilStatus).IsOptional().HasColumnName("VETERANS_MIL_STATUS").HasMaxLength(128);
            this.Property(u => u.Nationality).IsOptional().HasColumnName("NATIONALITY").HasMaxLength(128);
            this.Property(u => u.PatientType).IsOptional().HasColumnName("PATIENT_TYPE").HasMaxLength(128);
            this.Property(u => u.PatientLocation).IsOptional().HasColumnName("PATIENT_LOCATION").HasMaxLength(128);
            this.Property(u => u.PatientStatus).IsOptional().HasColumnName("PATIENT_STATUS").HasMaxLength(128);
            this.Property(u => u.VisitNumber).IsOptional().HasColumnName("VISIT_NUMBER").HasMaxLength(128);
            this.Property(u => u.BedNumber).IsOptional().HasColumnName("BED_NUMBER").HasMaxLength(128);
            this.Property(u => u.Customer1).IsOptional().HasColumnName("CUSTOMER_1");
            this.Property(u => u.Customer2).IsOptional().HasColumnName("CUSTOMER_2");
            this.Property(u => u.Customer3).IsOptional().HasColumnName("CUSTOMER_3");
            this.Property(u => u.Customer4).IsOptional().HasColumnName("CUSTOMER_4");
            this.Property(u => u.PriorPatientName).IsOptional().HasColumnName("PRIOR_PATIENT_NAME").HasMaxLength(128);
            this.Property(u => u.PriorVisitNumber).IsOptional().HasColumnName("PRIOR_VISIT_NUMBER").HasMaxLength(128);

        }
    }
}
