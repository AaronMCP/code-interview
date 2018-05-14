using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    public class DictionaryModel : OamBaseModel
    {
        private string[] tag = null;
        private string[] defaultValue = null;
        private string[] site = null;

        public string[] Tag 
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
            }
        }

        public string[] DefaultValue 
        {
            get
            {
                return defaultValue;
            }
            set
            {
                defaultValue = value;
            }
        }

        public string[] Site
        {
            get
            {
                return site;
            }
            set
            {
                site = value;
            }
        }
    }
}
