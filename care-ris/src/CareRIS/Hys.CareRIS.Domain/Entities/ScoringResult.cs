using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class ScoringResult : Entity
    {

        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }
        public string ObjectGuid { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Type { get; set; }
        public string Result { get; set; }
        public string AccordRate { get; set; }
        public string Appraiser { get; set; }
        public string Comment { get; set; }
        public string Domain { get; set; }
        public string Result2 { get; set; }
        public int IsFinalVersion { get; set; }


    }
}
