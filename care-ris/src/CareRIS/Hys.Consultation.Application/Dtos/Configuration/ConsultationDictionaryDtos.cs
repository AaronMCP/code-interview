using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos
{
    public class ConsultationDictionaryDtos
    {
        public int Type { get; set; }
        public IEnumerable<ConsultationDictionaryDto> Dictionaries { get; set; }
    }
}
