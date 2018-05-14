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
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
using DataAccessLayer;
using Server.Utilities.Oam;
using CommonGlobalSettings;
using CommonGlobalSettings;
using LogServer;
using Server.Utilities.LogFacility;
using System.Web;
using Common.ActionResult;
using CommonGlobalSettings.BillBoard;
using Server.ReportDAO;
using ServerCommon = Server.DAO.Common;
using Server.Utilities.HippaLogTool;
using CommonGlobalSettings.HippaName;



namespace Server.DAO.Oam.Impl
{
    public class AbstractDBProvider : IDBProvider
    {
        protected Server.Utilities.LogFacility.LogManagerForServer logger = new Server.Utilities.LogFacility.LogManagerForServer("OAMServerLoglevel", "0800");

        #region IKeyPerformanceRatingDAO

        public void GetRatingPoolList(DataSetActionResult Result)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                string sql = "select distinct Name, StartDate, EndDate from tKeyPerformanceRating order by Name desc";
                DataTable dt = oKodalDAL.ExecuteQuery(sql);
                dt.TableName = "RatingPoolList";
                Result.DataSetData = new DataSet();
                Result.DataSetData.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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
        }

        public void GetUnRatingAppraisers(DataSetActionResult Result)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                DataTable dt = Result.DataSetData.Tables[0];
                string ratingPoolName = Convert.ToString(GetValue("RatingPoolName", dt)).Replace("'", "''");

                string sql = string.Format("select distinct u.LocalName, k.Appraiser from tKeyPerformanceRating k left join tUser u on k.Appraiser = u.UserGuid where Name = '{0}' and Score = ''", ratingPoolName);
                DataTable dt1 = oKodalDAL.ExecuteQuery(sql);
                dt1.TableName = "UnRatingAppraisers";
                Result.DataSetData = new DataSet();
                Result.DataSetData.Tables.Add(dt1);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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
        }

        public void GetRatingPoolInfo(DataSetActionResult Result)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                DataTable dt = Result.DataSetData.Tables[0];
                string ratingPoolName = Convert.ToString(GetValue("RatingPoolName", dt)).Replace("'", "''");

                string sql = string.Format("select k.*, u.LocalName as AppraiseeName,(case when o.OrderMessage IS NULL  then '0' when cast(o.OrderMessage as varchar(max)) = '' then '0' when o.OrderMessage.exist('/LeaveMessage/Message[@Type=\"d\"]') =1 then '1' when cast(o.OrderMessage as varchar(max)) <> '' then '0' end) as IsSelectedCase from tKeyPerformanceRating k left join tUser u on k.Appraisee = u.UserGuid left join tRegOrder o on k.AccNo = o.AccNo where Name = '{0}' order by AppraiseeSequenceId asc, k.AccNo asc, AppraiserSequenceId asc", ratingPoolName);
                DataTable dt1 = oKodalDAL.ExecuteQuery(sql);
                dt1.TableName = "RatingPoolInfo";
                Result.DataSetData = new DataSet();
                Result.DataSetData.Tables.Add(dt1);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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
        }

        public void NewRatingPool(DataSetActionResult Result)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                DataTable dt = Result.DataSetData.Tables[0];
                Random ran;
                StringBuilder sqlInsert = new StringBuilder();

                System.DateTime examStart = Convert.ToDateTime(GetValue("examStartDate", dt));
                System.DateTime examEnd = Convert.ToDateTime(GetValue("examEndDate", dt));
                System.DateTime appraiseStart = Convert.ToDateTime(GetValue("appraisingStartDate", dt));
                System.DateTime appraiseEnd = Convert.ToDateTime(GetValue("appraisingEndDate", dt));
                Int32 count = Convert.ToInt32(GetValue("count", dt));
                string modalityType = Convert.ToString(GetValue("modalityType", dt));
                string ratingPoolName = Convert.ToString(GetValue("name", dt)).Replace("'", "''");
                string configXML = Convert.ToString(GetValue("NewRatingPoolConfig", dt));
                DataTable appraisers = (DataTable)GetValue("appraisers", dt);
                DataTable appraisees = (DataTable)GetValue("appraisees", dt);

                long tick = DateTime.Now.Ticks;
                ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                Dictionary<string, Int32> temp = new Dictionary<string, int>();
                int appraiserOrder = 1;
                DataTable dt1 = appraisers.Copy();
                for (Int32 j = 0; dt1.Rows.Count > 0; j++)
                {
                    Int32 k = ran.Next(0, dt1.Rows.Count);
                    string appraiserGuid = dt1.Rows[k]["GUID"].ToString();
                    if (!temp.ContainsKey(appraiserGuid))
                    {
                        temp.Add(appraiserGuid, appraiserOrder);
                    }
                    appraiserOrder++;
                    dt1.Rows[k].Delete();
                    dt1.AcceptChanges();
                }



                System.DateTime now = System.DateTime.Now;
                int appraiseeOrder = 0;
                string accNos = "";
                foreach (DataRow row in appraisees.Rows)
                {
                    appraiseeOrder++;
                    string appraiseeGuid = row["GUID"].ToString();
                    string sql;
                    if (modalityType == "All")
                    {
                        sql = string.Format("select distinct o.AccNo from tRegOrder o join tRegProcedure p on o.OrderGuid = p.OrderGuid and p.Technician = '{0}' and p.ExamineDt between '{1}' and '{2}' and p.Status = '120'", appraiseeGuid, examStart, examEnd);
                    }
                    else
                    {
                        sql = string.Format("select distinct o.AccNo from tRegOrder o join tRegProcedure p on o.OrderGuid = p.OrderGuid and p.Technician = '{0}' and p.ExamineDt between '{1}' and '{2}' and p.Status = '120' and p.ModalityType = '{3}'", appraiseeGuid, examStart, examEnd, modalityType);
                    }
                    if (!string.IsNullOrWhiteSpace(accNos))
                    {
                        sql = sql + string.Format(" and o.AccNo not in ({0}) ", accNos.Trim(','));
                    }

                    DataTable dataTable = oKodalDAL.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    if (dataTable == null || dataTable.Rows.Count == 0)
                    {
                        continue;
                    }

                    tick = DateTime.Now.Ticks;
                    ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                    for (Int32 i = 0; i < count && dataTable.Rows.Count > 0; i++)
                    {
                        Int32 index = ran.Next(0, dataTable.Rows.Count);
                        string accno = dataTable.Rows[index]["AccNo"].ToString();
                        for (Int32 j = 0; j < appraisers.Rows.Count; j++)
                        {
                            string appraiserGuid = appraisers.Rows[j]["GUID"].ToString();
                            sqlInsert.AppendFormat("insert into tKeyPerformanceRating values(NEWID(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','','','',(select top 1 Value from tSystemProfile where Name = 'Domain'),'{8}') \n",
                                  ratingPoolName, accno, appraiseeGuid, appraiseeOrder, appraiserGuid, temp[appraiserGuid].ToString(), appraiseStart, appraiseEnd, now);
                            accNos = accNos + string.Format("'{0}',", accno);
                        }
                        dataTable.Rows[index].Delete();
                        dataTable.AcceptChanges();
                    }
                }
                if (string.IsNullOrWhiteSpace(sqlInsert.ToString()))
                {
                    throw new Exception("There is no records matching conditions in database, please make sure the conditions are configured properly");
                }
                else
                {
                    oKodalDAL.ExecuteNonQuery(sqlInsert.ToString());

                    string sqlConfig = string.Format("update tSystemProfile set Value = '{0}' where Name = 'NewRatingPoolConfig'", configXML);
                    oKodalDAL.ExecuteNonQuery(sqlConfig);
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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

        }

        public void SetScore(DataSetActionResult Result)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                DataTable dt = Result.DataSetData.Tables[0];
                string guid = Convert.ToString(GetValue("Guid", dt));
                string score = Convert.ToString(GetValue("Score", dt));
                string comment = Convert.ToString(GetValue("RatingComment", dt)).Replace("'", "''");

                string sql0 = string.Format("select * from tKeyPerformanceRating where ID = '{0}'", guid);
                DataTable dt1 = oKodalDAL.ExecuteQuery(sql0);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(dt1.Rows[0]["StartDate"]).Date > DateTime.Today || Convert.ToDateTime(dt1.Rows[0]["EndDate"]).Date < DateTime.Today)
                    {
                        throw new Exception("Today is beyond appraising period");
                    }
                }

                string sql = string.Format("update tKeyPerformanceRating set Score = '{0}' , Comment = '{1}' where ID = '{2}'", score, comment, guid);
                oKodalDAL.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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
        }

        public void CreateTeaching(DataSetActionResult Result)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                DataTable dt = Result.DataSetData.Tables[0];
                string accNo = Convert.ToString(GetValue("AccNo", dt));
                string ratingComment = Convert.ToString(GetValue("RatingComment", dt));
                string userGuid = Convert.ToString(GetValue("UserGuid", dt));
                string userName = Convert.ToString(GetValue("UserName", dt));
                string subject = "";

                string sql = string.Format("select * from tRegOrder where AccNo = '{0}' and OrderMessage.exist('//LeaveMessage/Message[@Type=\"d\"]') = 1", accNo);
                DataTable temp = oKodalDAL.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                if (temp != null && temp.Rows.Count > 0)
                {
                    throw new Exception("the flag with type = d already exists");
                }
                else
                {
                    sql = string.Format("select * from tRegOrder where AccNo = '{0}'", accNo);
                    temp = oKodalDAL.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    if (temp == null || temp.Rows.Count == 0)
                    {
                        throw new Exception("cannot find this examination");
                    }
                }

                sql = "select Text from tDictionaryValue where Tag = 93 and ShortcutCode = 'd'";
                temp = oKodalDAL.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                if (temp != null && temp.Rows.Count > 0)
                {
                    subject = temp.Rows[0][0].ToString();
                }

                oKodalDAL.Parameters.AddVarChar("AccNo", accNo);
                oKodalDAL.Parameters.AddVarChar("Type", "d");
                oKodalDAL.Parameters.AddVarChar("UserGuid", userGuid);
                oKodalDAL.Parameters.AddVarChar("UserName", userName);
                oKodalDAL.Parameters.AddVarChar("Subject", subject);
                oKodalDAL.Parameters.AddVarChar("Context", ratingComment);
                oKodalDAL.Parameters.AddVarChar("ErrorMessage", 256, ParameterDirection.Output);

                oKodalDAL.ExecuteNonQuerySP("usp_Insert_OrderMessage");

                if (!string.IsNullOrWhiteSpace(oKodalDAL.Parameters["ErrorMessage"].Value.ToString()))
                {
                    throw new Exception(oKodalDAL.Parameters["ErrorMessage"].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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
        }

        private object GetValue(string key, DataTable source)
        {
            object obj = null;
            DataRow[] rows = source.Select(string.Format("key = '{0}'", key));
            if (rows != null && rows.Length > 0)
            {
                obj = rows[0]["value"];
            }

            return obj;
        }

        #endregion

        #region IRandomInspectionDAO
        public void NewRandomInspectionPool(DataSetActionResult Result)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                DataTable dt = Result.DataSetData.Tables[0];
                Random ran;
                StringBuilder sqlInsert = new StringBuilder();

                System.DateTime examStart = Convert.ToDateTime(GetValue("examStartDate", dt));
                System.DateTime examEnd = Convert.ToDateTime(GetValue("examEndDate", dt));
                System.DateTime appraiseStart = Convert.ToDateTime(GetValue("appraisingStartDate", dt));
                System.DateTime appraiseEnd = Convert.ToDateTime(GetValue("appraisingEndDate", dt));
                Int32 count = Convert.ToInt32(GetValue("count", dt));
                string modalityType = Convert.ToString(GetValue("modalityType", dt));
                string patientType = Convert.ToString(GetValue("patientType", dt));
                string ratingPoolName = Convert.ToString(GetValue("name", dt)).Replace("'", "''");
                string configXML = Convert.ToString(GetValue("NewRandomInspectionPoolConfig", dt));
                DataTable imageDoctorAppraisers = (DataTable)GetValue("imageDoctorAppraisers", dt);
                DataTable imageTechnicianAppraisers = (DataTable)GetValue("imageTechnicianAppraisers", dt);
                DataTable reportDoctorAppraisers = (DataTable)GetValue("reportDoctorAppraisers", dt);
                DataTable reportTechnicianAppraisers = (DataTable)GetValue("reportTechnicianAppraisers", dt);
                long tick = DateTime.Now.Ticks;
                ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                Dictionary<string, Int32> dcImageAppraiser = new Dictionary<string, int>();
                Dictionary<string, Int32> dcReportAppraiser = new Dictionary<string, int>();
                #region imageAppraiser and reportAppraiser
                int appraiserOrder = 1;
                DataTable dt1 = imageDoctorAppraisers.Copy();
                for (Int32 j = 0; dt1.Rows.Count > 0; j++)
                {
                    Int32 k = ran.Next(0, dt1.Rows.Count);
                    string appraiserGuid = dt1.Rows[k]["GUID"].ToString();
                    if (!dcImageAppraiser.ContainsKey(appraiserGuid))
                    {
                        dcImageAppraiser.Add(appraiserGuid, appraiserOrder);
                    }
                    appraiserOrder++;
                    dt1.Rows[k].Delete();
                    dt1.AcceptChanges();
                }

                DataTable dt2 = imageTechnicianAppraisers.Copy();
                for (Int32 j = 0; dt2.Rows.Count > 0; j++)
                {
                    Int32 k = ran.Next(0, dt2.Rows.Count);
                    string appraiserGuid = dt2.Rows[k]["GUID"].ToString();
                    if (!dcImageAppraiser.ContainsKey(appraiserGuid))
                    {
                        dcImageAppraiser.Add(appraiserGuid, appraiserOrder);
                    }
                    appraiserOrder++;
                    dt2.Rows[k].Delete();
                    dt2.AcceptChanges();
                }

                appraiserOrder = 1;
                DataTable dt3 = reportDoctorAppraisers.Copy();
                for (Int32 j = 0; dt3.Rows.Count > 0; j++)
                {
                    Int32 k = ran.Next(0, dt3.Rows.Count);
                    string appraiserGuid = dt3.Rows[k]["GUID"].ToString();
                    if (!dcReportAppraiser.ContainsKey(appraiserGuid))
                    {
                        dcReportAppraiser.Add(appraiserGuid, appraiserOrder);
                    }
                    appraiserOrder++;
                    dt3.Rows[k].Delete();
                    dt3.AcceptChanges();
                }

                DataTable dt4 = reportTechnicianAppraisers.Copy();
                for (Int32 j = 0; dt4.Rows.Count > 0; j++)
                {
                    Int32 k = ran.Next(0, dt4.Rows.Count);
                    string appraiserGuid = dt4.Rows[k]["GUID"].ToString();
                    if (!dcReportAppraiser.ContainsKey(appraiserGuid))
                    {
                        dcReportAppraiser.Add(appraiserGuid, appraiserOrder);
                    }
                    appraiserOrder++;
                    dt4.Rows[k].Delete();
                    dt4.AcceptChanges();
                }
                #endregion
                System.DateTime now = System.DateTime.Now;
                int appraiseeOrder = 0;
                string accNos = "";
                string sql = string.Format("select distinct AccNo from tRandomInspection where (examStartDate between '{0}' and '{1}') or (examEndDate between '{0}' and '{1}')  ", examStart.ToShortDateString(), examEnd.ToShortDateString());
                DataTable dtAccNo = oKodalDAL.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                if (dtAccNo != null && dtAccNo.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAccNo.Rows.Count; i++)
                    {
                        if (i == dtAccNo.Rows.Count - 1)
                        {
                            accNos += "'" + dtAccNo.Rows[i]["AccNo"].ToString() + "'";
                        }
                        else
                        {
                            accNos += "'" + dtAccNo.Rows[i]["AccNo"].ToString() + "',";
                        }
                    }
                }
                string reportGuids = "";
                foreach (var item in dcReportAppraiser)
                {
                    reportGuids = reportGuids + "'" + item.Key + "',";
                }
                reportGuids = reportGuids.TrimEnd(',');
                string imageGuids = "";
                foreach (var item in dcImageAppraiser)
                {
                    imageGuids = imageGuids + "'" + item.Key + "',";
                }
                imageGuids = imageGuids.TrimEnd(',');

                if (modalityType == "All")
                {
                    sql = string.Format("select  distinct top  {0} tRegOrder.AccNo FROM tRegOrder,tRegProcedure   left join tReport on tReport.ReportGuid = tRegProcedure.ReportGuid where tRegOrder.OrderGuid=tRegProcedure.OrderGuid and tRegProcedure.Status >=120 and tRegProcedure.ExamineDt between '{1}' and '{2}' ", count, examStart, examEnd);
                    //sql = string.Format("select tRegOrder.AccNo,tReport.ReportGuid,tReport.submitter,tRegProcedure.Technician FROM tRegOrder,tRegProcedure  left join tReport on tReport.ReportGuid = tRegProcedure.ReportGuid where tRegOrder.OrderGuid=tRegProcedure.OrderGuid and accno in (select tregorder. AccNo from tRegOrder where orderguid in(select distinct top {0} orderguid from tRegProcedure where  tRegProcedure.Status >=120 and tRegProcedure.ExamineDt between '{1}' and '{2}'))", count, examStart, examEnd);
                    //sql = string.Format("select distinct top {0} tRegOrder.AccNo,tReport.ReportGuid,tReport.submitter,tRegProcedure.Technician FROM tRegOrder,tRegProcedure  left join tReport on tReport.ReportGuid = tRegProcedure.ReportGuid where tRegOrder.OrderGuid=tRegProcedure.OrderGuid and tRegProcedure.Status >=120 and tRegProcedure.ExamineDt between '{1}' and '{2}' ", count, examStart, examEnd);
                }
                else
                {
                    sql = string.Format("select  distinct top  {0} tRegOrder.AccNo FROM tRegOrder,tRegProcedure  left join tReport on tReport.ReportGuid = tRegProcedure.ReportGuid where tRegOrder.OrderGuid=tRegProcedure.OrderGuid and tRegProcedure.Status >=120 and tRegProcedure.ModalityType = '{3}'", count, examStart, examEnd, modalityType);
                    //sql = string.Format("select tRegOrder.AccNo,tReport.ReportGuid,tReport.submitter,tRegProcedure.Technician FROM tRegOrder,tRegProcedure  left join tReport on tReport.ReportGuid = tRegProcedure.ReportGuid where tRegOrder.OrderGuid=tRegProcedure.OrderGuid and accno in (select tregorder. AccNo from tRegOrder where orderguid in(select distinct top {0} orderguid from tRegProcedure where  tRegProcedure.Status >=120 and tRegProcedure.ExamineDt between '{1}' and '{2}'))  and tRegProcedure.ModalityType = '{3}'", count, examStart, examEnd, modalityType);
                    //sql = string.Format("select distinct top {0} tRegOrder.AccNo,tReport.ReportGuid,tReport.submitter,tRegProcedure.Technician FROM tRegOrder,tRegProcedure  left join tReport on tReport.ReportGuid = tRegProcedure.ReportGuid where tRegOrder.OrderGuid=tRegProcedure.OrderGuid and tRegProcedure.Status >=120 and tRegProcedure.ExamineDt between '{1}' and '{2}' and tRegProcedure.ModalityType = '{3}'", count, examStart, examEnd, modalityType);
                }
                if (!string.IsNullOrWhiteSpace(patientType))
                {
                    sql = sql + string.Format("and tRegOrder.patientType in({0}) ", patientType);
                }
                if (!string.IsNullOrWhiteSpace(accNos))
                {
                    sql = sql + string.Format(" and tRegOrder.AccNo not in ({0}) ", accNos.Trim(','));
                }
                if (!string.IsNullOrWhiteSpace(imageGuids))
                {
                    sql = sql + string.Format(" and tRegProcedure.Technician not in ({0}) ", imageGuids);
                }
                if (!string.IsNullOrWhiteSpace(reportGuids))
                {
                    sql = sql + string.Format(" and tReport.submitter not in ({0}) ", reportGuids);
                }
                string sql1 = "select tRegOrder.AccNo,tReport.ReportGuid,tReport.submitter,tRegProcedure.Technician FROM tRegOrder,tRegProcedure  left join tReport on tReport.ReportGuid = tRegProcedure.ReportGuid where tRegOrder.OrderGuid=tRegProcedure.OrderGuid and accno in(";
                sql = sql1 + sql + ")";
                //sql = sql + "order by newid()";
                DateTime time = DateTime.Now;
                DataTable dataTable = oKodalDAL.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                TimeSpan ts = DateTime.Now - time;
                int consume = ts.Seconds;
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Sql Search", 1, sql, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Search Time", 1, consume.ToString(), Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                tick = DateTime.Now.Ticks;
                //ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                Dictionary<int, string> dicAccNo = new Dictionary<int, string>();
                for (Int32 i = 0; i < dataTable.Rows.Count; i++)
                {
                    //Int32 index = ran.Next(0, dataTable.Rows.Count);
                    Int32 index = i;
                    string accno = dataTable.Rows[index]["AccNo"].ToString();
                    if (dicAccNo.ContainsValue(accno) == true)
                    {
                        //dataTable.Rows[index].Delete();
                        //dataTable.AcceptChanges();
                        continue;
                    }
                    else
                    {
                        dicAccNo.Add(i, accno);
                        string technician = dataTable.Rows[index]["Technician"].ToString();
                        string submitter = dataTable.Rows[index]["submitter"].ToString();
                        string reportGuid = dataTable.Rows[index]["ReportGuid"].ToString();
                        foreach (var item in dcImageAppraiser)
                        {
                            appraiseeOrder++;
                            string appraiserGuid = item.Key;
                            int order = item.Value;
                            sqlInsert.AppendFormat("insert into tRandomInspection([ID],[Name],[AccNo],[Appraisee],[AppraiseeSequenceId],[Appraiser],[AppraiserSequenceId],[StartDate],[EndDate],[Domain],[CreateDate],[Type],[examStartDate],[examEndDate]) values(NEWID(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',(select top 1 Value from tSystemProfile where Name = 'Domain'),'{8}',1,'{9}','{10}') \n",
                                  ratingPoolName, accno, technician, appraiseeOrder, appraiserGuid, order, appraiseStart, appraiseEnd, now, examStart, examEnd);

                        }
                        appraiseeOrder = 0;
                        foreach (var item in dcReportAppraiser)
                        {
                            appraiseeOrder++;
                            string appraiserGuid = item.Key;
                            int order = item.Value;
                            sqlInsert.AppendFormat("insert into tRandomInspection([ID],[Name],[AccNo],[Appraisee],[AppraiseeSequenceId],[Appraiser],[AppraiserSequenceId],[StartDate],[EndDate],[Domain],[CreateDate],[Type],[ReportGuid],[examStartDate],[examEndDate]) values(NEWID(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',(select top 1 Value from tSystemProfile where Name = 'Domain'),'{8}',2,'{9}','{10}','{11}') \n",
                                    ratingPoolName, accno, submitter, appraiseeOrder, appraiserGuid, order, appraiseStart, appraiseEnd, now, reportGuid, examStart, examEnd);
                        }
                    }
                    //dataTable.Rows[index].Delete();
                    //dataTable.AcceptChanges();
                }

                if (string.IsNullOrWhiteSpace(sqlInsert.ToString()))
                {
                    throw new Exception("There is no records matching conditions in database, please make sure the conditions are configured properly");
                }
                else
                {
                    time = DateTime.Now;
                    oKodalDAL.ExecuteNonQuery(sqlInsert.ToString());
                    ts = DateTime.Now - time;
                    consume = ts.Seconds;
                    logger.Error((long)ModuleEnum.Oam_DA, "Oam Sql Insert", 1, sqlInsert.ToString(), Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                        (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                    logger.Error((long)ModuleEnum.Oam_DA, "Oam Insert Time", 1, consume.ToString(), Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                        (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                    string sqlConfig = string.Format("update tSystemProfile set Value = '{0}' where Name = 'NewRandomInspectionConfig'", configXML);
                    oKodalDAL.ExecuteNonQuery(sqlConfig);
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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

        }

        public void GetRandomInspectionPoolList(DataSetActionResult Result)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                string sql = "select distinct Name, StartDate, EndDate from tRandomInspection order by Name desc";
                DataTable dt = oKodalDAL.ExecuteQuery(sql);
                dt.TableName = "RatingPoolList";
                Result.DataSetData = new DataSet();
                Result.DataSetData.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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
        }

        public void GetRandomInspectionPoolListByUser(DataSetActionResult Result)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                DataTable dt = Result.DataSetData.Tables[0];
                string userGuid = Convert.ToString(GetValue("userGuid", dt));
                string sql = string.Format("select distinct Name, StartDate, EndDate from tRandomInspection  where appraiser='{0}' or appraisee='{0}' order by Name desc", userGuid);
                dt = oKodalDAL.ExecuteQuery(sql);
                dt.TableName = "RatingPoolList";
                Result.DataSetData = new DataSet();
                Result.DataSetData.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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
        }

        public void GetRandomInspectionPoolInfo(DataSetActionResult Result)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                DataTable dt = Result.DataSetData.Tables[0];
                string selectedPoolName = Convert.ToString(GetValue("RandomInspectionPoolName", dt)).Replace("'", "''");

                string sql = string.Format("Select  * from tRandomInspection where Name = '{0}' and type=1 order by newid()", selectedPoolName);
                DataTable dt1 = oKodalDAL.ExecuteQuery(sql);
                dt1.TableName = "ImageInfo";
                Result.DataSetData = new DataSet();
                Result.DataSetData.Tables.Add(dt1);

                sql = string.Format("Select  * from tRandomInspection where Name = '{0}' and type=2 order by newid()", selectedPoolName);
                DataTable dt2 = oKodalDAL.ExecuteQuery(sql);
                dt2.TableName = "ReportInfo";
                Result.DataSetData.Tables.Add(dt2);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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
        }

        public string DeleteInspectionPool(string strParam)
        {
            RisDAL oKodak = new RisDAL();
            string message = "";
            try
            {
                string selectedPoolName = CommonGlobalSettings.Utilities.GetParameter("selectedPoolName", strParam);
                selectedPoolName = selectedPoolName.Replace("'", "''");
                string sql = string.Format("select count(*)  as Num from tRandomInspection where name='{0}' and Grade is not Null", selectedPoolName);

                DataTable dt = oKodak.ExecuteQuery(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 0)
                    {
                        message = "Already Rating, Can't be deleted ";
                    }
                    else
                    {
                        sql = string.Format("delete from tRandomInspection where name='{0}'", selectedPoolName);
                        oKodak.ExecuteNonQuery(sql);
                        message = "Has been deleted successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return message;
        }

        public void InspectionSetScore(DataSetActionResult Result)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                DataTable dt = Result.DataSetData.Tables[0];

                string guid = Convert.ToString(dt.Rows[0]["ID"]);
                int score = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Score"]));
                string comment = Convert.ToString(dt.Rows[0]["Comment"]);
                string grade = Convert.ToString(dt.Rows[0]["Grade"]);
                string type = Convert.ToString(dt.Rows[0]["Type"]);
                string result = Convert.ToString(dt.Rows[0]["Result"]);
                string appraiser = Convert.ToString(dt.Rows[0]["Appraiser"]);
                string indexs = Convert.ToString(dt.Rows[0]["Indexs"]);
                string accordRate = "";
                if (type == "2")
                {
                    accordRate = Convert.ToString(dt.Rows[0]["AccordRate"]);
                }
                string sql0 = string.Format("select * from tRandomInspection where ID = '{0}'", guid);
                DataTable dt1 = oKodalDAL.ExecuteQuery(sql0);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(dt1.Rows[0]["StartDate"]).Date > DateTime.Today || Convert.ToDateTime(dt1.Rows[0]["EndDate"]).Date < DateTime.Today)
                    {
                        throw new Exception("Today is beyond appraising period");
                    }
                }
                string sql = string.Format("update tRandomInspection set Result='{0}', Grade='{1}',Score = {2} , Comment = '{3}', Indexs='{4}',AccordRate='{5}' where ID = '{6}'", result, grade, score, comment, indexs, accordRate, guid);
                oKodalDAL.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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
        }

        public string GetScoreCardSettings(string strParam, ref string version)
        {
            RisDAL oKodak = new RisDAL();
            try
            {
                string Site = CommonGlobalSettings.Utilities.GetParameter("Site", strParam);
                string Type = CommonGlobalSettings.Utilities.GetParameter("Type", strParam);
                string Version = CommonGlobalSettings.Utilities.GetParameter("Version", strParam);
                string From = CommonGlobalSettings.Utilities.GetParameter("From", strParam);
                string sql = string.Format("select Settings,VersionNo from tInspectionScoreSettings where VersionNo='{0}'", Version);
                if (string.IsNullOrWhiteSpace(Version))
                {
                    if (From == "ForUse")
                    {
                        sql = string.Format("select Settings,VersionNo from tInspectionScoreSettings where IsCurrent=1 and (Site='{0}' or Site='') and Type='{1}' order by Site desc", Site, Type);
                    }
                    else
                    {
                        sql = string.Format("select Settings,VersionNo from tInspectionScoreSettings where IsCurrent=1 and Site='{0}' and Type='{1}'", Site, Type);
                    }
                }
                DataTable dt = oKodak.ExecuteQuery(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    version = dt.Rows[0][1].ToString();
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return "";
        }
        #endregion

        #region ILoginSettingsDAO


        public string FullShowLoginBackground(ref string strSite)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                string domain = ConfigurationManager.AppSettings["Domain"];
                string site = ConfigurationManager.AppSettings["Site"];
                strSite = site;
                string strSQL = string.Format("select * from tSiteProfile where Name = 'FullShowLoginBackground' and Site='{0}'", site);
                DataTable dataTable = oKodalDAL.ExecuteQuery(strSQL);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0]["Value"].ToString();
                }
                else
                {
                    strSQL = string.Format("select * from tSystemProfile where Name = 'FullShowLoginBackground' and Domain='{0}'", domain);
                    dataTable = oKodalDAL.ExecuteQuery(strSQL);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        return dataTable.Rows[0]["Value"].ToString();
                    }
                }

                return "0";
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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

        }

        public string GetXML(string site)
        {
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                string domain = ConfigurationManager.AppSettings["Domain"];
                string strSQL = string.Format("select * from tSiteProfile where Name = 'login_settings' and Site='{0}'", site);
                DataTable dataTable = oKodalDAL.ExecuteQuery(strSQL);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0]["Value"].ToString();
                }
                else
                {
                    strSQL = string.Format("select * from tSystemProfile where Name = 'login_settings' and Domain='{0}'", domain);
                    dataTable = oKodalDAL.ExecuteQuery(strSQL);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        return dataTable.Rows[0]["Value"].ToString();
                    }
                }

                string xml = @"<?xml version='1.0' encoding='gb2312'?>
                         <login>
                          <updateDateTime>2012/8/10</updateDateTime>
                          <IsNew>1</IsNew>
                          <title>Carestream RIS GC</title>
                          <font>Microsoft Sans Serif</font>
                          <fontStyle>0</fontStyle>
                          <color>-1</color>
                          <logo>Carestream.jpg</logo>
                          <isRegular>0</isRegular>
                          <regularNum>1</regularNum>
                          <regularUnit>days</regularUnit>
                          <random>0</random>
                          <pictures default='' previous='' current='' next='' times='0'>
                          </pictures>
                        </login>";
                return xml;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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
        }

        public bool WriteXML(string site, string xml)
        {
            xml = xml.Replace("'", "''");
            RisDAL oKodalDAL = new RisDAL();
            try
            {
                string strSQL = string.Format("select 1 from tSiteProfile where Name = 'login_settings' and Site='{0}'", site);
                DataTable dataTable = oKodalDAL.ExecuteQuery(strSQL);
                int i = 0;
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    strSQL = string.Format("update tSiteProfile set Value = '{0}' where Name = 'login_settings' and Site='{1}'", xml, site);
                    i = oKodalDAL.ExecuteNonQuery(strSQL);
                }
                else
                {
                    strSQL = string.Format("insert into tSiteProfile values('login_settings','0000', '{0}', 1,'login_settings','',0,0,0,122224,(select top 1 Value from tSystemProfile where Name = 'Domain' ),NEWID(),'{1}')", xml, site);
                    i = oKodalDAL.ExecuteNonQuery(strSQL);
                }
                if (i > 0)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
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
        }

        public bool isWeekBeginFromMonday()
        {
            RisDAL dataAccess = new RisDAL();

            try
            {
                string sql = "select value from tSystemProfile where Name = 'WeekBeginFromMonday'";
                int isWeekBeginFromMonday = Convert.ToInt32(dataAccess.ExecuteScalar(sql));
                if (isWeekBeginFromMonday == 1)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
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

        #endregion

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



        public virtual DataSet GetRoleDataSet(string strDomain)
        {

            RisDAL oKodakDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            string sql = string.Format("SELECT distinct RoleName,Description,IsSystem,RoleID from tRole where Domain='{0}'", strDomain);
            try
            {
                DataTable dataTable = oKodakDAL.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                //Build the custom DataTabe
                DataTable customedTable = Server.Utilities.Oam.Utilities.CreataCustomRoleDataTable();

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

        /// <summary>
        /// Name:AddRole
        /// Function:Insert the role details in the tRole and tRoleProfile table
        /// Input: roleName -- Role name ; description -- Role Description
        /// Output:DataSet holding the existing role in the database
        /// Return:true if Success, false if fail
        /// </summary>
        /// <param name="roleName">Role name to be inserted</param>
        /// <param name="description">Role name description of the newly added role to be inserted</param>
        /// <returns>true or false</returns>
        public virtual int AddRole(string strRoleName, string strRoleDescription, string strDomain, string parentId)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataTable dtSystemProfileDetails = null;
            try
            {
                List<string> listSQL = new List<string>();
                //Check if the role or Description already exists. If yes then throw the exception

                if (IsRoleNameAreadyExists(strRoleName, oKodakDAL, strDomain) || IsRoleDescriptionAreadyExists(strRoleDescription, oKodakDAL, strDomain))
                {
                    return 1;
                }
                else
                {
                    //Copy the system profile to role profile
                    string site = GetRoleSite2(parentId, strDomain);
                    InsertRoleAndProfileDetails(strRoleName, "", strRoleDescription, listSQL, false, oKodakDAL, dtSystemProfileDetails, site, strDomain);

                    AddLeafRole(strRoleName, parentId, strDomain, oKodakDAL);
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

        public virtual DataSet GetRoleProfDetDataSet(string roleName, string strDomain, string userGuid, bool isSiteAdmin)
        {
            RisDAL oKodalDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            //DataTable dtSystemProf = new DataTable();
            string sql = SQLGetDictionaryList;
            string strGetRoleProfSQL = string.Empty;
            string strGetSysProfSQL = string.Empty;
            try
            {
                strGetRoleProfSQL = string.Format("SELECT [RoleName],[Name],[tRoleProfile].[ModuleID],Title as ModuleName,[Value],[Exportable],[PropertyDesc],[PropertyOptions],[Inheritance],[PropertyType],[IsHidden],[OrderingPos] FROM [dbo].[tRoleProfile], dbo.tModule where tModule.ModuleID = [tRoleProfile].[ModuleID] AND [tRoleProfile].[RoleName] = '{0}' and ([tRoleProfile].[IsHidden] & 2) = 2 and tModule.Domain='{1}'  and tRoleProfile.Domain='{2}' ORDER BY [tRoleProfile].[OrderingPos]", roleName.Trim(), strDomain, strDomain);
                DataTable dataTable = oKodalDAL.ExecuteQuery(strGetRoleProfSQL, RisDAL.ConnectionState.KeepOpen);
                string site = GetRoleSite(roleName, strDomain);
                //Build the custom DataTable
                DataTable customedTable = Server.Utilities.Oam.Utilities.CreataCustomDataTable();

                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                string[] arrDftVal = null;
                foreach (DataRow row in dataTable.Rows)
                {
                    DataRow rowData = customedTable.NewRow();

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
                                    string strSQLBelongToSite = string.Format("select Value from tUserProfile where UserGuid = '{0}' and Name = 'BelongToSite'", userGuid);
                                    string belongToSite = oKodalDAL.ExecuteScalar(strSQLBelongToSite).ToString();
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

                                oKodalDAL.ExecuteQuery(strSQL, dtPropOption);
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


                    //DefaultValue -5
                    if (rowData[2].ToString().Contains("|"))
                    {
                        arrDftVal = rowData[2].ToString().Split('|');
                        foreach (string strDftVal in arrDftVal)
                        {
                            //DefaultValue -5
                            rowData[5] += strDftVal + ",";
                        }
                        rowData[5] = rowData[5].ToString().TrimEnd(',');
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
                    rowData[11] = "";//default not set data to orderid(not use in system)
                    rowData[12] = site;

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

        public string GetRoleSite(string roleName, string strDomain)
        {
            RisDAL oKodakDAL = new RisDAL();
            string site = "", nodeName = "", parentId = "";
            try
            {
                DataTable dt;
                parentId = oKodakDAL.ExecuteScalar(string.Format("select ParentID from tRoleDir where Name ='{0}' and Domain ='{1}' and Leaf = 1", roleName, strDomain), RisDAL.ConnectionState.KeepOpen) as string;
                dt = oKodakDAL.ExecuteQuery(string.Format("select Name,ParentID from tRoleDir where UniqueID ='{0}'", parentId), RisDAL.ConnectionState.KeepOpen);

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
                    dt = oKodakDAL.ExecuteQuery(string.Format("select Name,ParentID from tRoleDir where UniqueID ='{0}'", parentId), RisDAL.ConnectionState.KeepOpen);
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return site;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return site;

        }

        public string GetRoleSite2(string parentGUID, string strDomain)
        {
            RisDAL oKodakDAL = new RisDAL();
            string site = "", nodeName = "", parentId = "";
            try
            {
                DataTable dt;
                parentId = parentGUID;
                dt = oKodakDAL.ExecuteQuery(string.Format("select Name,ParentID from tRoleDir where UniqueID ='{0}'", parentId), RisDAL.ConnectionState.KeepOpen);

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
                    dt = oKodakDAL.ExecuteQuery(string.Format("select Name,ParentID from tRoleDir where UniqueID ='{0}'", parentId), RisDAL.ConnectionState.KeepOpen);
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                     (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
                return site;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return site;

        }

        public virtual bool EditRole(RoleModel model, string strDomain)
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
                        strBuilder.AppendFormat("UPDATE dbo.tRoleProfile set Value = '{0}' where RoleName = '{1}' and Name = '{2}' and ModuleId = '{3}' and Domain='{4}'",
                            row["FieldValue"].ToString(), row["Id"].ToString(),
                            row["FieldName"].ToString(), row["FieldDescription"].ToString(),
                            strDomain);
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

        /// <summary>
        /// The DeleteRole method is used to delete the role details from the tRole and tRoleProfile table.
        /// and also delete it from the tRoleDir table.
        /// </summary>
        /// <param name="strRoleName">Role name that needs to be deleted</param>
        /// <returns></returns>
        public virtual int DeleteRole(string strRoleName, string strDomain)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //Defect #: EK_HI00040188
                string strRoleExistVldSQL = string.Format("select top 1 RoleName from tRole where RoleName= '{0}' and Domain='{1}'", strRoleName.ToString(), strDomain);
                string strRoleExist = Convert.ToString(oKodakDAL.ExecuteScalar(strRoleExistVldSQL));
                if (strRoleExist == string.Empty)
                {
                    //The role don't exist in the table(tRole).There cannot delete.
                    return 2;
                }

                List<string> listSQL = new List<string>();
                //Check if the user exists for the role to needs to be deleted. If yes then throw the exception
                string strUserExistVldSQL = string.Format("SELECT TOP 1 RoleName from tRole2User where RoleName= '{0}' and Domain='{1}'", strRoleName.ToString(), strDomain);
                string strUserExist = Convert.ToString(oKodakDAL.ExecuteScalar(strUserExistVldSQL));
                StringBuilder strBuilder = new StringBuilder();
                if (strUserExist != string.Empty)
                {
                    //user exists for the role. Therefore cannot delete
                    return 1;
                }
                else
                {
                    strBuilder.AppendFormat(@"Delete from tRoleDir where RoleId in 
                               (select RoleId from tRole where RoleName='{0}' and Domain='{1}')", strRoleName.ToString(), strDomain);

                    strBuilder.AppendFormat("DELETE FROM tRoleProfile where RoleName = '{0}' and Domain='{1}'", strRoleName.ToString(), strDomain);
                    listSQL.Add(strBuilder.ToString());
                    strBuilder.Remove(0, strBuilder.Length);
                    strBuilder.AppendFormat("DELETE FROM tRole where RoleName = '{0}' and Domain='{1}'", strRoleName.ToString(), strDomain);
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

        /// <summary>
        /// The CopyRole method is used is to Copy the existing role and create a new role
        /// </summary>
        /// <param name="strOldRoleName">Old Role name that needs to be copied</param>
        /// <param name="strNewRoleName">New Role name that will be created</param>
        /// <param name="strNewRoleDescription">New Role name Description</param>
        /// <returns></returns>
        public virtual int CopyRole(string strNewRoleName, string strNewRoleDescription, string strOldRoleName, string strDomain, string strParentId)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataTable dtRoleProfileDetails = null;
            try
            {
                List<string> listSQL = new List<string>();
                //Check if the role already exists. If yes then throw the exception

                if (IsRoleNameAreadyExists(strNewRoleName, oKodakDAL, strDomain))
                {
                    return 1;
                }
                else
                {
                    string site = GetRoleSite2(strParentId, strDomain);
                    InsertRoleAndProfileDetails(strOldRoleName, strNewRoleName, strNewRoleDescription, listSQL, true, oKodakDAL, dtRoleProfileDetails, site, strDomain);
                    AddLeafRole(strNewRoleName, strParentId, strDomain, oKodakDAL);
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


        /// <summary>
        /// GetRoleDir with Role Info
        /// </summary>
        /// <param name="strIsAdmin"></param>
        /// <param name="currentUserGUID"></param>
        /// <param name="strDomain"></param>
        /// <returns></returns>
        /// 
        public virtual DataSet GetRoleDir(string strDomain)
        {
            DataSet ds = new DataSet();
            try
            {
                using (RisDAL oKodakDAL = new RisDAL())
                {
                    string strSql = string.Format(@"select distinct tRoleDir.*,tRole. RoleName,tRole.Description,tRole.IsSystem 
                        from tRoleDir left join tRole on tRole.RoleID =tRoleDir.RoleID where tRoleDir.Domain='{0}' order by Name", strDomain);
                    DataTable dt = oKodakDAL.ExecuteQuery(strSql);
                    dt.TableName = "RoleDir";
                    ds.Tables.Add(dt);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw ex;
            }
        }

        public virtual void AddRoleNode(RoleNodeModel roleNode)
        {
            try
            {

                using (RisDAL oKodakDAL = new RisDAL())
                {
                    string strSqlExists = "select 1 from tRoleDir where Name=@name";
                    oKodakDAL.Parameters.AddVarChar("@Name", roleNode.Name);
                    oKodakDAL.Parameters.AddVarChar("@ParentID", roleNode.ParentId);
                    oKodakDAL.Parameters.AddInt("@Leaf", roleNode.Leaf);

                    object obj = oKodakDAL.ExecuteScalar(strSqlExists);
                    if (Convert.ToString(obj) != string.Empty)
                    {
                        throw new Exception("Existed Name");
                    }

                    string strAddRoleNode = "insert into tRoleDir(UniqueID,Name,ParentID,Leaf,OrderID,RoleID,Domain) Values(@UniqueID,@Name,@ParentID,@Leaf,@OrderId,@RoleID,@Domain)";
                    oKodakDAL.Parameters.Clear();
                    oKodakDAL.Parameters.AddVarChar("@UniqueID", roleNode.UniqueId);
                    oKodakDAL.Parameters.AddVarChar("@Name", roleNode.Name);
                    oKodakDAL.Parameters.AddVarChar("@ParentID", roleNode.ParentId);
                    oKodakDAL.Parameters.AddInt("@orderId", roleNode.OrderId);
                    oKodakDAL.Parameters.AddInt("@Leaf", roleNode.Leaf ? 1 : 0);
                    oKodakDAL.Parameters.AddVarChar("@RoleId", roleNode.RoleID);
                    oKodakDAL.Parameters.AddVarChar("@Domain", roleNode.Domain);

                    oKodakDAL.ExecuteNonQuery(strAddRoleNode);


                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw ex;
            }
        }

        public virtual void DeleteRoleNode(string nodeId)
        {
            try
            {
                using (RisDAL oKodakDAL = new RisDAL())
                {
                    string strHasChild = "select 1 from tRoleDir where ParentId =@NodeID";
                    oKodakDAL.Parameters.AddVarChar("@NodeID", nodeId);
                    object obj = oKodakDAL.ExecuteScalar(strHasChild);
                    if (Convert.ToString(obj) != string.Empty)
                    {
                        throw new Exception("NonEmptyNode");
                    }

                    string strDelete = "Delete from tRoleDir where UniqueId=@UniqueId";
                    oKodakDAL.Parameters.AddVarChar("@UniqueId", nodeId);
                    oKodakDAL.ExecuteNonQuery(strDelete);
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw ex;
            }
        }

        public void UpdateRoleNode(string nodeName, string parentId, string uniqueId)
        {
            try
            {
                using (RisDAL oKodakDAL = new RisDAL())
                {
                    string strExists = "select 1 from tRoleDir where Name=@name and UniqueID <> @UniqueID";
                    oKodakDAL.Parameters.AddVarChar("@name", nodeName);
                    oKodakDAL.Parameters.AddVarChar("@UniqueID", uniqueId);

                    object obj = oKodakDAL.ExecuteScalar(strExists);
                    if (Convert.ToString(obj) != string.Empty)
                    {
                        throw new Exception("Existed Name");
                    }

                    string strUpdate = "Update tRoleDir set Name=@name,ParentID=@ParentID where UniqueID=@UniqueID";
                    oKodakDAL.Parameters.Clear();
                    oKodakDAL.Parameters.AddVarChar("@name", nodeName);
                    oKodakDAL.Parameters.AddVarChar("@ParentID", parentId);
                    oKodakDAL.Parameters.AddVarChar("@UniqueID", uniqueId);

                    oKodakDAL.ExecuteNonQuery(strUpdate);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private void AddLeafRole(string roleName, string parentId, string doamin, RisDAL oKodakDAL)
        {
            string strAddLeafRole = "insert into tRoleDir(Name,ParentID,Leaf,OrderID,RoleID,Domain) Values(@Name,@ParentID,@Leaf,@OrderId,(select RoleId from tRole where RoleName=@RoleName and Domain=@Domain),@Domain)";
            oKodakDAL.Parameters.Clear();
            oKodakDAL.Parameters.AddVarChar("@Name", roleName);
            oKodakDAL.Parameters.AddVarChar("@ParentID", parentId);
            oKodakDAL.Parameters.AddVarChar("@RoleName", roleName);
            oKodakDAL.Parameters.AddInt("@orderId", 0);
            oKodakDAL.Parameters.AddInt("@Leaf", 1);
            oKodakDAL.Parameters.AddVarChar("@Domain", doamin);

            oKodakDAL.ExecuteNonQuery(strAddLeafRole);
        }

        #endregion

        #region IUserDAO Section

        public virtual DataSet LoadUserDataSet(string strIsAdmin, string currentUserGUID, string strDomain, string strSite)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            string SQLAllUser = string.Empty;
            try
            {

                if (strIsAdmin == "A")
                {

                    SQLAllUser = string.Format("SELECT A.UserGuid,A.LoginName,A.LocalName,A.DisplayName,A.EnglishName,A.Password,A.Title,A.Address,A.Comments,A.SignImage,A.PrivateKey,A.PublicKey,A.IkeySn," +
                                   "B.[Domain],B.Department,A.Deletemark,B.DomainLoginName,B.Telephone,B.Mobile,B.Email,B.IsSetExpireDate,cast(B.IsSetExpireDate as varchar) as SetExpireDate,B.StartDate,B.EndDate,C.Value as BelongToSite,C.Value as BelongToSitetext,cast(D.IsActive as nvarchar) as CertEnabled, A.IsLocked " +
                                   "FROM tUser A inner join tUser2Domain B on A.UserGuid=B.UserGuid and " +
                                   "B.Domain='{0}' and A.DeleteMark = 0 Left join tuserprofile C on A.UserGuid=c.UserGuid Left join tusercerts D on A.UserGuid = D.UserGuid and D.IsActive = 1 where c.Name='Belongtosite'", strDomain);
                }
                else if (strIsAdmin == "S")
                {
                    SQLAllUser = string.Format("SELECT A.UserGuid,A.LoginName,A.LocalName,A.DisplayName,A.EnglishName,A.Password,A.Title,A.Address,A.Comments,A.SignImage,A.PrivateKey,A.PublicKey,A.IkeySn," +
                                   "B.[Domain],B.Department,A.Deletemark,B.DomainLoginName,B.Telephone,B.Mobile,B.Email,B.IsSetExpireDate,cast(B.IsSetExpireDate as varchar) as SetExpireDate,B.StartDate,B.EndDate ,C.Value as BelongToSite,C.Value as BelongToSitetext,cast(D.IsActive as nvarchar) as CertEnabled, A.IsLocked  " +
                                   "FROM tUser A inner join tUser2Domain B on A.UserGuid=B.UserGuid and " +
                                   "B.Domain='{0}' and A.DeleteMark = 0 Left join tuserprofile C on A.UserGuid=c.UserGuid  Left join tusercerts D on A.UserGuid = D.UserGuid and D.IsActive = 1 where c.Name='Belongtosite'", strDomain);

                }
                else
                {
                    SQLAllUser = string.Format("SELECT A.UserGuid,A.LoginName,A.LocalName,A.DisplayName,A.EnglishName,A.Password,A.Title,A.Address,A.Comments,A.SignImage,A.PrivateKey,A.PublicKey,A.IkeySn," +
                                "B.[Domain],B.Department,A.Deletemark,B.DomainLoginName,B.Telephone,B.Mobile,B.Email,B.IsSetExpireDate,cast(B.IsSetExpireDate as varchar) as SetExpireDate,B.StartDate,B.EndDate ,C.Value as BelongToSite,C.Value as BelongToSitetext,cast(D.IsActive as nvarchar) as CertEnabled, A.IsLocked  " +
                                "FROM tUser A inner join tUser2Domain B on A.UserGuid=B.UserGuid and " +
                                " B.Domain='{0}' and A.DeleteMark = 0 and A.UserGuid='{1}'  Left join tuserprofile C on A.UserGuid=c.UserGuid  Left join tusercerts D on A.UserGuid = D.UserGuid and D.IsActive = 1 where c.Name='Belongtosite'", strDomain, currentUserGUID.ToString());

                }


                DataTable dtAllUser = oKodakDAL.ExecuteQuery(SQLAllUser, RisDAL.ConnectionState.KeepOpen);
                dtAllUser.TableName = "User";
                dataSet.Tables.Add(dtAllUser);



                string SQLAllRole = string.Format("select * from tRole where Domain='{0}'", strDomain);
                DataTable dtAllRole = oKodakDAL.ExecuteQuery(SQLAllRole, RisDAL.ConnectionState.KeepOpen);
                dtAllRole.TableName = "Role";
                dataSet.Tables.Add(dtAllRole);

                SQLAllRole = "select * from dbo.tRole ";
                DataTable dtAllRole1 = oKodakDAL.ExecuteQuery(SQLAllRole, RisDAL.ConnectionState.KeepOpen);
                dtAllRole1.TableName = "AllRole";
                dataSet.Tables.Add(dtAllRole1);



                string SQLAllDepartment = string.Format("select * from tDictionaryValue where (Tag = 2 and Domain='{0}') and ((Tag not in (select distinct Tag from tDictionaryValue where Site='{1}') and (Site='' or Site is null)) or Site='{1}') ORDER BY tag,orderid,text", strDomain, CommonGlobalSettings.Utilities.GetCurSite());
                DataTable dtAllDepartment = oKodakDAL.ExecuteQuery(SQLAllDepartment, RisDAL.ConnectionState.KeepOpen);
                dtAllDepartment.TableName = "department";
                dataSet.Tables.Add(dtAllDepartment);

                SQLAllDepartment = string.Format("select * from tDictionaryValue where Tag = 2 and ((Tag not in (select distinct Tag from tDictionaryValue where Site='{0}') and (Site='' or Site is null)) or Site='{0}') ORDER BY tag,orderid,text", CommonGlobalSettings.Utilities.GetCurSite());
                DataTable dtAllDepartment1 = oKodakDAL.ExecuteQuery(SQLAllDepartment, RisDAL.ConnectionState.KeepOpen);
                dtAllDepartment1.TableName = "alldepartment";
                dataSet.Tables.Add(dtAllDepartment1);


                string SQLAllTitle = string.Format("select * from tDictionaryValue where Tag = 7 and Domain='{0}' ORDER BY tag,orderid,text", strDomain);
                DataTable dtAllTitle = oKodakDAL.ExecuteQuery(SQLAllTitle, RisDAL.ConnectionState.KeepOpen);
                dtAllTitle.TableName = "Title";
                dataSet.Tables.Add(dtAllTitle);

                SQLAllTitle = string.Format("select * from tDictionaryValue where Tag = 7  ORDER BY tag,orderid,text", strDomain);
                DataTable dtAllTitle1 = oKodakDAL.ExecuteQuery(SQLAllTitle, RisDAL.ConnectionState.KeepOpen);
                dtAllTitle1.TableName = "allTitle";
                dataSet.Tables.Add(dtAllTitle1);


                //KodakDAL okodak = new KodakDAL();
                string SQLAllOnlineUsers = string.Format("select UserGuid from tOnlineClient where IsOnline = 1");
                DataTable dtAllOnlineUsers;
                using (var okodak = new RisDAL())
                {
                    dtAllOnlineUsers = okodak.ExecuteQuery(SQLAllOnlineUsers);
                }
                dtAllOnlineUsers.TableName = "Online";


                DataTable dtSelectedForUser = new DataTable();
                DataRow drSelectedForUser = null;

                dtSelectedForUser.Columns.Add("LoginName", typeof(string));
                dtSelectedForUser.Columns.Add("LocalName", typeof(string));
                dtSelectedForUser.Columns.Add("EnglishName", typeof(string));
                dtSelectedForUser.Columns.Add("UserRole", typeof(string));
                dtSelectedForUser.Columns.Add("Department", typeof(string));
                //dtSelectedForUser.Columns.Add("Title", typeof(string));
                dtSelectedForUser.Columns.Add("Telephone", typeof(string));
                dtSelectedForUser.Columns.Add("Mobile", typeof(string));
                dtSelectedForUser.Columns.Add("Email", typeof(string));
                dtSelectedForUser.Columns.Add("DomainLoginName", typeof(string));
                dtSelectedForUser.Columns.Add("OnlineUser", typeof(string));
                dtSelectedForUser.Columns.Add("UserGuid", typeof(string));

                dtSelectedForUser.Columns.Add("IsSetExpireDate", typeof(string));
                dtSelectedForUser.Columns.Add("SetExpireDate", typeof(string));
                dtSelectedForUser.Columns.Add("StartDate", typeof(DateTime));
                dtSelectedForUser.Columns.Add("EndDate", typeof(DateTime));
                dtSelectedForUser.Columns.Add("SignImage", typeof(object));
                dtSelectedForUser.Columns.Add("Domain", typeof(object));

                if (dtAllUser != null && dtAllUser.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAllUser.Rows)
                    {
                        drSelectedForUser = dtSelectedForUser.NewRow();
                        drSelectedForUser["UserGuid"] = dr["UserGuid"];

                        string SQLAllRoleForUser = string.Format("select RoleName from tRole2User where UserGuid = '{0}' and Domain='{1}'", dr["UserGuid"].ToString(), strDomain);
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

                        DataRow[] arrDtDepartment = dtAllDepartment.Select("Value = '" + dr["Department"].ToString() + "'");
                        if (arrDtDepartment.Length > 0)
                        {
                            drSelectedForUser["Department"] = arrDtDepartment[0]["Text"];
                        }
                        else
                        {
                            drSelectedForUser["Department"] = "";
                        }



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

                        drSelectedForUser["IsSetExpireDate"] = dr["IsSetExpireDate"];
                        if (dr["SetExpireDate"] != DBNull.Value)
                        {
                            if (Convert.ToInt32(dr["SetExpireDate"]) == 0)
                            {
                                drSelectedForUser["SetExpireDate"] = "No";
                                dr["StartDate"] = DBNull.Value;
                                dr["EndDate"] = DBNull.Value;
                            }
                            else
                            {
                                drSelectedForUser["SetExpireDate"] = "Yes";
                            }
                        }
                        else
                        {
                            drSelectedForUser["SetExpireDate"] = "No";
                            dr["StartDate"] = DBNull.Value;
                            dr["EndDate"] = DBNull.Value;
                        }


                        drSelectedForUser["StartDate"] = dr["StartDate"];
                        drSelectedForUser["EndDate"] = dr["EndDate"];
                        drSelectedForUser["SignImage"] = dr["SignImage"];
                        drSelectedForUser["Domain"] = dr["Domain"];
                        //#endregion RC-00001 jameswei 2007-11-07
                        dtSelectedForUser.Rows.Add(drSelectedForUser);


                    }
                    dtSelectedForUser.TableName = "SelectUser";
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

        public virtual DataSet GetUserProfDetDataSet(string userGuid, string strDomain, string strSite)
        {
            RisDAL oKodalDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            //DataTable dtSystemProf = new DataTable();
            string strGetUserProfSQL = string.Empty;
            string strGetUserRoleSQL = string.Empty;
            try
            {
                strGetUserProfSQL = string.Format(@"
select A.Name,A.ModuleId,B.Title as ModuleName,A.UserGuid,A.RoleName,A.Value,A.Exportable,
    A.PropertyDesc,A.PropertyOptions,A.Inheritance,A.PropertyType,A.IsHidden,A.OrderingPos 
from tUserProfile A, tModule B 
where A.ModuleId = B.ModuleId and 
    ((A.IsHidden & 1) = 1) and A.UserGuid = '{0}' and 
    A.Domain='{1}' ORDER BY A.OrderingPos", userGuid.ToString(), strDomain);
                DataTable dataTable = oKodalDAL.ExecuteQuery(strGetUserProfSQL);
                //Build the custom DataTable
                DataTable customedTable = Server.Utilities.Oam.Utilities.CreataCustomDataTable();

                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                string[] arrDftVal = null;
                foreach (DataRow row in dataTable.Rows)
                {
                    DataRow rowData = customedTable.NewRow();

                    //role name - 0
                    rowData[0] = row[dataTable.Columns["ModuleId"]].ToString();

                    //FieldName - 1
                    rowData[1] = row[dataTable.Columns["Name"]].ToString();

                    //FieldValue -2
                    rowData[2] = row[dataTable.Columns["Value"]].ToString();
                    //strValue = row[dataTable.Columns["Value"]].ToString();

                    // Sub Category Module Name -3
                    rowData[3] = row[dataTable.Columns["RoleName"]].ToString();

                    //ShorCut -4 -- Property Options
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

                                //if (strName.ToUpper() == "BELONGTOSITE" && !string.IsNullOrWhiteSpace(strSite))
                                //{
                                //    if (strProp.Contains("where"))
                                //    {
                                //        strSQL = strProp + string.Format(" and domain='{0}' and site='{1}'", strDomain,strSite);
                                //    }
                                //    else
                                //    {
                                //        strSQL = strProp + string.Format(" where domain='{0}' and site='{1}'", strDomain,strSite);
                                //    }
                                //}
                                //else
                                //{
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
                                // }
                                oKodalDAL.ExecuteQuery(strSQL, dtPropOption);
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
                                        rowData[4] = "";
                                }
                            }
                        }

                    }
                    else
                    {
                        rowData[4] += strPropertyOption;
                    }

                    //DefaultValue -5
                    if (rowData[2].ToString().Contains("|"))
                    {
                        arrDftVal = rowData[2].ToString().Split('|');
                        foreach (string strDftVal in arrDftVal)
                        {
                            //DefaultValue -5
                            rowData[5] += strDftVal + ",";
                        }
                        rowData[5] = rowData[5].ToString().Remove(rowData[5].ToString().Length - 1);
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
                    rowData[11] = "";//default not set data to orderid(not use in system)
                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }

                customedTable.TableName = "UserProfile";
                dataSet.Tables.Add(customedTable);

                strGetUserRoleSQL = string.Format("select RoleName from tRole2User where UserGuid = '{0}' and Domain='{1}'", userGuid.ToString(), strDomain);
                DataTable dtUserRole = oKodalDAL.ExecuteQuery(strGetUserRoleSQL);
                dtUserRole.TableName = "UserRole";
                dataSet.Tables.Add(dtUserRole);

                strGetUserRoleSQL = string.Format("select department from tUser2Domain where userguid = '{0}' and domain = '{1}'", userGuid.ToString(), strDomain);
                DataTable dtUserDomain = oKodalDAL.ExecuteQuery(strGetUserRoleSQL);
                dtUserDomain.TableName = "UserDomain";
                dataSet.Tables.Add(dtUserDomain);
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


        public virtual int AddUser(UserModel model, string strSite)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string[] arrRoleName = null;
                List<string> listSQL = new List<string>();
                StringBuilder strBuilder = new StringBuilder();
                string userGuid = model.UserGuid;
                if (IsUserNameAlreadyExists(model.LoginName, ref userGuid, model.LoginNameChanged, false, oKodakDAL))
                {
                    return 1;
                }
                else if (IsDomainLoginNameAreadyExists(model.DomainLoginName, ref userGuid, model.DomainLoginNameChanged, false, oKodakDAL))
                {
                    return 2;
                }
                else if (IsUserNameAlreadyExists(model.LoginName, ref userGuid, model.LoginNameChanged, true, oKodakDAL))
                {
                    if (!model.RecoverDeletedUser)
                    {
                        return 4;
                    }
                }
                else if (IsDomainLoginNameAreadyExists(model.DomainLoginName, ref userGuid, model.DomainLoginNameChanged, true, oKodakDAL))
                {
                    if (!model.RecoverDeletedUser)
                    {
                        return 6;
                    }
                }
                else if (IsDisplayNameAlreadyExists(model.DisplayName, ref userGuid, model.DisplayNameChanged, false, oKodakDAL))
                {
                    if (!model.AddSameDisplayNameUser)
                    {
                        return 3;
                    }
                }

                DataAccessLayer.MyCryptography c = new DataAccessLayer.MyCryptography("GCRIS2-20061025");
                #region add new user or recover a deleted user
                {
                    Guid UserGuid = Guid.NewGuid();
                    model.UserGuid = model.RecoverDeletedUser ? userGuid : UserGuid.ToString();//user deleted user's guid or new guid
                    if (!model.RecoverDeletedUser)//new user
                    {
                        //prepare the SQL for inserting the user details in the tUser table

                        strBuilder.AppendFormat("insert into tUser(UserGuid,LoginName,LocalName,DisplayName,EnglishName,Password,Title,Address,Comments,deletemark,domain,IsLocked) values ('{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',N'{8}',0,N'{9}',{10})",
                        model.UserGuid,
                        model.LoginName,
                        model.DisplayName,
                        model.DisplayName,
                        model.EnglishName,
                        c.Encrypt(model.Password),
                        model.Title,
                        model.Address,
                        model.Comments,
                        model.Domain,
                        model.IsLocked ? 1 : 0
                        );
                        listSQL.Add(strBuilder.ToString());
                        strBuilder.Remove(0, strBuilder.Length);

                        if (model.AddSameDisplayNameUser)//user want to add a same active displayname user,should update to localname(loginname).
                        {
                            strBuilder.Clear();
                            strBuilder.AppendFormat("update tUser set LocalName = DisplayName + '('+ LoginName + ')' where DisplayName = '{0}' and deletemark = 0", model.DisplayName);
                        }

                #endregion
                    }
                    else//recover same domain/loginname user
                    {
                        strBuilder.AppendFormat("Update tUser set deleteMark = 0 where UserGuid ='{0}'", model.UserGuid);
                        listSQL.Add(strBuilder.ToString());
                        strBuilder.Remove(0, strBuilder.Length);

                        //if exists same displayname in active user,should update to localname(loginname).
                        string sqlQueryDisplayName = string.Format("select displayname from tuser where userguid ='{0}'", model.UserGuid);
                        string deletedDisplayName = model.DisplayName;
                        string temp = "";
                        DataTable dtDisplayName = new DataTable();
                        oKodakDAL.ExecuteQuery(sqlQueryDisplayName, dtDisplayName);
                        if (dtDisplayName != null && dtDisplayName.Rows.Count > 0)
                        {
                            deletedDisplayName = Convert.ToString(dtDisplayName.Rows[0]["DisplayName"]);//because of the displayname perhaps not same,so to get the delted displayname
                        }
                        if (IsDisplayNameAlreadyExists(deletedDisplayName, ref temp, model.DisplayNameChanged, false, oKodakDAL))
                        {
                            strBuilder.Clear();
                            strBuilder.AppendFormat("update tUser set LocalName = DisplayName + 'N('+ LoginName + ')' where DisplayName = '{0}'", deletedDisplayName);
                        }
                        else
                        {
                            strBuilder.Clear();
                            strBuilder.AppendFormat("update tUser set LocalName = DisplayName  where DisplayName = N'{0}'", deletedDisplayName);//recover it!

                        }
                    }



                    if (model.IsSetExpireDate == 1)
                    {
                        strBuilder.AppendFormat("INSERT INTO tUser2Domain(UserGuid,Department,DomainLoginName,Telephone,Mobile,Email,IsSetExpireDate,StartDate,EndDate,Domain) values ('{0}',N'{1}',N'{2}',N'{3}','{4}','{5}',{6},'{7}','{8}','{9}')",
                            model.UserGuid,
                            model.Department,
                            model.DomainLoginName,
                            model.Telephone,
                            model.Mobile,
                            model.Email,
                            model.IsSetExpireDate,
                            model.StartDate.ToString("yyyy.MM.dd"),
                            model.EndDate.ToString("yyyy.MM.dd"),
                            model.Domain
                            );
                    }
                    else
                    {
                        strBuilder.AppendFormat("INSERT INTO tUser2Domain(UserGuid,Department,DomainLoginName,Telephone,Mobile,Email,IsSetExpireDate,Domain) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}')",
                            model.UserGuid,
                            model.Department,
                            model.DomainLoginName,
                            model.Telephone,
                            model.Mobile,
                            model.Email,
                            model.IsSetExpireDate,
                            model.Domain
                            );
                    }
                    listSQL.Add(strBuilder.ToString());
                    strBuilder.Remove(0, strBuilder.Length);


                    //prepare the SQL for inserting the user and role mapping in the tRole2User

                    arrRoleName = model.RoleName.Split(',');

                    if (arrRoleName != null && arrRoleName.Length > 0)
                    {
                        foreach (string strRoleName in arrRoleName)
                        {
                            strBuilder.AppendFormat("insert into tRole2User(RoleName,UserGuid,Domain) values ('{0}','{1}','{2}')", strRoleName.ToString().Trim(), model.UserGuid, model.Domain);
                            listSQL.Add(strBuilder.ToString());
                            strBuilder.Remove(0, strBuilder.Length);
                        }
                    }

                    //Prepare the SQL for inserting the user profile details in the tUserProfile table

                    string SQLRoleProfile = string.Format("select * from tRoleProfile where inheritance > 0 and (RoleName ='' or RoleName is null)  and Domain='{0}'", model.Domain);
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
                            string strName = Convert.ToString(drRoleProfileDetails["Name"]).Trim();
                            strBuilder.AppendFormat("INSERT INTO tUserProfile(Name,ModuleID,RoleName,UserGuid,[Value],Exportable,PropertyDesc,PropertyOptions,Inheritance,PropertyType,IsHidden,OrderingPos,[Domain]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                             strName,
                             Convert.ToString(drRoleProfileDetails["ModuleId"]).Trim(),
                             Convert.ToString(drRoleProfileDetails["RoleName"]).Trim(),
                             model.UserGuid,
                             (strName.ToUpper() == "BELONGTOSITE") ? strSite : Convert.ToString(drRoleProfileDetails["Value"]),
                             Convert.ToInt32(drRoleProfileDetails["Exportable"]),
                             Convert.ToString(drRoleProfileDetails["PropertyDesc"]).Trim(),
                             strPropertyOption,
                             iInheritance,
                             Convert.ToInt16(drRoleProfileDetails["PropertyType"]),
                             1,//Convert.ToInt16(drRoleProfileDetails["IsHidden"]),
                             Convert.ToInt32(drRoleProfileDetails["OrderingPos"]),
                             model.Domain);

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


                    if (model.Sign != null)
                    {
                        try
                        {
                            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(oKodakDAL.ConnectionString))
                            {
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                                cmd.CommandTimeout = 0;
                                cmd.Connection = conn;
                                cmd.CommandText = string.Format("update tUser set SignImage=@file where UserGuid='{0}'", model.UserGuid);
                                cmd.Parameters.AddWithValue("@file", model.Sign);
                                //spFile.Value = bytes;                        
                                //cmd.Parameters.Add(spFile);
                                cmd.ExecuteNonQuery();
                            }

                        }
                        catch
                        {

                            throw new Exception("Database error");
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception("Database error!");
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

        public virtual int ModifyUser(UserModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string[] arrRoleName = null;
                string[] arrAddNewRoleProfile = null;
                string[] arrDeletOldRoleProfile = null;
                List<string> listSQL = new List<string>();
                StringBuilder strBuilder = new StringBuilder();
                string userguid = model.UserGuid;
                if (IsUserNameAlreadyExists(model.LoginName, ref userguid, model.LoginNameChanged, false, oKodakDAL))
                {
                    return 1;
                }
                else if (IsDomainLoginNameAreadyExists(model.DomainLoginName, ref userguid, model.DomainLoginNameChanged, false, oKodakDAL))
                {
                    return 2;
                }
                else if (IsUserNameAlreadyExists(model.LoginName, ref userguid, model.LoginNameChanged, true, oKodakDAL))
                {
                    return 4;
                }
                else if (IsDomainLoginNameAreadyExists(model.DomainLoginName, ref userguid, model.DomainLoginNameChanged, true, oKodakDAL))
                {
                    return 6;
                }
                else if (IsDisplayNameAlreadyExists(model.DisplayName, ref userguid, model.DisplayNameChanged, false, oKodakDAL))
                {
                    if (!model.AddSameDisplayNameUser)
                    {
                        return 3;
                    }
                }

                #region Modified by Blue for RC603.2 - US16932, 06/03/2014
                string sql = string.Format("select IsLocked from tUser where UserGuid = '{0}'", model.UserGuid);
                int orginalIsLocked = Convert.ToInt32(oKodakDAL.ExecuteScalar(sql));
                if (orginalIsLocked == 1 && !model.IsLocked)
                {
                    listSQL.Add(string.Format("update tUser set InvalidLoginCount = 0 where UserGuid = '{0}'", model.UserGuid));
                }
                #endregion

                DataAccessLayer.MyCryptography c = new DataAccessLayer.MyCryptography("GCRIS2-20061025");
                //prepare the SQL for updating the user details in the tUser table
                if (model.ChangedPasswordChecked == true)
                {

                    strBuilder.AppendFormat("Update tUser set LoginName=N'{0}',LocalName=N'{1}',DisplayName=N'{2}',EnglishName=N'{3}',Password='{4}',Title='{5}',Address='{6}',Comments='{7}',IsLocked={9} where UserGuid='{8}'",
                    model.LoginName,
                    model.DisplayName,
                    model.DisplayName,
                    model.EnglishName,
                    c.Encrypt(model.Password),
                    model.Title,
                    model.Address,
                    model.Comments,
                    userguid,
                    model.IsLocked ? 1 : 0
                    );
                    listSQL.Add(strBuilder.ToString());
                    strBuilder.Remove(0, strBuilder.Length);
                }
                else
                {
                    strBuilder.AppendFormat("Update tUser set LoginName=N'{0}',LocalName=N'{1}',DisplayName=N'{2}',EnglishName=N'{3}',Title='{4}',Address='{5}',Comments='{6}',IsLocked={8} where UserGuid='{7}'",
                    model.LoginName,
                    model.DisplayName,
                    model.DisplayName,
                    model.EnglishName,
                    model.Title,
                    model.Address,
                    model.Comments,
                    userguid,
                    model.IsLocked ? 1 : 0
                    );
                    listSQL.Add(strBuilder.ToString());
                    strBuilder.Remove(0, strBuilder.Length);
                }

                //if exists same displayname in active user,should update to localname(loginname).
                string userGuid = model.UserGuid;
                if (model.AddSameDisplayNameUser ||//user modify to other same displaynameuser
                    IsDisplayNameAlreadyExists(model.DisplayName, ref userguid, true, false, oKodakDAL))//user save current duplictated user
                {
                    strBuilder.Clear();
                    strBuilder.AppendFormat("update tUser set LocalName = DisplayName + 'N('+ LoginName + ')' where DisplayName = '{0}' ", model.DisplayName);
                    listSQL.Add(strBuilder.ToString());
                }
                else
                {
                    strBuilder.Clear();
                    strBuilder.AppendFormat("update tUser set LocalName = DisplayName  where DisplayName = '{0}'", model.DisplayName);//recover it!
                    listSQL.Add(strBuilder.ToString());
                }

                if (model.DisplayNameChanged)//whether the original displayname should use localname(loginname) format.
                {
                    string[] nameArray = model.LocalName.Split("(".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    string originalDisplayName = "";
                    if (nameArray.Length > 1)//combined name
                    {
                        originalDisplayName = nameArray[0];
                        string sqlQueryHasDuplicatedDisplayNameOnline = string.Format("select count(1) from tuser where displayname = '{0}' and userguid != '{1}' and deletemark = 0 ", originalDisplayName, userguid);
                        int count = Convert.ToInt32(oKodakDAL.ExecuteScalar(sqlQueryHasDuplicatedDisplayNameOnline));
                        if (count == 1)//no dulicated original displayname,so set to localname = displayname
                        {
                            strBuilder.Clear();
                            strBuilder.AppendFormat("update tUser set LocalName = DisplayName  where DisplayName = '{0}' and deletemark = 0 ", originalDisplayName);//recover it!
                            listSQL.Add(strBuilder.ToString());
                        }
                    }
                }

                strBuilder.Clear();
                if (model.IsSetExpireDate == 1)
                {
                    strBuilder.AppendFormat("if not exists(select * from tUser2Domain where UserGuid='{0}' and Domain='{1}') \r\n", model.UserGuid, model.Domain)
                        .AppendFormat("INSERT INTO tUser2Domain(UserGuid,Department,DomainLoginName,Telephone,Mobile,Email,IsSetExpireDate,StartDate,EndDate,Domain) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','8','9') \r\n",
                                model.UserGuid,
                                model.Department,
                                model.DomainLoginName,
                                model.Telephone,
                                model.Mobile,
                                model.Email,
                                model.IsSetExpireDate,
                                model.StartDate.ToString("yyyy.MM.dd"),
                                model.EndDate.ToString("yyyy.MM.dd"),
                                model.Domain
                                )
                        .Append(" else \r\n")
                        .AppendFormat("Update tUser2Domain set Department='{0}',DomainLoginName='{1}',Telephone='{2}',IsSetExpireDate={3},StartDate='{4}',EndDate='{5}',Mobile='{6}',Email='{7}' where UserGuid='{8}' and  Domain='{9}'",
                        model.Department,
                        model.DomainLoginName,
                        model.Telephone,
                        model.IsSetExpireDate,
                        model.StartDate.ToString("yyyy.MM.dd"),
                        model.EndDate.ToString("yyyy.MM.dd"),
                        model.Mobile,
                        model.Email,
                        userguid,
                        model.Domain
                        );
                }
                else
                {
                    strBuilder.AppendFormat("if not exists(select * from tUser2Domain where UserGuid='{0}' and Domain='{1}') \r\n", model.UserGuid, model.Domain)
                                .AppendFormat("INSERT INTO .tUser2Domain(UserGuid,Department,DomainLoginName,Telephone,Mobile,Email,IsSetExpireDate,Domain) values ('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}') \r\n",
                                model.UserGuid,
                                model.Department,
                                model.DomainLoginName,
                                model.Telephone,
                                model.Mobile,
                                model.Email,
                                model.IsSetExpireDate,
                                model.Domain
                                )
                            .Append(" else \r\n")
                            .AppendFormat("Update tUser2Domain set Department='{0}',DomainLoginName='{1}',Telephone='{2}',Mobile='{3}',Email='{4}',IsSetExpireDate={5} where UserGuid='{6}' and  Domain='{7}'",
                            model.Department,
                            model.DomainLoginName,
                            model.Telephone,
                            model.Mobile,
                            model.Email,
                            model.IsSetExpireDate,
                            userguid,
                            model.Domain
                            );
                }
                listSQL.Add(strBuilder.ToString());
                strBuilder.Remove(0, strBuilder.Length);




                //prepare the SQL for deleting and then inserting the user and role mapping in the tRole2User
                strBuilder.AppendFormat("Delete from tRole2User where UserGuid = '{0}' and Domain='{1}'", model.UserGuid.ToString(), model.Domain);
                listSQL.Add(strBuilder.ToString());
                strBuilder.Remove(0, strBuilder.Length);
                arrRoleName = model.RoleName.Split(',');
                if (arrRoleName != null && arrRoleName.Length > 0)
                {
                    foreach (string strRoleName in arrRoleName)
                    {
                        strBuilder.AppendFormat("insert into tRole2User(RoleName,UserGuid,Domain) values ('{0}','{1}','{2}')", strRoleName.ToString().Trim(), model.UserGuid.ToString(), model.Domain);
                        listSQL.Add(strBuilder.ToString());
                        strBuilder.Remove(0, strBuilder.Length);
                    }
                }



                /*no need to insert into userprofile when modify user!
                arrAddNewRoleProfile = model.AddNewRole.Split(',');
                if (arrAddNewRoleProfile != null && arrAddNewRoleProfile.Length > 0)
                {
                    foreach (string strAddNewRoleProfile in arrAddNewRoleProfile)
                    {
                        //Prepare the SQL for inserting the user profile details in the tUserProfile table
                        string SQLRoleProfile = string.Format("select * from tRoleProfile where inheritance > 0 and RoleName = ('{0}') and Domain='{1}'", strAddNewRoleProfile.ToString(), model.Domain);
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
                                 model.Domain);

                                listSQL.Add(strBuilder.ToString());
                                strBuilder.Remove(0, strBuilder.Length);

                            }

                        }

                        //strBuilder.AppendFormat("insert into tRole2User values ('{0}','{1}')", strRoleName.ToString().Trim(), model.UserGuid.ToString());
                        // listSQL.Add(strBuilder.ToString());
                        //strBuilder.Remove(0, strBuilder.Length);
                    }
                }
                */
                //defect EK_HI00075665
                //arrDeletOldRoleProfile = model.DeleteOldRole.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //if (arrDeletOldRoleProfile != null && arrDeletOldRoleProfile.Length > 0)
                //{
                //    foreach (string strDeletOldRoleProfile in arrDeletOldRoleProfile)
                //    {
                //        strBuilder.AppendFormat("DELETE FROM tUserProfile where RoleName = '{0}' and UserGuid = '{1}' and Domain='{2}'", strDeletOldRoleProfile.ToString().Trim(), model.UserGuid.ToString(), model.Domain);
                //        listSQL.Add(strBuilder.ToString());
                //        strBuilder.Remove(0, strBuilder.Length);
                //    }
                //}

                //Prepare the SQL for updating the user profile details in the tUserProfile table 
                if (model.SaveUserProfile.Tables != null)
                {
                    if (model.SaveUserProfile.Tables.Contains("UserProfile"))
                    {
                        foreach (DataRow row in model.SaveUserProfile.Tables["UserProfile"].Rows)
                        {
                            strBuilder.AppendFormat("UPDATE tUserProfile set Value = '{0}' where Name = '{1}' and UserGuid = '{2}'  and Domain='{3}'", row["FieldValue"].ToString(), row["FieldName"].ToString(), model.UserGuid, model.Domain);
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

                if (model.Sign != null)
                {
                    try
                    {
                        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(oKodakDAL.ConnectionString))
                        {
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.CommandTimeout = 0;
                            cmd.Connection = conn;
                            if (model.Sign.ToString().ToUpper() == "DELSIGN")
                            {
                                cmd.CommandText = string.Format("update tUser set SignImage=null where UserGuid='{0}'", model.UserGuid);

                            }
                            else
                            {
                                cmd.CommandText = string.Format("update tUser set SignImage=@file where UserGuid='{0}'", model.UserGuid);
                                cmd.Parameters.AddWithValue("@file", model.Sign);

                            }
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        throw new Exception("Database error");
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception("Database error!");
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

        public virtual int ModifyUserIKeySN(UserModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string userguid = model.UserGuid;
                string iKeySn = model.iKeySN;
                string occupant = string.Empty;

                if (!string.IsNullOrEmpty(iKeySn) && IsExistedIKeySN(userguid, iKeySn, ref occupant, oKodakDAL))
                {
                    model.Comments = occupant;

                    return 1;
                }

                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat(" Update tUser set IKEYSN='{0}' where UserGuid='{1}' ",
                    iKeySn, userguid);
                string sql = strBuilder.ToString();


                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(oKodakDAL.ConnectionString))
                {
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.CommandTimeout = 0;
                    cmd.Connection = conn;
                    cmd.CommandText = sql;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

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

        public virtual int DeleteUser(string userGuid, string strDomain)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //Check if the user is deleted by another user
                if (IsUserDeleted(userGuid, oKodakDAL, strDomain))
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
                strBuilder.AppendFormat("DELETE FROM tUser2Domain where UserGuid = '{0}' and Domain='{1}'", userGuid.ToString(), strDomain);
                listSQL.Add(strBuilder.ToString());
                strBuilder.Remove(0, strBuilder.Length);


                strBuilder.AppendFormat("if not exists(select * from tUser2Domain where UserGuid='{0}' and Domain!='{1}') \r\n", userGuid.ToString(), strDomain)
                  .AppendFormat("UPDATE tUser set deletemark=1 where UserGuid='{0}'",
                  userGuid.ToString());
                listSQL.Add(strBuilder.ToString());
                strBuilder.Remove(0, strBuilder.Length);



                //Prepare the SQL to delete the user detail from tUserProfile table
                strBuilder.AppendFormat("DELETE FROM tUserProfile where UserGuid = '{0}' and Domain='{1}'", userGuid.ToString(), strDomain);
                listSQL.Add(strBuilder.ToString());
                strBuilder.Remove(0, strBuilder.Length);

                //Prepare the SQL to delete the user detail from tRole2User table
                strBuilder.AppendFormat("DELETE FROM tRole2User where UserGuid = '{0}' and Domain='{1}'", userGuid.ToString(), strDomain);
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

                DataTable dtTmp = new DataTable();
                //find the remain same displayname user
                string sqlQueryHasDuplicatedDisplayNameOnline = string.Format("select displayname,userguid from tuser where displayname = (select displayname from tuser where userguid ='{0}') and userguid != '{0}' and deletemark = 0", userGuid);
                oKodakDAL.ExecuteQuery(sqlQueryHasDuplicatedDisplayNameOnline, RisDAL.ConnectionState.KeepOpen, dtTmp);
                if (dtTmp != null && dtTmp.Rows.Count == 1)//one dulicated original displayname,so set to localname = displayname
                {
                    string sqlUpdateLocalName = string.Format("update tUser set LocalName = DisplayName  where UserGuid ='{0}' ", Convert.ToString(dtTmp.Rows[0]["UserGuid"]));//recover it!
                    oKodakDAL.ExecuteNonQuery(sqlUpdateLocalName);
                }





            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception("Database error!");
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

        public virtual string GetUserDepartment(string userGuid, string strDomain)
        {

            try
            {
                string sql = string.Format("select department from tUser2Domain where userGuid='{0}' and Domain='{1}'",
                    userGuid, strDomain);

                using (RisDAL oKodakDAL = new RisDAL())
                {
                    object obj = oKodakDAL.ExecuteScalar(sql);
                    return Convert.ToString(obj);
                }

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception("Database error!");
            }
            return "";
        }


        public virtual int CheckSyncronization(string userGuid, string strDomain)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //Check if the user is deleted by another user
                if (IsUserDeleted(userGuid, oKodakDAL, strDomain))
                {
                    return 1;
                }


                string strValue = ServerCommon.DaoInstanceFactory.GetInstance().GetSystemProfileValue("WorkingMode", "0000");
                //string strSQL = "select value from tSystemProfile where Name='WorkingMode'";
                // Object obj = oKodak.ExecuteScalar(strSQL);

                if (strValue == string.Empty || Convert.ToInt32(strValue) == 0)
                {
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

                    string strInsertSyncRecord = string.Format("Insert into dbo.tSync(SyncType,Guid,Owner,OwnerIP,Domain) values('{0}','{1}','{2}','{3}','{4}')", 12, userGuid.ToString(), OperateUserID, OperateIP, strDomain);
                    oKodakDAL.ExecuteNonQuery(strInsertSyncRecord, RisDAL.ConnectionState.KeepOpen);
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

        public virtual bool DeleteSyncronization(string userGuid)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                oKodakDAL.BeginTransaction();
                string strDeleteSyncRecord = string.Format("Delete from dbo.tSync where SyncType = 12 and Guid = '{0}'", userGuid.ToString());
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


        public virtual DataTable GetUserCerts(string userGuid)
        {
            string strSql = "select * from tUserCerts where UserGuid =@UserGuid";

            using (RisDAL oKodak = new RisDAL())
            {
                oKodak.Parameters.Add("@UserGuid", userGuid);

                var dt = oKodak.ExecuteQuery(strSql);

                return dt;
            }

        }

        public virtual DataTable GetUserCertInUse(string userGuid)
        {
            string strSql = "select * from tUserCerts where UserGuid =@UserGuid and IsActive = 1";

            using (RisDAL oKodak = new RisDAL())
            {
                oKodak.Parameters.Add("@UserGuid", userGuid);

                var dt = oKodak.ExecuteQuery(strSql);

                return dt;
            }
        }

        public virtual DataTable GetUserCertByCertSN(string userGuid, string certSN)
        {
            string strSql = "select * from tUserCerts where UserGuid =@UserGuid and CertSN = @CertSN";

            using (RisDAL oKodak = new RisDAL())
            {
                oKodak.Parameters.Add("@UserGuid", userGuid);
                oKodak.Parameters.Add("@CertSN", certSN);

                var dt = oKodak.ExecuteQuery(strSql);

                return dt;
            }
        }

        public virtual bool IsExistUserCert(string userGuid, string certSN)
        {
            string strSql = "select 1 from tUserCerts where UserGuid =@UserGuid and CertSN = @CertSN";

            using (RisDAL oKodak = new RisDAL())
            {

                oKodak.Parameters.Add("@UserGuid", userGuid);
                oKodak.Parameters.Add("@CertSN", certSN);

                var result = oKodak.ExecuteScalar(strSql);

                if (Convert.ToString(result) == "1")
                    return true;

                return false;
            }
        }

        public virtual void AddUserCert(UserCertModel model)
        {

            string strSql = "Insert into tUserCerts(UserGuid,CertID,CertSN,CertBasicInfo,CertInfo,{0}IsActive,Domain)"
                + " Values(@UserGuid,@CertID,@CertSN,@CertBasicInfo,@CertInfo,{1}@IsActive,@Domain)";
            using (RisDAL oKodak = new RisDAL())
            {

                string strExistCert = "select 1 from tUserCerts where CertSN= @CertSN";

                oKodak.Parameters.Add("@CertSN", model.CertSN);
                var result = oKodak.ExecuteScalar(strExistCert, RisDAL.ConnectionState.KeepOpen);
                if (Convert.ToString(result) == "1")
                {
                    throw new Exception("CertAllreadyBound");
                }

                oKodak.Parameters.Add("@UserGuid", model.UserGuid);

                if (model.IsActive == 1)
                {
                    string strSql2 = "Update tUserCerts set IsActive = 0 where UserGuid =@UserGuid ";
                    oKodak.ExecuteNonQuery(strSql2);
                }
                string imagePicStr = Convert.ToString(model.SignPic);
                byte[] imagePic = null;
                if (!string.IsNullOrEmpty(imagePicStr))
                {
                    imagePic = Convert.FromBase64String(imagePicStr);
                }

                oKodak.Parameters.Clear();
                oKodak.Parameters.Add("@UserGuid", model.UserGuid);
                oKodak.Parameters.Add("@CertID", model.CertID);
                oKodak.Parameters.Add("@CertSN", model.CertSN);
                oKodak.Parameters.Add("@CertBasicInfo", model.CertBasicInfo);
                oKodak.Parameters.Add("@CertInfo", model.CertInfo);
                if (imagePic != null)
                {
                    oKodak.Parameters.Add("@SignPic", imagePic);
                    strSql = string.Format(strSql, "SignPic,", "@SignPic,");
                }
                else
                {
                    strSql = string.Format(strSql, "", "");
                }
                oKodak.Parameters.Add("@IsActive", model.IsActive);
                oKodak.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());

                oKodak.ExecuteNonQuery(strSql);
            }
        }

        public void EnableUserCert(string userGuid, string certSN)
        {
            string strSql = "Update tUserCerts set IsActive = 0 where UserGuid =@UserGuid "
                + " Update tUserCerts set IsActive = 1 where UserGuid =@UserGuid and CertSN = @CertSN";

            using (RisDAL oKodak = new RisDAL())
            {
                oKodak.Parameters.Add("@UserGuid", userGuid);
                oKodak.Parameters.Add("@CertSN", certSN);

                oKodak.ExecuteNonQuery(strSql);
            }
        }

        public void RemoveUserCert(string userGuid, string certSN)
        {
            string strUsed = "select 1 from tSignedHistory where CertSN = @CertSN";

            string strSql = "Delete from tUserCerts where UserGuid =@UserGuid and CertSN =@CertSN";

            using (RisDAL oKodak = new RisDAL())
            {
                oKodak.Parameters.Add("@UserGuid", userGuid);
                oKodak.Parameters.Add("@CertSN", certSN);

                var result = oKodak.ExecuteScalar(strUsed, RisDAL.ConnectionState.KeepOpen);
                if (Convert.ToString(result) == "1")
                    throw new Exception("CertIDUsed");

                oKodak.Parameters.Clear();
                oKodak.Parameters.Add("@UserGuid", userGuid);
                oKodak.Parameters.Add("@CertSN", certSN);
                oKodak.ExecuteNonQuery(strSql);
            }
        }

        #region Modified by Blue for [RC603.1] - US16931, 06/09/2014
        public void ResetPassword(string strParam)
        {

            DataAccessLayer.MyCryptography c = new DataAccessLayer.MyCryptography("GCRIS2-20061025");
            string userGuid = CommonGlobalSettings.Utilities.GetParameter("UserGuid", strParam);
            string defaultPassword = c.Encrypt(CommonGlobalSettings.Utilities.GetParameter("password", strParam));

            string strSql = string.Format("update tUser set Password = '{0}' where UserGuid = '{1}'", defaultPassword, userGuid);

            using (RisDAL oKodak = new RisDAL())
            {
                oKodak.ExecuteNonQuery(strSql);
            }
        }

        public void ChangePassword(string strParam)
        {
            DataAccessLayer.MyCryptography c = new DataAccessLayer.MyCryptography("GCRIS2-20061025");
            string loginName = CommonGlobalSettings.Utilities.GetParameter("LoginName", strParam);
            string password = c.Encrypt(CommonGlobalSettings.Utilities.GetParameter("Password", strParam));

            string strSql = string.Format("update tUser set Password = '{0}' where LoginName = '{1}'", password, loginName);

            using (RisDAL oKodak = new RisDAL())
            {
                oKodak.ExecuteNonQuery(strSql);
            }
        }
        #endregion
        #endregion

        #region ISystemProfileDAO

        public virtual DataSet GetSystemProfileDataSet(string strDomain)
        {
            RisDAL oKodalDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            string strGetSysProfSQL = string.Empty;
            string strCurrentRowName = "";
            try
            {

                //strGetSysProfSQL = string.Format("SELECT [tSystemProfile].[Name],[tSystemProfile].[ModuleID],[tModule].Title as ModuleName,[tSystemProfile].[Value],[tSystemProfile].[Exportable],[tSystemProfile].[PropertyDesc],[tSystemProfile].[PropertyOptions],[tSystemProfile].[Inheritance],[tSystemProfile].[PropertyType],[tSystemProfile].[IsHidden],[tSystemProfile].[OrderingPos]FROM [tSystemProfile], tModule where tModule.ModuleID = [tSystemProfile].[ModuleID] AND (([tSystemProfile].[IsHidden] & 4) = 4) AND [tSystemProfile].[Inheritance] >= 0 and tSystemProfile.Domain='{0}' ORDER BY [tSystemProfile].[OrderingPos]", strDomain);
                strGetSysProfSQL = string.Format("SELECT [tSystemProfile].[Name],[tSystemProfile].[ModuleID],[tModule].Title as ModuleName,[tSystemProfile].[Value],[tSystemProfile].[Exportable],[PropertyDesc],[PropertyOptions],[Inheritance],[PropertyType],[IsHidden],[OrderingPos],[OrderNo] FROM [tSystemProfile], tModule where tModule.ModuleID = [tSystemProfile].[ModuleID] AND (([tSystemProfile].[IsHidden] & 4) = 4) AND [tSystemProfile].[Inheritance] >= 0 and tModule.Domain='{0}' and tSystemProfile.Domain='{1}' ORDER BY [tSystemProfile].[OrderingPos]", strDomain, strDomain);
                DataTable dataTable = oKodalDAL.ExecuteQuery(strGetSysProfSQL);
                //Build the custom DataTable
                DataTable customedTable = Server.Utilities.Oam.Utilities.CreataCustomDataTable();

                Regex regCheckDomain = new Regex(@"from\s+tDomainList", RegexOptions.IgnoreCase); //[^-_\w]domain[^-_\w]

                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                string[] arrDftVal = null;
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
                                string strSQL = "";

                                // EK_HI00114442
                                // if it queries from tDomainList, the sql sentence will not be appended "domain".
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
                                    strSQL = strSQL.Replace("@site", CommonGlobalSettings.Utilities.GetCurSite());
                                }

                                if (strSQL.Contains("*site"))
                                {

                                    string strSQL1 = strSQL.Replace("*site", CommonGlobalSettings.Utilities.GetCurSite());
                                    oKodalDAL.ExecuteQuery(strSQL1, dtPropOption);
                                    if (dtPropOption == null || dtPropOption.Rows.Count == 0)
                                    {
                                        strSQL1 = strSQL.Replace("site in('*site')", "(site='' or site is null)");
                                        oKodalDAL.ExecuteQuery(strSQL1, dtPropOption);
                                    }


                                }
                                else
                                {
                                    oKodalDAL.ExecuteQuery(strSQL, dtPropOption);
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

                    //DefaultValue -5
                    if (rowData[2].ToString().Contains("|"))
                    {
                        arrDftVal = rowData[2].ToString().Split('|');
                        foreach (string strDftVal in arrDftVal)
                        {
                            //DefaultValue -5
                            rowData[5] += strDftVal + ",";
                        }
                        rowData[5] = rowData[5].ToString().Remove(rowData[5].ToString().Length - 1);
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
                    rowData[11] = row[dataTable.Columns["OrderNo"]].ToString();//default not set data to orderid(not use in system)
                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }
                dataSet.Tables.Add(customedTable);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "GetSystemProfileDataSet, Error=" + ex.Message);

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

        public virtual bool EditSystemProfile(SystemModel model, string strDomain)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string strSQL = "";
                StringBuilder strBuilder = new StringBuilder();
                int valueIndex = 0;
                oKodakDAL.BeginTransaction();
                foreach (DataTable table in model.SaveSystemProfile.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        oKodakDAL.Parameters.Clear();

                        //table.Columns("FieldValue");
                        //table.Columns("ModuleId");
                        //table.Columns("FieldName");
                        //table.Columns("ProfileLevel");//0-systemprofile, 1-siteprofile, 2-roleprofile, 3-userprofile
                        //table.Columns("LevelFilter");//systemprofile-none, siteprofile-site, roleprofile-roleName, userprofile-userGuid
                        if (!table.Columns.Contains("ProfileLevel") || (table.Columns.Contains("ProfileLevel") && row["ProfileLevel"].ToString() == "0"))
                        {
                            if (table.Columns.Contains("ModuleId"))
                            {
                                strBuilder.AppendFormat("UPDATE dbo.tSystemProfile set Value = {0} where ModuleId = '{1}' and Name = '{2}' and Domain='{3}'", "@FieldValue", row["ModuleId"].ToString(), row["FieldName"].ToString(), strDomain);
                                oKodakDAL.Parameters.Add("@FieldValue", row["FieldValue"].ToString());
                            }
                            else if (table.Columns.Contains("Id"))
                            {
                                strBuilder.AppendFormat("UPDATE dbo.tSystemProfile set Value = {0} where ModuleId = '{1}' and Name = '{2}' and Domain='{3}'", "@FieldValue", row["Id"].ToString(), row["FieldName"].ToString(), strDomain);
                                oKodakDAL.Parameters.Add("@FieldValue", row["FieldValue"].ToString());
                            }
                            else
                            {
                                System.Diagnostics.Debug.Assert(false, "Invalid table structure!");
                            }


                            #region #region Modified by Blue for [RC604] - US17286, 06/12/2014
                            if (row["FieldName"].Equals("PrescriptionNoMaxLengthAndInitValue"))
                            {
                                using (RisDAL oKodakDAL1 = new RisDAL())
                                {
                                    int currentInitValue = 0;
                                    int initValue = 0;
                                    string[] lengthvalue;
                                    //retrieve current init value    
                                    string sql = string.Format("select Value from tSystemProfile where Name = 'PrescriptionNoMaxLengthAndInitValue' and Domain = '{0}'", strDomain);
                                    object obj = oKodakDAL1.ExecuteScalar(sql, RisDAL.ConnectionState.KeepOpen);
                                    if (obj != null && obj != DBNull.Value)
                                    {
                                        lengthvalue = obj.ToString().Split("|".ToCharArray());
                                        if (lengthvalue != null && lengthvalue.Length == 2)
                                        {
                                            currentInitValue = Convert.ToInt32(lengthvalue[1]);
                                        }
                                    }
                                    lengthvalue = row["FieldValue"].ToString().Split("|".ToCharArray());
                                    if (lengthvalue != null && lengthvalue.Length == 2)
                                    {
                                        initValue = Convert.ToInt32(lengthvalue[1]);
                                    }
                                    if (currentInitValue != initValue)
                                    {
                                        oKodakDAL1.ExecuteNonQuery(string.Format("UPDATE tIDMaxValue SET Value = {0} WHERE Tag = 5", initValue), RisDAL.ConnectionState.KeepOpen);
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (row["ProfileLevel"].ToString() == "1")
                        {
                            strBuilder.AppendFormat("UPDATE dbo.tSiteProfile set Value = {0} where ModuleId = '{1}' and Name = '{2}' and Domain='{3}' and Site='{4}'", "@FieldValue", row["ModuleId"].ToString(), row["FieldName"].ToString(), strDomain, row["LevelFilter"].ToString());
                            oKodakDAL.Parameters.Add("@FieldValue", row["FieldValue"].ToString());
                        }
                        else if (row["ProfileLevel"].ToString() == "2")
                        {
                            strBuilder.AppendFormat("UPDATE dbo.tRoleProfile set Value = {0} where ModuleId = '{1}' and Name = '{2}' and Domain='{3}' and RoleName='{4}'", "@FieldValue", row["ModuleId"].ToString(), row["FieldName"].ToString(), strDomain, row["LevelFilter"].ToString());
                            oKodakDAL.Parameters.Add("@FieldValue", row["FieldValue"].ToString());
                        }
                        else if (row["ProfileLevel"].ToString() == "3")
                        {
                            DataTable dt = oKodakDAL.ExecuteQuery(
                                string.Format("select 1 from dbo.tUserProfile where ModuleId = '{0}' and Name = '{1}' and Domain='{2}' and UserGuid='{3}'", row["ModuleId"].ToString(), row["FieldName"].ToString(), strDomain, row["LevelFilter"].ToString()),
                                RisDAL.ConnectionState.KeepOpen);
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                strBuilder.AppendFormat("INSERT INTO dbo.tUserProfile VALUES('{0}','{1}','','{2}',{3},'1','','','0','0','0','0','{4}',NEWID())", row["FieldName"].ToString(), row["ModuleId"].ToString(), row["LevelFilter"].ToString(), "@FieldValue", strDomain);
                                oKodakDAL.Parameters.Add("@FieldValue", row["FieldValue"].ToString());
                            }
                            else
                            {
                                strBuilder.AppendFormat("UPDATE dbo.tUserProfile set Value = {0} where ModuleId = '{1}' and Name = '{2}' and Domain='{3}' and UserGuid='{4}'", "@FieldValue", row["ModuleId"].ToString(), row["FieldName"].ToString(), strDomain, row["LevelFilter"].ToString());
                                oKodakDAL.Parameters.Add("@FieldValue", row["FieldValue"].ToString());
                            }
                        }

                        strSQL = strBuilder.ToString();
                        strBuilder.Remove(0, strBuilder.Length);
                        logger.Debug((long)ModuleEnum.Oam_DA, "Oam Data Access", 1, strSQL, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                        (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                        oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                    }
                }
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

        #region IClientConfigDAO

        public virtual DataSet GetClientConfigDataSet()
        {
            RisDAL oKodalDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            string strGetClientConfigSQL = string.Empty;
            try
            {
                strGetClientConfigSQL = string.Format("SELECT A.ConfigName,A.ModuleID ,B.Title as ModuleName,A.[Value] ,A.Exportable,A.PropertyDesc,A.PropertyOptions,A.Inheritance,A.PropertyType,A.IsHidden,A.OrderingPos,A.Domain,A.[Type],[OrderNo] FROM [dbo].[tConfigDic] A,[dbo].[tModule] B WHERE A.ModuleID = B.ModuleID AND A.IsHidden = 0 AND A.[Type] = 1 ORDER BY A.OrderingPos");
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
                    drNewRow["OrderNo"] = row["OrderNo"];
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

        #region Private Methods
        /// <summary>
        ///  This private method will validate if the role name already exists
        /// </summary>
        /// <param name="strRoleName"> Role name that needs to be validated</param>
        /// <returns></returns>
        private bool IsRoleNameAreadyExists(string strRoleName, RisDAL oKodakDAL, string strDomain)
        {

            string strRoleExistVldSQL = string.Format("SELECT RoleName from tRole where RoleName= '{0}' and Domain='{1}'", strRoleName.ToString(), strDomain);
            string strRoleExists = Convert.ToString(oKodakDAL.ExecuteScalar(strRoleExistVldSQL));
            if (strRoleExists != string.Empty)
            {
                return true;
            }

            strRoleExistVldSQL = string.Format("SELECT Name from tRoleDir where Name= '{0}' and Domain='{1}'", strRoleName.ToString(), strDomain);
            strRoleExists = Convert.ToString(oKodakDAL.ExecuteScalar(strRoleExistVldSQL));
            if (strRoleExists != string.Empty)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        ///  This private method will validate if the role strDescription(localculture name) already exists
        /// </summary>
        /// <param name="strRoleName"> Role strDescription that needs to be validated</param>
        /// <returns></returns>
        private bool IsRoleDescriptionAreadyExists(string strDescription, RisDAL oKodakDAL, string strDomain)
        {

            string strRoleExistVldSQL = string.Format("SELECT Description from tRole where Description= '{0}' and Domain='{1}'", strDescription.ToString(), strDomain);
            string strRoleExists = Convert.ToString(oKodakDAL.ExecuteScalar(strRoleExistVldSQL));
            if (strRoleExists != string.Empty)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// This private method will insert the Role details in the tRole and tRoleProfile table.
        /// It is a common private method for AddRole and CopyRole public method
        /// </summary>
        /// <param name="strRoleName"> Role Name</param>
        /// <param name="strRoleDescription">Role Description</param>
        /// <param name="listSQL">List of SQL statement</param>
        /// <param name="IsCopy">Bool value to check if the method is called from AddRole or CopyRole</param>
        /// <param name="dtRoleProfileDetails">DataTable for the Role and Profile Details</param> 
        private void InsertRoleAndProfileDetails(string strRoleName, string strCopyNewRoleName, string strRoleDescription, List<string> listSQL, bool IsCopy, RisDAL oKodakDAL, DataTable dtRoleProfileDetails, string site, string strDomain)
        {
            string strRole = string.Empty;
            string strGetRoleProfSQL = string.Empty;
            //Copy the system profile to role profile. When add new role
            if (!IsCopy)
            {
                //strGetRoleProfSQL = string.Format("SELECT [Name] ,[tSystemProfile].[ModuleID],[Value],[Exportable],[PropertyDesc],[PropertyOptions],[Inheritance],[PropertyType],[IsHidden],[OrderingPos] FROM [GCRIS2].[dbo].[tSystemProfile], dbo.tModule where tModule.ModuleID = [tSystemProfile].[ModuleID] and [Inheritance] > 0");
                strGetRoleProfSQL = string.Format("SELECT [Name] ,[tSystemProfile].[ModuleID],[Value],[Exportable],[PropertyDesc],[PropertyOptions],[Inheritance],[PropertyType],[IsHidden],[OrderingPos] FROM [tSystemProfile] where [Inheritance] > 0 and tSystemProfile.Domain='{0}'", strDomain);

                strRole = strRoleName;
            }
            //Copy the existing role profile to a new role profile. When Copy As role is done
            else
            {
                strGetRoleProfSQL = string.Format("SELECT * from tRoleProfile where RoleName = '{0}' and Domain='{1}'", strRoleName, strDomain);
                strRole = strCopyNewRoleName;
            }
            dtRoleProfileDetails = new DataTable("RoleProfileDetails");
            oKodakDAL.ExecuteQuery(strGetRoleProfSQL, dtRoleProfileDetails);
            StringBuilder strInsertRoleAndProfSQL = new StringBuilder();
            if (!IsCopy)
            {
                strInsertRoleAndProfSQL.AppendFormat("INSERT INTO tRole(RoleName,Description,IsSystem,Domain) VALUES ('{0}','{1}',0,'{2}')", strRoleName, strRoleDescription, strDomain);
            }
            //Copy the existing role profile to a new role profile. When Copy As role is done
            else
            {
                strInsertRoleAndProfSQL.AppendFormat("INSERT INTO tRole(RoleName,Description,IsSystem,Domain) VALUES ('{0}','{1}',0,'{2}')", strCopyNewRoleName, strRoleDescription, strDomain);
            }
            listSQL.Add(strInsertRoleAndProfSQL.ToString());
            strInsertRoleAndProfSQL.Remove(0, strInsertRoleAndProfSQL.Length);
            string strPropertyOption = string.Empty;
            int iInheritance = -1;
            if (dtRoleProfileDetails != null && dtRoleProfileDetails.Rows.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(site))
                {
                    DataRow[] rows = dtRoleProfileDetails.Select(" Name = 'QueryCategories'");
                    foreach (DataRow row in rows)
                    {
                        row["PropertyOptions"] = string.Format("select QueryName from tQuery where len(ID)>0 and (Site = '' or Site ='{0}')", site);
                    }
                    rows = dtRoleProfileDetails.Select(" PropertyDesc = 'Access site data'");
                    foreach (DataRow row in rows)
                    {
                        row["PropertyOptions"] = string.Format("if exists(select 1 from tRoleProfile where 1=1 and Domain in (select Value from tSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'and Value='')(select site value,alias text from tSiteList where Domain in(select Value from tSystemProfile where Name='Domain'))else(select site value,alias text from tSiteList where Domain in(select Value from tSystemProfile where Name='Domain')and charindex('|'+site+'|',(select top 1 '|'+Value+'|' from tRoleProfile where Domain in(select Value from tSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'))>0)", Convert.ToString(row["Name"]).Trim());
                        row["Value"] = Convert.ToString(row["Name"]).Trim();
                    }
                    rows = dtRoleProfileDetails.Select(" PropertyDesc = 'IM can access site'");
                    foreach (DataRow row in rows)
                    {
                        row["PropertyOptions"] = string.Format("if exists(select 1 from tRoleProfile where 1=1 and Domain in (select Value from tSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'and Value='')(select site value,alias text from tSiteList where Domain in(select Value from tSystemProfile where Name='Domain'))else(select site value,alias text from tSiteList where Domain in(select Value from tSystemProfile where Name='Domain')and charindex('|'+site+'|',(select top 1 '|'+Value+'|' from tRoleProfile where Domain in(select Value from tSystemProfile where Name='Domain')and RoleName='siteadmin'and Name='{0}'))>0)", Convert.ToString(row["Name"]).Trim());
                        row["Value"] = Convert.ToString(row["Name"]).Trim().Replace("IM_", "");
                    }
                }
                foreach (DataRow drRoleProfileDetails in dtRoleProfileDetails.Rows)
                {
                    strPropertyOption = Convert.ToString(drRoleProfileDetails["PropertyOptions"]);
                    if (!strPropertyOption.Contains("|") || strPropertyOption.Contains("'|'"))
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

                    strInsertRoleAndProfSQL.AppendFormat("INSERT INTO tRoleProfile(RoleName,Name,ModuleID,[Value],Exportable,PropertyDesc,PropertyOptions,Inheritance,PropertyType,IsHidden,OrderingPos,[Domain],UniqueID) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',NEWID())",
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
                    Convert.ToString(drRoleProfileDetails["OrderingPos"]),
                    strDomain);

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

        private List<string> CSTextBox(string strUserName, RisDAL oKodakDAL)
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


            string strUserOnlineSQL = string.Format("SELECT TOP 1 RoleName from tOnlineClient where UserGuid= {0}", strUserGuid.ToString());
            string strUserOnline = Convert.ToString(oKodakDAL.ExecuteScalar(strUserOnlineSQL));
            if (strUserOnline != string.Empty)
            {
                return false;
            }
            return true;
        }

        private bool IsUserDeleted(string strUserGuid, RisDAL oKodakDAL, string strDomain)
        {

            string strUserDeletedSQL = string.Format("SELECT count(*) from tUser2Domain where UserGuid= '{0}' and Domain='{1}'", strUserGuid.ToString(), strDomain);
            object obj = oKodakDAL.ExecuteScalar(strUserDeletedSQL);
            if (obj != null && Convert.ToInt32(obj) > 0)
            {
                return false;
            }
            return true;
        }

        private bool IsUserExistsIntSync(string strUserGuid, RisDAL oKodakDAL)
        {

            string strUserExistsIntSyncSQL = string.Format("SELECT TOP 1 Guid from dbo.tSync where Guid= '{0}' and SyncType = 12", strUserGuid.ToString());
            string strUserExistsIntSync = Convert.ToString(oKodakDAL.ExecuteScalar(strUserExistsIntSyncSQL));
            if (strUserExistsIntSync != string.Empty)
            {
                return true;
            }
            return false;
        }

        private bool IsExistedIKeySN(string userGuid, string iKeySN, ref string occupant, RisDAL oKodakDAL)
        {
            string sql = string.Format(
                "SELECT LocalName from tUser where UserGuid <> '{0}' AND iKeySN = '{1}' and DeleteMark=0",
                userGuid, iKeySN);

            DataTable existsDt = oKodakDAL.ExecuteQuery(sql);

            if (existsDt != null && existsDt.Rows.Count > 0)
            {
                occupant = System.Convert.ToString(existsDt.Rows[0]["LocalName"]);

                return true;
            }

            return false;

        }

        /// <summary>
        ///  This private method will validate if the user name already exists
        /// </summary>
        /// <param name="strUserName"> User name that needs to be validated</param>
        /// <returns></returns>
        private bool IsUserNameAlreadyExists(string strUserName, ref string userGuid, bool LoginNameChanged, bool searchDeletedUser, RisDAL oKodakDAL)
        {
            int deleteMark = Convert.ToInt32(searchDeletedUser);
            //string strUserExistVldSQL = string.Format("SELECT LoginName,UserGuid from tUser where LoginName= '{0}'", strUserName.ToString());
            string strUserExistVldSQL = string.Format("SELECT tUser.LoginName,tUser.UserGuid from tUser where tUser.LoginName= '{0}' and tUser.DeleteMark={1}", strUserName, searchDeletedUser ? 1 : 0);
            DataTable existsDt = oKodakDAL.ExecuteQuery(strUserExistVldSQL);
            //modify
            if (userGuid != "")
            {
                if (LoginNameChanged == true)
                {
                    if (existsDt != null && existsDt.Rows.Count > 0)
                    {
                        return true;
                    }
                }
            }
            //add
            else
            {
                if (existsDt != null && existsDt.Rows.Count > 0)
                {
                    userGuid = existsDt.Rows[0]["UserGuid"].ToString();
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        ///  This private method will validate if the user name already exists
        /// </summary>
        /// <param name="strUserName"> User name that needs to be validated</param>
        /// <returns></returns>
        private bool IsDisplayNameAlreadyExists(string strDisplayName, ref string userGuid, bool DisplayNameChanged, bool searchDeletedUser, RisDAL oKodakDAL)
        {
            int deleteMark = Convert.ToInt32(searchDeletedUser);
            string strLocalNameExistVldSQL = string.Format("SELECT tUser.LoginName,tUser.UserGuid from tUser  where tUser.DisplayName= '{0}' and tUser.DeleteMark={1} and tUser.UserGuid != '{2}'", strDisplayName.ToString(), searchDeletedUser ? 1 : 0, userGuid);
            DataTable existsDt = oKodakDAL.ExecuteQuery(strLocalNameExistVldSQL);
            if (userGuid != "")
            {
                if (DisplayNameChanged == true)
                {
                    if (existsDt != null && existsDt.Rows.Count > 0)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (existsDt != null && existsDt.Rows.Count > 0)
                {
                    userGuid = existsDt.Rows[0]["UserGuid"].ToString();
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        ///  This private method will validate if the domain name already exists
        /// </summary>
        /// <param name="strDomainLoginName"> Domain Login name that needs to be validated</param>
        /// <returns></returns>
        private bool IsDomainLoginNameAreadyExists(string strDomainLoginName, ref string userGuid, bool DomainLoginNameChanged, bool searchDeletedUser, RisDAL oKodakDAL)
        {
            int deleteMark = Convert.ToInt32(searchDeletedUser);
            //string strDmnLgnNameExistVldSQL = string.Format("SELECT UserGuid from tUser2Domain where DomainLoginName= '{0}' and DeleteMark = {1} ", strDomainLoginName.ToString(), searchDeletedUser ? 1 : 0);
            string strDmnLgnNameExistVldSQL = string.Format("SELECT UserGuid from tUser2Domain where DomainLoginName= '{0}' ", strDomainLoginName.ToString());
            DataTable existsDt = oKodakDAL.ExecuteQuery(strDmnLgnNameExistVldSQL);
            if (userGuid != "")
            {
                if (DomainLoginNameChanged == true)
                {
                    if (existsDt != null && existsDt.Rows.Count > 0)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (existsDt != null && existsDt.Rows.Count > 0)
                {
                    userGuid = existsDt.Rows[0]["UserGuid"].ToString();
                    return true;
                }
            }
            return false;

        }

        #endregion

        #region IDictionaryDAO Section
        private const string SQLGetDictionaryList = "Select * From tDictionary where ishidden=0 ";
        private const string SQLGetDictionaryItemList = "Select * From tDictionaryValue";
        private const string SQLDeleteDictionary = "Delete From tDictionary Where Tag=";
        private const string SQLDeleteDictionaryValue = "Delete From tDictionaryValue Where Tag=";
        private const string SQLAddDictionaryValue = "Insert into tDictionaryValue(Tag, Value,Text,IsDefault, ShortcutCode,OrderID,MapTag,Domain,Site) ";
        //private const string SQLUpdateDictonaryValue = "Update tDictionaryValue Set Name=";

        public virtual bool SavePhysicalCompany(BaseDataSetModel model)
        {
            StringBuilder sb = new StringBuilder();
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                DataTable dt = model.DataSetParameter.Tables[0];
                sb.Append("delete from tPhysicalCompany;\r\n");
                dataAccess.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Clear();
                    dataAccess.Parameters.Clear();
                    dataAccess.Parameters.Add("@Group", dr["Group"].ToString());
                    dataAccess.Parameters.Add("@Service", dr["Service"].ToString());
                    dataAccess.Parameters.Add("@ClinicCode", dr["ClinicCode"].ToString());
                    dataAccess.Parameters.Add("@ClinicFullName", dr["ClinicFullName"].ToString());
                    dataAccess.Parameters.Add("@Telephone", dr["Telephone"].ToString());
                    dataAccess.Parameters.Add("@Address", dr["Address"].ToString());
                    sb.Append(@"insert into tPhysicalCompany([guid],[group],[service],cliniccode,clinicfullname,telephone,address,comment,optional1,optional2)
                    values(NEWID(),@Group,@Service,@ClinicCode,@ClinicFullName,@Telephone,@Address,'','','')");
                    dataAccess.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception e)
            {
                dataAccess.RollbackTransaction();
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(e.Message);
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

        public virtual DataSet GetPhysicalCompany(string parameters)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet dataSet = new DataSet();
            string sql = "select * from tPhysicalCompany order by [Group], Service, ClinicCode";
            try
            {
                DataTable dt = dataAccess.ExecuteQuery(sql);
                dt.TableName = "tPhysicalCompany";
                dataSet.Tables.Add(dt);
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
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

        public virtual DataSet GetDictionaryDataSet(string strSite)
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
                    string[] rowData = new string[13];

                    //ID-0
                    rowData[0] = ((int)row[dataTable.Columns["Tag"]]).ToString();

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

                    //OrderID
                    rowData[11] = "";

                    rowData[10] = "";

                    //Site
                    rowData[12] = "";

                    int tag = (int)row[dataTable.Columns["Tag"]];

                    string detailSql = SQLGetDictionaryItemList + " Where Tag=" + tag + " and Site = '" + strSite + "' order by OrderID ASC";
                    if (string.IsNullOrWhiteSpace(strSite))
                    {
                        detailSql = SQLGetDictionaryItemList + " Where Tag=" + tag + " and (Site = '' or Site is null) order by OrderID ASC";
                    }
                    DataTable detailDataTable = dataAccess.ExecuteQuery(detailSql, RisDAL.ConnectionState.KeepOpen);
                    if (detailDataTable.Rows.Count == 0)
                    {
                        detailSql = SQLGetDictionaryItemList + " Where Tag=" + tag + " and (Site = '' or Site is null) order by OrderID ASC";
                        detailDataTable = dataAccess.ExecuteQuery(detailSql, RisDAL.ConnectionState.KeepOpen);
                    }
                    int i = 0;//for judge not the first row
                    foreach (DataRow subRow in detailDataTable.Rows)
                    {
                        if (rowData[2].Equals("") && (i == 0))
                        {
                            rowData[2] = subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Text"]] as string;
                        }
                        else
                        {
                            rowData[2] += subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "|" + "" : "|" + (subRow[detailDataTable.Columns["Text"]] as string);
                        }

                        if (rowData[3].Equals("") && (i == 0))//only using for first row
                        {
                            rowData[3] = subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Text"]] as string;
                        }
                        else
                        {
                            rowData[3] += subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "|" + "" : "|" + (subRow[detailDataTable.Columns["Text"]] as string);
                        }

                        if (rowData[4].Equals("") && (i == 0))//only using for first row
                        {
                            rowData[4] = subRow[detailDataTable.Columns["ShortcutCode"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["ShortcutCode"]] as string;
                        }
                        else
                        {
                            rowData[4] += subRow[detailDataTable.Columns["ShortcutCode"]] == DBNull.Value ? "|" + "" : "|" + (subRow[detailDataTable.Columns["ShortcutCode"]] as string);
                        }
                        //orderid
                        if (rowData[11].Equals("") && (i == 0))
                        {
                            rowData[11] = (subRow[detailDataTable.Columns["OrderID"]] == DBNull.Value) ? "" : ((int)subRow[detailDataTable.Columns["OrderID"]]).ToString();
                        }
                        else
                        {
                            if (subRow[detailDataTable.Columns["OrderID"]] != DBNull.Value)
                            {
                                rowData[11] += "|" + (((int)subRow[detailDataTable.Columns["OrderID"]]).ToString());
                            }
                            else
                            {
                                rowData[11] += "|" + "";
                            }
                        }

                        //Site
                        if (rowData[12].Equals("") && (i == 0))
                        {
                            rowData[12] = (subRow[detailDataTable.Columns["Site"]] == DBNull.Value) ? "" : (subRow[detailDataTable.Columns["Site"]]).ToString();
                        }
                        else
                        {
                            if (subRow[detailDataTable.Columns["Site"]] != DBNull.Value)
                            {
                                rowData[12] += "|" + ((subRow[detailDataTable.Columns["Site"]]).ToString());
                            }
                            else
                            {
                                rowData[12] += "|" + "";
                            }
                        }

                        int isDefault = subRow[detailDataTable.Columns["IsDefault"]] == DBNull.Value ? 0 : (int)subRow[detailDataTable.Columns["IsDefault"]];
                        if (isDefault == 1)
                        {
                            //DefaultValue-3
                            rowData[5] = subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Text"]] as string;
                            rowData[10] = subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Text"]] as string;
                        }
                        i++;
                    }

                    //CategoryName-6
                    rowData[6] = "Dictionary";

                    //FieldType-7
                    rowData[7] = PropertyItemType.ListBox;

                    //RegularExpress-8
                    rowData[8] = "";

                    //Description-9
                    rowData[9] = "(" + tag.ToString() + ")" + row[dataTable.Columns["description"]] as string;

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

        public virtual DataSet CustomDictionaryItems(string tag, string strSite)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet dataSet = new DataSet();
            string publicSql = SQLGetDictionaryItemList + " Where Tag=" + tag + " and (Site = '' or Site is null) order by OrderID ASC";
            string sql = "";
            try
            {
                DataTable detailDataTable = dataAccess.ExecuteQuery(publicSql, RisDAL.ConnectionState.KeepOpen);
                if (detailDataTable != null && detailDataTable.Rows.Count > 0)
                {
                    dataAccess.BeginTransaction();
                    foreach (DataRow row in detailDataTable.Rows)
                    {
                        sql += string.Format(" insert into tDictionaryValue(Tag,Value,Text,IsDefault,ShortcutCode,OrderID,Domain,mapTag,MapValue,Site) values({0},'{1}','{2}',{3},'{4}',{5},'{6}',{7},'{8}','{9}')",
                            row["Tag"] is DBNull ? "null" : Convert.ToString(row["Tag"]), row["Value"] is DBNull ? "" : Convert.ToString(row["Value"]),
                            row["Text"] is DBNull ? "" : Convert.ToString(row["Text"]), row["IsDefault"] is DBNull ? "" : Convert.ToString(row["IsDefault"]),
                            row["ShortcutCode"] is DBNull ? "" : Convert.ToString(row["ShortcutCode"]), row["OrderID"] is DBNull ? "null" : Convert.ToString(row["OrderID"]),
                            row["Domain"] is DBNull ? "" : Convert.ToString(row["Domain"]), row["mapTag"] is DBNull ? "null" : Convert.ToString(row["mapTag"]),
                            row["MapValue"] is DBNull ? "" : Convert.ToString(row["MapValue"]), strSite);
                    }
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    dataAccess.CommitTransaction();
                }
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                dataAccess.RollbackTransaction();
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }

            dataSet = GetSiteDictionaryItems(tag, strSite);
            return dataSet;
        }

        public virtual DataSet DeleteDictionaryItems(string tag, string strSite)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet dataSet = new DataSet();
            string sql = "Delete From tDictionaryValue Where Tag=" + tag + " and Site = '" + strSite + "'";
            if (string.IsNullOrWhiteSpace(strSite))
            {
                sql = "Delete From tDictionaryValue Where Tag=" + tag + " and (Site = '' or Site is null)";
            }
            try
            {
                dataAccess.ExecuteNonQuery(sql);
                dataSet = GetSiteDictionaryItems(tag, strSite);
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

        public virtual DataSet GetSiteDictionaryItems(string tag, string strSite)
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
                    if (((int)row[dataTable.Columns["Tag"]]).ToString() != tag)
                    {
                        continue;
                    }
                    string[] rowData = new string[13];

                    //ID-0
                    rowData[0] = ((int)row[dataTable.Columns["Tag"]]).ToString();

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

                    //OrderID
                    rowData[11] = "";

                    rowData[10] = "";

                    //Site
                    rowData[12] = "";


                    string detailSql = SQLGetDictionaryItemList + " Where Tag=" + tag + " and Site = '" + strSite + "' order by OrderID ASC";
                    if (string.IsNullOrWhiteSpace(strSite))
                    {
                        detailSql = SQLGetDictionaryItemList + " Where Tag=" + tag + " and (Site = '' or Site is null) order by OrderID ASC";
                    }
                    DataTable detailDataTable = dataAccess.ExecuteQuery(detailSql, RisDAL.ConnectionState.CloseOnExit);
                    if (detailDataTable.Rows.Count == 0)
                    {
                        break;
                    }
                    int i = 0;//for judge not the first row
                    foreach (DataRow subRow in detailDataTable.Rows)
                    {
                        if (rowData[2].Equals("") && (i == 0))
                        {
                            rowData[2] = subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Text"]] as string;
                        }
                        else
                        {
                            rowData[2] += subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "|" + "" : "|" + (subRow[detailDataTable.Columns["Text"]] as string);
                        }

                        if (rowData[3].Equals("") && (i == 0))//only using for first row
                        {
                            rowData[3] = subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Text"]] as string;
                        }
                        else
                        {
                            rowData[3] += subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "|" + "" : "|" + (subRow[detailDataTable.Columns["Text"]] as string);
                        }

                        if (rowData[4].Equals("") && (i == 0))//only using for first row
                        {
                            rowData[4] = subRow[detailDataTable.Columns["ShortcutCode"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["ShortcutCode"]] as string;
                        }
                        else
                        {
                            rowData[4] += subRow[detailDataTable.Columns["ShortcutCode"]] == DBNull.Value ? "|" + "" : "|" + (subRow[detailDataTable.Columns["ShortcutCode"]] as string);
                        }
                        //orderid
                        if (rowData[11].Equals("") && (i == 0))
                        {
                            rowData[11] = (subRow[detailDataTable.Columns["OrderID"]] == DBNull.Value) ? "" : ((int)subRow[detailDataTable.Columns["OrderID"]]).ToString();
                        }
                        else
                        {
                            if (subRow[detailDataTable.Columns["OrderID"]] != DBNull.Value)
                            {
                                rowData[11] += "|" + (((int)subRow[detailDataTable.Columns["OrderID"]]).ToString());
                            }
                            else
                            {
                                rowData[11] += "|" + "";
                            }
                        }

                        //Site
                        if (rowData[12].Equals("") && (i == 0))
                        {
                            rowData[12] = (subRow[detailDataTable.Columns["Site"]] == DBNull.Value) ? "" : (subRow[detailDataTable.Columns["Site"]]).ToString();
                        }
                        else
                        {
                            if (subRow[detailDataTable.Columns["Site"]] != DBNull.Value)
                            {
                                rowData[12] += "|" + ((subRow[detailDataTable.Columns["Site"]]).ToString());
                            }
                            else
                            {
                                rowData[12] += "|" + "";
                            }
                        }

                        int isDefault = subRow[detailDataTable.Columns["IsDefault"]] == DBNull.Value ? 0 : (int)subRow[detailDataTable.Columns["IsDefault"]];
                        if (isDefault == 1)
                        {
                            //DefaultValue-3
                            rowData[5] = subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Text"]] as string;
                            rowData[10] = subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Text"]] as string;
                        }
                        i++;
                    }

                    //CategoryName-6
                    rowData[6] = "Dictionary";

                    //FieldType-7
                    rowData[7] = PropertyItemType.ListBox;

                    //RegularExpress-8
                    rowData[8] = "";

                    //Description-9
                    rowData[9] = "(" + tag.ToString() + ")" + row[dataTable.Columns["description"]] as string;

                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                    break;

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

        //Defect: #EK_HI00044625
        public virtual DataSet GetDictionaryVaild(int DicTag)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                DataSet dataSet = new DataSet();
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("select DescLength,PropertyOptions from tDictionary where Tag={0}", DicTag);
                string sqlStatement = sql.ToString();
                DataTable dataTable = dataAccess.ExecuteQuery(sqlStatement);
                dataSet.Tables.Add(dataTable);
                return dataSet;
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

            return null;
        }

        public virtual bool ModifyDictionaryDefaultValue(DictionaryModel model)
        {
            List<string> listSQL = new List<string>();
            for (int i = 0; i < model.Tag.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(model.Site[i]))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Update tDictionaryValue set IsDefault=0 Where Tag={0} and (Site ='' or Site is null)", Convert.ToInt32(model.Tag[i]));
                    listSQL.Add(sb.ToString());

                    sb.Remove(0, sb.Length);
                    sb.AppendFormat("Update tDictionaryValue set IsDefault=1 Where Tag={0} And Value ='{1}' and (Site ='' or Site is null)", Convert.ToInt32(model.Tag[i]), model.DefaultValue[i]);
                    listSQL.Add(sb.ToString());
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Update tDictionaryValue set IsDefault=0 Where Tag={0} and Site ='{1}'", Convert.ToInt32(model.Tag[i]), model.Site[i]);
                    listSQL.Add(sb.ToString());

                    sb.Remove(0, sb.Length);
                    sb.AppendFormat("Update tDictionaryValue set IsDefault=1 Where Tag={0} And Value ='{1}' and Site ='{2}'", Convert.ToInt32(model.Tag[i]), model.DefaultValue[i], model.Site[i]);
                    listSQL.Add(sb.ToString());
                }
            }
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    dataAccess.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception e)
            {
                dataAccess.RollbackTransaction();
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual bool AddDictionaryValue(string tag, string codeValue, string codeDescription, string shortcutCode, string strDomain, string strSite)
        {
            RisDAL dataAccess = new RisDAL();
            int orderid = 0;//default is zero
            //get max orderid by tag
            string maxOrderIDSQL = "select max(orderid) from tDictionaryValue where tag = " + tag + " and Site='" + strSite + "'";
            if (string.IsNullOrWhiteSpace(strSite))
            {
                maxOrderIDSQL = "select max(orderid) from tDictionaryValue where tag = " + tag + " and (Site='' or Site is null)";
            }
            Object o = dataAccess.ExecuteScalar(maxOrderIDSQL, RisDAL.ConnectionState.KeepOpen);
            if (o == DBNull.Value)//first tag
            {
                orderid = 0;
            }
            else
            {
                orderid = Convert.ToInt32(o) + 1;//add to last
            }

            string sql = SQLAddDictionaryValue;

            string sqlCheckmapgTag = string.Format("select top 1 mapTag from tDictionaryValue where tag ={0} and MapTag is not Null", tag);

            try
            {


                object objmapTag = dataAccess.ExecuteScalar(sqlCheckmapgTag, RisDAL.ConnectionState.KeepOpen);

                sql += " Values(" + tag + ",'" + codeValue + "','" + codeDescription + "',0,'" + shortcutCode + "'," + orderid + ",'" + Convert.ToString(objmapTag) + "','" + strDomain + "','" + strSite + "')";

                dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);


            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual bool ModifyDictionaryValue(string tag, string oldCodeValue, string newCodeValue, string newDescription, string shortcutCode, string dictionaryValues, string strSite)
        {
            RisDAL dataAccess = new RisDAL();
            StringBuilder sb = new StringBuilder();
            sb.Append("Update tDictionaryValue Set Value='" + newCodeValue + "', Text='" + newDescription + "'");
            sb.Append(", ShortcutCode='" + shortcutCode + "'");
            sb.Append(", Site='" + strSite + "'");
            if (string.IsNullOrWhiteSpace(strSite))
            {
                sb.Append(" Where tag=" + Convert.ToInt32(tag) + " And Value='" + oldCodeValue + "'" + " and (Site='' or Site is null)\r\n");
            }
            else
            {
                sb.Append(" Where tag=" + Convert.ToInt32(tag) + " And Value='" + oldCodeValue + "'" + " and Site='" + strSite + "'\r\n");
            }
            try
            {
                //upadte all this tag dictionaryvalue's order
                string[] strDictionaryValueInOrder = dictionaryValues.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);//split by ','
                for (int i = 0; i < strDictionaryValueInOrder.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(strSite))
                    {
                        sb.AppendFormat("Update tDictionaryValue Set OrderID={0} where Value='{1}' and tag = '{2}' and (Site = '' or Site is null)", i, strDictionaryValueInOrder[i], Convert.ToInt32(tag));
                    }
                    else
                    {
                        sb.AppendFormat("Update tDictionaryValue Set OrderID={0} where Value='{1}' and tag = '{2}' and Site = '{3}'", i, strDictionaryValueInOrder[i], Convert.ToInt32(tag), strSite);
                    }
                    sb.AppendFormat("\r\n");
                }

                dataAccess.ExecuteNonQuery(sb.ToString());
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual bool DeleteDictionaryValue(string tag, string codeValue, string strSite)
        {
            RisDAL dataAccess = new RisDAL();
            string sql = SQLDeleteDictionaryValue;
            string siteInfo = string.IsNullOrEmpty(strSite) ? "(Site='' or Site is null)" : "(Site='" + strSite + "')";
            sql += Convert.ToInt32(tag) + " And Value='" + codeValue + "' and " + siteInfo;
            int row = 0;
            try
            {
                row = dataAccess.ExecuteNonQuery(sql);
                return true;
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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


        }

        public virtual DataSet GetDicMappingDataset(string strSite)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet dataSet = new DataSet();
            string sql = "Select  tDv.UniqueID,tD.Name,tDv.Tag,tDv.Value,tDv.Text,tDv.MapTag,tDv.MapValue From tDictionaryValue tDv inner join tDictionary tD on tDv.Tag =tD.Tag where Site='" + strSite + "' and mapTag is not null and mapTag <>'' and ishidden=0";
            if (string.IsNullOrWhiteSpace(strSite))
            {
                sql = "Select  tDv.UniqueID,tD.Name,tDv.Tag,tDv.Value,tDv.Text,tDv.MapTag,tDv.MapValue From tDictionaryValue tDv inner join tDictionary tD on tDv.Tag =tD.Tag where (Site='' or Site is null) and mapTag is not null and mapTag <>'' and ishidden=0";
            }
            try
            {
                DataTable dataTable = dataAccess.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);

                //Build the custom DataTabe
                DataTable customedTable = Server.Utilities.Oam.Utilities.CreataCustomDataTable();

                foreach (DataRow row in dataTable.Rows)
                {
                    string[] rowData = new string[12];

                    //ID-0
                    rowData[0] = Convert.ToString(row[dataTable.Columns["UniqueID"]]);

                    //FieldName-1
                    rowData[1] = row[dataTable.Columns["Text"]] as string;

                    //FieldValue-2
                    rowData[2] = "";

                    //FieldDescription-3
                    rowData[3] = "";

                    //ShortcutCode-4
                    rowData[4] = "";

                    //MapValue-5
                    rowData[5] = Convert.ToString(row[dataTable.Columns["MapValue"]]);

                    //OrderID
                    rowData[11] = "";

                    //MapValueDescription-10
                    rowData[10] = "";

                    int tag = (int)row[dataTable.Columns["MapTag"]];
                    string detailSql = SQLGetDictionaryItemList + " Where Tag=" + tag + " and Site = '" + strSite + "' order by OrderID ASC";
                    if (string.IsNullOrWhiteSpace(strSite))
                    {
                        detailSql = SQLGetDictionaryItemList + " Where Tag=" + tag + " and (Site = '' or Site is null) order by OrderID ASC";
                    }
                    DataTable detailDataTable = dataAccess.ExecuteQuery(detailSql, RisDAL.ConnectionState.KeepOpen);
                    if (detailDataTable.Rows.Count == 0)
                    {
                        detailSql = SQLGetDictionaryItemList + " Where Tag=" + tag + " and (Site = '' or Site is null) order by OrderID ASC";
                        detailDataTable = dataAccess.ExecuteQuery(detailSql, RisDAL.ConnectionState.KeepOpen);
                    }
                    string getDicNameSql = string.Format("Select Name from tDictionary where tag ={0}", tag.ToString());
                    object obj = dataAccess.ExecuteScalar(getDicNameSql, RisDAL.ConnectionState.CloseOnExit);
                    string mapTagDicName = Convert.ToString(obj);

                    int i = 0;//for judge not the first row
                    foreach (DataRow subRow in detailDataTable.Rows)
                    {
                        if (rowData[2].Equals(""))
                        {
                            rowData[2] = subRow[detailDataTable.Columns["Value"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Value"]] as string;
                        }
                        else
                        {
                            rowData[2] += subRow[detailDataTable.Columns["Value"]] == DBNull.Value ? "|" + "" : "|" + (subRow[detailDataTable.Columns["Value"]] as string);
                        }

                        if (rowData[3].Equals("") && (i == 0))//only using for first row
                        {
                            rowData[3] = subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Text"]] as string;
                        }
                        else
                        {
                            rowData[3] += subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "|" + "" : "|" + (subRow[detailDataTable.Columns["Text"]] as string);
                        }

                        if (rowData[4].Equals("") && (i == 0))//only using for first row
                        {
                            rowData[4] = subRow[detailDataTable.Columns["ShortcutCode"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["ShortcutCode"]] as string;
                        }
                        else
                        {
                            rowData[4] += subRow[detailDataTable.Columns["ShortcutCode"]] == DBNull.Value ? "|" + "" : "|" + (subRow[detailDataTable.Columns["ShortcutCode"]] as string);
                        }
                        //orderid
                        if (rowData[11].Equals(""))
                        {
                            rowData[11] = (subRow[detailDataTable.Columns["OrderID"]] == DBNull.Value) ? "" : ((int)subRow[detailDataTable.Columns["OrderID"]]).ToString();
                        }
                        else
                        {
                            if (subRow[detailDataTable.Columns["OrderID"]] != DBNull.Value)
                            {
                                rowData[11] += "|" + (((int)subRow[detailDataTable.Columns["OrderID"]]).ToString());
                            }
                        }

                        int isDefault = subRow[detailDataTable.Columns["IsDefault"]] == DBNull.Value ? 0 : (int)subRow[detailDataTable.Columns["IsDefault"]];
                        if (isDefault == 1)
                        {
                            //DefaultValue-3
                            //rowData[5] = subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Text"]] as string;
                            rowData[10] = subRow[detailDataTable.Columns["Text"]] == DBNull.Value ? "" : subRow[detailDataTable.Columns["Text"]] as string;
                        }
                        i++;
                    }

                    //CategoryName-6
                    rowData[6] = Convert.ToString(row[dataTable.Columns["Name"]]) + "->" + mapTagDicName;

                    //FieldType-7
                    rowData[7] = PropertyItemType.ListBox;

                    //RegularExpress-8
                    rowData[8] = "";

                    string mapText = "";
                    if (detailDataTable != null)
                    {
                        DataRow[] drs = detailDataTable.Select(string.Format("Tag={0} and Value ='{1}'", Convert.ToString(row["MapTag"]), rowData[5]));
                        if (drs.Length > 0)
                            mapText = Convert.ToString(drs[0]["Text"]);
                    }
                    //Description-9
                    rowData[9] = (row[dataTable.Columns["Text"]] as string) + "->" + mapText;

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

        public virtual bool SaveDicMapping(DictionaryModel model)
        {
            List<string> listSQL = new List<string>();
            for (int i = 0; i < model.Tag.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Update tDictionaryValue set MapValue ='{0}' Where UniqueID='{1}'", model.DefaultValue[i], model.Tag[i]);
                listSQL.Add(sb.ToString());
            }
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    dataAccess.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception e)
            {
                dataAccess.RollbackTransaction();
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual DataSet GetTeachingCategory()
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                DataSet dataSet = new DataSet();
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("select * from tTeachingCategory where Domain = '{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
                string sqlStatement = sql.ToString();
                DataTable dataTable = dataAccess.ExecuteQuery(sqlStatement);
                dataSet.Tables.Add(dataTable);
                return dataSet;
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

            return null;
        }

        public virtual DataSet IsTeachingCategoryExists(string parentID, string categoryName)
        {
            RisDAL dataAccess = new RisDAL();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("select * from tTeachingCategory where CategoryName = '{0}' and ParentID = '{1}'", categoryName, parentID);
                DataTable dataTable = dataAccess.ExecuteQuery(sql.ToString());
                DataSet ds = new DataSet();
                ds.Tables.Add(dataTable);
                return ds;
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
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

        public virtual bool AddTeachingCategory(string guid, string categoryName, string optionSettingName, string parentID, int level, int orderNo)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                #region Modified by Blue for RC569, 4/23/2014
                string sql = string.Format("insert into tTeachingCategory values ('{0}', '{1}',{2},'{3}',{4},'{5}','{6}');", guid, categoryName, level, parentID, orderNo, CommonGlobalSettings.Utilities.GetCurDomain(), optionSettingName);
                //Add default option field setting into tSystemProfile
                sql += string.Format(@"INSERT INTO tSystemProfile(Name, ModuleID, Value, Exportable, PropertyDesc, PropertyOptions, Inheritance, PropertyType, IsHidden, OrderingPos, Domain, UniqueID) "
                         + @"VALUES('{0}', '0500', 'Option1-备注1|Option2-备注2|Option3-备注3|Option4-备注4|Option5-备注5|Option6-备注6', 1, '{0}', '', 0, 0, 4, '050100', (SELECT Value FROM tSystemProfile WHERE Name = 'Domain'), NEWID())", optionSettingName);
                #endregion
                dataAccess.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual bool ModifyTeachingCategory(string guid, string categoryName, string optionSettingName, string originalSettingName, string path, string orginalPath)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                #region Modified by Blue for RC569, 4/23/2014
                string sql = string.Format("update tTeachingCategory set CategoryName = '{0}', OptionFieldSettingName = '{2}' where UniqueID = '{1}';", categoryName, guid, optionSettingName);
                //update option field setting in tSystemProfile
                sql += string.Format("update tSystemProfile set Name = '{0}' where Name = '{1}' and ModuleID = '0500';", optionSettingName, originalSettingName);
                #endregion
                sql += string.Format("update tTeaching set FileType = '{0}' where FileType = '{1}';", path, orginalPath);
                sql += string.Format("update tTeaching set FileType = '{0}->' + SUBSTRING(FileType,(LEN('{1}->')) +1,LEN(filetype)) where FileType like '{1}->%'", path, orginalPath);
                dataAccess.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual bool DeleteTeachingCategory(string guid)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string guidSql = string.Empty;
                string[] guidList = guid.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (guidList != null && guidList.Length > 0)
                {
                    guidSql = " where ";
                    foreach (string item in guidList)
                    {
                        guidSql += string.Format(" UniqueID = '{0}' or", item);
                    }
                }
                else
                {
                    throw new Exception("Invalid guids");
                    return false;
                }

                #region Modified by Blue for RC569, 4/23/2014
                string sql = string.Format("delete from tSystemProfile where Name in (select OptionFieldSettingName from tTeachingCategory {0}) and ModuleID = '0500';", guidSql.TrimEnd("or".ToCharArray()));
                sql += string.Format("delete from tTeachingCategory {0}", guidSql.TrimEnd("or".ToCharArray()));
                #endregion
                dataAccess.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual DataSet IsTeachingCategoryUsedIncludingChildren(string paths)
        {
            string fileTypeSql = string.Empty;
            if (!string.IsNullOrWhiteSpace(paths))
            {
                string[] pathList = paths.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (pathList != null && pathList.Length > 0)
                {
                    fileTypeSql = " where ";
                    foreach (string path in pathList)
                    {
                        fileTypeSql += string.Format("FileType = '{0}' or ", path);
                    }
                    fileTypeSql = fileTypeSql.TrimEnd("or ".ToCharArray());
                }
            }

            RisDAL dataAccess = new RisDAL();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("select * from tTeaching {0}", fileTypeSql);
                DataTable dataTable = dataAccess.ExecuteQuery(sql.ToString());
                DataSet ds = new DataSet();
                ds.Tables.Add(dataTable);
                return ds;
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
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

        public virtual DataTable GetApplyDoctors()
        {
            RisDAL dataAccess = new RisDAL();
            DataTable dt = new DataTable();
            string sql = @"select B.ApplyDept,A.*  from tApplyDoctor A left join  tApplyDept B on A.ApplyDeptID = B.ID";
            try
            {
                dataAccess.ExecuteQuery(sql, dt);
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
            return dt;
        }

        #region Added by Blue for [RC607] - US17706
        public virtual DataTable GetExamNameByDomainSite(string domainName, string siteName, bool isDomain)
        {
            RisDAL dataAccess = new RisDAL();
            DataTable dt = new DataTable();
            string sql = isDomain ? string.Format("select ExamNameGuid, ExamName, ParentExamNameGuid from tExamName where Domain = '{0}' and Site = ''", domainName)
                : string.Format("select ExamNameGuid, ExamName, ParentExamNameGuid from tExamName where Domain = '{0}' and Site = '{1}'", domainName, siteName);
            try
            {
                dataAccess.ExecuteQuery(sql, dt);
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
            return dt;
        }

        public virtual DataTable GetCurrentDomainSiteExamName(string modalityType)
        {
            RisDAL dataAccess = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string sql = string.Format("select ExamNameGuid, ExamName, ParentExamNameGuid, Domain, Site from tExamName where Domain = '{0}' and Site = '{1}'", CommonGlobalSettings.Utilities.GetCurDomain(), CommonGlobalSettings.Utilities.GetCurSite());
                dataAccess.ExecuteQuery(sql, dt);
                if (dt == null || dt.Rows.Count == 0 || dt.Select(string.Format("ParentExamNameGuid='{0}'", modalityType)).Length == 0)
                {
                    sql = string.Format("select ExamNameGuid, ExamName, ParentExamNameGuid, Domain, Site from tExamName where Domain = '{0}' and Site = ''", CommonGlobalSettings.Utilities.GetCurDomain());
                    dataAccess.ExecuteQuery(sql, dt);
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
            return dt;
        }

        public virtual bool AddExamName(string examName, string parentGuid, string domain, string site)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = string.Format("insert into tExamName(ExamNameGuid, ExamName, ParentExamNameGuid, Domain, Site) values (NEWID(), '{0}', '{1}', '{2}', '{3}')", examName, parentGuid, domain, site);
                dataAccess.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual bool EditExamName(string guid, string examName)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = string.Format("update tExamName set ExamName = '{0}' where ExamNameGuid = '{1}'", examName, guid);
                dataAccess.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual bool DeleteExamName(string guid)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string strSqlIn = string.Empty;
                string[] guids = guid.Split('*');
                if (guids != null && guids.Length > 0)
                {
                    foreach (string id in guids)
                    {
                        strSqlIn += string.Format("'{0}', ", id);
                    }
                    strSqlIn = strSqlIn.Trim().TrimEnd(',');
                }

                if (!string.IsNullOrWhiteSpace(strSqlIn))
                {
                    string sql = string.Format("delete from tExamName where ExamNameGuid in ({0})", strSqlIn);
                    dataAccess.ExecuteNonQuery(sql);
                }
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual DataTable GetScanningTechByDomainSite(string modalityType, string domainName, string siteName, bool isDomain)
        {
            RisDAL dataAccess = new RisDAL();
            DataTable dt = new DataTable();
            string sql = isDomain ? string.Format("select ScanningTechGuid, ScanningTech, ParentScanningTechGuid from tScanningTech where Domain = '{0}' and Site = '' and ModalityType = '{1}'", domainName, modalityType)
                : string.Format("select ScanningTechGuid, ScanningTech, ParentScanningTechGuid from tScanningTech where Domain = '{0}' and Site = '{1}' and ModalityType = '{2}'", domainName, siteName, modalityType);
            try
            {
                dataAccess.ExecuteQuery(sql, dt);
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
            return dt;
        }

        public virtual DataTable GetCurrentDomainSiteScanningTech(string modalityType)
        {
            RisDAL dataAccess = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                string sql = string.Format("select ScanningTechGuid, ScanningTech, ParentScanningTechGuid, Domain, Site from tScanningTech where Domain = '{0}' and Site = '{1}' and ModalityType = '{2}'", CommonGlobalSettings.Utilities.GetCurDomain(), CommonGlobalSettings.Utilities.GetCurSite(), modalityType);
                dataAccess.ExecuteQuery(sql, dt);
                if (dt == null || dt.Rows.Count == 0)
                {
                    sql = string.Format("select ScanningTechGuid, ScanningTech, ParentScanningTechGuid, Domain, Site from tScanningTech where Domain = '{0}' and Site = '' and ModalityType = '{1}'", CommonGlobalSettings.Utilities.GetCurDomain(), modalityType);
                    dataAccess.ExecuteQuery(sql, dt);
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
            return dt;
        }

        public virtual bool AddScanningTech(string scanningTech, string parentGuid, string modalityType, string domain, string site)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = string.Format("insert into tScanningTech(ScanningTechGuid, ScanningTech, ParentScanningTechGuid, ModalityType, Domain, Site) values (NEWID(), '{0}', '{1}', '{2}', '{3}', '{4}')", scanningTech, parentGuid, modalityType, domain, site);
                dataAccess.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual bool EditScanningTech(string guid, string scanningTech)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = string.Format("update tScanningTech set ScanningTech = '{0}' where ScanningTechGuid = '{1}'", scanningTech, guid);
                dataAccess.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual bool DeleteScanningTech(string guid)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string strSqlIn = string.Empty;
                string[] guids = guid.Split('*');
                if (guids != null && guids.Length > 0)
                {
                    foreach (string id in guids)
                    {
                        strSqlIn += string.Format("'{0}', ", id);
                    }
                    strSqlIn = strSqlIn.Trim().TrimEnd(',');
                }

                if (!string.IsNullOrWhiteSpace(strSqlIn))
                {
                    string sql = string.Format("delete from tScanningTech where ScanningTechGuid in ({0})", strSqlIn);
                    dataAccess.ExecuteNonQuery(sql);
                }
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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
        #endregion

        #region Added by Blue for [RC570] - US16220
        public virtual DataTable GetEventTypes()
        {
            RisDAL dataAccess = new RisDAL();
            DataTable dt = new DataTable();
            string sql = string.Format("select EventID, EventTypeCode, Priority as EventTypePriority, Category as EventType from tHippaEventType where Domain = '{0}' and EventTypeCode in (select Value from tDictionaryValue where Tag = 60)", CommonGlobalSettings.Utilities.GetCurDomain());
            try
            {
                dataAccess.ExecuteQuery(sql, dt);
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
            return dt;
        }

        public virtual bool UpdateEventTypes(DataTable datasource)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                if (datasource != null && datasource.Rows.Count > 0)
                {
                    string sql = string.Empty;
                    foreach (DataRow dr in datasource.Rows)
                    {
                        sql = string.Format("update tHippaEventType set Priority = '{0}', Category = '{1}' where EventTypeCode = '{2}' and Domain = '{3}'", dr["EventTypePriority"].ToString(), dr["EventType"].ToString(), dr["EventTypeCode"].ToString(), CommonGlobalSettings.Utilities.GetCurDomain());
                        dataAccess.ExecuteNonQuery(sql);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(Convert.ToInt64(ModuleEnum.Oam_DA.ToString()), ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
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

        public virtual DataTable GetEventCategories()
        {
            RisDAL dataAccess = new RisDAL();
            DataTable dt = new DataTable();
            string sql = string.Format("select distinct Category from tHippaEventType where Domain = '{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
            try
            {
                dataAccess.ExecuteQuery(sql, dt);
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
            return dt;
        }
        #endregion
        #endregion

        #region IResourceDAO Section
        private const string SQLGetResourceList = "Select Modality, ModalityType From tModality Order by Modality";

        public DataSet GetResourceDataSet(string strSite)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet dataSet = new DataSet();

            string sql = "";
            if (string.IsNullOrWhiteSpace(strSite))
            {
                sql = SQLGetResourceList;
            }
            else if (!strSite.Contains("SiteAdmin"))
            {
                sql = "Select Modality, ModalityType, WorkStationIP From tModality where site='' or site is null or site = '" + strSite + "' Order by Modality";
            }
            else
            {
                #region Modified by Blue Chen for US20057, 11/13/2014
                string site = strSite.Split('#')[0];
                if (!string.IsNullOrWhiteSpace(site))
                {
                    string[] sites = site.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    string strFormattedSites = string.Format("'{0}'", string.Join("','", sites));
                    sql = "Select Modality, ModalityType, WorkStationIP From tModality where site in (" + strFormattedSites + ") or site='' or site is null Order by Modality";
                }
                else
                {
                    sql = "Select Modality, ModalityType, WorkStationIP From tModality where 1 <> 1";
                }
                #endregion
            }

            try
            {
                DataTable dataTable = dataAccess.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                dataTable.TableName = "ModalityName";
                dataSet.Tables.Add(dataTable);

                sql = "Select ModalityType From tModalityType";
                DataTable modalityTypeTable = dataAccess.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                modalityTypeTable.TableName = "ModalityType";
                dataSet.Tables.Add(modalityTypeTable);
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
            return dataSet;
        }

        public virtual ResourceModel QueryResource(string modalityName)
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

                    //IPAddress
                    model.WorkStationIP = row[table.Columns["WorkStationIP"]] as string;

                    //MaxLoad
                    model.MaxLoad = ((int)row[table.Columns["MaxLoad"]]).ToString();
                    //model.MaxLoad = Convert.ToString(Convert.ToInt16(row[table.Columns["MaxLoad"]]));


                    //Description
                    model.Description = row[table.Columns["Description"]] as string;

                    //BookingShowMode
                    model.BookingShowMode = (int)(row[table.Columns["BookingShowMode"]]);

                    //ApplyHaltPeriod
                    model.ApplyHaltPeriod = Convert.ToBoolean(row[table.Columns["ApplyHaltPeriod"]]);

                    //Start DateTime
                    model.BeginDt = Convert.ToDateTime(row[table.Columns["StartDt"]]);

                    //End DateTime
                    model.EndDt = Convert.ToDateTime(row[table.Columns["EndDt"]]);

                    //Site
                    model.Site = row[table.Columns["Site"]] as string;

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

        public bool AddResource(ResourceModel model)
        {
            model.Description = model.Description.Replace("'", "''");
            RisDAL dataAccess = new RisDAL();
            string sqlTemp = string.Format("select count(*) Modality from tModality where Modality = '{0}'", model.ModalityName);
            string sql = "Insert into tModality(ModalityGuid, ModalityType, Modality, Room, IPAddress, MaxLoad, Description,BookingShowMode,ApplyHaltPeriod,StartDt,EndDt,Domain,Site,WorkStationIP)";
            sql += " Values('" + Guid.NewGuid() + "','" + model.ModalityType + "','" + model.ModalityName + "','";
            sql += model.Room + "','" + model.IPAddress + "'," + Convert.ToInt32(model.MaxLoad) + ",'";
            sql += model.Description + "'," + model.BookingShowMode + "," + Convert.ToInt32(model.ApplyHaltPeriod) + ",'" + model.BeginDt + "','" + model.EndDt + "','" + model.Domain + "','" + model.Site + "','" + model.WorkStationIP + "')";
            try
            {

                if (Convert.ToInt32(dataAccess.ExecuteScalar(sqlTemp)) > 0)
                {
                    throw new Exception("ModalityExist");
                    return false;
                }

                if (dataAccess.ExecuteNonQuery(sql) > 0)
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

        public bool AddModalityType(ResourceModel model)
        {
            RisDAL dataAccess = new RisDAL();
            string sqlTemp = string.Format("select count(*) from tModalityType where ModalityType = '{0}'", model.ModalityType);
            string sql = "Insert into tModalityType(ModalityType, SOPClass,Domain)";
            sql += " Values('" + model.ModalityType + "','','" + model.Domain + "')";
            try
            {

                if (Convert.ToInt32(dataAccess.ExecuteScalar(sqlTemp)) > 0)
                {
                    throw new Exception("ModalityTypeExist");
                    return false;
                }

                if (dataAccess.ExecuteNonQuery(sql) > 0)
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

        public bool DeleteModalityType(ResourceModel model)
        {
            RisDAL dataAccess = new RisDAL();
            bool step1Ok = false;
            string sqlTemp = string.Format("select count(*) from tProcedureCode where ModalityType = '{0}'", model.ModalityType);
            string sqlTemp1 = string.Format("select count(*)from tModalityType A,tModality B,tRegProcedure C where A.ModalityType = B.ModalityType and B.ModalityType = C.ModalityType and A.ModalityType='{0}'", model.ModalityType);
            string sqlTemp2 = "Delete from tModality where ModalityType = '" + model.ModalityType + "' \n\r";
            sqlTemp2 += "Delete from tBookingTimeSync where ModalityType = '" + model.ModalityType + "' \n\r";
            sqlTemp2 += "Delete from tModalityTimeSlice where ModalityType = '" + model.ModalityType + "' \n\r";
            sqlTemp2 += "Delete from tWarningTime where ModalityType = '" + model.ModalityType + "' \n\r";
            sqlTemp2 += "Delete from tModalityType where ModalityType = '" + model.ModalityType + "' \n\r";
            try
            {

                if (Convert.ToInt32(dataAccess.ExecuteScalar(sqlTemp, RisDAL.ConnectionState.KeepOpen)) > 0)//exists modalitytype use in tprocedurecode
                {
                    throw new Exception("ModalityTypeUsedInProcedureCode");
                    return false;
                }

                if (Convert.ToInt32(dataAccess.ExecuteScalar(sqlTemp1, RisDAL.ConnectionState.KeepOpen)) > 0)//exists modality used in tregprocedure
                {
                    throw new Exception("ModalityUsedInRegProcedure");
                    return false;
                }

                if (dataAccess.ExecuteNonQuery(sqlTemp2) != -1)
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
                return false;
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

        public bool DeleteResource(ResourceModel model)
        {
            RisDAL dataAccess = new RisDAL();
            string sqlTemp = "Select count(*) from tRegProcedure where Modality = '" + model.ModalityName.Trim() + "'";
            string sql = "Delete From tModality Where ModalityGuid='" + model.ModalityGuid + "'";

            try
            {

                if (Convert.ToInt32(dataAccess.ExecuteScalar(sqlTemp)) > 0)
                {
                    throw new Exception("ModalityUsed");
                    return false;
                }


                if (dataAccess.ExecuteNonQuery(sql) > 0)
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

        public bool UpdateResource(ResourceModel model)
        {
            model.Description = model.Description.Replace("'", "''");
            RisDAL dataAccess = new RisDAL();
            string sqlTemp = string.Format("select count(*) from tModality where Modality = '{0}' and ModalityGuid <> '{1}'", model.ModalityName, model.ModalityGuid);
            string sql = "Update tModality set ModalityType='" + model.ModalityType + "',";
            sql += " Modality='" + model.ModalityName + "',";
            sql += " Room='" + model.Room + "',";
            sql += " IPAddress='" + model.IPAddress + "',";
            sql += " MaxLoad=" + model.MaxLoad + ",";
            sql += " Description='" + model.Description + "',";
            sql += " BookingShowMode = " + model.BookingShowMode.ToString() + ",";
            sql += " ApplyHaltPeriod = " + Convert.ToInt32(model.ApplyHaltPeriod).ToString() + ",";
            sql += " StartDt = '" + model.BeginDt.ToString() + "',";
            sql += " Site = '" + model.Site + "',";
            sql += " EndDt = '" + model.EndDt.ToString() + "',";
            sql += " WorkStationIP = '" + model.WorkStationIP.ToString() + "'";
            sql += " Where ModalityGuid='" + model.ModalityGuid + "'";
            try
            {

                if (Convert.ToInt32(dataAccess.ExecuteScalar(sqlTemp)) > 0)
                {

                    throw new Exception("ModalityExist");
                    return false;
                }

                if (dataAccess.ExecuteNonQuery(sql) > 0)
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
        #endregion

        #region IProcedureCodeDAO Section
        public virtual DataSet GetProcedureCodeList(string strDomain, string strSite)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet dataSet = new DataSet();

            try
            {
                //Get the Modality List
                string sql = "Select * From tModalityType";
                DataTable modalityTable = dataAccess.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                modalityTable.TableName = TableConst.ModalityType;
                dataSet.Tables.Add(modalityTable);

                //Get the Body Category List
                sql = "Select distinct BodyCategory as Text From tProcedureCode";
                DataTable bodyCategoryTable = dataAccess.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                bodyCategoryTable.TableName = TableConst.BodyCategory;
                dataSet.Tables.Add(bodyCategoryTable);

                //Get the FilmSpec 
                sql = "Select Text From tDictionaryValue Where Tag='" + TableConst.FilmSpecTag + "' and ((Tag not in (select distinct Tag from tDictionaryValue where Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') and (Site='' or Site is null)) or Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') ORDER BY OrderID";
                DataTable filmSpecTable = dataAccess.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                filmSpecTable.TableName = TableConst.FilmSpecTable;
                dataSet.Tables.Add(filmSpecTable);

                //Get all modality
                sql = "Select * From tModality";
                DataTable modalitytable = dataAccess.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                modalitytable.TableName = TableConst.ModalityTable;
                dataSet.Tables.Add(modalitytable);

                //Get All of the Procedure Codes
                sql = "Select ProcedureCode,Description,BodyCategory,Frequency,BodyPart,BodyPartFrequency,CheckingItem,CheckingItemFrequency," +
                    "EnglishDescription,ModalityType,Charge,Preparation,Duration,FilmSpec,FilmCount,ContrastName,ContrastDose," +
                    "ImageCount,ExposalCount,BookingNotice,ShortcutCode,Enhance,Effective,TechnicianWeight,RadiologistWeight,ApprovedRadiologistWeight,DefaultModality,Site From tProcedureCode where Domain='" + strDomain + "' and Externals=0 and site='" + strSite + "'";
                DataTable procedureCodeTable = dataAccess.ExecuteQuery(sql);
                procedureCodeTable.TableName = TableConst.ProcedureCode;
                dataSet.Tables.Add(procedureCodeTable);
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

            return dataSet;
        }

        public virtual DataSet GetProceTimeSliceDuration(string timeSliceDur)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet dataSet = new DataSet();

            try
            {
                int tDictionaryValue = 0;
                //int int_timeSliceDur = Convert.ToInt16(timeSliceDur);
                switch (timeSliceDur)
                {
                    case "10":
                        tDictionaryValue = 15;
                        break;
                    case "15":
                        tDictionaryValue = 18;
                        break;
                    case "5":
                        tDictionaryValue = 20;
                        break;
                }

                string sql = string.Format("select Value from tDictionaryValue where Tag={0} ORDER BY OrderID", tDictionaryValue);
                DataTable timeSliceDurtable = dataAccess.ExecuteQuery(sql);
                dataSet.Tables.Add(timeSliceDurtable);
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

            return dataSet;
        }

        public virtual bool AddProcedureCode(ProcedureCodeModel model)
        {
            RisDAL dataAccess = new RisDAL();

            try
            {

                string sql = string.Format("select count(*) from tprocedurecode where procedurecode='{0}'", model.ProcedureCode);
                Object obj = dataAccess.ExecuteScalar(sql);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    throw new Exception("Duplication procedure code!");
                }


                sql = "Insert into tProcedureCode(ProcedureCode, Description, EnglishDescription, ModalityType, BodyPart, ";
                sql += "CheckingItem, Charge, Preparation, Frequency,BodyPartFrequency,CheckingItemFrequency, BodyCategory, Duration, FilmSpec, ";
                sql += "FilmCount, ContrastName, ContrastDose, ImageCount, ExposalCount, BookingNotice, ShortcutCode,Enhance,Effective,Domain,TechnicianWeight,RadiologistWeight,ApprovedRadiologistWeight,DefaultModality,ClinicalModality,Puncture,Radiography,Site) ";
                sql += "Values('" + model.ProcedureCode + "',";
                sql += "'" + model.Description + "',";
                sql += "'" + model.EnglishDescription + "',";
                sql += "'" + model.ModalityType + "',";
                sql += "'" + model.BodyPart + "',";
                sql += "'" + model.CheckingItem + "',";
                sql += model.ChargeRate + ",";
                sql += "'" + model.Preparation + "',";
                sql += model.Frequency + ",";
                sql += model.BodyPartFrequency + ",";
                sql += model.CheckingItemFrequency + ",";
                sql += "'" + model.BodyCategory + "',";
                sql += model.Duration + ",";
                sql += "'" + model.FilmSpec + "',";
                sql += model.FilmCount + ",";
                sql += "'" + model.ContrastName + "',";
                sql += "'" + model.ContrastDose + "',";
                sql += model.ImageCount + ",";
                sql += model.ExposalCount + ",";
                sql += "'" + model.BookingNotice + "',";
                sql += "'" + model.ShortcutCode + "',";
                sql += model.Enhance + ",";
                sql += model.Effective + ",";
                sql += "'" + model.Domain + "',";
                sql += model.TechnicianWeight + ",";
                sql += model.RadiologistWeight + ",";
                sql += model.ApprovedRadiologistWeight + ",";
                sql += "'" + model.DefaultModality + "',";
                sql += "'" + model.ClinicalModality + "',";
                sql += "'" + model.Puncture + "',";
                sql += "'" + model.Radiography + "',";
                sql += "'" + model.Site + "')";

                StringBuilder temp = new StringBuilder();
                temp.AppendFormat("Select * From tBodySystemMap Where ModalityType='{0}' and BodyPart='{1}'",
                    model.ModalityType, model.BodyPart);
                DataTable dataTable = dataAccess.ExecuteQuery(temp.ToString());
                if (dataTable.Rows.Count <= 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("Insert into tBodySystemMap(ModalityType,BodyPart,ExamSystem) Values('{0}','{1}','{2}')",
                        model.ModalityType, model.BodyPart, model.ExamSystem);
                    dataAccess.BeginTransaction();
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    dataAccess.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                    dataAccess.CommitTransaction();
                    if (model.UseOldCharge == 0 && model.Ds != null)
                    {
                        UpdateChargeTypeFee(model);
                    }
                    return true;
                }
                else
                {
                    dataAccess.BeginTransaction();
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    dataAccess.CommitTransaction();
                    if (model.UseOldCharge == 0 && model.Ds != null)
                    {
                        UpdateChargeTypeFee(model);
                    }
                    return true;

                }
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

            return false;
        }

        public virtual int DeleteProcedureCode(string procedureCode, string site)
        {
            RisDAL dataAccess = new RisDAL();

            try
            {
                //Used by registration?
                string sql = string.Format("Select * From tRegProcedure where ProcedureCode='{0}'", procedureCode);
                DataTable dataTable = dataAccess.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                if (dataTable.Rows.Count > 0)
                {

                    return 1;
                }

                //Used by registration in archive db?
                sql = string.Format("Select * From  RISArchive..tRegProcedure where ProcedureCode='{0}' ", procedureCode);
                dataTable = dataAccess.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                if (dataTable.Rows.Count > 0)
                {

                    return 1;
                }



                //not be used and deleted from DB
                StringBuilder deleteSB = new StringBuilder();
                deleteSB.AppendFormat("Delete From tProcedureCode Where ProcedureCode='{0}'", procedureCode);
                dataAccess.ExecuteNonQuery(deleteSB.ToString(), RisDAL.ConnectionState.CloseOnExit);
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

            return 0;
        }

        public virtual bool ModifyProcedureCode(ProcedureCodeModel model)
        {
            RisDAL dataAccess = new RisDAL();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("Update tProcedureCode set Description='{0}', ModalityType='{1}',BodyPart='{2}', CheckingItem='{3}', Preparation='{4}', EnglishDescription='{5}', Frequency='{6}',BodyPartFrequency='{22}',CheckingItemFrequency='{23}',BodyCategory='{7}', Duration={8}, FilmSpec='{9}', FilmCount={10}, ContrastName='{11}',ContrastDose='{12}',ImageCount={13}, ExposalCount={14},BookingNotice='{15}', ShortcutCode='{16}',Enhance = {18}, Effective={19},Charge={20},TechnicianWeight ={21},RadiologistWeight ={24},ApprovedRadiologistWeight={25},DefaultModality='{26}',ClinicalModality='{28}',Puncture='{29}',Radiography='{30}'  Where ProcedureCode='{17}' and site='{27}'",
                    model.Description, model.ModalityType, model.BodyPart, model.CheckingItem, model.Preparation, model.EnglishDescription, model.Frequency, model.BodyCategory, model.Duration, model.FilmSpec, model.FilmCount, model.ContrastName, model.ContrastDose, model.ImageCount, model.ExposalCount, model.BookingNotice, model.ShortcutCode, model.ProcedureCode, model.Enhance, model.Effective, model.ChargeRate, model.TechnicianWeight, model.BodyPartFrequency, model.CheckingItemFrequency, model.RadiologistWeight, model.ApprovedRadiologistWeight, model.DefaultModality, model.Site, model.ClinicalModality, model.Puncture, model.Radiography);

                if (dataAccess.ExecuteNonQuery(sql.ToString()) > 0)
                {
                    if (model.UseOldCharge == 0 && model.Ds != null)//use tcharge table
                    {
                        if (UpdateChargeTypeFee(model))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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

        /// <summary>
        /// UpdateChargeTypeFee
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual bool UpdateChargeTypeFee(ProcedureCodeModel model)
        {
            RisDAL dataAccess = new RisDAL();
            dataAccess.BeginTransaction();
            try
            {
                string sql = "";
                int resultCount = 0;
                if (model.Ds == null && model.Ds.Tables.Count == 0)
                {
                    return false;
                }
                if (model.Ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in model.Ds.Tables[0].Rows)
                    {
                        sql = "Select count(*) from tCharge where ProcedureCode = '" + model.ProcedureCode + "' and ChargeType = '" + dr["ChargeType"].ToString() + "'";
                        resultCount = (int)dataAccess.ExecuteScalar(sql.ToString(), RisDAL.ConnectionState.KeepOpen);
                        if (resultCount > 0)//exists
                        {
                            decimal charge = dr["Charge"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Charge"]);
                            sql = string.Format("Update tCharge set Charge = '{0}' where ProcedureCode = '{1}' and ChargeType = '{2}'", charge, model.ProcedureCode, dr["ChargeType"].ToString());
                        }
                        else
                        {
                            decimal charge = dr["Charge"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Charge"]);
                            sql = string.Format("Insert into tCharge (ChargeGuid,ProcedureCode,ChargeType,Charge) values('{0}','{1}','{2}','{3}')", Guid.NewGuid().ToString(), model.ProcedureCode, dr["ChargeType"].ToString(), charge);
                        }
                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    dataAccess.CommitTransaction();
                    return true;
                }
            }
            catch (Exception e)
            {
                dataAccess.RollbackTransaction();
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

        public virtual DataSet QueryExamSystem(string modalityType, string bodyPart)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet dataSet = new DataSet();

            try
            {
                string sql = "Select ExamSystem From tBodySystemMap Where ModalityType='" + modalityType + "' ";
                sql += " And BodyPart='" + bodyPart + "'";
                DataTable dataTable = dataAccess.ExecuteQuery(sql);
                dataTable.TableName = TableConst.BodySystemMap;
                dataSet.Tables.Add(dataTable);
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

            return dataSet;
        }

        public virtual DataSet QueryAllExamSystem()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet dataSet = new DataSet();

            try
            {
                string sql = "Select Distinct ExamSystem From tBodySystemMap";
                DataTable dataTable = dataAccess.ExecuteQuery(sql);
                dataTable.TableName = TableConst.BodySystemMap;
                dataSet.Tables.Add(dataTable);
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

            return dataSet;
        }

        public virtual bool QueryBodyCategory(string categoryName, string description, string shortcutCode)
        {
            RisDAL dataAccess = new RisDAL();

            try
            {
                string sql = "Select distinct BodyCategory From tProcedureCode";
                DataTable dataTable = dataAccess.ExecuteQuery(sql);
                foreach (DataRow row in dataTable.Rows)
                {
                    //CategoryName
                    string categoryNameColumn = row[dataTable.Columns["BodyCategory"]].ToString();

                    //Description
                    string descriptionColumn = row[dataTable.Columns["BodyCategory"]].ToString();

                    //ShortcutCode
                    // string shortcutCodeColumn = row[dataTable.Columns["ShortcutCode"]].ToString();

                    if (categoryNameColumn.Equals(categoryName) || descriptionColumn.Equals(description))
                    {
                        return true;
                    }

                    //  if (shortcutCodeColumn != null && shortcutCode != string.Empty && shortcutCodeColumn.Equals(shortcutCode))
                    {
                        //      return true;
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

            return false;
        }

        public virtual bool AddBodyCategory(string tag, string categoryName, string description, string shortcutCode)
        {
            RisDAL dataAccess = new RisDAL();

            try
            {
                string sql = "Insert into tDictionaryValue(Tag, Value, Text, IsDefault, ShortcutCode) ";
                sql += " Values('" + tag + "',";
                sql += "'" + categoryName + "',";
                sql += "'" + description + "',";
                sql += 0 + ",";
                sql += "'" + shortcutCode + "')";
                if (dataAccess.ExecuteNonQuery(sql) > 0)
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

        public virtual bool IsBodyPartExist(string modalityType, string bodyPart, string examSystem)
        {
            RisDAL dataAccess = new RisDAL();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("Select * From tBodySystemMap Where ModalityType='{0}'And BodyPart='{1}' And ExamSystem='{2}'",
                    modalityType, bodyPart, examSystem);
                DataTable dataTable = dataAccess.ExecuteQuery(sql.ToString());
                if (dataTable.Rows.Count > 0)
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

        public virtual bool AddBodyPart(string modalityType, string bodyPart, string examSystem, string domain)
        {
            RisDAL dataAccess = new RisDAL();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("Insert Into tBodySystemMap(ModalityType,BodyPart,ExamSystem,Domain) Values('{0}','{1}','{2}','{3}')",
                    modalityType, bodyPart, examSystem, domain);
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

        public virtual DataSet QueryBodyPart(string modalityType, string site)
        {
            RisDAL dataAccess = new RisDAL();

            try
            {
                StringBuilder sb = new StringBuilder();
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                #region Modified by Blue Chen for US19895, 10/31/2014
                sb.AppendFormat("Select Distinct BodyPart From tBodySystemMap Where ModalityType='{0}' and Site = '{1}'", modalityType, site);
                dataTable = dataAccess.ExecuteQuery(sb.ToString());
                if (dataTable.Rows.Count == 0)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.AppendFormat("Select Distinct BodyPart From tProcedureCode Where ModalityType='{0}'", modalityType);
                    dataTable = dataAccess.ExecuteQuery(sql.ToString());

                    sb.Clear();
                    DataTable dataTable2 = new DataTable();
                    sb.AppendFormat("Select Distinct BodyPart From tBodySystemMap Where ModalityType='{0}' and (Site = '' or Site is null)", modalityType);
                    dataTable2 = dataAccess.ExecuteQuery(sb.ToString());

                    foreach (DataRow row in dataTable2.Rows)
                    {
                        string[] dataRow = new string[1];
                        dataRow[0] = row[dataTable2.Columns["BodyPart"]] as string;
                        dataTable.Rows.Add(dataRow);
                    }
                }
                #endregion

                dataTable.TableName = TableConst.BodyPartTable;

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

        public virtual DataSet QueryCheckingItem(string modalityType)
        {
            RisDAL dataAccess = new RisDAL();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("Select Distinct CheckingItem From tProcedureCode Where ModalityType='{0}'", modalityType);
                DataSet dataSet = new DataSet();
                DataTable dataTable = dataAccess.ExecuteQuery(sql.ToString());


                dataTable.TableName = "CheckingItem";

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

        public virtual DataSet GetSiteProcedureCode(string site)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                DataSet dataSet = new DataSet();
                string strSQL = string.Format("SELECT * from tProcedureCode where site='{0}'", site);

                DataTable dt = dataAccess.ExecuteQuery(strSQL);
                if (dt.Rows.Count == 0)
                {
                    //Get procedurecode from domain
                    strSQL = string.Format("SELECT * from tProcedureCode where site=''");

                    dt = dataAccess.ExecuteQuery(strSQL);

                }

                dt.TableName = "ProcedureCode";

                dataSet.Tables.Add(dt);

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

        public virtual DataSet QueryChargeTypeFee(string procedureCode)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet dataSet = new DataSet();

            try
            {
                //Get the Modality List
                string sql = "select * from tdictionaryvalue where tag = 52 and ((Tag not in (select distinct Tag from tDictionaryValue where Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') and (Site='' or Site is null)) or Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') ORDER BY OrderID";
                DataTable ChargeType = dataAccess.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                ChargeType.TableName = "ChargeType";
                dataSet.Tables.Add(ChargeType);

                sql = "select distinct * from tcharge where ProcedureCode = '" + procedureCode + "'";
                DataTable Charge = dataAccess.ExecuteQuery(sql);
                Charge.TableName = "Charge";
                dataSet.Tables.Add(Charge);

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

            return dataSet;
        }

        public virtual bool ModifyProcedureCodeFrequency(ProcedureCodeModel model)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                List<string> sqlList = new List<string>(3);
                if (!string.IsNullOrEmpty(model.BodyCategory))
                {
                    sqlList.Add(string.Format("update tProcedureCode set Frequency = {0} where ModalityType = '{1}' and BodyCategory = '{2}'", model.Frequency, model.ModalityType, model.BodyCategory));
                }
                if (!string.IsNullOrEmpty(model.BodyPart))
                {
                    sqlList.Add(string.Format("update tProcedureCode set BodyPartFrequency = {0} where ModalityType = '{1}' and BodyCategory = '{2}' and BodyPart = '{3}'", model.BodyPartFrequency, model.ModalityType, model.BodyCategory, model.BodyPart));
                }
                if (!string.IsNullOrEmpty(model.CheckingItem))
                {
                    sqlList.Add(string.Format("update tProcedureCode set CheckingItemFrequency = {0} where ModalityType = '{1}' and BodyCategory = '{2}' and BodyPart = '{3}' and CheckingItem = '{4}'", model.CheckingItemFrequency, model.ModalityType, model.BodyCategory, model.BodyPart, model.CheckingItem));
                }

                foreach (string sql in sqlList)
                {
                    if (dataAccess.ExecuteNonQuery(sql) == 0)
                    {
                        return false;
                    }
                }
                return true;
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

        public virtual bool Copy2Site(string site, string domain)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {

                Object obj = dataAccess.ExecuteScalar(string.Format("select alias from tsitelist where site='{0}'", site));
                if (obj == null)
                {
                    throw new Exception("Alias of site is null");
                }
                string strSiteAlias = Convert.ToString(obj);

                string strSQL = string.Format(" if not exists(select * from tprocedurecode where site='{0}') ", site) +
                        string.Format(" INSERT INTO dbo.tProcedureCode ( ProcedureCode, Description, EnglishDescription, ModalityType, BodyPart, CheckingItem, Charge, Preparation, Frequency, BodyCategory, Duration, FilmSpec, FilmCount, ContrastName, ContrastDose, ImageCount, ExposalCount, BookingNotice, ShortcutCode, Enhance, ApproveWarningTime, Effective, [Domain], Externals, BodypartFrequency, CheckingItemFrequency, TechnicianWeight, RadiologistWeight, ApprovedRadiologistWeight, DefaultModality, Site ) " +
                                "select procedurecode+'_{0}', Description, EnglishDescription, ModalityType, BodyPart, CheckingItem, Charge, Preparation, Frequency, BodyCategory, Duration, FilmSpec, FilmCount, ContrastName, ContrastDose, ImageCount, ExposalCount, BookingNotice, ShortcutCode, Enhance, ApproveWarningTime, Effective, [Domain], Externals, BodypartFrequency, CheckingItemFrequency, TechnicianWeight, RadiologistWeight, ApprovedRadiologistWeight, DefaultModality, '{1}' as site from tprocedurecode where site=''", strSiteAlias, site);

                dataAccess.ExecuteNonQuery(strSQL);
                return true;
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
        public virtual bool Delall4Site(string site, string domain)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {

                string strSQL = string.Format(" select count(*) from tregprocedure where procedurecode in(select procedurecode from tprocedurecode where site='{0}')  ", site);
                Object obj = dataAccess.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    throw new Exception("Can not delete the used procedurecode");
                }


                strSQL = string.Format(" select count(*) from RISArchive..tregprocedure where procedurecode in(select procedurecode from tprocedurecode where site='{0}')  ", site);
                obj = dataAccess.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    throw new Exception("Can not delete the used procedurecode");
                }

                strSQL = string.Format(" delete from tprocedurecode where site='{0}' ", site);

                dataAccess.ExecuteNonQuery(strSQL);
                return true;
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

        #endregion

        #region IScheduleDAO Section
        public virtual DataSet QueryWorkTimeList()
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

        public virtual int GetStepLength()
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("Select Value from tSystemProfile Where Name='ItemDuration' And ModuleID='0300' and Domain='{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
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

        public virtual DataSet QueryScheduledModalityList(DateTime beginTime, DateTime endTime)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                DateTime beginTimeInQuery = Convert.ToDateTime(beginTime.ToLongDateString());
                DateTime endTimeInQuery = Convert.ToDateTime(endTime.ToLongDateString());

                string sql = "Select ModalityGuid From tModalityPlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='" + beginTimeInQuery + "'";
                sql += " And cast(convert(varchar(10), StartDt, 120) as datetime)<'" + endTimeInQuery.AddDays(1) + "'";
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

        public virtual int AddWorkTime(WorkTimeModel model)
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
                sql.AppendFormat("Insert into tWorkTime(WTGuid, WorkTimeName, StartDt, EndDt) Values('{0}','{1}','{2}','{3}')",
                    Guid.NewGuid(), model.WorkTimeName, Convert.ToDateTime(model.BeginTime), Convert.ToDateTime(model.EndTime));
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

        public virtual int ModifyWorkTime(WorkTimeModel model)
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
                sql.AppendFormat("Update tWorkTime Set StartDt='{0}',EndDt='{1}', WorkTimeName='{2}' Where WTGuid='{3}'",
                    Convert.ToDateTime(model.BeginTime), Convert.ToDateTime(model.EndTime), model.WorkTimeName, model.Guid);
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

        public virtual bool DeleteWorkTime(WorkTimeModel model)
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

        public virtual DataSet QueryEmployeeList(string type)
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

        public virtual DataSet QueryScheduledEmployeeList(DateTime beginTime, DateTime endTime)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = "Select UserGuid From tEmployeePlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='" + beginTime + "'";
                sql += " And cast(convert(varchar(10), StartDt, 120) as datetime)<'" + endTime.AddDays(1) + "'";
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

        public virtual bool AddEmployeeSchedule(EmployeeScheduleModel model)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();

                //Delete the existed schedules
                string sql = "Delete From tEmployeePlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='" + model.BeginTime + "'";
                sql += " And cast(convert(varchar(10), StartDt, 120) as datetime)<'" + model.EndTime.AddDays(1) + "'";
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

                                    sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateName, TemplateMark) Values('{0}','{1}','{2}','{3}','{4}',{5})",
                                        Guid.NewGuid(), userGuid, beginTimeInDB, endTimeInDB, model.TemplateName, templateMark);
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

                                    sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateName, TemplateMark) Values('{0}','{1}','{2}','{3}','{4}',{5})",
                                        Guid.NewGuid(), userGuid, beginTimeInDB, endTimeInDB, model.TemplateName, templateMark);
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

        public virtual bool CopyEmployeeSchedule(CopyScheduleModel model)
        {
            List<string> listSQL = new List<string>();
            RisDAL dataAccess = new RisDAL();
            try
            {
                //Delete the existed schedules
                string sql = "Delete From tEmployeePlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='" + model.BeginTime + "'";
                sql += " And cast(convert(varchar(10), StartDt, 120) as datetime)<'" + model.EndTime.AddDays(1) + "'";
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

                        sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateMark) Values('{0}','{1}','{2}','{3}','{4}')",
                            Guid.NewGuid(), userGuid, startDateTime, endDateTime, 0);
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

        public virtual bool ModifySchedule(CopyScheduleModel model)
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
                    string sql = "Delete From tEmployeePlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='" + beginTime + "'";
                    sql += " And cast(convert(varchar(10), StartDt, 120) as datetime)<'" + endTime.AddDays(1) + "'";
                    sql += " And UserGuid='" + userGuid + "'";
                    listSQL.Add(sql);

                    //Add the new schedules
                    foreach (DataRow row in model.Schedules.Rows)
                    {
                        DateTime startDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.StartDateColumn]] as string);
                        DateTime endDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.EndDateColumn]] as string);
                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateMark) Values('{0}','{1}','{2}','{3}','{4}')",
                            Guid.NewGuid(), userGuid, startDateTime, endDateTime, 0);
                        listSQL.Add(sb.ToString());
                    }
                }
                else if (model.ScheduleType.Equals("Template"))
                {
                    //Delete the existed schedules
                    string sql = "Delete From tEmployeePlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='" + beginTime + "'";
                    sql += " And cast(convert(varchar(10), StartDt, 120) as datetime)<'" + endTime.AddDays(1) + "'";
                    sql += " And TemplateName='" + model.Employees[0] + "'";
                    listSQL.Add(sql);

                    //Add the new schedules
                    foreach (DataRow row in model.Schedules.Rows)
                    {
                        DateTime startDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.StartDateColumn]] as string);
                        DateTime endDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.EndDateColumn]] as string);
                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat("Insert into tEmployeePlan(PlanGuid, UserGuid, StartDt, EndDt, TemplateName, TemplateMark) Values('{0}','{1}','{2}','{3}','{4}',{5})",
                            Guid.NewGuid(), Guid.NewGuid(), startDateTime, endDateTime, model.Employees[0], 1);
                        listSQL.Add(sb.ToString());
                    }
                }
                else
                {
                    string queryModalityGuid = "Select ModalityGuid from tModality where Modality='" + model.Modality + "'";
                    string modalityGuid = Convert.ToString(dataAccess.ExecuteScalar(queryModalityGuid));

                    //Delete the existed schedules
                    string sql = "Delete From tModalityPlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='" + beginTime + "'";
                    sql += " And cast(convert(varchar(10), StartDt, 120) as datetime)<'" + endTime.AddDays(1) + "'";
                    sql += " And ModalityGuid='" + modalityGuid + "'";
                    listSQL.Add(sql);

                    //Add the new schedules
                    foreach (DataRow row in model.Schedules.Rows)
                    {
                        DateTime startDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.StartDateColumn]] as string);
                        DateTime endDateTime = Convert.ToDateTime(row[model.Schedules.Columns[TableConst.EndDateColumn]] as string);
                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat("Insert into tModalityPlan(MPGuid, ModalityGuid, StartDt, EndDt) Values('{0}','{1}','{2}','{3}')",
                            Guid.NewGuid(), modalityGuid, startDateTime, endDateTime);
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

        /// <summary>
        /// Query the schedules of a specified user.
        /// </summary>
        /// <param name="beginTime">The begin time of the schedules.</param>
        /// <param name="endTime">The end time of the schedules.</param>
        /// <param name="name">The login name of the user or the template name.</param>
        /// <param name="isTemplate">Whether it is a template or not.</param>
        /// <returns>A DataSet which contains the schedules.</returns>
        public virtual DataSet QuerySchedule(DateTime beginTime, DateTime endTime, string name, bool isTemplate)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = "Select * From tEmployeePlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='" + beginTime + "'";
                sql += " And cast(convert(varchar(10), StartDt, 120) as datetime)<'" + endTime.AddDays(1) + "'";
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

        public virtual DataSet QueryModalitySchedule(DateTime beginTime, DateTime endTime, string modality)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                DateTime beginTimeInQuery = Convert.ToDateTime(beginTime.ToLongDateString());
                DateTime endTimeInQuery = Convert.ToDateTime(endTime.ToLongDateString());

                string sql = "Select * From tModalityPlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='" + beginTimeInQuery + "'";
                sql += " And cast(convert(varchar(10), StartDt, 120) as datetime)<'" + endTimeInQuery.AddDays(1) + "'";

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

        public virtual bool DeleteSchedule(DateTime beginTime, DateTime endTime, string name, bool isTemplate, string type)
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
                    sql.AppendFormat("Delete From tEmployeePlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='{0}' And cast(convert(varchar(10), StartDt, 120) as datetime)<'{1}'" + temp, beginTime, endTime.AddDays(1));
                }
                else
                {
                    string sqlModalityGuid = string.Format("select ModalityGuid from tModality where Modality = '{0}'", name);
                    string strModalityGuid = Convert.ToString(dataAccess.ExecuteScalar(sqlModalityGuid));
                    sql.AppendFormat("Delete From tModalityPlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='{0}' And cast(convert(varchar(10), StartDt, 120) as datetime)<'{1}' and ModalityGuid='{2}'", beginTime, endTime.AddDays(1), strModalityGuid);
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

        public virtual DataSet QueryAvailableEmployeeList(ModalityScheduleModel model)
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
                sql = string.Format("Select UserGuid From tRole2User Where RoleName='Techinician' and Domain='{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
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
                sql = string.Format("Select UserGuid From tRole2User Where RoleName='Nurse' and Domain='{0}'", CommonGlobalSettings.Utilities.GetCurDomain());
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

        public virtual bool AddModalitySchedule(ModalityScheduleModel model)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                DateTime beginTime = Convert.ToDateTime(model.BeginTime.ToLongDateString());
                DateTime endTime = Convert.ToDateTime(model.EndTime.ToLongDateString());
                string modalityGuid = GetModalityGuidByName(model.Modality);

                dataAccess.BeginTransaction();

                //Delete the existed schedules
                string sql = "Delete From tModalityPlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='" + beginTime + "'";
                sql += " And cast(convert(varchar(10), StartDt, 120) as datetime)<'" + endTime.AddDays(1) + "'";
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
                                sb.AppendFormat("Insert into tModalityPlan(MPGuid, ModalityGuid, StartDt, EndDt, DoctorGuid, TechnicianGuid, NurseGuid) Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                    Guid.NewGuid(), modalityGuid, beginTimeInDB, endTimeInDB, model.RadiologistGuid, model.TechnicianGuid, model.NurseGuid);
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
                                sb.AppendFormat("Insert into tModalityPlan(MPGuid, ModalityGuid, StartDt, EndDt, DoctorGuid, TechnicianGuid, NurseGuid) Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                    Guid.NewGuid(), modalityGuid, beginTimeInDB, endTimeInDB, model.RadiologistGuid, model.TechnicianGuid, model.NurseGuid);
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
                    string sql = "Select * From tEmployeePlan Where cast(convert(varchar(10), StartDt, 120) as datetime)>='" + beginTime + "'";
                    sql += " And cast(convert(varchar(10), StartDt, 120) as datetime)<'" + endTime.AddDays(1) + "'";
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

        /// <summary>
        /// Compare to show whether the compared time will cover the source time.
        /// </summary>
        /// <param name="beginTimeSource"></param>
        /// <param name="endTimeSource"></param>
        /// <param name="beginTimeCompare"></param>
        /// <param name="endTimeCompare"></param>
        /// <returns>true or false</returns>
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
            //May be the Work Time Rangle can be overlapped.So we comment it now.

            //string beginTimeString = model.BeginTime;
            //string endTimeString = model.EndTime;
            //DataSet dataSet = QueryWorkTimeList();
            //if(dataSet == null)
            //{
            //    return false;
            //}

            //DataTable dataTable = dataSet.Tables[0];
            //foreach (DataRow row in dataTable.Rows)
            //{
            //    string workTimeName = row[dataTable.Columns[TableConst.WorkTimeNameColumn]] as string;
            //    if(workTimeName.Equals(model.WorkTimeName))
            //    {
            //        continue;
            //    }

            //    DateTime beginDateTimeTemp = (DateTime)row[dataTable.Columns[TableConst.StartDateColumn]];
            //    DateTime endDateTimeTemp = (DateTime)row[dataTable.Columns[TableConst.EndDateColumn]];
            //    DateTime beginTime = Convert.ToDateTime(beginTimeString);
            //    DateTime endTime = Convert.ToDateTime(endTimeString);
            //    DateTime beginTimeInDB = Convert.ToDateTime(beginDateTimeTemp.ToShortTimeString());
            //    DateTime endTimeInDB = Convert.ToDateTime(endDateTimeTemp.ToShortTimeString());
            //    if(endTime <= beginTimeInDB || beginTime >= endTimeInDB)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}

            return false;
        }


        #endregion

        #region WorkingCalendar section

        public virtual DataSet GetWrokingCalendarAllSpecialDays()
        {
            try
            {
                using (RisDAL dataAccess = new RisDAL())
                {
                    DataSet dataSet = new DataSet();

                    string sql = "select date,dateType,dateDesp,Modality,Site from tWorkingCalendar";
                    DataTable dataTable = dataAccess.ExecuteQuery(sql);
                    dataTable.TableName = TableConst.Calendar;
                    dataSet.Tables.Add(dataTable);
                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Get Special days
        /// </summary>
        /// <param name="modality">if modality is empty, retrive only global days;
        /// otherwise, return days of the specified modality and global days</param>
        /// <returns></returns>
        public virtual DataSet GetWrokingCalendarSpecialDays(string modality, string site)
        {
            try
            {
                using (RisDAL dataAccess = new RisDAL())
                {
                    DataSet dataSet = new DataSet();

                    string sql = "select date,dateType,dateDesp,Modality,Site from tWorkingCalendar where Modality =@Modality"
                        + " Union select date,dateType,dateDesp,Modality,Site from tWorkingCalendar "
                        + " where (Modality = '' and Site = @Site) and "
                        + " date not in (select date from tWorkingCalendar where Modality = @Modality)"
                        + " Union select date,dateType,dateDesp,Modality,Site from tWorkingCalendar "
                        + " where (Modality = '' and (Site = '' or Site is null)) and "
                        + " date not in (select date from tWorkingCalendar where Modality = @Modality or (Modality = '' and Site = @Site))";

                    if (string.IsNullOrWhiteSpace(modality))
                    {
                        sql = "select date,dateType,dateDesp,Modality,Site from tWorkingCalendar "
                        + " where (Modality = '' and Site = @Site)"
                        + " Union select date,dateType,dateDesp,Modality,Site from tWorkingCalendar "
                        + " where (Modality = '' and (Site = '' or Site is null)) and "
                        + " date not in (select date from tWorkingCalendar where Modality = '' and Site = @Site)";
                    }

                    dataAccess.Parameters.AddVarChar("@Modality", modality);
                    dataAccess.Parameters.AddVarChar("@Site", site);
                    DataTable dataTable = dataAccess.ExecuteQuery(sql);
                    dataTable.TableName = TableConst.Calendar;
                    dataSet.Tables.Add(dataTable);
                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(ex.Message);
            }
            return null;
        }

        public virtual void SaveCalendarSpeicalDays(SpecialDayCollection sDayCol)
        {
            try
            {
                using (RisDAL dataAccess = new RisDAL())
                {
                    string sqlDelete = "";
                    string sqlInsert = "Insert into tWorkingCalendar(Date,DateType,DateDesp,Modality,Domain,Site)"
                                        + " values(@date,@dateType,@dateDesp,@Modality,@domain,@Site)";
                    string days = "";
                    if (sDayCol.SpecialDays.Count == 0)
                        return;

                    dataAccess.BeginTransaction();
                    for (int i = 0; i < sDayCol.SpecialDays.Count; i++)
                    {
                        sqlDelete = "delete from tWorkingCalendar where date =@date and Modality = @Modality";
                        if (string.IsNullOrWhiteSpace(sDayCol.SpecialDays[i].Modality))
                        {
                            if (string.IsNullOrWhiteSpace(sDayCol.SpecialDays[i].Site))
                            {
                                sqlDelete = "delete from tWorkingCalendar where date =@date and Modality = '' and (Site = '' or Site is null)";
                            }
                            else
                            {
                                sqlDelete = "delete from tWorkingCalendar where date =@date and Modality = '' and Site = @Site";
                            }
                        }
                        dataAccess.Parameters.Clear();
                        dataAccess.Parameters.AddDateTime("@Date", sDayCol.SpecialDays[i].Date);
                        dataAccess.Parameters.AddVarChar("@Modality", sDayCol.SpecialDays[i].Modality);
                        dataAccess.Parameters.AddVarChar("@Site", sDayCol.SpecialDays[i].Site);
                        dataAccess.ExecuteNonQuery(sqlDelete, RisDAL.ConnectionState.KeepOpen);
                    }

                    string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
                    foreach (SpecialDay sDay in sDayCol.SpecialDays)
                    {
                        dataAccess.Parameters.Clear();
                        dataAccess.Parameters.AddDateTime("@Date", sDay.Date);
                        dataAccess.Parameters.AddInt("DateType", sDay.DateType);
                        dataAccess.Parameters.AddVarChar("@DateDesp", sDay.Description);
                        dataAccess.Parameters.AddVarChar("@Modality", sDay.Modality);
                        dataAccess.Parameters.AddVarChar("@Domain", strDomain);
                        dataAccess.Parameters.AddVarChar("@Site", sDay.Site);
                        dataAccess.ExecuteNonQuery(sqlInsert, RisDAL.ConnectionState.KeepOpen);
                    }
                    dataAccess.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(ex.Message);
            }
        }

        public virtual void DeleteCalendarSpecialDays(SpecialDayCollection sDayCol)
        {
            try
            {
                using (RisDAL dataAccess = new RisDAL())
                {
                    dataAccess.BeginTransaction();
                    foreach (SpecialDay sDay in sDayCol.SpecialDays)
                    {
                        string sqlDelete = "Delete from tWorkingCalendar where Date=@Date and Modality = @Modality";
                        if (string.IsNullOrWhiteSpace(sDay.Modality))
                        {
                            if (string.IsNullOrWhiteSpace(sDay.Site))
                            {
                                sqlDelete = "delete from tWorkingCalendar where date =@Date and Modality = '' and (Site = '' or Site is null)";
                            }
                            else
                            {
                                sqlDelete = "delete from tWorkingCalendar where date =@Date and Modality = '' and Site = @Site";
                            }
                        }
                        dataAccess.Parameters.Clear();
                        dataAccess.Parameters.AddDateTime("@Date", sDay.Date);
                        dataAccess.Parameters.AddVarChar("@Modality", sDay.Modality);
                        dataAccess.Parameters.AddVarChar("@Site", sDay.Site);
                        dataAccess.ExecuteNonQuery(sqlDelete, RisDAL.ConnectionState.KeepOpen);
                    }
                    dataAccess.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region IHippaQueryDAO Section

        /// <summary>
        /// Name:HippaQuery
        /// Function:Query the detieal information about Hippa recored stored DB.
        /// Input: hippaModel -- HippaModel include model and messagename
        /// Output:DataSet holding the existing hippa record in the database
        /// Return:DataSet object if Success, Null if fail
        /// </summary>
        /// <param name="hippaModel">hippaModel</param>
        /// <returns>dataset or null</returns>
        public virtual DataSet HippaQuery(HippaModel hippaModel)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                bool flag = false;

                ArrayList userName = hippaModel.UserName;
                ArrayList eventID = hippaModel.EventID;
                ArrayList eventTypeCode = hippaModel.EventTypeCode;
                #region Added by Blue for [RC507] - US16220, 07/17/2014
                ArrayList eventCategory = hippaModel.EventCategory;
                string strEventCategorySql = string.Empty;
                #endregion
                DateTime eventDT = hippaModel.EventDT;
                DateTime eventBeginDt = hippaModel.EventBeginDt;
                DateTime eventEndDt = hippaModel.EventEndDt;
                int duringDays = hippaModel.DuringDays;

                int AllRowCount = 0;    //The sum count of hippa record in DB
                int PageRowCount = 0;   //How many row will display in current page
                int PageSize = 0;       //the total page number
                int PageSizeIndex = 0;  //the current page number

                AllRowCount = hippaModel.AllRowCnt;
                PageRowCount = hippaModel.PageRowCnt;
                PageSize = AllRowCount / PageRowCount + 1;
                PageSizeIndex = hippaModel.PageSizeIndex;

                StringBuilder sql = new StringBuilder();
                //old not page up/down in fact
                //sql.AppendFormat("select top {0} * from tActivityLog where ALGuid not in (select top ({1}*{2}) ALGuid from tActivityLog order by EventDT)", PageRowCount, PageRowCount, PageSizeIndex);

                #region new

                int pageRowCountLast = AllRowCount % PageRowCount;//get the last page size

                if ((PageSizeIndex < PageSize - 1) || pageRowCountLast == 0)// last page < pagesize && current page index not in last page
                {
                    pageRowCountLast = PageRowCount;//same as PageRowCount
                }


                #region Added by Blue for [RC507] - US16220, 07/17/2014
                if (hippaModel.IsVIP)
                {
                    sql.AppendFormat("select top {0} * from (select top ({1}*{2}) a.*, 'RIS GC' as ApplicationName, h.Priority, h.Category from RISHippa..tActivityLog a left outer join tHippaEventType h on a.EventTypeCode = h.EventTypeCode and a.EventID = h.EventID left outer join tRegPatient p on a.PartObjectID = p.PatientID where p.IsVIP = 1 and", pageRowCountLast, PageRowCount, PageSizeIndex + 1);
                }
                else
                {
                    sql.AppendFormat("select top {0} * from (select top ({1}*{2}) a.*, 'RIS GC' as ApplicationName, h.Priority, h.Category from RISHippa..tActivityLog a left outer join tHippaEventType h on a.EventTypeCode = h.EventTypeCode and a.EventID = h.EventID where", pageRowCountLast, PageRowCount, PageSizeIndex + 1);
                }
                #endregion

                if (duringDays >= 0 && flag == false)
                {
                    sql.AppendFormat(" DateDiff(DAY,EventDt,getDate())<={0}", duringDays);
                    flag = true;
                }
                if (eventBeginDt != DateTime.MinValue && eventEndDt != DateTime.MaxValue && flag == false)
                {
                    sql.AppendFormat(" EventDT >='{0} 0:00:00' and EventDT <'{1} 23:59:59'"
                        , eventBeginDt.ToShortDateString(), eventEndDt.ToShortDateString());
                    flag = true;
                }

                #endregion

                for (int i = 0; i < userName.Count; i++)
                {
                    if (!string.IsNullOrEmpty(userName[i].ToString()))
                    {
                        if (userName.Count == 1)
                        {
                            sql.AppendFormat("and UserName = '{0}' ", userName[i].ToString());
                            break;
                        }
                        else if (i == 0)
                        {
                            sql.AppendFormat("and (UserName = '{0}' ", userName[i].ToString());
                            continue;
                        }
                        if (i == userName.Count - 1)
                        {
                            sql.AppendFormat("or UserName = '{0}')", userName[i].ToString());
                        }
                        else
                        {
                            sql.AppendFormat("or UserName = '{0}'", userName[i].ToString());
                        }
                    }
                }

                for (int i = 0; i < eventID.Count; i++)
                {
                    if (!string.IsNullOrEmpty(eventID[i].ToString()))
                    {
                        if (eventID.Count == 1)
                        {
                            sql.AppendFormat(" and a.EventID = '{0}'", eventID[i].ToString());
                            break;
                        }
                        else if (i == 0)
                        {
                            sql.AppendFormat(" and (a.EventID = '{0}'", eventID[i].ToString());
                            continue;
                        }
                        if (i == eventID.Count - 1)
                        {
                            sql.AppendFormat(" or a.EventID = '{0}')", eventID[i].ToString());
                        }
                        else
                        {
                            sql.AppendFormat(" or a.EventID = '{0}'", eventID[i].ToString());
                        }
                    }
                }

                for (int i = 0; i < eventTypeCode.Count; i++)
                {
                    if (!string.IsNullOrEmpty(eventTypeCode[i].ToString()))
                    {
                        if (eventTypeCode.Count == 1)
                        {
                            sql.AppendFormat(" and a.EventTypeCode = '{0}'", eventTypeCode[i].ToString());
                            break;
                        }
                        else if (i == 0)
                        {
                            sql.AppendFormat(" and (a.EventTypeCode = '{0}'", eventTypeCode[i].ToString());
                            continue;
                        }
                        if (i == eventTypeCode.Count - 1)
                        {
                            sql.AppendFormat(" or a.EventTypeCode = '{0}')", eventTypeCode[i].ToString());
                        }
                        else
                        {
                            sql.AppendFormat(" or a.EventTypeCode = '{0}'", eventTypeCode[i].ToString());
                        }
                    }
                }

                #region Added by Blue for [RC507] - US16220, 07/17/2014
                if (eventCategory != null && eventCategory.Count > 0 && !string.IsNullOrWhiteSpace(eventCategory[0].ToString()))
                {
                    strEventCategorySql = " and h.Category in (";
                    for (int i = 0; i < eventCategory.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(eventCategory[i].ToString()))
                        {
                            strEventCategorySql += string.Format("'{0}',", eventCategory[i].ToString());
                        }
                    }
                    strEventCategorySql = strEventCategorySql.TrimEnd(',');
                    strEventCategorySql += ") ";
                    sql.Append(strEventCategorySql);
                }

                if (!string.IsNullOrEmpty(hippaModel.OperationResult))
                {
                    sql.AppendFormat(" and OperationResult ='{0}'", hippaModel.OperationResult);
                }
                #endregion

                if (!string.IsNullOrEmpty(hippaModel.PatientID))
                {
                    sql.AppendFormat(" and PartObjectID ='{0}'", hippaModel.PatientID);
                }
                if (!string.IsNullOrEmpty(hippaModel.PatientName))
                {
                    sql.AppendFormat(" and PartObjectName ='{0}'", hippaModel.PatientName);
                }

                //if (duringDays >= 1 && flag == false)
                //{
                //    sql.AppendFormat(" and DateDiff(DAY,EventDt,getDate())<={0}", duringDays);
                //    flag = true;
                //}
                //if (eventBeginDt != null && eventEndDt != null && flag == false)
                //{
                //    sql.AppendFormat(" and EventDT >='{0} 0:00:00' and EventDT <'{1} 23:59:59'"
                //        , eventBeginDt.ToShortDateString(), eventEndDt.ToShortDateString());
                //    flag = true;
                //}

                //convert StringBuilder to string
                String sqlStatement = sql.ToString();
                //sqlStatement = sqlStatement.Substring(0, sqlStatement.Length - 5);
                //sqlStatement = sqlStatement + " order by EventDT";
                sqlStatement = sqlStatement.TrimEnd("and".ToCharArray()).Trim(" where".ToCharArray()).Replace("whereand", "where").Replace("where and", "where").Replace("and and", "and").Replace("andand", "and") + " order by EventDT desc) A order by EventDT asc ";

                sqlStatement = " select * from ( " + sqlStatement + " ) B order by EventDt desc ";

                DataSet dataSet = new DataSet();
                DataTable dataTable = dataAccess.ExecuteQuery(sqlStatement);
                dataSet.Tables.Add(dataTable);
                return dataSet;
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message + e.StackTrace, Application.StartupPath.ToString(),
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


        /// <summary>
        /// Name:getAllRowCount
        /// Function:get the total row count about hippa record stored in DataBase.
        /// Input: hippaModel -- HippaModel include model and messagename
        /// Output:the total row count about hippa record stored in DataBase.
        /// Return:DataSet object if Success, Null if fail
        /// </summary>
        /// <param name="hippaModel">hippaModel</param>
        /// <returns>dataset or null</returns>
        public virtual int getAllRowCount(HippaModel hippaModel)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                bool flag = false;

                ArrayList userName = hippaModel.UserName;
                ArrayList eventID = hippaModel.EventID;
                ArrayList eventTypeCode = hippaModel.EventTypeCode;
                DateTime eventDT = hippaModel.EventDT;
                DateTime eventBeginDt = hippaModel.EventBeginDt;
                DateTime eventEndDt = hippaModel.EventEndDt;
                #region Added by Blue for [RC507] - US16220, 07/17/2014
                ArrayList eventCategory = hippaModel.EventCategory;
                string strEventCategorySql = string.Empty;
                #endregion
                int duringDays = hippaModel.DuringDays;

                StringBuilder sql = new StringBuilder();
                #region Added by Blue for [RC507] - US16220, 07/17/2014
                if (hippaModel.IsVIP)
                {
                    sql.AppendFormat("select count(*) from RISHippa..tActivityLog a left outer join tHippaEventType h on a.EventTypeCode = h.EventTypeCode and a.EventID = h.EventID left outer join tRegPatient p on a.PartObjectID = p.PatientID where p.IsVIP = 1 and 1=1 ");
                }
                else
                {
                    sql.AppendFormat("select count(*) from RISHippa..tActivityLog a left outer join tHippaEventType h on a.EventTypeCode = h.EventTypeCode and a.EventID = h.EventID left outer join tRegPatient p on a.PartObjectID = p.PatientID where 1=1 ");
                }
                #endregion
                for (int i = 0; i < userName.Count; i++)
                {
                    if (!string.IsNullOrEmpty(userName[i].ToString()))
                    {
                        if (userName.Count == 1)
                        {
                            sql.AppendFormat("and UserName = '{0}' ", userName[i].ToString());
                            break;
                        }
                        else if (i == 0)
                        {
                            sql.AppendFormat("and (UserName = '{0}' ", userName[i].ToString());
                            continue;
                        }
                        if (i == userName.Count - 1)
                        {
                            sql.AppendFormat("or UserName = '{0}')", userName[i].ToString());
                        }
                        else
                        {
                            sql.AppendFormat("or UserName = '{0}'", userName[i].ToString());
                        }
                    }
                }

                for (int i = 0; i < eventID.Count; i++)
                {
                    if (!string.IsNullOrEmpty(eventID[i].ToString()))
                    {
                        if (eventID.Count == 1)
                        {
                            sql.AppendFormat(" and a.EventID = '{0}'", eventID[i].ToString());
                            break;
                        }
                        else if (i == 0)
                        {
                            sql.AppendFormat(" and (a.EventID = '{0}'", eventID[i].ToString());
                            continue;
                        }
                        if (i == eventID.Count - 1)
                        {
                            sql.AppendFormat(" or a.EventID = '{0}')", eventID[i].ToString());
                        }
                        else
                        {
                            sql.AppendFormat(" or a.EventID = '{0}'", eventID[i].ToString());
                        }
                    }
                }

                for (int i = 0; i < eventTypeCode.Count; i++)
                {
                    if (!string.IsNullOrEmpty(eventTypeCode[i].ToString()))
                    {
                        if (eventTypeCode.Count == 1)
                        {
                            sql.AppendFormat(" and a.EventTypeCode = '{0}'", eventTypeCode[i].ToString());
                            break;
                        }
                        else if (i == 0)
                        {
                            sql.AppendFormat(" and (a.EventTypeCode = '{0}'", eventTypeCode[i].ToString());
                            continue;
                        }
                        if (i == eventTypeCode.Count - 1)
                        {
                            sql.AppendFormat(" or a.EventTypeCode = '{0}')", eventTypeCode[i].ToString());
                        }
                        else
                        {
                            sql.AppendFormat(" or a.EventTypeCode = '{0}'", eventTypeCode[i].ToString());
                        }
                    }
                }

                if (duringDays >= 0 && flag == false)
                {
                    sql.AppendFormat(" and DateDiff(DAY,EventDt,getDate())<={0}", duringDays);
                    flag = true;
                }
                if (eventBeginDt != DateTime.MinValue && eventEndDt != DateTime.MaxValue && flag == false)
                {
                    sql.AppendFormat(" and EventDT >='{0} 0:00:00' and EventDT <'{1} 23:59:59' "
                        , eventBeginDt.ToShortDateString(), eventEndDt.ToShortDateString());
                    flag = true;
                }

                #region Added by Blue for [RC507] - US16220, 07/17/2014
                if (eventCategory != null && eventCategory.Count > 0 && !string.IsNullOrWhiteSpace(eventCategory[0].ToString()))
                {
                    strEventCategorySql = " and h.Category in (";
                    for (int i = 0; i < eventCategory.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(eventCategory[i].ToString()))
                        {
                            strEventCategorySql += string.Format("'{0}',", eventCategory[i].ToString());
                        }
                    }
                    strEventCategorySql = strEventCategorySql.TrimEnd(',');
                    strEventCategorySql += ") ";
                    sql.Append(strEventCategorySql);
                }

                if (!string.IsNullOrEmpty(hippaModel.OperationResult))
                {
                    sql.AppendFormat(" and OperationResult ='{0}'", hippaModel.OperationResult);
                }
                #endregion

                if (!string.IsNullOrEmpty(hippaModel.PatientID))
                {
                    sql.AppendFormat("and PartObjectID ='{0}'", hippaModel.PatientID);
                }
                if (!string.IsNullOrEmpty(hippaModel.PatientName))
                {
                    sql.AppendFormat("and PartObjectName ='{0}'", hippaModel.PatientName);
                }

                //convert StringBuilder to string
                String sqlStatement = sql.ToString();
                //delete the last "and" in sql statement
                //sqlStatement = sqlStatement.Substring(0, sqlStatement.Length - 5);
                //sqlStatement = sqlStatement + " order by EventDT";
                object returnCell = null;
                if ((returnCell = dataAccess.ExecuteScalar(sqlStatement)) != null)
                {
                    return Convert.ToInt32(returnCell);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message + e.StackTrace, Application.StartupPath.ToString(),
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
            return 0;
        }
        #endregion

        #region Import&Export Section
        public virtual bool ImportPhraseTemplate(bool isClear, string strUserGuid, DataSet phraseTemplateDataSet, string site)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                if (isClear)
                {
                    //if (strUserGuid == "Global")
                    //{
                    //    dataAccess.ExecuteNonQuery("delete from tPhraseTemplate where Type = 0", KodakDAL.ConnectionState.KeepOpen);
                    //}
                    //else
                    //{
                    //    string strSqlDelete = string.Format("delete from tPhraseTemplate where Type = 1 and UserGuid = '{0}'", strUserGuid);
                    //    dataAccess.ExecuteNonQuery(strSqlDelete, KodakDAL.ConnectionState.KeepOpen);
                    //}
                    dataAccess.Parameters.Add("@Site", site);
                    dataAccess.Parameters.Add("@DirectoryType", "PHRASE");
                    dataAccess.ExecuteNonQuerySP("USP_DeleteTemplateRecursive", RisDAL.ConnectionState.KeepOpen);
                }
                string sql;
                if (phraseTemplateDataSet.Tables.Count == 0 || phraseTemplateDataSet.Tables.Count > 2)
                {
                    return false;
                }
                else if (phraseTemplateDataSet.Tables.Count == 1)
                {
                    foreach (DataRow myRow in phraseTemplateDataSet.Tables["PhraseTemplate"].Rows)
                    {
                        dataAccess.Parameters.Clear();
                        //string strGuid = Guid.NewGuid().ToString();
                        sql = "insert into tPhraseTemplate(TemplateGuid,ModalityType,TemplateName,TemplateInfo,ShortcutCode,Type,UserGuid,Domain) values (@TemplateGuid,@ModalityType,@TemplateName,@TemplateInfo,@ShortcutCode,@Type,@UserGuid,@Domain)";
                        dataAccess.Parameters.Add("@TemplateGuid", Guid.NewGuid().ToString());
                        dataAccess.Parameters.Add("@ModalityType", Convert.ToString(myRow["ModalityType"]));
                        dataAccess.Parameters.Add("@TemplateName", Convert.ToString(myRow["TemplateName"]));
                        dataAccess.Parameters.Add("@TemplateInfo", Convert.ToString(myRow["TemplateInfo"]));
                        dataAccess.Parameters.Add("@ShortcutCode", Convert.ToString(myRow["ShortcutCode"]));
                        //if (strUserGuid == "Global")
                        //{
                        //    dataAccess.Parameters.AddInt("@Type", 0);
                        //    dataAccess.Parameters.Add("@UserGuid", "Global");
                        //}
                        //else
                        //{
                        //    dataAccess.Parameters.AddInt("@Type", 1);
                        //    dataAccess.Parameters.Add("@UserGuid", strUserGuid);
                        //}
                        dataAccess.Parameters.AddInt("@Type", Convert.ToInt32(myRow["Type"]));
                        dataAccess.Parameters.Add("@UserGuid", Convert.ToString(myRow["UserGuid"]));
                        dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    dataAccess.ExecuteNonQuerySP("USP_UpgradePhaseTemplate", RisDAL.ConnectionState.KeepOpen);
                }
                else if (phraseTemplateDataSet.Tables.Count == 2)
                {
                    string newTemplateGuid = "";
                    string oldTemplateGuid = "";
                    string newItemGuid = "";
                    string oldItemGuid = "";
                    foreach (DataRow myRow in phraseTemplateDataSet.Tables["PhraseTemplate"].Rows)
                    {
                        oldTemplateGuid = Convert.ToString(myRow["TemplateGuid"]);
                        DataRow[] drFounds = phraseTemplateDataSet.Tables["ReportTemplateDirec"].Select(string.Format("TemplateGuid = '{0}'", oldTemplateGuid));
                        if (drFounds.Length > 0)
                        {
                            newTemplateGuid = Guid.NewGuid().ToString();
                            foreach (DataRow dr in drFounds)
                            {
                                dr["TemplateGuid"] = newTemplateGuid;
                            }
                        }
                        else
                        {
                            newTemplateGuid = oldTemplateGuid;
                        }
                        dataAccess.Parameters.Clear();
                        //string strGuid = Guid.NewGuid().ToString();
                        sql = "insert into tPhraseTemplate(TemplateGuid,ModalityType,TemplateName,TemplateInfo,ShortcutCode,Type,UserGuid,Domain) values (@TemplateGuid,@ModalityType,@TemplateName,@TemplateInfo,@ShortcutCode,@Type,@UserGuid,@Domain)";
                        dataAccess.Parameters.Add("@TemplateGuid", newTemplateGuid);
                        dataAccess.Parameters.Add("@ModalityType", Convert.ToString(myRow["ModalityType"]));
                        dataAccess.Parameters.Add("@TemplateName", Convert.ToString(myRow["TemplateName"]));
                        dataAccess.Parameters.Add("@TemplateInfo", Convert.ToString(myRow["TemplateInfo"]));
                        dataAccess.Parameters.Add("@ShortcutCode", Convert.ToString(myRow["ShortcutCode"]));
                        //if (strUserGuid == "Global")
                        //{
                        //    dataAccess.Parameters.AddInt("@Type", 0);
                        //    dataAccess.Parameters.Add("@UserGuid", "Global");
                        //}
                        //else
                        //{
                        //    dataAccess.Parameters.AddInt("@Type", 1);
                        //    dataAccess.Parameters.Add("@UserGuid", strUserGuid);
                        //}
                        dataAccess.Parameters.AddInt("@Type", Convert.ToInt32(myRow["Type"]));
                        dataAccess.Parameters.Add("@UserGuid", Convert.ToString(myRow["UserGuid"]));
                        dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }

                    foreach (DataRow myRow in phraseTemplateDataSet.Tables["ReportTemplateDirec"].Rows)
                    {
                        newItemGuid = Guid.NewGuid().ToString();
                        oldItemGuid = Convert.ToString(myRow["ItemGUID"]);
                        myRow["ItemGUID"] = newItemGuid;
                        DataRow[] drFounds = phraseTemplateDataSet.Tables["ReportTemplateDirec"].Select(string.Format("ParentID='{0}'", oldItemGuid));
                        foreach (DataRow dr in drFounds)
                        {
                            dr["ParentID"] = newItemGuid;
                        }
                    }

                    foreach (DataRow myRow in phraseTemplateDataSet.Tables["ReportTemplateDirec"].Rows)
                    {
                        dataAccess.Parameters.Clear();
                        sql = "insert into tReportTemplateDirec(ItemGUID,ParentID,Depth,ItemName,ItemOrder,Type,UserGuid,TemplateGuid,Leaf,Domain,DirectoryType) values(@ItemGUID,@ParentID,@Depth,@ItemName,@ItemOrder,@Type,@UserGuid,@TemplateGuid,@Leaf,@Domain,@DirectoryType)";
                        dataAccess.Parameters.Add("@ItemGUID", Convert.ToString(myRow["ItemGUID"] == DBNull.Value ? "" : myRow["ItemGUID"]));
                        dataAccess.Parameters.Add("@ParentID", Convert.ToString(myRow["ParentID"] == DBNull.Value ? "" : myRow["ParentID"]));
                        dataAccess.Parameters.AddInt("@Depth", Convert.ToInt32(myRow["Depth"] == DBNull.Value ? "0" : myRow["Depth"]));
                        dataAccess.Parameters.Add("@ItemName", Convert.ToString(myRow["ItemName"] == DBNull.Value ? "" : myRow["ItemName"]));
                        dataAccess.Parameters.AddInt("@ItemOrder", Convert.ToInt32(myRow["ItemOrder"] == DBNull.Value ? "0" : myRow["ItemOrder"]));
                        dataAccess.Parameters.AddInt("@Type", Convert.ToInt32(myRow["Type"]));
                        dataAccess.Parameters.Add("@UserGuid", Convert.ToString(myRow["UserGuid"] == DBNull.Value ? "" : myRow["UserGuid"]));
                        dataAccess.Parameters.Add("@TemplateGuid", Convert.ToString(myRow["TemplateGuid"] == DBNull.Value ? "" : myRow["TemplateGuid"]));
                        dataAccess.Parameters.AddInt("@Leaf", Convert.ToInt32(myRow["Leaf"]));
                        dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                        dataAccess.Parameters.Add("@DirectoryType", Convert.ToString(myRow["DirectoryType"] == DBNull.Value ? "report" : myRow["DirectoryType"]));
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
        public virtual DataSet GetAllPhraseTemplate(string strUserGuid, string Site = "")
        {
            RisDAL dataAccess = new RisDAL();
            string spName = "USP_GetTemplateRecursive";
            DataSet dsTemplate = new DataSet();
            try
            {
                dsTemplate.DataSetName = "PhraseTemplateDataSet";
                dataAccess.Parameters.Add("@Site", Site);
                dataAccess.Parameters.Add("@DirectoryType", "Phrase");
                dataAccess.ExecuteQuerySP(spName, dsTemplate, "PhraseTemplate");
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
        public virtual bool ImportPrintTemplate(bool isClear, int type, DataSet printTemplateDataSet, string site, ref string errorInfo)
        {
            RisDAL dataAccess = new RisDAL();
            string info = "";
            try
            {
                dataAccess.BeginTransaction();
                if (isClear)
                {
                    string strSql;
                    if (type == -1)
                    {
                        strSql = string.Format("delete from tPrintTemplate where site ='{0}'", site);
                    }
                    else
                    {
                        strSql = string.Format("delete from tPrintTemplate where Type = {0} and site ='{1}'", type, site);
                    }
                    dataAccess.ExecuteNonQuery(strSql, RisDAL.ConnectionState.KeepOpen);
                }
                if (printTemplateDataSet.Tables.Count != 1)
                {
                    return false;
                }
                string sql;
                int count = 0;
                foreach (DataRow myRow in printTemplateDataSet.Tables[0].Rows)
                {
                    count = Convert.ToInt32(dataAccess.ExecuteScalar(string.Format("select count(1) from tPrintTemplate where TemplateGuid ='{0}'", Convert.ToString(myRow["TemplateGuid"])), RisDAL.ConnectionState.KeepOpen));
                    if (count > 0)
                    {
                        errorInfo = string.Format("(TemplateGuid='{0}',TemplateName='{1}')", Convert.ToString(myRow["TemplateGuid"]), Convert.ToString(myRow["TemplateName"]));
                        dataAccess.RollbackTransaction();
                        return false;
                    }

                    dataAccess.Parameters.Clear();
                    //string strGuid = Guid.NewGuid().ToString();
                    sql = "insert into tPrintTemplate(TemplateGuid,Type,TemplateName,TemplateInfo,IsDefaultByType,Version,ModalityType,IsDefaultByModality,Domain,Site) values (@TemplateGuid,@Type,@TemplateName,@TemplateInfo,@IsDefaultByType,@Version,@ModalityType,@IsDefaultByModality,@Domain,@Site)";
                    dataAccess.Parameters.Add("@TemplateGuid", Convert.ToString(myRow["TemplateGuid"]));
                    dataAccess.Parameters.Add("@ModalityType", Convert.ToString(myRow["ModalityType"]));
                    dataAccess.Parameters.Add("@TemplateName", Convert.ToString(myRow["TemplateName"]));
                    dataAccess.Parameters.Add("@TemplateInfo", System.Text.Encoding.Unicode.GetBytes(myRow["TemplateInfoStr"].ToString()));
                    dataAccess.Parameters.AddInt("@Version", Convert.ToInt32(myRow["Version"]));
                    dataAccess.Parameters.AddInt("@IsDefaultByModality", Convert.ToInt32(myRow["IsDefaultByModality"]));
                    dataAccess.Parameters.AddInt("@IsDefaultByType", Convert.ToInt32(myRow["IsDefaultByType"]));

                    dataAccess.Parameters.AddInt("@Type", Convert.ToInt32(myRow["Type"]));
                    dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                    dataAccess.Parameters.Add("@Site", site);

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
        public virtual DataSet GetAllPrintTemplate(int type, string site = "")
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
                PrintTemplateTable.TableName = type.ToString();
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
        public virtual DataSet GetALLReportTemplate(string Site = "")
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
        public virtual bool ImportReportTemplate(bool isClear, DataSet reportTemplateDataSet, string site)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                if (isClear)
                {
                    //if (strUserGuid == "Global")
                    //{
                    //    dataAccess.ExecuteNonQuery("delete from tPhraseTemplate where Type = 0", KodakDAL.ConnectionState.KeepOpen);
                    //}
                    //else
                    //{
                    //    string strSqlDelete = string.Format("delete from tPhraseTemplate where Type = 1 and UserGuid = '{0}'", strUserGuid);
                    //    dataAccess.ExecuteNonQuery(strSqlDelete, KodakDAL.ConnectionState.KeepOpen);
                    //}
                    dataAccess.Parameters.Add("@Site", site);
                    dataAccess.Parameters.Add("@DirectoryType", "REPORT");
                    dataAccess.ExecuteNonQuerySP("USP_DeleteTemplateRecursive", RisDAL.ConnectionState.KeepOpen);
                }
                if (reportTemplateDataSet.Tables.Count != 2)
                {
                    return false;
                }
                string sql;
                string newTemplateGuid = "";
                string oldTemplateGuid = "";
                string newItemGuid = "";
                string oldItemGuid = "";
                foreach (DataRow myRow in reportTemplateDataSet.Tables["ReportTemplate"].Rows)
                {
                    oldTemplateGuid = Convert.ToString(myRow["TemplateGuid"]);
                    DataRow[] drFounds = reportTemplateDataSet.Tables["ReportTemplateDirec"].Select(string.Format("TemplateGuid = '{0}'", oldTemplateGuid));
                    if (drFounds.Length > 0)
                    {
                        newTemplateGuid = Guid.NewGuid().ToString();
                        foreach (DataRow dr in drFounds)
                        {
                            dr["TemplateGuid"] = newTemplateGuid;
                        }
                    }
                    else
                    {
                        newTemplateGuid = oldTemplateGuid;
                    }
                    dataAccess.Parameters.Clear();
                    sql = "insert into tReportTemplate(TemplateGuid,TemplateName,ModalityType,BodyPart,WYS,WYG,AppendInfo,TechInfo,CheckItemName ,DoctorAdvice,ShortcutCode,ACRCode,ACRAnatomicDesc,ACRPathologicDesc,BodyCategory,Domain) "
                    + " values(@TemplateGuid,@TemplateName,@ModalityType,@BodyPart,@WYS,@WYG,@AppendInfo,@TechInfo,@CheckItemName,@DoctorAdvice,@ShortcutCode,@ACRCode,@ACRAnatomicDesc,@ACRPathologicDesc,@BodyCategory,@Domain)";
                    dataAccess.Parameters.Add("@TemplateGuid", newTemplateGuid);
                    dataAccess.Parameters.Add("@ModalityType", Convert.ToString(myRow["ModalityType"] == DBNull.Value ? "" : myRow["ModalityType"]));
                    dataAccess.Parameters.Add("@TemplateName", Convert.ToString(myRow["TemplateName"] == DBNull.Value ? "" : myRow["TemplateName"]));
                    dataAccess.Parameters.Add("@BodyPart", Convert.ToString(myRow["BodyPart"] == DBNull.Value ? "" : myRow["BodyPart"]));
                    dataAccess.Parameters.Add("@CheckItemName", Convert.ToString(myRow["CheckItemName"] == DBNull.Value ? "" : myRow["CheckItemName"]));
                    dataAccess.Parameters.Add("@DoctorAdvice", Convert.ToString(myRow["DoctorAdvice"] == DBNull.Value ? "" : myRow["DoctorAdvice"]));
                    dataAccess.Parameters.Add("@ShortcutCode", Convert.ToString(myRow["ShortcutCode"] == DBNull.Value ? "" : myRow["ShortcutCode"]));
                    dataAccess.Parameters.Add("@ACRCode", Convert.ToString(myRow["ACRCode"] == DBNull.Value ? "" : myRow["ACRCode"]));
                    dataAccess.Parameters.Add("@ACRAnatomicDesc", Convert.ToString(myRow["ACRAnatomicDesc"] == DBNull.Value ? "" : myRow["ACRAnatomicDesc"]));
                    dataAccess.Parameters.Add("@ACRPathologicDesc", Convert.ToString(myRow["ACRPathologicDesc"] == DBNull.Value ? "" : myRow["ACRPathologicDesc"]));
                    dataAccess.Parameters.Add("@BodyCategory", Convert.ToString(myRow["BodyCategory"] == DBNull.Value ? "" : myRow["BodyCategory"]));
                    dataAccess.Parameters.Add("@WYS", System.Text.Encoding.Default.GetBytes(myRow["WYSStr"].ToString()));
                    dataAccess.Parameters.Add("@WYG", System.Text.Encoding.Default.GetBytes(myRow["WYGStr"].ToString()));
                    dataAccess.Parameters.Add("@AppendInfo", System.Text.Encoding.Default.GetBytes(myRow["AppendInfoStr"].ToString()));
                    dataAccess.Parameters.Add("@TechInfo", System.Text.Encoding.Default.GetBytes(myRow["TechInfoStr"].ToString()));
                    dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }
                foreach (DataRow myRow in reportTemplateDataSet.Tables["ReportTemplateDirec"].Rows)
                {
                    newItemGuid = Guid.NewGuid().ToString();
                    oldItemGuid = Convert.ToString(myRow["ItemGUID"]);
                    myRow["ItemGUID"] = newItemGuid;
                    DataRow[] drFounds = reportTemplateDataSet.Tables["ReportTemplateDirec"].Select(string.Format("ParentID='{0}'", oldItemGuid));
                    foreach (DataRow dr in drFounds)
                    {
                        dr["ParentID"] = newItemGuid;
                    }
                }

                foreach (DataRow myRow in reportTemplateDataSet.Tables["ReportTemplateDirec"].Rows)
                {
                    dataAccess.Parameters.Clear();
                    sql = "insert into tReportTemplateDirec(ItemGUID,ParentID,Depth,ItemName,ItemOrder,Type,UserGuid,TemplateGuid,Leaf,Domain,DirectoryType) values(@ItemGUID,@ParentID,@Depth,@ItemName,@ItemOrder,@Type,@UserGuid,@TemplateGuid,@Leaf,@Domain,@DirectoryType)";
                    dataAccess.Parameters.Add("@ItemGUID", Convert.ToString(myRow["ItemGUID"] == DBNull.Value ? "" : myRow["ItemGUID"]));
                    dataAccess.Parameters.Add("@ParentID", Convert.ToString(myRow["ParentID"] == DBNull.Value ? "" : myRow["ParentID"]));
                    dataAccess.Parameters.AddInt("@Depth", Convert.ToInt32(myRow["Depth"] == DBNull.Value ? "0" : myRow["Depth"]));
                    dataAccess.Parameters.Add("@ItemName", Convert.ToString(myRow["ItemName"] == DBNull.Value ? "" : myRow["ItemName"]));
                    dataAccess.Parameters.AddInt("@ItemOrder", Convert.ToInt32(myRow["ItemOrder"] == DBNull.Value ? "0" : myRow["ItemOrder"]));
                    dataAccess.Parameters.AddInt("@Type", Convert.ToInt32(myRow["Type"]));
                    dataAccess.Parameters.Add("@UserGuid", Convert.ToString(myRow["UserGuid"] == DBNull.Value ? "" : myRow["UserGuid"]));
                    dataAccess.Parameters.Add("@TemplateGuid", Convert.ToString(myRow["TemplateGuid"] == DBNull.Value ? "" : myRow["TemplateGuid"]));
                    dataAccess.Parameters.AddInt("@Leaf", Convert.ToInt32(myRow["Leaf"]));
                    dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                    dataAccess.Parameters.Add("@DirectoryType", "report");
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
        public virtual bool ImportBookingNoticeTemplate(bool isClear, DataSet noticeTemplateDataset)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                if (isClear)
                {
                    dataAccess.ExecuteNonQuery("Disable trigger DeleteBookingNoticeTemplate on tBookingNoticeTemplate", RisDAL.ConnectionState.KeepOpen);
                    dataAccess.ExecuteNonQuery("delete from tBookingNoticeTemplate", RisDAL.ConnectionState.KeepOpen);
                    dataAccess.ExecuteNonQuery("Enable  trigger DeleteBookingNoticeTemplate on tBookingNoticeTemplate", RisDAL.ConnectionState.KeepOpen);

                }
                if (noticeTemplateDataset.Tables.Count < 1)
                {
                    return false;
                }
                string sql;

                foreach (DataRow myRow in noticeTemplateDataset.Tables[0].Rows)
                {
                    dataAccess.Parameters.Clear();
                    sql = "insert into tBookingNoticeTemplate(Guid,ModalityType,TemplateName,BookingNotice,Domain)"
                    + " values(@Guid,@ModalityType,@TemplateName,@BookingNotice,@Domain)";
                    dataAccess.Parameters.Add("@Guid", Convert.ToString(myRow["Guid"]));
                    dataAccess.Parameters.Add("@ModalityType", Convert.ToString(myRow["ModalityType"] == DBNull.Value ? "" : myRow["ModalityType"]));
                    dataAccess.Parameters.Add("@TemplateName", Convert.ToString(myRow["TemplateName"] == DBNull.Value ? "" : myRow["TemplateName"]));
                    dataAccess.Parameters.Add("@BookingNotice", System.Text.Encoding.Unicode.GetBytes(myRow["BookingNoticeStr"].ToString()));
                    dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
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
        public virtual DataSet GetAllBookingNoticeTemplate()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            string sql1 = "select * from tbookingNoticeTemplate";

            try
            {
                DataTable bookingNoticeTemplateTable;
                bookingNoticeTemplateTable = dataAccess.ExecuteQuery(sql1);
                bookingNoticeTemplateTable.TableName = "BookingNoticeTemplateTable";
                ds.Tables.Add(bookingNoticeTemplateTable);
                ds.DataSetName = "BookingNoticeTemplateDataSet";

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

        #region ICD10DAO Section
        #region DataSet SearchICD10(string condition)
        /// <summary>
        /// SearchICD10(string condition)
        /// </summary>
        /// <param>string condition</param>
        /// <returns>true if db operation successful otherwise false</returns>
        ///
        public virtual DataSet SearchICD10(string condition)
        {
            DataSet ds = new DataSet("ICD10DataSet");
            DataTable dt;
            RisDAL dataAccess = new RisDAL();
            string sql = string.Format("select * from tICD10 where {0}", condition);
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
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

        #region DataSet GetAllICD10()
        /// <summary>
        /// DataSet GetAllICD10()
        /// </summary>
        /// <param></param>
        /// <returns>true if db operation successful otherwise false</returns>
        ///
        public virtual DataSet GetAllICD10()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet("ICD10DataSet");
            DataTable dt;
            string sql = @"select  * from tICD10";
            try
            {
                dt = dataAccess.ExecuteQuery(sql);
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {

                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
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

        #region bool AddICD10(BaseDataSetModel model)
        /// <summary>
        /// bool  AddICD10(BaseDataSetModel model)
        /// </summary>
        /// <param>BaseDataSetModel model</param>
        /// <returns>true if db operation successful otherwise false</returns>
        ///
        public bool AddICD10(BaseDataSetModel model)
        {
            RisDAL dataAccess = new RisDAL();
            DataTable dt = model.DataSetParameter.Tables[0];
            string sql = "insert into tICD10(ID,INAME,PY,WB,TJM,BZLB,ZLBM,JLZT,MEMO,DOMAIN) values(@ID,@INAME,@PY,@WB,@TJM,@BZLB,@ZLBM,@JLZT,@MEMO,@DOMAIN)";
            dataAccess.Parameters.AddVarChar("@ID", dt.Rows[0]["ID"].ToString());
            dataAccess.Parameters.AddVarChar("@INAME", dt.Rows[0]["INAME"].ToString());
            dataAccess.Parameters.AddVarChar("@PY", dt.Rows[0]["PY"].ToString());
            dataAccess.Parameters.AddVarChar("@WB", dt.Rows[0]["WB"].ToString());
            dataAccess.Parameters.AddVarChar("@TJM", dt.Rows[0]["TJM"].ToString());
            dataAccess.Parameters.AddVarChar("@BZLB", dt.Rows[0]["BZLB"].ToString());
            dataAccess.Parameters.AddVarChar("@ZLBM", dt.Rows[0]["ZLBM"].ToString());
            dataAccess.Parameters.AddVarChar("@JLZT", dt.Rows[0]["JLZT"].ToString());
            dataAccess.Parameters.AddVarChar("@MEMO", dt.Rows[0]["MEMO"].ToString());
            dataAccess.Parameters.AddVarChar("@DOMAIN", dt.Rows[0]["DOMAIN"].ToString());
            int result = 0;
            try
            {
                result = dataAccess.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
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
        #endregion

        #region bool ModifyICD10(BaseDataSetModel model)
        /// <summary>
        /// bool ModifyICD10(BaseDataSetModel model)
        /// </summary>
        /// <param>BaseDataSetModel model</param>
        /// <returns>true if db operation successful otherwise false</returns>
        ///
        public bool ModifyICD10(BaseDataSetModel model)
        {
            RisDAL dataAccess = new RisDAL();
            DataTable dt = model.DataSetParameter.Tables[0];
            string sql = "update tICD10 set ID=@NID , INAME=@INAME , PY=@PY , " +
                "WB=@WB , TJM=@TJM , BZLB=@BZLB , ZLBM=@ZLBM , JLZT=@JLZT , " +
                "MEMO=@MEMO where ID=@ID";
            dataAccess.Parameters.AddVarChar("@ID", dt.Rows[0]["ID"].ToString());
            dataAccess.Parameters.AddVarChar("@INAME", dt.Rows[0]["INAME"].ToString());
            dataAccess.Parameters.AddVarChar("@PY", dt.Rows[0]["PY"].ToString());
            dataAccess.Parameters.AddVarChar("@WB", dt.Rows[0]["WB"].ToString());
            dataAccess.Parameters.AddVarChar("@TJM", dt.Rows[0]["TJM"].ToString());
            dataAccess.Parameters.AddVarChar("@BZLB", dt.Rows[0]["BZLB"].ToString());
            dataAccess.Parameters.AddVarChar("@JLZT", dt.Rows[0]["JLZT"].ToString());
            dataAccess.Parameters.AddVarChar("@MEMO", dt.Rows[0]["MEMO"].ToString());
            dataAccess.Parameters.AddVarChar("@ZLBM", dt.Rows[0]["ZLBM"].ToString());
            dataAccess.Parameters.AddVarChar("@NID", dt.Rows[0]["NID"].ToString());
            int result = 0;
            try
            {
                result = dataAccess.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                throw new Exception(ex.Message);
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return (result > 0 ? true : false);
        }
        #endregion

        #region bool DeleteICD10(string ID)
        /// <summary>
        /// bool DeleteICD10(string parameters)
        /// </summary>
        /// <param>string parameters</param>
        /// <returns>true if db operation successful otherwise false</returns>
        ///
        public bool DeleteICD10(string ID)
        {
            RisDAL dataAccess = new RisDAL();
            string sql = string.Format("Delete from tICD10 where ID='{0}'", ID);
            int result = 0;
            try
            {
                result = dataAccess.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                return false;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
            return (result > 0 ? true : false);
        }
        #endregion

        #region bool ImportICD10(DataSet icd10DataSet,bool isClear)
        public virtual bool ImportICD10(DataSet icd10DataSet, bool isClear)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                if (isClear)
                {
                    dataAccess.ExecuteNonQuery("delete from tICD10", RisDAL.ConnectionState.KeepOpen);
                }

                string sql;
                DataTable dt = icd10DataSet.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    dataAccess.Parameters.Clear();
                    sql = "insert into tICD10(ID,INAME,PY,WB,TJM,BZLB,ZLBM,JLZT,MEMO,Domain) values(@ID,@INAME,@PY,@WB,@TJM,@BZLB,@ZLBM,@JLZT,@MEMO,@Domain)";
                    dataAccess.Parameters.AddVarChar("@ID", row["ID"].ToString());
                    dataAccess.Parameters.AddVarChar("@INAME", row["INAME"].ToString());
                    dataAccess.Parameters.AddVarChar("@PY", row["PY"].ToString());
                    dataAccess.Parameters.AddVarChar("@WB", row["WB"].ToString());
                    dataAccess.Parameters.AddVarChar("@TJM", row["TJM"].ToString());
                    dataAccess.Parameters.AddVarChar("@BZLB", row["BZLB"].ToString());
                    dataAccess.Parameters.AddVarChar("@ZLBM", row["ZLBM"].ToString());
                    dataAccess.Parameters.AddVarChar("@JLZT", row["JLZT"].ToString());
                    dataAccess.Parameters.AddVarChar("@MEMO", row["MEMO"].ToString());
                    dataAccess.Parameters.AddVarChar("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }

                dataAccess.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
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
        #endregion
        #endregion

        #region IACRCodeDAO Section
        public virtual bool ImportProcedureCode(DataSet procedureCodeDataSet, bool isClear, string Site = "")
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();

                if (procedureCodeDataSet.Tables.Count < 1)
                {
                    return false;
                }
                string sql;

                foreach (DataRow myRow in procedureCodeDataSet.Tables["ProcedureCodeTable"].Rows)
                {
                    dataAccess.Parameters.Clear();
                    string sqlselect = string.Format("select count(*) from tProcedureCode where ProcedureCode='{0}' and Site ='{1}'", Convert.ToString(myRow["ProcedureCode"]), Site);
                    int count = Convert.ToInt32(dataAccess.ExecuteScalar(sqlselect, RisDAL.ConnectionState.KeepOpen));
                    sqlselect = string.Format("select count(*) from tProcedureCode where ShortcutCode='{0}' and ProcedureCode != '{1}' and ShortcutCode != '' and Site ='{2}'", Convert.ToString(myRow["ShortcutCode"]), Convert.ToString(myRow["ProcedureCode"]), Site);
                    int count0 = Convert.ToInt32(dataAccess.ExecuteScalar(sqlselect, RisDAL.ConnectionState.KeepOpen));
                    sqlselect = string.Format("select count(*) from tProcedureCode where ModalityType='{0}'and BodyCategory = '{1}' and BodyPart = '{2}' and CheckingItem = '{3}' and ProcedureCode != '{4}' and Site ='{5}'",
                        Convert.ToString(myRow["ModalityType"]), Convert.ToString(myRow["BodyCategory"]), Convert.ToString(myRow["BodyPart"]), Convert.ToString(myRow["CheckingItem"]), Convert.ToString(myRow["ProcedureCode"]), Site);
                    int count1 = Convert.ToInt32(dataAccess.ExecuteScalar(sqlselect, RisDAL.ConnectionState.KeepOpen));
                    string shortCutCode = (myRow["shortCutCode"] == DBNull.Value ? "" : Convert.ToString(myRow["shortCutCode"]));
                    //shortcutcode duplicated
                    if (count0 > 0)
                    {
                        ProduceUniqueShortCutCode(ref shortCutCode, Convert.ToString(myRow["ProcedureCode"]), Site);
                    }
                    if (count1 > 0)
                    {
                        continue;
                    }

                    if (count == 0)
                    {
                        sql = "Insert into tProcedureCode(ProcedureCode,Description,EnglishDescription,ModalityType,BodyPart,CheckingItem,Charge,Preparation,Frequency,BodyCategory,Duration,FilmSpec,FilmCount,ContrastName,ContrastDose,ImageCount,ExposalCount,BookingNotice,ShortcutCode,Domain,BodyPartFrequency,CheckingItemFrequency,ApprovedRadiologistWeight, TechnicianWeight, RadiologistWeight,Site)  "
                                               + " values(@ProcedureCode,@Description,@EnglishDescription,@ModalityType,@BodyPart,@CheckingItem,@Charge,@Preparation,@Frequency,@BodyCategory,@Duration,@FilmSpec,@FilmCount,@ContrastName,@ContrastDose,@ImageCount,@ExposalCount,@BookingNotice,@ShortcutCode,@Domain,@BodyPartFrequency,@CheckingItemFrequency,@ApprovedRadiologistWeight, 1, 1,@Site) ";
                        dataAccess.Parameters.Add("@ProcedureCode", Convert.ToString(myRow["ProcedureCode"]));
                        dataAccess.Parameters.Add("@Description", Convert.ToString(myRow["Description"]));
                        dataAccess.Parameters.Add("@EnglishDescription", Convert.ToString(myRow["EnglishDescription"]));
                        dataAccess.Parameters.Add("@ModalityType", Convert.ToString(myRow["ModalityType"]));
                        dataAccess.Parameters.Add("@BodyPart", Convert.ToString(myRow["BodyPart"]));
                        dataAccess.Parameters.Add("@CheckingItem", Convert.ToString(myRow["CheckingItem"]));
                        dataAccess.Parameters.AddDecimal("@Charge", Convert.ToDecimal(myRow["Charge"] == DBNull.Value ? 0 : myRow["Charge"]));
                        dataAccess.Parameters.Add("@Preparation", Convert.ToString(myRow["Preparation"]));
                        dataAccess.Parameters.AddInt("@Frequency", Convert.ToInt32(myRow["Frequency"] == DBNull.Value ? 0 : myRow["Frequency"]));
                        dataAccess.Parameters.Add("@BodyCategory", Convert.ToString(myRow["BodyCategory"]));
                        dataAccess.Parameters.AddInt("@Duration", Convert.ToInt32(myRow["Duration"] == DBNull.Value ? 0 : myRow["Duration"]));
                        dataAccess.Parameters.Add("@FilmSpec", Convert.ToString(myRow["FilmSpec"]));
                        dataAccess.Parameters.AddInt("@FilmCount", Convert.ToInt32(myRow["FilmCount"] == DBNull.Value ? 0 : myRow["FilmCount"]));
                        dataAccess.Parameters.Add("@ContrastName", Convert.ToString(myRow["ContrastName"]));
                        dataAccess.Parameters.Add("@ContrastDose", Convert.ToString(myRow["ContrastDose"]));
                        dataAccess.Parameters.AddInt("@ImageCount", Convert.ToInt32(myRow["ImageCount"] == DBNull.Value ? 0 : myRow["ImageCount"]));
                        dataAccess.Parameters.AddInt("@ExposalCount", Convert.ToInt32(myRow["ExposalCount"] == DBNull.Value ? 0 : myRow["ExposalCount"]));
                        dataAccess.Parameters.Add("@BookingNotice", Convert.ToString(myRow["BookingNotice"]));
                        dataAccess.Parameters.Add("@ShortcutCode", Convert.ToString(myRow["ShortcutCode"]));
                        dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                        dataAccess.Parameters.AddInt("@BodyPartFrequency", Convert.ToInt32(myRow["BodyPartFrequency"] == DBNull.Value ? 0 : myRow["BodyPartFrequency"]));
                        dataAccess.Parameters.AddInt("@CheckingItemFrequency", Convert.ToInt32(myRow["CheckingItemFrequency"] == DBNull.Value ? 0 : myRow["CheckingItemFrequency"]));
                        dataAccess.Parameters.AddInt("@ApprovedRadiologistWeight", Convert.ToInt32(myRow["ApprovedRadiologistWeight"] == DBNull.Value ? 0 : myRow["ApprovedRadiologistWeight"]));
                        dataAccess.Parameters.Add("@Site", Site);

                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    else
                    {
                        sql = string.Format("update tProcedureCode set Description=@Description,EnglishDescription=@EnglishDescription,ModalityType=@ModalityType,BodyPart=@BodyPart,CheckingItem=@CheckingItem,Charge=@Charge,Preparation=@Preparation,Frequency = @Frequency,BodyCategory=@BodyCategory,Duration=@Duration,FilmSpec=@FilmSpec,FilmCount=@FilmCount,ContrastName=@ContrastName,ContrastDose=@ContrastDose,ImageCount=@ImageCount,ExposalCount=@ExposalCount,BookingNotice=@BookingNotice,ShortcutCode=@ShortcutCode,Domain=@Domain,BodypartFrequency=@BodypartFrequency,CheckingItemFrequency=@CheckingItemFrequency,ApprovedRadiologistWeight=@ApprovedRadiologistWeight  "
                                              + " where ProcedureCode= '{0}' and Site ='{1}'", Convert.ToString(myRow["ProcedureCode"]), Site);

                        dataAccess.Parameters.Add("@Description", Convert.ToString(myRow["Description"]));
                        dataAccess.Parameters.Add("@EnglishDescription", Convert.ToString(myRow["EnglishDescription"]));
                        dataAccess.Parameters.Add("@ModalityType", Convert.ToString(myRow["ModalityType"]));
                        dataAccess.Parameters.Add("@BodyPart", Convert.ToString(myRow["BodyPart"]));
                        dataAccess.Parameters.Add("@CheckingItem", Convert.ToString(myRow["CheckingItem"]));
                        dataAccess.Parameters.AddDecimal("@Charge", Convert.ToDecimal(myRow["Charge"] == DBNull.Value ? 0 : myRow["Charge"]));
                        dataAccess.Parameters.Add("@Preparation", Convert.ToString(myRow["Preparation"]));
                        dataAccess.Parameters.AddInt("@Frequency", Convert.ToInt32(myRow["Frequency"] == DBNull.Value ? 0 : myRow["Frequency"]));
                        dataAccess.Parameters.Add("@BodyCategory", Convert.ToString(myRow["BodyCategory"]));
                        dataAccess.Parameters.AddInt("@Duration", Convert.ToInt32(myRow["Duration"] == DBNull.Value ? 0 : myRow["Duration"]));
                        dataAccess.Parameters.Add("@FilmSpec", Convert.ToString(myRow["FilmSpec"]));
                        dataAccess.Parameters.AddInt("@FilmCount", Convert.ToInt32(myRow["FilmCount"] == DBNull.Value ? 0 : myRow["FilmCount"]));
                        dataAccess.Parameters.Add("@ContrastName", Convert.ToString(myRow["ContrastName"]));
                        dataAccess.Parameters.Add("@ContrastDose", Convert.ToString(myRow["ContrastDose"]));
                        dataAccess.Parameters.AddInt("@ImageCount", Convert.ToInt32(myRow["ImageCount"] == DBNull.Value ? 0 : myRow["ImageCount"]));
                        dataAccess.Parameters.AddInt("@ExposalCount", Convert.ToInt32(myRow["ExposalCount"] == DBNull.Value ? 0 : myRow["ExposalCount"]));
                        dataAccess.Parameters.Add("@BookingNotice", Convert.ToString(myRow["BookingNotice"]));
                        dataAccess.Parameters.Add("@ShortcutCode", Convert.ToString(myRow["ShortcutCode"]));
                        dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                        dataAccess.Parameters.AddInt("@BodyPartFrequency", Convert.ToInt32(myRow["BodyPartFrequency"] == DBNull.Value ? 0 : myRow["BodyPartFrequency"]));
                        dataAccess.Parameters.AddInt("@CheckingItemFrequency", Convert.ToInt32(myRow["CheckingItemFrequency"] == DBNull.Value ? 0 : myRow["CheckingItemFrequency"]));
                        dataAccess.Parameters.AddInt("@ApprovedRadiologistWeight", Convert.ToInt32(myRow["ApprovedRadiologistWeight"] == DBNull.Value ? 0 : myRow["ApprovedRadiologistWeight"]));
                        dataAccess.Parameters.Add("@Site", Site);

                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                }

                foreach (DataRow myRow in procedureCodeDataSet.Tables["BodySystemMapTable"].Rows)
                {
                    if (myRow["ModalityType"] == null || myRow["BodyPart"] == null || myRow["ExamSystem"] == null)
                    {
                        continue;
                    }
                    if (myRow["ModalityType"] == DBNull.Value || myRow["BodyPart"] == DBNull.Value || myRow["ExamSystem"] == DBNull.Value)
                    {
                        continue;
                    }
                    dataAccess.Parameters.Clear();
                    string sqltemp;
                    string sqlselect = string.Format("select count(*) from tBodySystemMap where modalityType='{0}' and bodypart='{1}'", Convert.ToString(myRow["ModalityType"]), Convert.ToString(myRow["Bodypart"]));
                    int count = Convert.ToInt32(dataAccess.ExecuteScalar(sqlselect, RisDAL.ConnectionState.KeepOpen));
                    if (count == 0)
                    {
                        sqltemp = string.Format("insert into tBodySystemMap(ModalityType,Bodypart,ExamSystem,Domain) values('{0}','{1}','{2}','{3}')", Convert.ToString(myRow["ModalityType"]), Convert.ToString(myRow["Bodypart"]), Convert.ToString(myRow["ExamSystem"]), CommonGlobalSettings.Utilities.GetCurDomain());
                        dataAccess.ExecuteNonQuery(sqltemp, RisDAL.ConnectionState.KeepOpen);
                    }
                    else
                    {
                        sqltemp = string.Format("Update tBodySystemMap set ExamSystem='{0}'  where modalityType='{1}' and bodypart='{2}'", Convert.ToString(myRow["ExamSystem"]), Convert.ToString(myRow["ModalityType"]), Convert.ToString(myRow["Bodypart"]));
                        dataAccess.ExecuteNonQuery(sqltemp, RisDAL.ConnectionState.KeepOpen);
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

        /// <summary>
        /// execute insert/update procedurecode
        /// </summary>
        /// <param name="procedureCodeDataSet"></param>
        /// <param name="errorCode">0--shortcut duplicated,1--modalitytype&bodycategory&bodypart&checkingitem is duplicated</param>
        /// <param name="errorString">not empty if error code 0 or 1 else empty</param>
        /// <returns>false import failure else successful</returns>
        public virtual bool ImportProcedureCode(DataSet procedureCodeDataSet, ref int errorCode, ref string errorString, string Site)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                string errorStrDelimiter = ",";
                string shortCutCode = "";

                if (procedureCodeDataSet.Tables.Count < 1)
                {
                    return false;
                }
                string sql;

                foreach (DataRow myRow in procedureCodeDataSet.Tables["ProcedureCodeTable"].Rows)
                {
                    dataAccess.Parameters.Clear();
                    string sqlselect = string.Format("select count(*) from tProcedureCode where ProcedureCode='{0}' and Site ='{1}'", Convert.ToString(myRow["ProcedureCode"]), Site);
                    int count = Convert.ToInt32(dataAccess.ExecuteScalar(sqlselect, RisDAL.ConnectionState.KeepOpen));
                    sqlselect = string.Format("select count(*) from tProcedureCode where ShortcutCode='{0}' and ProcedureCode != '{1}' and ShortcutCode != '' and Site ='{2}'", Convert.ToString(myRow["ShortcutCode"]), Convert.ToString(myRow["ProcedureCode"]), Site);
                    int count0 = Convert.ToInt32(dataAccess.ExecuteScalar(sqlselect, RisDAL.ConnectionState.KeepOpen));
                    sqlselect = string.Format("select count(*) from tProcedureCode where ModalityType='{0}'and BodyCategory = '{1}' and BodyPart = '{2}' and CheckingItem = '{3}' and ProcedureCode != '{4}' and Site ='{5}'",
                        Convert.ToString(myRow["ModalityType"]), Convert.ToString(myRow["BodyCategory"]), Convert.ToString(myRow["BodyPart"]), Convert.ToString(myRow["CheckingItem"]), Convert.ToString(myRow["ProcedureCode"]), Site);
                    int count1 = Convert.ToInt32(dataAccess.ExecuteScalar(sqlselect, RisDAL.ConnectionState.KeepOpen));
                    shortCutCode = (myRow["shortCutCode"] == DBNull.Value ? "" : Convert.ToString(myRow["shortCutCode"]));
                    //shortcutcode duplicated
                    if (count0 > 0)
                    {
                        /* current use + "_1" policy
                        errorCode = 0;
                        errorString = Convert.ToString(myRow["ShortcutCode"]);
                        dataAccess.RollbackTransaction();
                        return false;
                         * */
                        ProduceUniqueShortCutCode(ref shortCutCode, Convert.ToString(myRow["ProcedureCode"]), Site);
                    }
                    //ModalityType,BodyCategory,BodyPart,CheckingItem are duplicated
                    if (count1 > 0)
                    {
                        //Modified by Blue for EK_HI00120232
                        //errorCode = 1;
                        //errorString = Convert.ToString(myRow["ModalityType"]) + errorStrDelimiter + Convert.ToString(myRow["BodyCategory"]) + errorStrDelimiter + Convert.ToString(myRow["BodyPart"]) + errorStrDelimiter + Convert.ToString(myRow["CheckingItem"]);
                        //dataAccess.RollbackTransaction();
                        //return false;
                        continue;
                    }


                    if (count == 0)
                    {
                        #region

                        #endregion
                        sql = "Insert into tProcedureCode(ProcedureCode,Description,EnglishDescription,ModalityType,BodyPart,CheckingItem,Charge,Preparation,Frequency,BodyCategory,Duration,FilmSpec,FilmCount,ContrastName,ContrastDose,ImageCount,ExposalCount,BookingNotice,ShortcutCode,Effective,Enhance,Domain,BodyPartFrequency,CheckingItemFrequency,TechnicianWeight,RadiologistWeight,ApprovedRadiologistWeight,Site)  "
                                               + " values(@ProcedureCode,@Description,@EnglishDescription,@ModalityType,@BodyPart,@CheckingItem,@Charge,@Preparation,@Frequency,@BodyCategory,@Duration,@FilmSpec,@FilmCount,@ContrastName,@ContrastDose,@ImageCount,@ExposalCount,@BookingNotice,@ShortcutCode,@Effective,@Enhance,@Domain,@BodyPartFrequency,@CheckingItemFrequency, 1, 1, 1,@Site) ";
                        dataAccess.Parameters.Add("@ProcedureCode", myRow["ProcedureCode"] == DBNull.Value ? "" : Convert.ToString(myRow["ProcedureCode"]));
                        dataAccess.Parameters.Add("@Description", myRow["Description"] == DBNull.Value ? "" : Convert.ToString(myRow["Description"]));
                        dataAccess.Parameters.Add("@EnglishDescription", myRow["EnglishDescription"] == DBNull.Value ? "" : Convert.ToString(myRow["EnglishDescription"]));
                        dataAccess.Parameters.Add("@ModalityType", myRow["ModalityType"] == DBNull.Value ? "" : Convert.ToString(myRow["ModalityType"]));
                        dataAccess.Parameters.Add("@BodyPart", myRow["BodyPart"] == DBNull.Value ? "" : Convert.ToString(myRow["BodyPart"]));
                        dataAccess.Parameters.Add("@CheckingItem", myRow["CheckingItem"] == DBNull.Value ? "" : Convert.ToString(myRow["CheckingItem"]));
                        dataAccess.Parameters.AddDecimal("@Charge", Convert.ToDecimal(myRow["Charge"] == DBNull.Value ? 0 : myRow["Charge"]));
                        dataAccess.Parameters.Add("@Preparation", myRow["Preparation"] == DBNull.Value ? "" : Convert.ToString(myRow["Preparation"]));
                        dataAccess.Parameters.AddInt("@Frequency", Convert.ToInt32(myRow["Frequency"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["Frequency"])));
                        dataAccess.Parameters.Add("@BodyCategory", myRow["BodyCategory"] == DBNull.Value ? "" : Convert.ToString(myRow["BodyCategory"]));
                        dataAccess.Parameters.AddInt("@Duration", Convert.ToInt32(myRow["Duration"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["Duration"])));
                        dataAccess.Parameters.Add("@FilmSpec", myRow["FilmSpec"] == DBNull.Value ? "" : Convert.ToString(myRow["FilmSpec"]));
                        dataAccess.Parameters.AddInt("@FilmCount", Convert.ToInt32(myRow["FilmCount"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["FilmCount"])));
                        dataAccess.Parameters.Add("@ContrastName", myRow["ContrastName"] == DBNull.Value ? "" : Convert.ToString(myRow["ContrastName"]));
                        dataAccess.Parameters.Add("@ContrastDose", myRow["ContrastDose"] == DBNull.Value ? "" : Convert.ToString(myRow["ContrastDose"]));
                        dataAccess.Parameters.AddInt("@ImageCount", Convert.ToInt32(myRow["ImageCount"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["ImageCount"])));
                        dataAccess.Parameters.AddInt("@ExposalCount", Convert.ToInt32(myRow["ExposalCount"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["ExposalCount"])));
                        dataAccess.Parameters.Add("@BookingNotice", myRow["BookingNotice"] == DBNull.Value ? "" : Convert.ToString(myRow["BookingNotice"]));
                        dataAccess.Parameters.Add("@ShortcutCode", shortCutCode);
                        dataAccess.Parameters.AddInt("@Effective", Convert.ToInt32(myRow["Effective"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["Effective"])));
                        dataAccess.Parameters.AddInt("@Enhance", Convert.ToInt32(myRow["Enhance"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["Enhance"])));
                        dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                        dataAccess.Parameters.AddInt("@BodyPartFrequency", Convert.ToInt32(myRow["BodyPartFrequency"] == DBNull.Value ? 0 : myRow["BodyPartFrequency"]));
                        dataAccess.Parameters.AddInt("@CheckingItemFrequency", Convert.ToInt32(myRow["CheckingItemFrequency"] == DBNull.Value ? 0 : myRow["CheckingItemFrequency"]));
                        dataAccess.Parameters.AddInt("@TechnicianWeight", Convert.ToInt32(myRow["TechnicianWeight"] == DBNull.Value ? 0 : myRow["TechnicianWeight"]));
                        dataAccess.Parameters.AddInt("@RadiologistWeight", Convert.ToInt32(myRow["RadiologistWeight"] == DBNull.Value ? 0 : myRow["RadiologistWeight"]));
                        dataAccess.Parameters.AddInt("@ApprovedRadiologistWeight", Convert.ToInt32(myRow["ApprovedRadiologistWeight"] == DBNull.Value ? 0 : myRow["ApprovedRadiologistWeight"]));
                        dataAccess.Parameters.Add("@Site", Site);
                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    else
                    {
                        sql = string.Format(@"update tProcedureCode set Description=@Description,EnglishDescription=@EnglishDescription,ModalityType=@ModalityType,BodyPart=@BodyPart,
                                      CheckingItem=@CheckingItem,Charge=@Charge,Preparation=@Preparation,Frequency = @Frequency,BodyCategory=@BodyCategory,Duration=@Duration,FilmSpec=@FilmSpec,
                                      FilmCount=@FilmCount,ContrastName=@ContrastName,ContrastDose=@ContrastDose,ImageCount=@ImageCount,ExposalCount=@ExposalCount,BookingNotice=@BookingNotice,
                                      ShortcutCode=@ShortcutCode,Effective=@Effective,Enhance=@Enhance,Domain=@Domain,BodypartFrequency=@BodypartFrequency,CheckingItemFrequency=@CheckingItemFrequency,
                                      TechnicianWeight=@TechnicianWeight,RadiologistWeight=@RadiologistWeight,ApprovedRadiologistWeight=@ApprovedRadiologistWeight,Site=@Site"
                                              + " where ProcedureCode= '{0}'", Convert.ToString(myRow["ProcedureCode"]));

                        dataAccess.Parameters.Add("@Description", myRow["Description"] == DBNull.Value ? "" : Convert.ToString(myRow["Description"]));
                        dataAccess.Parameters.Add("@EnglishDescription", myRow["EnglishDescription"] == DBNull.Value ? "" : Convert.ToString(myRow["EnglishDescription"]));
                        dataAccess.Parameters.Add("@ModalityType", myRow["ModalityType"] == DBNull.Value ? "" : Convert.ToString(myRow["ModalityType"]));
                        dataAccess.Parameters.Add("@BodyPart", myRow["BodyPart"] == DBNull.Value ? "" : Convert.ToString(myRow["BodyPart"]));
                        dataAccess.Parameters.Add("@CheckingItem", myRow["CheckingItem"] == DBNull.Value ? "" : Convert.ToString(myRow["CheckingItem"]));
                        dataAccess.Parameters.AddDecimal("@Charge", Convert.ToDecimal(myRow["Charge"] == DBNull.Value ? 0 : myRow["Charge"]));
                        dataAccess.Parameters.Add("@Preparation", myRow["Preparation"] == DBNull.Value ? "" : Convert.ToString(myRow["Preparation"]));
                        dataAccess.Parameters.AddInt("@Frequency", Convert.ToInt32(myRow["Frequency"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["Frequency"])));
                        dataAccess.Parameters.Add("@BodyCategory", myRow["BodyCategory"] == DBNull.Value ? "" : Convert.ToString(myRow["BodyCategory"]));
                        dataAccess.Parameters.AddInt("@Duration", Convert.ToInt32(myRow["Duration"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["Duration"])));
                        dataAccess.Parameters.Add("@FilmSpec", myRow["FilmSpec"] == DBNull.Value ? "" : Convert.ToString(myRow["FilmSpec"]));
                        dataAccess.Parameters.AddInt("@FilmCount", Convert.ToInt32(myRow["FilmCount"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["FilmCount"])));
                        dataAccess.Parameters.Add("@ContrastName", myRow["ContrastName"] == DBNull.Value ? "" : Convert.ToString(myRow["ContrastName"]));
                        dataAccess.Parameters.Add("@ContrastDose", myRow["ContrastDose"] == DBNull.Value ? "" : Convert.ToString(myRow["ContrastDose"]));
                        dataAccess.Parameters.AddInt("@ImageCount", Convert.ToInt32(myRow["ImageCount"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["ImageCount"])));
                        dataAccess.Parameters.AddInt("@ExposalCount", Convert.ToInt32(myRow["ExposalCount"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["ExposalCount"])));
                        dataAccess.Parameters.Add("@BookingNotice", myRow["BookingNotice"] == DBNull.Value ? "" : Convert.ToString(myRow["BookingNotice"]));
                        dataAccess.Parameters.Add("@ShortcutCode", shortCutCode);
                        dataAccess.Parameters.AddInt("@Effective", Convert.ToInt32(myRow["Effective"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["Effective"])));
                        dataAccess.Parameters.AddInt("@Enhance", Convert.ToInt32(myRow["Enhance"] == DBNull.Value ? 0 : Convert.ToInt32(myRow["Enhance"])));
                        dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                        dataAccess.Parameters.AddInt("@BodyPartFrequency", Convert.ToInt32(myRow["BodyPartFrequency"] == DBNull.Value ? 0 : myRow["BodyPartFrequency"]));
                        dataAccess.Parameters.AddInt("@CheckingItemFrequency", Convert.ToInt32(myRow["CheckingItemFrequency"] == DBNull.Value ? 0 : myRow["CheckingItemFrequency"]));
                        dataAccess.Parameters.AddInt("@TechnicianWeight", Convert.ToInt32(myRow["TechnicianWeight"] == DBNull.Value ? 0 : myRow["TechnicianWeight"]));
                        dataAccess.Parameters.AddInt("@RadiologistWeight", Convert.ToInt32(myRow["RadiologistWeight"] == DBNull.Value ? 0 : myRow["RadiologistWeight"]));
                        dataAccess.Parameters.AddInt("@ApprovedRadiologistWeight", Convert.ToInt32(myRow["ApprovedRadiologistWeight"] == DBNull.Value ? 0 : myRow["ApprovedRadiologistWeight"]));
                        dataAccess.Parameters.Add("@Site", Site);
                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    #region BodySystemMap
                    if (myRow["ModalityType"] == null || myRow["BodyPart"] == null || myRow["ExamSystem"] == null)
                    {
                        continue;
                    }
                    if (myRow["ModalityType"] == DBNull.Value || myRow["BodyPart"] == DBNull.Value || myRow["ExamSystem"] == DBNull.Value)
                    {
                        continue;
                    }
                    dataAccess.Parameters.Clear();
                    string sqltemp;
                    sqlselect = string.Format("select count(*) from tBodySystemMap where modalityType='{0}' and bodypart='{1}' and ExamSystem='{2}' ", Convert.ToString(myRow["ModalityType"]), Convert.ToString(myRow["Bodypart"]), Convert.ToString(myRow["ExamSystem"]));
                    count = Convert.ToInt32(dataAccess.ExecuteScalar(sqlselect, RisDAL.ConnectionState.KeepOpen));
                    if (count == 0)
                    {
                        sqltemp = string.Format("insert into tBodySystemMap(ModalityType,Bodypart,ExamSystem) values('{0}','{1}','{2}')", Convert.ToString(myRow["ModalityType"]), Convert.ToString(myRow["Bodypart"]), Convert.ToString(myRow["ExamSystem"]));
                        dataAccess.ExecuteNonQuery(sqltemp, RisDAL.ConnectionState.KeepOpen);
                    }
                    //else
                    //{
                    //    sqltemp = string.Format("Update tBodySystemMap set ExamSystem='{0}'  where modalityType='{1}' and bodypart='{2}'", Convert.ToString(myRow["ExamSystem"]), Convert.ToString(myRow["ModalityType"]), Convert.ToString(myRow["Bodypart"]));
                    //    dataAccess.ExecuteNonQuery(sqltemp, KodakDAL.ConnectionState.KeepOpen);
                    //}
                    #endregion
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

        #region Modified by Blue Chen for US19895, 10/30/2014
        public virtual bool ImportBodyPartSystemMap(DataSet dsBodyPartSystemMap, ref int errorCode, ref string errorString)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();

                if (dsBodyPartSystemMap.Tables.Count < 1)
                {
                    return false;
                }
                string sql;

                foreach (DataRow myRow in dsBodyPartSystemMap.Tables["BodyPartSystemMapTable"].Rows)
                {
                    dataAccess.Parameters.Clear();
                    string sqlselect = string.Format("select count(*) from tBodySystemMap where ModalityType='{0}' and BodyPart='{1}' and ExamSystem='{2}' and Domain='{3}' and Site ='{4}'", myRow["ModalityType"].ToString(), myRow["BodyPart"].ToString(), myRow["ExamSystem"].ToString(), CommonGlobalSettings.Utilities.GetCurDomain(), myRow["Site"].ToString());
                    int count = Convert.ToInt32(dataAccess.ExecuteScalar(sqlselect, RisDAL.ConnectionState.KeepOpen));

                    if (count == 0)
                    {
                        sql = "Insert into tBodySystemMap(ModalityType,BodyPart,ExamSystem,Domain,Site,UniqueID)  "
                            + " values(@ModalityType,@BodyPart,@ExamSystem,@Domain,@Site,NEWID()) ";
                        dataAccess.Parameters.Add("@ModalityType", myRow["ModalityType"] == DBNull.Value ? "" : Convert.ToString(myRow["ModalityType"]));
                        dataAccess.Parameters.Add("@BodyPart", myRow["BodyPart"] == DBNull.Value ? "" : Convert.ToString(myRow["BodyPart"]));
                        dataAccess.Parameters.Add("@ExamSystem", myRow["ExamSystem"] == DBNull.Value ? "" : Convert.ToString(myRow["ExamSystem"]));
                        dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                        dataAccess.Parameters.Add("@Site", myRow["Site"] == DBNull.Value ? "" : Convert.ToString(myRow["Site"]));
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
        #endregion

        private void ProduceUniqueShortCutCode(ref string shortCutCode, string procedureCode, string site)
        {
            RisDAL dal = new RisDAL();
            shortCutCode += "_1";
            string sqlSrcItemName = string.Format("select ShortCutCode from tProcedureCode where ShortCutCode ='" + shortCutCode + "'" + "and ProcedureCode != '" + procedureCode + "' and Site ='{0}'", site);//get source item node item name
            try
            {
                DataTable dtName = dal.ExecuteQuery(sqlSrcItemName);
                if (dtName != null && dtName.Rows.Count > 0)
                {
                    string newName = dtName.Rows[0][0].ToString() + "_1";//default duplicated name add "_1" string
                    bool stillHaveSameName = true;
                    while (stillHaveSameName)
                    {
                        DataTable dt = null;
                        string sqlSameDuplitedAgain = "select ShortCutCode from tProcedureCode where ShortCutCode ='" + newName + "'" + "and ProcedureCode != '" + procedureCode + "'";
                        dt = dal.ExecuteQuery(sqlSameDuplitedAgain, RisDAL.ConnectionState.KeepOpen);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            newName = dt.Rows[0][0].ToString() + "_1";
                        }
                        else
                        {
                            stillHaveSameName = false;
                            shortCutCode = newName;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                     (new System.Diagnostics.StackFrame(true)).GetFileName(),
                     Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
        }

        public virtual bool ImportACRcode(DataSet acrcodeDataSet, bool isClear)
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
                    dataAccess.Parameters.Clear();
                    sql = "Insert into tACRCodeAnatomical(AID,Description,DescriptionEn,Domain)  values(@AID,@Description,@DescriptionEn,@Domain) ";
                    dataAccess.Parameters.Add("@AID", Convert.ToString(myRow["AID"]));
                    dataAccess.Parameters.Add("@Description", Convert.ToString(myRow["Description"]));
                    dataAccess.Parameters.Add("@DescriptionEn", Convert.ToString(myRow["DescriptionEn"]));
                    dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }

                foreach (DataRow myRow in acrcodeDataSet.Tables["SubAnatomical"].Rows)
                {
                    dataAccess.Parameters.Clear();
                    sql = "Insert into tACRCodeSubAnatomical(AID,SID,Description,DescriptionEn,IsUserAdd,Domain)  values(@AID,@SID,@Description,@DescriptionEn,@IsUserAdd,@Domain) ";
                    dataAccess.Parameters.Add("@AID", Convert.ToString(myRow["AID"]));
                    dataAccess.Parameters.Add("@SID", Convert.ToString(myRow["SID"]));
                    dataAccess.Parameters.Add("@Description", Convert.ToString(myRow["Description"]));
                    dataAccess.Parameters.Add("@DescriptionEn", Convert.ToString(myRow["DescriptionEn"]));
                    dataAccess.Parameters.Add("@IsUserAdd", Convert.ToInt32(myRow["IsUserAdd"]));
                    dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }

                foreach (DataRow myRow in acrcodeDataSet.Tables["Pathological"].Rows)
                {
                    dataAccess.Parameters.Clear();
                    sql = "Insert into tACRCodePathological(AID,PID,Description,DescriptionEn,Domain)  values(@AID,@PID,@Description,@DescriptionEn,@Domain) ";
                    dataAccess.Parameters.Add("@AID", Convert.ToString(myRow["AID"]));
                    dataAccess.Parameters.Add("@PID", Convert.ToString(myRow["PID"]));
                    dataAccess.Parameters.Add("@Description", Convert.ToString(myRow["Description"]));
                    dataAccess.Parameters.Add("@DescriptionEn", Convert.ToString(myRow["DescriptionEn"]));
                    dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }

                foreach (DataRow myRow in acrcodeDataSet.Tables["SubPathological"].Rows)
                {
                    dataAccess.Parameters.Clear();
                    sql = "Insert into tACRCodeSubPathological(AID,PID,SID,Description,DescriptionEn,IsUserAdd,Domain)  values(@AID,@PID,@SID,@Description,@DescriptionEn,@IsUserAdd,@Domain) ";
                    dataAccess.Parameters.Add("@AID", Convert.ToString(myRow["AID"]));
                    dataAccess.Parameters.Add("@PID", Convert.ToString(myRow["PID"]));
                    dataAccess.Parameters.Add("@SID", Convert.ToString(myRow["SID"]));
                    dataAccess.Parameters.Add("@Description", Convert.ToString(myRow["Description"]));
                    dataAccess.Parameters.Add("@DescriptionEn", Convert.ToString(myRow["DescriptionEn"]));
                    dataAccess.Parameters.Add("@IsUserAdd", Convert.ToInt32(myRow["IsUserAdd"]));
                    dataAccess.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
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
        public virtual DataSet GetAllAcrCode()
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
        public virtual DataSet GetAllProcedureCode(string Site = "")
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            string sql = string.Format(@"select distinct A.*,B.ExamSystem from tProcedureCode A 
                                        left join 
                                        (select ModalityType, Bodypart,MAX(ExamSystem) ExamSystem from tBodySystemMap group by ModalityType,Bodypart) as B
                                         on A.ModalityType = B.ModalityType  
                                        and A.BodyPart = B.BodyPart where A.Domain='{0}' And A.Site='{1}' order by A.ModalityType", CommonGlobalSettings.Utilities.GetCurDomain(), Site);

            string sql1 = "select * from tBodySystemMap";
            string sql2 = "select * from tDictionaryValue where tag=12 ORDER BY OrderID";
            try
            {
                DataTable procedureCodeTable;
                DataTable bodySystemMapTable;
                DataTable bodyCategoryTable;
                procedureCodeTable = dataAccess.ExecuteQuery(sql);
                procedureCodeTable.TableName = "ProcedureCodeTable";
                ds.Tables.Add(procedureCodeTable);
                bodySystemMapTable = dataAccess.ExecuteQuery(sql1);
                bodySystemMapTable.TableName = "BodySystemMapTable";
                ds.Tables.Add(bodySystemMapTable);
                bodyCategoryTable = dataAccess.ExecuteQuery(sql2);
                bodyCategoryTable.TableName = "BodyCategoryTable";
                ds.Tables.Add(bodyCategoryTable);
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
        public virtual DataSet GetAllUser()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            string sqlAnatomical = "select UserGuid,LoginName,LocalName from tUser where DeleteMark = 0";
            try
            {
                DataTable dt = new DataTable();
                dt = dataAccess.ExecuteQuery(sqlAnatomical);
                ds.Tables.Add(dt);

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
        public virtual DataTable GetMainAnatomy()
        {

            DataTable anatomyTable = null;
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string strQuery = @"select * from tACRCodeAnatomical order by AID";
                anatomyTable = oKodakDAL.ExecuteQuery(strQuery);

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

            return anatomyTable;
        }
        public virtual DataTable GetSubAnatomy(int aid)
        {
            DataTable subAnatomyTable = null;
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string strQuery = string.Format(@"select * from tACRCodeSubAnatomical where aid = '{0}' order by sid ", aid);
                subAnatomyTable = oKodakDAL.ExecuteQuery(strQuery);

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

            return subAnatomyTable;
        }

        public virtual DataTable GetMainPathology(int aid)
        {
            DataTable pathologyTable = null;
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string strQuery = string.Format(@"select * from tACRCodePathological where aid = '{0}' order by pid ", aid);
                pathologyTable = oKodakDAL.ExecuteQuery(strQuery);

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

            return pathologyTable;
        }
        public virtual DataTable GetSubPathology(int aid, int pid)
        {
            DataTable subPathologyTable = null;
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string strQuery = string.Format(@"select * from tACRCodeSubPathological where aid = '{0}' and pid = '{1}' order by sid ", aid, pid);
                subPathologyTable = oKodakDAL.ExecuteQuery(strQuery);

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

            return subPathologyTable;
        }
        public virtual bool AddNewAnatomy(string strAid, string strSid, string strDesc, string strDesc_en, string strDomain)
        {
            int mark = 0;
            strAid.Trim();
            strSid.Trim();
            strDesc.Trim();
            strDesc.Replace("'", "''");
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //DEFECT NO.EK_HI00060978
                string strQuery = string.Format(@"insert into tACRCodeSubAnatomical(aid, sid, description,isuseradd,domain)  values('{0:D}', '{1:D}', '{2:D}','{3:D}','{4}')", strAid, strSid, strDesc, 1, strDomain);
                mark = oKodakDAL.ExecuteNonQuery(strQuery);

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
            return true;
        }
        public virtual string AddNewAnatomyStorProc(string strAid, string strDesc, string strDesc_en, string strDomain)
        {
            string strSid = null;
            strDesc.Replace("'", "''");
            RisDAL oKodakDAL = new RisDAL();
            try
            {

                string szstoredProc = @"SP_AddAnatomy";
                oKodakDAL.Parameters.AddVarChar("@aid", strAid, 1);
                oKodakDAL.Parameters.AddVarChar("@desc", strDesc, 255);
                oKodakDAL.Parameters.AddVarChar("@domain", strDomain, 255);
                //DEFECT NO.EK_HI00060978
                oKodakDAL.BeginTransaction();
                strSid = Convert.ToString(oKodakDAL.ExecuteScalarSP(szstoredProc, RisDAL.ConnectionState.KeepOpen));
                string strQuery = string.Format(@"update tACRCodeSubAnatomical set isuseradd= 1 where aid='{0:D}' and sid ='{1:D}'", strAid, strSid);
                oKodakDAL.ExecuteNonQuery(strQuery, RisDAL.ConnectionState.KeepOpen);
                oKodakDAL.CommitTransaction();
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
        public virtual bool AddNewPathology(string strAid, string strPid, string strSid, string strDesc, string strDesc_en, string strDomain)
        {
            int mark = 0;
            strAid.Trim();
            strPid.Trim();
            strSid.Trim();
            strDesc.Trim();
            strDesc.Replace("'", "''");
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //DEFECT NO.EK_HI00060978
                string strQuery = string.Format(@"insert into tACRCodeSubPathological(aid, pid, sid, description,isuseradd,Domain)  values('{0:D}', '{1:D}', '{2:D}','{3:D}','{4:D}','{5}')", strAid, strPid, strSid, strDesc, 1, strDomain);
                mark = oKodakDAL.ExecuteNonQuery(strQuery);

            }
            catch (Exception Ex)
            {
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
        public virtual string AddNewPathologyStorProc(string strAid, string strPid, string strDesc, string strDesc_en, string strDomain)
        {

            string strSid = null;
            strDesc.Replace("'", "''");
            RisDAL oKodakDAL = new RisDAL();
            try
            {

                string szstoredProc = @"SP_AddPathology";
                oKodakDAL.Parameters.AddVarChar("@aid", strAid, 1);
                oKodakDAL.Parameters.AddVarChar("@pid", strPid, 1);
                oKodakDAL.Parameters.AddVarChar("@desc", strDesc, 255);
                oKodakDAL.Parameters.AddVarChar("@domain", strDomain, 255);
                //DEFECT NO.EK_HI00060978
                oKodakDAL.BeginTransaction();
                strSid = Convert.ToString(oKodakDAL.ExecuteScalarSP(szstoredProc, RisDAL.ConnectionState.KeepOpen));
                string strQuery = string.Format(@"update tACRCodeSubPathological set isuseradd= 1 where aid='{0:D}' and pid ='{1:D}' and sid ='{2:D}'", strAid, strPid, strSid);
                oKodakDAL.ExecuteNonQuery(strQuery, RisDAL.ConnectionState.KeepOpen);
                oKodakDAL.CommitTransaction();
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
        public virtual bool UpdateMainAnatomy(string strAid, string strDesc, string strDesc_en)
        {
            strDesc.Replace("'", "''");
            int mark = 0;
            strAid.Trim();
            strDesc.Trim();
            strDesc.Replace("'", "''");
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                DataTable existAnatomyTable = GetMainAnatomy();
                if (existAnatomyTable != null && existAnatomyTable.Rows.Count != 0)
                {
                    foreach (DataRow myRow in existAnatomyTable.Rows)
                    {
                        if (myRow["description"].ToString().ToUpper() == strDesc.ToUpper())
                        {

                            throw new Exception("SameAnatomyDesc");

                        }
                    }
                }
                string strQuery = string.Format(@"update tACRCodeAnatomical set description='{1:D}' where aid='{0:D}'", strAid, strDesc);
                mark = oKodakDAL.ExecuteNonQuery(strQuery);

            }
            catch (Exception Ex)
            {
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
        public virtual bool UpdateSubAnatomy(string strAid, string strSid, string strDesc, string strDesc_en)
        {
            int mark = 0;
            strAid.Trim();
            strSid.Trim();
            strDesc.Trim();
            strDesc.Replace("'", "''");
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                DataTable existAnatomyTable = GetSubAnatomy(int.Parse(strAid));
                if (existAnatomyTable != null && existAnatomyTable.Rows.Count != 0)
                {
                    foreach (DataRow myRow in existAnatomyTable.Rows)
                    {
                        if (myRow["description"].ToString().ToUpper() == strDesc.ToUpper())
                        {
                            throw new Exception("SameAnatomyDesc");

                        }
                    }
                }
                string strQuery = string.Format(@"update tACRCodeSubAnatomical set description='{2:D}' where aid='{0:D}' and sid='{1:D}'", strAid, strSid, strDesc);
                mark = oKodakDAL.ExecuteNonQuery(strQuery);

            }
            catch (Exception Ex)
            {
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
        public virtual bool UpdateMainPathology(string strAid, string strPid, string strDesc, string strDesc_en)
        {
            int mark = 0;
            strAid.Trim();
            strPid.Trim();
            strDesc.Trim();
            strDesc.Replace("'", "''");
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                DataTable existPathologyTable = GetMainPathology(int.Parse(strAid));
                if (existPathologyTable != null && existPathologyTable.Rows.Count != 0)
                {
                    foreach (DataRow myRow in existPathologyTable.Rows)
                    {
                        if (myRow["description"].ToString().ToUpper() == strDesc.ToUpper())
                        {
                            throw new Exception("SamePathologyDesc");

                        }
                    }
                }
                string strQuery = string.Format(@"update tACRCodePathological set description='{2:D}' where aid='{0:D}' and pid='{1:D}'", strAid, strPid, strDesc);
                mark = oKodakDAL.ExecuteNonQuery(strQuery);

            }
            catch (Exception Ex)
            {
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
        public virtual bool UpdateSubPathology(string strAid, string strPid, string strSid, string strDesc, string strDesc_en)
        {

            int mark = 0;
            strAid.Trim();
            strPid.Trim();
            strSid.Trim();
            strDesc.Trim();
            strDesc.Replace("'", "''");
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                DataTable existPathologyTable = GetSubPathology(int.Parse(strAid), int.Parse(strPid));
                if (existPathologyTable != null && existPathologyTable.Rows.Count != 0)
                {
                    foreach (DataRow myRow in existPathologyTable.Rows)
                    {
                        if (myRow["description"].ToString().ToUpper() == strDesc.ToUpper())
                        {
                            throw new Exception("SamePathologyDesc");

                        }
                    }
                }
                string strQuery = string.Format(@"update tACRCodeSubPathological set description='{3:D}' where aid='{0:D}' and pid='{1:D}' and sid='{2:D}'", strAid, strPid, strSid, strDesc);
                mark = oKodakDAL.ExecuteNonQuery(strQuery);

            }
            catch (Exception Ex)
            {
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

        public virtual bool IsExistAnatomay(string strAid, string strSid, out string strResultMessage)
        {
            strAid.Trim();
            strSid.Trim();
            RisDAL oKodakDAL = new RisDAL();
            DataTable dt = new DataTable();
            int iCount = 0;
            strResultMessage = "";
            try
            {
                //DEFECT NO.EK_HI00060978、EK_HI00061781
                List<string> ls = new List<string>();
                string strSQL1 = string.Format("select count(1) from tReport where acrcode='{0}'", strAid + strSid + ".");
                string strSQL2 = string.Format("select count(1) from tTeaching where acrcode='{0}'", strAid + strSid + ".");
                string strSQL3 = string.Format("select count(1) from tReportList where acrcode='{0}'", strAid + strSid + ".");
                string strSQL4 = string.Format("select count(1) from tReportTemplate where acrcode='{0}'", strAid + strSid + ".");
                ls.Add(strSQL1);
                ls.Add(strSQL2);
                ls.Add(strSQL3);
                iCount = Convert.ToInt32(oKodakDAL.ExecuteScalar(strSQL4).ToString());
                //exists in tReportTemplate,so we should nofity the user
                if (iCount > 0)
                {
                    strResultMessage = "This ACR Code is Using in Report Template!";
                    return true;
                }
                iCount = 0;
                //others(tReport,tTeaching,tReportList)
                foreach (string str in ls)
                {
                    iCount += Convert.ToInt32(oKodakDAL.ExecuteScalar(str).ToString());
                }
                if (iCount > 0)
                {
                    strResultMessage = "This ACR Code is in use!";
                    return true;
                }
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
            return false;
        }

        public virtual bool IsExistPathology(string strAid, string strPid, string strSid, out string strResultMessage)
        {
            strAid.Trim();
            strSid.Trim();
            strPid.Trim();
            RisDAL oKodakDAL = new RisDAL();
            DataTable dt = new DataTable();
            int iCount = 0;
            strResultMessage = "";
            try
            {
                //DEFECT NO.EK_HI00060978、EK_HI00061781
                List<string> ls = new List<string>();
                string strSQL1 = string.Format("select count(1) from tReport where acrcode like '{0}' ", strAid + "%" + "." + strPid + strSid);
                string strSQL2 = string.Format("select count(1) from tTeaching where acrcode like '{0}'", strAid + "%" + "." + strPid + strSid);
                #region EK_HI00061781 jameswei 2007-11-23 reportlist is only the history for report
                //string strSQL3 = string.Format("select count(1) from tReportList where acrcode like '{0}' ", strAid + "%" + "." + strPid + strSid);
                #endregion
                string strSQL4 = string.Format("select count(1) from tReportTemplate where acrcode like '{0}'", strAid + "%" + "." + strPid + strSid);
                ls.Add(strSQL1);
                ls.Add(strSQL2);
                //ls.Add(strSQL3);
                iCount = Convert.ToInt32(oKodakDAL.ExecuteScalar(strSQL4).ToString());
                //exists in tReportTemplate,so we should nofity the user
                if (iCount > 0)
                {
                    strResultMessage = "This ACR Code is Using in Report Template!";
                    return true;
                }
                iCount = 0;
                //others(tReport,tTeaching,tReportList)
                foreach (string str in ls)
                {
                    iCount += Convert.ToInt32(oKodakDAL.ExecuteScalar(str).ToString());
                }
                if (iCount > 0)
                {
                    strResultMessage = "This ACR Code is in use!";
                    return true;
                }
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
            return false;
        }


        public virtual bool DeleteAnatomy(string strAid, string strSid)
        {

            int mark = 0;
            strAid.Trim();
            strSid.Trim();
            RisDAL oKodakDAL = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                //DEFECT NO.EK_HI00060978
                string strQuery = string.Format(@"delete from tACRCodeSubAnatomical where aid='{0}'and sid = '{1}'", strAid, strSid);
                mark = oKodakDAL.ExecuteNonQuery(strQuery, RisDAL.ConnectionState.KeepOpen);
            }
            catch (Exception Ex)
            {
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


        public virtual bool DeletePathology(string strAid, string strPid, string strSid)
        {
            int mark = 0;
            strAid.Trim();
            strSid.Trim();
            RisDAL oKodakDAL = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                //DEFECT NO.EK_HI00060978
                string strQuery = string.Format(@"delete from tACRCodeSubPathological where aid='{0}'and pid = '{1}'and sid='{2}'", strAid, strPid, strSid);
                mark = oKodakDAL.ExecuteNonQuery(strQuery);

            }
            catch (Exception Ex)
            {
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
        public virtual string GetACRCodeDesc(string strAMainCode, string strASubCode, string strPMainCode, string strPSubCode)
        {
            strAMainCode.Trim();
            strASubCode.Trim();
            strPMainCode.Trim();
            strPSubCode.Trim();
            string strAMainDesc = "", strASubDesc = "", strPMainDesc = "", strPSubDesc = "", strResult = null;
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string strQuery1 = string.Format(@"select Description from tACRCodeAnatomical where AID = '{0}'", strAMainCode);
                string strQuery2 = string.Format(@"select Description from tACRCodeSubAnatomical where AID = '{0}' and SID = '{1}'", strAMainCode, strASubCode);
                string strQuery3 = string.Format(@"select Description from tACRCodePathological where AID = '{0}'and PID = '{1}'", strAMainCode, strPMainCode);
                string strQuery4 = string.Format(@"select Description from tACRCodeSubPathological where AID = '{0}'and PID = '{1}'and SID='{2}'", strAMainCode, strPMainCode, strPSubCode);
                if (strAMainCode != null && strAMainCode != "")
                    strAMainDesc = Convert.ToString(oKodakDAL.ExecuteScalar(strQuery1, RisDAL.ConnectionState.KeepOpen));
                if (strASubCode != null && strASubCode != "")
                    strASubDesc = Convert.ToString(oKodakDAL.ExecuteScalar(strQuery2, RisDAL.ConnectionState.KeepOpen));
                if (strPMainCode != null && strPMainCode != "")
                    strPMainDesc = Convert.ToString(oKodakDAL.ExecuteScalar(strQuery3, RisDAL.ConnectionState.KeepOpen));
                if (strPSubCode != null && strPSubCode != "")
                    strPSubDesc = Convert.ToString(oKodakDAL.ExecuteScalar(strQuery4));
                strResult = string.Format("strAMainDesc={0}&strASubDesc={1}&strPMainDesc={2}&strPSubDesc={3}", strAMainDesc, strASubDesc, strPMainDesc, strPSubDesc);
                return strResult;
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
        }
        public virtual DataTable SearchACRCode(string strADesc, string strPDesc, string strACode, string strASubCode, string strPCode, string strPSubCode)
        {
            strADesc.Trim(); strPDesc.Trim(); strACode.Trim(); strASubCode.Trim(); strPCode.Trim(); strPSubCode.Trim();
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string szstoredProc = @"SP_SearchACRCode";
                oKodakDAL.Parameters.AddVarChar("@AnatomicalDesc", strADesc, 255);
                oKodakDAL.Parameters.AddVarChar("@PathologicalDesc", strPDesc, 255);
                oKodakDAL.Parameters.AddVarChar("@aid", strACode, 1);
                oKodakDAL.Parameters.AddVarChar("@asid", strASubCode, 10);
                oKodakDAL.Parameters.AddVarChar("@pid", strPCode, 1);
                oKodakDAL.Parameters.AddVarChar("@psid", strPSubCode, 10);
                DataTable myTable = oKodakDAL.ExecuteQuerySP(szstoredProc);
                return myTable;
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

        }
        #endregion

        #region ISystemProfileDAO's WarningTime interface
        #region GetAllWarningTimeSet()
        /// <summary>
        /// GetAllWarningTimeSet
        /// </summary>
        /// <param name=NULL></param>
        /// <returns>DataSet which contaions one datatable having all Warning time records</returns>
        public virtual DataSet GetAllWarningTimeSet()
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            /*
            sb.Append("SELECT B.Type as TypeEN, B.PatientType AS PatientTypeEn,");
            sb.Append("(SELECT Text FROM tDictionaryValue AS tDictionaryValue_1");
            sb.Append(" WHERE (Value = B.Type) and (Tag='31')) AS Type, B.ModalityType,");
            //sb.Append("(SELECT Text FROM tDictionaryValue");
            //sb.Append(" WHERE (Value = B.PatientType) and (Tag='5')) AS PatientType, B.WarningTime");
            sb.Append(" B.PatientType, B.WarningTime, B.Site, C.Alias");
            sb.Append(" FROM tDictionaryValue AS A INNER JOIN  tWarningTime AS B ON A.Value = B.Type inner join tSiteList C ON B.Site = C.Site order by C.Site, B.ModalityType");
            */
            sb.Append(@"select [type]as TypeEN,PatientType AS PatientTypeEn,(SELECT  distinct Text FROM tDictionaryValue WHERE Value = [Type] and (Tag='31')) AS Type,ModalityType,PatientType,WarningTime,Site,dbo.translateSite(site) as Alias from tWarningTime order by Site,ModalityType");
            try
            {
                dt = oKodakDAL.ExecuteQuery(sb.ToString());
                ds.Tables.Add(dt);
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
            }
            return ds;
        }
        #endregion
        #region GetWarningTimeSelectConditionSet()
        /// <summary>
        /// GetWarningTimeSelectConditionSet
        /// </summary>
        /// <param name=NULL></param>
        /// <returns>DataSet which contaions 3 datatable(1.Warning time Type in tDictionaryValue,2.Modality Type in tModalityType,3.Patient Type in tDictionaryValue)</returns>
        public virtual DataSet GetWarningTimeSelectConditionSet()
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<string> ls = new List<string>();
            ls.Add("SELECT Value as TypeEN,Text as Type FROM tDictionaryValue where Tag='31'  ORDER BY OrderID");
            ls.Add("SELECT ModalityType,ModalityType as ModalityType1 FROM tModalityType");
            ls.Add("SELECT Value as PatientEN,Text as Patient FROM tDictionaryValue where Tag='5' and ((Tag not in (select distinct Tag from tDictionaryValue where Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') and (Site='' or Site is null)) or Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') ORDER BY OrderID");

            try
            {
                foreach (string str in ls)
                {
                    dt = oKodakDAL.ExecuteQuery(str, RisDAL.ConnectionState.KeepOpen);
                    ds.Tables.Add(dt);
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
            }
            return ds;
        }
        #endregion
        #region bool AddNewWarningTime(SystemModel model)
        /// <summary>
        /// bool AddNewWarningTime(SystemModel model)
        /// </summary>
        /// <param name="model">one record of warning time</param>
        /// <returns>1.true if add success,2.false if already exists</returns>
        public virtual bool AddNewWarningTime(SystemModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = model.SaveSystemProfile.Tables[0];
            string strSQL = string.Format("insert into tWarningTime(Type,Modalitytype,PatientType,WarningTime,Domain,Site) values('{0}','{1}','{2}',{3},'{4}','{5}')", dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), CommonGlobalSettings.Utilities.GetCurDomain(), dt.Rows[0][4].ToString());

            try
            {
                oKodakDAL.ExecuteNonQuery(strSQL);
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
        #region bool DeleteWarningTime(SystemModel model)
        /// <summary>
        /// bool DeleteWarningTime(SystemModel model)
        /// </summary>
        /// <param name="model">one record of warning time</param>
        /// <returns>1.true if add success,2.false if not exists this record</returns>
        public virtual bool DeleteWarningTime(SystemModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = model.SaveSystemProfile.Tables[0];
            string strSQL = string.Format("delete from tWarningTime where Type ='{0}'and ModalityType='{1}'and PatientType='{2}' and Site = '{3}'", dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][4].ToString());
            try
            {
                oKodakDAL.ExecuteNonQuery(strSQL);
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
        #region bool UpdateWarningTime(SystemModel model)
        /// <summary>
        /// bool UpdateWarningTime(SystemModel model)
        /// </summary>
        /// <param name="model">one record of warning time</param>
        /// <returns>1.true if add success,2.false if not exists this record</returns>
        public virtual bool UpdateWarningTime(SystemModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = model.SaveSystemProfile.Tables[0];
            string strSQL = string.Format("update tWarningTime set WarningTime ={0} where Type='{1}' and ModalityType='{2}'and PatientType='{3}' and Site = '{4}'", Convert.ToInt32(dt.Rows[0][3].ToString()), dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][4].ToString());
            try
            {
                oKodakDAL.ExecuteNonQuery(strSQL);
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
        #region bool IsExistWarningTime(SystemModel model)
        /// <summary>
        /// bool UpdateWarningTime(SystemModel model)
        /// </summary>
        /// <param name="model">one record of warning time</param>
        /// <returns>1.true if add success,2.false if not exists this record</returns>
        public virtual bool IsExistWarningTime(SystemModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = model.SaveSystemProfile.Tables[0];
            string strSQL = string.Format("select count(1) as count from tWarningTime where  (Type = '{0}') AND (ModalityType = '{1}') AND (PatientType = '{2}') and Site = '{3}'", dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][4].ToString());
            try
            {
                int iCount = Convert.ToInt32(oKodakDAL.ExecuteScalar(strSQL).ToString());
                if (iCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
            }
        }
        #endregion
        #endregion
        #region ISystemProfileDAO's GridColumnOption interface
        /// <summary>
        /// DataSet GetAllGridColumnOptionListNames()
        /// </summary>
        /// <param></param>
        /// <returns>GetAllGridColumnOptionListNames</returns>
        ///
        #region
        public DataSet GetAllGridColumnOptionListNames()
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string strSQL = " select Title, ModuleID, ListName, (select top 1 1 from tBaseList where ID=ListName) AS IsBaseList from "
                + "( SELECT DISTINCT m.Title,m.ModuleID, g.ListName from tModule m,tGridColumnOption g where m.ModuleID = g.ModuleID"
                + " Union select distinct m.Title,m.ModuleID, b.ID from tBaseList b inner join tModule m on m.ModuleID =b.ModuleID"
                + ") tmpGridList";

            try
            {
                dt = oKodakDAL.ExecuteQuery(strSQL);
                ds.Tables.Add(dt);
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
            }
            return ds;
        }
        #endregion
        /// <summary>
        /// DataSet GetAllGridColumnOptionTableRows()
        /// </summary>
        /// <param></param>
        /// <returns>AllGridColumnOptionTableRows</returns>
        ///
        public DataSet GetAllGridColumnOptionRows(string strListName)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //string strSQL = string.Format("SELECT * FROM tGridColumnOption where ListName='{0}'",strListName);
            StringBuilder sb = new StringBuilder();
            //sb.Append("if exists(select 1 from tGridColumnOption o, tGridColumn g where o.guid=g.guid and g.userGuid = '' and o.listName=" + "'" + strListName + "')");
            //sb.Append(" select g.Guid,g.OrderID,o.ColumnName,g.isHidden,o.ModuleID from tGridColumnOption o, tGridColumn g");
            //sb.Append(" where o.guid=g.guid and g.userGuid = '' and o.listName='" + strListName + "'order by g.orderID else select Guid, OrderID, ColumnName, isHidden ,ModuleID from tGridColumnOption");
            //sb.Append(" where listName='" + strListName + "'order by orderID");

            sb.Append("SELECT     Guid, OrderID, ColumnName,IsHidden, ModuleID,TableName, 0 as Required");
            sb.Append(" FROM         tGridColumnOption");
            sb.Append(" WHERE     (ListName = '" + strListName + "') AND (Guid NOT IN");
            sb.Append(" (SELECT     g.Guid");
            sb.Append(" FROM          tGridColumnOption AS o INNER JOIN");
            sb.Append(" tGridColumn AS g ON o.Guid = g.Guid AND g.UserGuid = '' AND o.ListName = '" + strListName + "'))");
            sb.Append(" UNION ALL");
            sb.Append(" SELECT     g.Guid, g.OrderID, o.ColumnName, g.IsHidden, o.ModuleID,o.TableName,0 as Required");
            sb.Append(" FROM         tGridColumnOption AS o INNER JOIN");
            sb.Append(" tGridColumn AS g ON o.Guid = g.Guid AND g.UserGuid = '' AND o.ListName = '" + strListName + "'");
            sb.Append(" ORDER BY OrderID");

            string getBaselist = string.Format("select * from tbaselist where id ='{0}'", strListName);

            try
            {
                dt = oKodakDAL.ExecuteQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                #region US27609 Report list doctor name and doctor id show incorrect  Modified By WADE Zhou
                //var dtBaseList = oKodakDAL.ExecuteQuery(getBaselist, KodakDAL.ConnectionState.KeepOpen);
                //if (dtBaseList.Rows.Count > 0)
                //{
                //    string viewName = System.Convert.ToString(dtBaseList.Rows[0]["ViewName"]);

                //    string getBaesListViewCols = "";
                //    if (viewName.ToUpper().StartsWith("USP_"))
                //    {
                //        getBaesListViewCols = string.Format("execute {0} 1,30,'','1=2',''", viewName);
                //    }
                //    else
                //    {
                //        getBaesListViewCols = string.Format("select * from {0} where 1<>1", viewName);
                //    }
                //    var dtViewCols = oKodakDAL.ExecuteQuery(getBaesListViewCols, KodakDAL.ConnectionState.KeepOpen);

                //    int orderId = 1000 + new Random().Next(100);
                //    if (dt.Rows.Count > 0)
                //        Int32.TryParse(Convert.ToString(dt.Rows[dt.Rows.Count - 1]["OrderId"]), out orderId);

                //    string[] requiredFilds = Convert.ToString(dtBaseList.Rows[0]["RequiredFields"]).Split(',');
                //    List<string[]> addCols = new List<string[]>();

                //    foreach (DataColumn dc in dtViewCols.Columns)
                //    {
                //        var index = dc.ColumnName.IndexOf("__");
                //        string[] colInfo = { "", "" };

                //        if (index < 1 || index == dc.ColumnName.Length - 1)
                //        {
                //            logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1,
                //                    string.Format("illeagal Column {0}", dc.ColumnName),
                //                    Application.StartupPath.ToString(),
                //                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                //                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());

                //            colInfo = new string[] { "", dc.ColumnName };
                //        }
                //        else
                //        {
                //            colInfo = new string[] { dc.ColumnName.Substring(0, index), dc.ColumnName.Substring(index + 2, dc.ColumnName.Length - index - 2) };
                //        }

                //        bool required = false;
                //        for (int i = 0; i < requiredFilds.Length; i++)
                //        {
                //            if (string.Compare(requiredFilds[i].Trim(), dc.ColumnName, true) == 0)
                //            {
                //                required = true;
                //                break;
                //            }
                //        }

                //        var rows = dt.Select(string.Format("TableName ='{0}' and ColumnName='{1}'", colInfo[0], colInfo[1]));
                //        if (rows.Length == 0)
                //        {
                //            if (required || colInfo[1].ToLower().Contains("guid"))
                //            {
                //                dt.Rows.Add("", orderId++, colInfo[1], 1,
                //                    dtBaseList.Rows[0]["ModuleID"],
                //                    colInfo[0], 1);
                //            }
                //            else
                //            {

                //                dt.Rows.Add("", orderId++, colInfo[1], 2,
                //                        dtBaseList.Rows[0]["ModuleID"],
                //                        colInfo[0], 0);
                //            }
                //        }
                //        else
                //        {
                //            if (required)
                //                rows[0]["Required"] = 1;
                //        }

                //    }
                //}
                #endregion
                ds.Tables.Add(dt);
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
            }
            return ds;
        }
        /// <summary>
        /// bool UpdateGridColumnOptionTable()
        /// </summary>
        /// <param></param>
        /// <returns>1.ture update success 2. false update fail</returns>
        ///
        public bool UpdateGridColumnOptionTable(SystemModel model, string strListName)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int i = 0;
            dt = model.SaveSystemProfile.Tables[0];
            try
            {
                //string strDelete = string.Format("delete from tGridColumn where userGuid='' and guid in (select guid from tGridColumnOption where listName='{0}')", strListName);

                string getMaxColID = string.Format("select Max(ColumnID) from tGridColumnOption where listName = '{0}'", strListName);

                var objId = oKodakDAL.ExecuteScalar(getMaxColID);
                int colId = 1000 + (new Random()).Next(100);
                Int32.TryParse(Convert.ToString(objId), out colId);

                oKodakDAL.BeginTransaction();
                //oKodakDAL.ExecuteNonQuery(strDelete, KodakDAL.ConnectionState.KeepOpen);
                int iRowIndex = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    int isHidden = Convert.ToInt32(dr["IsHidden"].ToString());
                    if (Convert.ToString(dr["Guid"]) != string.Empty)
                    {
                        if (isHidden == 2)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("delete from tGridColumnOption where Guid= '{0}'", Convert.ToString(dr["Guid"]))
                                .Append(" delete from tGridColumn where guid not in (select distinct guid from tGridColumnOption)");

                            oKodakDAL.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                        }
                        else
                        {
                            string strQuery = string.Format("select guid,{0},'',{1},domain from tGridColumnOption where listName='{2}' and Guid='{3}'",
                                Convert.ToInt32(iRowIndex.ToString()), Convert.ToInt32(dr["IsHidden"].ToString()), strListName, dr["Guid"].ToString());
                            DataTable table = oKodakDAL.ExecuteQuery(strQuery, RisDAL.ConnectionState.KeepOpen);

                            if (table != null && table.Rows.Count > 0)
                            {
                                DataRow row = table.Rows[0];
                                //string strInsert = string.Format("insert into tGridColumn(Guid, OrderID, UserGuid, isHidden, Domain) select guid,{0},'',{1},domain from tGridColumnOption where listName='{2}' and Guid='{3}'", Convert.ToInt32(iRowIndex.ToString()), Convert.ToInt32(dr["IsHidden"].ToString()), strListName, dr["Guid"].ToString());
                                string strUpdate = string.Format("update tGridColumn set OrderID={0}, UserGuid='', isHidden={1}, Domain='{2}' where Guid='{3}'",
                                    Convert.ToInt32(iRowIndex.ToString()), Convert.ToInt32(dr["IsHidden"].ToString()), row["domain"].ToString(), row["guid"].ToString());
                                oKodakDAL.ExecuteNonQuery(strUpdate, RisDAL.ConnectionState.KeepOpen);
                                iRowIndex++;
                            }
                        }
                    }
                    else
                    {
                        if (isHidden == 2)
                            continue;
                        //Insert
                        string insertSql = "Insert into tGridColumnOption(" +
                                "Guid,ListName, ColumnID, TableName, ColumnName, OrderID, IsHidden, ModuleID, Domain)" +
                         "Values(@Guid,@ListName,@ColumnID,@TableName,@ColumnName,@OrderID,@IsHidden,@ModuleID,@Domain)";

                        oKodakDAL.Parameters.Clear();
                        oKodakDAL.Parameters.AddVarChar("@Guid", Guid.NewGuid().ToString());
                        oKodakDAL.Parameters.AddVarChar("@ListName", strListName);
                        oKodakDAL.Parameters.AddInt("@ColumnID", ++colId);
                        oKodakDAL.Parameters.AddVarChar("@TableName", dr["TableName"]);
                        oKodakDAL.Parameters.AddVarChar("@ColumnName", dr["ColumnName"]);
                        oKodakDAL.Parameters.AddVarChar("@OrderID", iRowIndex++);
                        oKodakDAL.Parameters.AddInt("@IsHidden", dr["IsHidden"]);
                        oKodakDAL.Parameters.AddVarChar("@ModuleID", dr["ModuleID"]);
                        oKodakDAL.Parameters.AddVarChar("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());

                        oKodakDAL.ExecuteNonQuery(insertSql, RisDAL.ConnectionState.KeepOpen);

                        StringBuilder sb = new StringBuilder();
                        sb.Append("insert into tGridColumn(guid, OrderID, userGuid, sorting, isHidden, domain)")
                            .Append(" select guid, OrderID, '', sorting, isHidden, domain")
                            .Append(" from tGridColumnOption where guid not in (select distinct guid from tGridColumn)");

                        oKodakDAL.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);

                    }
                }
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
        /// <summary>
        /// bool IsExistGridColumnOption(SystemModel model)
        /// </summary>
        /// <param></param>
        /// <returns>1.exist this record 2. not exist this record</returns>
        ///
        public bool IsExistGridColumnOption(SystemModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = model.SaveSystemProfile.Tables[0];
            string strSQL = string.Format("select count(1) from tGridColumnOption where Guid='{0}'", dt.Rows[0][0].ToString());
            try
            {
                int iCount = Convert.ToInt32(oKodakDAL.ExecuteScalar(strSQL).ToString());
                if (iCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
            }
        }

        #region EK_HI00062351 jameswei 2007-11-22
        public DataSet getSystemDateNow()
        {
            RisDAL oKodakDAL = new RisDAL();
            string strSQL = @"select getdate()";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                dt = oKodakDAL.ExecuteQuery(strSQL);
                ds.Tables.Add(dt);
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
            }
            return ds;

        }
        #endregion


        #endregion
        #region IBulletinBoard Implementation
        #region bool AddNewBulletin(SystemModel model)
        /// <summary>
        /// bool AddNewBulletin(SystemModel model)
        /// </summary>
        /// <param name="model">one bulletin row</param>
        /// <returns>true if succ otherwise fase</returns>
        public virtual bool AddNewBulletin(BulletinBoardModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int iCount = 0;//inserted rows count
            List<string> sqlLIST = new List<string>();

            string strSQL1 = string.Format("insert into tBillBoard(Guid,Title,groupId,Type,BeginDate,EndDate,Intervals,ShowTime,Body,AttachmentURL,Creator,CreateDate,Domain,groupType)"
            + " values('{0}','{1}','{2}',{3},'{4}','{5}',{6},{7},@RtfBody,'{8}','{9}','{10}','{11}',{12})",
            model.Guid,
            model.Title,
            model.GroupId,
            model.Type,
            model.BeginDate,
            model.EndDate,
            model.Interval,
            model.ShowTime,
            model.AttachmentURL,
            model.Creator,
            DateTime.Now,
            CommonGlobalSettings.Utilities.GetCurDomain(),
            model.GroupType
            );
            sqlLIST.Add(strSQL1);

            string strSQL2 = string.Format("insert into tBillBoardOperation(Guid,Submitter,SubmitDate,SubmitTo,Approver,ApproveDate,Rejector,RejectDate,RejectCause,State,OperationHistory,Domain,Site)"
            + " values('{0}',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,{1},'{2}','{3}','{4}')",
            model.Guid,
            model.State,
            buildOperationHistory(model),
            CommonGlobalSettings.Utilities.GetCurDomain(),
            model.Site);
            sqlLIST.Add(strSQL2);
            try
            {
                oKodakDAL.BeginTransaction();
                oKodakDAL.Parameters.Add("@RtfBody", Encoding.Default.GetBytes(model.Body));
                foreach (string str in sqlLIST)
                {
                    iCount += oKodakDAL.ExecuteNonQuery(str, RisDAL.ConnectionState.KeepOpen);
                }
                oKodakDAL.CommitTransaction();
                return (iCount == 2 ? true : false);

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

        private string buildOperationHistory(BulletinBoardModel bbm)
        {
            string seperator = "\t";
            string newline = "\n\r";
            RisDAL oKodakDAL = new RisDAL();
            string sqlGetOperationHistory = string.Format("select OperationHistory from tBillBoardOperation where guid = '{0}'", bbm.Guid);
            try
            {
                DataTable dt = oKodakDAL.ExecuteQuery(sqlGetOperationHistory);
                switch (bbm.ActionType)
                {
                    case (int)BulletinActionType.Create:
                        {
                            if (dt.Rows.Count > 0)
                            {
                                string history = dt.Rows[0]["OperationHistory"].ToString();
                                string sql = string.Format("select LocalName from tuser where UserGuid ='{0}'", bbm.Creator);
                                DataTable dtUser = oKodakDAL.ExecuteQuery(sqlGetOperationHistory);
                                if (history.Length > 0)
                                {
                                    return history += dtUser.Rows[0][0].ToString() + seperator + DateTime.Now.ToString() + seperator + BulletinActionType.Create + newline;
                                }
                            }
                            else
                            {
                                return bbm.Creator + seperator + DateTime.Now.ToString() + seperator + BulletinActionType.Create + newline;
                            }
                        }
                        break;
                    case (int)BulletinActionType.Submit:
                        {
                            string sqlIsNew = string.Format("select count(*) from tBillBoard where guid = {0}", bbm.Guid);
                            object obj = oKodakDAL.ExecuteScalar(sqlIsNew);
                            if (obj == null || Convert.ToInt32(obj) == 0)//first submit
                            {
                                if (dt.Rows[0].ToString() == "")//&first create
                                {
                                    string sql = string.Format("select LocalName from tuser where UserGuid ='{0}'", bbm.Submitter);
                                    DataTable dtUser = oKodakDAL.ExecuteQuery(sqlGetOperationHistory);
                                    string strTemp = "";
                                    strTemp += dtUser.Rows[0][0].ToString() + seperator + DateTime.Now.ToString() + seperator + BulletinActionType.Create + newline;
                                    strTemp += dtUser.Rows[0][0].ToString() + DateTime.Now.ToString() + seperator + BulletinActionType.Submit + newline;
                                    return strTemp;
                                }

                            }
                            else//for update submit
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    string history = dt.Rows[0]["OperationHistory"].ToString();
                                    if (history.Length > 0)
                                    {
                                        string sql = string.Format("select LocalName from tuser where UserGuid ='{0}'", bbm.Submitter);
                                        DataTable dtUser = oKodakDAL.ExecuteQuery(sqlGetOperationHistory);
                                        history += dtUser.Rows[0][0].ToString() + seperator + DateTime.Now.ToString() + seperator + BulletinActionType.Submit + newline;
                                        return history;
                                    }
                                }
                            }
                        }
                        break;
                    case (int)BulletinActionType.Approve:
                        {
                            if (dt.Rows.Count > 0)
                            {
                                string history = dt.Rows[0]["OperationHistory"].ToString();
                                if (history.Length > 0)
                                {
                                    string sql = string.Format("select LocalName from tuser where UserGuid ='{0}'", bbm.Approver);
                                    DataTable dtUser = oKodakDAL.ExecuteQuery(sqlGetOperationHistory);
                                    history += dtUser.Rows[0][0].ToString() + seperator + DateTime.Now.ToString() + seperator + BulletinActionType.Approve + newline;
                                    return history;
                                }
                            }
                        }
                        break;
                    case (int)BulletinActionType.Reject:
                        {
                            if (dt.Rows.Count > 0)
                            {
                                string history = dt.Rows[0]["OperationHistory"].ToString();
                                if (history.Length > 0)
                                {
                                    string sql = string.Format("select LocalName from tuser where UserGuid ='{0}'", bbm.Approver);
                                    DataTable dtUser = oKodakDAL.ExecuteQuery(sqlGetOperationHistory);
                                    history += dtUser.Rows[0][0].ToString() + seperator + DateTime.Now.ToString() + seperator + BulletinActionType.Reject + newline;
                                    return history;
                                }
                            }
                        }
                        break;
                    default:
                        break;
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
            }

            return "";
        }
        #endregion
        #region bool DeleteBulletin(SystemModel model)
        /// <summary>
        /// bool DeleteBulletin(SystemModel model)
        /// </summary>
        /// <param name="model">delete one record of bulletin</param>
        /// <returns>1.true if delete success,2.false if not exists this record</returns>
        public virtual bool DeleteBulletin(BulletinBoardModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int iCount = 0;//deleted record count

            string strSQL0 = string.Format("select count(1) from tBillBoardOperation where guid ='{0}' and state = 3", model.Guid);
            string strSQL1 = string.Format("delete from tBillBoardOperation where guid ='{0}'", model.Guid);
            string strSQL2 = string.Format("delete from tBillBoard where guid ='{0}'", model.Guid);
            try
            {
                iCount = Convert.ToInt32(oKodakDAL.ExecuteScalar(strSQL0, RisDAL.ConnectionState.KeepOpen));
                if (iCount == 1)
                {
                    throw new Exception("The note is already published by someone!");
                }
                iCount = 0;
                oKodakDAL.BeginTransaction();
                iCount = oKodakDAL.ExecuteNonQuery(strSQL1, RisDAL.ConnectionState.KeepOpen);
                iCount += oKodakDAL.ExecuteNonQuery(strSQL2, RisDAL.ConnectionState.KeepOpen);
                oKodakDAL.CommitTransaction();
                return (iCount == 2 ? true : false);
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
        #region bool UpdateBulletin(SystemModel model)

        /// <summary>
        /// bool UpdateBulletin(SystemModel model)
        /// </summary>
        /// <param name="model">update one record of warning time</param>
        /// <returns>1.true if add success,2.false not successful</returns>
        public virtual bool UpdateBulletin(BulletinBoardModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            StringBuilder sb = new StringBuilder();
            BulletinState bs = (BulletinState)model.State;
            bool publishFlag = false;
            switch (bs)
            {
                case BulletinState.Created:
                    {
                        oKodakDAL.Parameters.Add("@RtfBody", Encoding.Default.GetBytes(model.Body));
                        #region//EK_HI00073209 delete ,not update creator field
                        //sb.Append("update tBillBoard set creator = '" + model.Creator + "'");
                        #endregion
                        sb.Append("update tBillBoard set ");
                        sb.Append("Title = '" + model.Title + "'");
                        sb.Append(",BeginDate = '" + model.BeginDate + "'");
                        sb.Append(",EndDate = '" + model.EndDate + "'");
                        sb.Append(",Type = " + model.Type);
                        sb.Append(",Intervals = " + model.Interval);
                        sb.Append(",ShowTime = " + model.ShowTime);
                        sb.Append(",groupId = '" + model.GroupId + "'");
                        sb.Append(",groupType = " + model.GroupType);
                        sb.Append(",Body = @RtfBody");
                        sb.Append(" where Guid='" + model.Guid + "'\r\n");
                        sb.Append("update tBillBoardOperation set ");
                        sb.Append("State = " + (int)BulletinState.Created);
                        sb.Append(",OperationHistory='" + buildOperationHistory(model) + "'");
                        sb.Append(",Site='" + model.Site + "'");
                        sb.Append(" where Guid='" + model.Guid + "'");
                    }
                    break;
                case BulletinState.Submitted:
                    {
                        sb.Append("update tBillBoardOperation set submitter = '" + model.Submitter + "'");
                        sb.Append(",SubmitDate = '" + DateTime.Now + "'");
                        sb.Append(",SubmitTo = '" + model.SubmitTo + "'");
                        sb.Append(",State = " + (int)BulletinState.Submitted);
                        sb.Append(",OperationHistory='" + buildOperationHistory(model) + "'");
                        sb.Append(",Site='" + model.Site + "'");
                        sb.Append(" where Guid='" + model.Guid + "'");
                    }
                    break;
                case BulletinState.Approved:
                    {
                        oKodakDAL.Parameters.Add("@RtfBody", Encoding.Default.GetBytes(model.Body));
                        sb.Append("update tBillBoard set");
                        sb.Append(" Title = '" + model.Title + "'");
                        sb.Append(",BeginDate = '" + model.BeginDate + "'");
                        sb.Append(",EndDate = '" + model.EndDate + "'");
                        sb.Append(",Type = " + model.Type);
                        sb.Append(",Intervals = " + model.Interval);
                        sb.Append(",ShowTime = " + model.ShowTime);
                        sb.Append(",groupId = '" + model.GroupId + "'");
                        sb.Append(",groupType = " + model.GroupType);
                        sb.Append(",Body = @RtfBody");
                        sb.Append(" where Guid='" + model.Guid + "'\r\n");

                        sb.Append("update tBillBoardOperation set approver = '" + model.Approver + "'");
                        sb.Append(",ApproveDate = '" + DateTime.Now + "'");
                        sb.Append(",State = " + (int)BulletinState.Approved);
                        sb.Append(",OperationHistory='" + buildOperationHistory(model) + "'");
                        sb.Append(",Site='" + model.Site + "'");
                        sb.Append(" where Guid='" + model.Guid + "'");
                        publishFlag = true;//now want to publish it;
                    }
                    break;
                case BulletinState.Published:
                    {
                        oKodakDAL.Parameters.Add("@RtfBody", Encoding.Default.GetBytes(model.Body));
                        sb.Append("update tBillBoard set");
                        sb.Append(" Title = '" + model.Title + "'");
                        sb.Append(",BeginDate = '" + model.BeginDate + "'");
                        sb.Append(",EndDate = '" + model.EndDate + "'");
                        sb.Append(",Type = " + model.Type);
                        sb.Append(",Intervals = " + model.Interval);
                        sb.Append(",ShowTime = " + model.ShowTime);
                        sb.Append(",groupId = '" + model.GroupId + "'");
                        sb.Append(",groupType = " + model.GroupType);
                        sb.Append(",Body = @RtfBody");
                        sb.Append(" where Guid='" + model.Guid + "'\r\n");

                        sb.Append("update tBillBoardOperation set approver = '" + model.Approver + "'");
                        sb.Append(",ApproveDate = '" + DateTime.Now + "'");
                        sb.Append(",Publisher = '" + model.Publisher + "'");
                        sb.Append(",PublishDate = '" + DateTime.Now + "'");
                        sb.Append(",State = " + (int)BulletinState.Published);
                        sb.Append(",OperationHistory='" + buildOperationHistory(model) + "'");
                        sb.Append(",Site='" + model.Site + "'");
                        sb.Append(" where Guid='" + model.Guid + "'");
                        publishFlag = true;//now want to publish it;
                    }
                    break;
                case BulletinState.Rejected:
                    {
                        sb.Append("update tBillBoardOperation set submitter = '" + model.Rejector + "'");
                        sb.Append(",RejectDate = '" + DateTime.Now + "'");
                        sb.Append(",State = " + (int)BulletinState.Rejected);
                        sb.Append(",RejectCause = '" + model.RejectCause + "'");
                        sb.Append(",OperationHistory='" + buildOperationHistory(model) + "'");
                        sb.Append(",Site='" + model.Site + "'");
                        sb.Append(" where Guid=" + model.Guid + "'");
                    }
                    break;
            }

            try
            {
                oKodakDAL.BeginTransaction();
                oKodakDAL.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                if (publishFlag)
                {
                    bool bresult = PublishBulletin(model);
                    if (bresult)
                    {
                        oKodakDAL.CommitTransaction();
                    }
                    else
                    {
                        oKodakDAL.RollbackTransaction();//not update to DB
                    }
                }
                else
                {
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

        #region PublishBulletin
        private bool PublishBulletin(BulletinBoardModel bbModel)
        {
            GCRISNote note = new GCRISNote();
            note.BeginDateTime = bbModel.BeginDate.ToString("yyyy-MM-dd HH:mm:ss");
            note.EndDateTime = bbModel.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
            note.ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            note.CreateDate = bbModel.CreateDate.CompareTo(DateTime.MinValue) == 0 ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : bbModel.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            note.Body = bbModel.Body;
            note.Creator = bbModel.Creator;
            note.Guid = bbModel.Guid;
            note.Interval = bbModel.Interval;
            note.GroupId = bbModel.GroupId;
            note.GroupType = bbModel.GroupType;
            note.ShowTime = bbModel.ShowTime;
            note.State = bbModel.State;
            note.Title = bbModel.Title;
            note.Type = bbModel.Type;
            note.Approver = bbModel.Approver;
            note.AttachmentURL = "";
            note.OperationHistory = "";
            note.RejectCause = "";
            note.RejectDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
            note.Rejector = "";
            note.SubmitDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
            note.Submitter = "";
            note.SubmitTo = "";
            note.Site = bbModel.Site;
            string strError = "";
            int sentResult = CommonGlobalSettings.BillBoard.Utility.SendNote(note, ref strError);
            if (sentResult == -1)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, string.Format("Fail to Send Note '{0}'! Error is {1}", note.Title, strError), Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region DataSet GetAllBulletinsExceptBody()
        /// <summary>
        /// DataSet GetAllBulletinsExceptBody(),for list view only
        /// </summary>
        /// <param></param>
        /// <returns>AllBulletinsExceptBody dataset</returns>
        ///
        public virtual DataSet GetAllBulletinsExceptBodyHistory(BulletinBoardModel model)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            string strCondition = buildBulletinQueryCondition(model);
            sb.Append("SELECT     tBillBoard.Guid,Title,groupId,groupType,Type,BeginDate,EndDate,Intervals,ShowTime,AttachmentURL, ");
            sb.Append(" (SELECT localname FROM tuser WHERE userguid = Creator) AS Creator ");
            sb.Append(",CreateDate,");
            sb.Append(" (SELECT localname FROM tuser WHERE userguid = Submitter) AS Submitter ");
            sb.Append(",SubmitDate,SubmitTo,");
            sb.Append(" (SELECT localname FROM tuser WHERE userguid = Approver) AS Approver ");
            sb.Append(",ApproveDate,");
            sb.Append(" (SELECT localname FROM tuser WHERE userguid = Publisher) AS Publisher ");
            sb.Append(",PublishDate,");
            sb.Append(" (SELECT localname FROM tuser WHERE userguid = Rejector) AS Rejector ");
            sb.Append(",RejectDate,RejectCause,State");
            sb.Append(" FROM         tBillBoard,tBillBoardOperation");
            sb.Append(" WHERE     tBillBoard.Guid = tBillBoardOperation.Guid ");
            if (strCondition.Length > 0)
            {
                sb.Append(strCondition);
            }
            sb.Append(string.Format(" and tBillBoardOperation.Site = '{0}'", model.Site));
            sb.Append(" order by CreateDate desc");

            try
            {
                dt = oKodakDAL.ExecuteQuery(sb.ToString());
                DataTable dtBulletinBoard = new DataTable();
                #region build new datatable

                DataRow dr = null;
                dtBulletinBoard.Columns.Add("Guid", typeof(string));
                dtBulletinBoard.Columns.Add("Title", typeof(string));
                dtBulletinBoard.Columns.Add("groupId", typeof(string));
                dtBulletinBoard.Columns.Add("groupType", typeof(int));
                dtBulletinBoard.Columns.Add("Type", typeof(string));
                dtBulletinBoard.Columns.Add("BeginDateTime", typeof(DateTime));
                dtBulletinBoard.Columns.Add("EndDateTime", typeof(DateTime));
                dtBulletinBoard.Columns.Add("Interval", typeof(string));
                dtBulletinBoard.Columns.Add("ShowTime", typeof(string));
                dtBulletinBoard.Columns.Add("AttachmentURL", typeof(string));
                dtBulletinBoard.Columns.Add("Creator", typeof(string));
                dtBulletinBoard.Columns.Add("CreateDate", typeof(DateTime));
                dtBulletinBoard.Columns.Add("Submitter", typeof(string));
                dtBulletinBoard.Columns.Add("SubmitDate", typeof(DateTime));
                dtBulletinBoard.Columns.Add("SubmitTo", typeof(string));
                dtBulletinBoard.Columns.Add("Approver", typeof(string));
                dtBulletinBoard.Columns.Add("ApproveDate", typeof(DateTime));
                dtBulletinBoard.Columns.Add("Publisher", typeof(string));
                dtBulletinBoard.Columns.Add("PublishDate", typeof(DateTime));
                dtBulletinBoard.Columns.Add("Rejector", typeof(string));
                dtBulletinBoard.Columns.Add("RejectDate", typeof(DateTime));
                dtBulletinBoard.Columns.Add("RejectCause", typeof(string));
                dtBulletinBoard.Columns.Add("State", typeof(string));
                #endregion
                #region from int to string
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow drx in dt.Rows)
                    {
                        DataRow drNew = dtBulletinBoard.NewRow();
                        drNew["Guid"] = drx["Guid"];
                        drNew["Title"] = drx["Title"];
                        drNew["groupId"] = drx["groupId"].ToString();
                        drNew["groupType"] = drx["groupType"];
                        drNew["Type"] = drx["Type"].ToString();
                        drNew["BeginDateTime"] = drx["BeginDate"];
                        drNew["EndDateTime"] = drx["EndDate"];
                        drNew["Interval"] = (Convert.ToInt32(drx["Intervals"].ToString()) / 60);//to minute
                        drNew["ShowTime"] = (Convert.ToInt32(drx["ShowTime"]) / 60);//to minute
                        drNew["AttachmentURL"] = drx["AttachmentURL"];
                        drNew["Creator"] = drx["Creator"];
                        drNew["CreateDate"] = drx["CreateDate"];
                        drNew["Submitter"] = drx["Submitter"];
                        drNew["SubmitDate"] = drx["SubmitDate"];
                        drNew["SubmitTo"] = drx["SubmitTo"];
                        drNew["Approver"] = drx["Approver"];
                        drNew["ApproveDate"] = drx["ApproveDate"];
                        drNew["Publisher"] = drx["Publisher"];
                        drNew["PublishDate"] = drx["PublishDate"];
                        drNew["Rejector"] = drx["Rejector"];
                        drNew["RejectDate"] = drx["RejectDate"];
                        drNew["RejectCause"] = drx["RejectCause"];
                        drNew["State"] = drx["State"].ToString();
                        dtBulletinBoard.Rows.Add(drNew);
                    }
                #endregion
                }
                if (dtBulletinBoard != null && dtBulletinBoard.Rows.Count > 0 && !string.IsNullOrEmpty(model.GroupId))
                {
                    string groupID = "";
                    string[] queryGroupIDs;
                    bool exists = false;
                    List<DataRow> lsDelete = new List<DataRow>();
                    foreach (DataRow drItem in dtBulletinBoard.Rows)
                    {
                        if (drItem["groupId"] != null)
                        {
                            groupID = Convert.ToString(drItem["groupId"]);
                            queryGroupIDs = model.GroupId.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            exists = Array.Exists(groupID.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries), m => Array.Exists(queryGroupIDs, q => q.Equals(m)));
                            if (!exists)
                            {
                                lsDelete.Add(drItem);
                            }
                        }
                    }
                    if (lsDelete.Count > 0)
                    {
                        foreach (DataRow drDelete in lsDelete)
                        {
                            drDelete.Delete();
                        }
                    }
                    dtBulletinBoard.AcceptChanges();
                }
                ds.Tables.Add(dtBulletinBoard);
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
            }
            return ds;
        }
        #endregion
        #region build bulletin query condition(include SQL :and ...)
        private string buildBulletinQueryCondition(BulletinBoardModel bbModel)
        {
            string strSeperator = " and ";
            List<string> lsConditions = new List<string>();
            string strTemp = "";
            if (bbModel == null)
            {
                return "";
            }
            if (bbModel.Types.Length > 0)
            {
                strTemp = "";
                strTemp = strSeperator + buildMultiOrCondition("Type", "int", bbModel.Types);
                lsConditions.Add(strTemp);
            }
            if (bbModel.Submitters.Length > 0)
            {
                strTemp = "";
                //strTemp = strSeperator + buildMultiOrCondition("Submitter", "string", bbModel.Submitters);
                strTemp = strSeperator + buildMultiOrCondition("Creator", "string", bbModel.Submitters);
                lsConditions.Add(strTemp);
            }
            if (bbModel.Approvers.Length > 0)
            {
                strTemp = "";
                strTemp = strSeperator + buildMultiOrCondition("Approver", "string", bbModel.Approvers);
                lsConditions.Add(strTemp);
            }
            if (bbModel.SubmitDate != DateTime.MinValue)
            {
                strTemp = "";
                strTemp = strSeperator + "SubmitDate" + " >= '" + bbModel.SubmitDate.ToString("yyyy-MM-dd") + " 00:00:00'" + strSeperator + "SubmitDate" + " <= '" + bbModel.SubmitDate.ToString("yyyy-MM-dd") + " 23:59:59'";
                lsConditions.Add(strTemp);
            }
            if (bbModel.ApproveDate != DateTime.MinValue)
            {
                strTemp = "";
                strTemp = strSeperator + "ApproveDate" + " >= '" + bbModel.SubmitDate.ToString("yyyy-MM-dd") + " 00:00:00'" + strSeperator + "ApproveDate" + " <= '" + bbModel.SubmitDate.ToString("yyyy-MM-dd") + " 23:59:59'";
                lsConditions.Add(strTemp);
            }
            if (bbModel.Title.Length > 0)
            {
                strTemp = "";
                strTemp = strSeperator + "Title" + " like '%" + bbModel.Title + "%'";
                lsConditions.Add(strTemp);
            }
            if (bbModel.States.Length > 0)
            {
                strTemp = "";
                strTemp = strSeperator + buildMultiOrCondition("State", "string", bbModel.States);
                lsConditions.Add(strTemp);
            }
            if (bbModel.Publishers.Length > 0)
            {
                strTemp = "";
                strTemp = strSeperator + buildMultiOrCondition("Publisher", "string", bbModel.Publishers);
                lsConditions.Add(strTemp);
            }
            if ((bbModel.SearchByDate & 1) == 1)//by create date
            {
                strTemp = "";
                strTemp = strSeperator + "CreateDate" + " >= '" + bbModel.CreateDateFrom.ToString("yyyy-MM-dd HH:mm:ss") + "'" + strSeperator + "CreateDate" + " <= '" + bbModel.CreateDateTo.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                lsConditions.Add(strTemp);
            }
            if ((bbModel.SearchByDate & 2) == 2)//by publish date
            {
                strTemp = "";
                strTemp = strSeperator + "PublishDate" + " >= '" + bbModel.PublishDateFrom.ToString("yyyy-MM-dd HH:mm:ss") + "'" + strSeperator + "PublishDate" + " <= '" + bbModel.PublishDateTo.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                lsConditions.Add(strTemp);
            }
            if (lsConditions.Count > 1)
            {
                strTemp = "";
                foreach (string str in lsConditions)
                {
                    strTemp += str;
                }
            }
            return strTemp;
        }
        #endregion

        #region build multi-or condition
        public string buildMultiOrCondition(string name, string type, string source)
        {
            string[] array = source.Split(new char[] { ',' });
            char[] trimString = new char[] { 'O', 'R', ' ' };
            string orReturn = " ";
            if (source.Length == 0)
            {
                return "";
            }
            if (type.Equals("string"))//type of string
            {
                if (array.Length == 1)
                {
                    return orReturn += name + "='" + array[0] + "' ";
                }
                orReturn += "(";
                for (int i = 0; i < array.Length; i++)
                {
                    orReturn += name + "='" + array[i] + "' ";
                    if (i == array.Length - 1)
                    {
                        break;
                    }
                    orReturn += " OR ";
                }
                if (array.Length > 1)
                {
                    orReturn.TrimEnd(trimString);
                    orReturn += ")";
                }
            }
            else if (type.Equals("int"))//type of int
            {
                if (array.Length == 1)
                {
                    return orReturn += name + "=" + array[0] + " ";
                }
                orReturn += "(";
                for (int i = 0; i < array.Length; i++)
                {
                    orReturn += name + "=" + array[i] + " ";
                    if (i == array.Length - 1)
                    {
                        break;
                    }
                    orReturn += " OR ";
                }
                if (array.Length > 1)
                {
                    orReturn.TrimEnd(trimString);
                }
                orReturn += ")";
            }
            return orReturn;

        }

        #endregion

        #region DataSet GetOneBulletinRow(string Guid)
        /// <summary>
        /// DataSet GetOneBulletinRow(string Guid),for detail view
        /// </summary>
        /// <param></param>
        /// <returns>OneBulletinRow dataset</returns>
        ///
        public virtual DataSet GetOneBulletinRow(string Guid)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT     A.Guid,Title,groupId,groupType,Type,BeginDate,EndDate,Intervals,ShowTime,Body,AttachmentURL,Creator,CreateDate,Submitter,SubmitDate,SubmitTo,Approver,ApproveDate,Publisher,PublishDate,Rejector,RejectDate,RejectCause,State,OperationHistory");
            sb.Append(" FROM      tBillBoard A,tBillBoardOperation B");
            sb.Append(" WHERE     A.Guid = B.Guid  and A.Guid = '" + Guid + "' order by CreateDate desc");
            try
            {
                dt = oKodakDAL.ExecuteQuery(sb.ToString());
                ds.Tables.Add(dt);
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
            }
            return ds;
        }
        #endregion
        #region DataSet GetUsers(string Roles)
        /// <summary>
        /// DataSet GetUsers(string Roles), roles="" is all users
        /// </summary>
        /// <param></param>
        /// <returns>OneBulletinRow dataset</returns>
        ///
        public virtual DataSet GetUsers(string Roles)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat("select distinct a.UserGuid, a.LocalName, c.Value as BelongToSite from tUser a inner join tRole2User b on a.UserGuid = b.UserGuid left join tUserProfile c on  a.UserGuid = c.UserGuid and c.Name ='belongtosite' and b.Domain='{0}' ", CommonGlobalSettings.Utilities.GetCurDomain());
            sb.Append(buildConditionForGetUsers(Roles));
            sb.Append(" Order By A.LocalName ASC");


            try
            {
                dt = oKodakDAL.ExecuteQuery(sb.ToString());
                ds.Tables.Add(dt);
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
            }
            return ds;
        }

        private string buildConditionForGetUsers(string Roles)
        {
            string[] roleArray = Roles.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            char[] trimString = new char[] { 'O', 'R', ' ' };
            string roleOrs = " and ";
            if (roleArray.Length == 0)
            {
                return ""; //default all user
            }
            if (roleArray.Length == 1)
            {
                return roleOrs += "B.RoleName = '" + roleArray[0] + "' ";
            }
            for (int i = 0; i < roleArray.Length; i++)
            {
                roleOrs += "B.RoleName = '" + roleArray[i] + "' ";
                if (i == roleArray.Length - 1)
                {
                    break;
                }
                roleOrs += "OR ";
            }
            if (roleArray.Length > 1)
            {
                roleOrs.TrimEnd(trimString);
            }
            return roleOrs;
        }
        #endregion
        #region GetDictionaryValue(string name)
        /// <summary>
        /// GetDictionaryValue(string name)
        /// </summary>
        /// <param></param>
        /// <returns>OneBulletinRow dataset</returns>
        ///
        public DataSet GetDictionaryValue(string tag)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT     B.Value AS ValueMember, B.Text AS DisplayMember, B.IsDefault");
            sb.Append(" FROM      tDictionary AS A INNER JOIN");
            sb.Append(" tDictionaryValue AS B ON A.Tag = B.Tag AND A.Tag = " + tag + " and ((B.Tag not in (select distinct Tag from tDictionaryValue where Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') and (Site='' or Site is null)) or Site='" + CommonGlobalSettings.Utilities.GetCurSite() + "') ORDER BY OrderID");

            try
            {
                dt = oKodakDAL.ExecuteQuery(sb.ToString());
                ds.Tables.Add(dt);
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
            }
            return ds;
        }
        #endregion

        #region LockBulletin(string name)
        /// <summary>
        /// LockBulletin(string name)
        /// </summary>
        /// <param></param>
        /// <returns>1.true locked by me 2. false locked by other one</returns>
        ///
        public bool LockBulletin(string name)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<string> sqls = new List<string>();
            string temp = string.Format("select count(1) from tBillBoardOperation where Guid = '{0}' and Counts =1", name);
            sqls.Add(temp);
            temp = string.Format("update  tBillBoardOperation set Counts =1 where Guid = '{0}'", name);
            sqls.Add(temp);
            try
            {
                oKodakDAL.BeginTransaction();
                object obj = oKodakDAL.ExecuteScalar(sqls[0], RisDAL.ConnectionState.KeepOpen);//wether it is locked now
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    return false;//it is locked by other one
                }
                else
                {
                    oKodakDAL.ExecuteQuery(sqls[1]);//it is locked by me
                    return true;
                }
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
            return false;
        }
        #endregion

        #region UnLockBulletin(string name)
        /// <summary>
        /// UnLockBulletin(string name)
        /// </summary>
        /// <param></param>
        /// <returns>true if db operation successful otherwise false</returns>
        ///
        public bool UnLockBulletin(string name)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Format("update  tBillBoardOperation set Counts =0 where Guid = '{0}'", name);
            try
            {
                oKodakDAL.ExecuteQuery(sql);//it is locked by me
                return true;
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

        #endregion

        #region IQualityScoringDAO
        public virtual string ParseWhere(string strParam)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string strWhere = "";
            char[] sep1 = { '&' };
            char[] sep2 = { '=' };
            char[] sep3 = { ',' };
            string[] arrItems = strParam.Split(sep1);
            strWhere = " and " + arrItems[0];
            return strWhere;
        }

        #region **1.QueryQualityScoringList(string strParam, DataSet ds, ref string strError)
        /// <summary>
        /// QueryQualityScoringList(string strParam, DataSet ds, ref string strError)
        /// </summary>
        /// <param>string strParam, DataSet ds, ref string strError</param>
        /// <returns>true if db operation successful otherwise false</returns>
        ///
        public virtual bool QueryQualityScoringList(string strParam, DataSet ds, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                string strSQL = "select tRegProcedure.ProcedureGuid,tRegOrder.AccNo,tRegPatient.LocalName,tRegProcedure.ExamSystem,tRegProcedure.Modality,tRegProcedure.Status as RPStatus," +
            "(SELECT LocalName from tUser where tUser.UserGuid=tRegProcedure.Technician) as Technician," +
            "tRegPatient.Gender," +
            "tRegOrder.ApplyDept," +
            "tRegOrder.PatientType," +
            "tRegProcedure.ModalityType,tQualityScoring.Result,tProcedureCode.CheckingItem" +
            " FROM tRegPatient,tRegOrder,tRegProcedure,tProcedureCode,tModality,tQualityScoring" +
            " where tRegPatient.PatientGuid=tRegOrder.PatientGuid and tRegOrder.OrderGuid=tRegProcedure.OrderGuid and  tRegProcedure.ProcedureCode=tProcedureCode.ProcedureCode and tRegProcedure.Modality=tModality.Modality";
                strSQL += ParseWhere(strParam);

                logger.Debug((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, strSQL, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "QualityScoringList";
                ds.Tables.Add(dt);


            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }
        #endregion

        #region **2.QueryQualityScoringList(string strParam, int nPageIndex, int nPageSize, ref int nTotalCount, DataSet ds, ref string strError)
        /// <summary>
        /// QueryQualityScoringList(string strParam, int nPageIndex, int nPageSize, ref int nTotalCount, DataSet ds, ref string strError)
        /// </summary>
        /// <param>string strParam, int nPageIndex, int nPageSize, ref int nTotalCount, DataSet ds, ref string strError</param>
        /// <returns>true if db operation successful otherwise false</returns>
        ///
        public virtual bool QueryQualityScoringList(string strParam, int nPageIndex, int nPageSize, ref int nTotalCount, DataSet ds, ref string strError, int randomNum, int queyrObject, string IncludingAppraised)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                string strWhere = ParseWhere(strParam);
                string strQueryObject = "";
                if (randomNum > 0 && IncludingAppraised == "0")
                {
                    switch (queyrObject)
                    {
                        case 1:
                            strQueryObject = " And (tQualityScoring.Result2 is null or (len(tQualityScoring.Result2)=0))";
                            break;
                        case 2:
                            strQueryObject = " And (tReport.ReportQuality is null or  (len(tReport.ReportQuality)=0))";
                            break;
                        case 3:
                            strQueryObject = " And (tQualityScoring.Result2 is null or len(tQualityScoring.Result2)=0 or (tReport.ReportQuality is null or len(tReport.ReportQuality)=0))";
                            break;
                        default:
                            break;
                    }
                }

                /*
                //1.remove tqualityscoring associated conditions
                string[] strWhereArray = strWhere.Split(new string[] { "and" }, StringSplitOptions.RemoveEmptyEntries);
                //string strWhereQualityScoring = " 1=1 ";
                if (strWhereArray.Length >= 1)
                {
                    strWhere = "";
                    foreach (string strItem in strWhereArray)
                    {
                        string str = strItem.Trim();
                        if (str.Trim() == string.Empty)
                        {
                            continue;
                        }

                        if (str.ToUpper().Contains("tQualityScoring.Result in (".ToUpper()))
                        {

                            string strInCondition = str.Substring(str.IndexOf('(') + 1, str.Length - str.IndexOf('(') - 2);
                            string strOrCondition = " (";
                            foreach (string strOneIn in strInCondition.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (strOneIn == "'-1'")
                                {
                                    strOrCondition += " tQualityScoring.Result is null Or";
                                }
                                else
                                {
                                    strOrCondition += " tQualityScoring.Result = " + strOneIn + " Or";
                                }
                            }
                            strOrCondition = strOrCondition.TrimEnd("Or".ToCharArray()) + ")";
                            strWhere += " and " + strOrCondition;
                        }
                        else
                        {
                            if (str.ToUpper().Contains("tQualityScoring.Result = '-1'"))
                            {
                                strWhere += str.Replace("= '-1'", "IS NULL");
                            }
                            else
                            {
                                strWhere += " and " + str;
                            }
                        }
                         
                    }
                }
                */
                if (strQueryObject.Length > 0)
                {
                    strWhere += strQueryObject;
                }


                if (strWhere.Length > 4000)
                {
                    throw new Exception("The query conditions is too long!");
                }
                //}
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddInt("@PageIndex", nPageIndex);
                oKodak.Parameters.AddInt("@PageSize", nPageSize);
                oKodak.Parameters.AddVarChar("@Where", strWhere, 8000);
                //oKodak.Parameters.AddVarChar("@WhereQualityScoring", strWhereQualityScoring, 8000);
                oKodak.Parameters.AddInt("@TotalCount", nTotalCount, ParameterDirection.Output);
                oKodak.Parameters.AddInt("@RandomNum", randomNum);
                oKodak.ExecuteQuerySP("SP_QualityScoringList", dt);
                dt.TableName = "QualityScoringList";
                ds.Tables.Add(dt);
                if (oKodak.Parameters["@TotalCount"].Value != null)
                {
                    nTotalCount = Convert.ToInt32(oKodak.Parameters["@TotalCount"].Value);
                }

            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }
        #endregion

        #region **3.bool SaveAppraise(DataSet ds, ref string errorMsg)
        /// <summary>
        /// SaveAppraise(DataSet ds, ref string errorMsg)
        /// </summary>
        /// <param>DataSet ds,it is data to save</param>
        /// <returns>true if db operation successful otherwise false</returns>
        ///
        public virtual bool SaveAppraise(DataSet ds, ref string errorMsg)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            string strresult = string.Empty;
            int result = 0;
            try
            {

                string appraiseSaveObject = Convert.ToString(ds.Tables[0].Rows[0]["AppraiseSaveObject"]);
                //reportqualityscoring
                if (appraiseSaveObject == "2" || appraiseSaveObject == "3")
                {
                    string reportGuid = Convert.ToString(ds.Tables[0].Rows[0]["ReportGuid"]);
                    string reportQuality = Convert.ToString(ds.Tables[0].Rows[0]["ReportQuality"]);
                    string reportQuality2 = Convert.ToString(ds.Tables[0].Rows[0]["ReportQuality2"]);
                    string reportQualityComments = Convert.ToString(ds.Tables[0].Rows[0]["ReportQualityComments"]);
                    string sql = "Update tReport set ReportQuality = @ReqportQuality,ReportQuality2 = @ReqportQuality2,ReportQualityComments=@ReportQualityComments where ReportGuid ='{0}'";
                    oKodak.Parameters.Add("@ReqportQuality", reportQuality);
                    oKodak.Parameters.Add("@ReqportQuality2", reportQuality2);
                    oKodak.Parameters.Add("@ReportQualityComments", reportQualityComments);

                    result = oKodak.ExecuteNonQuery(string.Format(sql, reportGuid));
                    if (result < 1)
                    {
                        return false;
                    }
                }
                //imagequalityscoring
                if (appraiseSaveObject == "1" || appraiseSaveObject == "3")
                {
                    bool bImageNotRequired = false;
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Columns.Contains("ImageNotRequired"))
                        bImageNotRequired = true;

                    string UpdateSql = "";
                    string InsertSql = "";
                    string sqlIfExistedOthers = string.Format("select * from tQualityScoring where AppraiseObject in ({0}) and Appraiser <> '{1}'", ds.Tables[0].Rows[0]["AppraiseObject"].ToString(), ds.Tables[0].Rows[0]["Appraiser"].ToString());
                    string sqlIfExistedOthersLinkExam = string.Format("select * from tQualityScoring where OrderGuid = '{0}' and Appraiser <> '{1}'", ds.Tables[0].Rows[0]["OrderGuid"].ToString(), ds.Tables[0].Rows[0]["Appraiser"].ToString());
                    //string sqlLinkExamRP = string.Format("Select ProcedureGuid,ExamineDt,Technician from tRegProcedure where OrderGuid ='{0}' and IsExistImage = 1 and ProcedureGuid Not in (Select AppraiseObject from tQualityScoring Where OrderGuid = '{0}' )", ds.Tables[0].Rows[0]["OrderGuid"].ToString());
                    string sqlLinkExamRP = string.Format("Select ProcedureGuid,ExamineDt,Technician from tRegProcedure where OrderGuid ='{0}' " + (bImageNotRequired ? "" : " and IsExistImage = 1 ") + " and ProcedureGuid not in (Select AppraiseObject from tQualityScoring Where OrderGuid = '{0}') ", ds.Tables[0].Rows[0]["OrderGuid"].ToString());
                    string sqlGeneral = string.Format("Select ProcedureGuid,ExamineDt,Technician from tRegProcedure where OrderGuid ='{0}' " + (bImageNotRequired ? "" : " and IsExistImage = 1 ") + " and ProcedureGuid in ('{1}') ", ds.Tables[0].Rows[0]["OrderGuid"].ToString(), System.Convert.ToString(ds.Tables[0].Rows[0]["AppraiseObject"]).Trim("'".ToCharArray()));
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["Action"]) == (int)QualityScoringDBAction.Update)//update
                    {
                        if (ds.Tables[0].Columns.Contains("Result2"))
                        {
                            UpdateSql = string.Format("Update tQualityScoring set Result ='{0}',AppraiseDate ='{1}',Comment = @Comment, Result2 ='{3}', Result3 ='{4}' where AppraiseObject in = '{2}'",
                                               ds.Tables[0].Rows[0]["Result"].ToString(),
                                               DateTime.Now,
                                               ds.Tables[0].Rows[0]["AppraiseObject"].ToString()
                                               , ds.Tables[0].Rows[0]["Result2"].ToString()
                                               , ds.Tables[0].Rows[0]["Result3"].ToString()
                                               );
                        }
                        else
                        {
                            UpdateSql = string.Format("Update tQualityScoring set Result ='{0}',AppraiseDate ='{1}',Comment = @Comment where AppraiseObject in = '{2}'",
                                               ds.Tables[0].Rows[0]["Result"].ToString(),
                                               DateTime.Now,
                                               ds.Tables[0].Rows[0]["AppraiseObject"].ToString()
                                               );
                        }
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0]["Action"]) == (int)QualityScoringDBAction.Save)//save
                    {
                        DataTable dt = oKodak.ExecuteQuery(sqlGeneral);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            InsertSql = string.Format(" Delete from tQualityScoring where AppraiseObject in ('{0}') \r\n ", System.Convert.ToString(ds.Tables[0].Rows[0]["AppraiseObject"]).Trim("'".ToCharArray()));
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (ds.Tables[0].Columns.Contains("Result2"))
                                {
                                    InsertSql += string.Format(" Insert into tQualityScoring(Guid,AppraiseObject,OrderGuid,ExaminateDt,Type,Result,Appraisee,Appraiser,AppraiseDate,Comment,Domain,Result2,Result3) values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',@Comment,'{9}','{10}','{11}')",
                                                        Guid.NewGuid().ToString(),
                                                        dr["ProcedureGuid"].ToString(),
                                                        ds.Tables[0].Rows[0]["OrderGuid"].ToString(),
                                                        dr["ExamineDt"].ToString().ToString(),
                                                        Convert.ToInt32(ds.Tables[0].Rows[0]["Type"].ToString()),
                                                        ds.Tables[0].Rows[0]["Result"].ToString(),
                                                        dr["Technician"].ToString(),
                                                        ds.Tables[0].Rows[0]["Appraiser"].ToString(),
                                                        DateTime.Now,
                                                        CommonGlobalSettings.Utilities.GetCurDomain()
                                                        , ds.Tables[0].Rows[0]["Result2"].ToString()
                                                        , ds.Tables[0].Rows[0]["Result3"].ToString()
                                                        ) + "\r\n";
                                }
                                else
                                {
                                    InsertSql += string.Format(" Insert into tQualityScoring(Guid,AppraiseObject,OrderGuid,ExaminateDt,Type,Result,Appraisee,Appraiser,AppraiseDate,Comment,Domain) values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',@Comment,'{9}')",
                                                        Guid.NewGuid().ToString(),
                                                        dr["ProcedureGuid"].ToString(),
                                                        ds.Tables[0].Rows[0]["OrderGuid"].ToString(),
                                                        dr["ExamineDt"].ToString().ToString(),
                                                        Convert.ToInt32(ds.Tables[0].Rows[0]["Type"].ToString()),
                                                        ds.Tables[0].Rows[0]["Result"].ToString(),
                                                        dr["Technician"].ToString(),
                                                        ds.Tables[0].Rows[0]["Appraiser"].ToString(),
                                                        DateTime.Now,
                                                        CommonGlobalSettings.Utilities.GetCurDomain()
                                                        ) + "\r\n";
                                }
                            }
                        }
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0]["Action"]) == (int)QualityScoringDBAction.SaveLinkExam || Convert.ToInt32(ds.Tables[0].Rows[0]["Action"]) == (int)QualityScoringDBAction.UpdateLinkExam)
                    {
                        //DataTable dt = oKodak.ExecuteQuery(sqlLinkExamRP);
                        DataTable dt = oKodak.ExecuteQuery(sqlLinkExamRP);
                        DataTable dtAppraised = oKodak.ExecuteQuery(sqlLinkExamRP);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //1. firstly delete appraised ones
                            //foreach (DataRow dr in dt.Rows)
                            //{
                            //InsertSql = string.Format("Delete from tQualityScoring where AppraiseObject in({0}) \r\n", getGuidArray(dt));
                            //InsertSql = string.Format("Delete from tQualityScoring where AppraiseObject in({0}) ", ds.Tables[0].Rows[0]["AppraiseObject"].ToString());
                            //}
                            //string[] appraiseObjects = ds.Tables[0].Rows[0]["AppraiseObject"].ToString().Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries);
                            //InsertSql = string.Format("Delete from tQualityScoring where OrderGuid ='{0}'", ds.Tables[0].Rows[0]["OrderGuid"].ToString());

                            foreach (DataRow dr in dt.Rows)
                            {

                                InsertSql += string.Format("Insert into tQualityScoring(Guid,AppraiseObject,OrderGuid,ExaminateDt,Type,Result,Appraisee,Appraiser,AppraiseDate,Comment,Domain) values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',@Comment,'{9}')",
                                                    Guid.NewGuid().ToString(),
                                                    dr["ProcedureGuid"].ToString(),
                                                    ds.Tables[0].Rows[0]["OrderGuid"].ToString(),
                                                    dr["ExamineDt"].ToString(),
                                                    Convert.ToInt32(ds.Tables[0].Rows[0]["Type"].ToString()),
                                                    ds.Tables[0].Rows[0]["Result"].ToString(),
                                                    dr["Technician"].ToString(),
                                                    ds.Tables[0].Rows[0]["Appraiser"].ToString(),
                                                    DateTime.Now,
                                                    CommonGlobalSettings.Utilities.GetCurDomain()
                                                    ) + "\r\n";

                            }
                        }
                        else
                        {
                            errorMsg = "LinkExamWarning";
                            return false;
                        }

                    }
                    /* linkexam only can appraise the not appraised objects
                    //**1.if others appraised it!!! warning to the user cannot save it!!!(link exam)
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["Action"]) == (int)QualityScoringDBAction.SaveLinkExam || Convert.ToInt32(ds.Tables[0].Rows[0]["Action"]) == (int)QualityScoringDBAction.UpdateLinkExam)
                    {
                        strresult = Convert.ToString(oKodak.ExecuteScalar(sqlIfExistedOthersLinkExam, KodakDAL.ConnectionState.KeepOpen));
                        if (strresult != string.Empty)
                        {
                            errorMsg = "OthersAppraised!";
                            return false;
                        }
                    }
                     */
                    //**2.check if others appraise it (not link exam)
                    oKodak.Parameters.Add("@Comment", ds.Tables[0].Rows[0]["Comment"]);
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["Action"]) == (int)QualityScoringDBAction.Update || Convert.ToInt32(ds.Tables[0].Rows[0]["Action"]) == (int)QualityScoringDBAction.Save)//not prompted to user,so prompt it!!!
                    {
                        strresult = string.Empty;
                        /*//current appraised one can be appraised again by other people
                        strresult = Convert.ToString(oKodak.ExecuteScalar(sqlIfExistedOthers, KodakDAL.ConnectionState.KeepOpen));
                        if (strresult != string.Empty)//exists my appraised one
                        {
                            errorMsg = "OthersAppraised!";//others appraised!!!
                            return false;

                        }
                         */
                        result = oKodak.ExecuteNonQuery(InsertSql);//use insert totally
                        if (result >= 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                    else
                    {
                        result = oKodak.ExecuteNonQuery(InsertSql);
                        if (result >= 1)
                        {
                            return true;
                        }
                        else
                        {
                            //errorMsg = "AppraisedRedoIt?";//let user to select
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                bReturn = false;
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        private string getGuidArray(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return "";
            }
            string temp = "";
            foreach (DataRow dr in dt.Rows)
            {
                temp += "'" + dr[0].ToString() + "'" + ",";
            }
            return temp.TrimEnd(new char[] { ',' });
        }
        #endregion

        #region **3.bool GetAppraise(string RPGuid, DataSet ds, ref string errorMsg)
        /// <summary>
        /// GetAppraise(string RPGuid, DataSet ds, ref string errorMsg)
        /// </summary>
        /// <param>DataSet ds,it is data to output</param>
        /// <returns>true if db operation successful otherwise false</returns>
        ///
        public virtual bool GetAppraise(string RPGuid, DataSet ds, ref string errorMsg)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            int result = -1;

            try
            {
                string sql = "Select * from tQualityScoring where AppraiseObject = " + RPGuid;
                dt = oKodak.ExecuteQuery(sql);
                if (dt != null && (dt.Rows.Count > 0))
                {
                    ds.Tables.Add(dt);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }
        #endregion

        public string GetSettings(string strParam, ref string version)
        {
            RisDAL oKodak = new RisDAL();
            try
            {
                string Site = CommonGlobalSettings.Utilities.GetParameter("Site", strParam);
                string Type = CommonGlobalSettings.Utilities.GetParameter("Type", strParam);
                string Version = CommonGlobalSettings.Utilities.GetParameter("Version", strParam);
                string From = CommonGlobalSettings.Utilities.GetParameter("From", strParam);
                string sql = string.Format("select Settings,VersionNo from tScoringSettings where VersionNo='{0}'", Version);
                if (string.IsNullOrWhiteSpace(Version))
                {
                    if (From == "ForUse")
                    {
                        sql = string.Format("select Settings,VersionNo from tScoringSettings where IsCurrent=1 and (Site='{0}' or Site='') and Type='{1}' order by Site desc", Site, Type);
                    }
                    else
                    {
                        sql = string.Format("select Settings,VersionNo from tScoringSettings where IsCurrent=1 and Site='{0}' and Type='{1}'", Site, Type);
                    }
                }
                DataTable dt = oKodak.ExecuteQuery(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    version = dt.Rows[0][1].ToString();
                    return dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return "";
        }

        public bool SaveSettings(string strParam, DataSet ds)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            try
            {
                string Site = CommonGlobalSettings.Utilities.GetParameter("Site", strParam);
                string Type = CommonGlobalSettings.Utilities.GetParameter("Type", strParam);
                string Version = CommonGlobalSettings.Utilities.GetParameter("Version", strParam);
                string Settings;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Settings = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    throw new Exception("settings cannot be empty");
                }
                oKodak.BeginTransaction();
                oKodak.ExecuteNonQuery(string.Format("update tScoringSettings set IsCurrent=0 where IsCurrent=1 and Site='{0}' and Type='{1}'", Site, Type), RisDAL.ConnectionState.KeepOpen);
                string sql = string.Format("insert into tScoringSettings values('{0}','{1}','1','{2}','{3}','{4}')", Version, Type, Settings.Replace("'", "''"), Site, CommonGlobalSettings.Utilities.GetCurDomain());
                oKodak.ExecuteQuery(sql, RisDAL.ConnectionState.KeepOpen);
                oKodak.CommitTransaction();
            }
            catch (Exception ex)
            {
                bReturn = false;
                oKodak.RollbackTransaction();
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public bool GetAppraiseNew(string rpGuid, string type, DataSet ds, ref string errorMsg)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            int result = -1;

            try
            {
                string sql = "Select * from tQualityScoring where AppraiseObject = '" + rpGuid + "'";
                if (type.ToUpper() == "REPORT")
                {
                    sql = "select tReport.* from tReport,tRegProcedure where tReport.ReportGuid = tRegProcedure.ReportGuid and tRegProcedure.ProcedureGuid = '" + rpGuid + "'";
                }
                dt = oKodak.ExecuteQuery(sql);
                if (dt != null && (dt.Rows.Count > 0))
                {
                    ds.Tables.Add(dt);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public bool SaveAppraiseNew(DataSet ds, ref string errorMsg)
        {
            RisDAL oKodak = new RisDAL();
            int result = 0;
            string AppraiseObject = string.Empty;
            string OrderGuid = string.Empty;
            string Type = string.Empty;
            string Result = string.Empty;
            string Appraiser = string.Empty;
            string Comment = string.Empty;
            string Version = string.Empty;
            string Indexs = string.Empty;
            string SelectedTexts = string.Empty;
            string AccordRate = string.Empty;
            string UserID = string.Empty;
            string UserName = string.Empty;
            string UserRole = string.Empty;
            string IP = string.Empty;
            string Temp = string.Empty;
            try
            {
                oKodak.BeginTransaction();
                AppraiseObject = Convert.ToString(ds.Tables[0].Rows[0]["AppraiseObject"]);
                OrderGuid = Convert.ToString(ds.Tables[0].Rows[0]["OrderGuid"]);
                Type = Convert.ToString(ds.Tables[0].Rows[0]["Type"]);
                Result = Convert.ToString(ds.Tables[0].Rows[0]["Result"]);
                Appraiser = Convert.ToString(ds.Tables[0].Rows[0]["Appraiser"]);
                Comment = Convert.ToString(ds.Tables[0].Rows[0]["Comment"]);
                Version = Convert.ToString(ds.Tables[0].Rows[0]["Version"]);
                Indexs = Convert.ToString(ds.Tables[0].Rows[0]["Indexs"]);
                SelectedTexts = Convert.ToString(ds.Tables[0].Rows[0]["SelectedTexts"]);
                AccordRate = Convert.ToString(ds.Tables[0].Rows[0]["AccordRate"]);
                UserID = Convert.ToString(ds.Tables[0].Rows[0]["UserID"]);
                UserName = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                UserRole = Convert.ToString(ds.Tables[0].Rows[0]["UserRole"]);
                IP = Convert.ToString(ds.Tables[0].Rows[0]["IP"]);


                string sql = "";
                if (Type == "2")
                {

                    sql = string.Format("Update tReport set ReportQuality = @ReqportQuality,ReportQuality2 = @ReqportQuality2,ReportQualityComments=@ReportQualityComments,ScoringVersion=@ScoringVersion,AccordRate=@AccordRate where ReportGuid in ({0}) \r\n", AppraiseObject);
                    oKodak.Parameters.Add("@ReqportQuality", Result);
                    oKodak.Parameters.Add("@ReqportQuality2", Indexs);
                    oKodak.Parameters.Add("@ReportQualityComments", Comment);
                    oKodak.Parameters.Add("@ScoringVersion", Version);
                    oKodak.Parameters.Add("@AccordRate", AccordRate);

                    sql += string.Format(" Update tScoringResult set IsFinalVersion =0 where IsFinalVersion = 1 and ObjectGuid in ('{0}') and Type='{1}' \r\n ", AppraiseObject.Trim("'".ToCharArray()), Type);
                    string[] arr = AppraiseObject.Split(',');
                    foreach (string guid in arr)
                    {
                        if (!string.IsNullOrWhiteSpace(guid))
                        {
                            sql += string.Format("Insert into tScoringResult(Guid,ObjectGuid,Type,Result,Domain,Result2,Appraiser,Comment,AccordRate) values(NEWID(),{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                guid, Type, SelectedTexts.Replace("'", "''"), CommonGlobalSettings.Utilities.GetCurDomain(), Result, Appraiser, Comment, AccordRate) + "\r\n";
                        }
                    }
                }
                else
                {//imagequalityscoring
                    string sqlGeneral = string.Format("Select ProcedureGuid,ExamineDt,Technician from tRegProcedure where OrderGuid ='{0}' and IsExistImage = 1 and ProcedureGuid in ('{1}') ",
                                        ds.Tables[0].Rows[0]["OrderGuid"].ToString(), System.Convert.ToString(ds.Tables[0].Rows[0]["AppraiseObject"]).Trim("'".ToCharArray()));

                    DataTable dt = oKodak.ExecuteQuery(sqlGeneral, RisDAL.ConnectionState.KeepOpen);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sql = string.Format(" Delete from tQualityScoring where AppraiseObject in ('{0}') \r\n ", AppraiseObject.Trim("'".ToCharArray()));
                        foreach (DataRow dr in dt.Rows)
                        {
                            sql += string.Format(" Insert into tQualityScoring(Guid,AppraiseObject,OrderGuid,ExaminateDt,Type,Result,Appraisee,Appraiser,AppraiseDate,Comment,Domain,Result2,Result3) values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',@Comment,'{9}','{10}','{11}')",
                                                Guid.NewGuid().ToString(),
                                                dr["ProcedureGuid"].ToString(),
                                                OrderGuid,
                                                dr["ExamineDt"].ToString().ToString(),
                                                Type,
                                                Result,
                                                dr["Technician"].ToString(),
                                                Appraiser,
                                                DateTime.Now,
                                                CommonGlobalSettings.Utilities.GetCurDomain(),
                                                Version,
                                                Indexs
                                                ) + "\r\n";
                        }
                        oKodak.Parameters.Add("@Comment", ds.Tables[0].Rows[0]["Comment"]);

                        sql += string.Format(" Update tScoringResult set IsFinalVersion = 0 where IsFinalVersion = 1 and ObjectGuid in ('{0}') and Type='{1}' \r\n ", AppraiseObject.Trim("'".ToCharArray()), Type);
                        string[] arr = AppraiseObject.Split(',');
                        foreach (string guid in arr)
                        {
                            if (!string.IsNullOrWhiteSpace(guid))
                            {
                                sql += string.Format("Insert into tScoringResult(Guid,ObjectGuid,Type,Result,Domain,Result2,Appraiser,Comment) values(NEWID(),{0},'{1}','{2}','{3}','{4}','{5}','{6}')",
                                      guid, Type, SelectedTexts.Replace("'", "''"), CommonGlobalSettings.Utilities.GetCurDomain(), Result, Appraiser, Comment) + "\r\n";
                            }
                        }
                    }
                }

                if (sql.Trim().Length > 0)
                {
                    result = oKodak.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();
                Temp = "";
                if (Type == "2")
                {
                    Temp = "Comment:" + Comment + ";AccordRate:" + AccordRate + ";";
                }
                else
                {
                    Temp = "Comment:" + Comment + ";";
                }
                if (result > 0)
                {
                    HippaLogTool.AuditEvtMsg("Scoring", ActionCode.Scoring, (Type == "2" ? EventTypeCode.ScoreReport : EventTypeCode.ScoreImage), "", UserID, UserName, UserRole, IP, "True", "Scoring", "Scoring", "Scoring", AppraiseObject, Result, SelectedTexts, Temp, true);
                }
                return true;
            }
            catch (Exception ex)
            {
                oKodak.RollbackTransaction();
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                HippaLogTool.AuditEvtMsg("Scoring", ActionCode.Scoring, (Type == "2" ? EventTypeCode.ScoreReport : EventTypeCode.ScoreImage), "", UserID, UserName, UserRole, IP, "True", "Scoring", "Scoring", "Scoring", AppraiseObject, Result, SelectedTexts, Temp, false);

            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return true;
        }

        public bool QueryScoringHistoryList(string strParam, DataSet ds)
        {
            bool bReturn = false;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                string Type = CommonGlobalSettings.Utilities.GetParameter("Type", strParam);
                string ObjectGuid = CommonGlobalSettings.Utilities.GetParameter("ObjectGuid", strParam);
                string sql = string.Format("Select CreateDate,Result2, (select top 1 LocalName from tUser where UserGuid = Appraiser) as Appraiser,Comment,AccordRate,Result from tScoringResult where ObjectGuid = '{0}' and Type = 1 order by CreateDate desc", ObjectGuid);
                if (Type.ToUpper() == "REPORT")
                {
                    sql = string.Format("Select CreateDate,Result2, (select top 1 LocalName from tUser where UserGuid = Appraiser) as Appraiser,Comment,AccordRate,Result from tScoringResult where ObjectGuid = '{0}' and Type = 2 order by CreateDate desc", ObjectGuid);
                }
                else if (string.IsNullOrWhiteSpace(Type))
                {
                    string[] arr = ObjectGuid.Split(';');
                    sql = string.Format("select ImageScoringResult,ImageScoringDoctor,ImageScoringDate,ReportScoringResult,ReportScoringDoctor,ReportScoringDate from (select top 1 Result2 as ImageScoringResult, (select top 1 LocalName from tUser where UserGuid = Appraiser) as ImageScoringDoctor, CreateDate as ImageScoringDate, 1 as id from tscoringresult where Type=1 and ObjectGuid = '{0}' order by CreateDate desc) a full outer join (select top 1 Result2 as ReportScoringResult, (select top 1 LocalName from tUser where UserGuid = Appraiser) as ReportScoringDoctor, CreateDate as ReportScoringDate, 1 as id from tscoringresult where Type=2 and ObjectGuid = '{1}' order by CreateDate desc) b on a.id = b.id ", arr[0], arr[1]);
                }
                dt = oKodak.ExecuteQuery(sql);
                if (dt != null)
                {
                    ds.Tables.Add(dt);
                    bReturn = true;
                }
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        #endregion

        #region **IKnowledgeDataManagementDAO
        #region **1.bool AddNewInnerNode(string strGuid, string strParentID, string strName,string strComments)
        /// <summary>
        /// Name:AddNewNode
        /// Function:Add a new directory node of KMS
        /// </summary>
        /// <param name="strGuid">New directory node guid</param>
        /// <param name="strParentID">New directory node's parent guid</param>
        /// <param name="strPath">The path from root ex: "...root/node"</param>
        /// <param name="strName">Name of the node</param>
        /// <param name="strUserGuid">UserGuid</param>
        /// <param name="strComments">comments of this node</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool AddNewInnerNode(string strGuid, string strParentID, string strPath, string strName, string strUserGuid, string strComments, string strNodeOrder)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                //select the max nodeorder in db
                string strMaxOrderFileSQL = string.Format("Select MAX(NodeOrder) from tKnowledgeFiles Where KnowledgeGuid ='{0}'", strParentID);

                string strMaxOrderCategorySQL = string.Format("Select MAX(NodeOrder) from tKnowledge Where ParentID ='{0}'", strParentID);
                //1.max in knowledge table
                Object obj = dataAccess.ExecuteScalar(strMaxOrderCategorySQL);
                int NewOrderCategory = 0;
                if (obj == DBNull.Value || obj == null)//no one order
                {
                    NewOrderCategory = 0;
                }
                else
                {
                    if (Convert.ToInt32(obj) >= 0)
                    {
                        NewOrderCategory = Convert.ToInt32(obj) + 1;
                    }
                }
                //2.max in knowledgefiles table
                obj = dataAccess.ExecuteScalar(strMaxOrderFileSQL);
                int NewOrderFile = 0;
                if (obj == DBNull.Value || obj == null)//no one order
                {
                    NewOrderFile = 0;
                }
                else
                {
                    if (Convert.ToInt32(obj) >= 0)
                    {
                        NewOrderFile = Convert.ToInt32(obj) + 1;
                    }
                }

                int NewOrder = ((NewOrderFile >= NewOrderCategory) ? NewOrderFile : NewOrderCategory);


                string strSQL = string.Format("Insert into tKnowledge(KnowledgeGuid, ParentID, Path, Name,IsLeaf,Creator,CreateDt,Comments, NodeOrder,Domain) Values('{0}','{1}','{2}','{3}',0,'{4}','{5}',@Comments,{6},'{7}')",
                    strGuid, strParentID, strPath, strName, strUserGuid, DateTime.Now, NewOrder, CommonGlobalSettings.Utilities.GetCurDomain());
                dataAccess.Parameters.Add("@Comments", strComments);
                dataAccess.ExecuteNonQuery(strSQL);
                return true;

            }
            catch (Exception e)
            {
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
        }
        #endregion
        #region **2.bool AddNewLeafNode(string strGuid, string strParentID, string strFileName)
        /// <summary>
        /// Name:AddNewLeafNode
        /// Function:Add a New KMS leaf node to the tKnowledgeFiles table
        /// </summary>
        /// <param name="strGuid">New KMS leaf node guid </param>
        /// <param name="strParentID">New KMS leaf node 's parent guid</param>
        /// <param name="strFileName">New KMS leaf node's FileName</param>
        /// <param name="strFileName">New KMS leaf node's NodeOrder</param>/// 
        /// <returns>True:successful    False:failed</returns>
        public virtual bool AddNewLeafNode(string strGuid, string strParentID, string strFileName, string strNodeOrder)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                //select the max nodeorder in db
                string strMaxOrderFileSQL = string.Format("Select MAX(NodeOrder) from tKnowledgeFiles Where KnowledgeGuid ='{0}'", strParentID);
                string strMaxOrderCategorySQL = string.Format("Select MAX(NodeOrder) from tKnowledge Where ParentID ='{0}'", strParentID);

                //1.max in knowledge table
                Object obj = dataAccess.ExecuteScalar(strMaxOrderCategorySQL);
                int NewOrderCategory = 0;
                if (obj == DBNull.Value || obj == null)//no one order
                {
                    NewOrderCategory = 0;
                }
                else
                {
                    if (Convert.ToInt32(obj) >= 0)
                    {
                        NewOrderCategory = Convert.ToInt32(obj) + 1;
                    }
                }
                //2.max in knowledgefiles table
                obj = dataAccess.ExecuteScalar(strMaxOrderFileSQL);
                int NewOrderFile = 0;
                if (obj == DBNull.Value || obj == null)//no one order
                {
                    NewOrderFile = 0;
                }
                else
                {
                    if (Convert.ToInt32(obj) >= 0)
                    {
                        NewOrderFile = Convert.ToInt32(obj) + 1;
                    }
                }

                int NewOrder = ((NewOrderFile >= NewOrderCategory) ? NewOrderFile : NewOrderCategory);


                string strSQL = string.Format("Insert into tKnowledgeFiles(Guid, KnowledgeGuid, FileName,IsLink,LinkToGuid,NodeOrder,Domain) Values('{0}','{1}','{2}',0,'',{3},'{4}')",
                    strGuid, strParentID, strFileName, NewOrder, CommonGlobalSettings.Utilities.GetCurDomain());
                dataAccess.ExecuteNonQuery(strSQL);
                return true;

            }
            catch (Exception e)
            {
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
        }
        #endregion
        #region **3.bool UpdateInnerNodeName(string strGuid, string strName)
        /// <summary>
        /// Name : UpdateNodeName
        /// Function:Modify the name of node(tKnowledge node's Name only)
        /// </summary>
        /// <param name="strGuid">Current node 's guid</param>
        /// <param name="strName">New node name</param>
        /// <param name="strComments">New node comments</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool UpdateInnerNodeName(string strGuid, string strName, string strComments)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string strSQL = string.Format("Update tKnowledge set Name='{0}',Comments =@Comments  where KnowledgeGuid='{1}'", strName, strGuid);
                oKodakDAL.Parameters.Add("@Comments", strComments);
                int count = oKodakDAL.ExecuteNonQuery(strSQL);
                return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
                return false;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return false;
        }
        #endregion
        #region**4.bool DeleteInnerNode(string strGuid)
        /// <summary>
        /// Name : DeleteInnerNode
        /// Function:Delete the node by guid
        /// </summary>
        /// <param name="strGuid">Current node 's guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool DeleteInnerNode(string strGuid)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int iCount = 0;//deleted record count

            string strSQL = string.Format("delete from tKnowledge where KnowledgeGuid ='{0}'", strGuid);
            try
            {
                iCount = oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                return true;
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
            return false;

        }
        #endregion
        #region**5.bool DeleteLeafNode(string strGuid)
        /// <summary>
        /// Name : DeleteNode
        /// Function:Delete the Leaf node by guid(node and leaf node),if it have subnodes delete them firstly too!
        /// </summary>
        /// <param name="strGuid">Current node 's guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool DeleteLeafNode(string strGuid)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int iCount = 0;//deleted record count

            string strSQL = string.Format("delete from tKnowledgeFiles where Guid ='{0}'", strGuid);
            try
            {
                iCount = oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                return true;
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
            return false;

        }
        #endregion
        #region**6.DataSet GetAllInnerNodes( )
        /// <summary>
        /// Name:GetAllInnerNodes
        /// Function:Get all nodes of the directory node(exclude the leaf nodes)
        /// </summary>
        /// <returns>Return dataset have one table,it contains child nodes information</returns>
        public virtual DataSet GetAllInnerNodes()
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string strCondition = "Select * from tKnowledge";
            try
            {
                dt = oKodakDAL.ExecuteQuery(strCondition);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.TableName = "tKnowledge";
                    ds.Tables.Add(dt);
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
            }
            return ds;

        }
        #endregion
        #region**7.DataSet GetAllLeafNodes()
        /// <summary>
        /// Name:GetAllLeafNodes
        /// Function:Get all nodes of the directory node(exclude the leaf nodes)
        /// </summary>
        /// <param name="strGuid">KMS node guid</param>
        /// <returns>Return dataset have one table,it contains child nodes information</returns>
        public virtual DataSet GetAllLeafNodes()
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            string strCondition = "Select * from tKnowledgeFiles order by KnowledgeGuid ASC";
            try
            {
                dt = oKodakDAL.ExecuteQuery(strCondition);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.TableName = "tKnowledgeFiles";
                    ds.Tables.Add(dt);
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
            }
            return ds;

        }
        #endregion
        #region**8.bool IsLeaf(string strGuid)
        /// <summary>
        /// Name:IsLeaf
        /// Function:Checking current node if is a leaf node
        /// </summary>
        /// <param name="strGuid">Current node guid</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool IsLeaf(string strGuid)
        {
            return true;//currently always return true,for they are all inner node.
        }
        #endregion
        #region**9.DataSet GetInnerNodeInfo()
        /// <summary>
        /// Name:GetInnerNodeInfo
        /// Function:Get InnerNodeInfo
        /// </summary>
        /// <returns>Return dataset have one table,it contains InnerNodeInfo</returns>  
        public virtual DataSet GetInnerNodeInfo(string guid)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            string strCondition = string.Format("Select * from tKnowledge where KnowledgeGuid ='{0}'", guid);
            try
            {
                dt = oKodakDAL.ExecuteQuery(strCondition);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.TableName = "tKnowledge";
                    ds.Tables.Add(dt);
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
            }
            return ds;

        }
        #endregion
        #region**10.bool UpdatePath(string guid,string strPath)
        /// <summary>
        /// Name:UpdateFath
        /// Function:UpdatePath(string strGuid, string strPath)
        /// </summary>
        /// <returns>UpdateFath if rename node</returns>
        public virtual bool UpdatePath(string strGuid, string strPath)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string strSQL = string.Format("Update tKnowledge set Path='{0}' where KnowledgeGuid='{1}'", strPath, strGuid);
                int count = oKodakDAL.ExecuteNonQuery(strSQL);
                return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
                return false;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return false;
        }
        #endregion
        #region**11.bool MoveTo(string strGuid,string strParentGuid,string isLeaf)
        /// <summary>
        /// Name:MoveTo
        /// Function:MoveTo(string strGuid,string strParentGuid)
        /// </summary>
        /// <returns>Move to exist node</returns>
        public virtual bool MoveTo(string strGuid, string strParentGuid, string isLeaf, string strNodeOrder)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //select the max nodeorder in db
                //string strMaxOrderSQL = "";
                //if (!Convert.ToBoolean(isLeaf))//root
                //{
                //    strMaxOrderSQL = string.Format("Select MAX(NodeOrder) from tKnowledge Where ParentID ='{0}'", strParentGuid);
                //}
                //else
                //{
                //    strMaxOrderSQL = string.Format("Select MAX(NodeOrder) from tKnowledgeFiles Where KnowledgeGuid ='{0}'", strParentGuid);
                //}
                //select the max nodeorder in db
                string strMaxOrderFileSQL = string.Format("Select MAX(NodeOrder) from tKnowledgeFiles Where KnowledgeGuid ='{0}'", strParentGuid);
                string strMaxOrderCategorySQL = string.Format("Select MAX(NodeOrder) from tKnowledge Where ParentID ='{0}'", strParentGuid);

                //1.max in knowledge table
                Object obj = oKodakDAL.ExecuteScalar(strMaxOrderCategorySQL);
                int NewOrderCategory = 0;
                if (obj == DBNull.Value || obj == null)//no one order
                {
                    NewOrderCategory = 0;
                }
                else
                {
                    if (Convert.ToInt32(obj) >= 0)
                    {
                        NewOrderCategory = Convert.ToInt32(obj) + 1;
                    }
                }
                //2.max in knowledgefiles table
                obj = oKodakDAL.ExecuteScalar(strMaxOrderFileSQL);
                int NewOrderFile = 0;
                if (obj == DBNull.Value || obj == null)//no one order
                {
                    NewOrderFile = 0;
                }
                else
                {
                    if (Convert.ToInt32(obj) >= 0)
                    {
                        NewOrderFile = Convert.ToInt32(obj) + 1;
                    }
                }

                int NewOrder = ((NewOrderFile >= NewOrderCategory) ? NewOrderFile : NewOrderCategory);


                string strSQL = "";
                if (!Convert.ToBoolean(isLeaf))//category
                {
                    strSQL = string.Format("Update tKnowledge set ParentID ='{0}',Path= (Select Path from tKnowledge where KnowledgeGuid = '{1}')+'/'+(Select Name from tKnowledge where KnowledgeGuid = '{1}',IsLink = 0),NodeOrder = {2}  where KnowledgeGuid = '{1}'", strParentGuid, strGuid, NewOrder);
                    //strSQL = string.Format("insert into tKnowledge (KnowledgeGuid, ParentID, Path, Name,IsLeaf,Creator,CreateDt,Comments,IsLink) Values('{0}','{1}','','',0,'{2}','{3}','',1))",
                    //            strGuid, strParentID, strUserGuid, DateTime.Now);
                    //strSQL += string.Format("\r\n update tKnowledge set Path = (select Path from tKnowledge where KnowledgeGuid ='{0}'),Name =(select Name from tKnowledge where KnowledgeGuid ='{0}'),Comments = (select Path from tKnowledge where KnowledgeGuid ='{0}')",
                    //        strGuid);
                }
                else
                {
                    strSQL = string.Format("Update tKnowledgeFiles set KnowledgeGuid ='{1}',NodeOrder = {2} where Guid = '{0}'", strGuid, strParentGuid, NewOrder);
                    //strSQL = string.Format("insert into tKnowledgeFiles (Guid, KnowledgeGuid, FileName,IsLink) Values('{0}','{1}','',1))",
                    //            strGuid, strParentID, DateTime.Now);
                    //strSQL += string.Format("\r\n update tKnowledgeFiles set Path = (select Path from tKnowledge where KnowledgeGuid ='{0}') + '/'+ (select FileName from tKnowledge where Guid ='{1}') where Guid = '{1}')",
                    //        strParentGuid,strGuid);

                }
                obj = oKodakDAL.ExecuteNonQuery(strSQL);
                if (obj == DBNull.Value || obj == null)
                {
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(obj) > 0)
                    {
                        return true;
                    }
                }
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
            return false;
        }
        #endregion
        #region**12.bool CopyTo(string strGuid, string strParentGuid,string strUserGuid,string isLeaf)
        /// <summary>
        /// Name:CopyTo
        /// Function:CopyTo(string strGuid, string strParentGuid,string strUserGuid,string isLeaf)
        /// </summary>
        /// <returns>Copy  to exist node</returns>
        public virtual bool CopyTo(string strGuid, string strParentID, string strSrcID, string strUserGuid, string isLeaf, string strOrd)
        {
            {
                RisDAL oKodakDAL = new RisDAL();
                try
                {
                    //select the max nodeorder in db
                    // string strMaxOrderSQL = "";
                    //if (!Convert.ToBoolean(isLeaf))//root
                    //{
                    //    strMaxOrderSQL = string.Format("Select MAX(NodeOrder) from tKnowledge Where ParentID ='{0}'", strParentID);
                    //}
                    //else
                    //{
                    //    strMaxOrderSQL = string.Format("Select MAX(NodeOrder) from tKnowledgeFiles Where KnowledgeGuid ='{0}'", strParentID);
                    //} 
                    //select the max nodeorder in db
                    string strMaxOrderFileSQL = string.Format("Select MAX(NodeOrder) from tKnowledgeFiles Where KnowledgeGuid ='{0}'", strParentID);
                    string strMaxOrderCategorySQL = string.Format("Select MAX(NodeOrder) from tKnowledge Where ParentID ='{0}'", strParentID);

                    //1.max in knowledge table
                    Object obj = oKodakDAL.ExecuteScalar(strMaxOrderCategorySQL);
                    int NewOrderCategory = 0;
                    if (obj == DBNull.Value || obj == null)//no one order
                    {
                        NewOrderCategory = 0;
                    }
                    else
                    {
                        if (Convert.ToInt32(obj) >= 0)
                        {
                            NewOrderCategory = Convert.ToInt32(obj) + 1;
                        }
                    }
                    //2.max in knowledgefiles table
                    obj = oKodakDAL.ExecuteScalar(strMaxOrderFileSQL);
                    int NewOrderFile = 0;
                    if (obj == DBNull.Value || obj == null)//no one order
                    {
                        NewOrderFile = 0;
                    }
                    else
                    {
                        if (Convert.ToInt32(obj) >= 0)
                        {
                            NewOrderFile = Convert.ToInt32(obj) + 1;
                        }
                    }

                    int NewOrder = ((NewOrderFile >= NewOrderCategory) ? NewOrderFile : NewOrderCategory);


                    string strSQL = "";
                    if (!Convert.ToBoolean(isLeaf))//category
                    {
                        strSQL = string.Format("insert into tKnowledge (KnowledgeGuid, ParentID, Path, Name,IsLeaf,Creator,CreateDt,Comments,IsLink,NodeOrder) Values('{0}','{1}','','',0,'{2}','{3}','',0,{4})",
                                 strGuid, strParentID, strUserGuid, DateTime.Now, NewOrder);
                        strSQL += string.Format("\r\n update tKnowledge set Path = (select Path from tKnowledge where KnowledgeGuid ='{0}') + '/'+ (select Name from tKnowledge where KnowledgeGuid ='{0}'),Name =(select Name from tKnowledge where KnowledgeGuid ='{0}'),Comments =(select Comments from tKnowledge where KnowledgeGuid ='{0}') where KnowledgeGuid = '{1}')",
                                strSrcID, strGuid);

                    }
                    else
                    {
                        strSQL = string.Format("insert into tKnowledgeFiles (Guid, KnowledgeGuid, FileName,IsLink,LinkToGuid,NodeOrder) Values('{0}','{1}','',0,'{2}',{3})",
                                    strGuid, strParentID, strSrcID, NewOrder);
                        strSQL += string.Format("\r\n update tKnowledgeFiles set FileName = (select FileName from tKnowledgeFiles where Guid ='{0}'),IsLink = 0 ,LinkToGuid = '{0}' where Guid = '{1}'",
                                strSrcID, strGuid);
                    }
                    obj = oKodakDAL.ExecuteNonQuery(strSQL);
                    if (obj == DBNull.Value || obj == null)
                    {
                        return false;
                    }
                    else
                    {
                        if (Convert.ToInt32(obj) > 0)
                        {
                            return true;
                        }
                    }
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
                return false;
            }
        }
        #endregion
        #region **13. bool NodeNameExisted(string strName, string strParentID,string isLeaf)

        public virtual bool NodeNameExisted(string strName, string strParentID, string isLeaf)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string strSQL = "";
                if (!Convert.ToBoolean(isLeaf))//category
                {
                    strSQL = string.Format("select count(*) from tKnowledge where Name ='{0}' and ParentID ='{1}'",
                             strName, strParentID);

                }
                else
                {
                    strSQL = string.Format("select count(*) from tKnowledgeFiles where FileName ='{0}' and KnowledgeGuid ='{1}'",
                             strName, strParentID);
                }
                object obj = oKodakDAL.ExecuteScalar(strSQL);
                if (obj == DBNull.Value)
                {
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(obj) > 0)
                    {
                        return true;
                    }
                }
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
            return false;
        }
        #endregion
        #region **14.bool SwapNodeOrder(string strGuidUp, string strGuidDown, string isUp)
        public virtual bool SwapNodeOrder(string strGuidUp, string strGuidDown, string isLeaf)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                #region which tables will be upate the NodeOrder field
                string strOrderFileUpSQL = string.Format("Select NodeOrder from tKnowledgeFiles Where Guid ='{0}'", strGuidUp);
                string strOrderFileDownSQL = string.Format("Select NodeOrder from tKnowledgeFiles Where Guid ='{0}'", strGuidDown);

                string strOrderCategoryUpSQL = string.Format("Select NodeOrder from tKnowledge Where KnowledgeGuid ='{0}'", strGuidUp);
                string strOrderCategoryDownSQL = string.Format("Select NodeOrder from tKnowledge Where KnowledgeGuid ='{0}'", strGuidDown);

                //1.max in knowledge table
                DataTable dtOrderFileUp = oKodakDAL.ExecuteQuery(strOrderFileUpSQL);
                DataTable dtOrderFileDown = oKodakDAL.ExecuteQuery(strOrderFileDownSQL);
                DataTable dtOrderCategoryUp = oKodakDAL.ExecuteQuery(strOrderCategoryUpSQL);
                DataTable dtOrderCategoryDown = oKodakDAL.ExecuteQuery(strOrderCategoryDownSQL);

                DataTable dtOrderUp = null;
                DataTable dtOrderDown = null;
                string needUpdateUpTableName = "";
                string needUpdateDownTableName = "";

                //retrive two node order value table
                if (0 != dtOrderFileUp.Rows.Count) { dtOrderUp = dtOrderFileUp; needUpdateUpTableName = "KnowledgeFiles"; };
                if (0 != dtOrderFileDown.Rows.Count) { dtOrderDown = dtOrderFileDown; needUpdateDownTableName = "KnowledgeFiles"; };
                if (0 != dtOrderCategoryUp.Rows.Count) { dtOrderUp = dtOrderCategoryUp; needUpdateUpTableName = "Knowledge"; };
                if (0 != dtOrderCategoryDown.Rows.Count) { dtOrderDown = dtOrderCategoryDown; needUpdateDownTableName = "Knowledge"; };



                int orderUp = 0;
                int orderDown = 0;
                if (dtOrderUp != null && dtOrderUp.Rows.Count > 0)//have one order
                {
                    orderUp = Convert.ToInt32(dtOrderUp.Rows[0]["NodeOrder"]);
                }
                if (dtOrderDown != null && dtOrderDown.Rows.Count > 0)//have one order
                {
                    orderDown = Convert.ToInt32(dtOrderDown.Rows[0]["NodeOrder"]);
                }
                #endregion
                string strSQL = "";
                //if (Convert.ToBoolean(isLeaf))//leaf
                {
                    //1. up 
                    if (needUpdateUpTableName == "KnowledgeFiles")
                    {
                        strSQL = string.Format("Update tKnowledgeFiles Set NodeOrder = {0}  Where Guid = '{1}' \r\n",
                                 orderDown, strGuidUp);//donw one to up
                    }
                    else
                    {
                        strSQL += string.Format("Update tKnowledge Set NodeOrder = {0}  Where KnowledgeGuid = '{1}' \r\n",
                                 orderDown, strGuidUp);//donw one to up  
                    }
                    //1. down 
                    if (needUpdateDownTableName == "KnowledgeFiles")
                    {
                        strSQL += string.Format("Update tKnowledgeFiles Set NodeOrder = {0}  Where Guid = '{1}' \r\n",
                                 orderUp, strGuidDown);//donw one to up
                    }
                    else
                    {
                        strSQL += string.Format("Update tKnowledge Set NodeOrder = {0}  Where KnowledgeGuid = '{1}' \r\n",
                                 orderUp, strGuidDown);//donw one to up  
                    }

                }
                int count = oKodakDAL.ExecuteNonQuery(strSQL);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        }
        #endregion
        #endregion

        #region IConditionCOlDao

        #region DataSet GetAllConditionItems()

        public virtual DataSet GetAllConditionItems()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dtCondition;
            DataTable dtConditionCol;
            string GetConditionCol = "select type,conditionname,itemID,ModuleID,label,orderID,IsHidden,orderIDQuick,isHiddenQuick,[Group] from tpanel inner join tconditioncolumn on tpanel.title=tconditioncolumn.conditionname";
            string GetCondition = "select distinct conditionname,ModuleID,title from tpanel inner join tconditioncolumn on tpanel.title=tconditioncolumn.conditionname order by moduleID";
            try
            {
                dtConditionCol = dataAccess.ExecuteQuery(GetConditionCol);
                dtConditionCol.TableName = "dtConditionCol";
                ds.Tables.Add(dtConditionCol);
                dtCondition = dataAccess.ExecuteQuery(GetCondition);
                dtCondition.TableName = "dtCondition";
                ds.Tables.Add(dtCondition);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
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

        public virtual bool SaveColChange(BaseDataSetModel model)
        {
            DataTable dt = model.DataSetParameter.Tables[0];
            StringBuilder sb = new StringBuilder();

            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                foreach (DataRow dr in dt.Rows)
                {
                    sb.AppendFormat("update tconditioncolumn set OrderID={0} ,IsHidden={1},OrderIDQuick={2} ,IsHiddenQuick={3} ", dr["OrderID"].ToString(), dr["IsHidden"].ToString(), dr["OrderIDQuick"].ToString(), dr["IsHiddenQuick"].ToString());
                    sb.AppendFormat(" where conditionName='{0}' and ItemID={1}", dr["ConditionName"].ToString(), dr["ItemID"].ToString());
                    dataAccess.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                    sb.Remove(0, sb.Length);
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception ex)
            {
                dataAccess.RollbackTransaction();
                throw new Exception(ex.Message);
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
        #endregion

        #region ExclusionCondition
        public virtual DataSet GetAllExclusionConditions()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dtCondition;
            DataTable dtConditionCol;
            DataTable dtExclusionCondition;
            string GetConditionCol = "select * from tpanel inner join tconditioncolumn on tpanel.title=tconditioncolumn.conditionname";
            string GetCondition = "select distinct conditionname,ModuleID,title from tpanel inner join tconditioncolumn on tpanel.title=tconditioncolumn.conditionname order by moduleID";
            string GetExclusionCondition = "select * from texclusioncondition";
            try
            {
                dtConditionCol = dataAccess.ExecuteQuery(GetConditionCol);
                dtConditionCol.TableName = "dtConditionCol";
                ds.Tables.Add(dtConditionCol);
                dtCondition = dataAccess.ExecuteQuery(GetCondition);
                dtCondition.TableName = "dtCondition";
                ds.Tables.Add(dtCondition);
                dtExclusionCondition = dataAccess.ExecuteQuery(GetExclusionCondition);
                dtExclusionCondition.TableName = "dtExclusionCondition";
                //foreach (DataRow dr in dtExclusionCondition.Rows)
                //{
                //    dr["ExclusionConditionSql"] = dr["ExclusionConditionSql"].ToString().Replace("''", "'");
                //}
                ds.Tables.Add(dtExclusionCondition);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
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

        public virtual DataSet GetOperatorMap()
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            try
            {
                string sql = "select * from tOperatorMap";
                ds.Tables.Add(dataAccess.ExecuteQuery(sql));
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
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

        public virtual DataSet GetDataSource(string sql)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            try
            {
                ds.Tables.Add(dataAccess.ExecuteQuery(sql));
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
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

        public virtual bool SaveExclusionCondition(DataTable dt, string conditionName, string domain, string site)
        {
            StringBuilder sb = new StringBuilder();
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                sb.AppendFormat("delete from tExclusionCondition where ConditionName = '{0}' and Site='{1}';", conditionName, site);
                foreach (DataRow dr in dt.Rows)
                {
                    string exclusionSql = dr["ExclusionConditionSql"].ToString();
                    sb.AppendFormat("insert into tExclusionCondition values ('{0}','{1}',{2},'{3}','{4}','{5}');", conditionName, exclusionSql.Replace("'", "''"), Convert.ToInt32(dr["IsDefault"]), dr["Alias"].ToString(), domain, dr["Site"].ToString());
                }
                dataAccess.ExecuteNonQuery(sb.ToString(), RisDAL.ConnectionState.KeepOpen);
                dataAccess.CommitTransaction();
            }
            catch (Exception ex)
            {
                dataAccess.RollbackTransaction();
                throw new Exception(ex.Message);
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

        public virtual DataSet GetExclusionConditionSqlByPanelTitle(string title)
        {
            RisDAL dataAccess = new RisDAL();
            DataSet ds = new DataSet();
            try
            {
                string sql = string.Format("select * from tExclusionCondition where ConditionName = '{0}' and IsDefault = 1 and Domain = '{1}' and Site = '{2}'", title, CommonGlobalSettings.Utilities.GetCurDomain(), CommonGlobalSettings.Utilities.GetCurSite());
                DataTable dt = dataAccess.ExecuteQuery(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["ExclusionConditionSql"] = dr["ExclusionConditionSql"].ToString().Replace("{", "(").Replace("}", ")");
                    }
                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Templates_DA, ModuleInstanceName.Templates, 1, ex.Message, Application.StartupPath.ToString(),
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
        #endregion

        #region **IModalityTimeSliceSettingDAO
        #region **1.bool AddModalityTimeSlice(string strGuid, string strModalityType, string strModality, string strStartTime, string strEndTime,strMaxMan)
        /// <summary>
        /// Name:AddModalityTimeSlice
        /// Function:Add a new AddModalityTimeSlice
        /// </summary>
        /// <param name="strGuid">New ModalityTimeSlice guid</param>
        /// <param name="strModalityType">ModalityType</param>
        /// <param name="strModality">Modality</param>
        /// <param name="strStartTime">StartTime</param>
        /// <param name="strEndTime">EndTime</param>
        /// <param name="strDescription">Description</param>/// 
        /// <returns>True:successful    False:failed</returns>
        public virtual bool AddModalityTimeSlice(string strGuid, string strModalityType, string strModality, string strStartTime, string strEndTime, string strDescription, string strMaxMan, string strDomain)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                //check if description is duplicated in same modality
                string strSQLModaliltyDul = string.Format("Select Count(1) from tModalityTimeSlice Where Description =@Description and Modality = '{0}'", strModality);
                dataAccess.Parameters.Add("@Description", strDescription);

                object obj = dataAccess.ExecuteScalar(strSQLModaliltyDul);
                if (obj == DBNull.Value)
                {
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(obj) > 0)
                    {
                        throw new Exception("Decsription can not be duplicated in same modality!");
                    }
                }

                string strSQL = string.Format("Insert into tModalityTimeSlice(TimeSliceGuid, ModalityType, Modality,StartDt,EndDt,Description,MaxNumber,Domain) Values('{0}','{1}','{2}','{3}','{4}',@Description1,'{5}','{6}')",
                    strGuid, strModalityType, strModality, strStartTime, strEndTime, strMaxMan, strDomain);
                dataAccess.Parameters.Add("@Description1", strDescription);
                dataAccess.ExecuteNonQuery(strSQL);
                return true;

            }
            catch (Exception e)
            {
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
        }

        public virtual string AddModalityTimeSliceEx(string strModalityType, string strModality, string strStartTime,
            string strEndTime, string strDescription, string strMaxMan,
            string strDomain, string strDateTypes, string strAvailableDt)
        {
            RisDAL dataAccess = new RisDAL();
            try
            {
                string[] arrDateType = strDateTypes.Split(new char[] { ',' });
                //check if description is duplicated in same modality
                string strSQLModaliltyDul = string.Format("Select dateType from tModalityTimeSlice Where Description =@Description and Modality = '{0}' and AvailableDate='{1}'",
                    strModality, strAvailableDt);

                dataAccess.Parameters.Add("@Description", strDescription);
                DataTable dtTypes = dataAccess.ExecuteQuery(strSQLModaliltyDul);
                DataTable dtModality = dataAccess.ExecuteQuery("select * from tModality where Domain in (select Value from tSystemProfile where name = 'Domain')");
                DataTable dtSiteList = dataAccess.ExecuteQuery("select * from tSiteList where Domain in (select Value from tSystemProfile where name = 'Domain')");

                foreach (DataRow dr in dtTypes.Rows)
                {
                    if (strDateTypes.Contains(Convert.ToString(dr[0])))
                    {
                        throw new DuplicateDescrpException("Decsription can not be duplicated in same modality and DateType");
                        //throw new Exception("Decsription can not be duplicated in same modality and DateType");
                    }
                }

                string guid = "", strSQL = "", modalitySite = "";
                foreach (DataRow dr in dtModality.Rows)
                {
                    if (dr["Modality"].ToString() == strModality)
                    {
                        modalitySite = dr["Site"].ToString();
                        break;
                    }
                }
                dataAccess.BeginTransaction();
                foreach (string curDateType in arrDateType)
                {
                    DataTable temp = dataAccess.ExecuteQuery(string.Format("select 1 from tModalityTimeSlice where Modality = '{0}' and DateType = '{1}' and AvailableDate = '{2}'", strModality, curDateType, strAvailableDt), RisDAL.ConnectionState.KeepOpen);
                    if (temp != null && temp.Rows.Count == 0)
                    {
                        string maxBookingDate = "";
                        if (!IsAvailableDateValid(strModality, Convert.ToDateTime(strAvailableDt), dataAccess, out maxBookingDate))
                        {
                            throw new Exception(maxBookingDate);
                        }
                    }
                    guid = Guid.NewGuid().ToString();
                    dataAccess.Parameters.Clear();
                    strSQL = string.Format("Insert into tModalityTimeSlice(TimeSliceGuid, ModalityType, Modality,StartDt,EndDt,"
                                + "Description,MaxNumber,Domain,DateType,AvailableDate) Values('{0}','{1}','{2}','{3}','{4}',@Description1,'{5}','{6}','{7}','{8}');\r\n",
                        guid, strModalityType, strModality, strStartTime, strEndTime, strMaxMan, strDomain, curDateType, strAvailableDt);

                    if (string.IsNullOrWhiteSpace(modalitySite))
                    {
                        foreach (DataRow dr in dtSiteList.Rows)
                        {
                            strSQL = strSQL + string.Format("insert into tModalityShare values(newid(),'{0}','{1}',1,'{2}','{2}','Default_Hide',null);\r\n", guid, dr["Site"].ToString(), strMaxMan);
                        }
                    }
                    else
                    {
                        strSQL = strSQL + string.Format("insert into tModalityShare values(newid(),'{0}','{1}',1,'{2}','{2}','Default_Hide',null);\r\n", guid, modalitySite, strMaxMan);
                    }
                    DataTable dt2 = dataAccess.ExecuteQuery(string.Format(@"
                        select distinct Date from tModalityShare a 
                        inner join tModalityTimeSlice b on a.TimeSliceGuid = b.TimeSliceGuid 
                        where Modality = '{0}' and DateType ='{1}' 
                            and AvailableDate ='{2}' and Date is not null and Date >= '{3}'; \r\n",
                   strModality, curDateType, strAvailableDt, DateTime.Today.ToString("yyyy-MM-dd")), RisDAL.ConnectionState.KeepOpen);
                    foreach (DataRow dr in dt2.Rows)
                    {
                        strSQL = strSQL + string.Format(@"
                        insert into tModalityShare 
                        select NEWID(),TimeSliceGuid,ShareTarget,TargetType,MaxCount,AvailableCount,GroupId,'{0}' 
                        from tModalityShare 
                        where Date is null and TimeSliceGuid = '{1}'; \r\n", dr["Date"].ToString(), guid);
                    }
                    dataAccess.Parameters.Add("@Description1", strDescription);
                    dataAccess.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                dataAccess.CommitTransaction();
                return guid;

            }
            catch (Exception e)
            {
                dataAccess.RollbackTransaction();
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, e.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw e;
            }
            finally
            {
                if (dataAccess != null)
                {
                    dataAccess.Dispose();
                }
            }
        }

        #endregion
        #region**2.bool ModifyModalityTimeSlice(string strGuid, string strStartTime, string strEndTime,string strDescription, string strMaxMan)
        /// <summary>
        /// Name:ModifyModalityTimeSlice
        /// Function:Modify a ModalityTimeSlice
        /// </summary>
        /// <param name="strGuid">ModalityTimeSlice guid to modify</param>
        /// <param name="strStartTime">StartTime</param>
        /// <param name="strEndTime">EndTime</param>
        /// <param name="strDescription">Description</param>/// 
        /// <returns>True:successful    False:failed</returns>

        public virtual bool ModifyModalityTimeSlice(string strGuid, string strStartTime, string strEndTime, string strDescription, string strMaxMan, string strDateType)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                //check if description is duplicated in same modality
                oKodakDAL.Parameters.Add("@Description", strDescription);
                string strSQLModaliltyDul = string.Format("Select Count(1) from tModalityTimeSlice Where Description=@Description and "
                    + " Modality = (Select Modality from tModalityTimeSlice Where TimeSliceGuid ='{0}') "
                    + " and AvailableDate =(Select top 1 AvailableDate from tModalityTimeSlice Where TimeSliceGuid ='{0}') "
                    + " and TimeSliceGuid <> '{0}' and DateType ='{1}'", strGuid, strDateType);

                object obj = oKodakDAL.ExecuteScalar(strSQLModaliltyDul);
                if (obj == DBNull.Value)
                {
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(obj) > 0)
                    {
                        throw new Exception("Decsription can not be duplicated in same modality!");
                    }
                }
                //check if description is duplicated in same modality
                oKodakDAL.Parameters.Add("@Description1", strDescription);
                string strSQL = string.Format("Update tModalityTimeSlice set StartDt='{0}',EndDt ='{1}', MaxNumber = '{3}', Description=@Description1 where TimeSliceGuid='{2}';\r\n", strStartTime, strEndTime, strGuid, Convert.ToInt32(strMaxMan));
                strSQL += string.Format("update tModalityShare set MaxCount ='{0}', AvailableCount = '{0}' where TimeSliceGuid = '{1}' and Date is null and GroupId = 'Default_Hide';\r\n", Convert.ToInt32(strMaxMan), strGuid);
                strSQL += string.Format("update tModalityShare set AvailableCount = AvailableCount + '{0}' - MaxCount, MaxCount ='{0}' where TimeSliceGuid = '{1}' and Date is not null and Date >= '{2}' and GroupId = 'Default_Hide';\r\n", Convert.ToInt32(strMaxMan), strGuid, DateTime.Today.ToString("yyyy-MM-dd"));

                oKodakDAL.BeginTransaction();
                int count = oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                oKodakDAL.CommitTransaction();
                return true;
            }
            catch (Exception Ex)
            {
                oKodakDAL.RollbackTransaction();
                throw Ex;
                return false;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return false;
        }
        #endregion
        #region**3.bool DeleteModalityTimeSlice(string strGuid)
        /// <summary>
        /// Name:DeleteModalityTimeSlice
        /// Function:Delete a ModalityTimeSlice
        /// </summary>
        /// <param name="strGuid">ModalityTimeSlice guid to find to delete</param>
        /// <returns>True:successful    False:failed</returns>
        public virtual bool DeleteModalityTimeSlice(string strGuid)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int iCount = 0;//deleted record count

            string strSQL = string.Format("delete from tModalityTimeSlice where TimeSliceGuid ='{0}'", strGuid);
            strSQL += string.Format("delete from tModalityShare where TimeSliceGuid = '{0}'", strGuid);
            try
            {
                oKodakDAL.BeginTransaction();
                iCount = oKodakDAL.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                oKodakDAL.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                oKodakDAL.RollbackTransaction();
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
            return false;

        }
        #endregion
        #region**4.bool IsModalityTimeSliceOverLap(string strGuid,string strModalityType, string strModality, string strStartTime, string strEndTime)
        public virtual bool IsModalityTimeSliceOverLap(string strGuid, string strModalityType, string strModality, string strStartTime, string strEndTime, string isModify)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string strSQL = "";
                {
                    if (Convert.ToBoolean(isModify) == false)//add action
                    {
                        strSQL = string.Format("select count(1) from tModalityTimeSlice where ((StartDt < '{1}') and (EndDt > '{0}' )) And ModalityType ='{2}' And Modality ='{3}' ",
                                 strStartTime, strEndTime, strModalityType, strModality);
                    }
                    else
                    {
                        strSQL = string.Format("select count(1) from tModalityTimeSlice where ((StartDt < '{1}') and (EndDt > '{0}' )) And ModalityType ='{2}' And Modality ='{3}' And TimeSliceGuid <> '{4}'",
                                 strStartTime, strEndTime, strModalityType, strModality, strGuid);

                    }

                }
                object obj = oKodakDAL.ExecuteScalar(strSQL);
                if (obj == DBNull.Value)
                {
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(obj) > 0)
                    {
                        return true;
                    }
                }
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
            return false;
        }

        public virtual bool IsModalityTimeSliceOverLap(string strGuid, string strModalityType, string strModality,
            string strStartTime, string strEndTime, string isModify, string strDateType, string strAvailableDate)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string strSQL = "";
                {
                    if (Convert.ToBoolean(isModify) == false)//add action
                    {
                        strSQL = string.Format("select count(1) from tModalityTimeSlice where ((StartDt < '{1}') "
                                + "and (EndDt > '{0}' )) And ModalityType ='{2}' And Modality ='{3}' And DateType = '{4}' and AvailableDate='{5}'",
                                 strStartTime, strEndTime, strModalityType, strModality, strDateType, strAvailableDate);
                    }
                    else
                    {
                        strSQL = string.Format("select count(1) from tModalityTimeSlice where ((StartDt < '{1}')"
                                + "and (EndDt > '{0}' )) And ModalityType ='{2}' And Modality ='{3}' And "
                                + "TimeSliceGuid <> '{4}' And DateType ='{5}' and AvailableDate = '{6}'",
                                 strStartTime, strEndTime, strModalityType, strModality, strGuid, strDateType, strAvailableDate);

                    }

                }
                object obj = oKodakDAL.ExecuteScalar(strSQL);
                if (obj == DBNull.Value)
                {
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(obj) > 0)
                    {
                        return true;
                    }
                }
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
            return false;
        }
        #endregion
        #region**5.DataSet GetModalityTimeSlice(string strModality )
        public virtual DataSet GetModalityTimeSlice(string strModality)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            try
            {
                DataTable dt = new DataTable();
                StringBuilder sb = new StringBuilder();
                string strCondition = string.Format("Select * from tModalityTimeSlice Where Modality='{0}' order by StartDt", strModality);

                DataTable dtShared = oKodakDAL.ExecuteQuery("select a.TimeSliceGuid from tModalityShare a inner join tModalityTimeSlice b on a.TimeSliceGuid = b.TimeSliceGuid and GroupId <> 'Default_Hide' and Date is null and Modality='" + strModality + "'");
                dt = oKodakDAL.ExecuteQuery(strCondition);
                if (dt != null)
                {
                    dt.TableName = "tModalityTimeSlice";
                    dt.Columns.Add("IsShared");
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow[] drs = dtShared.Select("TimeSliceGuid='" + dr["TimeSliceGuid"].ToString() + "'");
                        if (drs.Length > 0)
                        {
                            dr["IsShared"] = "1";
                        }
                        else
                        {
                            dr["IsShared"] = "0";
                        }
                    }
                    ds.Tables.Add(dt);
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
            }
            return ds;
        }
        #endregion

        #region**６.DataSet GetModalityTimeSliceOverLapGuids(string strGuid,string strModalityType, string strModality, string strStartTime, string strEndTime)
        public virtual DataSet GetModalityTimeSliceOverLapGuids(string strGuid, string strModalityType, string strModality, string strStartTime, string strEndTime, string isModify)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "";
                {
                    if (Convert.ToBoolean(isModify) == false)//add action
                    {
                        strSQL = string.Format("select TimeSliceGuid from tModalityTimeSlice where ((StartDt < '{1}') and (EndDt > '{0}' )) And ModalityType ='{2}' And Modality ='{3}' ",
                                 strStartTime, strEndTime, strModalityType, strModality);
                    }
                    else
                    {
                        strSQL = string.Format("select TimeSliceGuid from tModalityTimeSlice where ((StartDt < '{1}') and (EndDt > '{0}' )) And ModalityType ='{2}' And Modality ='{3}' And TimeSliceGuid <> '{4}'",
                                 strStartTime, strEndTime, strModalityType, strModality, strGuid);

                    }

                }
                dt = oKodakDAL.ExecuteQuery(strSQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                    return ds;
                }
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
            return null;
        }

        public virtual DataSet GetModalityTimeSliceOverLapGuids(string strGuid, string strModalityType, string strModality,
            string strStartTime, string strEndTime, string isModify, string strDateType, string strAvailableDt)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                string strSQL = "";
                {
                    if (Convert.ToBoolean(isModify) == false)//add action
                    {
                        //strSQL = string.Format("select TimeSliceGuid from tModalityTimeSlice where ((StartDt Between '{0}' And '{1}') OR (EndDt Between '{0}' And '{1}')) And ModalityType ='{2}' And Modality ='{3}' And DateType ='{4}'",
                        strSQL = string.Format("select TimeSliceGuid from tModalityTimeSlice where ((StartDt < '{1}') and (EndDt > '{0}' )) "
                            + "And ModalityType ='{2}' And Modality ='{3}' And DateType ='{4}' and AvailableDate= '{5}'",
                                 strStartTime, strEndTime, strModalityType, strModality, strDateType, strAvailableDt);
                    }
                    else
                    {
                        strSQL = string.Format("select TimeSliceGuid from tModalityTimeSlice where ((StartDt < '{1}') and (EndDt > '{0}' )) "
                            + "And ModalityType ='{2}' And Modality ='{3}' And TimeSliceGuid <> '{4}' And DateType ='{4}' and AvailableDate= '{5}'",
                                 strStartTime, strEndTime, strModalityType, strModality, strGuid, strDateType, strAvailableDt);

                    }

                }
                dt = oKodakDAL.ExecuteQuery(strSQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ds.Tables.Add(dt);
                    return ds;
                }
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
            return null;
        }
        #endregion

        public virtual void BulkAddModalityTimeSlice(string strModalityType, string strModality, string startTime, string endTime,
            string strMaxNumber, string strDomain, string strInterval, string strDateTypes, string strAvailableDt)
        {
            RisDAL oKodak = new RisDAL();
            try
            {
                DateTime dtStart, dtEnd;
                if (!DateTime.TryParse(startTime, out dtStart))
                {
                    throw new Exception("startTime is not correct datetime format!");
                }
                if (!DateTime.TryParse(endTime, out dtEnd))
                {
                    throw new Exception("endTime is not correct datetime format!");
                }

                string[] arrDateType = strDateTypes.Split(new char[] { ',' });

                string strDescription = "", strSQL = "", modalitySite = "";
                int interval = Convert.ToInt32(strInterval);
                DateTime dtSliceStart = dtStart;
                DateTime dtSliceEnd = dtStart;
                DataTable dtModality = oKodak.ExecuteQuery("select * from tModality where Domain in (select Value from tSystemProfile where name = 'Domain')");
                DataTable dtSiteList = oKodak.ExecuteQuery("select * from tSiteList where Domain in (select Value from tSystemProfile where name = 'Domain')");

                foreach (DataRow dr in dtModality.Rows)
                {
                    if (dr["Modality"].ToString() == strModality)
                    {
                        modalitySite = dr["Site"].ToString();
                        break;
                    }
                }

                oKodak.BeginTransaction();
                int i = 1000;
                while (i > 0)
                {
                    if (DateTime.Compare(dtSliceEnd, dtEnd) >= 0)
                        break;
                    dtSliceEnd = dtSliceStart.AddMinutes(interval);
                    if (dtSliceEnd > dtEnd)
                    {
                        dtSliceEnd = dtEnd;//Add a timeslice using the left time 
                    }
                    strDescription = dtSliceStart.ToString("HH:mm") + "-" + dtSliceEnd.ToString("HH:mm");
                    foreach (string curDateType in arrDateType)
                    {
                        DataTable temp = oKodak.ExecuteQuery(string.Format("select 1 from tModalityTimeSlice where Modality = '{0}' and DateType = '{1}' and AvailableDate = '{2}'", strModality, curDateType, strAvailableDt), RisDAL.ConnectionState.KeepOpen);
                        if (temp != null && temp.Rows.Count == 0)
                        {
                            string maxBookingDate = "";
                            if (!IsAvailableDateValid(strModality, Convert.ToDateTime(strAvailableDt), oKodak, out maxBookingDate))
                            {
                                throw new Exception(maxBookingDate);
                            }
                        }
                        oKodak.Parameters.Clear();
                        string guid = Guid.NewGuid().ToString();
                        strSQL = string.Format("Insert into tModalityTimeSlice("
                        + "TimeSliceGuid, ModalityType, Modality,StartDt,EndDt,Description,MaxNumber,Domain,DateType,AvailableDate) "
                        + "Values('{0}','{1}','{2}',@StartDt,@EndDt,@Description,'{5}','{6}','{7}','{8}')",
                            guid, strModalityType, strModality, dtSliceStart, dtSliceEnd,
                            strMaxNumber, strDomain, curDateType, strAvailableDt);
                        if (string.IsNullOrWhiteSpace(modalitySite))
                        {
                            foreach (DataRow dr in dtSiteList.Rows)
                            {
                                strSQL = strSQL + string.Format("insert into tModalityShare values(newid(),'{0}','{1}',1,'{2}','{2}','Default_Hide',null);\r\n", guid, dr["Site"].ToString(), strMaxNumber);
                            }
                        }
                        else
                        {
                            strSQL = strSQL + string.Format("insert into tModalityShare values(newid(),'{0}','{1}',1,'{2}','{2}','Default_Hide',null);\r\n", guid, modalitySite, strMaxNumber);
                        }
                        DataTable dt2 = oKodak.ExecuteQuery(string.Format("select distinct Date from tModalityShare a inner join tModalityTimeSlice b on a.TimeSliceGuid = b.TimeSliceGuid where Modality = '{0}' and DateType ='{1}' and AvailableDate ='{2}' and Date is not null and Date >= '{3}'; \r\n", strModality, curDateType, strAvailableDt, DateTime.Today.ToString("yyyy-MM-dd")), RisDAL.ConnectionState.KeepOpen);
                        foreach (DataRow dr in dt2.Rows)
                        {
                            strSQL = strSQL + string.Format("insert into tModalityShare select NEWID(),TimeSliceGuid,ShareTarget,TargetType,MaxCount,AvailableCount,GroupId,'{0}' from tModalityShare where Date is null and TimeSliceGuid = '{1}'; \r\n", dr["Date"].ToString(), guid);
                        }
                        oKodak.Parameters.Add("@Description", strDescription);
                        oKodak.Parameters.AddDateTime("@StartDt", dtSliceStart);
                        oKodak.Parameters.AddDateTime("@EndDt", dtSliceEnd);
                        oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                    }
                    dtSliceStart = dtSliceEnd;
                    //dtSliceEnd = dtSliceStart.AddMinutes(interval);
                    i--;
                }
                oKodak.CommitTransaction();
            }
            catch (Exception ex)
            {
                oKodak.RollbackTransaction();
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(),
                        (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw ex;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
        }

        public virtual void UpdateAvailableTime(string strModalityType, string strModality, string availabeDate, string exAvailableDt, string dateType)
        {
            try
            {
                string checkExitAvbTime = @"select 1 from tModalityTimeSlice where ModalityType= @Modalitytype and Modality = @Modality
                                            and AvailableDate =@AvailableDate and DateType = @dateType";
                using (RisDAL oKodak = new RisDAL())
                {
                    DateTime dtAvailable, dtExAvailable;
                    if (!DateTime.TryParse(availabeDate, out dtAvailable))
                    {
                        throw new Exception("Available Date is not correct datetime format!");
                    }
                    if (!DateTime.TryParse(exAvailableDt, out dtExAvailable))
                    {
                        throw new Exception("Available Date is not correct datetime format!");
                    }

                    string maxBookingDate = "";
                    if (!IsAvailableDateValid(strModality, dtAvailable, oKodak, out maxBookingDate))
                    {
                        throw new Exception(maxBookingDate);
                    }

                    oKodak.Parameters.Add("@Modalitytype", strModalityType);
                    oKodak.Parameters.Add("@Modality", strModality);
                    oKodak.Parameters.AddDateTime("@AvailableDate", dtAvailable);
                    oKodak.Parameters.Add("@DateType", dateType);
                    var result = oKodak.ExecuteScalar(checkExitAvbTime);
                    if (result != null)
                    {
                        throw new Exception("AvailableDateExist");
                    }
                    string sqlUpdate = "Update tModalityTimeSlice set AvailableDate= @AvailableDate "
                                + "where ModalityType= @Modalitytype and Modality = @Modality and AvailableDate =@ExAvailbledate ";

                    oKodak.Parameters.Clear();
                    oKodak.Parameters.Add("@Modalitytype", strModalityType);
                    oKodak.Parameters.Add("@Modality", strModality);
                    oKodak.Parameters.AddDateTime("@AvailableDate", dtAvailable);
                    oKodak.Parameters.AddDateTime("@ExAvailbledate", dtExAvailable);

                    oKodak.ExecuteNonQuery(sqlUpdate);

                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw ex;
            }
        }

        private bool IsAvailableDateValid(string strModality, DateTime AvailableDate, RisDAL oKodak, out string maxBookingDate)
        {
            object maxDate = oKodak.ExecuteScalar(string.Format("select MAX(BookingBeginDt) from tRegProcedure a where Modality = '{0}' and Status = 10", strModality), RisDAL.ConnectionState.KeepOpen);
            if (maxDate != null && maxDate != DBNull.Value)
            {
                maxBookingDate = (Convert.ToDateTime(maxDate)).ToString("yyyy-MM-dd");
                return AvailableDate > Convert.ToDateTime(maxDate);
            }
            else
            {
                maxBookingDate = "";
                return true;
            }
        }

        public DataSet GetShareSettings(string timeSliceGuid)
        {
            RisDAL oKodakDAL = new RisDAL();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            string strCondition = string.Format("Select a.*, b.Alias as ShareTargetDesc from tModalityShare a left join tSiteList b on a.ShareTarget = b.Site and a.TargetType = 1 Where TimeSliceGuid='{0}' and Date is null and GroupId <> 'Default_Hide' order by GroupId", timeSliceGuid);
            try
            {
                dt = oKodakDAL.ExecuteQuery(strCondition);
                if (dt != null)
                {
                    dt.TableName = "ModalityShare";
                    ds.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 1, ex.Message, Application.StartupPath.ToString(),
                       (new System.Diagnostics.StackFrame(0, true)).GetFileName(), (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                throw new Exception(ex.Message);
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

        public bool SaveModalityShare(string timeSliceGuids, DataSet model)
        {
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string[] guids = timeSliceGuids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                timeSliceGuids = "'" + timeSliceGuids.Replace(",", "','") + "'";
                StringBuilder sql = new StringBuilder();
                sql.Append(string.Format("delete from tModalityShare WITH (HOLDLOCK) where TimeSliceGuid in ({0}) and Date is null; \r\n", timeSliceGuids));

                string strModality = "", strMaxNumber = "", modalitySite = "";
                DataTable dtModality = oKodakDAL.ExecuteQuery("select * from tModality where Domain in (select Value from tSystemProfile where name = 'Domain')");
                DataTable dtSiteList = oKodakDAL.ExecuteQuery("select * from tSiteList where Domain in (select Value from tSystemProfile where name = 'Domain')");

                if (model.Tables[0].Rows.Count == 0)
                {//clear user add records and insert default hide records
                    foreach (string guid in guids)
                    {
                        DataTable temp = oKodakDAL.ExecuteQuery(string.Format("select * from tModalityTimeSlice where TimeSliceGuid = '{0}'", guid));
                        strModality = temp.Rows[0]["Modality"].ToString();
                        strMaxNumber = temp.Rows[0]["MaxNumber"].ToString();
                        foreach (DataRow dr in dtModality.Rows)
                        {
                            if (dr["Modality"].ToString() == strModality)
                            {
                                modalitySite = dr["Site"].ToString();
                                break;
                            }
                        }
                        if (string.IsNullOrWhiteSpace(modalitySite))
                        {
                            foreach (DataRow dr in dtSiteList.Rows)
                            {
                                sql.Append(string.Format("insert into tModalityShare values(newid(),'{0}','{1}',1,'{2}','{2}','Default_Hide',null);\r\n", guid, dr["Site"].ToString(), strMaxNumber));
                            }
                        }
                        else
                        {
                            sql.Append(string.Format("insert into tModalityShare values(newid(),'{0}','{1}',1,'{2}','{2}','Default_Hide',null);\r\n", guid, modalitySite, strMaxNumber));
                        }
                        //update records with Date is not null
                        DataTable dt1 = oKodakDAL.ExecuteQuery(string.Format(@"
                                            select distinct Date 
                                            from tModalityShare 
                                            where TimeSliceGuid = '{0}' 
                                            and Date is not null 
                                            and Date >= '{1}' 
                                            and Date not in (select distinct Date 
                                                             from tModalityShare 
                                                             where TimeSliceGuid = '{0}' 
                                                                and Date is not null 
                                                                and Date >= '{1}' 
                                                                and MaxCount <> AvailableCount); \r\n"
                        , guid, DateTime.Today.ToString("yyyy-MM-dd")));

                        sql.Append(string.Format(@"
                            delete from tModalityShare 
                            where TimeSliceGuid = '{0}' 
                            and Date is not null and Date >= '{1}' 
                            and Date not in (select distinct Date 
                                             from tModalityShare 
                                             where TimeSliceGuid = '{0}' 
                                             and Date is not null 
                                             and Date >= '{1}' and MaxCount <> AvailableCount); \r\n",
                        guid, DateTime.Today.ToString("yyyy-MM-dd")));
                        foreach (DataRow dr in dt1.Rows)
                        {
                            sql.Append(string.Format(@"
                                insert into tModalityShare 
                                select NEWID(),TimeSliceGuid,ShareTarget,TargetType,MaxCount,AvailableCount,GroupId,'{0}' 
                                from tModalityShare where Date is null and TimeSliceGuid = '{1}'; \r\n",
                            dr["Date"].ToString(), guid));
                        }
                    }
                }
                else
                {
                    foreach (string guid in guids)
                    {
                        Dictionary<string, string> gID = new Dictionary<string, string>();
                        foreach (DataRow dr in model.Tables[0].Rows)
                        {
                            if (string.IsNullOrWhiteSpace(dr["GroupId"].ToString()))
                            {
                                sql.Append(string.Format("insert into tModalityShare values(NEWID(),'{0}','{1}',{2},{3},{4},'{5}',null); \r\n",
                                           guid, dr["ShareTarget"].ToString(), dr["TargetType"], dr["MaxCount"], dr["MaxCount"], ""));
                            }
                            else
                            {
                                if (!gID.ContainsKey(dr["GroupId"].ToString()))
                                {
                                    gID.Add(dr["GroupId"].ToString(), Guid.NewGuid().ToString());
                                }
                                sql.Append(string.Format("insert into tModalityShare values(NEWID(),'{0}','{1}',{2},{3},{4},'{5}',null); \r\n",
                                    guid, dr["ShareTarget"].ToString(), dr["TargetType"], dr["MaxCount"], dr["MaxCount"], gID[dr["GroupId"].ToString()]));
                            }
                        }
                        //update records with Date is not null
                        DataTable dt1 = oKodakDAL.ExecuteQuery(string.Format("select distinct Date from tModalityShare where TimeSliceGuid = '{0}' and Date is not null and Date >= '{1}' and Date not in (select distinct Date from tModalityShare where TimeSliceGuid = '{0}' and Date is not null and Date >= '{1}' and MaxCount <> AvailableCount); \r\n", guid, DateTime.Today.ToString("yyyy-MM-dd")));
                        sql.Append(string.Format("delete from tModalityShare where TimeSliceGuid = '{0}' and Date is not null and Date >= '{1}' and Date not in (select distinct Date from tModalityShare where TimeSliceGuid = '{0}' and Date is not null and Date >= '{1}' and MaxCount <> AvailableCount); \r\n", guid, DateTime.Today.ToString("yyyy-MM-dd")));
                        foreach (DataRow dr in dt1.Rows)
                        {
                            sql.Append(string.Format("insert into tModalityShare select NEWID(),TimeSliceGuid,ShareTarget,TargetType,MaxCount,AvailableCount,GroupId,'{0}' from tModalityShare where Date is null and TimeSliceGuid = '{1}'; \r\n", dr["Date"].ToString(), guid));
                        }
                    }
                }

                oKodakDAL.BeginTransaction();
                int count = oKodakDAL.ExecuteNonQuery(sql.ToString(), RisDAL.ConnectionState.KeepOpen);
                oKodakDAL.CommitTransaction();
                return true;
            }
            catch (Exception Ex)
            {
                oKodakDAL.RollbackTransaction();
                throw Ex;
                return false;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return false;
        }

        public bool LockModalityQuota(string parameters, out string lockGuid)
        {
            lockGuid = "";
            RisDAL oKodakDAL = new RisDAL();
            try
            {
                string sql = "";
                string modality = CommonGlobalSettings.Utilities.GetParameter("Modality", parameters);
                string dateType = CommonGlobalSettings.Utilities.GetParameter("DateType", parameters);
                string availableDate = CommonGlobalSettings.Utilities.GetParameter("AvailableDate", parameters);
                string bookingDate = CommonGlobalSettings.Utilities.GetParameter("BookingDate", parameters);
                string timeSliceGuid = CommonGlobalSettings.Utilities.GetParameter("TimeSliceGuid", parameters);
                string bookingSite = CommonGlobalSettings.Utilities.GetParameter("BookingSite", parameters);
                bool unlock = CommonGlobalSettings.Utilities.GetParameter("unlock", parameters) == "1";
                string guid = CommonGlobalSettings.Utilities.GetParameter("Guid", parameters);
                int count = 0;
                oKodakDAL.Parameters.Add("@Modality", modality);
                oKodakDAL.Parameters.Add("@DateType", dateType);
                oKodakDAL.Parameters.Add("@AvailableDate", availableDate);
                oKodakDAL.Parameters.Add("@BookingDate", bookingDate);
                oKodakDAL.Parameters.Add("@TimeSliceGuid", timeSliceGuid);
                oKodakDAL.Parameters.Add("@BookingSite", bookingSite);
                if (unlock)
                {
                    oKodakDAL.Parameters.Add("@UnlockGuid", guid);
                }
                else
                {
                    oKodakDAL.Parameters.Add("@UnlockGuid", "");
                }
                oKodakDAL.Parameters.Add("@LockGuid", DbType.String, 256);
                oKodakDAL.Parameters["@LockGuid"].Direction = ParameterDirection.Output;
                oKodakDAL.Parameters.AddInt("@cnt", ParameterDirection.Output);

                oKodakDAL.ExecuteNonQuerySP("usp_LockModalityQuota");
                count = Convert.ToInt32(oKodakDAL.Parameters["@cnt"].Value);
                lockGuid = Convert.ToString(oKodakDAL.Parameters["@LockGuid"].Value);
                if (count > 0)
                {
                    return true;
                }
            }
            catch (Exception Ex)
            {
                oKodakDAL.RollbackTransaction();
                throw Ex;
                return false;
            }
            finally
            {
                if (oKodakDAL != null)
                {
                    oKodakDAL.Dispose();
                }
            }
            return false;
        }

        #endregion

        #region lock
        public virtual bool QueryLock(string stOwner, string stBeginTime, string stEndTime, DataSet ds, ref string strError)
        {

            bool bReturn = true;
            DataTable dt = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {

                //  string strSQL = string.Format("SELECT  tSync.*,tSync.CreateDt as LockTime, tuser.Localname as  OwnerLocalName, tuser.loginname as OwnerLoginName, isnull((select top 1 cast(isOnline as varchar(16)) from tOnlineClient c where c.userGuid=tUser.userGuid), '0') as isOnline from tSync INNER JOIN tuser");
                //  strSQL += string.Format(" ON  (PATINDEX('%{0}%', tSync.Owner) >0) AND (tUser.UserGuid = SUBSTRING(tSync.Owner,PATINDEX('%{0}%', tSync.Owner),LEN('{0}')))", stOwner);
                string strSQL = string.Format("SELECT  tSync.*,tSync.CreateDt as LockTime from tSync where 1=1 ");
                if (stOwner.Trim().Length > 0)
                {
                    //strSQL += string.Format(" and (PATINDEX('%{0}%', tSync.Owner) >0)", stOwner);
                    strSQL += string.Format(" and tSync.Owner ='{0}'", stOwner);
                }
                if (stBeginTime.Length > 0)
                {
                    strSQL += string.Format(" and CreateDt >= '{0} 00:00:00' ", stBeginTime);
                }
                if (stEndTime.Length > 0)
                {
                    strSQL += string.Format(" and CreateDt <= '{0} 23:59:59'", stEndTime);
                }

                strSQL += string.Format(" order by LockTime DESC");


                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "Sync";
                dt.Columns.Add("OwnerLocalName", typeof(string));
                dt.Columns.Add("OwnerLoginName", typeof(string));
                dt.Columns.Add("IsOnline", typeof(string));
                Char[] sep = { '|' };
                foreach (DataRow dr in dt.Rows)
                {
                    string strOwnerGuids = Convert.ToString(dr["Owner"]);
                    string[] ownerguids = strOwnerGuids.Split(sep);
                    string localName = "";
                    string loginName = "";
                    foreach (string strOwnerguid in ownerguids)
                    {
                        strSQL = string.Format("select LocalName,loginname from tUser where userguid='{0}'", strOwnerguid);
                        DataTable dtUser = oKodak.ExecuteQuery(strSQL);
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {
                            localName = localName + ((dtUser.Rows[0]["LocalName"] == null) ? "" : (dtUser.Rows[0]["LocalName"].ToString()));
                            localName += "|";
                            loginName = loginName + ((dtUser.Rows[0]["LoginName"] == null) ? "" : (dtUser.Rows[0]["LoginName"].ToString()));
                            loginName += "|";
                        }

                        strSQL = string.Format("select count(*) from tOnlineClient where userguid='{0}' and IsOnline=1", strOwnerguid);
                        Object obj = oKodak.ExecuteScalar(strSQL);
                        if (obj == null || Convert.ToInt32(obj) == 0)
                        {
                            dr["IsOnline"] = "0";
                        }
                        else
                        {
                            dr["IsOnline"] = "1";
                        }
                    }
                    localName = localName.TrimEnd('|');
                    loginName = loginName.TrimEnd('|');

                    dr["OwnerLocalName"] = localName;
                    dr["OwnerLoginName"] = loginName;
                }



                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public virtual bool UnLock(int nObjectType, int nSyncType, string strObjectGuid, string strOwner, ref string strError)
        {

            bool bReturn = true;
            RisDAL oKodak = new RisDAL();


            try
            {
                string strSQL = "";
                strObjectGuid = strObjectGuid.Trim();

                if (strObjectGuid.Length > 0)
                {

                    strSQL = string.Format("DELETE FROM tSync WHERE SyncType={0} and Owner='{1}' and Guid='{2}'", nSyncType, strOwner, strObjectGuid);


                }
                else
                //no GUID is input, all records belong to this owner will be unlocked
                {
                    strSQL = string.Format("DELETE FROM tSync WHERE SyncType={0} AND Owner='{1}'", nSyncType, strOwner);
                }

                oKodak.ExecuteNonQuery(strSQL);


            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {

                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;

        }
        #endregion


        #region online user
        public virtual bool QueryOnline(DataSet ds, ref string strError)
        {

            bool bReturn = true;
            DataTable dt = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {

                string strSQL = "SELECT A.UserGuid, A.MachineIP,A.RoleName,A.IISUrl,A.LoginTime,A.Comments,A.SessionID,cast(A.IsOnline as nvarchar(8)) as IsOnline,A.IsOnline as offline,A.Site,A.[Domain],B.LocalName,B.LoginName FROM tOnlineClient A left join tUser B on A.UserGuid=B.UserGuid where A.IsOnline = 1";
                dt = oKodak.ExecuteQuery(strSQL);


                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public virtual bool SetOffline(string strUserGuid, ref string strError)
        {

            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {
                List<string> listSQL = new List<string>();
                string strSQL = string.Format("Update dbo.tOnlineClient Set IsOnline = 0 where UserGuid='{0}'", strUserGuid);
                listSQL.Add(strSQL);
                strSQL = string.Format("Delete from dbo.tSync where Owner = '{0}'", strUserGuid.ToString());
                listSQL.Add(strSQL);


                oKodak.BeginTransaction();
                foreach (string str in listSQL)
                {
                    oKodak.ExecuteNonQuery(str, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();



            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {

                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;

        }
        #endregion
        #region domain
        public virtual bool DomainList(DataSet ds, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {


                //Get domain list
                string strSQL = "SELECT Domain,DomainPrefix,Connstring,FtpServer,FtpPort,FtpUser,FtpPassword,PacsAETitle,Telephone,Address,PacsServer,PacsWebServer,tab,Alias,IISURL FROM dbo.tDomainList";
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "DomainList";
                ds.Tables.Add(dt);

                //Get Site list
                strSQL = "select Site,Domain,DomainPrefix,Connstring,FtpServer,FtpPort,FtpUser,FtpPassword,PacsAETitle,Telephone,Address,PacsServer,PacsWebServer,Tab,Alias,IISUrl from tSiteList";
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "SiteList";
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.DataCenter_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }
        public virtual bool AddDomain(DataTable dtDomain, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {
                string strDomain = dtDomain.Rows[0]["Domain"] as string;
                if (strDomain.Trim().Length == 0)
                {
                    throw new Exception("Invalid parameter");
                }
                string strSQL = string.Format("select count(*) from tDomainList where Domain='{0}'", strDomain);
                Object obj = oKodak.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    throw new Exception("Exists this domain, save failure");
                }

                string strDomainPrefix = dtDomain.Rows[0]["DomainPrefix"] as string;
                if (!string.IsNullOrEmpty(strDomainPrefix))
                {
                    strSQL = string.Format("select count(*) from tDomainList where DomainPrefix='{0}'", strDomainPrefix);
                    obj = oKodak.ExecuteScalar(strSQL);
                    if (obj != null && Convert.ToInt32(obj) > 0)
                    {
                        throw new Exception("Exists this domain prefix, save failure");
                    }
                }

                strSQL = string.Format("select count(*) from tDomainList where Alias='{0}'", dtDomain.Rows[0]["Alias"].ToString());
                obj = oKodak.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    throw new Exception("This Domain alias has been used");
                }

                strSQL = string.Format("INSERT INTO dbo.tDomainList([Domain],DomainPrefix,Connstring,FtpServer,FtpPort,FtpUser,FtpPassword,PacsAETitle,Telephone,Address,PacsServer,PacsWebServer,Alias,Tab) Values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',0)",
                    strDomain, Convert.ToString(dtDomain.Rows[0]["DomainPrefix"]), Convert.ToString(dtDomain.Rows[0]["Connstring"]), Convert.ToString(dtDomain.Rows[0]["FtpServer"]), Convert.ToInt32(dtDomain.Rows[0]["FtpPort"]), Convert.ToString(dtDomain.Rows[0]["FtpUser"]),
                    Convert.ToString(dtDomain.Rows[0]["FtpPassword"]), Convert.ToString(dtDomain.Rows[0]["PacsAETitle"]), Convert.ToString(dtDomain.Rows[0]["Telephone"]), Convert.ToString(dtDomain.Rows[0]["Address"]), Convert.ToString(dtDomain.Rows[0]["PacsServer"]), Convert.ToString(dtDomain.Rows[0]["PacsWebServer"]), Convert.ToString(dtDomain.Rows[0]["Alias"]));

                oKodak.ExecuteNonQuery(strSQL);


            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.DataCenter_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


            }
            finally
            {

                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }
        public virtual bool ModifyDomain(DataTable dtDomain, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {
                string strDomain = dtDomain.Rows[0]["Domain"] as string;
                if (strDomain.Trim().Length == 0)
                {
                    throw new Exception("Invalid parameter");
                }
                string strSQL = string.Format("select count(*) from tDomainList where Domain='{0}'", strDomain);
                Object obj = oKodak.ExecuteScalar(strSQL);
                if (obj == null || Convert.ToInt32(obj) == 0)
                {
                    throw new Exception("Does not exist this domain, modify failure");
                }

                string strDomainPrefix = dtDomain.Rows[0]["DomainPrefix"] as string;
                if (!string.IsNullOrEmpty(strDomainPrefix))
                {
                    strSQL = string.Format("select count(*) from tDomainList where DomainPrefix='{0}' and Domain!='{1}'", strDomainPrefix, strDomain);
                    obj = oKodak.ExecuteScalar(strSQL);
                    if (obj != null && Convert.ToInt32(obj) > 0)
                    {
                        throw new Exception("Exists this domain prefix, modify failure");
                    }
                }

                strSQL = string.Format("select count(*) from tDomainList where Alias='{0}' and Domain <> '{1}'", dtDomain.Rows[0]["Alias"].ToString(), strDomain);
                obj = oKodak.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    throw new Exception("This Domain alias has been used");
                }

                strSQL = string.Format("Update dbo.tDomainList set DomainPrefix='{0}',Connstring='{1}',FtpServer='{2}',FtpPort={3},FtpUser='{4}',FtpPassword='{5}',PacsAETitle='{6}',Telephone='{7}',Address='{8}',PacsServer='{9}',PacsWebServer='{10}',Alias='{11}' where Domain='{12}'",
                    Convert.ToString(dtDomain.Rows[0]["DomainPrefix"]), Convert.ToString(dtDomain.Rows[0]["Connstring"]), Convert.ToString(dtDomain.Rows[0]["FtpServer"]), Convert.ToInt32(dtDomain.Rows[0]["FtpPort"]), Convert.ToString(dtDomain.Rows[0]["FtpUser"]),
                    Convert.ToString(dtDomain.Rows[0]["FtpPassword"]), Convert.ToString(dtDomain.Rows[0]["PacsAETitle"]), Convert.ToString(dtDomain.Rows[0]["Telephone"]), Convert.ToString(dtDomain.Rows[0]["Address"]), Convert.ToString(dtDomain.Rows[0]["PacsServer"]), Convert.ToString(dtDomain.Rows[0]["PacsWebServer"]), Convert.ToString(dtDomain.Rows[0]["Alias"]), strDomain);

                oKodak.ExecuteNonQuery(strSQL);


            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.DataCenter_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


            }
            finally
            {

                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }
        public virtual bool DelDomain(string strDomain, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {

                if (strDomain.Trim().Length == 0)
                {
                    throw new Exception("Invalid parameter");
                }

                string strSQL = string.Format("Delete from  dbo.tDomainList where [Domain]='{0}'", strDomain);

                oKodak.ExecuteNonQuery(strSQL);


            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.DataCenter_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public virtual bool AddSite(DataTable dtSite, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {
                string strSite = dtSite.Rows[0]["Site"].ToString();
                if (strSite.Trim().Length == 0)
                {
                    throw new Exception("Invalid parameter");
                }
                string strSQL = string.Format("select count(*) from tSiteList where Site='{0}'", strSite);
                Object obj = oKodak.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    throw new Exception("Exists this site, save failure");
                }

                strSQL = string.Format("select count(*) from tSiteList where Alias='{0}'", dtSite.Rows[0]["Alias"].ToString());
                obj = oKodak.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    throw new Exception("This site alias has been used");
                }

                //string strDomainPrefix = dtDomain.Rows[0]["DomainPrefix"] as string;
                //if (!string.IsNullOrEmpty(strDomainPrefix))
                //{
                //    strSQL = string.Format("select count(*) from tDomainList where DomainPrefix='{0}'", strDomainPrefix);
                //    obj = oKodak.ExecuteScalar(strSQL);
                //    if (obj != null && Convert.ToInt32(obj) > 0)
                //    {
                //        throw new Exception("Exists this domain prefix, save failure");
                //    }
                //}

                strSQL = string.Format("INSERT INTO dbo.tSiteList([Domain],DomainPrefix,Connstring,FtpServer,FtpPort,FtpUser,FtpPassword,PacsAETitle,Telephone,Address,PacsServer,PacsWebServer,Site,Alias,Tab) Values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',0)",
                    Convert.ToString(dtSite.Rows[0]["Domain"]), Convert.ToString(dtSite.Rows[0]["DomainPrefix"]), Convert.ToString(dtSite.Rows[0]["Connstring"]), Convert.ToString(dtSite.Rows[0]["FtpServer"]), Convert.ToInt32(dtSite.Rows[0]["FtpPort"]), Convert.ToString(dtSite.Rows[0]["FtpUser"]),
                    Convert.ToString(dtSite.Rows[0]["FtpPassword"]), Convert.ToString(dtSite.Rows[0]["PacsAETitle"]), Convert.ToString(dtSite.Rows[0]["Telephone"]), Convert.ToString(dtSite.Rows[0]["Address"]), Convert.ToString(dtSite.Rows[0]["PacsServer"]), Convert.ToString(dtSite.Rows[0]["PacsWebServer"]), strSite, Convert.ToString(dtSite.Rows[0]["Alias"]));

                oKodak.ExecuteNonQuery(strSQL);


            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.DataCenter_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


            }
            finally
            {

                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public virtual bool ModifySite(DataTable dtSite, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {
                string strSite = dtSite.Rows[0]["Site"] as string;
                if (strSite.Trim().Length == 0)
                {
                    throw new Exception("Invalid parameter");
                }
                string strSQL = string.Format("select count(*) from tSiteList where Site='{0}'", strSite);
                Object obj = oKodak.ExecuteScalar(strSQL);
                if (obj == null || Convert.ToInt32(obj) == 0)
                {
                    throw new Exception("Does not exist this site, modify failure");
                }

                strSQL = string.Format("select count(*) from tSiteList where Alias='{0}' and Site <> '{1}'", dtSite.Rows[0]["Alias"].ToString(), strSite);
                obj = oKodak.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    throw new Exception("This site alias has been used");
                }
                //string strDomainPrefix = dtDomain.Rows[0]["DomainPrefix"] as string;
                //if (!string.IsNullOrEmpty(strDomainPrefix))
                //{
                //    strSQL = string.Format("select count(*) from tDomainList where DomainPrefix='{0}' and Domain!='{1}'", strDomainPrefix, strDomain);
                //    obj = oKodak.ExecuteScalar(strSQL);
                //    if (obj != null && Convert.ToInt32(obj) > 0)
                //    {
                //        throw new Exception("Exists this domain prefix, modify failure");
                //    }
                //}

                strSQL = string.Format("Update dbo.tSiteList set DomainPrefix='{0}',Connstring='{1}',FtpServer='{2}',FtpPort={3},FtpUser='{4}',FtpPassword='{5}',PacsAETitle='{6}',Telephone='{7}',Address='{8}',PacsServer='{9}',PacsWebServer='{10}',Alias='{11}' where Site='{12}'",
                    Convert.ToString(dtSite.Rows[0]["DomainPrefix"]), Convert.ToString(dtSite.Rows[0]["Connstring"]), Convert.ToString(dtSite.Rows[0]["FtpServer"]), Convert.ToInt32(dtSite.Rows[0]["FtpPort"]), Convert.ToString(dtSite.Rows[0]["FtpUser"]),
                    Convert.ToString(dtSite.Rows[0]["FtpPassword"]), Convert.ToString(dtSite.Rows[0]["PacsAETitle"]), Convert.ToString(dtSite.Rows[0]["Telephone"]), Convert.ToString(dtSite.Rows[0]["Address"]), Convert.ToString(dtSite.Rows[0]["PacsServer"]), Convert.ToString(dtSite.Rows[0]["PacsWebServer"]), Convert.ToString(dtSite.Rows[0]["Alias"]), strSite);

                oKodak.ExecuteNonQuery(strSQL);


            }
            catch (Exception ex)
            {
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.DataCenter_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


            }
            finally
            {

                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public virtual bool SyncDomainSiteList(DataSet ds, ref string strError)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            string strSQL = "";

            try
            {
                oKodak.BeginTransaction();
                //update domain list
                strSQL = " delete from tDomainList ";
                foreach (DataRow row in ds.Tables["DomainList"].Rows)
                {
                    strSQL += string.Format(" INSERT INTO dbo.tDomainList([Domain],DomainPrefix,Connstring,FtpServer,FtpPort,FtpUser,FtpPassword,PacsAETitle,Telephone,Address,PacsServer,PacsWebServer,Tab,Alias,IISUrl) Values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')  ",
                    row["Domain"] is DBNull ? "" : Convert.ToString(row["Domain"]), row["DomainPrefix"] is DBNull ? "" : Convert.ToString(row["DomainPrefix"]),
                    "",//row["Connstring"] is DBNull ? "" : Convert.ToString(row["Connstring"]), 
                    row["FtpServer"] is DBNull ? "" : Convert.ToString(row["FtpServer"]),
                    row["FtpPort"] is DBNull ? 21 : Convert.ToInt32(row["FtpPort"]), row["FtpUser"] is DBNull ? "" : Convert.ToString(row["FtpUser"]),
                    row["FtpPassword"] is DBNull ? "" : Convert.ToString(row["FtpPassword"]), row["PacsAETitle"] is DBNull ? "" : Convert.ToString(row["PacsAETitle"]),
                    row["Telephone"] is DBNull ? "" : Convert.ToString(row["Telephone"]), row["Address"] is DBNull ? "" : Convert.ToString(row["Address"]),
                    row["PacsServer"] is DBNull ? "" : Convert.ToString(row["PacsServer"]), row["PacsWebServer"] is DBNull ? "" : Convert.ToString(row["PacsWebServer"]),
                    row["Tab"] is DBNull ? "" : Convert.ToString(row["Tab"]), row["Alias"] is DBNull ? "" : Convert.ToString(row["Alias"]), row["IISUrl"] is DBNull ? "" : Convert.ToString(row["IISUrl"]));
                }
                //oKodak.ExecuteNonQuery(strSQL);

                //update Site list
                strSQL += " delete from tSiteList ";
                foreach (DataRow row in ds.Tables["SiteList"].Rows)
                {
                    strSQL += string.Format(" INSERT INTO dbo.tSiteList([Domain],DomainPrefix,Connstring,FtpServer,FtpPort,FtpUser,FtpPassword,PacsAETitle,Telephone,Address,PacsServer,PacsWebServer,Site,Tab,Alias,IISUrl) Values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')  ",
                    row["Domain"] is DBNull ? "" : Convert.ToString(row["Domain"]), row["DomainPrefix"] is DBNull ? "" : Convert.ToString(row["DomainPrefix"]),
                    "",//row["Connstring"] is DBNull ? "" : Convert.ToString(row["Connstring"]), 
                    row["FtpServer"] is DBNull ? "" : Convert.ToString(row["FtpServer"]),
                    row["FtpPort"] is DBNull ? 21 : Convert.ToInt32(row["FtpPort"]), row["FtpUser"] is DBNull ? "" : Convert.ToString(row["FtpUser"]),
                    row["FtpPassword"] is DBNull ? "" : Convert.ToString(row["FtpPassword"]), row["PacsAETitle"] is DBNull ? "" : Convert.ToString(row["PacsAETitle"]),
                    row["Telephone"] is DBNull ? "" : Convert.ToString(row["Telephone"]), row["Address"] is DBNull ? "" : Convert.ToString(row["Address"]),
                    row["PacsServer"] is DBNull ? "" : Convert.ToString(row["PacsServer"]), row["PacsWebServer"] is DBNull ? "" : Convert.ToString(row["PacsWebServer"]),
                    row["Site"] is DBNull ? "" : Convert.ToString(row["Site"]), row["Tab"] is DBNull ? "" : Convert.ToString(row["Tab"]),
                    row["Alias"] is DBNull ? "" : Convert.ToString(row["Alias"]), row["IISUrl"] is DBNull ? "" : Convert.ToString(row["IISUrl"]));
                }
                oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                oKodak.CommitTransaction();

            }
            catch (Exception ex)
            {
                oKodak.RollbackTransaction();
                bReturn = false;
                strError = ex.Message;

                logger.Error((long)ModuleEnum.DataCenter_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public virtual DataSet GetSiteProfileDataSet(string domainName, string siteName)
        {
            RisDAL oKodalDAL = new RisDAL();
            DataSet dataSet = new DataSet();
            string strGetSysProfSQL = string.Empty;
            string strCurrentRowName = "";
            try
            {

                //strGetSysProfSQL = string.Format("SELECT [tSystemProfile].[Name],[tSystemProfile].[ModuleID],[tModule].Title as ModuleName,[tSystemProfile].[Value],[tSystemProfile].[Exportable],[tSystemProfile].[PropertyDesc],[tSystemProfile].[PropertyOptions],[tSystemProfile].[Inheritance],[tSystemProfile].[PropertyType],[tSystemProfile].[IsHidden],[tSystemProfile].[OrderingPos]FROM [tSystemProfile], tModule where tModule.ModuleID = [tSystemProfile].[ModuleID] AND (([tSystemProfile].[IsHidden] & 4) = 4) AND [tSystemProfile].[Inheritance] >= 0 and tSystemProfile.Domain='{0}' ORDER BY [tSystemProfile].[OrderingPos]", strDomain);
                strGetSysProfSQL = string.Format("SELECT [tSiteProfile].[Name],[tSiteProfile].[ModuleID],[tModule].Title as ModuleName,[tSiteProfile].[Value],[tSiteProfile].[Exportable],[PropertyDesc],[PropertyOptions],[Inheritance],[PropertyType],[IsHidden],[OrderingPos],[OrderNo] FROM [tSiteProfile], tModule where tModule.ModuleID = [tSiteProfile].[ModuleID] AND (([tSiteProfile].[IsHidden] & 4) = 4) AND [tSiteProfile].[Inheritance] >= 0 and tModule.Domain='{0}' and tSiteProfile.Domain='{1}' and tSiteProfile.Site='{2}' ORDER BY [tSiteProfile].[OrderingPos]", domainName, domainName, siteName);
                DataTable dataTable = oKodalDAL.ExecuteQuery(strGetSysProfSQL);
                //Build the custom DataTable
                DataTable customedTable = Server.Utilities.Oam.Utilities.CreataCustomDataTable();

                Regex regCheckDomain = new Regex(@"from\s+tDomainList", RegexOptions.IgnoreCase); //[^-_\w]domain[^-_\w]
                Regex regCheckSite = new Regex(@"from\s+tSiteList", RegexOptions.IgnoreCase);

                string strPropertyOption = string.Empty;
                string[] arrPropertyOptionSQL = null;
                string[] arrDftVal = null;
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
                                oKodalDAL.ExecuteQuery(strSQL, dtPropOption);
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

                    //DefaultValue -5
                    if (rowData[2].ToString().Contains("|"))
                    {
                        arrDftVal = rowData[2].ToString().Split('|');
                        foreach (string strDftVal in arrDftVal)
                        {
                            //DefaultValue -5
                            rowData[5] += strDftVal + ",";
                        }
                        rowData[5] = rowData[5].ToString().Remove(rowData[5].ToString().Length - 1);
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
                    rowData[11] = row[dataTable.Columns["OrderNo"]].ToString();//default not set data to orderid(not use in system)
                    //Add to the DataTable
                    customedTable.Rows.Add(rowData);

                }
                dataSet.Tables.Add(customedTable);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "GetSystemProfileDataSet, Error=" + ex.Message);

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

        public virtual bool EditSiteProfile(SystemModel model, string domainName, string siteName)
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
                        if (table.Columns.Contains("ModuleId"))
                        {
                            strBuilder.AppendFormat("UPDATE dbo.tSiteProfile set Value = '{0}' where ModuleId = '{1}' and Name = '{2}' and Domain='{3}' and Site='{4}'", row["FieldValue"].ToString(), row["ModuleId"].ToString(), row["FieldName"].ToString(), domainName, siteName);
                        }
                        else if (table.Columns.Contains("Id"))
                        {
                            strBuilder.AppendFormat("UPDATE dbo.tSiteProfile set Value = '{0}' where ModuleId = '{1}' and Name = '{2}' and Domain='{3}' and Site='{4}'", row["FieldValue"].ToString(), row["Id"].ToString(), row["FieldName"].ToString(), domainName, siteName);
                        }
                        else
                        {
                            System.Diagnostics.Debug.Assert(false, "Invalid table structure!");
                        }
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

        public virtual bool AddSiteProfile(string domainName, string siteName, string fieldName, string moduleID)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            try
            {
                string sql = string.Format("insert into tSiteProfile select *, '{0}' from tSystemProfile where domain = '{1}' and name = '{2}' and moduleid = '{3}';", siteName, domainName, fieldName, moduleID);
                if (fieldName.Equals("AutoGenerateAccNo"))
                {
                    sql += string.Format("if not exists (select 1 from tIDMaxValue where tag = 3 and site = '{0}') insert into tIDMaxValue(tag,value,createdt,domain,site) values (3,0,'{1}','{2}','{0}');", siteName, DateTime.Now.ToString(), domainName);
                }
                else if (fieldName.Equals("AutoGeneratePatientID"))
                {
                    sql += string.Format("if not exists (select 1 from tIDMaxValue where tag = 2 and site = '{0}') insert into tIDMaxValue(tag,value,createdt,domain,site) values (2,0,'{1}','{2}','{0}');", siteName, DateTime.Now.ToString(), domainName);
                }
                oKodak.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error((long)ModuleEnum.DataCenter_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public virtual bool DeleteSiteProfile(string domainName, string siteName, string fieldName, string moduleID)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            try
            {
                string sql = string.Format("delete from tSiteProfile where domain = '{0}' and site = '{1}' and name = '{2}' and moduleid = '{3}';", domainName, siteName, fieldName, moduleID);
                if (fieldName.Equals("AutoGenerateAccNo"))
                {
                    sql += string.Format("delete from tIDMaxValue where tag = 3 and site = '{0}';", siteName);
                }
                else if (fieldName.Equals("AutoGeneratePatientID"))
                {
                    sql += string.Format("delete from tIDMaxValue where tag = 2 and site = '{0}';", siteName);
                }
                oKodak.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error((long)ModuleEnum.DataCenter_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }
        #endregion

        #region ChargeCode
        public virtual DataSet GetAllChargeCodes()
        {
            DataSet ds = new DataSet();
            DataTable dtCode;
            RisDAL oKodak = new RisDAL();

            try
            {
                string strSQL = "SELECT Code,Description,ShortcutCode,Type,Unit,Price,s.Alias,c.Site from tChargeItem c left join tSiteList s on c.site = s.Site";
                dtCode = oKodak.ExecuteQuery(strSQL);
                dtCode.TableName = "ChargeCode";
                ds.Tables.Add(dtCode);
                return ds;
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw ex;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
        }

        public virtual void AddChargeCode(ChargeCodeModel chargeCodeModel)
        {
            RisDAL oKodak = new RisDAL();
            oKodak.Parameters.Clear();
            oKodak.Parameters.AddVarChar("Code", chargeCodeModel.Code);


            try
            {
                string checkExists = "Select 1 from tChargeItem where code=@Code";
                object obj = oKodak.ExecuteScalar(checkExists);
                if (Convert.ToString(obj) == "1")
                {
                    throw new Exception("The charge code is existed!");
                }
                checkExists = "Select 1 from tChargeItem where Description=@Description";

                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("Description", chargeCodeModel.Description);
                obj = oKodak.ExecuteScalar(checkExists);
                if (Convert.ToString(obj) == "1")
                {
                    throw new Exception("The description is existed!");
                }
                checkExists = "Select 1 from tChargeItem where ShortCutCode=@ShortCutCode";
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("ShortCutCode", chargeCodeModel.ShortCutCode);
                obj = oKodak.ExecuteScalar(checkExists);
                if (Convert.ToString(obj) == "1")
                {
                    throw new Exception("The ShortCutCode is existed!");
                }
                string curDomain = CommonGlobalSettings.Utilities.GetCurDomain();

                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("Code", chargeCodeModel.Code);
                oKodak.Parameters.AddVarChar("Description", chargeCodeModel.Description);
                oKodak.Parameters.AddVarChar("ShortCutCode", chargeCodeModel.ShortCutCode);
                oKodak.Parameters.AddVarChar("Type", chargeCodeModel.Type);
                oKodak.Parameters.AddVarChar("Unit", chargeCodeModel.Unit);
                oKodak.Parameters.AddDecimal("Price", chargeCodeModel.Price);
                oKodak.Parameters.AddVarChar("Domain", curDomain);
                oKodak.Parameters.AddVarChar("Site", chargeCodeModel.Site);
                string strSQL = "Insert tChargeItem(Code,Description,ShortCutCode,Type,Price,Unit,Domain,Site) values(@Code,@Description,@ShortCutCode,@Type,@Price,@Unit,@Domain,@Site)";
                oKodak.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw ex;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }

        }

        public virtual void UpdateChargeCode(ChargeCodeModel chargeCodeModel)
        {
            RisDAL oKodak = new RisDAL();
            try
            {
                string checkExists = "Select 1 from tChargeItem where Description= @Description and Code!=@Code";
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("Code", chargeCodeModel.Code);
                oKodak.Parameters.AddVarChar("Description", chargeCodeModel.Description);
                object obj = oKodak.ExecuteScalar(checkExists);
                if (Convert.ToString(obj) == "1")
                {
                    throw new Exception("The description is existed!");
                }

                checkExists = "Select 1 from tChargeItem where ShortCutCode= @ShortCutCode and Code!=@Code";
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("Code", chargeCodeModel.Code);
                oKodak.Parameters.AddVarChar("ShortCutCode", chargeCodeModel.ShortCutCode);
                obj = oKodak.ExecuteScalar(checkExists);
                if (Convert.ToString(obj) == "1")
                {
                    throw new Exception("The ShortCutCode is existed!");
                }

                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("Code", chargeCodeModel.Code);
                oKodak.Parameters.AddVarChar("Description", chargeCodeModel.Description);
                oKodak.Parameters.AddVarChar("ShortCutCode", chargeCodeModel.ShortCutCode);
                oKodak.Parameters.AddVarChar("Type", chargeCodeModel.Type);
                oKodak.Parameters.AddVarChar("Unit", chargeCodeModel.Unit);
                oKodak.Parameters.AddDecimal("Price", chargeCodeModel.Price);
                oKodak.Parameters.AddVarChar("Site", chargeCodeModel.Site);
                string strSQL = "Update tChargeItem set Description=@Description,ShortCutCode=@ShortCutCode,Type=@Type,Unit=@Unit,Price=@Price,Site=@Site where Code=@Code";
                oKodak.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw ex;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }

        }

        public virtual void DeleteChargeCode(string chargeCode)
        {
            RisDAL oKodak = new RisDAL();

            try
            {
                string strDeleteCharge = "Delete from tChargeItem where Code=@Code";
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("Code", chargeCode);
                oKodak.ExecuteNonQuery(strDeleteCharge);
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw ex;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
        }

        public virtual bool IsChargeCodeInUse(string chargeCode)
        {
            RisDAL oKodak = new RisDAL();

            try
            {
                string checkExists = "SELECT 1 from tOrderCharge where Code=@Code";
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("Code", chargeCode);

                object obj = oKodak.ExecuteScalar(checkExists);
                if (Convert.ToString(obj) == "1")
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw ex;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
        }
        #endregion

        #region HotKey
        public virtual DataSet GetAllHotKeys()
        {
            DataSet ds = new DataSet();
            RisDAL oKodak = new RisDAL();

            try
            {
                string strSQL = "select * from tHotKey";
                ds.Tables.Add(oKodak.ExecuteQuery(strSQL));
                return ds;
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw ex;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
        }

        public virtual bool SaveHotKeys(BaseDataSetModel model)
        {
            DataTable dt = model.DataSetParameter.Tables[0];
            RisDAL dataAccess = new RisDAL();
            try
            {
                dataAccess.BeginTransaction();
                foreach (DataRow dr in dt.Rows)
                {
                    string sql = string.Format("update tHotKey set HotKey = '{0}' where Guid = '{1}' and FunctionName = '{2}'", dr["HotKey"].ToString(), dr["Guid"].ToString(), dr["FunctionName"].ToString());
                    dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception ex)
            {
                dataAccess.RollbackTransaction();
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(ex.Message);
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
        #endregion

        #region MessageConfig
        public virtual DataSet GetAllMessageConfigs()
        {
            DataSet ds = new DataSet();
            RisDAL oKodak = new RisDAL();

            try
            {
                string strSQL = "select * from tMessageConfig";
                ds.Tables.Add(oKodak.ExecuteQuery(strSQL));
                return ds;
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw ex;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
        }
        public virtual bool SaveMessageConfigs(BaseDataSetModel model)
        {
            DataTable dt = model.DataSetParameter.Tables[0];
            RisDAL dataAccess = new RisDAL();
            try
            {
                string sql = "";
                dataAccess.BeginTransaction();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Modified)
                    {
                        sql = string.Format(@"if not exists(select 1 from tMessageConfig where Guid = '{0}')
                    insert into tMessageConfig(Guid,EventType,TemplateSample,Template,TemplateSP,MessageType,ReceiveType,ReceiveObject,Enabled,Site,Domain,RetryTimes,RetryTimeInterval,EventRelativeTimeStart,EventRelativeTimeEnd,EventProcessRecurrencePattern)
                    values('{0}','{1}',@TemplateSample,@Template,'{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}',@EventRelativeTimeStart,@EventRelativeTimeEnd,@EventProcessRecurrencePattern)
                    else
                    update tMessageConfig set EventType ='{1}',TemplateSample=@TemplateSample,Template=@Template,TemplateSP ='{2}',MessageType ='{3}',ReceiveType='{4}',ReceiveObject='{5}',Enabled ={6},Site='{7}',Domain='{8}',RetryTimes='{9}',RetryTimeInterval='{10}',EventRelativeTimeStart=@EventRelativeTimeStart,EventRelativeTimeEnd=@EventRelativeTimeEnd,EventProcessRecurrencePattern=@EventProcessRecurrencePattern where Guid ='{0}'",
                        Convert.ToString(dr["Guid"]), Convert.ToString(dr["EventType"]), Convert.ToString(dr["TemplateSP"]), Convert.ToString(dr["MessageType"]),
                        Convert.ToString(dr["ReceiveType"]), Convert.ToString(dr["ReceiveObject"]), Convert.ToInt32(dr["Enabled"]), Convert.ToString(dr["Site"]), Convert.ToString(dr["Domain"]), Convert.ToString(dr["RetryTimes"]), Convert.ToString(dr["RetryTimeInterval"]));
                        dataAccess.Parameters.Clear();
                        dataAccess.Parameters.AddVarChar("@TemplateSample", Convert.ToString(dr["TemplateSample"]));
                        dataAccess.Parameters.AddVarChar("@Template", Convert.ToString(dr["Template"]));
                        dataAccess.Parameters.AddInt("@EventRelativeTimeStart", dr["EventRelativeTimeStart"]);
                        dataAccess.Parameters.AddInt("@EventRelativeTimeEnd", dr["EventRelativeTimeEnd"]);
                        dataAccess.Parameters.AddVarChar("@EventProcessRecurrencePattern", Convert.ToString(dr["EventProcessRecurrencePattern"]));
                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    else if (dr.RowState == DataRowState.Deleted)
                    {
                        sql = string.Format("delete from tMessageConfig where guid ='{0}'", Convert.ToString(dr["Guid", DataRowVersion.Original]));
                        dataAccess.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                }
                dataAccess.CommitTransaction();
            }
            catch (Exception ex)
            {
                dataAccess.RollbackTransaction();
                logger.Error(
                    (long)ModuleEnum.Oam_DA,
                    ModuleInstanceName.Oam,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                throw new Exception(ex.Message);
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
        #endregion

        #region Do
        public virtual bool Do(int dbAction, BaseDataSetModel model, ref string dbActionMsg)
        {
            switch (dbAction)
            {
                case 1://new
                    {
                        DataTable dt = model.DataSetParameter.Tables[0];
                        return New(dt, ref dbActionMsg);
                    }
                case 2://update
                    {
                        DataTable dt = model.DataSetParameter.Tables[0];
                        return Update(dt, ref dbActionMsg);
                    }
                case 3://delete
                    {
                        DataTable dt = model.DataSetParameter.Tables[0];
                        return Delete(dt, ref dbActionMsg);
                    }
                case 4://query
                    {
                        DataTable dt = model.DataSetParameter.Tables[0];
                        DataTable dtQuery = new DataTable();
                        if (dt.Columns.Contains("Site"))
                        {
                            dtQuery = Query(Convert.ToString(dt.Rows[0]["TableName"]), Convert.ToString(dt.Rows[0]["OrderBy"]), Convert.ToString(dt.Rows[0]["Site"]), ref dbActionMsg);
                        }
                        else
                        {
                            dtQuery = Query(Convert.ToString(dt.Rows[0]["TableName"]), Convert.ToString(dt.Rows[0]["OrderBy"]), "", ref dbActionMsg);
                        }
                        model.DataSetParameter.Tables.Clear();
                        model.DataSetParameter.Tables.Add(dtQuery);
                        return true;
                    }
                default:
                    dbActionMsg = string.Format("No implementation action({0})", dbAction);
                    return false;
            }

        }

        private bool New(DataTable dtNew, ref string msg)
        {
            RisDAL dal = new RisDAL();
            try
            {
                string sql = string.Format("insert into {0} ", dtNew.TableName);
                string tableColumns = "";
                string values = "";
                foreach (DataColumn dc in dtNew.Columns)
                {
                    tableColumns += dc.ColumnName + ",";
                }
                tableColumns = tableColumns.TrimEnd(new char[] { ',' });
                sql += "(" + tableColumns + ")";
                dal.BeginTransaction();
                foreach (DataRow dr in dtNew.Rows)
                {
                    values = "";
                    dal.Parameters.Clear();
                    foreach (DataColumn dc in dtNew.Columns)
                    {
                        values += "@" + dc.ColumnName + ",";
                        if (dc.ColumnName.ToLower().Equals("domain"))
                        {
                            dal.Parameters.Add("@" + dc.ColumnName, CommonGlobalSettings.Utilities.GetCurDomain());
                        }
                        else
                        {
                            dal.Parameters.Add("@" + dc.ColumnName, dr[dc.ColumnName]);
                        }
                    }
                    values = values.TrimEnd(",".ToCharArray());
                    dal.ExecuteNonQuery(sql + " values (" + values + ")", RisDAL.ConnectionState.KeepOpen);
                }
                dal.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
        }

        private bool Update(DataTable dtModified, ref string msg)
        {
            RisDAL dal = new RisDAL();
            try
            {
                string sql = string.Format("update {0} set ", dtModified.TableName);
                string setClause = "";
                string whereCluse = " where ID = '{0}'";
                dal.BeginTransaction();
                foreach (DataRow dr in dtModified.Rows)
                {
                    setClause = "";
                    dal.Parameters.Clear();
                    foreach (DataColumn dc in dtModified.Columns)
                    {
                        setClause += dc.ColumnName + "=@" + dc.ColumnName + ",";
                        if (dc.ColumnName.ToLower().Equals("domain"))
                        {
                            dal.Parameters.Add("@" + dc.ColumnName, CommonGlobalSettings.Utilities.GetCurDomain());
                        }
                        else
                        {
                            dal.Parameters.Add("@" + dc.ColumnName, dr[dc.ColumnName]);
                        }
                        //dal.Parameters.Add("@" + dc.ColumnName, dr[dc.ColumnName]);
                    }
                    setClause = setClause.TrimEnd(",".ToCharArray());
                    dal.ExecuteNonQuery(sql + setClause + string.Format(whereCluse, Convert.ToString(dr["ID"])), RisDAL.ConnectionState.KeepOpen);
                }
                dal.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
        }

        private bool Delete(DataTable dtDeleted, ref string msg)
        {
            RisDAL dal = new RisDAL();
            try
            {
                string sql = string.Format("delete from {0} ", dtDeleted.TableName);
                string whereCluse = " where ID = '{0}'";
                dal.BeginTransaction();
                foreach (DataRow dr in dtDeleted.Rows)
                {
                    dal.ExecuteNonQuery(sql + string.Format(whereCluse, Convert.ToString(dr["ID", DataRowVersion.Original])), RisDAL.ConnectionState.KeepOpen);
                }
                dal.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
        }

        private DataTable Query(string tableName, string orderBy, string site, ref string msg)
        {
            RisDAL dal = new RisDAL();
            try
            {
                DataTable dt = new DataTable(tableName);
                string sql = "";
                if (string.IsNullOrWhiteSpace(site))
                {
                    sql = string.Format("select * from {0} order by {1}", tableName, orderBy);
                }
                else
                {
                    sql = string.Format("select * from {0} where site='{1}' or site='' or site is null order by {2}", tableName, site, orderBy);
                }
                dal.ExecuteQuery(sql, dt);
                return dt;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return null;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
        }
        #endregion

        #region peoplescheduler
        public DataTable GetPeopleScheduleTemplate(string site, string templateType, string beginTime)
        {
            RisDAL dal = new RisDAL();
            try
            {
                DataTable dt = new DataTable("PeopleScheduleTemplate");
                string sql = "";
                DateTime firstDay = DateTime.Now.AddDays(-((int)DateTime.Now.DayOfWeek - 1));
                DateTime.TryParse(beginTime, out firstDay);
                if (string.IsNullOrWhiteSpace(site))
                {
                    sql = string.Format("select * from tScheduleTempate where templateType ='{0}' and begintime >= '{1}' and begintime <= '{2}' and site='' or site is null", templateType, firstDay.ToString("yyyy-MM-dd"), firstDay.AddDays(6).ToString("yyyy-MM-dd"));
                }
                else
                {
                    sql = string.Format("select * from tScheduleTempate where templateType ='{0}' and site ='{1}' and begintime >= '{2}' and begintime <= '{3}' ", templateType, site, firstDay.ToString("yyyy-MM-dd"), firstDay.AddDays(6).ToString("yyyy-MM-dd"));
                }
                dal.ExecuteQuery(sql, dt);
                return dt;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return null;
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
        }

        public bool SavePeopleScheduleTemplate(DataTable dt)
        {
            RisDAL oKodak = new RisDAL();
            try
            {
                string sql = "";
                //new
                DataTable dtAdded = dt.GetChanges(DataRowState.Added);
                if (dtAdded != null)
                {
                    oKodak.BeginTransaction();
                    foreach (DataRow dr in dtAdded.Rows)
                    {
                        sql = string.Format(@"INSERT INTO [tScheduleTempate]
                           ([GUID],[Level],[TemplateType],[NodeType],[Name],[BeginTime],[EndTime],[WorkStationIP],[OrderId],[Parent],[Site],[Domain])
                            values('{0}',{1},'{2}',{3},@Name,'{4}','{5}','{6}',{7},'{8}','{9}','{10}')",
                               Convert.ToString(dr["Guid"]),
                               Convert.ToString(dr["Level"]),
                               Convert.ToString(dr["TemplateType"]),
                               Convert.ToString(dr["NodeType"]),
                               Convert.ToString(dr["BeginTime"]),
                               Convert.ToString(dr["EndTime"]),
                               Convert.ToString(dr["WorkStationIP"]),
                               Convert.ToInt32(dr["OrderId"]),
                               Convert.ToString(dr["Parent"]),
                               Convert.ToString(dr["Site"]),
                               Convert.ToString(dr["Domain"])
                               );
                        oKodak.Parameters.Clear();
                        oKodak.Parameters.AddVarChar("@Name", Convert.ToString(dr["Name"]));
                        oKodak.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    oKodak.CommitTransaction();
                }
                //modify
                DataTable dtModified = dt.GetChanges(DataRowState.Modified);
                if (dtModified != null)
                {
                    oKodak.BeginTransaction();
                    foreach (DataRow dr in dtModified.Rows)
                    {
                        oKodak.Parameters.Clear();
                        sql = string.Format(@"update [tScheduleTempate] set Name = @Name,BeginTime = '{1}',EndTime = '{2}',NodeType ={3},WorkStationIP = @WorkStationIP where Guid ='{0}'", Convert.ToString(dr["Guid"]), Convert.ToString(dr["BeginTime", DataRowVersion.Current]), Convert.ToString(dr["EndTime", DataRowVersion.Current]), Convert.ToInt32(dr["NodeType"]));
                        oKodak.Parameters.AddVarChar("@Name", Convert.ToString(dr["Name", DataRowVersion.Current]));
                        oKodak.Parameters.AddVarChar("@WorkStationIP", Convert.ToString(dr["WorkStationIP", DataRowVersion.Current]));
                        oKodak.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    oKodak.CommitTransaction();
                }

                //delete
                DataTable dtDeleted = dt.GetChanges(DataRowState.Deleted);
                if (dtDeleted != null)
                {
                    oKodak.BeginTransaction();
                    foreach (DataRow dr in dtDeleted.Rows)
                    {
                        sql = string.Format(@"delete from [tScheduleTempate] where Guid ='{0}'", Convert.ToString(dr["Guid", DataRowVersion.Original]));
                        oKodak.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                        //need delete associated people schedule
                        sql = string.Format(@"delete from [tPeopleSchedule] where TemplateGuid ='{0}'", Convert.ToString(dr["Guid", DataRowVersion.Original]));
                        oKodak.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    oKodak.CommitTransaction();
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
        }

        public bool SavePeopleSchedule(DataTable dt)
        {
            RisDAL oKodak = new RisDAL();
            try
            {
                string sql = "";
                //new
                DataTable dtAdded = dt.GetChanges(DataRowState.Added);
                if (dtAdded != null)
                {
                    oKodak.BeginTransaction();
                    foreach (DataRow dr in dtAdded.Rows)
                    {
                        oKodak.Parameters.Clear();
                        sql = string.Format(@"INSERT INTO [tPeopleSchedule]
                           ([GUID],[UserGuid],[BeginTime],[EndTime],[TemplateGuid],[WorkStationIP],[Site],[Domain])
                            values('{0}','{1}',@BeginTime,@EndTime,'{2}','{3}','{4}','{5}')",
                               Convert.ToString(dr["Guid"]),
                               Convert.ToString(dr["UserGuid"]),
                               Convert.ToString(dr["TemplateGuid"]),
                               Convert.ToString(dr["WorkStationIP"]),
                               Convert.ToString(dr["Site"]),
                               Convert.ToString(dr["Domain"])
                               );
                        oKodak.Parameters.AddDateTime("@BeginTime", dr["BeginTime"]);
                        oKodak.Parameters.AddDateTime("@EndTime", dr["EndTime"]);
                        oKodak.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    oKodak.CommitTransaction();
                }
                //modify
                DataTable dtModified = dt.GetChanges(DataRowState.Modified);
                if (dtModified != null)
                {
                    oKodak.BeginTransaction();
                    foreach (DataRow dr in dtModified.Rows)
                    {
                        oKodak.Parameters.Clear();
                        sql = string.Format(@"update [tPeopleSchedule] set 
                                UserGuid ='{0}',TemplateGuid = '{1}',BeginTime = @BeginTime,EndTime = @EndTime, WorkStationIP= '{2}',Site = '{3}',Domain = '{4}' where Guid ='{5}'",
                               Convert.ToString(dr["UserGuid"]),
                               Convert.ToString(dr["TemplateGuid"]),
                               Convert.ToString(dr["WorkStationIP"]),
                               Convert.ToString(dr["Site"]),
                               Convert.ToString(dr["Domain"]),
                               Convert.ToString(dr["Guid"])
                                   );
                        oKodak.Parameters.AddDateTime("@BeginTime", dr["BeginTime"]);
                        oKodak.Parameters.AddDateTime("@EndTime", dr["EndTime"]);
                        oKodak.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    oKodak.CommitTransaction();
                }

                //delete
                DataTable dtDeleted = dt.GetChanges(DataRowState.Deleted);
                if (dtDeleted != null)
                {
                    oKodak.BeginTransaction();
                    foreach (DataRow dr in dtDeleted.Rows)
                    {
                        sql = string.Format(@"delete from [tPeopleSchedule] where Guid ='{0}'", Convert.ToString(dr["Guid", DataRowVersion.Original]));
                        oKodak.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                    }
                    oKodak.CommitTransaction();
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
        }

        public DataTable GetPeoplesWeekSchedule(DataTable dtCondition)
        {
            RisDAL oKodak = new RisDAL();
            string sql = "";
            DataTable dtPeopleSchedules = new DataTable();
            try
            {
                if (dtCondition != null && dtCondition.Rows.Count > 0)
                {
                    string peoples = Convert.ToString(dtCondition.Rows[0]["UserGuids"]);
                    sql = "UserGuid in ( ";
                    if (!string.IsNullOrEmpty(peoples))
                    {
                        sql += "'" + peoples.Replace(",", "','") + "'";
                    }
                    sql += " ) And (";
                    foreach (DataRow dr in dtCondition.Rows)
                    {
                        sql += string.Format(" BeginTime>='{0}' and DatePart(d,BeginTime) != DatePart(d,'{1}') and   EndTime <= '{1}'", Convert.ToString(dr["BeginTime"]), Convert.ToString(dr["EndTime"]));
                    }
                    sql += ")";
                    sql += string.Format(" And Site='{0}'", Convert.ToString(dtCondition.Rows[0]["Site"]));
                    sql = string.Format(@"select *,dbo.translateUser(UserGuid) as UserName from tPeopleSchedule where " + sql);
                    oKodak.ExecuteQuery(sql, dtPeopleSchedules);
                }
                return dtPeopleSchedules;
            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Oam_DA, ModuleInstanceName.Oam, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                 (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return dtPeopleSchedules;
            }
            finally
            {
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
        }
        #endregion

    }
}
