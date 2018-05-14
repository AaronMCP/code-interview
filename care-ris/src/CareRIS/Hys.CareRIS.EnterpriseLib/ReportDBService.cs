using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hys.CareRIS.EnterpriseLib
{
    public class ReportDBService
    {
        private string _connectionString = "";
        //private static DataTable m_dtPrint;
        //private static DataTable _dt4Print;

        public ReportDBService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RisProContext"].ConnectionString;
        }

        public int GetPatientExamNo(string patientID, string orderID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(_connectionString);
                DbCommand dbCommand = db.GetStoredProcCommand("procGetPatientExamNo");
                db.AddInParameter(dbCommand, "@PatientID", DbType.String, patientID);
                db.AddInParameter(dbCommand, "@OrderGuid", DbType.String, orderID);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0][0] != null)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }

            }
            catch (Exception ex)
            {
            }

            return 0;

        }

        public void GetReportPrintTemplate(string accession_number, string modality_type, string report_guid, string loginSite,
            out string strTemplateGuid, out DataTable dt)
        {
            strTemplateGuid = "";
            dt = null;
            SqlConnection conn = new SqlConnection();
            try
            {
                if (string.IsNullOrEmpty(accession_number))
                {
                    return;
                }

                conn.ConnectionString = _connectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                string[] paramsA = new string[2] { accession_number, report_guid };
                cmd.CommandType = CommandType.StoredProcedure;
                string site = loginSite;
                cmd.Parameters.Add(new SqlParameter("@AccNo", accession_number));
                cmd.Parameters.Add(new SqlParameter("@Type", "3"));
                cmd.Parameters.Add(new SqlParameter("@ModalityType", modality_type));
                cmd.Parameters.Add(new SqlParameter("@Site", site));
                cmd.Parameters.Add(new SqlParameter("@ReportGuid", report_guid));

                SqlParameter sqlTemplateGUID = new SqlParameter("@TemplateGuid", SqlDbType.VarChar, 128);
                sqlTemplateGUID.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(sqlTemplateGUID);
                SqlParameter MergeMultiProcedures = new SqlParameter("@MergeMultiProcedures", SqlDbType.Int, 8);
                MergeMultiProcedures.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(MergeMultiProcedures);

                cmd.CommandText = "procGetPrintTemplate";
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dt = ds.Tables[0];
                strTemplateGuid = cmd.Parameters["@TemplateGuid"].Value.ToString();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

        public void GetOtherReportPrintTemplate(string accno, string modalityType, string templateType, string site,
            out string strTemplateInfo, out DataTable dtData, int type, out string templateID)
        {
            strTemplateInfo = "";
            templateID = "";
            dtData = null;
            DataTable m_dtPrint = null;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AccNo", accno));
            cmd.Parameters.Add(new SqlParameter("@Type", templateType));
            cmd.Parameters.Add(new SqlParameter("@ModalityType", modalityType));
            cmd.Parameters.Add(new SqlParameter("@Site", site));
            cmd.Parameters.Add(new SqlParameter("@ReportGuid", ""));

            SqlParameter sqlTemplateGUID = new SqlParameter("@TemplateGuid", SqlDbType.VarChar, 128);
            sqlTemplateGUID.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sqlTemplateGUID);
            SqlParameter MergeMultiProcedures = new SqlParameter("@MergeMultiProcedures", SqlDbType.Int, 8);
            MergeMultiProcedures.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(MergeMultiProcedures);

            cmd.CommandText = "procGetPrintTemplate";
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            var dt = ds.Tables[0];
            var strTemplateGuid = cmd.Parameters["@TemplateGuid"].Value.ToString();

            if (type == 1)
            {
                templateID = strTemplateGuid;
                return;
            }
            string strMergeMultiProcedures = cmd.Parameters["@MergeMultiProcedures"].Value.ToString();
            try
            {
                //if (m_dtPrint != null)
                //{
                //    m_dtPrint.Rows.Clear();
                //}
                //else
                //{
                //    InitialTemplateDatatable();
                //}
                m_dtPrint = InitialTemplateDatatable();

                DataRow dr = m_dtPrint.NewRow();
                for (int ir = 0; ir < dt.Rows.Count; ir++)
                {
                    if (ir == 0 || strMergeMultiProcedures.CompareTo("0") == 0)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            foreach (DataColumn col in m_dtPrint.Columns)
                            {
                                if (col.ColumnName == dc.ColumnName)
                                {
                                    object objValue = dt.Rows[ir][dc.ColumnName];
                                    string Value;
                                    if (objValue.GetType() == System.Type.GetType("System.Byte[]"))
                                    {
                                        Byte[] buff = objValue as Byte[];
                                        if (dc.ColumnName == "BookingNotice")
                                        {
                                            Value = System.Text.Encoding.UTF8.GetString(buff);
                                        }
                                        else
                                        {
                                            Value = System.Text.Encoding.Default.GetString(buff);
                                        }
                                        dr[dc.ColumnName] = Value;
                                    }
                                    else if (col.DataType == typeof(System.DateTime))
                                    {
                                        dr[dc.ColumnName] = objValue;
                                    }
                                    else
                                    {
                                        Value = objValue.ToString().Trim();
                                        dr[dc.ColumnName] = GetTranslationString(dc.ColumnName, Value);
                                    }
                                    break;
                                }
                            }
                        }
                        if (strMergeMultiProcedures.CompareTo("0") == 0)
                        {
                            m_dtPrint.Rows.Add(dr);
                            dr = m_dtPrint.NewRow();
                        }
                    }
                    else
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName == "CheckingItem" || dc.ColumnName == "Modality" || dc.ColumnName == "ModalityType" || dc.ColumnName == "Description")
                            {
                                if (dt.Rows[ir][dc.ColumnName].ToString().Trim() != dr[dc.ColumnName].ToString().Trim())
                                {
                                    dr[dc.ColumnName] = dr[dc.ColumnName] + "," + dt.Rows[ir][dc.ColumnName];
                                }
                            }
                            if (dc.ColumnName == "BookingNotice")
                            {
                                object objValue = dt.Rows[ir][dc.ColumnName];
                                string Value;
                                if (objValue.GetType() == System.Type.GetType("System.Byte[]"))
                                {
                                    Byte[] buff = objValue as Byte[];
                                    Value = System.Text.Encoding.UTF8.GetString(buff);
                                    dr[dc.ColumnName] = MergeRTFFiles(dr[dc.ColumnName].ToString(), Value);
                                }
                            }
                        }
                    }
                    dr["AccNo"] = accno;
                    dr["Image"] = null;
                }
                if (strMergeMultiProcedures.CompareTo("0") != 0)
                {
                    m_dtPrint.Rows.Add(dr);
                }

                strTemplateInfo = LoadPrintTemplateInfo(strTemplateGuid);
                dtData = m_dtPrint;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public bool GetDateTypeAvailableDate(string modality, DateTime bookingDate, ref string dateType, ref DateTime availableDate)
        {
            DbCommand dbCommand = null;
            try
            {
                SqlDatabase db = new SqlDatabase(_connectionString);
                dbCommand = db.GetStoredProcCommand("procGetDateTypeAvailableDate");
                db.AddInParameter(dbCommand, "@Modality", DbType.String, modality);
                db.AddInParameter(dbCommand, "@BookingDate", DbType.String, bookingDate.ToString("yyyy-MM-dd"));
                db.AddOutParameter(dbCommand, "@DateType", DbType.String, 256);
                db.AddOutParameter(dbCommand, "@AvailableDate", DbType.String, 256);
                db.ExecuteNonQuery(dbCommand);
                if (DateTime.TryParse(db.GetParameterValue(dbCommand, "@AvailableDate") as string, out availableDate))
                {
                    dateType = db.GetParameterValue(dbCommand, "@DateType") as string;
                    return true;
                }
            }
            finally
            {
                if (dbCommand != null) dbCommand.Dispose();
            }

            return false;
        }

        public string LockModalityQuota(string modality, string dateType, DateTime availableDate,
            DateTime bookingDate, string timeSliceGuid, string bookingSite)
        {
            DbCommand dbCommand = null;
            string lockGuid = "";
            try
            {
                SqlDatabase db = new SqlDatabase(_connectionString);
                dbCommand = db.GetStoredProcCommand("procLockModalityQuota");
                db.AddInParameter(dbCommand, "@Modality", DbType.String, modality);
                db.AddInParameter(dbCommand, "@DateType", DbType.String, dateType);
                db.AddInParameter(dbCommand, "@AvailableDate", DbType.String, availableDate.ToString("yyyy-MM-dd"));
                db.AddInParameter(dbCommand, "@BookingDate", DbType.String, bookingDate.ToString("yyyy-MM-dd"));
                db.AddInParameter(dbCommand, "@TimeSliceGuid", DbType.String, timeSliceGuid);
                db.AddInParameter(dbCommand, "@BookingSite", DbType.String, bookingSite);
                db.AddInParameter(dbCommand, "@UnlockGuid", DbType.String, "");
                db.AddOutParameter(dbCommand, "@LockGuid", DbType.String, 256);
                db.AddOutParameter(dbCommand, "@cnt", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCommand);
                if (Convert.ToInt32(db.GetParameterValue(dbCommand, "@cnt")) > 0)
                {
                    lockGuid = db.GetParameterValue(dbCommand, "@LockGuid") as string;
                }
            }
            finally
            {
                if (dbCommand != null) dbCommand.Dispose();
            }

            return lockGuid;
        }
        public void UnlockModalityQuota(string unlockGuid)
        {
            DbCommand dbCommand = null;
            try
            {
                SqlDatabase db = new SqlDatabase(_connectionString);
                dbCommand = db.GetStoredProcCommand("procLockModalityQuota");
                db.AddInParameter(dbCommand, "@Modality", DbType.String, "");
                db.AddInParameter(dbCommand, "@DateType", DbType.String, "");
                db.AddInParameter(dbCommand, "@AvailableDate", DbType.String, "");
                db.AddInParameter(dbCommand, "@BookingDate", DbType.String, "");
                db.AddInParameter(dbCommand, "@TimeSliceGuid", DbType.String, "");
                db.AddInParameter(dbCommand, "@BookingSite", DbType.String, "");
                db.AddInParameter(dbCommand, "@UnlockGuid", DbType.String, unlockGuid);
                db.AddOutParameter(dbCommand, "@LockGuid", DbType.String, 256);
                db.AddOutParameter(dbCommand, "@cnt", DbType.Int32, 4);

                db.ExecuteNonQuery(dbCommand);
                if (Convert.ToInt32(db.GetParameterValue(dbCommand, "@cnt")) <= 0)
                {
                    throw new Exception(string.Format("Unlock Guid {0} failed", unlockGuid));
                }
            }
            finally
            {
                if (dbCommand != null) dbCommand.Dispose();
            }
        }

        private string LoadPrintTemplateInfo(string strTemplateGuid)
        {
            SqlConnection conn = new SqlConnection();
            string strTemplateInfo = string.Empty;
            try
            {
                conn.ConnectionString = _connectionString;
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Clear();
                cmd.CommandText = string.Format("select TemplateInfo from tbPrintTemplate where TemplateGuid='{0}'", strTemplateGuid);
                strTemplateInfo = System.Text.Encoding.Unicode.GetString(cmd.ExecuteScalar() as byte[]);
            }
            catch (Exception e)
            {
                //logger.Error(e.ToString());
                //return null;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return strTemplateInfo;
        }

        private DataTable InitialTemplateDatatable()
        {
            DataTable m_dtPrint = new DataTable();
            m_dtPrint.Columns.Add("Room", typeof(string));
            m_dtPrint.Columns.Add("PatientID", typeof(string));
            m_dtPrint.Columns.Add("LocalName", typeof(string));
            m_dtPrint.Columns.Add("EnglishName", typeof(string));
            m_dtPrint.Columns.Add("Birthday", typeof(DateTime));
            m_dtPrint.Columns.Add("Gender", typeof(string));
            m_dtPrint.Columns.Add("Telephone", typeof(string));
            m_dtPrint.Columns.Add("ReferenceNo", typeof(string));
            m_dtPrint.Columns.Add("Address", typeof(string));
            m_dtPrint.Columns.Add("InhospitalNo", typeof(string));
            m_dtPrint.Columns.Add("PatientType", typeof(string));
            m_dtPrint.Columns.Add("ClinicNo", typeof(string));
            m_dtPrint.Columns.Add("BedNo", typeof(string));
            m_dtPrint.Columns.Add("InhospitalRegion", typeof(string));
            m_dtPrint.Columns.Add("ApplyDoctor", typeof(string));
            m_dtPrint.Columns.Add("ApplyDept", typeof(string));
            m_dtPrint.Columns.Add("RegisterDt", typeof(DateTime));
            m_dtPrint.Columns.Add("Description", typeof(string));
            m_dtPrint.Columns.Add("ModalityType", typeof(string));
            m_dtPrint.Columns.Add("Modality", typeof(string));
            m_dtPrint.Columns.Add("BodyPart", typeof(string));
            m_dtPrint.Columns.Add("CheckingItem", typeof(string));
            m_dtPrint.Columns.Add("BookingBeginDt", typeof(string));
            m_dtPrint.Columns.Add("BookingEndDt", typeof(string));
            m_dtPrint.Columns.Add("VisitComment", typeof(string));
            m_dtPrint.Columns.Add("HealthHistory", typeof(string));
            m_dtPrint.Columns.Add("Observation", typeof(string));
            m_dtPrint.Columns.Add("Alias", typeof(string));
            m_dtPrint.Columns.Add("QueueNo", typeof(string));
            m_dtPrint.Columns.Add("RemotePID", typeof(string));
            m_dtPrint.Columns.Add("HisID", typeof(string));
            m_dtPrint.Columns.Add("RemoteAccNo", typeof(string));
            m_dtPrint.Columns.Add("CardNo", typeof(string));
            m_dtPrint.Columns.Add("MedicareNo", typeof(string));
            m_dtPrint.Columns.Add("BedSide", typeof(string));
            m_dtPrint.Columns.Add("BodyWeight", typeof(string));
            m_dtPrint.Columns.Add("BookingTimeAlias", typeof(string));
            m_dtPrint.Columns.Add("Age", typeof(string));
            m_dtPrint.Columns.Add("OrderComment", typeof(string));
            m_dtPrint.Columns.Add("ErethismType", typeof(string));
            m_dtPrint.Columns.Add("ErethismCode", typeof(string));
            m_dtPrint.Columns.Add("ErethismGrade", typeof(string));
            m_dtPrint.Columns.Add("OrderOptional1", typeof(string));
            m_dtPrint.Columns.Add("TakeReportDate", typeof(string));
            m_dtPrint.Columns.Add("BookingNotice", typeof(string));
            m_dtPrint.Columns.Add("Image", typeof(Image));
            m_dtPrint.Columns.Add("AccNo", typeof(string));

            return m_dtPrint;
        }

        private string GetTranslationString(string colName, string Value)
        {
            try
            {
                colName = colName.ToUpper().Trim();

                if (colName == "AGE")
                {
                    string[] split = Value.Split(new Char[] { ' ' });
                    if (split.Length != 2)
                    {
                        return Value;
                    }

                    Value = split[0] + " " + DataClass.GetDictionaryText(6, split[1]);
                }
                else if (DataClass.listUserGuid2.Contains(colName))
                {
                    Value = DataClass.GetUserLocalName(Value);
                }
                else if (DataClass.listYesNo2.Contains(colName))
                {
                    Value = DataClass.GetDictionaryText(70, Value);
                }
                else if (DataClass.listSite2.Contains(colName))
                {
                    Value = DataClass.GetSiteAlias(Value);
                }
                else if (DataClass.listDomain2.Contains(colName))
                {
                    Value = DataClass.GetDomainAlias(Value);
                }
            }
            catch (Exception ex)
            {
                //logger.Error("GetTranslationString " + ex.Message);
            }

            return Value;
        }

        private string MergeRTFFiles(string strDest, string strSource)
        {
            string strMerge = strDest;
            try
            {
                if (strDest.Contains(strSource))
                {
                    return strMerge;
                }

                RichTextBox richTextBox1 = new RichTextBox();
                RichTextBox richTextBox2 = new RichTextBox();
                RichTextBox richTextBox3 = new RichTextBox();//for restore the original content in clipboard

                richTextBox3.Paste();

                richTextBox2.Copy();
                richTextBox2.Rtf = strDest;
                if (richTextBox2.Text.Trim().Length == 0)
                {
                    richTextBox2.Rtf = strSource;
                }
                else
                {
                    richTextBox2.Text = richTextBox2.Text + "\r\n";
                    richTextBox1.Rtf = strSource;
                    if (richTextBox1.Text.Trim().Length > 0)
                    {
                        richTextBox1.SelectAll();
                        richTextBox1.Copy();
                        richTextBox2.Select(richTextBox2.TextLength, 0);
                        richTextBox2.Paste();
                    }
                }
                strMerge = richTextBox2.Rtf;

                richTextBox1.Dispose();
                richTextBox2.Dispose();
                richTextBox1 = null;
                richTextBox2 = null;

                richTextBox3.SelectAll();
                richTextBox3.Copy();
                richTextBox3.SelectAll();
                richTextBox3.Paste();
                richTextBox3.Dispose();
                richTextBox3 = null;
            }
            catch (Exception ex)
            {
                //logger.Error("MergeRTFFiles " + ex.Message);
            }
            return strMerge;
        }

        #region 权限配置
        /// <summary>
        /// 用户权限配置
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="strDomain"></param>
        /// <returns></returns>
        public DataSet GetUserProfDetDataSet(string userGuid, string strDomain)
        {
            DataSet dataSet = new DataSet();
            //DataTable dtSystemProf = new DataTable();
            string strGetUserProfSQL = string.Empty;
            string strGetUserRoleSQL = string.Empty;
            string strTemplateInfo = string.Empty;
            try
            {
                strGetUserProfSQL = string.Format("select A.Name,A.ModuleId,B.Title as ModuleName,A.UserGuid,A.RoleName,A.Value,A.Exportable,A.PropertyDesc,A.PropertyOptions,A.Inheritance,A.PropertyType,A.IsHidden,A.OrderingPos from tbUserProfile A, tbModule B where A.ModuleId = B.ModuleId and ((A.IsHidden & 1) = 1) and A.UserGuid = '{0}' and A.Domain='{1}' ORDER BY A.OrderingPos", userGuid.ToString(), strDomain);
                DataTable dataTable = ExecuteQuery(strGetUserProfSQL);
                DataTable customedTable = CreataCustomDataTable();
                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                foreach (DataRow row in dataTable.Rows)
                {
                    DataRow rowData = customedTable.NewRow();                  
                    rowData[0] = row[dataTable.Columns["ModuleId"]].ToString();
                    rowData[1] = row[dataTable.Columns["Name"]].ToString();
                    rowData[2] = row[dataTable.Columns["Value"]].ToString();
                    rowData[3] = row[dataTable.Columns["RoleName"]].ToString();
                    strPropertyOption = row[dataTable.Columns["PropertyOptions"]].ToString();
                    string strName = row[dataTable.Columns["Name"]].ToString();
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
                                string strSQL = "";
                                if (strProp.Contains("where"))
                                {
                                    strSQL = strProp + string.Format(" and domain='{0}'", strDomain);
                                }
                                else if (strProp.Contains("1=1"))
                                {
                                    strSQL = strProp;
                                }
                                else
                                {
                                    strSQL = strProp + string.Format(" where domain='{0}'", strDomain);
                                }
                               
                                dtPropOption= ExecuteQuery(strSQL);
                            }

                            bool hasText = false;
                            bool hasValue = false;
                            foreach (DataColumn column in dtPropOption.Columns)
                            {
                                if (column.ColumnName.ToUpper() == "TEXT")
                                    hasText = true;
                                if (column.ColumnName.ToUpper() == "VALUE")
                                    hasValue = true;
                            }

                            if (dtPropOption.Columns.Count > 1 && hasText && hasValue)
                            {
                                dtPropOption.TableName = row[dataTable.Columns["Name"]].ToString();
                                rowData[4] = dtPropOption;
                            }
                            else
                            {
                                ////单列结果也做成dataTable
                                //DataTable daTable = new DataTable();
                                //daTable.Columns.Add("Text", typeof(object));
                                //daTable.Columns.Add("Value", typeof(object));               
                                foreach (DataRow drPropDetail in dtPropOption.Rows)
                                {
                                    ////下拉集合的row
                                    //DataRow opRow = daTable.NewRow();
                                    //opRow["Text"] = drPropDetail[0];
                                    //opRow["Value"] = drPropDetail[0];
                                    //daTable.Rows.Add(opRow);
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
                                //rowData[4] = daTable;
                                if ((row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.ListBox)
                                 || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckComboBox)
                                    || (row[dataTable.Columns["PropertyType"]].ToString() == PropertyItemType.CheckBox))
                                {
                                    if (rowData[4] != null)
                                        rowData[4] = rowData[4].ToString().Remove(rowData[4].ToString().Length - 1);
                                    else
                                        rowData[4] = "";
                                }
                            }
                        }
                    }
                    else
                    {
                        rowData[4] += strPropertyOption;
                    }
                    rowData[5] = row[dataTable.Columns["ModuleName"]].ToString();
                    rowData[6] = row[dataTable.Columns["PropertyType"]].ToString();
                    rowData[7] = "";
                    rowData[8] = row[dataTable.Columns["PropertyDesc"]].ToString();
                    rowData[9] = "";
                    rowData[10] = "";//default not set data to orderid(not use in system)
                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }
                customedTable.TableName = "UserProfile";
                dataSet.Tables.Add(customedTable);

                strGetUserRoleSQL = string.Format("select RoleName from tbRole2User where UserGuid = '{0}' and Domain='{1}'", userGuid.ToString(), strDomain);
                DataTable dtUserRole = ExecuteQuery(strGetUserRoleSQL);
                dtUserRole.TableName = "UserRole";
                dataSet.Tables.Add(dtUserRole);

                strGetUserRoleSQL = string.Format("select department from tbUser2Domain where userguid = '{0}' and domain = '{1}'", userGuid.ToString(), strDomain);
                DataTable dtUserDomain = ExecuteQuery(strGetUserRoleSQL);
                dtUserDomain.TableName = "UserDomain";
                dataSet.Tables.Add(dtUserDomain);

            }
            catch (Exception ex)
            {
                //logger.Error(e.ToString());
                //return null;
                throw new Exception(ex.Message);
            }
            return dataSet;
        }

        private string GetRoleSite(string roleName, string strDomain)
        {      
            string site = "", nodeName = "", parentId = "";
            try
            {
                DataTable dt;
                parentId = ExecuteScalar(string.Format("select ParentID from tbRoleDir where Name ='{0}' and Domain ='{1}' and Leaf = 1", roleName, strDomain)) as string;
                dt = ExecuteQuery(string.Format("select Name,ParentID from tbRoleDir where UniqueID ='{0}'", parentId));

                while (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Name"].ToString() == "GlobalRole")
                    {
                        site = "";
                        break;
                    }
                    else if (dt.Rows[0]["Name"].ToString() == "RoleManagement")
                    {
                        site = nodeName;
                        break;
                    }
                    nodeName = dt.Rows[0]["Name"].ToString();
                    parentId = dt.Rows[0]["ParentID"].ToString();
                    dt = ExecuteQuery(string.Format("select Name,ParentID from tbRoleDir where UniqueID ='{0}'", parentId));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
            return site;

        }
        /// <summary>
        /// 角色权限配置
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="strDomain"></param>
        /// <param name="userGuid"></param>
        /// <param name="isSiteAdmin"></param>
        /// <returns></returns>
        public DataSet GetRoleProfDetDataSet(string roleName, string strDomain, string userGuid, bool isSiteAdmin)
        {
            DataSet dataSet = new DataSet();
            //string sql = SQLGetDictionaryList;
            string strGetRoleProfSQL = string.Empty;
            string strGetSysProfSQL = string.Empty;
            try
            {
                strGetRoleProfSQL = string.Format("SELECT [RoleName],[Name],[tbRoleProfile].[ModuleID],Title as ModuleName,[Value],[Exportable],[PropertyDesc],[PropertyOptions],[Inheritance],[PropertyType],[IsHidden],[OrderingPos] FROM [dbo].[tbRoleProfile], dbo.tbModule where tbModule.ModuleID = [tbRoleProfile].[ModuleID] AND [tbRoleProfile].[RoleName] = '{0}' and ([tbRoleProfile].[IsHidden] & 2) = 2 and tbModule.Domain='{1}'  and tbRoleProfile.Domain='{2}' ORDER BY [tbRoleProfile].[OrderingPos]", roleName.Trim(), strDomain, strDomain);
                DataTable dataTable = ExecuteQuery(strGetRoleProfSQL);
                string site = GetRoleSite(roleName, strDomain);
                //Build the custom DataTable
                DataTable customedTable = CreataCustomDataTable();

                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                foreach (DataRow row in dataTable.Rows)
                {
                    DataRow rowData = customedTable.NewRow();        
                    rowData[0] = row[dataTable.Columns["ModuleId"]].ToString();
                    rowData[1] = row[dataTable.Columns["Name"]].ToString();
                    rowData[2] = row[dataTable.Columns["Value"]].ToString();
                    rowData[3] = row[dataTable.Columns["RoleName"]].ToString();

                    strPropertyOption = row[dataTable.Columns["PropertyOptions"]].ToString();
                    if (!strPropertyOption.Contains("|") || strPropertyOption.Contains("'|'"))
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
                                string strSQL = "";
                                if (strProp.Contains("1=1"))
                                {
                                    strSQL = strProp;
                                }
                                else if (strProp.Contains("where"))
                                {
                                    strSQL = strProp + string.Format(" and Domain='{0}'", strDomain);
                                }
                                else
                                {
                                    strSQL = strProp + string.Format(" where Domain='{0}'", strDomain);
                                }

                                if (isSiteAdmin && row[dataTable.Columns["Name"]].ToString() == "QueryCategories")
                                {
                                    string strSQLBelongToSite = string.Format("select Value from tbUserProfile where UserGuid = '{0}' and Name = 'BelongToSite'", userGuid);
                                    string belongToSite = ExecuteScalar(strSQLBelongToSite).ToString();
                                    if (!string.IsNullOrWhiteSpace(belongToSite))
                                    {
                                        string[] belongToSiteList = belongToSite.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                        strSQL += string.Format(" and Site in ('{0}')", string.Join("' , '", belongToSiteList));
                                    }
                                    else
                                    {
                                        strSQL += " and 1<>1";
                                    }
                                }

                                dtPropOption=ExecuteQuery(strSQL);
                            }

                            bool hasText = false;
                            bool hasValue = false;
                            foreach (DataColumn column in dtPropOption.Columns)
                            {
                                if (column.ColumnName.ToUpper() == "TEXT")
                                    hasText = true;
                                if (column.ColumnName.ToUpper() == "VALUE")
                                    hasValue = true;
                            }

                            if (dtPropOption.Columns.Count > 1 && hasText && hasValue)
                            {
                                dtPropOption.TableName = row[dataTable.Columns["Name"]].ToString();
                                rowData[4] = dtPropOption;
                            }
                            else
                            {
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
                                        rowData[4] = rowData[4].ToString().TrimEnd('|');
                                    else
                                        rowData[4] = "";
                                }
                            }
                        }

                    }
                    else
                    {
                        rowData[4] += strPropertyOption;
                    }

                    rowData[5] = row[dataTable.Columns["ModuleName"]].ToString();
                    rowData[6] = row[dataTable.Columns["PropertyType"]].ToString();
                    rowData[7] = "";
                    rowData[8] = row[dataTable.Columns["PropertyDesc"]].ToString();
                    rowData[9] = "";
                    rowData[10] = "";//default not set data to orderid(not use in system)
                    rowData[11] = site;

                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }
                customedTable.TableName = "RoleProfiles";
                dataSet.Tables.Add(customedTable);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dataSet;
        }
        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <param name="strDomain"></param>
        /// <param name="curSite"></param>
        /// <returns></returns>
        public virtual DataSet GetSystemProfileDataSet(string strDomain,string curSite)
        {

            DataSet dataSet = new DataSet();
            string strGetSysProfSQL = string.Empty;
            string strCurrentRowName = "";
            try
            {

                //strGetSysProfSQL = string.Format("SELECT [tbSystemProfile].[Name],[tbSystemProfile].[ModuleID],[tbModule].Title as ModuleName,[tbSystemProfile].[Value],[tbSystemProfile].[Exportable],[tbSystemProfile].[PropertyDesc],[tbSystemProfile].[PropertyOptions],[tbSystemProfile].[Inheritance],[tbSystemProfile].[PropertyType],[tbSystemProfile].[IsHidden],[tbSystemProfile].[OrderingPos]FROM [tbSystemProfile], tbModule where tbModule.ModuleID = [tbSystemProfile].[ModuleID] AND (([tbSystemProfile].[IsHidden] & 4) = 4) AND [tbSystemProfile].[Inheritance] >= 0 and tbSystemProfile.Domain='{0}' ORDER BY [tbSystemProfile].[OrderingPos]", strDomain);
                strGetSysProfSQL = string.Format("SELECT [tbSystemProfile].[Name],[tbSystemProfile].[ModuleID],[tbModule].Title as ModuleName,[tbSystemProfile].[Value],[tbSystemProfile].[Exportable],[PropertyDesc],[PropertyOptions],[Inheritance],[PropertyType],[IsHidden],[OrderingPos],[OrderNo] FROM [tbSystemProfile], tbModule where tbModule.ModuleID = [tbSystemProfile].[ModuleID] AND (([tbSystemProfile].[IsHidden] & 4) = 4) AND [tbSystemProfile].[Inheritance] >= 0 and tbModule.Domain='{0}' and tbSystemProfile.Domain='{1}' ORDER BY [tbSystemProfile].[OrderingPos]", strDomain, strDomain);
                DataTable dataTable = ExecuteQuery(strGetSysProfSQL);
                //Build the custom DataTable
                DataTable customedTable = CreataCustomDataTable(); ;

                Regex regCheckDomain = new Regex(@"from\s+tbDomainList", RegexOptions.IgnoreCase); //[^-_\w]domain[^-_\w]

                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                foreach (DataRow row in dataTable.Rows)
                {
                    DataRow rowData = customedTable.NewRow();

                    //Module ID- 0
                    rowData[0] = row[dataTable.Columns["ModuleId"]].ToString();

                    //FieldName - 1
                    rowData[1] = row[dataTable.Columns["Name"]].ToString();
                    strCurrentRowName = row[dataTable.Columns["Name"]].ToString();
                    //FieldValue -2
                    rowData[2] = row[dataTable.Columns["Value"]].ToString();
                    //strValue = row[dataTable.Columns["Value"]].ToString();

                    //FieldDescription -3
                    rowData[3] = "";

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
                                string strSQL = "";

                                // EK_HI00114442
                                // if it queries from tbDomainList, the sql sentence will not be appended "domain".
                                if (regCheckDomain.IsMatch(strProp))
                                {

                                    strSQL = strProp;
                                }
                                else
                                {


                                    if (strProp.Contains("where"))
                                    {
                                        strSQL = strProp + string.Format(" and domain='{0}'", strDomain);
                                    }
                                    else
                                    {
                                        strSQL = strProp + string.Format(" where domain='{0}'", strDomain);
                                    }
                                }
                                // string sql = strProp;
                                if (strSQL.Contains("@domain"))
                                {
                                    strSQL = strSQL.Replace("@domain", strDomain);
                                }
                                else if (strSQL.Contains("@site"))
                                {
                                    strSQL = strSQL.Replace("@site", curSite);
                                }

                                if (strSQL.Contains("*site"))
                                {

                                    string strSQL1 = strSQL.Replace("*site", curSite);
                                    dtPropOption= ExecuteQuery(strSQL1);
                                    if (dtPropOption == null || dtPropOption.Rows.Count == 0)
                                    {
                                        strSQL1 = strSQL.Replace("site in('*site')", "(site='' or site is null)");
                                        dtPropOption=ExecuteQuery(strSQL1 );
                                    }


                                }
                                else
                                {
                                    dtPropOption= ExecuteQuery(strSQL);
                                }
                            }

                            bool hasText = false;
                            bool hasValue = false;
                            foreach (DataColumn column in dtPropOption.Columns)
                            {
                                if (column.ColumnName.ToUpper() == "TEXT")
                                    hasText = true;
                                if (column.ColumnName.ToUpper() == "VALUE")
                                    hasValue = true;
                            }

                            if (dtPropOption.Columns.Count > 1 && hasText && hasValue)
                            {
                                dtPropOption.TableName = row[dataTable.Columns["Name"]].ToString();
                                rowData[4] = dtPropOption;
                            }
                            else
                            {
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
                                    if (rowData[4] != null && Convert.ToString(rowData[4]).Length > 0)
                                        rowData[4] = rowData[4].ToString().Remove(rowData[4].ToString().Length - 1);
                                    else
                                        rowData[4] += "";
                                }
                            }
                        }

                    }
                    else
                    {
                        rowData[4] += strPropertyOption;
                    }

                  

                    //CategoryName-6
                    rowData[5] = row[dataTable.Columns["ModuleName"]].ToString();

                    //FieldType- 7
                    rowData[6] = row[dataTable.Columns["PropertyType"]].ToString();

                    //RegularExpress-8
                    rowData[7] = "";

                    //Description-9
                    rowData[8] = row[dataTable.Columns["PropertyDesc"]].ToString();
                    rowData[9] = "";
                    rowData[10] = row[dataTable.Columns["OrderNo"]].ToString();//default not set data to orderid(not use in system)
                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }
                customedTable.TableName = "SystemProfiles";
                dataSet.Tables.Add(customedTable);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dataSet;
        }
        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public virtual DataSet GetSiteProfileDataSet(string domainName, string siteName)
        {

            DataSet dataSet = new DataSet();
            string strGetSysProfSQL = string.Empty;
            string strCurrentRowName = "";
            try
            {

                //strGetSysProfSQL = string.Format("SELECT [tSystemProfile].[Name],[tSystemProfile].[ModuleID],[tModule].Title as ModuleName,[tSystemProfile].[Value],[tSystemProfile].[Exportable],[tSystemProfile].[PropertyDesc],[tSystemProfile].[PropertyOptions],[tSystemProfile].[Inheritance],[tSystemProfile].[PropertyType],[tSystemProfile].[IsHidden],[tSystemProfile].[OrderingPos]FROM [tSystemProfile], tModule where tModule.ModuleID = [tSystemProfile].[ModuleID] AND (([tSystemProfile].[IsHidden] & 4) = 4) AND [tSystemProfile].[Inheritance] >= 0 and tSystemProfile.Domain='{0}' ORDER BY [tSystemProfile].[OrderingPos]", strDomain);
                strGetSysProfSQL = string.Format("SELECT [tbSiteProfile].[Name],[tbSiteProfile].[ModuleID],[tbModule].Title as ModuleName,[tbSiteProfile].[Value],[tbSiteProfile].[Exportable],[PropertyDesc],[PropertyOptions],[Inheritance],[PropertyType],[IsHidden],[OrderingPos],[OrderNo] FROM [tbSiteProfile], tbModule where tbModule.ModuleID = [tbSiteProfile].[ModuleID] AND (([tbSiteProfile].[IsHidden] & 4) = 4) AND [tbSiteProfile].[Inheritance] >= 0 and tbModule.Domain='{0}' and tbSiteProfile.Domain='{1}' and tbSiteProfile.Site='{2}' ORDER BY [tbSiteProfile].[OrderingPos]", domainName, domainName, siteName);
                DataTable dataTable = ExecuteQuery(strGetSysProfSQL);
                //Build the custom DataTable
                DataTable customedTable = CreataCustomDataTable();

                Regex regCheckDomain = new Regex(@"from\s+tbDomainList", RegexOptions.IgnoreCase); //[^-_\w]domain[^-_\w]
                Regex regCheckSite = new Regex(@"from\s+tbSiteList", RegexOptions.IgnoreCase);

                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                foreach (DataRow row in dataTable.Rows)
                {
                    DataRow rowData = customedTable.NewRow();

                    //Module ID- 0
                    rowData[0] = row[dataTable.Columns["ModuleId"]].ToString();

                    //FieldName - 1
                    rowData[1] = row[dataTable.Columns["Name"]].ToString();
                    strCurrentRowName = row[dataTable.Columns["Name"]].ToString();
                    //FieldValue -2
                    rowData[2] = row[dataTable.Columns["Value"]].ToString();
                    //strValue = row[dataTable.Columns["Value"]].ToString();

                    //FieldDescription -3
                    rowData[3] = "";

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
                                string strSQL = "";
                                // EK_HI00114442
                                // if it queries from tDomainList, the sql sentence will not be appended "domain".
                                if (regCheckDomain.IsMatch(strProp) || regCheckSite.IsMatch(strProp))
                                {
                                    strSQL = strProp;
                                }
                                else
                                {
                                    if (strProp.Contains("where"))
                                    {
                                        strSQL = strProp + string.Format(" and domain='{0}'", domainName);
                                    }
                                    else
                                    {
                                        strSQL = strProp + string.Format(" where domain='{0}'", domainName);
                                    }
                                }
                                // string sql = strProp;
                                if (strSQL.Contains("@domain"))
                                {
                                    strSQL = strSQL.Replace("@domain", domainName);
                                }
                                else if (strSQL.Contains("@site"))
                                {
                                    strSQL = strSQL.Replace("@site", siteName);
                                }
                                dtPropOption=ExecuteQuery(strSQL);
                            }
                            bool hasText = false;
                            bool hasValue = false;
                            foreach (DataColumn column in dtPropOption.Columns)
                            {
                                if (column.ColumnName.ToUpper() == "TEXT")
                                    hasText = true;
                                if (column.ColumnName.ToUpper() == "VALUE")
                                    hasValue = true;
                            }
                            if (dtPropOption.Columns.Count > 1 && hasText && hasValue)
                            {
                                dtPropOption.TableName = row[dataTable.Columns["Name"]].ToString();
                                rowData[4] = dtPropOption;
                            }
                            else
                            {
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
                                        rowData[4] = rowData[4].ToString().Remove(rowData[4].ToString().Length - 1);
                                    else
                                        rowData[4] += "";
                                }
                            }
                        }

                    }
                    else
                    {
                        rowData[4] += strPropertyOption;
                    }

                    //CategoryName-6
                    rowData[5] = row[dataTable.Columns["ModuleName"]].ToString();

                    //FieldType- 7
                    rowData[6] = row[dataTable.Columns["PropertyType"]].ToString();

                    //RegularExpress-8
                    rowData[7] = "";
                    //Description-9
                    rowData[8] = row[dataTable.Columns["PropertyDesc"]].ToString();
                    rowData[9] = "";
                    rowData[10] = row[dataTable.Columns["OrderNo"]].ToString();//default not set data to orderid(not use in system)
                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }
                customedTable.TableName = "SiteProfiles";
                dataSet.Tables.Add(customedTable);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dataSet;
        }
        protected DataTable ExecuteQuery(string szQuery)
        {
            System.Data.DataTable dtbTable = new System.Data.DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(szQuery, conn);
                adapter.Fill(dtbTable);
                return dtbTable;
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception("Database error");
                }
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
        protected object ExecuteScalar(string szQuery)
        {
            object o = null;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _connectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {      
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = szQuery;
                o = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    throw new Exception(ex.Message);
                }
                else
                {
                    throw new Exception("Database error");
                }
            }
            finally
            {
                cmd.Parameters.Clear();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return o;
        }
        public static DataTable CreataCustomDataTable()
        {
            DataTable customedTable = new DataTable();

            //ModuleId 0
            DataColumn idColumn = new DataColumn();
            idColumn.ColumnName = "ModuleId";
            customedTable.Columns.Add(idColumn);

            //FieldName 1
            DataColumn fieldNameColumn = new DataColumn();
            fieldNameColumn.ColumnName = "Name";
            customedTable.Columns.Add(fieldNameColumn);

            //FieldValue 2
            DataColumn fieldValueColumn = new DataColumn();
            fieldValueColumn.ColumnName = "Value";
            customedTable.Columns.Add(fieldValueColumn);

            //FieldDescription 3
            DataColumn fieldDescriptionColumn = new DataColumn();
            fieldDescriptionColumn.ColumnName = "RoleName";
            customedTable.Columns.Add(fieldDescriptionColumn);

            //PropertyOptions  4
            //DataColumn shortcutCodeColumn = new DataColumn();
            //shortcutCodeColumn.ColumnName = "ShortcutCode";
            DataColumn shortcutCodeColumn = new DataColumn("PropertyOptions", typeof(System.Object));
            customedTable.Columns.Add(shortcutCodeColumn);


            //ModuleName 5
            DataColumn categoryNameColumn = new DataColumn();
            categoryNameColumn.ColumnName = "ModuleName";
            customedTable.Columns.Add(categoryNameColumn);

            //FieldType 6
            DataColumn fieldTypeColumn = new DataColumn();
            fieldTypeColumn.ColumnName = "PropertyType";
            customedTable.Columns.Add(fieldTypeColumn);

            //RegularExpress
            DataColumn regularExpressColumn = new DataColumn();
            regularExpressColumn.ColumnName = "RegularExpress";
            customedTable.Columns.Add(regularExpressColumn);

            //Description
            DataColumn descriptionColumn = new DataColumn();
            descriptionColumn.ColumnName = "PropertyDesc";
            customedTable.Columns.Add(descriptionColumn);

            DataColumn defaultDescriptionColumn = new DataColumn();
            defaultDescriptionColumn.ColumnName = "DefaultDescription";
            customedTable.Columns.Add(defaultDescriptionColumn);

            DataColumn orderIDColumn = new DataColumn();
            orderIDColumn.ColumnName = "OrderID";
            customedTable.Columns.Add(orderIDColumn);

            DataColumn siteColumn = new DataColumn();
            siteColumn.ColumnName = "Site";
            customedTable.Columns.Add(siteColumn);
            return customedTable;
        }
        public class PropertyItemType
        {
            public const string TextBox = "0";
            public const string NumericBox = "1";
            public const string DoubleBox = "2";
            public const string ListBox = "3";
            public const string ComboBox = "4";//Edit ListBox
            public const string CheckComboBox = "5";
            public const string ColorDialog = "6";
            public const string FontDialog = "7";
            public const string FileDialog = "8";
            public const string FolderSelectDialog = "9";
            public const string IPAddress = "10";
            public const string CheckBox = "11";
            public const string Date = "12";//e.g. "2006/6/19"
            public const string Time = "13";//e.g. "9:00:00"
            public const string EmailAddress = "14";
            public const string DictionaryValueCheckCombobox = "15";
            public const string ACRCodeDialog = "20";
            public const string UserNameGuidCheckCombobox = "21";
            public const string DateTime = "22";//e.g. "2006/6/19 9:00:00"
        }

        #endregion
    }
}
