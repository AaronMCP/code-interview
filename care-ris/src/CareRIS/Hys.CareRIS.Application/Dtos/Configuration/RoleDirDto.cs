using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class RoleDirDto 
    {

        public string UniqueID { get; set; }
        public string Name { get; set; }
        public string ParentID { get; set; }
        public string RoleID { get; set; }
        public int? Leaf { get; set; }
        public int? OrderID { get; set; }
        public string Domain { get; set; }

        //role
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool? IsSystem { get; set; }
    }
}
