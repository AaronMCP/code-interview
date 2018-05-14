using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos.UserManagement
{
    public class UserPwdDto
    {
        public string UserID { get; set; }
    
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

    }
}
