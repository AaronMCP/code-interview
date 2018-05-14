using System;
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Panels;
using Server.ClientFramework.Common.Data.Login;
using Server.ClientFramework.Common.Data.Profile;
using DataAccessLayer;
using LogServer;
using CommonGlobalSettings;
using System.Web;
//using Sybase.Data.AseClient;
using System.Data;

namespace Server.DAO.ClientFramework
{
    public class OracleImplement : AbstractImplement
    {
        ILogManager logger = new LogManager();

        public   override string GetUserGuid(string LoginName, string Password, string RoleName)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("select  tUser.UserGuid ");
            strBuilder.Append("from tUser, tRole2User ");
            strBuilder.Append("where tRole2User.UserGuid = tUser.UserGuid ");
            strBuilder.AppendFormat("and tUser.LoginName = '{0}' and tRole2User.RoleName = '{1}' and tUser.PassWord = '{2}' ",
                                    LoginName, RoleName, Password);           
            strBuilder.Append(" and rownum < 2");
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
        public override string GetDbServerTime()
        {
            string Cmd = @"Select sysdate from dual";

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
        public override int GetOnlineUserNo(bool bWebUser, bool bSelfUser, string ipaddress)
        {
            string Cmd = "";
            if (!bWebUser)
            {
                Cmd = @"Select count(*) from tOnlineClient where IsOnline = 1 and (comments is null or comments != 'web login user') ";
            }
            else
            {
                Cmd = @"Select count(*) from tOnlineClient where IsOnline = 1 and (comments= 'web login user')";
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

        public override void OnlineStatusInit()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("Update tOnlineClient Set IsOnline = 0");
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

        public override Server.ClientFramework.Common.Data.Profile.DsConfigDic LoadConfigDic(int Type)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(
                @"SELECT ConfigName,Value,tConfigDic.ModuleID,
                    tModule.Title as ModuleName,Exportable,
                    nvl(PropertyDesc, '') as PropertyDesc,
                    nvl(PropertyOptions, '') as PropertyOptions,
                    Inheritance,PropertyType,IsHidden,OrderingPos,
                    nvl(Domain, '') as Domain 
                FROM tConfigDic, tModule 
                Where Type= " + Type.ToString() + @" And tModule.ModuleID = tConfigDic.ModuleID");
            RisDAL oKodakDAL = new RisDAL();
            Server.ClientFramework.Common.Data.Profile.DsConfigDic rt =
                new Server.ClientFramework.Common.Data.Profile.DsConfigDic();
            try
            {
                oKodakDAL.ExecuteQuery(strBuilder.ToString(), rt.ConfigDic);
                foreach (DataRow myRow in rt.Tables["ConfigDic"].Rows)
                {
                    if (myRow["Domain"].Equals(DBNull.Value))
                        myRow["Domain"] = "";
                    if (myRow["PropertyDesc"].Equals(DBNull.Value))
                        myRow["PropertyDesc"] = "";
                    if (myRow["PropertyOptions"].Equals(DBNull.Value))
                        myRow["PropertyOptions"] = "";
                    if (myRow["Value"].Equals(DBNull.Value))
                        myRow["Value"] = "";
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
        public override void LogOut(string UserGuid, bool bWebUser)
        {
            StringBuilder strBuilder = new StringBuilder();
            if (bWebUser)
            {
                strBuilder.AppendFormat("Update tOnlineClient Set IsOnline = 0 where UserGuid = '{0}' and (comments = 'web login user')", UserGuid);
            }
            else
            {
                strBuilder.AppendFormat("Update tOnlineClient Set IsOnline = 0 where UserGuid = '{0}' and (comments is null or comments != 'web login user')", UserGuid);
            }
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

        public override DsPanelInfo LoadDsPanelInfo()
        {
            DsPanelInfo m_dsPanelInfo = new DsPanelInfo();
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //string szQuery1 = "SELECT ModuleID, 'Modules.' + ModuleID as Title, Parameter, ImageIndex FROM tModule Where (Parameter & 1) = 0 order by OrderNo";
                string szQuery1 = "SELECT ModuleID,  Title, Parameter, ImageIndex FROM tModule Where (bitand (Parameter , 1) = 0) order by OrderNo";
                oKodakDAL.ExecuteQuery(szQuery1, m_dsPanelInfo, "Module");
                //string szQuery2 = "SELECT PanelID, 'Panels.' + PanelID as Title, AssemblyQualifiedName, Parameter, ModuleID, Flag, ImageIndex, [Key] FROM tPanel Where (Parameter & 1) = 0 order by OrderNo";
                string szQuery2 = "SELECT PanelID,  Title, AssemblyQualifiedName, Parameter, ModuleID, Flag, ImageIndex, Key FROM tPanel Where (bitand (Parameter , 1) = 0) order by OrderNo";
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
        public override DsProfile LoadSystemProfile(string ModuleID)
        {
            RisDAL oKodakDAL = new RisDAL();
            DsProfile m_ds = new DsProfile();
            try
            {
                string szQuery1 = "SELECT Name, nvl(Value, '') as Value, ModuleID, Exportable, nvl(PropertyDesc, '') as PropertyDesc, nvl(PropertyOptions, '') as PropertyOptions, Inheritance, PropertyType, IsHidden, OrderingPos FROM tSystemProfile " +
                    " WHERE  (ModuleID = '" + ModuleID + "')";
                oKodakDAL.ExecuteQuery(szQuery1, m_ds, "Profile");
                foreach (DataRow myRow in m_ds.Tables["Profile"].Rows)
                {
                    if (myRow["Value"].Equals(DBNull.Value))
                        myRow["Value"] = "";
                    if (myRow["PropertyDesc"].Equals(DBNull.Value))
                        myRow["PropertyDesc"] = "";
                    if (myRow["PropertyOptions"].Equals(DBNull.Value))
                        myRow["PropertyOptions"] = "";
                }
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
        public override DsProfile LoadUserProfile(string ModuleID, string RoleID, string UserID)
        {
            RisDAL oKodakDAL = new RisDAL();
            DsProfile m_ds = new DsProfile();
            try
            {
                string szQuery1 = "select Name, nvl(Value, '') as Value, ModuleID, Exportable, nvl(PropertyDesc, '') as PropertyDesc, nvl(PropertyOptions, '') as PropertyOptions, Inheritance, PropertyType, IsHidden, OrderingPos FROM tUserProfile " +
                    " where  (ModuleID = '" + ModuleID + "') and (RoleName = '" + RoleID + "') and (UserGUID = '" + UserID + "')";
                oKodakDAL.ExecuteQuery(szQuery1, m_ds, "Profile");
                foreach (DataRow myRow in m_ds.Tables["Profile"].Rows)
                {
                    if (myRow["Value"].Equals(DBNull.Value))
                        myRow["Value"] = "";
                    if (myRow["PropertyDesc"].Equals(DBNull.Value))
                        myRow["PropertyDesc"] = "";
                    if (myRow["PropertyOptions"].Equals(DBNull.Value))
                        myRow["PropertyOptions"] = "";
                }
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
        public override DsProfile LoadRoleProfile(string ModuleID, string RoleID)
        {
            RisDAL oKodakDAL = new RisDAL();
            DsProfile m_ds = new DsProfile();
            try
            {
                string szQuery1 = "select Name, nvl(Value, '') as Value, ModuleID, Exportable, nvl(PropertyDesc, '') as PropertyDesc, nvl(PropertyOptions, '') as PropertyOptions, Inheritance, PropertyType, IsHidden, OrderingPos FROM tRoleProfile " +
                    " where  (ModuleID = '" + ModuleID + "') and (RoleName = '" + RoleID + "')";
                oKodakDAL.ExecuteQuery(szQuery1, m_ds, "Profile");
                foreach (DataRow myRow in m_ds.Tables["Profile"].Rows)
                {
                    if (myRow["Value"].Equals(DBNull.Value))
                        myRow["Value"] = "";
                    if (myRow["PropertyDesc"].Equals(DBNull.Value))
                        myRow["PropertyDesc"] = "";
                    if (myRow["PropertyOptions"].Equals(DBNull.Value))
                        myRow["PropertyOptions"] = "";
                }
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
        public override void LogOutBySessionID(string SessionID)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("Update tOnlineClient Set IsOnline = 0 where SessionID = '{0}'", SessionID);
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

        public override int IsOnLine(string szUserGuid, string szRoleName, string szIpAddress, string szUrl, string szSessionID, bool IsLogined, bool IsWebAccess,bool IsHijackLogin, bool IsSelfService)
        {
            string Cmd = "";
          
            int rt = 0;
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                if (IsWebAccess == false)
                {
                    oKodakDAL.Parameters.Clear();
                    oKodakDAL.Parameters.AddChar("UserID", szUserGuid);
                    oKodakDAL.Parameters.AddChar("UserRoleName", szRoleName);
                    oKodakDAL.Parameters.AddChar("Url", szUrl);
                    oKodakDAL.Parameters.AddChar("IpAddress", szIpAddress);
                    oKodakDAL.Parameters.AddChar("Session", szSessionID);
                    oKodakDAL.Parameters.AddInt("IsLogined", IsLogined == true ? 1 : 0);
                    oKodakDAL.Parameters.Add("ReturnValue", DbType.Int32);
                    oKodakDAL.Parameters["UserID"].Direction = ParameterDirection.Input;
                    oKodakDAL.Parameters["UserRoleName"].Direction = ParameterDirection.Input;
                    oKodakDAL.Parameters["Url"].Direction = ParameterDirection.Input;
                    oKodakDAL.Parameters["IpAddress"].Direction = ParameterDirection.Input;
                    oKodakDAL.Parameters["Session"].Direction = ParameterDirection.Input;
                    oKodakDAL.Parameters["IsLogined"].Direction = ParameterDirection.Input;
                    oKodakDAL.Parameters["ReturnValue"].Direction = ParameterDirection.Output;
                    oKodakDAL.ExecuteNonQuerySP("checkonline");
                    rt = Convert.ToInt32( oKodakDAL.Parameters["ReturnValue"].Value);
                    //rt = Convert.ToInt32(oKodakDAL.ExecuteScalarSP("checkonline"));
                }
                else
                {
                    oKodakDAL.Parameters.Clear();
                    oKodakDAL.Parameters.AddChar("UserID", szUserGuid);
                    oKodakDAL.Parameters.AddChar("UserRoleName", szRoleName);
                    oKodakDAL.Parameters.AddChar("Url", szUrl);
                    oKodakDAL.Parameters.AddChar("IpAddress", szIpAddress);
                    oKodakDAL.Parameters.AddChar("Session", szSessionID);
                    oKodakDAL.Parameters.Add("ReturnValue", DbType.Int32);
                    oKodakDAL.Parameters["UserID"].Direction = ParameterDirection.Input;
                    oKodakDAL.Parameters["UserRoleName"].Direction = ParameterDirection.Input;
                    oKodakDAL.Parameters["Url"].Direction = ParameterDirection.Input;
                    oKodakDAL.Parameters["IpAddress"].Direction = ParameterDirection.Input;
                    oKodakDAL.Parameters["Session"].Direction = ParameterDirection.Input;
                    oKodakDAL.Parameters["ReturnValue"].Direction = ParameterDirection.Output;
                    oKodakDAL.ExecuteNonQuerySP("CHECKWEBONLINE");
                    rt = Convert.ToInt32(oKodakDAL.Parameters["ReturnValue"].Value);
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
    }
}
