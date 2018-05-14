using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
   public partial  class RequestCharge : Entity
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

       public string RequestItemID { get; set; }

       public string ItemCode { get; set; }

       public string ItemName { get; set; }

       public double? Price { get; set; }

       public int? Amount { get; set; }

       public int? IsItemCharged { get; set; }

    }
}
