using System.Data.Entity.ModelConfiguration;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class NotificationConfigMapper : EntityTypeConfiguration<NotificationConfig>
    {
        public NotificationConfigMapper()
        {
            this.ToTable("dbo.tbNotificationConfig");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasMaxLength(128);
            this.Property(u => u.Event).IsRequired();
            this.Property(u => u.IsDefault).IsRequired();
            this.Property(u => u.IsEnable).IsRequired();
            this.Property(u => u.SiteID).IsRequired().HasMaxLength(128);
            this.Property(u => u.Template).IsRequired();
            this.Property(u => u.Variables).IsRequired();
            
            this.Property(u => u.NotifyTypes);
            this.Property(u => u.SendingTimes);

            this.Property(u => u.LastEditTime).IsRequired();
            this.Property(u => u.LastEditUser).IsRequired().HasMaxLength(128);


        }
    }
}
