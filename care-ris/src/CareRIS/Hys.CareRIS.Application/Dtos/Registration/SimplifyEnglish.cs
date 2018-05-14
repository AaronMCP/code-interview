using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
  public  class SimplifyEnglishDto
    {
      public string LocalName { get;set;}
      public bool UpperFirstLetter { get; set; }
      public int SeparatePolicy { get; set; }
      public string Separator { get; set; }
    }
}
