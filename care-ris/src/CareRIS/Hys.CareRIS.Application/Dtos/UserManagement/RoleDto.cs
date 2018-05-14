using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos.UserManagement
{
    public class RoleDto
    {
        public string UniqueID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool? IsSystem { get; set; }
        public string Domain { get; set; }

        public string ParentID { get; set; }
        //fxl 用于角色管理 copy 和 site的角色

        public bool IsCopy { get; set; }
        public string CopyRoleName { get; set; }
        public string Site { get; set; }
        //角色配置list
        public List<ProfileDto> RoleProfileList { get; set; }

    }
}
