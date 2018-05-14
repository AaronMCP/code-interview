using Hys.CareRIS.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Hys.CareRIS.EntityFramework.Mappers
{
    public class RequestChargeMapper : EntityTypeConfiguration<RequestCharge>
    {
        public RequestChargeMapper()
        {
            this.ToTable("dbo.tbRequestCharge");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("RequestChargeID").HasMaxLength(64);
            this.Property(u => u.RequestID).IsOptional().HasMaxLength(64);
            this.Property(u => u.RequestItemID).IsOptional().HasMaxLength(64);
            this.Property(u => u.ItemCode).IsOptional().HasMaxLength(64);
            this.Property(u => u.ItemName).IsOptional().HasMaxLength(64);
            this.Property(u => u.Price).IsOptional();
            this.Property(u => u.Amount).IsOptional();
            this.Property(u => u.IsItemCharged).IsOptional();
        }
    }
}
