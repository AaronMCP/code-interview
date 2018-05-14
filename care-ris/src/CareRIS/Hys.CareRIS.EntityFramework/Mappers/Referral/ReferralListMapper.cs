using System.Data.Entity.ModelConfiguration;
using Hys.CareRIS.Domain.Entities.Referral;

namespace Hys.CareRIS.EntityFramework.Mappers.Referral
{
    public class ReferralListMapper : EntityTypeConfiguration<ReferralList>
    {
        public ReferralListMapper()
        {
            this.ToTable("dbo.tbReferralList");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasColumnName("ReferralID");
        }
    }
}
