using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Domain.Entities
{
    public class UserSetting : Entity
    {
        public override object UniqueId { get { return UserSettingID; } }

        public string UserSettingID { get; set; }
        public string RoleID { get; set; }
        public string UserID { get; set; }
        public int Type { get; set; }
        public string SettingValue { get; set; }
    }
}
