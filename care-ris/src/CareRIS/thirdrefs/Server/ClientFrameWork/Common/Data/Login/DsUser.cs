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
using System.Net;
using System.Web.Security;
using System.Management;

namespace Server.ClientFramework.Common.Data.Login
{


    partial class DsUser
    {
        partial class UserDataTable
        {
        }
        /// <summary>
        /// Init The buffer to store the login user infomation at the client side.
        /// 
        /// UserGuid : After successfully server-side logined, get this value.
        /// 
        /// RoleName, LoginName: Got from user input at the login form or dcom login call
        /// 
        /// The user inputed plain password is encrypted and stored here, encrypted is for Security net-work transfer.
        /// 
        /// ClientSessionID is a random string to encrypt and decrypt the password transfer between server and client Side
        /// 
        ///Para: different bit stand for different meanings
        ///It's Value is set by WebserviceManager to storage the status got from server side
        ///0bit: no meanings
        ///1bit: no meanings
        ///2bit: 1 for no hard Dog pluged at the IIS Server Machine
        ///3bit: 1 fot the server time has exceed the the expire-time recorded at the hard dog, 
        /// 
        /// FunsLicensedStr: stored the License function string recorded in the hard dog,got after successfully logined
        /// 
        /// IP: Client side IP Address, is for judging whether the calling is from the same client machine.
        /// 
        /// </summary>
        /// <returns></returns>
        public UserRow NewNullRow()
        {
            UserRow newRow = this.User.NewUserRow();

            //UserGuid : After successfully server-side logined, get this value.
            newRow.UserGuid = "";

            //RoleName, LoginName: Got from user input at the login form or dcom login call
            newRow.RoleName = "";
            newRow.LoginName = "";
            newRow.LocalName = "";

            //The user inputed plain password is encrypted and stored here, encrypted is for Security net-work transfer.
            newRow.PasswordEncrypted = "";

            //ClientSessionID is a random string to encrypt and decrypt the password transfer between server and client Side
            MyCryptography c = new MyCryptography("GCRIS2-20061025");
            newRow.ClientSessionID = c.Encrypt(DateTime.Now.ToLongTimeString());// FormsAuthentication.HashPasswordForStoringInConfigFile(DateTime.Now.ToLongTimeString(), "SHA1");

            //Para: different bit stand for different meanings
            //It's Value is set by WebserviceManager to storage the status got from server side
            //0bit: no meanings
            //1bit: no meanings
            //2bit: 1 for no hard Dog plugs at the IIS Server Machine
            //3bit: 1 for the server time has exceed the the expire-time recorded at the hard dog 
            newRow.Para = 0;

            //Para: different bit stand for different meanings
            //It's Value is set by client to storage the status got from client side
            //0bit: 1 for web clinic and 0 for smart client
            //1bit: no meanings
            //2bit: no meanings
            //3bit: no meanings
            newRow.ClientPara = 0;

            //FunsLicensedStr: stored the License function string recorded in the hard dog, got after successfully logined
            newRow.FunsLicensedStr = "";

            //ServerTime: IIS Server side time got after successfully logined
            newRow.ServerTime = System.DateTime.MinValue;

            string strHostName = Dns.GetHostName();
            IPHostEntry IPList = Dns.GetHostEntry(strHostName);
            string strIPAddress = "";
            foreach (IPAddress ip in IPList.AddressList)
            {
                strIPAddress = ip.ToString();
            }
            //IP: Client side IP Address, is for judging whether the calling is from the same client machine.
            newRow.IP = strIPAddress;

            #region Added by Blue for RC507 - US16220, 07/14/2014
            //get client side MAC address
            string mac = string.Empty;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                {
                    mac = mo["MacAddress"].ToString();
                }
            }
            newRow.IP = string.Format("{0}&{1}&{2}", newRow.IP, mac, strHostName);
            #endregion

            this.User.AddUserRow(newRow);
            return newRow;
        }
    }
}
