using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ActionResult
{
    public class BackupActionResult : CommonBaseActionResult
    {
        private string[] fileList = null;

        public string[] FileList 
        {
            get
            {
                return fileList;
            }
            set
            {
                fileList = value;
            }
        }
    }
}
