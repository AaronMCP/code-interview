using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos.PatientCase
{
    public class PatientCaseInfoDto
    {
        public string UniqueID { get; set; }

        public string PatientNo { get; set; }

        public string HospitalId { get; set; }

        public string PatientName { get; set; }

        public string PatientNamePy { get; set; }

        public string IdentityCard { get; set; }

        public string InsuranceNumber { get; set; }

        public string Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public string Age { get; set; }

        public string ClinicalDiagnosis { get; set; }

        public string History { get; set; }

        public int? Progress { get; set; }

        public string LastEditUser { get; set; }

        public DateTime LastEditTime { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public List<NewEMRItemDto> newEMRItems { get; set; }

        public List<ExamModuleDto> Modules { get; set; }
        public bool ModuleIsNew { get; set; }

        public int Status { get; set; }

        /// <summary>
        /// Delete info
        /// </summary>
        public int IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string DeleteReason { get; set; }
        public string DeleteUser { get; set; }
        public string DeleteUserName { get; set; }

        public bool IsMobile { get; set; }
        public string OrderID { get; set; }
    }
}
