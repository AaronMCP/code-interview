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
/*   Author : Fred Li                                                       */
/****************************************************************************/

#endregion

using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.Services;
using System.Web.Services.Protocols;

namespace Server.ClientFramework.Common
{


    /// <summary>
    /// This object will be carried in SoapHead to pass between client and server side.
    /// At client side, when call the webservice, some of the client login infomation will be load into this structure
    /// from UserRow object at LoginManager.
    /// At Server side, some infomation of the structure will be changed to pass back to client side, and further more,
    /// storaged in the UserRow object at LoginManager
    /// 
    /// 
    /// </summary>

    public class GcRisUserNameToken : SoapHeader
    {
        /// <summary>
        /// User Local Name
        /// </summary>
        private string szLocalName;

        /// <summary>
        /// User Login Name
        /// </summary>
        private string szLoginName;

        /// <summary>
        /// User Role Name
        /// </summary>
        private string szRoleName;

        /// <summary>
        /// Encrypted password
        /// </summary>
        private string szPassWord;

        /// <summary>
        /// The key to deencrypt the salty passward
        /// </summary>
        private string szClientSessionID;

        /// <summary>
        /// this will be used to store the Valid Functions define in the dog (64bit) 
        /// </summary>
        private string szFunsLicensed;

        /// <summary>
        /// If server side validation OK, stored the UserGuid of the LoginName/
        /// </summary>
        private string szUserGuid;

        /// <summary>
        /// Client machine IP
        /// </summary>
        private string szIP;

        /// <summary>
        /// Different bit stand for different meanings
        /// It's Value is set by WebserviceManager to pass the status got from server side or client side
        /// 0bit: whether is login command to webservice, 1 for login command
        /// 1bit: Logined status at client side, 1 for already logined
        /// 2bit: 1 for no hard Dog pluged at the IIS Server Machine
        /// 3bit: 1 for the server time has exceed the the expire-time recorded at the hard dog, 
        /// 4bit: 1 for user wants to hijack login
        /// </summary>
        private int intParameter;

        /// <summary>
        /// Para: different bit stand for different meanings
        /// It's Value is set by client to storage the status got from client side
        /// 0bit: 1 for web clinic and 0 for smart client
        /// 1bit: no meanings
        /// 2bit: no meanings
        /// 3bit: no meanings
        /// </summary>
        private int intClientParameter;

        public string IP
        {
            get { return this.szIP; }
            set { this.szIP = value; }
        }

        public int Parameter
        {
            get { return this.intParameter; }
            set { this.intParameter = value; }
        }

        public int ClientParameter
        {
            get { return this.intClientParameter; }
            set { this.intClientParameter = value; }
        }

        public string UserGuid
        {
            get { return this.szUserGuid; }
            set { this.szUserGuid = value; }
        }

        public string FunsLicensed
        {
            get { return this.szFunsLicensed; }
            set { this.szFunsLicensed = value; }
        }

        public string ClientSessionID
        {
            get { return this.szClientSessionID; }
            set { this.szClientSessionID = value; }
        }

        public string LoginName
        {
            get { return this.szLoginName; }
            set { this.szLoginName = value; }
        }

        public string LocalName
        {
            get { return this.szLocalName; }
            set { this.szLocalName = value; }
        }

        public string RoleName
        {
            get { return this.szRoleName; }
            set { this.szRoleName = value; }
        }

        public string PassWord
        {
            get { return this.szPassWord; }
            set { this.szPassWord = value; }
        }

        public GcRisUserNameToken()
            : base()
        {
            this.szLoginName = "";
            this.szLocalName = "";
            this.szRoleName = "";
            this.szPassWord = "";
            this.szClientSessionID = "";
            this.UserGuid = "";
            this.FunsLicensed = "";
            this.Parameter = 0;
            this.ClientParameter = 0;
            this.szIP = "";
        }


    }

}
