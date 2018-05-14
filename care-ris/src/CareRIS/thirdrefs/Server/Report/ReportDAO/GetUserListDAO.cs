using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class GetUserListDAO
    {
        public object Execute(object param)
        {
            using (RisDAL oKodak = new RisDAL())
            {
                string clsType = string.Format("{0}_{1}", this.GetType().ToString(), oKodak.DriverClassName.ToUpper());

                Type type = Type.GetType(clsType);
                IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
                return iRptDAO.Execute(param);
            }
        }
    }

    internal class GetUserListDAO_ABSTRACT : IReportDAO
    {
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return iRptDAO.Execute(param);
        }
    }

    internal class GetUserListDAO_SYBASE : IReportDAO
    {
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return iRptDAO.Execute(param);
        }
    }

    internal class GetUserListDAO_MSSQL : IReportDAO
    {
        public object Execute(object param)
        {
            string sql = "";

            try
            {
                using (RisDAL dal = new RisDAL())
                {

                    sql = "select tRole2User.rolename, tUser.userGuid, LoginName, localName, englishname,"
                         + " isnull(tRoleProfile.value, 0) as [level], tUser.IKEYSN, u1.Value as WriteReportModalityType, u2.Value as ApproveReportModalityType"
                         + " from tUser left join tRole2User"
                         + "		 left join tRoleProfile on tRoleProfile.rolename=tRole2User.rolename and tRoleProfile.name='RoleLevel'"
                         + "   on tUser.UserGuid = tRole2User.UserGuid"
                         + "		 left join tUserProfile u1 on u1.UserGuid=tUser.UserGuid and u1.name='WriteReportModalityType'"
                         + "		 left join tUserProfile u2 on u2.UserGuid=tUser.UserGuid and u2.name='ApproveReportModalityType'"
                         + " order by tRole2User.rolename, loginName";

                    DataSet ds = new DataSet();

                    dal.ExecuteQuery(sql, ds, "UserList");

                    return ds;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetUserListDAO_MSSQL, MSG=" + ex.Message + ", SQL=" + sql, "", 0);
            }

            return null;
        }
    }

    internal class GetUserListDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                using (RisDAL dal = new RisDAL())
                {

                    string sql = "select rolename, tUser.userGuid, LoginName, localName, englishname "
                        + " from tUser, tRole2User where tUser.UserGuid = tRole2User.UserGuid"
                        + " order by rolename, loginName";

                    DataSet ds = new DataSet();

                    dal.ExecuteQuery(sql, ds, "UserList");

                    return ds;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetUserListDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }
}
