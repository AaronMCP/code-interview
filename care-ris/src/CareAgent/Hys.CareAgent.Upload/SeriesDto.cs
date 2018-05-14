using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hys.CareAgent.Upload
{
    public class SeriesDto
    {
        public string UniqueID { get; set; }
        public string StudyInstanceUID { get; set; }
        public string BodyPart { get; set; }
        public string Modality { get; set; }
    }
}
