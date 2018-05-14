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
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Panels;
using Server.ClientFramework.Common.Data.Login;
using Server.ClientFramework.Common.Data.Profile;
using DataAccessLayer;
using LogServer;
using CommonGlobalSettings;
using CommonGlobalSettings.Utility;
using ServerCommon = Server.DAO.Common;
using Server.ClientFramework.Common;
using System.Web;
using System.Threading;
//using Sybase.Data.AseClient;
using System.Data;
using System.Web.Security;
namespace Server.DAO.ClientFramework
{
    /// <summary>
    /// The Data Access Layer of Framework
    /// </summary>
    public abstract class AbstractImplement : IFrameWorkDAO
    {
        ILogManager logger = new LogManager();

        public virtual string GetDbServerTime()
        {
            //defect EK_HI00070834 Foman Liang 2008-5-19
            string Cmd = @"Select convert(varchar,GetDate(),120)";

            string rt = "";
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                rt = oKodakDAL.ExecuteScalar(Cmd).ToString();
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                 string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return rt;
        }

        /// <summary>
        /// Get the Online User Numbers, called by DogCheck.Check
        /// </summary>
        /// <returns></returns>
        public virtual int GetOnlineUserNo(bool bWebUser, bool bSelfUser, string ipaddress)
        {
            string Cmd = "";
            if (!bWebUser)
            {
                Cmd = @"Select count(*) from [dbo].[tOnlineClient] where IsOnline = 1 and (comments is null or (comments != 'web login user' and comments != 'selfservice login user')) AND Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "'";
            }
            else
            {
                if (bSelfUser)
                {
                    Cmd = @"Select count(*) from [dbo].[tOnlineClient] where IsOnline = 1 and (comments = 'web login user' or comments = 'selfservice login user') AND Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "' AND MachineIP <> '" + ipaddress + "'";
                }
                else
                {
                    Cmd = @"Select count(*) from [dbo].[tOnlineClient] where IsOnline = 1 and (comments = 'web login user' or comments = 'selfservice login user') AND Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "' AND MachineIP <> '" + ipaddress + "'";

                }
            }

            int rt = 0;
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                rt = Convert.ToInt32(oKodakDAL.ExecuteScalar(Cmd));
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                 string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return rt;
        }
        /// <summary>
        /// Set all online status to 0. Called by Global.asas: Application_Start
        /// </summary>
        public virtual void OnlineStatusInit()
        {
            StringBuilder strBuilder = new StringBuilder();
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string iisUrl = HttpContext.Current.Request.Url.OriginalString;
                strBuilder.Append(string.Format("Update dbo.tOnlineClient Set IsOnline = 0 where IISUrl = '{0}'", iisUrl));
                oKodakDAL.ExecuteNonQuery(strBuilder.ToString());
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="UserGuid"></param>
        //public void UpdateLatestAccessTime(string UserGuid)
        //{
        //    StringBuilder strBuilder = new StringBuilder();
        //    strBuilder.AppendFormat("Update dbo.tOnlineClient Set loginTime = GetDate() where UserGuid = '{0}'", UserGuid);
        //    KodakDAL oKodakDAL = new KodakDAL();
        //    try
        //    {
        //        oKodakDAL.ExecuteNonQuery(strBuilder.ToString());
        //    }
        //    catch (Exception Ex)
        //    {
        //    }
        //    finally
        //    {
        //        if (oKodakDAL != null)
        //        {
        //            oKodakDAL.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Write ConfigDic Row. Called by Server.Utilities(When Ket Data in Dog changed, re-write to ConfigDic.)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public virtual int WriteConfigDicRow(Server.ClientFramework.Common.Data.Profile.DsConfigDic.ConfigDicRow row, int iType)
        {
            int rt = 0;
            string Cmd = @" If not exists (Select 1 from [dbo].[tConfigDic] 
                                where [ConfigName] = '" + row.ConfigName + @"' and [ModuleID] = '" + row.ModuleID.ToString() + @"')
                            Begin 
                            INSERT INTO [dbo].[tConfigDic]
                           ([ConfigName] ,[ModuleID] ,[Value] ,[Exportable] ,[PropertyDesc] ,[PropertyOptions]
                           ,[Inheritance] ,[PropertyType], [IsHidden], [OrderingPos], [Domain], [Type]) Values 
                            ( '" +
                                row.ConfigName + "', '" +
                                row.ModuleID.ToString() + "', '" +
                                row.Value + "', " +
                                row.Exportable.ToString() + ", '" +
                                row.PropertyDesc + "', '" +
                                row.PropertyOptions + "', " +
                                row.Inheritance.ToString() + ", " +
                                row.PropertyType.ToString() + ", " +
                                row.IsHidden.ToString() + ", " +
                                row.OrderingPos.ToString() + ", '" +
                                row.Domain.ToString() + "', " +
                                iType.ToString() +
                            @")
                            End
                            Else
                            Begin
                            Update [dbo].[tConfigDic] Set [Value] = '" + row.Value + "'" +
                              "Where  ConfigName = '" + row.ConfigName + "'and " +
                                    "ModuleID = '" + row.ModuleID.ToString() + "' and " +
                                    "Type = " + iType.ToString() +
                           " End";
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                oKodakDAL.ExecuteNonQuery(Cmd);
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                rt = -1;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return rt;
        }

        /// <summary>
        /// Read ConfigDic to buffer
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public virtual Server.ClientFramework.Common.Data.Profile.DsConfigDic LoadConfigDic(int Type)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(
                @"SELECT [ConfigName],[Value],tConfigDic.[ModuleID],
                    tModule.Title as ModuleName,[Exportable],
                    isnull([PropertyDesc], '') as [PropertyDesc],
                    isnull([PropertyOptions], '') as [PropertyOptions],
                    [Inheritance],[PropertyType],[IsHidden],[OrderingPos],
                    isnull(tConfigDic.[Domain], '') as Domain 
                FROM [dbo].[tConfigDic], [dbo].[tModule] 
                Where [Type]= " + Type.ToString() + @" And tModule.ModuleID = tConfigDic.ModuleID");
            RisDAL oKodakDAL = new RisDAL();
            Server.ClientFramework.Common.Data.Profile.DsConfigDic rt =
                new Server.ClientFramework.Common.Data.Profile.DsConfigDic();
            try
            {
                oKodakDAL.ExecuteQuery(strBuilder.ToString(), rt.ConfigDic);
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return rt;
        }

        /// <summary>
        /// Load all roles to store to Client side for displayment at login page
        /// </summary>
        /// <returns></returns>
        public virtual DsRole LoadAllRole()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("Select RoleName,Description from  tRole where Domain='{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
            RisDAL oKodakDAL = new RisDAL();
            DsRole rt = new DsRole();
            try
            {
                oKodakDAL.ExecuteQuery(strBuilder.ToString(), rt.Role);
                if (rt.Role == null || rt.Role.Count == 0)
                    logger.Debug((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, "Role table is empty!",
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return rt;
        }

        #region Added by Blue for US19332, 09/17/2014
        public virtual DsRole LoadAllRole4Login()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("select tRoleDir.Name as RoleName, tRoleDir.ParentID, tRole.Description from tRoleDir inner join tRole on tRoleDir.Name = tRole.RoleName where tRoleDir.Leaf = 1 and tRoleDir.Domain = '{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
            strBuilder.Append("select Name, UniqueID from tRoleDir where Leaf = 0 and RoleID = ''");
            strBuilder.AppendFormat("Select RoleName,Description from  tRole where Domain='{0}' and RoleName = 'GlobalAdmin'", CommonGlobalSettings.Utilities.GetCurDomain());
            RisDAL oKodakDAL = new RisDAL();
            DsRole rt = new DsRole();
            try
            {
                DataSet ds = new DataSet();
                oKodakDAL.ExecuteQuery(strBuilder.ToString(), ds, "role");
                if (ds.Tables != null && ds.Tables.Count == 3)
                {
                    DataRow[] drs = ds.Tables[1].Select(string.Format("Name='{0}' or Name='GlobalRole'", CommonGlobalSettings.Utilities.GetCurSite()));
                    if (drs != null && drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            DataRow[] roles = ds.Tables[0].Select(string.Format("ParentID='{0}'", dr["UniqueID"].ToString()));
                            if (roles != null && roles.Length > 0)
                            {
                                foreach (DataRow role in roles)
                                {
                                    DataRow newDr = rt.Role.NewRoleRow();
                                    newDr["RoleName"] = role["RoleName"].ToString();
                                    newDr["Description"] = role["Description"].ToString();
                                    rt.Role.Rows.Add(newDr);
                                }
                            }
                        }
                    }
                }

                //Add global admin
                if (ds.Tables[2].Rows.Count > 0)
                {
                    DataRow newDr = rt.Role.NewRoleRow();
                    newDr["RoleName"] = ds.Tables[2].Rows[0]["RoleName"].ToString();
                    newDr["Description"] = ds.Tables[2].Rows[0]["Description"].ToString();
                    rt.Role.Rows.Add(newDr);
                }
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return rt;
        }
        #endregion

        ///// <summary>
        ///// Can be called by User shut down the client side, 
        ///// </summary>
        ///// <param name="objSpan"></param>
        //public virtual void LogOut(TimeSpan objSpan)
        //{
        //    StringBuilder strBuilder = new StringBuilder();
        //    strBuilder.AppendFormat("Update dbo.tOnlineClient Set IsOnline = 0 where DATEADD (minute, {0}, LoginTime) <= Getdate();", objSpan.Minutes);
        //    KodakDAL oKodakDAL = new KodakDAL();
        //    try
        //    {
        //        oKodakDAL.ExecuteNonQuery(strBuilder.ToString());
        //    }
        //    catch (Exception Ex)
        //    {
        //    }
        //    finally
        //    {
        //        if (oKodakDAL != null)
        //        {
        //            oKodakDAL.Dispose();
        //        }
        //    }
        //}

        /// <summary>
        /// Be call by session at server side time out
        /// </summary>
        /// <param name="SessionID"></param>
        public virtual void LogOutBySessionID(string SessionID)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("Update dbo.tOnlineClient Set IsOnline = 0 where SessionID = '{0}'", SessionID);
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                oKodakDAL.ExecuteNonQuery(strBuilder.ToString());
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }

        /// <summary>
        /// Can be called by User shut down the client side
        /// </summary>
        /// <param name="UserGuid"></param>
        public virtual void LogOut(string UserGuid, bool bWebUser)
        {
            StringBuilder strBuilder = new StringBuilder();
            string strUserGuid = CommonGlobalSettings.Utilities.GetParameter("UserGuid", UserGuid);
            
            

            if (string.IsNullOrEmpty(strUserGuid))
            {
                strUserGuid = Convert.ToString(HttpContext.Current.Session["UserGuid"]);
            }
            string strIP = CommonGlobalSettings.Utilities.GetParameter("IP", UserGuid);
            if (bWebUser)
            {
                strBuilder.AppendFormat("Update dbo.tOnlineClient Set IsOnline = 0 where UserGuid = '{0}' and (comments = 'web login user')", strUserGuid);
            }
            else
            {
                //strBuilder.AppendFormat("Update dbo.tOnlineClient Set IsOnline = 0 where UserGuid = '{0}' and (comments is null or comments != 'web login user')", strUserGuid);

                // EK_HI00126809
                // check IP on logout
                strBuilder.AppendFormat("Update dbo.tOnlineClient Set IsOnline = 0 where UserGuid = '{0}' and MachineIP='{1}' and (comments is null or comments != 'web login user')", strUserGuid, strIP);
            }
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                oKodakDAL.ExecuteNonQuery(strBuilder.ToString());
                if (!bWebUser)
                {
                    string sql = string.Format("delete from  tSync where owner='{0}' and ownerIP='{1}'", strUserGuid, strIP);
                    oKodakDAL.ExecuteNonQuery(sql);
                    AuditUserAuthMsg("Logout", false, true);
                }
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                AuditUserAuthMsg("Logout", false, false);
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }

        /// <summary>
        /// Do online check
        /// </summary>
        /// <param name="szUserGuid"></param>
        /// <param name="szRoleName"></param>
        /// <param name="szIpAddress"></param>
        /// <param name="szUrl"></param>
        /// <param name="szSessionID"></param>
        /// <param name="IsLogined"></param>
        /// <History>
        /// 2007-01-10 Add new license for web clinic login
        /// on line check is separated to two types
        /// column comment in table tOnlineClient is used for identificaition of the login type 
        /// If it is web clinic input, the value should be web login user
        /// </History>
        /// <returns></returns>
        public virtual int IsOnLine(string szUserGuid, string szRoleName, string szIpAddress, string szUrl, string szSessionID, bool IsLogined, bool IsWebAccess, bool IsHijackLogin, bool IsSelfService)
        {
            RisDAL oKodakDAL = new RisDAL();
            //Check the domain and site is in domainlist/sitelist or not
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            string strSite = CommonGlobalSettings.Utilities.GetCurSite();
            string strMac = string.Empty;
            string strHostName = string.Empty;
            string strLocation = string.Empty;
            #region Added by Blue for RC507 - US16220, 07/14/2014
            if (szIpAddress.Contains("&"))
            {
                string[] ipMacHost = szIpAddress.Split('&');
                if (ipMacHost != null && ipMacHost.Length == 4)
                {
                    szIpAddress = ipMacHost[0];
                    strMac = ipMacHost[1];
                    strHostName = ipMacHost[2];
                    strLocation = ipMacHost[3];
                }
            }
            #endregion
            if (string.IsNullOrWhiteSpace(strDomain) || string.IsNullOrWhiteSpace(strSite))
            {
                return 5;
            }

            string strSQL = string.Format("select count(*) from tSiteList where Domain='{0}' and Site='{1}'", strDomain, strSite);
            Object obj = oKodakDAL.ExecuteScalar(strSQL);
            if (obj == null || Convert.ToInt32(obj) == 0)
            {
                return 5;
            }


            string Cmd = "";
            if (IsSelfService == false)
            {
                string comment = string.Empty;
                comment = IsWebAccess == true ? "web login user" : null;
                Cmd =
                    //this user is not found in database, insert new record
                        @"If not exists (select 1 from dbo.tOnlineClient where UserGuid = '" + szUserGuid + @"')
                    Begin --Has no login record
	                    Insert into dbo.tOnlineClient (UserGuid, RoleName, IISUrl, MachineIP, Comments, SessionID, IsOnline, LoginTime, Domain, Site, MachineName, MACAddress, Location)
		                    Values ('" + szUserGuid + @"', '" + szRoleName + @"', '" + szUrl + @"', '" + szIpAddress + @"', '" + comment + @"','" + szSessionID + @"', 1, GETDATE(), '" + CommonGlobalSettings.Utilities.GetCurDomain() + @"', '" + CommonGlobalSettings.Utilities.GetCurSite() + "', '" + strHostName + "', '" + strMac + "', '" + strLocation + @"') 
	                    Select 0 
                    End 
                    Else 
                    Begin --Has login record
	                    If exists (Select 1 from dbo.tOnlineClient where UserGuid = '" + szUserGuid
                    //user is found but already off-line
                    + @"' and IsOnline = 0 ) 
                Begin --(UserGuid's Online status is Off)
		            If '" + IsLogined
                    //is already login before,only check the machine name and user id to confirm and recall the online record
                            + @"' = 'True'
		            Begin -- This is a session has ever logined but time out now
                        if '" + IsHijackLogin + @"'<>'True' 
                            Begin
			                    IF exists (Select 1 from dbo.tOnlineClient where UserGuid = '" + szUserGuid + @"' and  '" +
                    //same user and different machine already login before
                                                                                               szIpAddress + @"' <> MachineIP )
			                    Begin -- ClientSeesion ID has been hijackered by other session
				                    --Return Value = 2 implicate Some one else hase ever been logined and logouted since this session time out
				                    Select 2
                                    Return
			                    End
                             End "
                    //same user, same machine, re login
                            + @"
			            Begin -- The ClientSessionID is of himself
				            Update dbo.tOnlineClient 
					            set LoginTime = getdate(), IsOnline = 1, MachineIP = '" + szIpAddress + @"',Comments = '" + comment + @"', MachineName = '" + strHostName + @"', MACAddress = '" + strMac + @"', Location = '" + strLocation + @"', SessionID = '" + szSessionID + @"', IISUrl = '" + szUrl + @"', RoleName='" + szRoleName + @"', Domain='" + CommonGlobalSettings.Utilities.GetCurDomain() + @"', Site='" + CommonGlobalSettings.Utilities.GetCurSite() + @"'
					            where UserGuid = '" + szUserGuid + @"'
				            Select 0 --Can Login
			            End
		            End
		            else"
                    //The first time for login, login and update database information
                + @"
		        Begin --Login try for the first time
			        Update dbo.tOnlineClient 
				        set LoginTime = getdate(), IsOnline = 1, MachineIP = '" + szIpAddress + @"',Comments = '" + comment + @"', MachineName = '" + strHostName + @"', MACAddress = '" + strMac + @"', Location = '" + strLocation + @"', SessionID = '" + szSessionID + @"', IISUrl = '" + szUrl + @"', RoleName='" + szRoleName + @"', Domain='" + CommonGlobalSettings.Utilities.GetCurDomain() + @"', Site='" + CommonGlobalSettings.Utilities.GetCurSite() + @"'
				        where UserGuid = '" + szUserGuid + @"'
			        Select 0 --Can Login
		        End
	        End 
	        Else 
	        Begin --(UserGuid's Online status is On)"
                    //If hijack login is true or  same user, same machine  and status is on-line could be allowed to login
                            + @"
		            if  '" + IsHijackLogin + @"'='True'  or 
                        exists (Select 1 from dbo.tOnlineClient where UserGuid = '" + szUserGuid + @"' and MachineIP = '" + szIpAddress + @"' and Comments = '" + comment + @"')
		            Begin --At Same Machine, imlicated that is the Same User(Maybe exit the client side abnormally or relogin when fisnishing lockscreen status)
			            Update dbo.tOnlineClient 
				            set LoginTime = getdate(), IsOnline = 1, MachineIP = '" + szIpAddress + @"',Comments = '" + comment + @"', MachineName = '" + strHostName + @"', MACAddress = '" + strMac + @"', Location = '" + strLocation + @"', SessionID = '" + szSessionID + @"', IISUrl = '" + szUrl + @"', RoleName='" + szRoleName + @"', Domain='" + CommonGlobalSettings.Utilities.GetCurDomain() + @"', Site='" + CommonGlobalSettings.Utilities.GetCurSite() + @"'
				            where UserGuid = '" + szUserGuid + @"'
			            Select 0 --Can Login
		            End
		            Else --At different Machine
		            Select 1  -- Some one Else with the Same UserID Is Online
	            End
            End";
            }
            else
            {
                Cmd =
                    //this IP is not found in database, insert new record
           @"If not exists (select 1 from dbo.tOnlineClient where MachineIP = '" + szIpAddress + @"' and ( comments = 'selfservice login user'))
            Begin --Has no login record
	            Insert into dbo.tOnlineClient (UserGuid, RoleName, IISUrl, MachineIP, SessionID, IsOnline, LoginTime,comments, Domain, Site, MachineName, MACAddress, Location)
		            Values ('" + szUserGuid + @"', '" + szRoleName + @"', '" + szUrl + @"', '" + szIpAddress + @"', '" + szSessionID + @"', 1, GETDATE(), 'selfservice login user', '" + CommonGlobalSettings.Utilities.GetCurDomain() + @"', '" + CommonGlobalSettings.Utilities.GetCurSite() + "', '" + strHostName + "', '" + strMac + "', '" + strLocation + @"') 
	            Select 0 
            End 
            Else 
            Begin --Has login record "
                    //this IP is found in database, only update with current information
                       + @"
	            Update dbo.tOnlineClient 
		            set LoginTime = getdate(), IsOnline = 1,  SessionID = '" + szSessionID + @"',UserGuid = '" + szUserGuid + @"' , IISUrl = '" + szUrl + @"', RoleName='" + szRoleName + @"', Domain='" + CommonGlobalSettings.Utilities.GetCurDomain() + @"', Site='" + CommonGlobalSettings.Utilities.GetCurSite() + @"'
		            where MachineIP = '" + szIpAddress + @"'and  comments = 'selfservice login user'
	            Select 0 --Can Login
            End";
            }
            int rt = 0;
            string loginName = oKodakDAL.ExecuteScalar(string.Format("select LoginName from tUser where UserGuid = '{0}'", szUserGuid)).ToString();
            try
            {
                rt = Convert.ToInt32(oKodakDAL.ExecuteScalar(Cmd));
                if (rt == 0)
                {
                    LoginRecordToHippa(loginName, szRoleName, "Successfully login", true);
                }
                else
                {
                    LoginRecordToHippa(loginName, szRoleName, "login failed", false);
                }
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                LoginRecordToHippa(loginName, szRoleName, "login failed", false);
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return rt;
        }
        /// <summary>
        /// Get local name with user guid
        /// </summary>
        /// <param name="strUserGuid"></param>
        /// <returns></returns>
        public virtual string GetLocalName(string strUserGuid)
        {
            string result = null;
            RisDAL oKodak = new RisDAL();
            string sql = string.Format(@"select LocalName from  tUser where UserGUID = '{0}'", strUserGuid.Trim());
            try
            {
                result = oKodak.ExecuteScalar(sql).ToString();
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return null;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return result;
        }

        /// <summary>
        /// Get Role name with user guid
        /// </summary>
        /// <param name="strUserGuid"></param>
        /// <returns></returns>
        public virtual string GetRoleName(string strUserGuid)
        {
            string result = null;
            RisDAL oKodak = new RisDAL();
            string sql = string.Format(@"select top 1 RoleName from  tRole2User where UserGUID = '{0}'", strUserGuid.Trim());
            try
            {
                result = oKodak.ExecuteScalar(sql).ToString();
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return null;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return result;
        }


        /// <summary>
        /// Do server side DomainLoginName, RoleName, LoginName  check
        /// </summary>
        /// <param name="DomainLoginName"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public virtual string GetUserGuidByDmnLgnName(string DomainLoginName, string RoleName)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("select top 1 tUser.UserGuid ");
            strBuilder.Append("from  tUser,  tRole2User, tUser2Domain ");
            strBuilder.Append("where tRole2User.UserGuid = tUser.UserGuid and tUser.UserGuid=tUser2Domain.UserGuid ");
            strBuilder.AppendFormat("and tUser2Domain.DomainLoginName = '{0}' and tRole2User.RoleName = '{1}' ",
                                    DomainLoginName, RoleName);
            strBuilder.AppendFormat(" and tUser2Domain.Domain='{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
            string rt = "";
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                rt = Convert.ToString(oKodakDAL.ExecuteScalar(strBuilder.ToString()));
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }

            return rt;
        }


        /// <summary>
        /// Do server side role, username, password and role check
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="Password"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public virtual string GetUserGuid(string LoginName, string Password, string RoleName)
        {

            //            StringBuilder strBuilder = new StringBuilder();
            //            strBuilder.Append("select top 1 tUser.UserGuid ");
            //            strBuilder.Append("from tUser, tRole2User,tUser2Domain ");
            //            strBuilder.Append("where tRole2User.UserGuid = tUser.UserGuid and tUser.UserGuid=tUser2Domain.UserGuid ");
            //            strBuilder.Append("and tUser.LoginName = @szLoginName and tRole2User.RoleName = @szRoleName and tUser.PassWord = @szPassWord");
            //            strBuilder.AppendFormat(" and tUser2Domain.Domain='{0}'", CommonGlobalSettings.Utilities.GetCurDomain());

            string rt = "";
            RisDAL oKodakDAL = new RisDAL();
            bool isFromLoginFrm = false;
            try
            {
                #region Modified by Blue for [RC603.1] - US16931, 06/09/2014
                if (LoginName.Contains("|FromLoginForm"))
                {
                    LoginName = LoginName.Substring(0, LoginName.LastIndexOf("|"));
                    isFromLoginFrm = true;
                }
                string strSQL = string.Format("update tUser set Password = '{0}' where LoginName = '{1}'", Password, LoginName);
                if (RoleName.Contains("|ChangePassword"))
                {
                    RoleName = RoleName.Substring(0, RoleName.LastIndexOf("|"));
                    oKodakDAL.ExecuteNonQuery(strSQL);
                    LoginRecordToHippa(LoginName, RoleName, "Change Password", true);
                }
                #endregion

                #region Modified by Blue for RC603.2 - US16932, 05/29/2014
                //check whether user is locked or not
                strSQL = string.Format("select IsLocked from tUser where LoginName = '{0}' and Domain = '{1}'", LoginName, CommonGlobalSettings.Utilities.GetCurDomain());
                object objLocked = oKodakDAL.ExecuteScalar(strSQL);
                int iLocked = objLocked != null ? Convert.ToInt32(objLocked) : 0;
                if (iLocked == 1)
                {
                    throw new LoginAccountLockedException();
                }
                #endregion

                string strspName = "dbo.SP_GetUserGuid";

                logger.Info((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, strspName,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                oKodakDAL.Parameters.Clear();
                oKodakDAL.Parameters.AddVarChar("@szLoginName", LoginName);
                oKodakDAL.Parameters.AddVarChar("@szRoleName", RoleName);
                oKodakDAL.Parameters.AddVarChar("@szPassWord", Password);
                oKodakDAL.Parameters.AddVarChar("szDomain", CommonGlobalSettings.Utilities.GetCurDomain());

                DataTable dt = oKodakDAL.ExecuteQuerySP(strspName);

                if (dt != null && dt.Rows.Count > 0)
                {
                    rt = Convert.ToString(dt.Rows[0][0]);
                    if (rt == "1")
                        throw new LoginServerInvalidNameException();
                    else if (rt == "2")
                        throw new LoginServerInvalidRoleException();
                    else if (rt == string.Empty)
                        throw new LoginServerInvalidPwdException();
                    #region Modified by Blue for RC603.10 - US16934, 06/09/2014
                    //check whether the use is already online
                    bool isCurrentUserOnline = false;
                    string sq = string.Format("select IsOnline from tOnlineClient inner join tUser on tUser.UserGuid = tOnlineClient.UserGuid where tUser.LoginName = '{0}' and tOnlineClient.Domain = '{1}' and tOnlineClient.Site = '{2}'", LoginName, CommonGlobalSettings.Utilities.GetCurDomain(), CommonGlobalSettings.Utilities.GetCurSite());
                    object objIsOnline = oKodakDAL.ExecuteScalar(sq);
                    isCurrentUserOnline = objIsOnline != null && objIsOnline != DBNull.Value && objIsOnline.ToString().Equals("1");
                    if (!isCurrentUserOnline)
                    {
                        //retrieve site profile max user count
                        sq = string.Format("select Value from tSiteProfile where Name = 'OnlineUserCheckTimePeriod' and Domain = '{0}' and Site = '{1}'", CommonGlobalSettings.Utilities.GetCurDomain(), CommonGlobalSettings.Utilities.GetCurSite());
                        object objSitePeriod = oKodakDAL.ExecuteScalar(sq);
                        bool isSiteLevel = objSitePeriod != null && objSitePeriod != DBNull.Value;
                        //retrieve system profile max user count
                        sq = string.Format("select Value from tSystemProfile where Name = 'OnlineUserCheckTimePeriod' and Domain = '{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
                        object objSystemPeriod = oKodakDAL.ExecuteScalar(sq);
                        bool isDomainLevel = objSystemPeriod != null && objSystemPeriod != DBNull.Value;
                        if (isSiteLevel || isDomainLevel)
                        {
                            int profileMaxUserNumber = GetMaxOnlineUserCount(isSiteLevel ? objSitePeriod.ToString() : objSystemPeriod.ToString());
                            //retrieve current user count
                            int currentUserNumber = 0;
                            sq = isSiteLevel ? string.Format("select count(1) from tOnlineClient where IsOnline = '1' and Domain = '{0}' and Site = '{1}'", CommonGlobalSettings.Utilities.GetCurDomain(), CommonGlobalSettings.Utilities.GetCurSite())
                                : string.Format("select count(1) from tOnlineClient where IsOnline = '1' and Domain = '{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
                            object objCurrentNumber = oKodakDAL.ExecuteScalar(sq);
                            if (objCurrentNumber != null && objCurrentNumber != DBNull.Value)
                            {
                                int.TryParse(objCurrentNumber.ToString(), out currentUserNumber);
                            }
                            if (currentUserNumber >= profileMaxUserNumber)
                            {
                                throw new LoginMaxOnlineUserErrorException();
                            }
                        }
                    }
                    #endregion
                    #region Modified by Blue for RC603.2 - US16932, 05/29/2014
                    //login successfully, reset InvalidLoginCount to 0
                    string sql = string.Format("update tUser set InvalidLoginCount = 0 where LoginName = '{0}' and Domain = '{1}'", LoginName, CommonGlobalSettings.Utilities.GetCurDomain());
                    oKodakDAL.ExecuteNonQuery(sql);
                    #endregion

                    #region Modified by Blue for RC603.1 - US16931, 06/10/2014
                    DataAccessLayer.MyCryptography c = new DataAccessLayer.MyCryptography("GCRIS2-20061025");
                    //check whether user login for the first time
                    sq = string.Format("select Value from tSystemProfile where Name = 'UserPasswordComplexityRegex' and Domain = '{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
                    object objCheckPwd = oKodakDAL.ExecuteScalar(sq);
                    if (objCheckPwd != null && objCheckPwd != DBNull.Value && !string.IsNullOrEmpty(objCheckPwd.ToString())
                        //&& Password.Equals(FormsAuthentication.HashPasswordForStoringInConfigFile("111111", "SHA1")))
                        && Password.Equals(c.Encrypt("111111")))
                    {
                        throw new LoginChangePasswordErrorException();
                    }
                    #endregion
                }
                else
                    throw new LoginServerInvalidPwdException();
            }

            catch (LoginServerInvalidNameException)
            {
                LoginRecordToHippa(LoginName, RoleName, "Invalid server", false);
                throw;
            }
            catch (LoginServerInvalidPwdException ex)
            {
                #region Modified by Blue for RC603.2 - US16932, 05/29/2014
                if (isFromLoginFrm)
                {
                    //Check InvalidLoginMaxCount is 0 or not
                    string sql = string.Format("select Value from tSystemProfile where Name = 'InvalidLoginMaxCount' and Domain = '{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
                    int count = Convert.ToInt32(oKodakDAL.ExecuteScalar(sql));
                    if (count != 0)
                    {
                        //Get current login count
                        sql = string.Format("select InvalidLoginCount from tUser where LoginName = '{0}' and Domain = '{1}'", LoginName, CommonGlobalSettings.Utilities.GetCurDomain());
                        int invalidCount = Convert.ToInt32(oKodakDAL.ExecuteScalar(sql));
                        if (invalidCount >= count - 1)
                        {
                            //lock the account
                            sql = string.Format("update tUser set IsLocked = 1 where LoginName = '{0}' and Domain = '{1}'", LoginName, CommonGlobalSettings.Utilities.GetCurDomain());
                            oKodakDAL.ExecuteNonQuery(sql);
                            LoginRecordToHippa(LoginName, RoleName, "Lock Account", true);
                        }
                        else
                        {
                            //update InvalidLoginCount
                            sql = string.Format("update tUser set InvalidLoginCount = InvalidLoginCount + 1 where LoginName = '{0}' and Domain = '{1}'", LoginName, CommonGlobalSettings.Utilities.GetCurDomain());
                            oKodakDAL.ExecuteNonQuery(sql);
                            LoginRecordToHippa(LoginName, RoleName, "Invalid Password", false);
                        }
                        throw new LoginServerInvalidPwdException(string.Format("LoginServerInvalidPwdException.InvalidLoginCount.{0}", count - invalidCount - 1));
                    }
                    else
                    {
                        LoginRecordToHippa(LoginName, RoleName, "Invalid Password", false);
                    }
                }
                #endregion

                throw;
            }
            catch (LoginMaxOnlineUserErrorException)
            {
                LoginRecordToHippa(LoginName, RoleName, "Max user count reached", false);
                throw;
            }
            catch (LoginChangePasswordErrorException)
            {
                LoginRecordToHippa(LoginName, RoleName, "Change password failed", false);
                throw;
            }
            catch (LoginAccountLockedException)
            {
                LoginRecordToHippa(LoginName, RoleName, "Account is locked", false);
                throw;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                LoginServerDBErrorException dbEx = new LoginServerDBErrorException("LoginServerDBErrorException-" + Ex.Message);
                LoginRecordToHippa(LoginName, RoleName, Ex.Message, false);
                //For client identify the type of exception
                throw dbEx;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }

            return rt;
        }

        #region Modified by Blue for RC603.2 - US16932, 05/29/2014
        public virtual int LoginRecordToHippa(string userName, string roleName, string comment, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserID = "";
            string strIPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            string UserName = userName;
            try
            {
                string sqlGetUserID = string.Format("select UserGuid from  tUser where LoginName='{0}'", UserName);
                strUserID = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserID));
                string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}' ", strUserID);
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetUserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strIPAddress = dt.Rows[0]["MachineIP"].ToString();
                    strMachineName = dt.Rows[0]["MachineName"].ToString();
                    strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                }
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain, OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain, @OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "User Authentication");
                oKodakDAL.Parameters.AddChar("@EventActionCode", "E");
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", "Login");
                oKodakDAL.Parameters.AddChar("@UserID", strUserID);
                oKodakDAL.Parameters.AddChar("@UserName", UserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", roleName);
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "");
                oKodakDAL.Parameters.AddChar("@PartObjectID", strIPAddress);
                oKodakDAL.Parameters.AddChar("@PartObjectName", "");
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4}", userName, strIPAddress, strMachineName, strMACAddress, strLocation));
                oKodakDAL.Parameters.AddChar("@Comments", comment);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10); 
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }
        #endregion

        #region Modified by Blue for RC603.10 - US16934, 06/09/2014
        private string GetConfigDicValue(string ConfigName, string ModuleID)
        {
            string val = string.Empty;
            try
            {
                Server.ClientFramework.Common.Data.Profile.DsConfigDic ds = LoadConfigDic(2);
                Server.ClientFramework.Common.Data.Profile.DsConfigDic ds1 = new Server.ClientFramework.Common.Data.Profile.DsConfigDic();
                ds1.ReadXml(System.Configuration.ConfigurationManager.AppSettings["ConfigDicFilePath"]);
                if (ds != null)
                {
                    Server.ClientFramework.Common.Data.Profile.DsConfigDic.ConfigDicRow row = ds.ConfigDic.FindByConfigNameModuleID(ConfigName, ModuleID);
                    if (row != null)
                    {
                        val = row.Value;
                    }
                }

                if (string.IsNullOrWhiteSpace(val) && ds1 != null)
                {
                    Server.ClientFramework.Common.Data.Profile.DsConfigDic.ConfigDicRow row = ds1.ConfigDic.FindByConfigNameModuleID(ConfigName, ModuleID);
                    if (row != null)
                    {
                        val = row.Value;
                    }
                }
            }
            catch
            { }

            return val;
        }

        private int GetMaxOnlineUserCount(string onlineUserCountSettingString)
        {
            int count = int.MaxValue;
            string[] items = onlineUserCountSettingString.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (items != null && items.Length > 0)
            {
                foreach (string item in items)
                {
                    string[] periodCounts = item.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (periodCounts != null && periodCounts.Length == 2)
                    {
                        string ct = periodCounts[1];
                        string[] periods = periodCounts[0].Split("~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (periods != null && periods.Length == 2)
                        {
                            DateTime beginTime = new DateTime();
                            DateTime.TryParse(periods[0], out beginTime);
                            DateTime endTime = new DateTime();
                            DateTime.TryParse(periods[1], out endTime);
                            DateTime now = DateTime.Now.AddSeconds(-DateTime.Now.Second);
                            if (string.Compare(now.ToString("yyyy-MM-dd HH:mm"), beginTime.ToString("yyyy-MM-dd HH:mm")) >= 0 && string.Compare(now.ToString("yyyy-MM-dd HH:mm"), endTime.ToString("yyyy-MM-dd HH:mm")) <= 0)
                            {
                                int.TryParse(ct, out count);
                                break;
                            }
                        }
                    }
                }
            }
            return count;
        }
        #endregion

        /// <summary>
        /// Get all Module and Panel information (All)
        /// The panels will be filtered by Authentication Panels Check( check license ),
        /// and then will be filtered at clientside by AuthorizedModules and Panels information at Roleprofile.
        /// </summary>
        /// <returns></returns>
        public virtual DsPanelInfo LoadDsPanelInfo()
        {
            DsPanelInfo m_dsPanelInfo = new DsPanelInfo();
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //string szQuery1 = "SELECT ModuleID, 'Modules.' + ModuleID as Title, Parameter, ImageIndex FROM  tModule Where (Parameter & 1) = 0 order by OrderNo";
                string szQuery1 = "SELECT ModuleID,  Title, Parameter, ImageIndex FROM  tModule Where (Parameter & 1) = 0 order by OrderNo";
                oKodakDAL.ExecuteQuery(szQuery1, m_dsPanelInfo, "Module");
                //string szQuery2 = "SELECT PanelID, 'Panels.' + PanelID as Title, AssemblyQualifiedName, Parameter, ModuleID, Flag, ImageIndex, [Key] FROM tPanel Where (Parameter & 1) = 0 order by OrderNo";
                string szQuery2 = "SELECT PanelID,  Title, AssemblyQualifiedName, Parameter, ModuleID, Flag, ImageIndex, [Key] FROM  tPanel Where (Parameter & 1) = 0 order by OrderNo";
                oKodakDAL.ExecuteQuery(szQuery2, m_dsPanelInfo, "Panel");
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                m_dsPanelInfo = null;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return m_dsPanelInfo;
        }

        /// <summary>
        /// Get System Profile Information
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public virtual DsProfile LoadSystemProfile(string ModuleID)
        {
            RisDAL oKodakDAL = new RisDAL();
            DsProfile m_ds = new DsProfile();
            try
            {
                string szQuery1 = "SELECT Name, isnull(Value, '') as Value, ModuleID, Exportable, isnull(PropertyDesc, '') as PropertyDesc, isnull(PropertyOptions, '') as PropertyOptions, Inheritance, PropertyType, IsHidden, OrderingPos FROM  tSystemProfile " +
                    " WHERE  (ModuleID = '" + ModuleID + "')";
                oKodakDAL.ExecuteQuery(szQuery1, m_ds, "Profile");
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                m_ds = null;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return m_ds;
        }

        /// <summary>
        /// Get Site Profile Information
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public virtual DsProfile LoadSiteProfile(string ModuleID, string SiteName)
        {
            RisDAL oKodakDAL = new RisDAL();
            DsProfile m_ds = new DsProfile();
            try
            {
                string szQuery1 = "SELECT Name, isnull(Value, '') as Value, ModuleID, Exportable, isnull(PropertyDesc, '') as PropertyDesc, isnull(PropertyOptions, '') as PropertyOptions, Inheritance, PropertyType, IsHidden, OrderingPos FROM  tSiteProfile " +
                    " WHERE  ModuleID = '" + ModuleID + "'";
                if (!string.IsNullOrWhiteSpace(SiteName))
                {
                    szQuery1 += " and Site = '" + SiteName + "'";
                }
                oKodakDAL.ExecuteQuery(szQuery1, m_ds, "Profile");
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                m_ds = null;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return m_ds;
        }

        public virtual DataSet LoadAllSiteProfile(string ModuleID)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet m_ds = new DataSet();
            try
            {
                string szQuery1 = "SELECT Name, isnull(Value, '') as Value, ModuleID, Exportable, isnull(PropertyDesc, '') as PropertyDesc, isnull(PropertyOptions, '') as PropertyOptions, Inheritance, PropertyType, IsHidden, OrderingPos + '|' + Site as OrderingPos FROM  tSiteProfile " +
                    " WHERE  ModuleID = '" + ModuleID + "'";
                oKodakDAL.ExecuteQuery(szQuery1, m_ds, "Profile");
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                m_ds = null;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return m_ds;
        }

        /// <summary>
        /// Get RoleProfile Information
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public virtual DsProfile LoadRoleProfile(string ModuleID, string RoleID)
        {
            RisDAL oKodakDAL = new RisDAL();
            DsProfile m_ds = new DsProfile();
            try
            {
                string szQuery1 = "select Name, isnull(Value, '') as Value, ModuleID, Exportable, isnull(PropertyDesc, '') as PropertyDesc, isnull(PropertyOptions, '') as PropertyOptions, Inheritance, PropertyType, IsHidden, OrderingPos FROM  tRoleProfile " +
                    " where  (ModuleID = '" + ModuleID + "') and (RoleName = '" + RoleID + "') and (Domain='" + CommonGlobalSettings.Utilities.GetCurDomain() + "')";
                oKodakDAL.ExecuteQuery(szQuery1, m_ds, "Profile");
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                m_ds = null;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return m_ds;
        }

        /// <summary>
        /// Get User profile information
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="RoleID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public virtual DsProfile LoadUserProfile(string ModuleID, string RoleID, string UserID)
        {
            RisDAL oKodakDAL = new RisDAL();
            DsProfile m_ds = new DsProfile();
            try
            {
                string szQuery1 = "select Name, isnull(Value, '') as Value, ModuleID, Exportable, isnull(PropertyDesc, '') as PropertyDesc, isnull(PropertyOptions, '') as PropertyOptions, Inheritance, PropertyType, IsHidden, OrderingPos FROM  tUserProfile " +
                    " where  (ModuleID = '" + ModuleID + "') and (RoleName = '" + RoleID + "') and (UserGUID = '" + UserID + "')";
                oKodakDAL.ExecuteQuery(szQuery1, m_ds, "Profile");
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                m_ds = null;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return m_ds;
        }

        /// <summary>
        /// Save the changed data to user Profile
        /// </summary>
        /// <param name="Ds"></param>
        /// <param name="RoleName"></param>
        /// <param name="UserGUID"></param>
        /// <returns></returns>
        public virtual int SaveUserProfile(DataSet Ds, string RoleName, string UserGUID)
        {
            RisDAL oKodakDAL = new RisDAL();
            string szCmd = "";
            try
            {
                foreach (DataRow row in Ds.Tables["Profile"].Rows)
                {
                    switch (row.RowState)
                    {
                        case DataRowState.Added:
                            szCmd = "INSERT INTO tUserProfile" +
                                    "(Name,ModuleID, UserGUID, RoleName, Value,  Exportable, PropertyDesc, PropertyOptions, Inheritance, " +
                                    "PropertyType, IsHidden, OrderingPos,Domain)" +
                                    "VALUES " +
                                    "('" + row["Name"] +
                                    "', '" + row["ModuleID"] +
                                    "', '" + UserGUID +
                                    "', '" + RoleName +
                                    "' ,@Value," + row["Exportable"] +
                                    " , '" + row["PropertyDesc"] +
                                    "' , '" + row["PropertyOptions"] +
                                    "' , " + row["Inheritance"] +
                                    " , " + row["PropertyType"] +
                                    " , " + row["IsHidden"] +
                                    " , " + row["OrderingPos"] +
                                    " , '" + CommonGlobalSettings.Utilities.GetCurDomain() + "')";
                            break;
                        case DataRowState.Modified:
                            szCmd = "update tUserProfile " +
                                    "set Name = '" + row["Name"] +
                                    "', ModuleID = '" + row["ModuleID"] +
                                    "', UserGUID = '" + UserGUID +
                                    "', RoleName = '" + RoleName +
                                    "', Value = @Value, Exportable = " + row["Exportable"] +
                                    ", PropertyDesc = '" + row["PropertyDesc"] +
                                    "', PropertyOptions = '" + row["PropertyOptions"] +
                                    "', Inheritance = " + row["Inheritance"] +
                                    ", PropertyType = " + row["PropertyType"] +
                                    ", IsHidden = " + row["IsHidden"] +
                                    ", OrderingPos = " + row["OrderingPos"] +
                                    " where Name = '" + row["Name", DataRowVersion.Original] +
                                    "' and ModuleID = '" + row["ModuleID", DataRowVersion.Original] +
                                    "' and RoleName = '" + RoleName +
                                    "' and UserGUID = '" + UserGUID +
                                    "' and Domain = '" + CommonGlobalSettings.Utilities.GetCurDomain() + "'";
                            break;
                        case DataRowState.Deleted:
                            szCmd = "delete tUserProfile " +
                                    " where Name = '" + row["Name", DataRowVersion.Original] +
                                    "' and ModuleID = '" + row["ModuleID", DataRowVersion.Original] +
                                    "' and RoleName = '" + RoleName +
                                    "' and UserGUID = '" + UserGUID +
                                    "' and Domain = '" + CommonGlobalSettings.Utilities.GetCurDomain() + "'";
                            break;
                    }

                    try
                    {
                        oKodakDAL.Parameters.Clear();
                        oKodakDAL.Parameters.Add("@Value", row["Value"]);
                        oKodakDAL.ExecuteNonQuery(szCmd);
                    }
                    catch (Exception sqlEx)//it is the duplicated index,so go to next
                    {
                        if (sqlEx.Message.Contains("ix_tuserprofile"))
                        {
                            logger.Info((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, sqlEx.Message,
                                 string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                            continue;
                        }
                        else
                        {
                            throw sqlEx;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return 0;
        }
        public KeyValuePair<int, int> ExpireDayCheck(string LoginName)
        {

            RisDAL oKodakDAL = new RisDAL();
            KeyValuePair<int, int> rt;
            if (LoginName.Contains("|FromLoginForm"))
            {
                LoginName = LoginName.Substring(0, LoginName.LastIndexOf("|"));
            }
            try
            {
                string sql = string.Format("select B.IsSetExpireDate,B.StartDate,B.EndDate from  tUser A,tUser2Domain B where A.UserGuid=B.UserGuid and A.LoginName = '{0}' and B.Domain='{1}'", LoginName.Trim(), CommonGlobalSettings.Utilities.GetCurDomain());
                string sqlLogOut = string.Format("Update dbo.tOnlineClient Set IsOnline = 0 where UserGuid in (select UserGuid from  tUser where LoginName = '{0}' )", LoginName.Trim());
                DataTable expireDateTable = oKodakDAL.ExecuteQuery(sql);
                if (expireDateTable.Rows.Count != 1)
                {
                    rt = new KeyValuePair<int, int>(-1, -1);
                    oKodakDAL.ExecuteNonQuery(sqlLogOut);
                    return rt;
                }
                DataRow expireDateRow = expireDateTable.Rows[0];
                if (expireDateRow["IsSetExpireDate"] == null || expireDateRow["IsSetExpireDate"] == DBNull.Value)
                {
                    rt = new KeyValuePair<int, int>(-1, -1);
                    oKodakDAL.ExecuteNonQuery(sqlLogOut);
                    return rt;
                }
                if (Convert.ToInt32(expireDateRow["IsSetExpireDate"]) == 0)
                {
                    rt = new KeyValuePair<int, int>(0, -1);
                    return rt;
                }
                if (expireDateRow["StartDate"] == null || expireDateRow["EndDate"] == null || expireDateRow["StartDate"] == DBNull.Value || expireDateRow["EndDate"] == DBNull.Value)
                {
                    rt = new KeyValuePair<int, int>(-1, -1);
                    oKodakDAL.ExecuteNonQuery(sqlLogOut);
                    return rt;
                }
                DateTime beginDate = Convert.ToDateTime(expireDateRow["StartDate"]);
                DateTime endDate = Convert.ToDateTime(expireDateRow["EndDate"]);
                if (DateTime.Now.Date < beginDate.Date)
                {
                    rt = new KeyValuePair<int, int>(1, -1);
                    oKodakDAL.ExecuteNonQuery(sqlLogOut);
                    return rt;
                }
                if (DateTime.Now.Date > endDate.Date)
                {
                    rt = new KeyValuePair<int, int>(2, -1);
                    oKodakDAL.ExecuteNonQuery(sqlLogOut);
                    return rt;
                }
                //sql = string.Format("select value from tSystemProfile where Name = 'UserAccountPreWarningDays' and ModuleID = '0000'");

                int maxDays = Convert.ToInt32(ServerCommon.DaoInstanceFactory.GetInstance().GetSystemProfileValue("UserAccountPreWarningDays", "0000"));//Convert.ToInt32(oKodakDAL.ExecuteScalar(sql));
                TimeSpan ts = new TimeSpan();
                ts = endDate.Date - DateTime.Now.Date;
                if (ts.Duration().Days + 1 > maxDays)
                {
                    rt = new KeyValuePair<int, int>(0, -1);
                    return rt;
                }
                rt = new KeyValuePair<int, int>(3, ts.Duration().Days + 1);
                return rt;


            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                rt = new KeyValuePair<int, int>(-1, -1);
                return rt;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }
        public KeyValuePair<int, int> GetExpireDays(string LoginName)
        {
            KeyValuePair<int, int> re = ExpireDayCheck(LoginName);
            KeyValuePair<int, int> rt;
            if (re.Key == 3)
            {
                rt = new KeyValuePair<int, int>(re.Key, re.Value);
                return rt;
            }
            else
            {
                rt = new KeyValuePair<int, int>(0, -1);
                return rt;
            }
        }
        #region HIPAA Functions
        private bool IsHipaaEnable()
        {

            //string sql = "Select value from tsystemprofile where name='HipaaEnabled'";
            //KodakDAL oKodakDAL = new KodakDAL();

            try
            {
                string v = Server.DAO.Common.DaoInstanceFactory.GetInstance().GetSystemProfileValue("HipaaEnabled", "0000");//Convert.ToString(oKodakDAL.ExecuteScalar(sql)).Trim();
                if (v == null || v == "" || v == "0")
                {
                    return false;
                }

            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return true;
            }
            //finally
            //{
            //    if (oKodakDAL != null)
            //    {
            //        oKodakDAL.Dispose();
            //    }
            //}
            return true;
        }

        //login，logout失败了怎么办，ip写在哪？要先登录，再调要这个接口
        public virtual int AuditUserAuthMsg(string EventTypeCode, bool bHijackLogin, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserName = "";
            string strIPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            string UserID = HttpContext.Current.Session["UserGuid"].ToString();
            try
            {
                string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}' ", UserID);
                string sqlGetUserName = string.Format("select LoginName from  tUser where UserGuid='{0}'", UserID);
                strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetUserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strIPAddress = dt.Rows[0]["MachineIP"].ToString();
                    strMachineName = dt.Rows[0]["MachineName"].ToString();
                    strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                }
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "User Authentication");
                oKodakDAL.Parameters.AddChar("@EventActionCode", "E");
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", EventTypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", strUserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", HttpContext.Current.Session["RoleName"].ToString());
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "");
                oKodakDAL.Parameters.AddChar("@PartObjectID", strIPAddress);
                oKodakDAL.Parameters.AddChar("@PartObjectName", "");
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4}", strUserName, strIPAddress, strMachineName, strMACAddress, strLocation));
                oKodakDAL.Parameters.AddChar("@Comments", bHijackLogin ? "HijackLogin" : "Logout");
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }
        //数据库中没有ObjectDescription这一列
        public virtual int AuditPatientRecordEvtMsg(string ActionCode, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserName = "";
            string strIPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string UserID = HttpContext.Current.Session["UserGuid"].ToString();
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            try
            {
                string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}'", UserID);
                string sqlGetUserName = string.Format("select LoginName from  tUser where UserGuid='{0}'", UserID);
                strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetUserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strIPAddress = dt.Rows[0]["MachineIP"].ToString();
                    strMachineName = dt.Rows[0]["MachineName"].ToString();
                    strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                }
                string strEventTypeCode = "";
                switch (ActionCode)
                {
                    case "C":
                        strEventTypeCode = "Create Patient";
                        break;
                    case "R":
                        strEventTypeCode = "Read Patient";
                        break;
                    case "U":
                        strEventTypeCode = "Update Patient";
                        break;
                    case "D":
                        strEventTypeCode = "Delete Patient";
                        break;
                    case "MP":
                        strEventTypeCode = "Merge Patient";
                        break;
                    case "RP":
                        strEventTypeCode = "Relate Patient";
                        break;
                    case "CA":
                        strEventTypeCode = "Create Allergy";
                        break;
                    case "DA":
                        strEventTypeCode = "Delete Allergy";
                        break;

                    default:
                        break;
                }
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "Patient Record");
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", strEventTypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", strUserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", HttpContext.Current.Session["RoleName"].ToString());
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "Person");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "Patient");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "Patient ID");
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4},Patient:{5},PatientID:{6},Action:{7}", strUserName, strIPAddress, strMachineName, strMACAddress, strLocation, ObjectName,ObjectID, strEventTypeCode));
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }

        }
        public virtual int AuditPatientRecordEvtMsg(string ActionCode, string TypeCode, string UserID, string UserName, string RoleName, string NetworkIP, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            //string strUserName = "";
            //string strIPAddress = "";

            try
            {
                //string sqlGetUserIP = string.Format("select MachineIP from tOnlineClient where UserGuid='{0}' and (comments is null or comments != 'web login user')", UserID);
                //string sqlGetUserName = string.Format("select LoginName from tUser where UserGuid='{0}'", UserID);
                //strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                //strIPAddress = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserIP));
                //string strEventTypeCode = "";
                //switch (ActionCode)
                //{
                //    case "C":
                //        strEventTypeCode = "Create Patient";
                //        break;
                //    case "R":
                //        strEventTypeCode = "Read Patient";
                //        break;
                //    case "U":
                //        strEventTypeCode = "Update Patient";
                //        break;
                //    case "D":
                //        strEventTypeCode = "Delete Patient";
                //        break;

                //}
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "Patient Record");
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", TypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", UserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", RoleName);
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "Person");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "Patient");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "Patient ID");
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", ObjectDetail);
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }

        }
        private int AuditOrderRecordEvtMsgEx(string ActionCode, string AccessionNumber, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserName = "";
            string strIPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string UserID = HttpContext.Current.Session["UserGuid"].ToString();
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            try
            {
                string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}' ", UserID);
                string sqlGetUserName = string.Format("select LoginName from  tUser where UserGuid='{0}'", UserID);
                strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetUserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strIPAddress = dt.Rows[0]["MachineIP"].ToString();
                    strMachineName = dt.Rows[0]["MachineName"].ToString();
                    strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                }
                string strEventTypeCode = "";
                switch (ActionCode)
                {
                    case "C":
                        strEventTypeCode = "Create Order";
                        break;
                    case "R":
                        strEventTypeCode = "Read Order";
                        break;
                    case "U":
                        strEventTypeCode = "Update Order";
                        break;
                    case "D":
                        strEventTypeCode = "Delete Order";
                        break;
                    case "QU":
                        strEventTypeCode = "Quash Order";
                        break;
                    case "RE":
                        strEventTypeCode = "Resume Order";

                        break;
                }
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "Order Record");
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", strEventTypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", strUserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", HttpContext.Current.Session["RoleName"].ToString());
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "Person");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "Patient");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "Patient ID");
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4},Patient:{5},AccessionNumber:{6},Action:{7}", strUserName, strIPAddress, strMachineName, strMACAddress, strLocation, ObjectName, AccessionNumber, strEventTypeCode));
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }
        private int AuditOrderRecordEvtMsgForQC(string ActionCode, string AccessionNumber, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserName = "";
            string strIPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string UserID = HttpContext.Current.Session["UserGuid"].ToString();
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            try
            {
                string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}'", UserID);
                string sqlGetUserName = string.Format("select LoginName from  tUser where UserGuid='{0}'", UserID);
                strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetUserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strIPAddress = dt.Rows[0]["MachineIP"].ToString();
                    strMachineName = dt.Rows[0]["MachineName"].ToString();
                    strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                }
                string strEventTypeCode = "";
                switch (ActionCode)
                {
                    case "MV":
                        strEventTypeCode = "Merge Visit";
                        break;
                    case "S":
                        strEventTypeCode = "Separate Visit";
                        break;
                    case "MVO":
                        strEventTypeCode = "Move Order";
                        break;
                    case "MRO":
                        strEventTypeCode = "Merge Order";
                        break;
                }
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "Order Record");
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", strEventTypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", strUserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", HttpContext.Current.Session["RoleName"].ToString());
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "Person");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "Patient");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "Patient ID");
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4},Patient:{5},Accession Number:{6},Action:{7}", strUserName, strIPAddress, strMachineName, strMACAddress, strLocation, ObjectName, AccessionNumber, strEventTypeCode));
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }

        private int AuditOrderRecordEvtMsgForExamApplication(string ActionCode, string AccessionNumber, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserName = "";
            string strIPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string UserID = HttpContext.Current.Session["UserGuid"].ToString();
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            try
            {
                string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}'", UserID);
                string sqlGetUserName = string.Format("select LoginName from  tUser where UserGuid='{0}'", UserID);
                strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetUserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strIPAddress = dt.Rows[0]["MachineIP"].ToString();
                    strMachineName = dt.Rows[0]["MachineName"].ToString();
                    strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                }

                string strEventTypeCode = "";
                switch (ActionCode)
                {
                    case "Rej":
                        strEventTypeCode = "Reject ExamApplication";
                        break;
                }
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "Order Record");
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", strEventTypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", strUserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", HttpContext.Current.Session["RoleName"].ToString());
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "Person");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "Patient");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "Patient ID");
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4},Patient:{5},Accession Number:{6},Action:{7}", strUserName, strIPAddress, strMachineName, strMACAddress, strLocation, ObjectName, AccessionNumber, strEventTypeCode));
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }

        public virtual int AuditOrderRecordEvtMsg(string ActionCode, string AccessionNumber, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            try
            {
                if (ActionCode == "MV" || ActionCode == "S" || ActionCode == "MVO" || ActionCode == "MRO")
                {
                    return AuditOrderRecordEvtMsgForQC(ActionCode, AccessionNumber, ObjectID, ObjectName, ObjectDescription, isSuccess);
                }
                if (ActionCode == "Rej")
                {
                    return AuditOrderRecordEvtMsgForExamApplication(ActionCode, AccessionNumber, ObjectID, ObjectName, ObjectDescription, isSuccess);
                }
                else
                {
                    return AuditOrderRecordEvtMsgEx(ActionCode, AccessionNumber, ObjectID, ObjectName, ObjectDescription, isSuccess);
                }
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
        }
        public virtual int AuditOrderRecordEvtMsg(string ActionCode, string TypeCode, string UserID, string UserName, string RoleName, string NetworkIP, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserName = "";
            string strIPAddress = "";
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            try
            {
                //string sqlGetUserIP = string.Format("select MachineIP from tOnlineClient where UserGuid='{0}' and (comments is null or comments != 'web login user')", UserID);
                // string sqlGetUserName = string.Format("select LoginName from tUser where UserGuid='{0}'", UserID);
                // strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                // strIPAddress = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserIP));
                //string strEventTypeCode = "";
                //switch (ActionCode)
                //{
                //    case "C":
                //        strEventTypeCode = "Create Order";
                //        break;
                //    case "R":
                //        strEventTypeCode = "Read Order";
                //        break;
                //    case "U":
                //        strEventTypeCode = "Update Order";
                //        break;
                //    case "D":
                //        strEventTypeCode = "Delete Order";
                //        break;
                //    case "M":
                //        strEventTypeCode = "Merge Order";
                //        break;
                //    case "S":
                //        strEventTypeCode = "Separate Order";
                //        break;

                //}
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "Order Record");
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", TypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", UserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", RoleName);
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "Order Message");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "Order Message");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "Order GUID");
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", ObjectDetail);
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }
        public virtual int AuditEvtMsg(string EventID, string ActionCode, string TypeCode, string EventOutComeIndicator, string UserID, string UserName, string RoleName, string NetworkIP, string UserIsRequestor, string ObjectTypeCode, string ObjectTypeCodeRole, string ObjectIDTypeCode, string ObjectID, string ObjectName, string ObjectDetail, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            try
            {
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                    + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", EventID);
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", TypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", UserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", UserIsRequestor);
                oKodakDAL.Parameters.AddChar("@RoleName", RoleName);
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", ObjectTypeCode);
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", ObjectTypeCodeRole);
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", ObjectIDTypeCode);
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", ObjectDetail);
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                    string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }
        //要加accessnumber 参数码?
        //要加accessnumber 参数码?
        public virtual int AuditProcedureRecordEvtMsgQC(string ActionCode, string AccessionNumber, string RP, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserName = "";
            string strIPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string UserID = HttpContext.Current.Session["UserGuid"].ToString();
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            try
            {
                string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}'", UserID);
                string sqlGetUserName = string.Format("select LoginName from  tUser where UserGuid='{0}'", UserID);
                strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetUserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strIPAddress = dt.Rows[0]["MachineIP"].ToString();
                    strMachineName = dt.Rows[0]["MachineName"].ToString();
                    strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                }
                string strEventTypeCode = "";
                switch (ActionCode)
                {
                    case "C":
                        strEventTypeCode = "Create Procedure";
                        break;
                    case "R":
                        strEventTypeCode = "Read Procedure";
                        break;
                    case "U":
                        strEventTypeCode = "Update Procedure";
                        break;
                    case "D":
                        strEventTypeCode = "Delete Procedure";
                        break;
                    case "M":
                        strEventTypeCode = "Move Procedure";
                        break;


                }
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "Procedure Record");
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", strEventTypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", strUserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", HttpContext.Current.Session["RoleName"].ToString());
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "System");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "Report");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "Study Instance UID");
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4},Patient:{5},AccessionNumber:{6},RP:{7},Action:{8}", strUserName, strIPAddress, strMachineName, strMACAddress, strLocation, ObjectName, AccessionNumber, RP, strEventTypeCode));
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }
        public virtual int AuditProcedureRecordEvtMsg(string strEventTypeCode, string AccessionNumber, string RP, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserName = "";
            string strIPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string UserID = HttpContext.Current.Session["UserGuid"].ToString();
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            try
            {
                string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}'", UserID);
                string sqlGetUserName = string.Format("select LoginName from  tUser where UserGuid='{0}'", UserID);
                strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetUserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strIPAddress = dt.Rows[0]["MachineIP"].ToString();
                    strMachineName = dt.Rows[0]["MachineName"].ToString();
                    strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                }
                string ActionCode = "U";


                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "Procedure Record");
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", strEventTypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", strUserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", HttpContext.Current.Session["RoleName"].ToString());
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "System");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "Report");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "Study Instance UID");
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4},Patient:{5},AccessionNumber:{6},RP:{7},Action:{8}", strUserName, strIPAddress, strMachineName, strMACAddress, strLocation, ObjectName, AccessionNumber, RP, strEventTypeCode));
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }
        //public virtual int AuditProcedureRecordEvtMsg(String ActionCode, String UserID, String AccessionNumber, String RP, String ObjectID, String ObjectName, String ObjectDescription)
        //{
        //    return 1;
        //}
        public virtual int AuditPatientCareAssignMsg(string ActionCode, string AccessionNumber, string ReportID, string ReportName, string ObjectID, string ObjectName, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserName = "";
            string strIPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string UserID = HttpContext.Current.Session["UserGuid"].ToString();
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            try
            {
                string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}'", UserID);
                string sqlGetUserName = string.Format("select LoginName from  tUser where UserGuid='{0}'", UserID);
                strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetUserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strIPAddress = dt.Rows[0]["MachineIP"].ToString();
                    strMachineName = dt.Rows[0]["MachineName"].ToString();
                    strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                }
                string strEventTypeCode = "";
                switch (ActionCode)
                {
                    case "C":
                        strEventTypeCode = "Create Report";
                        break;
                    case "Sub":
                        strEventTypeCode = "Submit Report";
                        break;
                    case "App":
                        strEventTypeCode = "Approved Report";
                        break;
                    case "Reb":
                        strEventTypeCode = "Rebuild Report";
                        break;
                    case "Rej":
                        strEventTypeCode = "Reject Report";
                        break;
                    case "D":
                        strEventTypeCode = "Delete Report";
                        break;
                    case "SecondApp":
                        strEventTypeCode = "Secondly Approved Report";
                        break;
                    case "CancelSub":
                        strEventTypeCode = "Cancel Submitted Report";
                        break;

                }
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult)";

                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "Patient Care Resource Assignment");
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", GlobalCommon.ToLongDateTime(DateTime.Now));
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", strEventTypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", strUserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", HttpContext.Current.Session["RoleName"].ToString());
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "Person");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "Patient");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "Patient ID");
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4},Patient:{5},AccessionNumber:{6},ReportID:{7},ReportName:{8}Action:{9}", strUserName, strIPAddress, strMachineName, strMACAddress, strLocation, ObjectName, AccessionNumber, ReportID, ReportName, strEventTypeCode));
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }

        public virtual int AuditChargeRecordEvtMsg(string ActionCode, string AccessionNumber, string ObjectID, string ObjectName, string ChargeID, string ChargeDescription, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserName = "";
            string strIPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string UserID = HttpContext.Current.Session["UserGuid"].ToString();
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            try
            {
                string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}' ", UserID);
                string sqlGetUserName = string.Format("select LoginName from  tUser where UserGuid='{0}'", UserID);
                strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetUserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strIPAddress = dt.Rows[0]["MachineIP"].ToString();
                    strMachineName = dt.Rows[0]["MachineName"].ToString();
                    strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                }
                string strEventTypeCode = "";
                switch (ActionCode)
                {
                    case "C":
                        strEventTypeCode = "Create Charge";
                        break;
                    case "R":
                        strEventTypeCode = "Read Order";
                        break;
                    case "U":
                        strEventTypeCode = "Update Charge";
                        break;
                    case "D":
                        strEventTypeCode = "Delete Charge";
                        break;
                }
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult) ";
                sql += "WAITFOR DELAY '0:0:00';";
                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "Charge Record");
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", strEventTypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", strUserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", HttpContext.Current.Session["RoleName"].ToString());
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "Person");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "Patient");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "Patient ID");
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail",
                    string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4},Patient:{5},AccessionNumber:{6},ChargeID:{7},ChargeDescription:{8},Action:{9}",
                    strUserName, strIPAddress, strMachineName, strMACAddress, strLocation, ObjectName, AccessionNumber, ChargeID, ChargeDescription, strEventTypeCode));
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }


        public virtual int AuditSuppilesRecordEvtMsg(string ActionCode,string strEventTypeCode,string ObjectID, string ObjectName, string SupplierID, string SupplierDescription, string ObjectDescription, bool isSuccess)
        {
            if (!IsHipaaEnable())
            {
                return 0;
            }
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.Clear();
            string strUserName = "";
            string strIPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string UserID = HttpContext.Current.Session["UserGuid"].ToString();
            string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            try
            {
                string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}' ", UserID);
                string sqlGetUserName = string.Format("select LoginName from  tUser where UserGuid='{0}'", UserID);
                strUserName = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserName));
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetUserIP);
                if (dt != null && dt.Rows.Count > 0)
                {
                    strIPAddress = dt.Rows[0]["MachineIP"].ToString();
                    strMachineName = dt.Rows[0]["MachineName"].ToString();
                    strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                    strLocation = dt.Rows[0]["Location"].ToString();
                }
                
               
                string sql = "INSERT INTO RISHippa..tActivityLog(ALGuid,EventID,EventActionCode,EventDt,EventOutComeIndicator,EventTypeCode,UserGuid,UserName,UserIsRequestor,RoleName,PartObjectTypeCode,PartObjectTypeCodeRole,PartObjectIDTypeCode,PartObjectID,PartObjectName,PartObjectDetail,Comments,Domain,OperationResult) "
                              + " VALUES(@ALGuid,@EventID,@EventActionCode,@EventDate,@EventOutComeIndicator,@EventTypeCode,@UserID,@UserName,@UserIsRequestor,@RoleName,@PartObjectTypeCode,@PartObjectTypeCodeRole,@PartObjectIDTypeCode,@PartObjectID,@PartObjectName ,@PartObjectDetail,@Comments,@Domain,@OperationResult) ";
                sql += "WAITFOR DELAY '0:0:00';";


                oKodakDAL.Parameters.AddChar("@ALGuid", Guid.NewGuid().ToString());
                oKodakDAL.Parameters.AddChar("@EventID", "Supplies Record");
                oKodakDAL.Parameters.AddChar("@EventActionCode", ActionCode);
                oKodakDAL.Parameters.AddDateTime("@EventDate", DateTime.Now.ToString());
                oKodakDAL.Parameters.AddChar("@EventOutComeIndicator", "");
                oKodakDAL.Parameters.AddChar("@EventTypeCode", strEventTypeCode);
                oKodakDAL.Parameters.AddChar("@UserID", UserID);
                oKodakDAL.Parameters.AddChar("@UserName", strUserName);
                oKodakDAL.Parameters.AddChar("@UserIsRequestor", "TRUE");
                oKodakDAL.Parameters.AddChar("@RoleName", HttpContext.Current.Session["RoleName"].ToString());
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCode", "Person");
                oKodakDAL.Parameters.AddChar("@PartObjectTypeCodeRole", "Supplies");
                oKodakDAL.Parameters.AddChar("@PartObjectIDTypeCode", "Supplies ID");
                oKodakDAL.Parameters.AddChar("@PartObjectID", ObjectID==null?"":ObjectID);
                oKodakDAL.Parameters.AddChar("@PartObjectName", ObjectName);
                oKodakDAL.Parameters.AddChar("@PartObjectDetail", string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4},Detail:{5},Action:{6}", strUserName, strIPAddress, strMachineName, strMACAddress, strLocation, SupplierDescription, strEventTypeCode));
                
                oKodakDAL.Parameters.AddChar("@Comments", ObjectDescription);
                oKodakDAL.Parameters.AddChar("@Domain", strDomain);
                oKodakDAL.Parameters.AddChar("@OperationResult", isSuccess ? "Success" : "Failed");
                
                //oKodakDAL.BeginExecuteNonQuery(HandleCallback, sql);
                //Thread.Sleep(10);
                oKodakDAL.ExecuteNonQuery(sql);
                return 0;
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return 1;
            }
            finally
            {
                /* TA74076 defect the oKodakDAL will use in HandleCallback, should not dispose in here!*/
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }

        private void HandleCallback(IAsyncResult result)
        {
            RisDAL oKodakDAL = null;
            try
            {
                logger.Info((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, "HandleCallBack invoked",
                    string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                oKodakDAL = (RisDAL)result.AsyncState;

                oKodakDAL.EndExecuteNonQuery(result);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
        }

        #endregion

        #region BillBoard
        public DataSet GetAllNotesInDB(string userGuid, string roleName)
        {

            RisDAL oKodakDAL = new RisDAL();
            DataSet billBoardDataSet = new DataSet();
            string currentDomain = CommonGlobalSettings.Utilities.GetCurDomain();
            string currentSite = CommonGlobalSettings.Utilities.GetCurSite();
            string userDept = "";
            try
            {
                string szQuery1 = string.Format(@"select Guid,Title,groupId,groupType,Type,BeginDate,EndDate,Intervals,ShowTime,AttachmentURL,Body,Creator,CreateDate from tBillBoard where EndDate>='{0}' and (tBillBoard.Guid in (select guid from tBillBoardOperation where state>2 and (Site is NULL or Site = '' or Site ='{4}'))) and Domain ='{3}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), roleName, userGuid.Trim(), currentDomain, currentSite);
                string szUserDept = string.Format("select department from tuser2domain where userguid='{0}' and Domain ='{1}'", userGuid.Trim(), currentDomain);
                DataTable dtUserDept = oKodakDAL.ExecuteQuery(szUserDept);
                if (dtUserDept != null)
                {
                    userDept = (dtUserDept.Rows.Count > 0 ? Convert.ToString(dtUserDept.Rows[0][0]) : "");
                }

                DataTable billBoardTable = oKodakDAL.ExecuteQuery(szQuery1);
                DataTable newBoardTable = billBoardTable.Clone();
                int groupType;
                string[] arrays;
                newBoardTable.Columns["Body"].DataType = typeof(string);
                newBoardTable.TableName = "BillBoard";
                StringBuilder sbGuids = new StringBuilder();
                foreach (DataRow curRow in billBoardTable.Rows)
                {
                    int.TryParse(Convert.ToString(curRow["GroupType"]), out groupType);
                    arrays = Convert.ToString(curRow["GroupID"]).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (groupType == 1 && Array.Exists(arrays, m => m.Equals(userDept))//dept
                        || groupType == 2 && Array.Exists(arrays, m => m.Equals(roleName))//role
                        || groupType == 3 && Array.Exists(arrays, m => m.Equals(userGuid))//user
                        || groupType == 0)
                    {
                        newBoardTable.Rows.Add(curRow["Guid"].ToString(), curRow["Title"].ToString(), Convert.ToString(curRow["groupId"]), Convert.ToInt32(curRow["GroupType"]), Convert.ToInt32(curRow["Type"]), Convert.ToDateTime(curRow["BeginDate"]), Convert.ToDateTime(curRow["EndDate"]), Convert.ToDouble(curRow["Intervals"]), Convert.ToDouble(curRow["ShowTime"]), curRow["AttachmentURL"].ToString(), System.Text.Encoding.Default.GetString(curRow["Body"] as byte[]), curRow["Creator"].ToString(), GlobalCommon.ToLongDateTime(Convert.ToDateTime(curRow["CreateDate"])));
                        sbGuids.Append("'" + curRow["Guid"].ToString() + "'" + ",");
                    }
                }

                string bulletinGuids = sbGuids.ToString();
                if (bulletinGuids.Length > 0)
                    bulletinGuids = bulletinGuids.Remove(bulletinGuids.Length - 1, 1);
                else
                    bulletinGuids = "''";
                string szQuery2 = string.Format(@"select [Guid],[Submitter],[SubmitDate],[SubmitTo],[Approver],[ApproveDate],[Rejector],[RejectDate],[RejectCause],[State],[OperationHistory],[Counts],[Publisher],[PublishDate],[Domain] from tBillBoardOperation where state>2 and tBillBoardOperation.Guid in ({0})", bulletinGuids);
                DataTable billBoardOperationTable = oKodakDAL.ExecuteQuery(szQuery2);
                billBoardOperationTable.TableName = "BillBoardOperation";
                billBoardDataSet.Tables.Add(newBoardTable);
                billBoardDataSet.Tables.Add(billBoardOperationTable);
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                billBoardDataSet = null;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return billBoardDataSet;
        }

        public DataSet GetBillBoardDictionaryData()
        {

            RisDAL oKodakDAL = new RisDAL();
            DataSet dicDataSet = new DataSet();
            try
            {
                string szQuery1 = string.Format("select * from tDictionaryValue where (tag=50 or tag=51 or tag=2) and ((Tag not in (select distinct Tag from tDictionaryValue where Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') and (Site='' or Site is null)) or Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "')");

                DataTable dicTable = oKodakDAL.ExecuteQuery(szQuery1);

                dicTable.TableName = "DictionaryTable";
                dicDataSet.Tables.Add(dicTable);

            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                dicDataSet = null;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return dicDataSet;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual DataSet LoadDictionary()
        {

            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();

            try
            {
                string strSQL = "SELECT * from tDictionaryValue where (Tag not in (select distinct Tag from tDictionaryValue where Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') and (Site='' or Site is null)) or Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "' order by tag,orderid,text asc";
                DataTable dt1 = oKodakDAL.ExecuteQuery(strSQL);
                dt1.TableName = "Dictionary";
                ds.Tables.Add(dt1);

                strSQL = string.Format("select tGridColumn.Guid,tGridColumn.ColumnWidth,tGridColumn.IsHidden,tGridColumnOption.ColumnID," +
                           "tGridColumnOption.TableName,tGridColumnOption.ColumnName,tGridColumnOption.Expression,tGridColumnOption.ListName,tGridColumn.OrderID," +
                           "tGridColumnOption.ModuleID,tGridColumnOption.IsImageColumn,tGridColumnOption.ImagePath from tGridColumn left join tGridColumnOption on " +
                           "tGridColumn.Guid=tGridColumnOption.Guid where " +
                           " tGridColumn.UserGuid='' " +
                           "order by tGridColumn.OrderID ");


                DataTable dt2 = oKodakDAL.ExecuteQuery(strSQL);
                dt2.TableName = "GridColumnList";
                ds.Tables.Add(dt2);

                strSQL = string.Format("select Domain,DomainPrefix,Connstring,Ftpserver,FtpPort,FtpUser,FtpPassword,PacsAETitle,PacsServer,PacsWebServer,Address,Telephone,Alias from tDomainList  ");


                DataTable dt3 = oKodakDAL.ExecuteQuery(strSQL);
                dt3.TableName = "DomainList";
                ds.Tables.Add(dt3);


                strSQL = string.Format("select * from tUser order by localname ");

                DataTable dt4 = oKodakDAL.ExecuteQuery(strSQL);
                dt4.TableName = "AllStaff";
                ds.Tables.Add(dt4);


                strSQL = string.Format("Select tUser.LocalName,tUser.loginname,tUser.UserGuid from tUser,tUser2Domain where tUser.UserGuid=tUser2Domain.UserGuid and tUser2Domain.Domain='{0}' and tuser.DeleteMark = 0 and (tUser2Domain.IsSetExpireDate = 0 or (tUser2Domain.StartDate <= GETDATE() and GETDATE() < DATEADD(D,1,tUser2Domain.EndDate))) order by tUser.LocalName", CommonGlobalSettings.Utilities.GetCurDomain());

                DataTable dt5 = oKodakDAL.ExecuteQuery(strSQL);
                dt5.TableName = "ValidDomainStaff";
                ds.Tables.Add(dt5);

                strSQL = string.Format("Select * from tPhraseTemplate");

                DataTable dt6 = oKodakDAL.ExecuteQuery(strSQL);
                dt6.TableName = "PhraseTemplate";
                ds.Tables.Add(dt6);

                strSQL = string.Format("select * from tSiteList");


                DataTable dt7 = oKodakDAL.ExecuteQuery(strSQL);
                dt7.TableName = "SiteList";
                ds.Tables.Add(dt7);

                strSQL = @"select * from tApplyDept order by ShortCutCode";
                DataTable dt8 = oKodakDAL.ExecuteQuery(strSQL);
                dt8.TableName = "tApplyDept";
                ds.Tables.Add(dt8);

                strSQL = @"select * from tApplyDoctor order by ShortCutCode";
                DataTable dt9 = oKodakDAL.ExecuteQuery(strSQL);
                dt9.TableName = "tApplyDoctor";
                ds.Tables.Add(dt9);

                //DICTIONARY FOR ALL SITES
                strSQL = "SELECT * from tDictionaryValue order by tag,orderid,text asc";
                DataTable dt10 = oKodakDAL.ExecuteQuery(strSQL);
                dt10.TableName = "AllDictionary";
                ds.Tables.Add(dt10);


                strSQL = "SELECT * from tPhysicalCompany order by clinicfullname";
                DataTable dt11 = oKodakDAL.ExecuteQuery(strSQL);
                dt11.TableName = "clinic";
                ds.Tables.Add(dt11);

            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return ds;
        }

        public virtual DataSet GetExamInfo(string strExamDomain, string strAccNo)
        {


            DataSet ds = new DataSet();

            try
            {

                //if (strExamDomain.Trim().Length > 0)
                //{
                //    string strSQL = string.Format("SELECT AccNo,ExamAccNo,ExamDomain,Domain,ExamSite from tRegOrder where AccNo='{0}' and ExamDomain='{1}'", strAccNo, strExamDomain);
                //    oKodakDAL = new KodakDAL(2);
                //    DataTable dt = oKodakDAL.ExecuteQuery(strSQL);
                //    dt.TableName = "ExamInfo";
                //    ds.Tables.Add(dt);
                //}
                //else
                //{
                string strSQL = string.Format("SELECT AccNo,ExamAccNo,ExamDomain,Domain,ExamSite from tRegOrder where AccNo='{0}'", strAccNo);
                using (RisDAL oKodakDAL = new RisDAL())
                {
                    DataTable dt = oKodakDAL.ExecuteQuery(strSQL);
                    dt.TableName = "ExamInfo";
                    ds.Tables.Add(dt);
                    //}
                }


            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }

            return ds;
        }
        /// <summary>
        /// Get role profile according to Role name and the configuration name
        /// </summary>
        /// <param name="name">configuration name</param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual string GetRoleProfileValue(string name, string roleName)
        {
            using (RisDAL access = new RisDAL())
            {
                try
                {
                    object obj = access.ExecuteScalar(string.Format("select Value from tRoleProfile where Name='{0}' and RoleName='{1}' and Domain='{2}'",
                                                    name, roleName, CommonGlobalSettings.Utilities.GetCurDomain()));
                    return Convert.ToString(obj);
                }
                catch (Exception ex)
                {
                    logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, ex.Message,
                         string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                         (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// User LoginRole and name check
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public virtual string GetUserGuidByLoginName(string LoginName, string RoleName)
        {

            if (LoginName.Contains("|FromLoginForm"))
            {
                LoginName = LoginName.Substring(0, LoginName.LastIndexOf("|"));

            }

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("select top 1 tUser.UserGuid ");
            strBuilder.Append("from tUser, tRole2User,tUser2Domain ");
            strBuilder.Append("where tRole2User.UserGuid = tUser.UserGuid and tUser.UserGuid=tUser2Domain.UserGuid ");
            strBuilder.Append("and tUser.LoginName = @szLoginName and tRole2User.RoleName = @szRoleName ");
            strBuilder.AppendFormat(" and tUser2Domain.Domain='{0}'", CommonGlobalSettings.Utilities.GetCurDomain());

            string rt = "";
            RisDAL oKodakDAL = new RisDAL();
            oKodakDAL.Parameters.AddChar("@szLoginName", LoginName);
            oKodakDAL.Parameters.AddChar("@szRoleName", RoleName);

            try
            {
                string strSql = strBuilder.ToString();
                logger.Info((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, strSql,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                rt = Convert.ToString(oKodakDAL.ExecuteScalar(strSql));
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }

            return rt;
        }


        public virtual DataSet GetOnlineClients()
        {
            string sql = "select MachineIP,SessionID from tOnlineClient where IsOnline = 1";
            DataSet ds = new DataSet();
            try
            {
                using (RisDAL oKodakDAL = new RisDAL())
                {
                    DataTable dtClients = oKodakDAL.ExecuteQuery(sql);
                    dtClients.TableName = "OnClients";
                    ds.Tables.Add(dtClients);
                    return ds;
                }
            }
            catch (Exception ex)
            {

                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return ds;
        }

        public virtual DataSet GetFilterSite(string strUserGuid, string strRoleName, string strCurSite, string strMatchingName)
        {

            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();

            try
            {
                string strSQL = string.Format("SELECT * from tUserProfile where name='AccessSite' and userguid='{0}'", strUserGuid);
                DataTable dt1 = oKodakDAL.ExecuteQuery(strSQL);
                dt1.TableName = "usersite";
                ds.Tables.Add(dt1);

                strSQL = string.Format("select * from tRoleProfile where RoleName='{0}' and name='{1}'", strRoleName, strCurSite);
                DataTable dt2 = oKodakDAL.ExecuteQuery(strSQL);
                dt2.TableName = "rolesite";
                ds.Tables.Add(dt2);


                strSQL = string.Format("select * from tSiteProfile where name='{0}' and Site='{1}'", strMatchingName, strCurSite);
                DataTable dt3 = oKodakDAL.ExecuteQuery(strSQL);
                if (dt3 != null && dt3.Rows.Count > 0)
                {
                    dt3.TableName = "matchsite";
                    ds.Tables.Add(dt3);
                }
                else
                {

                    strSQL = string.Format("select * from tSystemProfile where name='{0}'", strMatchingName);
                    dt3 = oKodakDAL.ExecuteQuery(strSQL);
                    dt3.TableName = "matchsite";
                    ds.Tables.Add(dt3);
                }

            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return ds;
        }

        public string GetSite(string settingSite)
        {
            string site = "";
            try
            {
                using (RisDAL oKodakDAL = new RisDAL())
                {
                    object obj = oKodakDAL.ExecuteScalar(string.Format("select site from tsiteList where site = '{0}'", settingSite));
                    if (obj != null && obj != DBNull.Value)
                    {
                        site = obj.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return site;
        }

        public virtual DataSet LoadAllProfile()
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet m_ds = new DataSet();
            try
            {
                string columns = "Name, isnull(Value, '') as Value, ModuleID, Exportable, isnull(PropertyDesc, '') as PropertyDesc, isnull(PropertyOptions, '') as PropertyOptions, Inheritance, PropertyType, IsHidden, OrderingPos";

                string szQuery1 = " SELECT " + columns + ", '' Site, '' RoleName, '' UserGuid FROM tSystemProfile ";

                string szQuery2 = " SELECT " + columns + ", Site, '' RoleName, '' UserGuid FROM tSiteProfile ";

                string szQuery3 = " SELECT " + columns + ", '' Site, RoleName, '' UserGuid FROM tRoleProfile "
                    + " where (Domain='" + CommonGlobalSettings.Utilities.GetCurDomain() + "')";

                string szQuery4 = " SELECT " + columns + ", '' Site, RoleName, UserGuid FROM tUserProfile "
                    + " where (Domain='" + CommonGlobalSettings.Utilities.GetCurDomain() + "')";

                DataTable dt1 = new DataTable("tSystemProfile");
                DataTable dt2 = new DataTable("tSiteProfile");
                DataTable dt3 = new DataTable("tRoleProfile");
                DataTable dt4 = new DataTable("tUserProfile");

                oKodakDAL.ExecuteQuery(szQuery1, dt1);
                oKodakDAL.ExecuteQuery(szQuery2, dt2);
                oKodakDAL.ExecuteQuery(szQuery3, dt3);
                oKodakDAL.ExecuteQuery(szQuery4, dt4);

                m_ds.Tables.Add(dt1);
                m_ds.Tables.Add(dt2);
                m_ds.Tables.Add(dt3);
                m_ds.Tables.Add(dt4);
            }
            catch (Exception Ex)
            {
                logger.Error((long)ModuleEnum.Framework_Client, ModuleInstanceName.Framework, 1, Ex.Message,
                     string.Empty, (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                m_ds = null;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }

            return m_ds;
        }

        public virtual void UpdatePasswordNewEncry(string userguid, string password)
        {
            if (string.IsNullOrWhiteSpace(userguid))
                return;
            using (RisDAL oKodakDAL = new RisDAL())
            {
                oKodakDAL.ExecuteNonQuery(string.Format("update tuser set password='{0}' where userguid='{1}'", password, userguid));

            }
        }
    }



}


