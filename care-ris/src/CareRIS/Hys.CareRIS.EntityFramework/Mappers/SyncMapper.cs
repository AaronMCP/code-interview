using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class SyncMapper : EntityTypeConfiguration<Sync>
    {
        public SyncMapper()
        {
            this.ToTable("dbo.tbSync");
            this.HasKey(u => new { u.OrderID, u.SyncType});
            this.Property(u => u.OrderID).IsRequired().HasColumnName("Guid").HasMaxLength(128);
            this.Property(u => u.SyncType).IsRequired();
            this.Property(u => u.Owner).IsOptional().HasMaxLength(128);
            this.Property(u => u.OwnerIP).IsOptional().HasMaxLength(128);
            this.Property(u => u.CreateTime).IsOptional().HasColumnName("CreateDt");
            this.Property(u => u.ModuleID).IsOptional().HasMaxLength(128);
            this.Property(u => u.PatientID).IsOptional().HasMaxLength(128);
            this.Property(u => u.PatientName).IsOptional().HasMaxLength(128);
            this.Property(u => u.AccNo).IsOptional().HasMaxLength(128);
            this.Property(u => u.Counter).IsOptional();
            this.Property(u => u.ProcedureIDs).IsOptional().HasColumnName("RPGuids").HasMaxLength(1024);
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
        }
    }
}
