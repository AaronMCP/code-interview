using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class TimesliceSharerRequest
    {
        public string[] SliceIds { get; set; }
        public TimesliceSharer[] Sharers { get; set; }
    }
}
