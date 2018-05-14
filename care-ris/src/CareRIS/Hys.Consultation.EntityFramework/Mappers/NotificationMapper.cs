using System.Data.Entity.ModelConfiguration;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class NotificationMapper : EntityTypeConfiguration<Notification>
    {
        public NotificationMapper()
        {
            this.ToTable("dbo.tbNotification");
            this.HasKey(u => u.UniqueID);
            this.Property(u => u.UniqueID).IsRequired().HasMaxLength(128);
            this.Property(u => u.IsSended).IsRequired();
            this.Property(u => u.NotifyType).IsRequired();
            this.Property(u => u.Recipients).IsRequired();
            this.Property(u => u.Text).IsRequired();

            this.Property(u => u.OwnerID).IsOptional().HasMaxLength(128);
            this.Property(u => u.Sender).IsOptional().HasMaxLength(128);
            this.Property(u => u.PendingTime).IsOptional();
            this.Property(u => u.Result).IsOptional();
            this.Property(u => u.Event).IsOptional();

            this.Property(u => u.LastEditTime).IsRequired();
            this.Property(u => u.LastEditUser).IsRequired().HasMaxLength(128);
        }
    }
}
