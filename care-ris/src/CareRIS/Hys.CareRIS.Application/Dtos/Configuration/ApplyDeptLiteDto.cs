using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class ApplyDeptLiteDto
    {
        public string UniqueID { get; set; }
        public string DeptName { get; set; }
        public string ShortcutCode { get; set; }
        public string FirstPingYinName { get; set; }
    }
}
