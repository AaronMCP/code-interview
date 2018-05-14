using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
   public partial  class RequestItem : Entity
    {
       public override object UniqueId
       {
           get
           {
               return UniqueID;
           }
       }
       public string UniqueID { get; set; }

       public string RequestID { get; set; }

       public string RequestItemUID { get; set; }

       public string ModalityType { get; set; }

       public string Modality { get; set; }

       public string ProcedureCode { get; set; }

       public string RPDesc { get; set; }

       public string ExamSystem { get; set; }

       public string ScheduleTime { get; set; }

       public string Comment { get; set; }

       public string TeethName { get; set; }

       public string TeethCode { get; set; }

       public int TeethCount { get; set; }

       public string AccNo { get; set; }

       public string Status { get; set; }
    }
}
