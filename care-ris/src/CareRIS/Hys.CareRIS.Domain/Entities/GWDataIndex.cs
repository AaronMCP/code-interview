namespace Hys.CareRIS.Domain.Entities
{
    using Hys.Platform.Domain;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class GWDataIndex : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

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
