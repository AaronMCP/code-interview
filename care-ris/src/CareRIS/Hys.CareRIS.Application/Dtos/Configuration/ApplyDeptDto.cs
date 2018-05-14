using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class ApplyDeptDto : ApplyDeptLiteDto
    {
        public string Telephone { get; set; }
        public string Site { get; set; }
        public string Domain { get; set; }
    }
}
