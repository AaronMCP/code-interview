using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos.PatientCase
{
    public class PatientCaseEditInfoDto : PatientCaseInfoDto
    {
        public List<string> DeletedFileData { get; set; }
        public List<string> DeletedItemData { get; set; }
    }
}
