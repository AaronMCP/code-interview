using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos
{
    public class UserSettingDto
    {
        public string UserSettingID { get; set; }
        public string RoleID { get; set; }
        public string UserID { get; set; }
        public int Type { get; set; }
        public string SettingValue { get; set; }
    }
}
