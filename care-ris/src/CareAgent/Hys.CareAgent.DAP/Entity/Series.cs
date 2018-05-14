namespace Hys.CareAgent.DAP.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class Series
    {
        public string SeriesInstanceUID { get; set; }
        public string StudyInstanceUID { get; set; }
        public string BodyPart { get; set; }
        public int SeriesNo { get; set; }
        public string Modality { get; set; }
        
    }
}
