using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class SystemProfileDto : ProfileLiteDto
    {
        public string ModuleID { get; set; }
        public string PropertyDesc { get; set; }
        public string PropertyOptions { get; set; }
        public bool IsExportable { get; set; }
        public bool CanBeInherited { get; set; }
        public bool PropertyType { get; set; }
        public bool IsHidden { get; set; }
        public string OrderingPos { get; set; }
        public string Domain { get; set; }

        public List<ProfileDto> SystemProfileList { get; set; }
    }
}
