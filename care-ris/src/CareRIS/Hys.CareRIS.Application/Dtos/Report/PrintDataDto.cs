using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class PrintDataDto
    {
        public string Template { get; set; }
        public  DataTable data { get; set; }
    }
}
