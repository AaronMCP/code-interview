using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class RoleProfileDto : SystemProfileDto
    {
        public string RoleName { get; set; }
        public string ModuleName { get; set; }
    }
}
