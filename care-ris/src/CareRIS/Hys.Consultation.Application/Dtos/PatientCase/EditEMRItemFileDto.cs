using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos.PatientCase
{
    public class EditEMRItemFileDto
    {
        public List<RegEMRItemFileDto> NewItems { get; set; }
        public List<RegEMRItemFileDto> EditItems { get; set; }
        public List<string> DeleteItems { get; set; }
    }
}
