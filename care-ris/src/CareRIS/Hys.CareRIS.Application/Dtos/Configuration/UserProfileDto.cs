using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class UserProfileDto : SystemProfileDto
    {
        public string UserID { get; set; }
        public string RoleName { get; set; }
    }
}
