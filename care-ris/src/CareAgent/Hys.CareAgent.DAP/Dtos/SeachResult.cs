using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hys.CareAgent.DAP.Entity;

namespace Hys.CareAgent.DAP
{
   public class SearchResult
   {
       public List<Study> Result { get; set; }
       public int Count { get; set; }
       public string DICOMPath { get; set; }
       public string LastTime { get; set; }
   }
}
