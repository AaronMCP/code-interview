using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Action;
namespace CommonGlobalSettings
{
     [Serializable()]
    public class PathologyTrackModel : OamBaseModel
    {
         public string PatientID { get; set; }
         public string Value { get; set; }
         public string GenerateTime { get; set; }
    }
}
