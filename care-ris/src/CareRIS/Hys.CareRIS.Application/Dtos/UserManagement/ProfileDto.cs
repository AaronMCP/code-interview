using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class ProfileDto 
    {
        public string ModuleID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string RoleName { get; set; }
        public string ModuleName { get; set; }
        public int PropertyType { get; set; }
        
    }
}
