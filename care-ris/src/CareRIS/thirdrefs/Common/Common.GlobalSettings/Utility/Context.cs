#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/*     Author : Caron Zhao                                                  */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;


namespace CommonGlobalSettings
{
    [Serializable()]
    public class Context
    {
        private string      messageName = null;
        private string      parameters = null;//e.g. action=delete&id=1&cascade=true
        private BaseModel   model = null;
        private string      userID = null;
        private string      roleName = null;
        private string      clientType = "0";// 1 means web user, 0 means smart client user. default is 0

        public string MessageName
        {
            get
            {
                return messageName;
            }
            set
            {
                messageName = value;
            }
        }

        public string ClientType
        {
            get
            {
                 return clientType;
            }
            set
            {
                clientType = value;
            }
        }

        public string Parameters 
        {
            get
            {
                return parameters;
            }
            set
            {
                parameters = value;
            }
        }

        public BaseModel Model
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

        public string UserID 
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }

        public string RoleName 
        {
            get
            {
                return roleName;
            }
            set
            {
                roleName = value;
            }
        }
    }
}
