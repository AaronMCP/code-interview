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
    public partial class QualityScore: Entity
    {

        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }
        public string AppraiseObject { get; set; }
        public string OrderID { get; set; }
        public DateTime? ExaminateTime { get; set; }
        public int? ScoreType { get; set; }
        public string Result { get; set; }
        public string Appraisee { get; set; }
        public string Appraiser { get; set; }
        public DateTime? AppraiseDate { get; set; }
        public string Comment { get; set; }
        public string Domain { get; set; }
        public string Result2 { get; set; }
        public string Result3 { get; set; }     

    }
}
