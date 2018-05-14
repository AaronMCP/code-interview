namespace Hys.CareAgent.DAP.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Image
    {
        public string SOPInstanceUID { get; set; }
        public string SeriesInstanceUID { get; set; }
        public int ImageNo { get; set; }
        public string FilePath { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
