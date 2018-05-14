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
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;
using DataAccessLayer;
using Server.Utilities.Oam;
using CommonGlobalSettings;
using LogServer;
using Server.Utilities.LogFacility;
using System.Web;
using System.IO;


namespace Server.DAO.Oam.Impl
{
    public class OracleProvider : AbstractDBProvider
    {
        protected Server.Utilities.LogFacility.LogManagerForServer logger = new Server.Utilities.LogFacility.LogManagerForServer("OAMServerLoglevel", "0800");


        #region IRoleDAO

        #region LoadRole -- Load all the role to the user when the user chooses role management
        /// <summary>
        /// Name:LoadRole
        /// Function:Load the existing role in the Database when the user clicks the role setting
        /// Input: none
        /// Output:DataSet holding the existing role in the database
        /// Return:true if Success false if fail
        /// </summary>
        /// <returns>true or false</returns>

        private const string SQLAllRole = "SELECT distinct RoleName,Description,IsSystem from tRole";


        public override DataSet GetRoleDataSet(string strDomain)
        {

            RisDAL oKodakDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            string sql = SQLAllRole;
            try
            {
                DataTable dataTable = oKodakDAL.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                //Build the custom DataTabe
                DataTable customedTable = null;// Server.Utilities.Oam.CreataCustomRoleDataTable();

                foreach (DataRow row in dataTable.Rows)
                {
                    string[] rowData = new string[3];

                    //Rolename-0
                    rowData[0] = row[dataTable.Columns["RoleName"]] as string;
                    //RoleDescription-1
                    rowData[1] = row[dataTable.Columns["Description"]] as string;
                    rowData[2] = row[dataTable.Columns["IsSystem"]].ToString();

                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }
                dataSet.Tables.Add(customedTable);

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
                if (dataSet != null)
                {
                    dataSet.Dispose();
                }
            }
            return dataSet;
        }
        #endregion

        public override int AddRole(string strRoleName, string strRoleDescription, string strDomain,string parentId)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataTable dtSystemProfileDetails = null;
            try
            {
                List<string> listSQL = new List<string>();
                //Check if the role already exists. If yes then throw the exception

                if (IsRoleNameAreadyExists(strRoleName, oKodakDAL))
                {
                    return 1;
                }
                else
                {
                    //Copy the system profile to role profile
                    InsertRoleAndProfileDetails(strRoleName, "", strRoleDescription, listSQL, false, oKodakDAL, dtSystemProfileDetails);

                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return -1;

            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
                if (dtSystemProfileDetails != null)
                {
                    dtSystemProfileDetails.Dispose();
                }

            }
            return 0;
        }

        public override bool EditRole(RoleModel model, string strDomain)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                List<string> listSQL = new List<string>();
                StringBuilder strBuilder = new StringBuilder();
                foreach (DataTable table in model.SaveRoleProfile.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        strBuilder.AppendFormat("UPDATE tRoleProfile set Value = '{0}' where RoleName = '{1}' and Name = '{2}' and ModuleId = '{3}' and Domain='{4}'", row["FieldValue"].ToString(), row["RoleName"].ToString(), row["FieldName"].ToString(), row["ModuleId"].ToString(), strDomain);
                        listSQL.Add(strBuilder.ToString());
                        strBuilder.Remove(0, strBuilder.Length);
                    }
                    oKodakDAL.BeginTransaction();
                    foreach (string strSQL in listSQL)
                    {


                        logger.Debug((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, strSQL, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                            (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                        oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                    }
                    oKodakDAL.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return false;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return true;

        }

        public override int DeleteRole(string strRoleName, string strDomain)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //Defect #: EK_HI00040188
                string strRoleExistVldSQL = string.Format("SELECT RoleName FROM (SELECT * FROM tRole ORDER BY RoleName)  WHERE ROWNUM <= 1 and Rolename='{0}'  ORDER BY ROWNUM ASC ", strRoleName.ToString());
                string strRoleExist = Convert.ToString(oKodakDAL.ExecuteScalar(strRoleExistVldSQL));
                if (strRoleExist == string.Empty)
                {
                    //The role don't exist in the table(tRole).There cannot delete.
                    return 2;
                }

                List<string> listSQL = new List<string>();
                //Check if the user exists for the role to needs to be deleted. If yes then throw the exception
                string strUserExistVldSQL = string.Format("select RoleName from (select * from tRole2User order by RoleName)  Where rownum <= 1 and RoleName='{0}'  order by rownum asc ", strRoleName.ToString());
                string strUserExist = Convert.ToString(oKodakDAL.ExecuteScalar(strUserExistVldSQL));
                StringBuilder strBuilder = new StringBuilder();
                if (strUserExist != string.Empty)
                {
                    //user exists for the role. Therefore cannot delete
                    return 1;
                }
                else
                {
                    strBuilder.AppendFormat("DELETE FROM tRoleProfile where RoleName = '{0}'", strRoleName.ToString());
                    listSQL.Add(strBuilder.ToString());
                    strBuilder.Remove(0, strBuilder.Length);
                    strBuilder.AppendFormat("DELETE FROM tRole where RoleName = '{0}'", strRoleName.ToString());
                    listSQL.Add(strBuilder.ToString());
                }
                oKodakDAL.BeginTransaction();
                foreach (string strSQL in listSQL)
                {

                    logger.Debug((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, strSQL, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                    oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodakDAL.CommitTransaction();

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return -1;
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

        public override int CopyRole(string strNewRoleName, string strNewRoleDescription, string strOldRoleName, string strDomains,string strParentId)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataTable dtRoleProfileDetails = null;
            try
            {
                List<string> listSQL = new List<string>();
                //Check if the role already exists. If yes then throw the exception

                if (IsRoleNameAreadyExists(strNewRoleName, oKodakDAL))
                {
                    return 1;
                }
                else
                {
                    InsertRoleAndProfileDetails(strOldRoleName, strNewRoleName, strNewRoleDescription, listSQL, true, oKodakDAL, dtRoleProfileDetails);
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return -1;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
                if (dtRoleProfileDetails != null)
                {
                    dtRoleProfileDetails.Dispose();
                }
            }
            return 0;
        }

        public override DataSet GetRoleProfDetDataSet(string roleName, string strDomain, string userGuid, bool isSiteAdmin)
        {
            RisDAL oKodalDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            //DataTable dtSystemProf = new DataTable();
            string sql = SQLGetDictionaryList;
            string strGetRoleProfSQL = string.Empty;
            string strGetSysProfSQL = string.Empty;
            try
            {
                strGetRoleProfSQL = string.Format("SELECT RoleName,Name,tRoleProfile.ModuleID,Title as ModuleName,Value,Exportable,PropertyDesc,PropertyOptions,Inheritance,PropertyType,IsHidden,OrderingPos FROM tRoleProfile, tModule where tModule.ModuleID = tRoleProfile.ModuleID AND tRoleProfile.RoleName = '{0}' and (bitand(tRoleProfile.IsHidden,2) = 2 ) ORDER BY tRoleProfile.OrderingPos", roleName.Trim());
                DataTable dataTable = oKodalDAL.ExecuteQuery(strGetRoleProfSQL, RisDAL.ConnectionState.KeepOpen);
                //Build the custom DataTable
                DataTable customedTable = Server.Utilities.Oam.Utilities.CreataCustomDataTable();

                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                string[] arrDftVal = null;
                foreach (DataRow row in dataTable.Rows)
                {
                    string[] rowData = new string[11];

                    //role name - 0
                    rowData[0] = row[dataTable.Columns["RoleName"]].ToString();

                    //FieldName - 1
                    rowData[1] = row[dataTable.Columns["Name"]].ToString();

                    //FieldValue -2
                    rowData[2] = row[dataTable.Columns["Value"]].ToString();
                    //strValue = row[dataTable.Columns["Value"]].ToString();

                    //FieldDescription -3
                    rowData[3] = row[dataTable.Columns["ModuleId"]].ToString();

                    //ShorCut -4 -- Property Options
                    strPropertyOption = row[dataTable.Columns["PropertyOptions"]].ToString();
                    if (!strPropertyOption.Contains("|"))
                    {
                        if (strPropertyOption.Contains("[") || strPropertyOption == string.Empty)
                        {
                            rowData[4] += strPropertyOption;
                        }
                        else if (strPropertyOption != string.Empty)
                        {
                            arrPropertyOptionSQL = strPropertyOption.Split(';');
                            DataTable dtPropOption = new DataTable();
                            foreach (string strProp in arrPropertyOptionSQL)
                            {
                                oKodalDAL.ExecuteQuery(strProp, dtPropOption);
                            }
                            foreach (DataRow drPropDetail in dtPropOption.Rows)
                            {
                                if ((row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.ListBox)
                                  || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckComboBox)
                                  || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckBox))
                                {
                                    rowData[4] += drPropDetail[0] + "|";
                                }
                                else if ((row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.ComboBox))
                                {
                                    rowData[4] += "|" + drPropDetail[0];
                                }
                            }
                            if ((row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.ListBox)
                             || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckComboBox)
                                || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckBox))
                            {
                                if (rowData[4] != null)
                                    rowData[4] = rowData[4].Remove(rowData[4].Length - 1);
                                else
                                    rowData[4] = "";
                            }
                        }

                    }
                    else
                    {
                        rowData[4] += strPropertyOption;
                    }


                    //DefaultValue -5
                    if (rowData[2].Contains("|"))
                    {
                        arrDftVal = rowData[2].Split('|');
                        foreach (string strDftVal in arrDftVal)
                        {
                            //DefaultValue -5
                            rowData[5] += strDftVal + ",";
                        }
                        rowData[5] = rowData[5].Remove(rowData[5].Length - 1);
                    }
                    else
                    {
                        rowData[5] = rowData[2];
                    }

                    //CategoryName-6
                    rowData[6] = row[dataTable.Columns["ModuleName"]].ToString();

                    //FieldType- 7
                    rowData[7] = row[dataTable.Columns["PropertyType"]].ToString();

                    //RegularExpress-8
                    rowData[8] = "";

                    //Description-9
                    rowData[9] = row[dataTable.Columns["PropertyDesc"]].ToString();
                    rowData[10] = "";

                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }
                dataSet.Tables.Add(customedTable);

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oKodalDAL != null)
                {
                    oKodalDAL.Dispose();
                }
            }

            return dataSet;
        }

        #endregion

        #region IUserDAO Section

        public override DataSet LoadUserDataSet(string currentUserRole, string currentUserGUID,string strDomain,string strSite)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            string SQLAllUser = string.Empty;
            try
            {
                if (currentUserRole.Trim().ToUpper() == "ADMINISTRATOR")
                {
                    SQLAllUser = string.Format("select * from tUser where DeleteMark = 0");
                }
                else
                {
                    SQLAllUser = string.Format("select * from tUser where UserGuid = '{0}' and DeleteMark = 0", currentUserGUID.ToString());
                }


                DataTable dtAllUser = oKodakDAL.ExecuteQuery(SQLAllUser, RisDAL.ConnectionState.KeepOpen);
                dataSet.Tables.Add(dtAllUser);




                string SQLAllDepartment = string.Format("select B.Value,B.Text from tDictionary A,tDictionaryValue B where A.Tag = B.Tag and A.Tag = 2");
                DataTable dtAllDepartment = oKodakDAL.ExecuteQuery(SQLAllDepartment, RisDAL.ConnectionState.KeepOpen);
                dataSet.Tables.Add(dtAllDepartment);

                string SQLAllTitle = string.Format("select B.Value,B.Text from tDictionary A,tDictionaryValue B where A.Tag = B.Tag and A.Tag = 7");
                DataTable dtAllTitle = oKodakDAL.ExecuteQuery(SQLAllTitle, RisDAL.ConnectionState.KeepOpen);
                dataSet.Tables.Add(dtAllTitle);

                string SQLAllRole = string.Format("select distinct RoleName from tRole");
                DataTable dtAllRole = oKodakDAL.ExecuteQuery(SQLAllRole, RisDAL.ConnectionState.KeepOpen);
                dataSet.Tables.Add(dtAllRole);

                string SQLAllOnlineUsers = string.Format("select UserGuid from tOnlineClient where IsOnline = 1");
                DataTable dtAllOnlineUsers = oKodakDAL.ExecuteQuery(SQLAllOnlineUsers);


                DataTable dtSelectedForUser = new DataTable();
                DataRow drSelectedForUser = null;

                dtSelectedForUser.Columns.Add("LoginName", typeof(string));
                dtSelectedForUser.Columns.Add("LocalName", typeof(string));
                dtSelectedForUser.Columns.Add("EnglishName", typeof(string));
                dtSelectedForUser.Columns.Add("UserRole", typeof(string));
                dtSelectedForUser.Columns.Add("Department", typeof(string));
                //dtSelectedForUser.Columns.Add("Title", typeof(string));
                dtSelectedForUser.Columns.Add("Telephone", typeof(string));
                dtSelectedForUser.Columns.Add("DomainLoginName", typeof(string));
                dtSelectedForUser.Columns.Add("OnlineUser", typeof(string));
                dtSelectedForUser.Columns.Add("UserGuid", typeof(string));

                if (dtAllUser != null && dtAllUser.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAllUser.Rows)
                    {
                        drSelectedForUser = dtSelectedForUser.NewRow();
                        drSelectedForUser["UserGuid"] = dr["UserGuid"];

                        string SQLAllRoleForUser = string.Format("select RoleName from tRole2User where UserGuid = '{0}'", dr["UserGuid"].ToString());
                        DataTable dtAllRoleForUser = oKodakDAL.ExecuteQuery(SQLAllRoleForUser);
                        if (dtAllRoleForUser != null && dtAllRoleForUser.Rows.Count > 0)
                        {
                            string strAllRoleForUser = "";
                            foreach (DataRow drAllRole in dtAllRoleForUser.Rows)
                            {
                                strAllRoleForUser += drAllRole["RoleName"].ToString() + ",";
                            }
                            if (strAllRoleForUser != string.Empty)
                            {
                                strAllRoleForUser = strAllRoleForUser.Remove(strAllRoleForUser.Length - 1);
                            }
                            drSelectedForUser["UserRole"] = strAllRoleForUser;

                        }
                        else
                        {
                            drSelectedForUser["UserRole"] = "";
                        }
                        if (dtAllRoleForUser != null && dtAllRoleForUser.Rows.Count > 0)
                        {
                            dtAllRoleForUser.Rows.Clear();
                            dtAllRoleForUser.Clear();
                        }
                        drSelectedForUser["LoginName"] = dr["LoginName"];
                        drSelectedForUser["LocalName"] = dr["LocalName"];
                        drSelectedForUser["EnglishName"] = dr["EnglishName"];

                        DataRow[] arrDtDepartment = dtAllDepartment.Select("DictionaryValue = '" + dr["Department"].ToString() + "'");
                        if (arrDtDepartment.Length > 0)
                        {
                            drSelectedForUser["Department"] = arrDtDepartment[0]["Description"];
                        }
                        else
                        {
                            drSelectedForUser["Department"] = "";
                        }

                        //DataRow[] arrDtTitle = dtAllTitle.Select("DictionaryValue = '" + dr["Title"].ToString() + "'");
                        //if (arrDtTitle.Length > 0)
                        //{
                        //    drSelectedForUser["Title"] = arrDtTitle[0]["Description"];

                        //}
                        //else
                        //{
                        //    drSelectedForUser["Title"] = "";
                        //}

                        drSelectedForUser["Telephone"] = dr["Telephone"];
                        drSelectedForUser["DomainLoginName"] = dr["DomainLoginName"];
                        if (dtAllOnlineUsers != null)
                        {
                            if (dtAllOnlineUsers.Select("UserGuid = '" + Convert.ToString(dr["UserGuid"]) + "'").Length > 0)
                            {
                                drSelectedForUser["OnlineUser"] = "Yes";

                            }
                            else
                            {
                                drSelectedForUser["OnlineUser"] = "No";
                            }
                        }
                        else
                        {
                            drSelectedForUser["OnlineUser"] = "No";
                        }
                        dtSelectedForUser.Rows.Add(drSelectedForUser);
                    }
                    dataSet.Tables.Add(dtSelectedForUser);
                }



            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);

            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
                if (dataSet != null)
                {
                    dataSet.Dispose();
                }
            }
            return dataSet;
        }

        public override DataSet GetUserProfDetDataSet(string userGuid, string strDomain,string strSite)
        {
            RisDAL oKodalDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            //DataTable dtSystemProf = new DataTable();
            string strGetUserProfSQL = string.Empty;
            string strGetUserRoleSQL = string.Empty;
            try
            {
                strGetUserProfSQL = string.Format("select A.Name,A.ModuleId,B.Title as ModuleName,A.UserGuid,A.RoleName,A.Value,A.Exportable,A.PropertyDesc,A.PropertyOptions,A.Inheritance,A.PropertyType,A.IsHidden,A.OrderingPos from tUserProfile A, tModule B where A.ModuleId = B.ModuleId and (BitAnd(A.IsHidden,1) = 1) and A.UserGuid = '{0}' ORDER BY A.OrderingPos", userGuid.ToString());
                DataTable dataTable = oKodalDAL.ExecuteQuery(strGetUserProfSQL);
                //Build the custom DataTable
                DataTable customedTable = Server.Utilities.Oam.Utilities.CreataCustomDataTable();

                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                string[] arrDftVal = null;
                foreach (DataRow row in dataTable.Rows)
                {
                    string[] rowData = new string[11];

                    //role name - 0
                    rowData[0] = row[dataTable.Columns["ModuleId"]].ToString();

                    //FieldName - 1
                    rowData[1] = row[dataTable.Columns["Name"]].ToString();

                    //FieldValue -2
                    rowData[2] = row[dataTable.Columns["Value"]].ToString();
                    //strValue = row[dataTable.Columns["Value"]].ToString();

                    // Sub Category Module Name -3
                    rowData[3] = row[dataTable.Columns["ModuleName"]].ToString();

                    //ShorCut -4 -- Property Options
                    strPropertyOption = row[dataTable.Columns["PropertyOptions"]].ToString();
                    if (!strPropertyOption.Contains("|"))
                    {
                        if (strPropertyOption.Contains("[") || strPropertyOption == string.Empty)
                        {
                            rowData[4] += strPropertyOption;
                        }
                        else if (strPropertyOption != string.Empty)
                        {
                            arrPropertyOptionSQL = strPropertyOption.Split(';');
                            DataTable dtPropOption = new DataTable();
                            foreach (string strProp in arrPropertyOptionSQL)
                            {
                                oKodalDAL.ExecuteQuery(strProp, dtPropOption);
                            }
                            foreach (DataRow drPropDetail in dtPropOption.Rows)
                            {
                                if ((row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.ListBox)
                                  || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckComboBox)
                                  || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckBox))
                                {
                                    rowData[4] += drPropDetail[0] + "|";
                                }
                                else if ((row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.ComboBox))
                                {
                                    rowData[4] += "|" + drPropDetail[0];
                                }
                            }
                            if ((row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.ListBox)
                             || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckComboBox)
                                || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckBox))
                            {
                                if (rowData[4] != null)
                                    rowData[4] = rowData[4].Remove(rowData[4].Length - 1);
                                else
                                    rowData[4] = "";
                            }
                        }

                    }
                    else
                    {
                        rowData[4] += strPropertyOption;
                    }

                    //DefaultValue -5
                    if (rowData[2].Contains("|"))
                    {
                        arrDftVal = rowData[2].Split('|');
                        foreach (string strDftVal in arrDftVal)
                        {
                            //DefaultValue -5
                            rowData[5] += strDftVal + ",";
                        }
                        rowData[5] = rowData[5].Remove(rowData[5].Length - 1);
                    }
                    else
                    {
                        rowData[5] = rowData[2];
                    }

                    //CategoryName-6
                    rowData[6] = row[dataTable.Columns["RoleName"]].ToString();

                    //FieldType- 7
                    rowData[7] = row[dataTable.Columns["PropertyType"]].ToString();

                    //RegularExpress-8
                    rowData[8] = "";

                    //Description-9
                    rowData[9] = row[dataTable.Columns["PropertyDesc"]].ToString();
                    rowData[10] = "";
                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }
                dataSet.Tables.Add(customedTable);

                strGetUserRoleSQL = string.Format("select RoleName from tRole2User where UserGuid = '{0}'", userGuid.ToString());
                DataTable dtUserRole = oKodalDAL.ExecuteQuery(strGetUserRoleSQL);
                dataSet.Tables.Add(dtUserRole);


            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oKodalDAL != null)
                {
                    oKodalDAL.Dispose();
                }
            }

            return dataSet;
        }


        public override int AddUser(UserModel model,string strSite)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string[] arrRoleName = null;
                List<string> listSQL = new List<string>();
                StringBuilder strBuilder = new StringBuilder();
                if (IsUserNameAlreadyExists(model.LoginName, model.UserGuid, model.LoginNameChanged, oKodakDAL))
                {
                    return 1;
                }
                else if (IsLocalNameAlreadyExists(model.LocalName, model.UserGuid, model.DisplayNameChanged, oKodakDAL))
                {
                    return 3;
                }
                else if (IsDomainLoginNameAreadyExists(model.DomainLoginName, model.UserGuid, model.DomainLoginNameChanged, oKodakDAL))
                {
                    return 2;
                }
                else
                {
                    Guid UserGuid = Guid.NewGuid();
                    //prepare the SQL for inserting the user details in the tUser table
                    strBuilder.AppendFormat("insert into tUser values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                        UserGuid.ToString(),
                        model.LoginName,
                        model.LocalName,
                        model.EnglishName,
                        model.Password,
                        model.Department,
                        model.Title,
                        model.Address,
                        model.Telephone,
                        model.Comments,
                        0,
                        model.DomainLoginName);
                    listSQL.Add(strBuilder.ToString());
                    strBuilder.Remove(0, strBuilder.Length);

                    //prepare the SQL for inserting the user and role mapping in the tRole2User

                    arrRoleName = model.RoleName.Split(',');
                    if (arrRoleName != null && arrRoleName.Length > 0)
                    {
                        foreach (string strRoleName in arrRoleName)
                        {
                            strBuilder.AppendFormat("insert into tRole2User values ('{0}','{1}')", strRoleName.ToString().Trim(), UserGuid.ToString());
                            listSQL.Add(strBuilder.ToString());
                            strBuilder.Remove(0, strBuilder.Length);
                        }
                    }

                    //Prepare the SQL for inserting the user profile details in the tUserProfile table
                    string SQLRoleProfile = string.Format("select * from tRoleProfile where inheritance > 0 and RoleName in ('{0}')", model.RoleName.ToString());
                    DataTable dtRoleProfileDetails = oKodakDAL.ExecuteQuery(SQLRoleProfile);
                    if (dtRoleProfileDetails != null && dtRoleProfileDetails.Rows.Count > 0)
                    {
                        string strPropertyOption = null;
                        int iInheritance = -1;
                        foreach (DataRow drRoleProfileDetails in dtRoleProfileDetails.Rows)
                        {
                            strPropertyOption = Convert.ToString(drRoleProfileDetails["PropertyOptions"]);
                            if (!strPropertyOption.Contains("|"))
                            {
                                strPropertyOption = strPropertyOption.Replace("'", "''");
                            }

                            iInheritance = Convert.ToInt32(drRoleProfileDetails["Inheritance"]) - 1;

                            strBuilder.AppendFormat("INSERT INTO tUserProfile(Name,ModuleID,RoleName,UserGuid,[Value],Exportable,PropertyDesc,PropertyOptions,Inheritance,PropertyType,IsHidden,OrderingPos,[Domain]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                             Convert.ToString(drRoleProfileDetails["Name"]).Trim(),
                             Convert.ToString(drRoleProfileDetails["ModuleId"]).Trim(),
                             Convert.ToString(drRoleProfileDetails["RoleName"]).Trim(),
                             UserGuid.ToString(),
                             Convert.ToString(drRoleProfileDetails["Value"]),
                             Convert.ToInt32(drRoleProfileDetails["Exportable"]),
                             Convert.ToString(drRoleProfileDetails["PropertyDesc"]).Trim(),
                             strPropertyOption,
                             iInheritance,

                             Convert.ToInt16(drRoleProfileDetails["PropertyType"]),
                             Convert.ToInt16(drRoleProfileDetails["IsHidden"]),
                             Convert.ToInt32(drRoleProfileDetails["OrderingPos"]),
                             CommonGlobalSettings.Utilities.GetCurDomain());

                            listSQL.Add(strBuilder.ToString());
                            strBuilder.Remove(0, strBuilder.Length);

                        }

                    }

                    oKodakDAL.BeginTransaction();
                    foreach (string strSQL in listSQL)
                    {

                        logger.Debug((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, strSQL, Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                        (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                        oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                    }
                    oKodakDAL.CommitTransaction();
                }

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return -1;
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

        public override int ModifyUser(UserModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string[] arrRoleName = null;
                string[] arrAddNewRoleProfile = null;
                string[] arrDeletOldRoleProfile = null;
                List<string> listSQL = new List<string>();
                StringBuilder strBuilder = new StringBuilder();
                if (IsUserNameAlreadyExists(model.LoginName, model.UserGuid, model.LoginNameChanged, oKodakDAL))
                {
                    return 1;
                }
                else if (IsLocalNameAlreadyExists(model.LocalName, model.UserGuid, model.DisplayNameChanged, oKodakDAL))
                {
                    return 3;
                }
                else if (IsDomainLoginNameAreadyExists(model.DomainLoginName, model.UserGuid, model.DomainLoginNameChanged, oKodakDAL))
                {
                    return 2;
                }
                //prepare the SQL for updating the user details in the tUser table
                if (model.ChangedPasswordChecked == true)
                {
                    strBuilder.AppendFormat("Update tUser set " +
                        "LocalName = '{0}'," +
                        "EnglishName = '{1}'," +
                        "Password = '{2}'," +
                        "Department = '{3}'," +
                        "Title = '{4}'," +
                        "Address = '{5}'," +
                        "Telephone = '{6}'," +
                        "Comments = '{7}'," +
                        "DomainLoginName = '{8}'," +
                        "LoginName = '{9}'" +
                        "where UserGuid = '{10}'",
                        model.LocalName,
                        model.EnglishName,
                        model.Password,
                        model.Department,
                        model.Title,
                        model.Address,
                        model.Telephone,
                        model.Comments,
                        model.DomainLoginName,
                        model.LoginName,
                        model.UserGuid);
                    listSQL.Add(strBuilder.ToString());
                    strBuilder.Remove(0, strBuilder.Length);
                }
                else
                {
                    strBuilder.AppendFormat("Update tUser set " +
                        "LocalName = '{0}'," +
                        "EnglishName = '{1}'," +
                        "Department = '{2}'," +
                        "Title = '{3}'," +
                        "Address = '{4}'," +
                        "Telephone = '{5}'," +
                        "Comments = '{6}'," +
                        "DomainLoginName = '{7}'," +
                        "LoginName = '{8}'" +
                        "where UserGuid = '{9}'",
                        model.LocalName,
                        model.EnglishName,
                        model.Department,
                        model.Title,
                        model.Address,
                        model.Telephone,
                        model.Comments,
                        model.DomainLoginName,
                        model.LoginName,
                        model.UserGuid);
                    listSQL.Add(strBuilder.ToString());
                    strBuilder.Remove(0, strBuilder.Length);
                }

                //prepare the SQL for deleting and then inserting the user and role mapping in the tRole2User
                strBuilder.AppendFormat("Delete from tRole2User where UserGuid = '{0}'", model.UserGuid.ToString());
                listSQL.Add(strBuilder.ToString());
                strBuilder.Remove(0, strBuilder.Length);
                arrRoleName = model.RoleName.Split(',');
                if (arrRoleName != null && arrRoleName.Length > 0)
                {
                    foreach (string strRoleName in arrRoleName)
                    {
                        strBuilder.AppendFormat("insert into tRole2User values ('{0}','{1}')", strRoleName.ToString().Trim(), model.UserGuid.ToString());
                        listSQL.Add(strBuilder.ToString());
                        strBuilder.Remove(0, strBuilder.Length);
                    }
                }

                arrAddNewRoleProfile = model.AddNewRole.Split(',');
                if (arrAddNewRoleProfile != null && arrAddNewRoleProfile.Length > 0)
                {
                    foreach (string strAddNewRoleProfile in arrAddNewRoleProfile)
                    {
                        //Prepare the SQL for inserting the user profile details in the tUserProfile table
                        string SQLRoleProfile = string.Format("select * from tRoleProfile where inheritance > 0 and RoleName = ('{0}')", strAddNewRoleProfile.ToString());
                        DataTable dtRoleProfileDetails = oKodakDAL.ExecuteQuery(SQLRoleProfile);
                        if (dtRoleProfileDetails != null && dtRoleProfileDetails.Rows.Count > 0)
                        {
                            string strPropertyOption = null;
                            int iInheritance = -1;
                            foreach (DataRow drRoleProfileDetails in dtRoleProfileDetails.Rows)
                            {
                                strPropertyOption = Convert.ToString(drRoleProfileDetails["PropertyOptions"]);
                                if (!strPropertyOption.Contains("|"))
                                {
                                    strPropertyOption = strPropertyOption.Replace("'", "''");
                                }

                                iInheritance = Convert.ToInt32(drRoleProfileDetails["Inheritance"]) - 1;

                                strBuilder.AppendFormat("INSERT INTO tUserProfile(Name,ModuleID,RoleName,UserGuid,[Value],Exportable,PropertyDesc,PropertyOptions,Inheritance,PropertyType,IsHidden,OrderingPos,[Domain]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                                 Convert.ToString(drRoleProfileDetails["Name"]).Trim(),
                                 Convert.ToString(drRoleProfileDetails["ModuleId"]).Trim(),
                                 Convert.ToString(drRoleProfileDetails["RoleName"]).Trim(),
                                 model.UserGuid.ToString(),
                                 Convert.ToString(drRoleProfileDetails["Value"]),
                                 Convert.ToInt32(drRoleProfileDetails["Exportable"]),
                                 Convert.ToString(drRoleProfileDetails["PropertyDesc"]).Trim(),
                                 strPropertyOption,
                                 iInheritance,

                                 Convert.ToInt16(drRoleProfileDetails["PropertyType"]),
                                 Convert.ToInt16(drRoleProfileDetails["IsHidden"]),
                                 Convert.ToInt32(drRoleProfileDetails["OrderingPos"]),
                                 CommonGlobalSettings.Utilities.GetCurDomain());

                                listSQL.Add(strBuilder.ToString());
                                strBuilder.Remove(0, strBuilder.Length);

                            }

                        }

                        //strBuilder.AppendFormat("insert into tRole2User values ('{0}','{1}')", strRoleName.ToString().Trim(), model.UserGuid.ToString());
                        // listSQL.Add(strBuilder.ToString());
                        //strBuilder.Remove(0, strBuilder.Length);
                    }
                }
                arrDeletOldRoleProfile = model.DeleteOldRole.Split(',');
                if (arrDeletOldRoleProfile != null && arrDeletOldRoleProfile.Length > 0)
                {
                    foreach (string strDeletOldRoleProfile in arrDeletOldRoleProfile)
                    {
                        strBuilder.AppendFormat("DELETE FROM tUserProfile where RoleName = '{0}' and UserGuid = '{1}' and Domain='{2}'", strDeletOldRoleProfile.ToString().Trim(), model.UserGuid.ToString(),CommonGlobalSettings.Utilities.GetCurDomain());
                        listSQL.Add(strBuilder.ToString());
                        strBuilder.Remove(0, strBuilder.Length);
                    }
                }
                //Prepare the SQL for updating the user profile details in the tUserProfile table 
                if (model.SaveUserProfile.Tables != null)
                {
                    foreach (DataTable table in model.SaveUserProfile.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            strBuilder.AppendFormat("UPDATE tUserProfile set Value = '{0}' where Name = '{1}' and UserGuid = '{2}' and RoleName = '{3}'", row["FieldValue"].ToString(), row["FieldName"].ToString(), row["UserGuid"].ToString(), row["RoleName"].ToString());
                            listSQL.Add(strBuilder.ToString());
                            strBuilder.Remove(0, strBuilder.Length);
                        }

                    }
                }
                //prepares the SQL for deleting the record from the tSync table for the user Guid
                strBuilder.AppendFormat("Delete from tSync where Guid = '{0}' and SyncType = 12", model.UserGuid.ToString());
                listSQL.Add(strBuilder.ToString());
                strBuilder.Remove(0, strBuilder.Length);
                oKodakDAL.BeginTransaction();
                foreach (string strSQL in listSQL)
                {

                    logger.Debug((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, strSQL, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                    oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodakDAL.CommitTransaction();


            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return -1;
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

        public override int DeleteUser(string userGuid,string strDomain)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //Check if the user is deleted by another user
                if (IsUserDeleted(userGuid, oKodakDAL,strDomain))
                {
                    return 1;
                }
                //Check if the user is getting modified by another user
                if (IsUserExistsIntSync(userGuid, oKodakDAL))
                {
                    return 2;
                }
                List<string> listSQL = new List<string>();
                StringBuilder strBuilder = new StringBuilder();
                //Prepare the SQL to update the tUser table with DeleteMark = 1 when the user is deleted from UI
                strBuilder.AppendFormat("UPDATE tUser set DeleteMark = 1 where UserGuid = '{0}'", userGuid.ToString());
                listSQL.Add(strBuilder.ToString());
                strBuilder.Remove(0, strBuilder.Length);

                //Prepare the SQL to delete the user detail from tUserProfile table
                strBuilder.AppendFormat("DELETE FROM tUserProfile where UserGuid = '{0}'", userGuid.ToString());
                listSQL.Add(strBuilder.ToString());
                strBuilder.Remove(0, strBuilder.Length);

                //Prepare the SQL to delete the user detail from tRole2User table
                strBuilder.AppendFormat("DELETE FROM tRole2User where UserGuid = '{0}'", userGuid.ToString());
                listSQL.Add(strBuilder.ToString());
                strBuilder.Remove(0, strBuilder.Length);

                oKodakDAL.BeginTransaction();
                foreach (string strSQL in listSQL)
                {

                    logger.Debug((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, strSQL, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                   (long)(new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                    oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodakDAL.CommitTransaction();

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return -1;
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

        public override int CheckSyncronization(string userGuid,string strDomain)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //Check if the user is deleted by another user
                if (IsUserDeleted(userGuid, oKodakDAL,strDomain))
                {
                    return 1;
                }
                //Check if the user is getting modified by another user
                if (IsUserExistsIntSync(userGuid, oKodakDAL))
                {
                    return 2;

                }
                string OperateUserID = HttpContext.Current.Session["UserGuid"].ToString();
                string OperateIP = "";
                if (OperateUserID != null && OperateUserID != "")
                {
                    string sqlGetUserIP = string.Format("select MachineIP from tOnlineClient where UserGuid='{0}'", OperateUserID);

                    OperateIP = Convert.ToString(oKodakDAL.ExecuteScalar(sqlGetUserIP));
                }
                oKodakDAL.BeginTransaction();
                string strInsertSyncRecord = string.Format("Insert into tSync(SyncType,Guid,Owner,OwnerIP) values ('{0}','{1}','{2}','{3}')", 12, userGuid.ToString(), OperateUserID, OperateIP);
                oKodakDAL.ExecuteNonQuery(strInsertSyncRecord, RisDAL.ConnectionState.KeepOpen);
                oKodakDAL.CommitTransaction();

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return -1;
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

        public override bool DeleteSyncronization(string userGuid)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                oKodakDAL.BeginTransaction();
                string strDeleteSyncRecord = string.Format("Delete from tSync where SyncType = 12 and Guid = '{0}'", userGuid.ToString());
                oKodakDAL.ExecuteNonQuery(strDeleteSyncRecord, RisDAL.ConnectionState.KeepOpen);
                oKodakDAL.CommitTransaction();

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return false;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return true;

        }

        #endregion

        #region ISystemProfileDAO

        public override DataSet GetSystemProfileDataSet(string strDomain)
        {
            RisDAL oKodalDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            string strGetSysProfSQL = string.Empty;
            string strCurrentRowName = "";
            try
            {
                strGetSysProfSQL = string.Format("SELECT Name,tSystemProfile.ModuleID,Title as ModuleName,Value,Exportable,PropertyDesc,PropertyOptions,Inheritance,PropertyType,IsHidden,OrderingPos FROM tSystemProfile, tModule where tModule.ModuleID = tSystemProfile.ModuleID AND (bitand(tSystemProfile.IsHidden , 4) = 4) AND tSystemProfile.Inheritance >= 0 ORDER BY tSystemProfile.OrderingPos");
                DataTable dataTable = oKodalDAL.ExecuteQuery(strGetSysProfSQL);
                //Build the custom DataTable
                DataTable customedTable = Server.Utilities.Oam.Utilities.CreataCustomDataTable();

                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                string[] arrDftVal = null;
                foreach (DataRow row in dataTable.Rows)
                {
                    string[] rowData = new string[11];

                    //Module ID- 0
                    rowData[0] = row[dataTable.Columns["ModuleId"]].ToString();

                    //FieldName - 1
                    rowData[1] = row[dataTable.Columns["Name"]].ToString();
                    strCurrentRowName = row[dataTable.Columns["Name"]].ToString();
                    //FieldValue -2
                    rowData[2] = row[dataTable.Columns["Value"]].ToString();
                    //strValue = row[dataTable.Columns["Value"]].ToString();

                    //FieldDescription -3
                    rowData[3] = row[dataTable.Columns["Value"]].ToString();

                    //ShorCut -4 -- Property Options
                    strPropertyOption = row[dataTable.Columns["PropertyOptions"]].ToString();
                    if (!strPropertyOption.Contains("|"))
                    {
                        if (strPropertyOption.Contains("[") || strPropertyOption == string.Empty)
                        {
                            rowData[4] += strPropertyOption;
                        }
                        else if (strPropertyOption != string.Empty)
                        {
                            arrPropertyOptionSQL = strPropertyOption.Split(';');
                            DataTable dtPropOption = new DataTable();
                            foreach (string strProp in arrPropertyOptionSQL)
                            {

                                oKodalDAL.ExecuteQuery(strProp, dtPropOption);
                            }
                            foreach (DataRow drPropDetail in dtPropOption.Rows)
                            {
                                if ((row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.ListBox)
                                  || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckComboBox)
                                  || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckBox))
                                {
                                    rowData[4] += drPropDetail[0] + "|";
                                }
                                else if ((row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.ComboBox))
                                {
                                    rowData[4] += "|" + drPropDetail[0];
                                }
                            }
                            if ((row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.ListBox)
                             || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckComboBox)
                                || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckBox))
                            {
                                if (rowData[4] != null)
                                    rowData[4] = rowData[4].Remove(rowData[4].Length - 1);
                                else
                                    rowData[4] += "";
                            }
                        }

                    }
                    else
                    {
                        rowData[4] += strPropertyOption;
                    }

                    //DefaultValue -5
                    if (rowData[2].Contains("|"))
                    {
                        arrDftVal = rowData[2].Split('|');
                        foreach (string strDftVal in arrDftVal)
                        {
                            //DefaultValue -5
                            rowData[5] += strDftVal + ",";
                        }
                        rowData[5] = rowData[5].Remove(rowData[5].Length - 1);
                    }
                    else
                    {
                        rowData[5] = rowData[2];
                    }

                    //CategoryName-6
                    rowData[6] = row[dataTable.Columns["ModuleName"]].ToString();

                    //FieldType- 7
                    rowData[7] = row[dataTable.Columns["PropertyType"]].ToString();

                    //RegularExpress-8
                    rowData[8] = "";

                    //Description-9
                    rowData[9] = row[dataTable.Columns["PropertyDesc"]].ToString();
                    rowData[10] = "";
                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }
                dataSet.Tables.Add(customedTable);

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oKodalDAL != null)
                {
                    oKodalDAL.Dispose();
                }
            }

            return dataSet;
        }

        public override bool EditSystemProfile(SystemModel model, string strDomain)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                List<string> listSQL = new List<string>();
                StringBuilder strBuilder = new StringBuilder();
                foreach (DataTable table in model.SaveSystemProfile.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        strBuilder.AppendFormat("UPDATE tSystemProfile set Value = '{0}' where ModuleId = '{1}' and Name = '{2}' and Domain='{3}'", row["FieldValue"].ToString(), row["ModuleId"].ToString(), row["FieldName"].ToString(),CommonGlobalSettings.Utilities.GetCurDomain());
                        listSQL.Add(strBuilder.ToString());
                        strBuilder.Remove(0, strBuilder.Length);
                    }
                    oKodakDAL.BeginTransaction();
                    foreach (string strSQL in listSQL)
                    {
                        logger.Debug((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, strSQL, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                            (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                        oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                    }
                    oKodakDAL.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return false;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return true;

        }

        #endregion

        #region IClientConfigDAO

        public override DataSet GetClientConfigDataSet()
        {
            RisDAL oKodalDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            string strGetClientConfigSQL = string.Empty;
            try
            {
                strGetClientConfigSQL = string.Format("SELECT A.ConfigName,A.ModuleID ,B.Title as ModuleName,A.Value ,A.Exportable,A.PropertyDesc,A.PropertyOptions,A.Inheritance,A.PropertyType,A.IsHidden,A.OrderingPos,A.Domain,A.Type FROM tConfigDic A,tModule B WHERE A.ModuleID = B.ModuleID AND A.IsHidden = 0 AND A.Domain = 0 AND A.Type = 1 ORDER BY A.OrderingPos");
                DataTable dataTable = oKodalDAL.ExecuteQuery(strGetClientConfigSQL);
                //Build the custom DataTable
                DataTable customedTable = Server.Utilities.Oam.Utilities.CreateClientConfigDataTable();
                DataRow drNewRow = null;
                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                string[] arrDftVal = null;
                foreach (DataRow row in dataTable.Rows)
                {
                    drNewRow = customedTable.NewRow();
                    drNewRow["ConfigName"] = row["ConfigName"];
                    drNewRow["ModuleID"] = row["ModuleID"];
                    drNewRow["ModuleName"] = row["ModuleName"];

                    //Value
                    //if (row["Value"].ToString().Contains("|"))
                    //{
                    //    arrDftVal = row["Value"].ToString().Split('|');
                    //    foreach (string strDftVal in arrDftVal)
                    //    {
                    //        //Value
                    //        drNewRow["Value"] += strDftVal + ",";
                    //    }
                    //    drNewRow["Value"] = drNewRow["Value"].ToString().Remove(drNewRow["Value"].ToString().Length - 1);
                    //}
                    //else
                    //{
                    drNewRow["Value"] = row["Value"];
                    //}


                    drNewRow["Exportable"] = row["Exportable"];
                    drNewRow["PropertyDesc"] = row["PropertyDesc"];
                    drNewRow["PropertyType"] = row["PropertyType"];

                    strPropertyOption = row["PropertyOptions"].ToString();
                    if (!strPropertyOption.Contains("|"))
                    {
                        if (strPropertyOption.Contains("[") || strPropertyOption == string.Empty)
                        {
                            drNewRow["PropertyOptions"] += strPropertyOption;
                        }
                        else if (strPropertyOption != string.Empty)
                        {
                            arrPropertyOptionSQL = strPropertyOption.Split(';');
                            DataTable dtPropOption = new DataTable();
                            foreach (string strProp in arrPropertyOptionSQL)
                            {
                                oKodalDAL.ExecuteQuery(strProp, dtPropOption);
                            }
                            foreach (DataRow drPropDetail in dtPropOption.Rows)
                            {
                                if ((row["PropertyType"].ToString() == PropertyItemType.ListBox)
                                  || (row["PropertyType"].ToString() == PropertyItemType.CheckComboBox)
                                  || (row["PropertyType"].ToString() == PropertyItemType.CheckBox))
                                {
                                    drNewRow["PropertyOptions"] += drPropDetail[0] + "|";
                                }
                                else if ((row["PropertyType"].ToString() == PropertyItemType.ComboBox))
                                {
                                    drNewRow["PropertyOptions"] += "|" + drPropDetail[0];
                                }
                            }
                            if ((row["PropertyType"].ToString() == PropertyItemType.ListBox)
                             || (row["PropertyType"].ToString() == PropertyItemType.CheckComboBox)
                                || (row["PropertyType"].ToString() == PropertyItemType.CheckBox))
                            {
                                if (dtPropOption.Rows.Count > 0)
                                {
                                    drNewRow["PropertyOptions"] = drNewRow["PropertyOptions"].ToString().Remove(drNewRow["PropertyOptions"].ToString().Length - 1);
                                }
                                else
                                {
                                    if (row["PropertyType"].ToString() == PropertyItemType.CheckComboBox)
                                    {
                                        drNewRow["PropertyOptions"] = "";
                                    }
                                }

                            }
                        }
                        else
                        {
                            drNewRow["PropertyOptions"] += strPropertyOption;
                        }

                    }
                    else
                    {
                        drNewRow["PropertyOptions"] += strPropertyOption;
                    }
                    drNewRow["Inheritance"] = row["Inheritance"];

                    drNewRow["IsHidden"] = row["IsHidden"];
                    drNewRow["OrderingPos"] = row["OrderingPos"];
                    drNewRow["Domain"] = row["Domain"];
                    drNewRow["Type"] = row["Type"];
                    customedTable.Rows.Add(drNewRow);

                }
                dataSet.Tables.Add(customedTable);

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oKodalDAL != null)
                {
                    oKodalDAL.Dispose();
                }
            }

            return dataSet;
        }

        #endregion

        #region private method

        private bool IsRoleNameAreadyExists(string strRoleName, RisDAL oKodakDAL)
        {

            string strRoleExistVldSQL = string.Format("SELECT RoleName from tRole where RoleName= '{0}'", strRoleName.ToString());
            string strRoleExists = Convert.ToString(oKodakDAL.ExecuteScalar(strRoleExistVldSQL));
            if (strRoleExists != string.Empty)
            {
                return true;
            }
            return false;

        }

        private void InsertRoleAndProfileDetails(string strRoleName, string strCopyNewRoleName, string strRoleDescription, List<string> listSQL, bool IsCopy, RisDAL oKodakDAL, DataTable dtRoleProfileDetails)
        {
            string strRole = string.Empty;
            string strGetRoleProfSQL = string.Empty;
            //Copy the system profile to role profile. When add new role
            if (!IsCopy)
            {
                strGetRoleProfSQL = string.Format("SELECT Name ,tSystemProfile.ModuleID,Value,Exportable,PropertyDesc,PropertyOptions,Inheritance,PropertyType,IsHidden,OrderingPos FROM tSystemProfile, tModule where tModule.ModuleID = tSystemProfile.ModuleID and Inheritance > 0");
                strRole = strRoleName;
            }
            //Copy the existing role profile to a new role profile. When Copy As role is done
            else
            {
                strGetRoleProfSQL = string.Format("SELECT * from tRoleProfile where RoleName = '{0}'", strRoleName);
                strRole = strCopyNewRoleName;
            }
            dtRoleProfileDetails = new DataTable("RoleProfileDetails");
            oKodakDAL.ExecuteQuery(strGetRoleProfSQL, dtRoleProfileDetails);
            StringBuilder strInsertRoleAndProfSQL = new StringBuilder();
            if (!IsCopy)
            {
                strInsertRoleAndProfSQL.AppendFormat("INSERT INTO tRole VALUES ('{0}','{1}',0)", strRoleName, strRoleDescription);
            }
            //Copy the existing role profile to a new role profile. When Copy As role is done
            else
            {
                strInsertRoleAndProfSQL.AppendFormat("INSERT INTO tRole VALUES ('{0}','{1}',0)", strCopyNewRoleName, strRoleDescription);
            }
            listSQL.Add(strInsertRoleAndProfSQL.ToString());
            strInsertRoleAndProfSQL.Remove(0, strInsertRoleAndProfSQL.Length);
            string strPropertyOption = string.Empty;
            int iInheritance = -1;
            if (dtRoleProfileDetails != null && dtRoleProfileDetails.Rows.Count > 0)
            {
                foreach (DataRow drRoleProfileDetails in dtRoleProfileDetails.Rows)
                {
                    strPropertyOption = Convert.ToString(drRoleProfileDetails["PropertyOptions"]);
                    if (!strPropertyOption.Contains("|"))
                    {
                        strPropertyOption = strPropertyOption.Replace("'", "''");
                    }
                    if (!IsCopy)
                    {
                        iInheritance = Convert.ToInt32(drRoleProfileDetails["Inheritance"]) - 1;
                    }
                    //Copy the existing role profile to a new role profile. When Copy As role is done
                    else
                    {
                        iInheritance = Convert.ToInt32(drRoleProfileDetails["Inheritance"]);
                    }

                    strInsertRoleAndProfSQL.AppendFormat("INSERT INTO tRoleProfile(RoleName,Name,ModuleID,[Value],Exportable,PropertyDesc,PropertyOptions,Inheritance,PropertyType,IsHidden,OrderingPos,[Domain]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                    strRole.Trim(),
                    Convert.ToString(drRoleProfileDetails["Name"]).Trim(),
                    Convert.ToString(drRoleProfileDetails["ModuleId"]).Trim(),
                    Convert.ToString(drRoleProfileDetails["Value"]).Trim(),
                    Convert.ToInt32(drRoleProfileDetails["Exportable"]),
                    Convert.ToString(drRoleProfileDetails["PropertyDesc"]).Trim(),
                    strPropertyOption.Trim(),
                    iInheritance,
                    Convert.ToInt16(drRoleProfileDetails["PropertyType"]),
                    Convert.ToInt16(drRoleProfileDetails["IsHidden"]),
                    Convert.ToInt32(drRoleProfileDetails["OrderingPos"]),
                    CommonGlobalSettings.Utilities.GetCurDomain());

                    listSQL.Add(strInsertRoleAndProfSQL.ToString());
                    strInsertRoleAndProfSQL.Remove(0, strInsertRoleAndProfSQL.Length);

                }
            }
            oKodakDAL.BeginTransaction();
            foreach (string strSQL in listSQL)
            {
                logger.Debug((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, strSQL, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
            }
            oKodakDAL.CommitTransaction();
        }


        private List<string> GetUserRoleSet(string strUserName, RisDAL oKodakDAL)
        {
            string strUserRoleSetdSQL = string.Format("SELECT DISTINCT RoleName FROM tUser LoginName = {0} where Domain='{1}'", strUserName.ToString(), CommonGlobalSettings.Utilities.GetCurDomain());
            DataTable dtGetUserRoleSet = new DataTable("DataTableGetUserRoleSet");
            List<string> listUserRoleSet = new List<string>();
            oKodakDAL.ExecuteQuery(strUserRoleSetdSQL, dtGetUserRoleSet);
            if (dtGetUserRoleSet != null && dtGetUserRoleSet.Rows.Count > 0)
            {
                foreach (DataRow drGetUserRoleSet in dtGetUserRoleSet.Rows)
                {
                    listUserRoleSet.Add(drGetUserRoleSet["RoleName"].ToString());
                }
            }

            return listUserRoleSet;
        }

        private List<string> GetSystemRoleSet(RisDAL oKodakDAL)
        {
            string strSystemRoleSetdSQL = string.Format("SELECT DISTINCT RoleName FROM tRole where Domain='{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
            DataTable dtGetSystemRoleSet = new DataTable("DataTableGetSystemRoleSet");
            List<string> listSystemRoleSet = new List<string>();
            oKodakDAL.ExecuteQuery("strSystemRoleSetdSQL", dtGetSystemRoleSet);
            if (dtGetSystemRoleSet != null && dtGetSystemRoleSet.Rows.Count > 0)
            {
                foreach (DataRow drGetSystemRoleSet in dtGetSystemRoleSet.Rows)
                {
                    listSystemRoleSet.Add(drGetSystemRoleSet["RoleName"].ToString());
                }
            }

            return listSystemRoleSet;
        }


        private bool IsDeleteable(string strUserGuid, RisDAL oKodakDAL)
        {

            string strUserOnlineSQL = string.Format("SELECT RoleName from (select * from  tOnlineClient order by RoleName) where rownum <=1 and UserGuid= '{0}' and Domain='{1}' order by rownum asc", strUserGuid.ToString(), CommonGlobalSettings.Utilities.GetCurDomain());
            string strUserOnline = Convert.ToString(oKodakDAL.ExecuteScalar(strUserOnlineSQL));
            if (strUserOnline != string.Empty)
            {
                return false;
            }
            return true;
        }

        private bool IsUserDeleted(string strUserGuid, RisDAL oKodakDAL,string strDomain)
        {

            //string strUserDeletedSQL = string.Format("SELECT TOP 1 LoginName from tUser where UserGuid= '{0}' and DeleteMark = 1", strUserGuid.ToString());
            string strUserDeletedSQL = string.Format("SELECT LoginName from (select * from tUser order by LoginName) where ROWNUM <= 1 and UserGuid= '{0}' and DeleteMark = 1 order by rownum asc", strUserGuid.ToString());
            string strUserDeleted = Convert.ToString(oKodakDAL.ExecuteScalar(strUserDeletedSQL));
            if (strUserDeleted != "")
            {
                return true;
            }
            return false;
        }

        private bool IsUserExistsIntSync(string strUserGuid, RisDAL oKodakDAL)
        {
            //string strUserExistsIntSyncSQL = string.Format("SELECT TOP 1 Guid from tSync where Guid= '{0}' and SyncType = 12", strUserGuid.ToString());
            string strUserExistsIntSyncSQL = string.Format("SELECT Guid from (select * from tSync order by Guid) where rownum <=1 and  Guid= '{0}' and SyncType = 12 order by rownum asc", strUserGuid.ToString());
            string strUserExistsIntSync = Convert.ToString(oKodakDAL.ExecuteScalar(strUserExistsIntSyncSQL));
            if (strUserExistsIntSync != "")
            {
                return true;
            }
            return false;
        }


        private bool IsUserNameAlreadyExists(string strUserName, string userGuid, bool LoginNameChanged, RisDAL oKodakDAL)
        {

            string strUserExistVldSQL = string.Format("SELECT LoginName from tUser where LoginName= '{0}' and DeleteMark = 0", strUserName.ToString());
            string strUserExists = Convert.ToString(oKodakDAL.ExecuteScalar(strUserExistVldSQL));
            //modify
            if (userGuid != "")
            {
                if (LoginNameChanged == true)
                {
                    if (strUserExists != "")//
                    {
                        return true;
                    }
                }
            }
            //add
            else
            {
                if (strUserExists != "")//string.Empty
                {
                    return true;
                }
            }
            return false;

        }

        private bool IsLocalNameAlreadyExists(string strLocalName, string userGuid, bool LocalNameChanged, RisDAL oKodakDAL)
        {

            string strLocalNameExistVldSQL = string.Format("SELECT LoginName from tUser where LocalName= '{0}' and DeleteMark = 0", strLocalName.ToString());
            string strLocalNameExists = Convert.ToString(oKodakDAL.ExecuteScalar(strLocalNameExistVldSQL));
            if (userGuid != "")
            {
                if (LocalNameChanged == true)
                {
                    if (strLocalNameExists != "") //string.Empty
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (strLocalNameExists != "") //string.Empty
                {
                    return true;
                }
            }
            return false;

        }

        private bool IsDomainLoginNameAreadyExists(string strDomainLoginName, string userGuid, bool DomainLoginNameChanged, RisDAL oKodakDAL)
        {

            string strDmnLgnNameExistVldSQL = string.Format("SELECT LoginName from tUser where DomainLoginName= '{0}' and DeleteMark = 0 ", strDomainLoginName.ToString());
            string strDmnLgnNameExist = Convert.ToString(oKodakDAL.ExecuteScalar(strDmnLgnNameExistVldSQL));
            if (userGuid != "")
            {
                if (DomainLoginNameChanged == true)
                {
                    if (strDmnLgnNameExist != "") //string.Empty
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (strDmnLgnNameExist != "") //string.Empty
                {
                    return true;
                }
            }
            return false;

        }


        #endregion

        #region IResourceDAO Section
        private const string SQLGetResourceList = "Select Modality, ModalityType From tModality Order by Modality";

        public override ResourceModel QueryResource(string modalityName)
        {
            RisDAL dataAccess = new RisDAL();
            ResourceModel model = null;
            string sql = "Select * From tModality Where Modality='" + modalityName + "'";
            try
            {
                DataTable table = dataAccess.ExecuteQuery(sql);
                if (table.Rows.Count > 0)
                {
                    model = new ResourceModel();
                }

                foreach (DataRow row in table.Rows)
                {
                    //ModalityGuid
                    model.ModalityGuid = row[table.Columns["ModalityGuid"]] as string;
                    //ModalityType
                    model.ModalityType = row[table.Columns["ModalityType"]] as string;
                    //Modality
                    model.ModalityName = row[table.Columns["Modality"]] as string;
                    //Room
                    model.Room = row[table.Columns["Room"]] as string;

                    //IPAddress
                    model.IPAddress = row[table.Columns["IPAddress"]] as string;

                    //MaxLoad
                    //model.MaxLoad = ((int)row[table.Columns["MaxLoad"]]).ToString();
                    model.MaxLoad = Convert.ToString(Convert.ToInt16(row[table.Columns["MaxLoad"]]));


                    //Description
                    model.Description = row[table.Columns["Description"]] as string;
                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return model;
        }

        #endregion

        #region IDictionaryDAO Section

        private const string SQLGetDictionaryList = "Select * From tDictionary Where IsHidden=0";
        private const string SQLGetDictionaryItemList = "Select * From tDictionaryValue";
        private const string SQLDeleteDictionary = "Delete From tDictionary Where Tag=";
        private const string SQLDeleteDictionaryValue = "Delete From tDictionaryValue Where Tag=";
        private const string SQLAddDictionaryValue = "Insert into tDictionaryValue(Tag, Value, Text, IsDefault, ShortcutCode) ";

        public override DataSet GetDictionaryDataSet(string strSite)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet dataSet = new DataSet();
            string sql = SQLGetDictionaryList;
            try
            {
                DataTable dataTable = dataAccess.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);

                //Build the custom DataTabe
                DataTable customedTable = Server.Utilities.Oam.Utilities.CreataCustomDataTable();

                foreach (DataRow row in dataTable.Rows)
                {
                    string[] rowData = new string[11];

                    //ID-0
                    rowData[0] = Convert.ToString(Convert.ToInt16(row[dataTable.Columns["Tag"]]));

                    //FieldName-1
                    rowData[1] = row[dataTable.Columns["Name"]] as string;

                    //FieldValue-2
                    rowData[2] = "";

                    //FieldDescription-3
                    rowData[3] = "";

                    //ShortcutCode-4
                    rowData[4] = "";

                    //DefaultValue-5
                    rowData[5] = "";

                    rowData[10] = "";
                    int tag = Convert.ToInt16(row[dataTable.Columns["Tag"]].ToString());
                    string detailSql = SQLGetDictionaryItemList + " Where Tag=" + tag + " and Site = '" + strSite + "' order by OrderID ASC";
                    DataTable detailDataTable = dataAccess.ExecuteQuery(detailSql, RisDAL.ConnectionState.CloseOnExit);
                    if (detailDataTable.Rows.Count == 0)
                    {
                        detailSql = SQLGetDictionaryItemList + " Where Tag=" + tag + " and Site = '' order by OrderID ASC";
                        detailDataTable = dataAccess.ExecuteQuery(detailSql, RisDAL.ConnectionState.CloseOnExit);
                    }
                    foreach (DataRow subRow in detailDataTable.Rows)
                    {
                        if (rowData[2].Equals(""))
                        {
                            rowData[2] = subRow[detailDataTable.Columns["Value"]] as string;
                        }
                        else
                        {
                            rowData[2] += "|" + (subRow[detailDataTable.Columns["Value"]] as string);
                        }

                        if (rowData[3].Equals(""))
                        {
                            rowData[3] = subRow[detailDataTable.Columns["Description"]] as string;
                        }
                        else
                        {
                            rowData[3] += "|" + (subRow[detailDataTable.Columns["Description"]] as string);
                        }

                        if (rowData[4].Equals(""))
                        {
                            rowData[4] = subRow[detailDataTable.Columns["ShortcutCode"]].ToString();
                        }
                        else
                        {
                            rowData[4] += "|" + subRow[detailDataTable.Columns["ShortcutCode"]].ToString();
                        }

                        int isDefault = Convert.ToInt16(subRow[detailDataTable.Columns["IsDefault"]].ToString());
                        if (isDefault == 1)
                        {
                            //DefaultValue-3
                            rowData[5] = subRow[detailDataTable.Columns["Value"]] as string;
                            rowData[10] = subRow[detailDataTable.Columns["Text"]] as string;
                        }
                    }

                    //CategoryName-6
                    rowData[6] = "Dictionary";

                    //FieldType-7
                    rowData[7] = PropertyItemType.ListBox;

                    //RegularExpress-8
                    rowData[8] = "";

                    //Description-9
                    rowData[9] = "(" + tag.ToString() + ")" + row[dataTable.Columns["Text"]] as string;

                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }
                dataSet.Tables.Add(customedTable);
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return dataSet;
        }

        #endregion

        #region IScheduleDAO Section

        public override DataSet QueryWorkTimeList()
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("Select * From tWorkTime");
                DataSet dataSet = new DataSet();
                DataTable dataTable = dataAccess.ExecuteQuery(sql.ToString());
                dataTable.TableName = TableConst.WorkTimeTable;
                dataSet.Tables.Add(dataTable);
                return dataSet;
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return null;
        }

        public override int GetStepLength()
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("Select Value from tSystemProfile Where Name='ItemDuration' And ModuleID='0300' where Domain='{0}'",CommonGlobalSettings.Utilities.GetCurDomain());
                DataSet dataSet = new DataSet();
                return Convert.ToInt32(dataAccess.ExecuteScalar(sql.ToString()));
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return 5;
        }

        public override DataSet QueryScheduledModalityList(DateTime beginTime, DateTime endTime)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                DateTime beginTimeInQuery = Convert.ToDateTime(beginTime.ToLongDateString());
                DateTime endTimeInQuery = Convert.ToDateTime(endTime.ToLongDateString());

                string sql = "Select ModalityGuid From tModalityPlan Where StartDt >= To_Date('" + beginTimeInQuery.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                sql += " And StartDt < To_Date('" + endTimeInQuery.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                DataSet dataSet = new DataSet();
                DataTable dataTable = dataAccess.ExecuteQuery(sql.ToString(), RisDAL.ConnectionState.KeepOpen);
                List<string> modalityList = new List<string>();
                foreach (DataRow row in dataTable.Rows)
                {
                    modalityList.Add(row[dataTable.Columns[0]] as string);
                }

                DataTable returnDataTable = new DataTable();
                returnDataTable.TableName = TableConst.ModalityTable;

                DataColumn modalityColumn = new DataColumn();
                modalityColumn.ColumnName = TableConst.ModalityColumn;
                returnDataTable.Columns.Add(modalityColumn);

                DataColumn scheduledColumn = new DataColumn();
                scheduledColumn.ColumnName = "Scheduled";
                returnDataTable.Columns.Add(scheduledColumn);

                sql = "Select * From tModality";
                DataTable dt = dataAccess.ExecuteQuery(sql);
                foreach (DataRow row in dt.Rows)
                {
                    string[] rowItem = new string[2];
                    string modalityGuid = row[dt.Columns[TableConst.ModalityGuidColumn]] as string;
                    string modalityName = row[dt.Columns[TableConst.ModalityColumn]] as string;
                    rowItem[0] = modalityName;
                    if (modalityList.Contains(modalityGuid))
                    {
                        rowItem[1] = "Y";
                    }
                    else
                    {
                        rowItem[1] = "N";
                    }
                    returnDataTable.Rows.Add(rowItem);
                }

                dataSet.Tables.Add(returnDataTable);
                return dataSet;
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return null;
        }

        public override int AddWorkTime(WorkTimeModel model)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                if (IsWorkTimeOverlap(model))
                {
                    return 1;
                }

                string querySql = "Select * From tWorkTime Where WorkTimeName='" + model.WorkTimeName + "'";
                if (dataAccess.ExecuteQuery(querySql).Rows.Count > 0)
                {
                    return 3;
                }

                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("Insert into tWorkTime(WTGuid, WorkTimeName, StartDt, EndDt) Values ('{0}','{1}',To_Date('{2}','yyyy-mm-dd hh24:mi:ss'),To_Date('{3}','yyyy-mm-dd hh24:mi:ss'))",
                    Guid.NewGuid(), model.WorkTimeName, Convert.ToDateTime(model.BeginTime).ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToDateTime(model.EndTime).ToString("yyyy-MM-dd HH:mm:ss"));
                if (dataAccess.ExecuteNonQuery(sql.ToString()) > 0)
                {
                    return 0;
                }

            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return 2;
        }

        public override int ModifyWorkTime(WorkTimeModel model)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                if (IsWorkTimeOverlap(model))
                {
                    return 1;
                }
                string sqlExistSameName = string.Format("select WorkTimeName ,WTGuid from tWorkTime where WorkTimeName = '{0}' ", model.WorkTimeName);

                DataTable ExistSameNameTable = dataAccess.ExecuteQuery(sqlExistSameName);
                if (ExistSameNameTable.Rows.Count > 0)
                {
                    foreach (DataRow myRow in ExistSameNameTable.Rows)
                    {
                        if (myRow["WorkTimeName"].ToString() == model.WorkTimeName && myRow["WTGuid"].ToString() != model.Guid)
                        {
                            throw new Exception("WorkTime.WorkTimeNameExist");
                            return 0;
                        }

                    }
                }
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("Update tWorkTime Set StartDt=To_Date('{0}','yyyy-mm-dd hh24:mi:ss'),EndDt=To_Date('{1}','yyyy-mm-dd hh24:mi:ss'), WorkTimeName='{2}' Where WTGuid='{3}'",
                    Convert.ToDateTime(model.BeginTime).ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToDateTime(model.EndTime).ToString("yyyy-MM-dd HH:mm:ss"), model.WorkTimeName, model.Guid);
                if (dataAccess.ExecuteNonQuery(sql.ToString()) > 0)
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return 2;
        }

        public override bool DeleteWorkTime(WorkTimeModel model)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("Delete From tWorkTime Where WTGuid='{0}'", model.Guid);
                if (dataAccess.ExecuteNonQuery(sql.ToString()) >= 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return false;
        }

        public override DataSet QueryEmployeeList(string type)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                StringBuilder sql = new StringBuilder();
                if (type.Equals("Employee"))
                {
                    sql.Append("Select LoginName, LocalName, Department, UserGuid From tUser");
                }
                else if (type.Equals("Modality"))
                {
                    sql.Append("Select Modality From tModality");
                }
                else
                {
                    sql.Append("Select Distinct(TemplateName) From tEmployeePlan Where TemplateName != ''");
                }

                DataSet dataSet = new DataSet();
                DataTable dataTable = dataAccess.ExecuteQuery(sql.ToString());
                dataTable.TableName = TableConst.UserTable;
                dataSet.Tables.Add(dataTable);
                return dataSet;
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return null;
        }

        public override DataSet QueryScheduledEmployeeList(DateTime beginTime, DateTime endTime)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = "Select UserGuid From tEmployeePlan Where StartDt >= To_Date('" + beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                sql += " And StartDt < To_Date('" + endTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                DataSet dataSet = new DataSet();
                DataTable dataTable = dataAccess.ExecuteQuery(sql);
                dataTable.TableName = TableConst.EmployeePlanTable;
                dataSet.Tables.Add(dataTable);
                return dataSet;
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return null;
        }

        public override bool AddEmployeeSchedule(EmployeeScheduleModel model)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();

                //Delete the existed schedules
                string sql = "Delete From tEmployeePlan Where StartDt >= To_Date('" + model.BeginTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                sql += " And StartD < To_Date('" + model.EndTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                sql += " And UserGuid in(";
                for (int i = 0; i < model.Employees.Length - 1; i++)
                {
                    sql += "'" + model.Employees[i] + "',";
                }
                sql += "'" + model.Employees[model.Employees.Length - 1] + "')";
                dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);

                //Add the new schedules
                DateTime beginTime = Convert.ToDateTime(model.BeginTime.ToLongDateString());
                if (model.Period != 7)
                {
                    //Schedule according to the Period
                    int period = 0;
                    for (; beginTime <= Convert.ToDateTime(model.EndTime.ToLongDateString()); beginTime = beginTime.AddDays(1))
                    {
                        foreach (string userGuid in model.Employees)
                        {
                            foreach (WorkTimeModel workTimeModel in model.WorkTimeModels)
                            {
                                if (workTimeModel.PeriodMark[period])
                                {
                                    StringBuilder sb = new StringBuilder();
                                    DateTime beginTimeModel = Convert.ToDateTime(workTimeModel.BeginTime);
                                    DateTime beginTimeInDB = beginTime.AddHours(beginTimeModel.Hour).AddMinutes(beginTimeModel.Minute);
                                    DateTime endTimeModel = Convert.ToDateTime(workTimeModel.EndTime);
                                    DateTime endTimeInDB = beginTime.AddHours(endTimeModel.Hour).AddMinutes(endTimeModel.Minute);
                                    int templateMark = model.IsTemplate ? 1 : 0;

                                    sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateName, TemplateMark) Values('{0}','{1}',To_Date('{2}','yyyy-mm-dd hh24:mi:ss'),To_Date('{3}','yyyy-mm-dd hh24:mi:ss'),'{4}',{5})",
                                        Guid.NewGuid(), userGuid, beginTimeInDB.ToString("yyyy-MM-dd HH:mm:ss"), endTimeInDB.ToString("yyyy-MM-dd HH:mm:ss"), model.TemplateName, templateMark);
                                    dataAccess.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                                }
                            }
                        }
                        period = (++period % model.Period);
                    }
                }
                else
                {
                    //Schedule according to the Week 
                    for (; beginTime <= model.EndTime; beginTime = beginTime.AddDays(1))
                    {
                        foreach (string userGuid in model.Employees)
                        {
                            foreach (WorkTimeModel workTimeModel in model.WorkTimeModels)
                            {
                                if (workTimeModel.PeriodMark[Server.Utilities.Oam.Utilities.ConvertDayOfWeek(beginTime.DayOfWeek)])
                                {
                                    StringBuilder sb = new StringBuilder();
                                    DateTime beginTimeModel = Convert.ToDateTime(workTimeModel.BeginTime);
                                    DateTime beginTimeInDB = beginTime.AddHours(beginTimeModel.Hour).AddMinutes(beginTimeModel.Minute);
                                    DateTime endTimeModel = Convert.ToDateTime(workTimeModel.EndTime);
                                    DateTime endTimeInDB = beginTime.AddHours(endTimeModel.Hour).AddMinutes(endTimeModel.Minute);
                                    int templateMark = model.IsTemplate ? 1 : 0;

                                    sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateName, TemplateMark) Values('{0}','{1}',To_Date('{2}','yyyy-mm-dd hh24:mi:ss'),To_Date('{3}','yyyy-mm-dd hh24:mi:ss'),'{4}',{5})",
                                        Guid.NewGuid(), userGuid, beginTimeInDB.ToString("yyyy-MM-dd HH:mm:ss"), endTimeInDB.ToString("yyyy-MM-dd HH:mm:ss"), model.TemplateName, templateMark);
                                    dataAccess.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                                }
                            }
                        }
                    }
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception e)
            {
                dataAccess.RollbackTransaction();
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return true;
        }

        public override bool CopyEmployeeSchedule(CopyScheduleModel model)
        {
            List<string> listSQL = new List<string>();
            RisDAL dataAccess = new RisDAL();
            try
            {
                //Delete the existed schedules
                string sql = "Delete From tEmployeePlan Where StartDt >= To_Date('" + model.BeginTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                sql += " And StartDt < To_Date('" + model.EndTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                sql += " And UserGuid in(";
                for (int i = 0; i < model.Employees.Length - 1; i++)
                {
                    sql += "'" + model.Employees[i] + "',";
                }
                sql += "'" + model.Employees[model.Employees.Length - 1] + "')";
                listSQL.Add(sql);
                //dataAccess.ExecuteNonQuery(sql, KodakDAL.ConnectionState.KeepOpen);

                //Add the new schedules
                foreach (string userGuid in model.Employees)
                {
                    foreach (DataRow row in model.Schedules.Rows)
                    {
                        DateTime startDateTime = (DateTime)row[model.Schedules.Columns[TableConst.StartDateColumn]];
                        DateTime endDateTime = (DateTime)row[model.Schedules.Columns[TableConst.EndDateColumn]];
                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateMark) Values('{0}','{1}',To_Date('{2}','yyyy-mm-dd hh24:mi:ss'),'{3}','{4}')",
                            Guid.NewGuid(), userGuid, startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), endDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 0);
                        listSQL.Add(sb.ToString());
                        //dataAccess.ExecuteNonQuery(sb.ToString(), KodakDAL.ConnectionState.KeepOpen);
                    }
                }

                dataAccess.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    dataAccess.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                dataAccess.CommitTransaction();

            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                dataAccess.RollbackTransaction();
                throw new Exception(e.Message);
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return true;
        }

        public override bool ModifySchedule(CopyScheduleModel model)
        {
            List<string> listSQL = new List<string>();
            RisDAL dataAccess = new RisDAL();
            try
            {
                DateTime beginTime = Convert.ToDateTime(model.BeginTime.ToLongDateString());
                DateTime endTime = Convert.ToDateTime(model.EndTime.ToLongDateString());

                if (model.ScheduleType.Equals("Employee"))
                {
                    string queryUserGuid = "Select UserGuid from tUser where LoginName='" + model.Employees[0] + "'";
                    string userGuid = Convert.ToString(dataAccess.ExecuteScalar(queryUserGuid));

                    //Delete the existed schedules
                    string sql = "Delete From tEmployeePlan Where StartDt >= To_Date('" + beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                    sql += " And StartDt < To_Date('" + endTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                    sql += " And UserGuid='" + userGuid + "'";
                    listSQL.Add(sql);

                    //Add the new schedules
                    foreach (DataRow row in model.Schedules.Rows)
                    {
                        DateTime startDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.StartDateColumn]] as string);
                        DateTime endDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.EndDateColumn]] as string);
                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateMark) Values('{0}','{1}',To_Date('{2}','yyyy-mm-dd hh24:mi:ss'),To_Date('{3}','yyyy-mm-dd hh24:mi:ss'),'{4}')",
                            Guid.NewGuid(), userGuid, startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), endDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 0);
                        listSQL.Add(sb.ToString());
                    }
                }
                else if (model.ScheduleType.Equals("Template"))
                {
                    //Delete the existed schedules
                    string sql = "Delete From tEmployeePlan Where StartDt >= To_Date('" + beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                    sql += " And StartDt < To_Date('" + endTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                    sql += " And TemplateName='" + model.Employees[0] + "'";
                    listSQL.Add(sql);

                    //Add the new schedules
                    foreach (DataRow row in model.Schedules.Rows)
                    {
                        DateTime startDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.StartDateColumn]] as string);
                        DateTime endDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.EndDateColumn]] as string);
                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateName, TemplateMark) Values('{0}','{1}',To_Date('{2}','yyyy-mm-dd hh24:mi:ss'),To_Date('{3}','yyyy-mm-dd hh24:mi:ss'),'{4}',{5})",
                            Guid.NewGuid(), Guid.NewGuid(), startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), endDateTime.ToString("yyyy-MM-dd HH:mm:ss"), model.Employees[0], 1);
                        listSQL.Add(sb.ToString());
                    }
                }
                else
                {
                    string queryModalityGuid = "Select ModalityGuid from tModality where Modality='" + model.Modality + "'";
                    string modalityGuid = Convert.ToString(dataAccess.ExecuteScalar(queryModalityGuid));

                    //Delete the existed schedules
                    string sql = "Delete From tModalityPlan Where StartDt >= To_Date('" + beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                    sql += " And StartDt < To_Date('" + endTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                    sql += " And ModalityGuid='" + modalityGuid + "'";
                    listSQL.Add(sql);

                    //Add the new schedules
                    foreach (DataRow row in model.Schedules.Rows)
                    {
                        DateTime startDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.StartDateColumn]] as string);
                        DateTime endDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.EndDateColumn]] as string);
                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat("Insert into tModalityPlan(MPGuid, ModalityGuid, StartDt, EndDt) Values('{0}','{1}',To_Date('{2}','yyyy-mm-dd hh24:mi:ss'),To_Date('{3}','yyyy-mm-dd hh24:mi:ss'))",
                            Guid.NewGuid(), modalityGuid, startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), endDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        listSQL.Add(sb.ToString());
                    }
                }

                dataAccess.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    dataAccess.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                dataAccess.RollbackTransaction();
                throw new Exception(e.Message);
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return true;
        }

        public override DataSet QuerySchedule(DateTime beginTime, DateTime endTime, string name, bool isTemplate)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = "Select * From tEmployeePlan Where StartDt >= To_date('" + beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                sql += " And StartDt < to_date('" + endTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                if (isTemplate)
                {
                    sql += " And TemplateName='" + name + "'" + "order by StartDt asc";
                }
                else
                {
                    string queryUserGuid = "Select UserGuid from tUser where LoginName='" + name + "'";
                    string userGuid = Convert.ToString(dataAccess.ExecuteScalar(queryUserGuid));
                    sql += " And UserGuid='" + userGuid + "'" + "order by StartDt asc";
                }

                DataSet dataSet = new DataSet();
                DataTable dataTable = dataAccess.ExecuteQuery(sql);
                dataTable.TableName = TableConst.EmployeePlanTable;
                dataSet.Tables.Add(dataTable);
                return dataSet;
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return null;
        }

        public override DataSet QueryModalitySchedule(DateTime beginTime, DateTime endTime, string modality)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                DateTime beginTimeInQuery = Convert.ToDateTime(beginTime.ToLongDateString());
                DateTime endTimeInQuery = Convert.ToDateTime(endTime.ToLongDateString());

                string sql = "Select * From tModalityPlan Where StartDt >= To_Date('" + beginTimeInQuery.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                sql += " And StartDt < To_Date('" + endTimeInQuery.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";

                string queryModalityGuid = "Select ModalityGuid from tModality where Modality='" + modality + "'";
                string modalityGuid = Convert.ToString(dataAccess.ExecuteScalar(queryModalityGuid));
                sql += " And ModalityGuid='" + modalityGuid + "'" + "order by StartDt asc";

                DataSet dataSet = new DataSet();
                DataTable dataTable = dataAccess.ExecuteQuery(sql);
                dataTable.TableName = TableConst.ModalityPlanTable;
                dataSet.Tables.Add(dataTable);
                return dataSet;
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return null;
        }

        public override bool DeleteSchedule(DateTime beginTime, DateTime endTime, string name, bool isTemplate, string type)
        {
            RisDAL dataAccess = new RisDAL();

            try
            {
                StringBuilder sql = new StringBuilder();
                if (type.Equals("Employee"))
                {
                    string temp = "";
                    if (isTemplate)
                    {
                        temp = " And TemplateName='" + name + "'";
                    }
                    else
                    {
                        string queryUserGuid = "Select UserGuid from tUser where LoginName='" + name + "'";
                        string userGuid = Convert.ToString(dataAccess.ExecuteScalar(queryUserGuid));
                        temp = " And UserGuid='" + userGuid + "'";
                    }
                    sql.AppendFormat("Delete From tEmployeePlan Where StartDt >=To_Date('{0}','yyyy-mm-dd hh24:mi:ss') And StartDt < To_Date('{1}','yyyy-mm-dd hh24:mi:ss')" + temp, beginTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    sql.AppendFormat("Delete From tModalityPlan Where StartDt >=To_Date('{0}','yyyy-mm-dd hh24:mi:ss') And StartDt< To_Date('{1}','yyyy-mm-dd hh24:mi:ss')", beginTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (dataAccess.ExecuteNonQuery(sql.ToString()) > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return false;
        }

        public override DataSet QueryAvailableEmployeeList(ModalityScheduleModel model)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                DataSet dataSet = new DataSet();

                //Query Radiologist
                string sql = "Select UserGuid From tRole2User Where RoleName='Radiologist'";
                DataTable dataTable = dataAccess.ExecuteQuery(sql);
                List<string> matchedEmployeeList = GetMatchedEmployees(model, dataTable);
                DataTable radiologistTable = new DataTable();
                radiologistTable.TableName = TableConst.RadiologistTable;

                DataColumn usereNameColumn = new DataColumn();
                usereNameColumn.ColumnName = TableConst.UserNameColumn;
                radiologistTable.Columns.Add(usereNameColumn);

                DataColumn userGuidColumn = new DataColumn();
                userGuidColumn.ColumnName = TableConst.UserGuidColumn;
                radiologistTable.Columns.Add(userGuidColumn);

                foreach (string userGuid in matchedEmployeeList)
                {
                    string[] rowItem = new string[2];
                    rowItem[0] = GetUserLoginNameByUserGuid(userGuid);
                    rowItem[1] = userGuid;
                    radiologistTable.Rows.Add(rowItem);
                }
                dataSet.Tables.Add(radiologistTable);

                //Query Technician
                sql = "Select UserGuid From tRole2User Where RoleName='Techinician'";
                dataTable = dataAccess.ExecuteQuery(sql);
                DataTable TechnicianTable = new DataTable();
                TechnicianTable.TableName = TableConst.TechnicianTable;

                DataColumn userNameColumn1 = new DataColumn();
                userNameColumn1.ColumnName = TableConst.UserNameColumn;
                TechnicianTable.Columns.Add(userNameColumn1);

                DataColumn userGuidColumn1 = new DataColumn();
                userGuidColumn1.ColumnName = TableConst.UserGuidColumn;
                TechnicianTable.Columns.Add(userGuidColumn1);

                matchedEmployeeList = GetMatchedEmployees(model, dataTable);
                foreach (string userGuid in matchedEmployeeList)
                {
                    string[] rowItem = new string[2];
                    rowItem[0] = GetUserLoginNameByUserGuid(userGuid);
                    rowItem[1] = userGuid;
                    TechnicianTable.Rows.Add(rowItem);
                }
                dataSet.Tables.Add(TechnicianTable);

                //Query Nurse
                sql = "Select UserGuid From tRole2User Where RoleName='Nurse'";
                dataTable = dataAccess.ExecuteQuery(sql);
                DataTable NurseTable = new DataTable();
                NurseTable.TableName = TableConst.NurseTable;

                DataColumn userNameColumn2 = new DataColumn();
                userNameColumn2.ColumnName = TableConst.UserNameColumn;
                NurseTable.Columns.Add(userNameColumn2);

                DataColumn userGuidColumn2 = new DataColumn();
                userGuidColumn2.ColumnName = TableConst.UserGuidColumn;
                NurseTable.Columns.Add(userGuidColumn2);

                matchedEmployeeList = GetMatchedEmployees(model, dataTable);
                foreach (string userGuid in matchedEmployeeList)
                {
                    string[] rowItem = new string[2];
                    rowItem[0] = GetUserLoginNameByUserGuid(userGuid);
                    rowItem[1] = userGuid;
                    NurseTable.Rows.Add(rowItem);
                }
                dataSet.Tables.Add(NurseTable);

                return dataSet;
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return null;
        }

        public override bool AddModalitySchedule(ModalityScheduleModel model)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                DateTime beginTime = Convert.ToDateTime(model.BeginTime.ToLongDateString());
                DateTime endTime = Convert.ToDateTime(model.EndTime.ToLongDateString());
                string modalityGuid = GetModalityGuidByName(model.Modality);

                dataAccess.BeginTransaction();

                //Delete the existed schedules
                string sql = "Delete From tModalityPlan Where StartDt >= To_Date('" + beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                sql += " And StartDt < To_Date('" + endTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                sql += " And ModalityGuid in(Select ModalityGuid From tModality Where Modality='" + model.Modality + "')";
                dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);

                //Add the new schedules
                if (model.Period != 7)
                {
                    //Schedule according to the Period
                    int period = 0;
                    for (; beginTime < endTime.AddDays(1); beginTime = beginTime.AddDays(1))
                    {
                        foreach (WorkTimeModel workTimeModel in model.WorkTimeModels)
                        {
                            if (workTimeModel.PeriodMark[period])
                            {
                                StringBuilder sb = new StringBuilder();
                                DateTime beginTimeModel = Convert.ToDateTime(workTimeModel.BeginTime);
                                DateTime beginTimeInDB = beginTime.AddHours(beginTimeModel.Hour).AddMinutes(beginTimeModel.Minute);
                                DateTime endTimeModel = Convert.ToDateTime(workTimeModel.EndTime);
                                DateTime endTimeInDB = beginTime.AddHours(endTimeModel.Hour).AddMinutes(endTimeModel.Minute);
                                if (beginTimeInDB < DateTime.Now)
                                {
                                    throw new Exception("lessThanCurrentTime");
                                }
                                sb.AppendFormat("Insert into tModalityPlan (MPGuid, ModalityGuid, StartDt, EndDt, DoctorGuid, TechnicianGuid, NurseGuid) Values ('{0}', '{1}', To_Date('{2}','yyyy-mm-dd hh24:mi:ss'), To_Date('{3}','yyyy-mm-dd hh24:mi:ss'), '{4}', '{5}', '{6}')",
                                    Guid.NewGuid(), modalityGuid, beginTimeInDB.ToString("yyyy-MM-dd HH:mm:ss"), endTimeInDB.ToString("yyyy-MM-dd HH:mm:ss"), model.RadiologistGuid, model.TechnicianGuid, model.NurseGuid);
                                dataAccess.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                            }
                        }

                        period = (++period % model.Period);
                    }
                }
                else
                {
                    //Schedule according to the Week 
                    for (; beginTime < endTime.AddDays(1); beginTime = beginTime.AddDays(1))
                    {
                        foreach (WorkTimeModel workTimeModel in model.WorkTimeModels)
                        {
                            if (workTimeModel.PeriodMark[Server.Utilities.Oam.Utilities.ConvertDayOfWeek(beginTime.DayOfWeek)])
                            {
                                StringBuilder sb = new StringBuilder();
                                DateTime beginTimeModel = Convert.ToDateTime(workTimeModel.BeginTime);
                                DateTime beginTimeInDB = beginTime.AddHours(beginTimeModel.Hour).AddMinutes(beginTimeModel.Minute);
                                DateTime endTimeModel = Convert.ToDateTime(workTimeModel.EndTime);
                                DateTime endTimeInDB = beginTime.AddHours(endTimeModel.Hour).AddMinutes(endTimeModel.Minute);
                                if (beginTimeInDB < DateTime.Now)
                                {
                                    throw new Exception("lessThanCurrentTime");
                                }
                                sb.AppendFormat("Insert into tModalityPlan(MPGuid, ModalityGuid, StartDt, EndDt, DoctorGuid, TechnicianGuid, NurseGuid) Values('{0}', '{1}', To_Date('{2}','yyyy-mm-dd hh24:mi:ss'), To_Date('{3}','yyyy-mm-dd hh24:mi:ss'), '{4}', '{5}', '{6}')",
                                    Guid.NewGuid(), modalityGuid, beginTimeInDB.ToString("yyyy-MM-dd HH:mm:ss"), endTimeInDB.ToString("yyyy-MM-dd HH:mm:ss"), model.RadiologistGuid, model.TechnicianGuid, model.NurseGuid);
                                dataAccess.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                            }
                        }
                    }
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception e)
            {
                dataAccess.RollbackTransaction();
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return true;
        }

        private List<string> GetMatchedEmployees(ModalityScheduleModel model, DataTable employeeTable)
        {
            DateTime beginTime = Convert.ToDateTime(model.BeginTime.ToLongDateString());
            DateTime endTime = Convert.ToDateTime(model.EndTime.ToLongDateString());
            List<string> employeeGuidList = new List<string>();

            RisDAL dataAccess = new RisDAL();
            try
            {
                foreach (DataRow row in employeeTable.Rows)
                {
                    string userGuid = row[employeeTable.Columns[TableConst.UserGuidColumn]] as string;
                    string sql = "Select * From tEmployeePlan Where StartDt >= To_Date('" + beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                    sql += " StartDt < To_Date('" + endTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
                    sql += " And UserGuid='" + userGuid + "'" + "order by StartDt asc";
                    DataTable workTimeDataTable = dataAccess.ExecuteQuery(sql);
                    if (IsMatchedEmployeeItem(model, workTimeDataTable))
                    {
                        employeeGuidList.Add(userGuid);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            return employeeGuidList;
        }

        private bool IsMatchedEmployeeItem(ModalityScheduleModel model, DataTable workTimeDataTable)
        {
            DateTime beginTime = Convert.ToDateTime(model.BeginTime.ToLongDateString());
            DateTime endTime = Convert.ToDateTime(model.EndTime.ToLongDateString());

            for (; beginTime <= model.EndTime; beginTime = beginTime.AddDays(1))
            {
                foreach (WorkTimeModel workTimeModel in model.WorkTimeModels)
                {
                    DateTime beginWorkTimeTemp = Convert.ToDateTime(workTimeModel.BeginTime);
                    DateTime endWorkTimeTemp = Convert.ToDateTime(workTimeModel.EndTime);
                    DateTime beginWorkTime = beginTime.AddHours(beginWorkTimeTemp.Hour).AddMinutes(beginWorkTimeTemp.Minute);
                    DateTime endWorkTime = beginTime.AddHours(endWorkTimeTemp.Hour).AddMinutes(endWorkTimeTemp.Minute);
                    DateTime beginTimeSource = beginTime.AddHours(beginWorkTime.Hour).AddMinutes(beginWorkTime.Minute);
                    DateTime endTimeSource = endTime.AddHours(endWorkTime.Hour).AddMinutes(endWorkTime.Minute);
                    bool isMatched = false;
                    foreach (DataRow row in workTimeDataTable.Rows)
                    {
                        DateTime beginTimCompare = (DateTime)row[workTimeDataTable.Columns[TableConst.StartDateColumn]];
                        DateTime endTimCompare = (DateTime)row[workTimeDataTable.Columns[TableConst.EndDateColumn]];
                        if (IsWorkTimeMatched(beginTimeSource, endTimeSource, beginTimCompare, endTimCompare))
                        {
                            isMatched = true;
                            break;
                        }
                    }
                    if (!isMatched)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private string GetUserGuidByUserLoginName(string loginName)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = "Select UserGuid from tUser where LoginName='" + loginName + "'";
                return Convert.ToString(dataAccess.ExecuteScalar(sql));
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
                return null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return null;
        }

        private string GetUserLoginNameByUserGuid(string userGuid)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = "Select LoginName from tUser where UserGuid='" + userGuid + "'";
                return Convert.ToString(dataAccess.ExecuteScalar(sql));
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
                return null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return null;
        }

        private string GetModalityGuidByName(string modality)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = "Select ModalityGuid from tModality where Modality='" + modality + "'";
                return Convert.ToString(dataAccess.ExecuteScalar(sql));
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
                return null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return null;
        }

        private bool IsWorkTimeMatched(DateTime beginTimeSource, DateTime endTimeSource, DateTime beginTimeCompare, DateTime endTimeCompare)
        {
            if (beginTimeSource >= beginTimeCompare && endTimeSource <= endTimeCompare)
            {
                return true;
            }

            return false;
        }

        private bool IsWorkTimeOverlap(WorkTimeModel model)
        {
            return false;
        }

        /*
           public override bool ModifySchedule(CopyScheduleModel model)
           {
               List<string> listSQL = new List<string>();
               KodakDAL dataAccess = new KodakDAL();
               try
               {
                   DateTime beginTime = Convert.ToDateTime(model.BeginTime.ToLongDateString());
                   DateTime endTime = Convert.ToDateTime(model.EndTime.ToLongDateString());

                   if (model.ScheduleType.Equals("Employee"))
                   {
                       string queryUserGuid = "Select UserGuid from tUser where LoginName='" + model.Employees[0] + "'";
                       string userGuid = Convert.ToString(dataAccess.ExecuteScalar(queryUserGuid));

                       //Delete the existed schedules
                       string sql = "Delete From tEmployeePlan Where StartDt >= To_Date('" + beginTime + "','yyyy-mm-dd hh24:mi:ss')";
                       sql += " And StartDt < To_Date('" + endTime.AddDays(1) + "','yyyy-mm-dd hh24:mi:ss')";
                       sql += " And UserGuid='" + userGuid + "'";
                       listSQL.Add(sql);

                       //Add the new schedules
                       foreach (DataRow row in model.Schedules.Rows)
                       {
                           DateTime startDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.StartDateColumn]] as string);
                           DateTime endDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.EndDateColumn]] as string);
                           StringBuilder sb = new StringBuilder();

                           sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateMark) Values('{0}','{1}',To_Date('{2}','yyyy-mm-dd hh24:mi:ss'),To_Date('{3}','yyyy-mm-dd hh24:mi:ss'),'{4}')",
                               Guid.NewGuid(), userGuid, startDateTime, endDateTime, 0);
                           listSQL.Add(sb.ToString());
                       }
                   }
                   else if (model.ScheduleType.Equals("Template"))
                   {
                       //Delete the existed schedules
                       string sql = "Delete From tEmployeePlan Where StartDt >= To_Date('" + beginTime + "','yyyy-mm-dd hh24:mi:ss')";
                       sql += " And StartDt < To_Date('" + endTime.AddDays(1) + "','yyyy-mm-dd hh24:mi:ss')";
                       sql += " And TemplateName='" + model.Employees[0] + "'";
                       listSQL.Add(sql);

                       //Add the new schedules
                       foreach (DataRow row in model.Schedules.Rows)
                       {
                           DateTime startDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.StartDateColumn]] as string);
                           DateTime endDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.EndDateColumn]] as string);
                           StringBuilder sb = new StringBuilder();

                           sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateName, TemplateMark) Values('{0}','{1}',To_Date('{2}','yyyy-mm-dd hh24:mi:ss'),To_Date('{3}','yyyy-mm-dd hh24:mi:ss'),'{4}',{5})",
                               Guid.NewGuid(), Guid.NewGuid(), startDateTime, endDateTime, model.Employees[0], 1);
                           listSQL.Add(sb.ToString());
                       }
                   }
                   else
                   {
                       string queryModalityGuid = "Select ModalityGuid from tModality where Modality='" + model.Modality + "'";
                       string modalityGuid = Convert.ToString(dataAccess.ExecuteScalar(queryModalityGuid));

                       //Delete the existed schedules
                       string sql = "Delete From tModalityPlan Where StartDt >= To_Date('" + beginTime + "','yyyy-mm-dd hh24:mi:ss')";
                       sql += " And StartDt < To_Date('" + endTime.AddDays(1) + "','yyyy-mm-dd hh24:mi:ss')";
                       sql += " And ModalityGuid='" + modalityGuid + "'";
                       listSQL.Add(sql);

                       //Add the new schedules
                       foreach (DataRow row in model.Schedules.Rows)
                       {
                           DateTime startDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.StartDateColumn]] as string);
                           DateTime endDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.EndDateColumn]] as string);
                           StringBuilder sb = new StringBuilder();

                           sb.AppendFormat("Insert into tModalityPlan(MPGuid, ModalityGuid, StartDt, EndDt) Values('{0}','{1}',To_Date('{2}','yyyy-mm-dd hh24:mi:ss'),To_Date('{3}','yyyy-mm-dd hh24:mi:ss'))",
                               Guid.NewGuid(), modalityGuid, startDateTime, endDateTime);
                           listSQL.Add(sb.ToString());
                       }
                   }

                   dataAccess.BeginTransaction();
                   foreach (string strSQL in listSQL)
                   {
                       dataAccess.ExecuteNonQuery(strSQL, KodakDAL.ConnectionState.KeepOpen);
                   }
                   dataAccess.CommitTransaction();
               }
               catch (Exception e)
               {
                   logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                       (new System.Diagnostics.StackFrame(true)).GetFileName(),
                       (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                   dataAccess.RollbackTransaction();
                   throw new Exception(e.Message);
                   return false;
               }
               finally
               {
                   if (dataAccess != null)
                   {
                       dataAccess.Dispose();
                   }
               }
               return true;
           }

           public override bool DeleteSchedule(DateTime beginTime, DateTime endTime, string name, bool isTemplate, string type)
           {
               KodakDAL dataAccess = new KodakDAL();

               try
               {
                   StringBuilder sql = new StringBuilder();
                   if (type.Equals("Employee"))
                   {
                       string temp = "";
                       if (isTemplate)
                       {
                           temp = " And TemplateName='" + name + "'";
                       }
                       else
                       {
                           string queryUserGuid = "Select UserGuid from tUser where LoginName='" + name + "'";
                           string userGuid = Convert.ToString(dataAccess.ExecuteScalar(queryUserGuid));
                           temp = " And UserGuid='" + userGuid + "'";
                       }
                       sql.AppendFormat("Delete From tEmployeePlan Where StartDt >= To_Date('{0}','yyyy-mm-dd hh24:mi:ss') And StartDt < To_date('{1}','yyyy-mm-dd hh24:mi:ss')" + temp, beginTime, endTime.AddDays(1));
                   }
                   else
                   {
                       sql.AppendFormat("Delete From tModalityPlan Where StartDt >= To_Date('{0}','yyyy-mm-dd hh24:mi:ss') And StartDt < To_Date('{1}','yyyy-mm-dd hh24:mi:ss')", beginTime, endTime.AddDays(1));
                   }
                   if (dataAccess.ExecuteNonQuery(sql.ToString()) > 0)
                   {
                       return true;
                   }
               }
               catch (Exception e)
               {
                   logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                       (new System.Diagnostics.StackFrame(true)).GetFileName(),
                       (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                   throw new Exception(e.Message);
               }
               finally
               {
                   if (dataAccess != null)
                   {
                       dataAccess.Dispose();
                   }
               }

               return false;
           }
   */

        #endregion

        #region Import&Export Section

        public override bool ImportPhraseTemplate(bool isClear, string strUserGuid, DataSet phraseTemplateDataSet,string site)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                if (isClear)
                {
                    dataAccess.ExecuteNonQuery("delete from tPhraseTemplate", RisDAL.ConnectionState.KeepOpen);
                }
                if (phraseTemplateDataSet.Tables.Count != 1)
                {
                    return false;
                }
                string sql;

                foreach (DataRow myRow in phraseTemplateDataSet.Tables["PhraseTemplate"].Rows)
                {
                    dataAccess.Parameters.Clear();
                    string strGuid = Guid.NewGuid().ToString();
                    string strModalityType = Convert.ToString(myRow["ModalityType"]);
                    string strTemplateName = Convert.ToString(myRow["TemplateName"]);
                    string strTemplateInfo = Convert.ToString(myRow["TemplateInfo"]);
                    string strShortcutCode = Convert.ToString(myRow["ShortcutCode"]);
                    int intType = Convert.ToInt32(myRow["Type"]);
                    string str_UserGuid = Convert.ToString(myRow["UserGuid"]);

                    sql = string.Format("insert into tPhraseTemplate(TemplateGuid,ModalityType,TemplateName,TemplateInfo,ShortcutCode,Type,UserGuid) values ('{0}','{1}','{2}','{3}','{4}',{5},'{6}')", strGuid, strModalityType, strTemplateName, strTemplateInfo, strShortcutCode, intType, str_UserGuid);
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);

                    //dataAccess.Parameters.AddChar("@TemplateGuid", strGuid);
                    //dataAccess.Parameters.AddChar("@ModalityType", Convert.ToString(myRow["ModalityType"]));
                    //dataAccess.Parameters.AddChar("@TemplateName", Convert.ToString(myRow["TemplateName"]));
                    //dataAccess.Parameters.AddChar("@TemplateInfo", Convert.ToString(myRow["TemplateInfo"]));
                    //dataAccess.Parameters.AddChar("@ShortcutCode", Convert.ToString(myRow["ShortcutCode"]));
                    //dataAccess.Parameters.AddInt("@Type", Convert.ToInt32(myRow["Type"]));
                    //dataAccess.Parameters.AddChar("@UserGuid", Convert.ToString(myRow["UserGuid"]));

                    //sql = "insert into tPhraseTemplate(TemplateGuid,ModalityType,TemplateName,TemplateInfo,ShortcutCode,Type,UserGuid) values (@TemplateGuid,@ModalityType,@TemplateName,@TemplateInfo,@ShortcutCode,@Type,@UserGuid)";
                    //dataAccess.ExecuteNonQuery(sql, KodakDAL.ConnectionState.KeepOpen);
                }


                dataAccess.CommitTransaction();
                return true;

            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dataAccess.RollbackTransaction();
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
        }

        public override DataSet GetAllPhraseTemplate(string strUserGuid,string Site)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            string sql;
            //if (strUserGuid == "Global")
            //    sql = "select * from tPhraseTemplate where Type = 0";
            //else
            //    sql = string.Format("select * from tPhraseTemplate where Type = 1 and UserGuid = '{0}'",strUserGuid);
            sql = "select * from tPhraseTemplate";
            try
            {
                DataTable PhraseTemplateTable;

                PhraseTemplateTable = dataAccess.ExecuteQuery(sql);
                PhraseTemplateTable.TableName = "PhraseTemplate";
                ds.Tables.Add(PhraseTemplateTable);
                ds.DataSetName = "PhraseTemplateDataSet";

            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                ds = null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }

        public override bool ImportPrintTemplate(bool isClear, int type, DataSet printTemplateDataSet,string site, ref string errorInfo)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                if (isClear)
                {
                    string strSql;
                    if (type == -1)
                    {
                        strSql = "delete from tPrintTemplate";
                    }
                    else
                    {
                        strSql = string.Format("delete from tPrintTemplate where Type = {0}", type);
                    }
                    dataAccess.ExecuteNonQuery(strSql, RisDAL.ConnectionState.KeepOpen);
                }
                if (printTemplateDataSet.Tables.Count != 1)
                {
                    return false;
                }
                string sql;

                Hashtable tempBlobVector = new Hashtable();

                foreach (DataRow myRow in printTemplateDataSet.Tables["PrintTemplate"].Rows)
                {


                    string strTemplateGuid = Guid.NewGuid().ToString();
                    string strModalityType = Convert.ToString(myRow["ModalityType"]);
                    string strTemplateName = Convert.ToString(myRow["TemplateName"]);

                    tempBlobVector.Add(strTemplateGuid, myRow["TemplateInfoStr"]);

                    int intVersion = Convert.ToInt32(myRow["Version"]);
                    int intIsDefaultByModality = Convert.ToInt32(myRow["IsDefaultByModality"]);
                    int intIsDefaultByType = Convert.ToInt32(myRow["IsDefaultByType"]);
                    int intType = Convert.ToInt32(myRow["Type"]);

                    //sql = string.Format("insert into tPrintTemplate(TemplateGuid,Type,TemplateName,TemplateInfo,IsDefaultByType,Version,ModalityType,IsDefaultByModality) values ('{0}',{1},'{2}','{3}',{4},{5},'{6}',{7})", strTemplateGuid, intType, strTemplateName, param.Value, intIsDefaultByType, intVersion, strModalityType, intIsDefaultByModality);

                    sql = string.Format("insert into tPrintTemplate(TemplateGuid,Type,TemplateName,TemplateInfo,IsDefaultByType,Version,ModalityType,IsDefaultByModality) values ('{0}',{1},'{2}','{3}',{4},{5},'{6}',{7})", strTemplateGuid, intType, strTemplateName, string.Empty, intIsDefaultByType, intVersion, strModalityType, intIsDefaultByModality);

                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);

                }

                dataAccess.CommitTransaction();

                //write blob datatype
                foreach (DictionaryEntry curblob in tempBlobVector)
                {
                    Byte[] tempByte = System.Text.Encoding.Unicode.GetBytes(Convert.ToString(curblob.Value));
                    dataAccess.WriteLargeObj("tPrintTemplate", "TemplateGuid", curblob.Key.ToString(), "TemplateInfo", tempByte, tempByte.Length, RisDAL.ConnectionState.KeepOpen);
                }

                return true;

            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dataAccess.RollbackTransaction();
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
        }

        public override DataSet GetAllPrintTemplate(int type,string site)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            string sql;
            if (type == -1)
                sql = string.Format("select * from tPrintTemplate where site = '{0}'", site);
            else
                sql = string.Format("select * from tPrintTemplate where Type = {0} and Site ='{1}'", type, site);
            try
            {
                DataTable PrintTemplateTable;
                PrintTemplateTable = dataAccess.ExecuteQuery(sql);
                foreach (DataRow myRow in PrintTemplateTable.Rows)
                {
                    myRow["Version"] = 0;
                }
                PrintTemplateTable.TableName = "PrintTemplate";
                ds.Tables.Add(PrintTemplateTable);
                ds.DataSetName = "PrintTemplateDataSet";
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                ds = null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }

        public override bool ImportReportTemplate(bool isClear, DataSet reportTemplateDataSet,string site)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                if (isClear)
                {
                    dataAccess.ExecuteNonQuery("delete from tReportTemplate", RisDAL.ConnectionState.KeepOpen);
                    dataAccess.ExecuteNonQuery("delete from tReportTemplateDirec", RisDAL.ConnectionState.KeepOpen);
                }
                if (reportTemplateDataSet.Tables.Count != 2)
                {
                    return false;
                }

                string sql;

                Hashtable tempBlobWYS = new Hashtable();
                Hashtable tempBlobWYG = new Hashtable();
                Hashtable tempBlobAppendInfo = new Hashtable();
                Hashtable tempBlobTechInfo = new Hashtable();

                foreach (DataRow myRow in reportTemplateDataSet.Tables["ReportTemplate"].Rows)
                {
                    string strTemplateGuid = Convert.ToString(myRow["TemplateGuid"]);
                    string strModalityType = Convert.ToString(myRow["ModalityType"]);
                    string strTemplateName = Convert.ToString(myRow["TemplateName"]);
                    string strBodyPart = Convert.ToString(myRow["BodyPart"]);
                    string strCheckItemName = Convert.ToString(myRow["CheckItemName"]);
                    string strDoctorAdvice = Convert.ToString(myRow["DoctorAdvice"]);
                    string strShortcutCode = Convert.ToString(myRow["ShortcutCode"]);
                    string strACRCode = Convert.ToString(myRow["ACRCode"]);
                    string strACRAnatomicDesc = Convert.ToString(myRow["ACRAnatomicDesc"]);
                    string strACRPathologicDesc = Convert.ToString(myRow["ACRPathologicDesc"]);
                    string strBodyCategory = Convert.ToString(myRow["BodyCategory"]);

                    tempBlobWYS.Add(strTemplateGuid, myRow["WYSStr"]);
                    tempBlobWYG.Add(strTemplateGuid, myRow["WYGStr"]);
                    tempBlobAppendInfo.Add(strTemplateGuid, myRow["AppendInfoStr"]);
                    tempBlobTechInfo.Add(strTemplateGuid, myRow["TechInfoStr"]);

                    sql = string.Format("insert into tReportTemplate(TemplateGuid,TemplateName,ModalityType,BodyPart,WYS,WYG,AppendInfo,TechInfo,CheckItemName ,DoctorAdvice,ShortcutCode,ACRCode,ACRAnatomicDesc,ACRPathologicDesc,BodyCategory) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", strTemplateGuid, strTemplateName, strModalityType, strBodyPart, string.Empty, string.Empty, string.Empty, string.Empty, strCheckItemName, strDoctorAdvice, strShortcutCode, strACRCode, strACRAnatomicDesc, strACRPathologicDesc, strBodyCategory);
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }

                foreach (DataRow myRow in reportTemplateDataSet.Tables["ReportTemplateDirec"].Rows)
                {
                    string strItemGuid = Convert.ToString(myRow["ItemGUID"]);
                    string strParentID = Convert.ToString(myRow["ParentID"]);
                    int intDepth = Convert.ToInt16(myRow["Depth"]);
                    string strItemName = Convert.ToString(myRow["ItemName"]);
                    int intItemOrder = Convert.ToInt16(myRow["ItemOrder"]);
                    int intType = Convert.ToInt16(myRow["Type"]);
                    string strUserGuid = Convert.ToString(myRow["UserGuid"]);
                    string strTemplateGuid = Convert.ToString(myRow["TemplateGuid"]);
                    int intLeaf = Convert.ToInt16(myRow["Leaf"]);

                    sql = string.Format("insert into tReportTemplateDirec(ItemGUID,ParentID,Depth,ItemName,ItemOrder,Type,UserGuid,TemplateGuid,Leaf) values('{0}','{1}',{2},'{3}',{4},{5},'{6}','{7}',{8})", strItemGuid, strParentID, intDepth, strItemName, intItemOrder, intType, strUserGuid, strTemplateGuid, intLeaf);
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }

                dataAccess.CommitTransaction();

                //write blob datatype
                foreach (DictionaryEntry curblob in tempBlobWYS)
                {
                    Byte[] tempByte = System.Text.Encoding.Unicode.GetBytes(Convert.ToString(curblob.Value));
                    dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", curblob.Key.ToString(), "WYS", tempByte, tempByte.Length, RisDAL.ConnectionState.KeepOpen);
                }

                foreach (DictionaryEntry curblob in tempBlobWYG)
                {
                    Byte[] tempByte = System.Text.Encoding.Unicode.GetBytes(Convert.ToString(curblob.Value));
                    dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", curblob.Key.ToString(), "WYG", tempByte, tempByte.Length, RisDAL.ConnectionState.KeepOpen);
                }

                foreach (DictionaryEntry curblob in tempBlobAppendInfo)
                {
                    Byte[] tempByte = System.Text.Encoding.Unicode.GetBytes(Convert.ToString(curblob.Value));
                    dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", curblob.Key.ToString(), "AppendInfo", tempByte, tempByte.Length, RisDAL.ConnectionState.KeepOpen);
                }

                foreach (DictionaryEntry curblob in tempBlobTechInfo)
                {
                    Byte[] tempByte = System.Text.Encoding.Unicode.GetBytes(Convert.ToString(curblob.Value));
                    dataAccess.WriteLargeObj("tReportTemplate", "TemplateGuid", curblob.Key.ToString(), "TechInfo", tempByte, tempByte.Length, RisDAL.ConnectionState.KeepOpen);
                }

                return true;

            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dataAccess.RollbackTransaction();
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
        }

        public override DataSet GetALLReportTemplate(string Site)
        {
            RisDAL dataAccess = new RisDAL();
            string spName = "USP_GetTemplateRecursive";
            DataSet dsTemplate = new DataSet();
            try
            {
                dsTemplate.DataSetName = "ReportTemplateDataSet";
                dataAccess.Parameters.Add("@Site", Site);
                dataAccess.Parameters.Add("@DirectoryType", "Report");
                dataAccess.ExecuteQuerySP(spName, dsTemplate, "ReportTemplate");
                if (dsTemplate.Tables.Count > 1)
                {
                    dsTemplate.Tables[1].TableName = "ReportTemplateDirec";
                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dsTemplate = null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return dsTemplate;
        }


        #endregion

        #region IACRCodeDAO Section

        public override string AddNewAnatomyStorProc(string strAid, string strDesc, string strDesc_en, string strDomain)
        {
            string strSid = null;
            strDesc.Replace("'", "''");
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string szstoredProc = @"SP_AddAnatomy";

                oKodakDAL.Parameters.AddVarChar("pare_aid", strAid, 1);
                oKodakDAL.Parameters.AddVarChar("pare_desc", strDesc, 255);
                oKodakDAL.Parameters.Add("pare_sid", OracleType.NVarChar);
                oKodakDAL.Parameters["pare_sid"].Direction = ParameterDirection.Output;
                oKodakDAL.ExecuteNonQuerySP(szstoredProc);

                strSid = oKodakDAL.Parameters["pare_sid"].Value.ToString();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return strSid.Trim();
        }

        public override string AddNewPathologyStorProc(string strAid, string strPid, string strDesc, string strDesc_en, string strDomain)
        {

            string strSid = null;
            strDesc.Replace("'", "''");
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string szstoredProc = @"SP_AddPathology";

                oKodakDAL.Parameters.AddVarChar("para_aid", strAid, 1);
                oKodakDAL.Parameters.AddVarChar("para_pid", strPid, 1);
                oKodakDAL.Parameters.AddVarChar("para_desc", strDesc, 255);
                oKodakDAL.Parameters.Add("para_sid", OracleType.NVarChar);
                oKodakDAL.Parameters["para_sid"].Direction = ParameterDirection.Output;
                oKodakDAL.ExecuteNonQuerySP(szstoredProc);

                strSid = oKodakDAL.Parameters["para_sid"].Value.ToString();
            }
            catch (Exception Ex)
            {
                throw Ex;

            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return strSid.Trim();
        }


        public override bool ImportProcedureCode(DataSet procedureCodeDataSet, bool isClear, string Site)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();

                if (procedureCodeDataSet.Tables.Count != 1)
                {
                    return false;
                }
                string sql;

                if (!procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Contains("EnglishDescription"))
                {
                    procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Add("EnglishDescription");
                }
                if (!procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Contains("Preparation"))
                {
                    procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Add("Preparation");
                }
                if (!procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Contains("FilmSpec"))
                {
                    procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Add("FilmSpec");
                }
                if (!procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Contains("ContrastName"))
                {
                    procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Add("ContrastName");
                }
                if (!procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Contains("ContrastDose"))
                {
                    procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Add("ContrastDose");
                }
                if (!procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Contains("BookingNotice"))
                {
                    procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Add("BookingNotice");
                }
                if (!procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Contains("ShortcutCode"))
                {
                    procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Add("ShortcutCode");
                }
                if (!procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Contains("FilmCount"))
                {
                    procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Add("FilmCount");
                }
                if (!procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Contains("ImageCount"))
                {
                    procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Add("ImageCount");
                }
                if (!procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Contains("ExposalCount"))
                {
                    procedureCodeDataSet.Tables["ProcedureCodeTable"].Columns.Add("ExposalCount");
                }

                int FilmCount;
                int ImageCount;
                int ExposalCount;

                foreach (DataRow myRow in procedureCodeDataSet.Tables["ProcedureCodeTable"].Rows)
                {
                    //dataAccess.Parameters.Clear();
                    string sqlselect = string.Format("select count(*) from tProcedureCode where ProcedureCode='{0}'", Convert.ToString(myRow["ProcedureCode"]));
                    int count = Convert.ToInt32(dataAccess.ExecuteScalar(sqlselect, RisDAL.ConnectionState.KeepOpen));
                    if (count == 0)
                    {

                        string ProcedureCode = Convert.ToString(myRow["ProcedureCode"]);
                        string Description = Convert.ToString(myRow["Description"]);

                        string EnglishDescription = Convert.ToString(myRow["EnglishDescription"]);
                        string Preparation = Convert.ToString(myRow["Preparation"]);
                        string FilmSpec = Convert.ToString(myRow["FilmSpec"]);
                        string ContrastName = Convert.ToString(myRow["ContrastName"]);
                        string ContrastDose = Convert.ToString(myRow["ContrastDose"]);
                        string BookingNotice = Convert.ToString(myRow["BookingNotice"]);
                        string ShortcutCode = Convert.ToString(myRow["ShortcutCode"]);


                        string ModalityType = Convert.ToString(myRow["ModalityType"]);
                        string BodyPart = Convert.ToString(myRow["BodyPart"]);
                        string CheckingItem = Convert.ToString(myRow["CheckingItem"]);
                        decimal Charge = Convert.ToDecimal(myRow["Charge"]);


                        int Frequency = Convert.ToInt16(myRow["Frequency"]);
                        string BodyCategory = Convert.ToString(myRow["BodyCategory"]);
                        int Duration = Convert.ToInt16(myRow["Duration"]);

                        if (DBNull.Value.Equals(myRow["FilmCount"]))
                            FilmCount = 0;
                        else
                            FilmCount = Convert.ToInt16(myRow["FilmCount"]);

                        if (DBNull.Value.Equals(myRow["ImageCount"]))
                            ImageCount = 0;
                        else
                            ImageCount = Convert.ToInt16(myRow["ImageCount"]);

                        if (DBNull.Value.Equals(myRow["ExposalCount"]))
                            ExposalCount = 0;
                        else
                            ExposalCount = Convert.ToInt16(myRow["ExposalCount"]);



                        sql = string.Format("Insert into tProcedureCode(ProcedureCode,Description,EnglishDescription,ModalityType,BodyPart,CheckingItem,Charge,Preparation,Frequency,BodyCategory,Duration,FilmSpec,FilmCount,ContrastName,ContrastDose,ImageCount,ExposalCount,BookingNotice,ShortcutCode) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}',{8},'{9}',{10},'{11}',{12},'{13}','{14}',{15},{16},'{17}','{18}') ", ProcedureCode, Description, EnglishDescription, ModalityType, BodyPart, CheckingItem, Charge, Preparation, Frequency, BodyCategory, Duration, FilmSpec, FilmCount, ContrastName, ContrastDose, ImageCount, ExposalCount, BookingNotice, ShortcutCode);
                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);

                    }
                    else
                    {
                        string ProcedureCode = Convert.ToString(myRow["ProcedureCode"]);
                        string Description = Convert.ToString(myRow["Description"]);

                        string EnglishDescription = Convert.ToString(myRow["EnglishDescription"]);
                        string Preparation = Convert.ToString(myRow["Preparation"]);
                        string FilmSpec = Convert.ToString(myRow["FilmSpec"]);
                        string ContrastName = Convert.ToString(myRow["ContrastName"]);
                        string ContrastDose = Convert.ToString(myRow["ContrastDose"]);
                        string BookingNotice = Convert.ToString(myRow["BookingNotice"]);
                        string ShortcutCode = Convert.ToString(myRow["ShortcutCode"]);


                        string ModalityType = Convert.ToString(myRow["ModalityType"]);
                        string BodyPart = Convert.ToString(myRow["BodyPart"]);
                        string CheckingItem = Convert.ToString(myRow["CheckingItem"]);
                        decimal Charge = Convert.ToDecimal(myRow["Charge"]);


                        int Frequency = Convert.ToInt16(myRow["Frequency"]);
                        string BodyCategory = Convert.ToString(myRow["BodyCategory"]);
                        int Duration = Convert.ToInt16(myRow["Duration"]);

                        //int FilmCount = Convert.ToInt16(myRow["FilmCount"]);
                        //int ImageCount = Convert.ToInt16(myRow["ImageCount"]);
                        //int ExposalCount = Convert.ToInt16(myRow["ExposalCount"]);

                        if (DBNull.Value.Equals(myRow["FilmCount"]))
                            FilmCount = 0;
                        else
                            FilmCount = Convert.ToInt16(myRow["FilmCount"]);

                        if (DBNull.Value.Equals(myRow["ImageCount"]))
                            ImageCount = 0;
                        else
                            ImageCount = Convert.ToInt16(myRow["ImageCount"]);

                        if (DBNull.Value.Equals(myRow["ExposalCount"]))
                            ExposalCount = 0;
                        else
                            ExposalCount = Convert.ToInt16(myRow["ExposalCount"]);


                        sql = string.Format("update tProcedureCode set Description='{1}',EnglishDescription='{2}',ModalityType='{3}',BodyPart='{4}',CheckingItem='{5}',Charge={6},Preparation='{7}',Frequency = {8},BodyCategory='{9}',Duration={10},FilmSpec='{11}',FilmCount={12},ContrastName='{13}',ContrastDose='{14}',ImageCount={15},ExposalCount={16},BookingNotice='{17}',ShortcutCode='{18}' where ProcedureCode= '{0}'", ProcedureCode, Description, EnglishDescription, ModalityType, BodyPart, CheckingItem, Charge, Preparation, Frequency, BodyCategory, Duration, FilmSpec, FilmCount, ContrastName, ContrastDose, ImageCount, ExposalCount, BookingNotice, ShortcutCode);
                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                }


                dataAccess.CommitTransaction();
                return true;

            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dataAccess.RollbackTransaction();
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
        }

        public override DataSet GetAllProcedureCode(string Site)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            string sql = string.Format("select * from tProcedureCode where site ='{0}'",Site);
            try
            {
                DataTable procedureCodeTable;

                procedureCodeTable = dataAccess.ExecuteQuery(sql);
                procedureCodeTable.TableName = "ProcedureCodeTable";
                ds.Tables.Add(procedureCodeTable);
                ds.DataSetName = "ProcedureCodeDataSet";

            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                ds = null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }

        public override bool ImportACRcode(DataSet acrcodeDataSet, bool isClear)
        {
            RisDAL dataAccess = new RisDAL();
            
            try
            {
                dataAccess.BeginTransaction();
                if (isClear)
                {
                    dataAccess.ExecuteNonQuery("delete from tACRCodeAnatomical", RisDAL.ConnectionState.KeepOpen);
                    dataAccess.ExecuteNonQuery("delete from tACRCodePathological", RisDAL.ConnectionState.KeepOpen);
                    dataAccess.ExecuteNonQuery("delete from tACRCodeSubAnatomical", RisDAL.ConnectionState.KeepOpen);
                    dataAccess.ExecuteNonQuery("delete from tACRCodeSubPathological", RisDAL.ConnectionState.KeepOpen);
                }
                if (acrcodeDataSet.Tables.Count != 4)
                {
                    return false;
                }
                string sql;

                foreach (DataRow myRow in acrcodeDataSet.Tables["Anatomical"].Rows)
                {
                    string strAID = Convert.ToString(myRow["AID"]);
                    string strDesc = Convert.ToString(myRow["Description"]);
                    string strDescEn = Convert.ToString(myRow["DescriptionEn"]);

                    if (strDesc.IndexOf("'") > 0)
                    {
                        strDesc = strDesc.Replace("'", "''");
                    }
                    if (strDescEn.IndexOf("'") > 0)
                    {
                        strDescEn = strDescEn.Replace("'", "''");
                    }

                    sql = string.Format("Insert into tACRCodeAnatomical(AID,Description,DescriptionEn)  values('{0}','{1}','{2}') ", strAID, strDesc, strDescEn);
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }

                foreach (DataRow myRow in acrcodeDataSet.Tables["SubAnatomical"].Rows)
                {
                    string strAID = Convert.ToString(myRow["AID"]);
                    string strSID = Convert.ToString(myRow["SID"]);
                    string strDesc = Convert.ToString(myRow["Description"]);
                    string strDescEn = Convert.ToString(myRow["DescriptionEn"]);

                    if (strDesc.IndexOf("'") > 0)
                    {
                        strDesc = strDesc.Replace("'", "''");
                    }
                    if (strDescEn.IndexOf("'") > 0)
                    {
                        strDescEn = strDescEn.Replace("'", "''");
                    }

                    sql = string.Format("Insert into tACRCodeSubAnatomical(AID,SID,Description,DescriptionEn)  values('{0}','{1}','{2}','{3}') ",strAID,strSID,strDesc,strDescEn);
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }

                foreach (DataRow myRow in acrcodeDataSet.Tables["Pathological"].Rows)
                {
                    string strAID = Convert.ToString(myRow["AID"]);
                    string strPID = Convert.ToString(myRow["PID"]);
                    string strDesc = Convert.ToString(myRow["Description"]);
                    string strDescEn = Convert.ToString(myRow["DescriptionEn"]);

                    if (strDesc.IndexOf("'") > 0)
                    {
                        strDesc = strDesc.Replace("'", "''");
                    }
                    if (strDescEn.IndexOf("'") > 0)
                    {
                        strDescEn = strDescEn.Replace("'", "''");
                    }

                    sql = string.Format("Insert into tACRCodePathological(AID,PID,Description,DescriptionEn)  values('{0}','{1}','{2}','{3}') ",strAID,strPID,strDesc,strDescEn);
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }

                foreach (DataRow myRow in acrcodeDataSet.Tables["SubPathological"].Rows)
                {
                    string strAID = Convert.ToString(myRow["AID"]);
                    string strPID = Convert.ToString(myRow["PID"]);
                    string strSID = Convert.ToString(myRow["SID"]);
                    string strDesc = Convert.ToString(myRow["Description"]);
                    string strDescEn = Convert.ToString(myRow["DescriptionEn"]);

                    if (strDesc.IndexOf("'")>0)
                    {
                        strDesc = strDesc.Replace("'", "''");
                    }
                    if (strDescEn.IndexOf("'") > 0)
                    {
                        strDescEn = strDescEn.Replace("'","''");
                    }

                    sql = string.Format("Insert into tACRCodeSubPathological(AID,PID,SID,Description,DescriptionEn)  values('{0}','{1}','{2}','{3}','{4}')", strAID, strPID, strSID, strDesc, strDescEn);

                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);

                }
                dataAccess.CommitTransaction();
                return true;

            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dataAccess.RollbackTransaction();
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

        }

        public override DataSet GetAllAcrCode()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet("ACRCodeDataSet");
            string sqlAnatomical = "select * from tACRCodeAnatomical";
            string sqlPathological = "select * from tACRCodePathological";
            string sqlSubAnatomical = "select * from tACRCodeSubAnatomical";
            string sqlSubPathological = "select * from tACRCodeSubPathological";
            try
            {
                DataTable anatomicalTable;
                DataTable subAnatomicalTable;
                DataTable pathologicalTable;
                DataTable SubPathologicalTable;
                anatomicalTable = dataAccess.ExecuteQuery(sqlAnatomical);
                anatomicalTable.TableName = "Anatomical";
                subAnatomicalTable = dataAccess.ExecuteQuery(sqlSubAnatomical);
                subAnatomicalTable.TableName = "SubAnatomical";
                pathologicalTable = dataAccess.ExecuteQuery(sqlPathological);
                pathologicalTable.TableName = "Pathological";
                SubPathologicalTable = dataAccess.ExecuteQuery(sqlSubPathological);
                SubPathologicalTable.TableName = "SubPathological";
                ds.Tables.Add(anatomicalTable);
                ds.Tables.Add(subAnatomicalTable);
                ds.Tables.Add(pathologicalTable);
                ds.Tables.Add(SubPathologicalTable);
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                ds = null;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return ds;
        }

        #endregion

    }
}
