using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class ProfilesDto
    {
        public IEnumerable<SystemProfileDto> SystemProiles { get; set; }
        public IEnumerable<SiteProfileDto> SiteProiles { get; set; }
        public IEnumerable<RoleProfileDto> RoleProiles { get; set; }
        public IEnumerable<UserProfileDto> UserProiles { get; set; }
    }
}
