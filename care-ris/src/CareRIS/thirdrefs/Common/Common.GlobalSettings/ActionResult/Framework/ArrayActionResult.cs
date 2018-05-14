using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ActionResult.Framework
{
    [Serializable()]
   public class ArrayActionResult:BaseActionResult
    {
        private string[] arr = null;
               

        public ArrayActionResult()
        {

        }

        public string[] ArrData
        {
            get
            { return arr;}
            set
            { arr = value;}
        }

    }
}
