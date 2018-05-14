using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class ApplyDoctorDto : ApplyDoctorLiteDto
    {
        public string ApplyDeptID { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string StaffNo { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Domain { get; set; }
        public string DeptName { get; set; }
    }
}
