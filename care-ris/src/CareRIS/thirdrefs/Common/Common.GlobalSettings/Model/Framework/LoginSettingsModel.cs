using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO ;

namespace CommonGlobalSettings
{
    public class LoginSettingsModel : BaseModel
    {
        public string UpdateDateTime;
        public string title;
        public string font;
        public int fontStyle;
        public string color;
        public string logo;
        public string isRegular;
        public string regularNum;
        public string regularUnit;
        public string random;
        public string defaultPicture;
        public string[] pictures = null;

        public LoginSettingsModel()
        {
            
        }



    }
}
