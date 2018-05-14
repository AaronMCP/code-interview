using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class RequisitionDto
    {
      public string UniqueID { get; set; }
      public string AccNo { get; set; }
      public string RelativePath { get; set; }
      public string FileName { get; set; }
      public DateTime? ScanTime { get; set; }
      public string Domain { get; set; }
      public DateTime? UpdateTime { get; set; }
      public int? Uploaded { get; set; }
      public DateTime? CreateTime { get; set; }
      public string ErNo { get; set; }
      public string Base64Str { get; set; }
    }

    public class RequisitionLiteDto
    {
      public string AccNo { get; set; }
      public string ErNo { get; set; }
      public string RelativePath { get; set; }
      public string ImageQualityLevel { get; set; }
    }
}
