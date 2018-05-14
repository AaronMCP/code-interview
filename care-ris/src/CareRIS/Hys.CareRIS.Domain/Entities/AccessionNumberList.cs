using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class AccessionNumberList : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }
        public string AccNo { get; set; }
        public string OrderID { get; set; }
        public string PatientID { get; set; }
        public string HisID { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
