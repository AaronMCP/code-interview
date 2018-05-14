using System.Data.Entity.ModelConfiguration;
using Hys.CareRIS.Domain.Entities.Referral;

namespace Hys.CareRIS.EntityFramework.Mappers.Referral
{
    public class ReferralLogMapper : EntityTypeConfiguration<ReferralLog>
    {
        public ReferralLogMapper()
        {
            this.ToTable("dbo.tbReferralLog");
            this.HasKey(u => new { u.ReferralID, u.OperateDt });
            this.Property(u => u.ReferralID).IsRequired().HasMaxLength(64);
            this.Property(u => u.SourceDomain).IsOptional().HasMaxLength(64);
            this.Property(u => u.TargetDomain).IsOptional().HasMaxLength(64);
            this.Property(u => u.Memo).IsOptional().HasMaxLength(512);
            this.Property(u => u.OperatorGuid).IsOptional().HasMaxLength(64);
            this.Property(u => u.OperatorName).IsOptional().HasMaxLength(64);
            this.Property(u => u.OperateDt).IsRequired();
            this.Property(u => u.CreateDt).IsOptional();

            this.Property(u => u.SourceSite).IsOptional().HasMaxLength(64);
            this.Property(u => u.TargetSite).IsOptional().HasMaxLength(64);

            this.Property(u => u.EventDesc).IsOptional().HasMaxLength(128);
            this.Property(u => u.RefPurpose).IsOptional();
        }
    }
}
