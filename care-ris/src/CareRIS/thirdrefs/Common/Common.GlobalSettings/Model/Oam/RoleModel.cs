using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.Action;
using System.Text.RegularExpressions;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class RoleModel : OamBaseModel
    {
        private DataSet m_dsSaveRoleProfile = null;

        public DataSet SaveRoleProfile
        {
            get
            {
                return m_dsSaveRoleProfile;
            }
            set
            {
                m_dsSaveRoleProfile = value;
            }
        }
    }
}
