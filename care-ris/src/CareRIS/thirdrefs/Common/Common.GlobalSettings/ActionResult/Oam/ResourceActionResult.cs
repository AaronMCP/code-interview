using System;
using System.Collections.Generic;
using System.Text;
using CommonGlobalSettings;

namespace Common.ActionResult.Oam
{
    public class ResourceActionResult : OamBaseActionResult
    {
        private ResourceModel model = null;

        public ResourceModel Model 
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
    }
}
