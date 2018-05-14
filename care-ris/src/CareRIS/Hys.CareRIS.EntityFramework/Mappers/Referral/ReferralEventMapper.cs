using System.Data.Entity.ModelConfiguration;
using Hys.CareRIS.Domain.Entities.Referral;

namespace Hys.CareRIS.EntityFramework.Mappers.Referral
{
    public class ReferralEventMapper : EntityTypeConfiguration<ReferralEvent>
    {
        public ReferralEventMapper()
        {
            this.ToTable("dbo.tbReferralEvent");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("EventGuid");
            this.Property(u => u.ReferralID).IsRequired().HasMaxLength(64);
            this.Property(u => u.SourceDomain).IsOptional().HasMaxLength(64);
            this.Property(u => u.TargetDomain).IsOptional().HasMaxLength(64);
            this.Property(u => u.Memo).IsOptional().HasMaxLength(512);
            this.Property(u => u.Event).IsOptional();
            this.Property(u => u.Status).IsOptional();
            this.Property(u => u.ExamDomain).IsOptional().HasMaxLength(64);
            this.Property(u => u.ExamAccNo).IsOptional().HasMaxLength(64);
            this.Property(u => u.OperatorGuid).IsOptional().HasMaxLength(64);
            this.Property(u => u.OperatorName).IsOptional().HasMaxLength(64);
            this.Property(u => u.OperateDt).IsOptional();
            this.Property(u => u.Tag).IsOptional();
            this.Property(u => u.Content).IsOptional();
            this.Property(u => u.Scope).IsOptional();
            this.Property(u => u.SourceSite).IsOptional().HasMaxLength(64);
            this.Property(u => u.TargetSite).IsOptional().HasMaxLength(64);
        }
    }
}
