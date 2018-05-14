using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class GWDataIndexDto
    {
        public string UniqueID { get; set; }
        public DateTime? DataTime { get; set; }
        public string EventType { get; set; }
        public string RecordIndex1 { get; set; }
        public string RecordIndex2 { get; set; }
        public string RecordIndex3 { get; set; }
        public string RecordIndex4 { get; set; }
        public string DataSource { get; set; }
        //public string ProcessFlag { get; set; }
    }
}
