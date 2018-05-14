using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CommonGlobalSettings;

namespace Common.ActionResult.Framework
{
    public class LoginSettingsActionResult : BaseActionResult
    {
       public string useLoginSettings;
       private string[] arr = null;
       private LoginSettingsModel model = null;

       public LoginSettingsActionResult()
       {

       }

       public LoginSettingsModel Model
       {
           get
           {
               return model;
           }
           set
           {
               model = value;
           }
       }

       public string[] ArrData
       {
           get
           { return arr; }
           set
           { arr = value; }
       }
    }
}
