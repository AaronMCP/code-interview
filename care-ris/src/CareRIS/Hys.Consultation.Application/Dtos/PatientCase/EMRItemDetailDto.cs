using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos.PatientCase
{
    public class EMRItemDetailDto
    {
        public string UniqueID { get; set; }
        public string EMRItemID { get; set; }
        public string DetailID { get; set; }
        public string DamID { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
