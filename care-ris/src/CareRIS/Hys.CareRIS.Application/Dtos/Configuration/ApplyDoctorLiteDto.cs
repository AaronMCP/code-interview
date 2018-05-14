using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class ApplyDoctorLiteDto
    {
        public string UniqueID { get; set; }
        public string DoctorName { get; set; }
        public string ShortcutCode { get; set; }
        public string FirstPingYinName { get; set; }
    }
}
