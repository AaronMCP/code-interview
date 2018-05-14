using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class RequisitionMapper : EntityTypeConfiguration<Requisition>
    {
        public RequisitionMapper()
        {
            this.ToTable("dbo.tbRequisition");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("RequisitionGuid").HasMaxLength(128);
            this.Property(u => u.AccNo).IsRequired().HasMaxLength(128);
            this.Property(u => u.RelativePath).IsOptional().HasMaxLength(256);
            this.Property(u => u.FileName).IsOptional().HasMaxLength(128);
            this.Property(u => u.ScanTime).IsOptional().HasColumnName("ScanDt");
            this.Property(u => u.UpdateTime).IsOptional();
            this.Property(u => u.Uploaded).IsOptional();
            this.Property(u => u.Domain).IsRequired().HasMaxLength(64);
            this.Property(u => u.CreateTime).IsOptional().HasColumnName("Createdt");
            this.Property(u => u.ErNo).IsOptional().HasColumnName("Erno").HasMaxLength(256);
        }
    }
}
