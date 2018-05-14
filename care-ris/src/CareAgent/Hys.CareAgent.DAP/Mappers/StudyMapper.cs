
namespace Hys.CareAgent.DAP.Mappers
{
    using System.Data.Entity.ModelConfiguration;
    using Hys.CareAgent.DAP.Entity;

    class StudyMapper : EntityTypeConfiguration<Study>
    {
        public StudyMapper()
        {
            this.ToTable("dbo.tStudy");
            this.HasKey(u => u.StudyInstanceUID);
            this.Property(u => u.StudyInstanceUID).IsRequired().HasMaxLength(256);
            this.Property(u => u.PatientID).IsOptional().HasMaxLength(256);
            this.Property(u => u.PatientName).IsOptional().HasMaxLength(128);
            this.Property(u => u.PatientDOB).IsOptional().HasMaxLength(64);
            this.Property(u => u.PatientAge).IsOptional().HasMaxLength(64);
            this.Property(u => u.PatientSex).IsOptional().HasMaxLength(64);
            this.Property(u => u.AccessionNo).IsOptional().HasMaxLength(256);
            this.Property(u => u.BodyPart).IsOptional().HasMaxLength(64);
            this.Property(u => u.Modality).IsOptional().HasMaxLength(64);
            this.Property(u => u.ExamCode).IsOptional().HasMaxLength(128);
            this.Property(u => u.StudyDate).IsOptional().HasMaxLength(32);
            this.Property(u => u.StudyTime).IsOptional().HasMaxLength(32);
            this.Property(u => u.StudyDescription).IsOptional().HasMaxLength(256);
            this.Property(u => u.ReferPhysician).IsOptional().HasMaxLength(128);
            this.Property(u => u.ReceiveTime).IsOptional();
        }
    }
}



