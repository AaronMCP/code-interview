using Hys.Consultation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.EntityFramework.Mappers
{
    public class UserSettingMapper : EntityTypeConfiguration<UserSetting>
    {
        public UserSettingMapper()
        {
            this.ToTable("dbo.tbUserSetting");
            this.HasKey(u => u.UserSettingID);

            this.Property(u => u.UserSettingID).IsRequired().HasMaxLength(128);
            this.Property(u => u.RoleID).IsOptional().HasMaxLength(128);
            this.Property(u => u.UserID).IsOptional().HasMaxLength(128);
            this.Property(u => u.Type).IsOptional();
            this.Property(u => u.SettingValue).IsOptional().HasMaxLength(4000);
        }
    }
}
