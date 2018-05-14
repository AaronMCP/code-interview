using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class BodySystemMapDto
    {
        public string UniqueID { get; set; }
        public string ModalityType { get; set; }
        public string BodyPart { get; set; }
        public string ExamSystem { get; set; }
        public string Site { get; set; }
        public string Domain { get; set; }
    }
}
