using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class RequestItemMapper : EntityTypeConfiguration<RequestItem>
    {
        public RequestItemMapper()
        {
            this.ToTable("dbo.tbRequestItem");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("RequestItemID").HasMaxLength(64);
            this.Property(u => u.RequestID).IsOptional().HasMaxLength(64);
            this.Property(u => u.RequestItemUID).IsOptional().HasMaxLength(64);
            this.Property(u => u.ModalityType).IsOptional().HasMaxLength(64);
            this.Property(u => u.Modality).IsOptional().HasMaxLength(64);
            this.Property(u => u.ProcedureCode).IsOptional().HasMaxLength(64);
            this.Property(u => u.RPDesc).IsOptional().HasMaxLength(254);
            this.Property(u => u.ExamSystem).IsOptional().HasMaxLength(64);
            this.Property(u => u.ScheduleTime).IsOptional().HasMaxLength(64);
            this.Property(u => u.Comment).IsOptional().HasMaxLength(512);
            this.Property(u => u.TeethName).IsOptional().HasMaxLength(64);
            this.Property(u => u.TeethCode).IsOptional().HasMaxLength(64);
            this.Property(u => u.TeethCount).HasColumnName("Teethcount").IsOptional();
            this.Property(u => u.AccNo).IsOptional().HasMaxLength(32);
            this.Property(u => u.Status).IsOptional().HasMaxLength(32);      
        }
    }
}
