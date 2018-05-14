using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class SystemModel : OamBaseModel
    {
        private DataSet m_dsSaveSystemProfile = null;

        public DataSet SaveSystemProfile
        {
            get
            {
                return m_dsSaveSystemProfile;
            }
            set
            {
                m_dsSaveSystemProfile = value;
            }
        }
    }
}
