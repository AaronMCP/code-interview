using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos
{
    public class ExamModuleDto
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public bool Visible { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
