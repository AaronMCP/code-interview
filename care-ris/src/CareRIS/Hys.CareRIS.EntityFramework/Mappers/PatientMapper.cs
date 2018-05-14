using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class PatientMapper : EntityTypeConfiguration<Patient>
    {
        public PatientMapper()
        {
            this.ToTable("dbo.tbRegPatient");
            this.HasKey(u => u.UniqueID);

            this.Property(u => u.UniqueID).IsRequired().HasColumnName("PatientGuid").HasMaxLength(128);
            this.Property(u => u.PatientNo).IsRequired().HasColumnName("PatientID").HasMaxLength(128);
            this.Property(u => u.LocalName).IsOptional().HasMaxLength(128);
            this.Property(u => u.EnglishName).IsOptional().HasMaxLength(512);
            this.Property(u => u.ReferenceNo).IsOptional().HasMaxLength(128);
            this.Property(u => u.Birthday).IsOptional();
            this.Property(u => u.Gender).IsOptional().HasMaxLength(128);
            this.Property(u => u.Address).IsOptional().HasMaxLength(256);
            this.Property(u => u.Telephone).IsOptional().HasMaxLength(128);
            this.Property(u => u.IsVIP).IsOptional();
            this.Property(u => u.CreateTime).IsOptional().HasColumnName("CreateDt");
            this.Property(u => u.Comments).IsOptional();
            this.Property(u => u.RemotePID).IsOptional().HasMaxLength(128);
            //this.Property(u => u.Optional1).IsOptional().HasMaxLength(512);
            //this.Property(u => u.Optional2).IsOptional().HasMaxLength(512);
            //this.Property(u => u.Optional3).IsOptional().HasMaxLength(512);
            this.Property(u => u.Marriage).IsOptional().HasMaxLength(32);
            this.Property(u => u.Alias).IsOptional().HasMaxLength(32);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.GlobalID).IsOptional().HasMaxLength(64);
            this.Property(u => u.MedicareNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.ParentName).IsOptional().HasMaxLength(128);
            this.Property(u => u.RelatedID).IsOptional().HasMaxLength(64);
            this.Property(u => u.Site).IsOptional().HasMaxLength(64);
            this.Property(u => u.SocialSecurityNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.UpdateTime).IsOptional();
            this.Property(u => u.IsUploaded).HasColumnName("Uploaded").IsOptional();
        }
    }
}
