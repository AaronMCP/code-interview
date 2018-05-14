using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using LogServer;
using System.Data;

namespace Server.ReportDAO
{

    public class SetDrawerDAO
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

    internal class SetDrawerDAO_ABSTRACT : IReportDAO
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

    internal class SetDrawerDAO_SYBASE : IReportDAO
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

    internal class SetDrawerDAO_MSSQL : IReportDAO
    {
        static int iWrittenCount = 0;

        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in SetDrawerDAO!"));
                }
                


                try
                {
                    DataSet ds = paramMap["DataSet"] as DataSet;

                    object objSign = ds.Tables[0].Rows[0]["DrawerSign"];
                    string strTakeFilmDept = Convert.ToString(ds.Tables[0].Rows[0]["TakeFilmDept"]);
                    string strTakeFilmRegion = Convert.ToString(ds.Tables[0].Rows[0]["TakeFilmRegion"]);
                    string strTakeFilmComment = Convert.ToString(ds.Tables[0].Rows[0]["TakeFilmComment"]);
                    //object objReportTextApprovedSign = ds.Tables[0].Rows[0]["ReportTextApprovedSign"];
                    //object objReportTextSubmittedSign = ds.Tables[0].Rows[0]["ReportTextSubmittedSign"];
                    //object objCombinedForCertification = ds.Tables[0].Rows[0]["CombinedForCertification"];
                    //object objSignCombinedForCertification = ds.Tables[0].Rows[0]["SignCombinedForCertification"];
                    using (RisDAL oKodak = new RisDAL())
                    {
                        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(oKodak.ConnectionString))
                        {
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.CommandTimeout = 0;
                            cmd.Connection = conn;



                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                string strReportGuid = Convert.ToString(dr["ReportGuid"]);
                                cmd.CommandText = string.Format(
                                    "update tReport set IsDraw=" + ((objSign is System.DBNull) ? "0" : "1") + ",TakeFilmDept=@dept,"
                                    + " TakeFilmRegion=@region,TakeFilmComment=@comments,"
                                    + " DrawerSign=@file,DrawTime=getdate() "
                                    + " where ReportGuid='{0}'", strReportGuid);
                                cmd.Parameters.Clear();

                                cmd.Parameters.AddWithValue("@dept", strTakeFilmDept);
                                cmd.Parameters.AddWithValue("@region", strTakeFilmRegion);
                                cmd.Parameters.AddWithValue("@comments", strTakeFilmComment);
                                //cmd.Parameters.AddWithValue("@file", objSign);

                                cmd.Parameters.Add("@file", SqlDbType.VarBinary, -1);
                                cmd.Parameters["@file"].Value = (objSign is System.DBNull) ? System.DBNull.Value : objSign;

                                cmd.ExecuteNonQuery();

                                //cmd.CommandText = string.Format("update tReport set "
                                //    + " ReportTextApprovedSign=@objReportTextApprovedSign"
                                //    + " where ReportGuid='{0}'", strReportGuid);
                                //cmd.Parameters.Add("@objReportTextApprovedSign", SqlDbType.VarBinary, -1);
                                //cmd.Parameters["@objReportTextApprovedSign"].Value
                                //    = (objReportTextApprovedSign is System.DBNull) ? System.DBNull.Value : objReportTextApprovedSign;
                                //cmd.ExecuteNonQuery();

                                //cmd.CommandText = string.Format("update tReport set "
                                //    + " ReportTextSubmittedSign=@objReportTextSubmittedSign"
                                //    + " where ReportGuid='{0}'", strReportGuid);
                                //cmd.Parameters.Add("@objReportTextSubmittedSign", SqlDbType.VarBinary, -1);
                                //cmd.Parameters["@objReportTextSubmittedSign"].Value
                                //    = (objReportTextSubmittedSign is System.DBNull) ? System.DBNull.Value : objReportTextSubmittedSign;
                                //cmd.ExecuteNonQuery();

                                //cmd.CommandText = string.Format("update tReport set "
                                //    + " CombinedForCertification=@objCombinedForCertification"
                                //    + " where ReportGuid='{0}'", strReportGuid);
                                //cmd.Parameters.Add("@objCombinedForCertification", SqlDbType.VarBinary, -1);
                                //cmd.Parameters["@objCombinedForCertification"].Value
                                //    = (objCombinedForCertification is System.DBNull) ? System.DBNull.Value : objCombinedForCertification;
                                //cmd.ExecuteNonQuery();

                                //cmd.CommandText = string.Format("update tReport set "
                                //    + " SignCombinedForCertification=@objSignCombinedForCertification"
                                //    + " where ReportGuid='{0}'", strReportGuid);
                                //cmd.Parameters.Add("@objSignCombinedForCertification", SqlDbType.VarBinary, -1);
                                //cmd.Parameters["@objSignCombinedForCertification"].Value
                                //    = (objSignCombinedForCertification is System.DBNull) ? System.DBNull.Value : objSignCombinedForCertification;
                                //cmd.ExecuteNonQuery();

                                //Send to gateway eventtype=34
                                tagReportInfo rptInfo = ServerPubFun.GetReportInfo(strReportGuid);
                                string strSql = OnDeliverReport(rptInfo);
                                if (strSql != null && strSql.Trim().Length > 0)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = strSql;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    //dal.RollbackTransaction();

                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "SetDrawerDAO=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                    throw (ex);
                }



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "SetDrawerDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                return false;
            }

            return true;
        }


        private string OnDeliverReport(tagReportInfo rptInfo)
        {
            string sql = "";
            // Need to send gateway
            if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                return sql;
            try
            {
                // Gateway
                string guid = Guid.NewGuid().ToString();
                int event_type = 34;
                string exam_status = "";

                sql = "insert GW_DataIndex(data_id, data_dt, event_type, RECORD_INDEX_1, Data_Source)"
                + " values('" + guid + "', getdate(), '" + event_type.ToString() + "', 'ReportGuid', 'Local')"
                + " insert GW_Patient(DATA_ID,DATA_DT,PATIENTID,OTHER_PID,PATIENT_NAME,PATIENT_LOCAL_NAME,"
                + "BIRTHDATE,SEX,PATIENT_ALIAS,ADDRESS,PHONENUMBER_HOME,MARITAL_STATUS,PATIENT_TYPE,"
                + "PATIENT_LOCATION,VISIT_NUMBER,BED_NUMBER,CUSTOMER_1,CUSTOMER_2,CUSTOMER_3,CUSTOMER_4)"
                + "    values('" + guid + "', getdate(), '" + rptInfo.patientID + "','" + rptInfo.remotePID + "','"
                + rptInfo.patientName + "',N'" + rptInfo.patientLocalName + "','" + rptInfo.birthday.ToString("yyyy-MM-dd") + "','"
                + rptInfo.gender + "', '" + rptInfo.patientAlias + "', '" + rptInfo.patientAddress + "', '"
                + rptInfo.patientPhone + "', '" + rptInfo.patientMarriage + "','" + rptInfo.patientType + "','"
                + rptInfo.inHospitalRegion + "', '" + rptInfo.visitNo + "', '" + rptInfo.bedNo + "', '" + rptInfo.patientName + "', '"
                + rptInfo.isVIP + "', '" + rptInfo.inHospitalNo + "', '" + rptInfo.patientComment + "') "
                + " insert GW_Order(DATA_ID,DATA_DT,ORDER_NO,PLACER_NO,FILLER_NO,PATIENT_ID,EXAM_STATUS,"
                + "PLACER_DEPARTMENT, PLACER, FILLER_DEPARTMENT, FILLER, REF_PHYSICIAN, REQUEST_REASON, "
                + "REUQEST_COMMENTS, EXAM_REQUIREMENT, SCHEDULED_DT, MODALITY, STATION_NAME, EXAM_LOCATION, "
                + "EXAM_DT, DURATION, TECHNICIAN, BODY_PART, PROCEDURE_CODE, PROCEDURE_DESC, EXAM_COMMENT, "
                + "CHARGE_STATUS, CHARGE_AMOUNT,CUSTOMER_1,CUSTOMER_2,CUSTOMER_3) "
                + "    values('" + guid + "', getdate(), '" + rptInfo.orderGuid + "','" + rptInfo.remoteAccNo + "', '"
                + rptInfo.AccNO + "', '" + rptInfo.patientID + "', '" + exam_status.ToString() + "', '" + rptInfo.dept + "', '" + rptInfo.applyDoctor + "','"
                + rptInfo.dept + "', '" + rptInfo.applyDoctor + "', '" + rptInfo.applyDoctor + "', '" + rptInfo.observation + "', '"
                + rptInfo.visitComments + "', '" + rptInfo.orderComments + "', '" + rptInfo.registerDt.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                + rptInfo.modalityType + "', '" + rptInfo.modality + "', '', '" + rptInfo.examineDt.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                + rptInfo.duration + "', '" + rptInfo.technician__LocalName + "', '" + rptInfo.bodypart + "', '" + rptInfo.procedureCode + "', '"
                + rptInfo.procedureDesc + "', '" + rptInfo.orderComments + "', '" + (rptInfo.isCharge == "1" ? "Y" : "N") + "', '"
                + rptInfo.charge + "','" + rptInfo.cardno + "','" + rptInfo.hisid + "','" + rptInfo.MedicareNo + "') ";

                /*
                + " insert GW_Report(data_id, data_dt, report_no, ACCESSION_NUMBER, PATIENT_ID, REPORT_STATUS, MODALITY, "
                + " REPORT_TYPE, REPORT_FILE, REPORT_WRITER, REPORT_APPROVER, REPORTDT, OBSERVATIONMETHOD,DIAGNOSE,COMMENTS,CUSTOMER_4)"
                + " values('" + guid + "', getdate(), '" + rptInfo.reportGuid + "', '" + rptInfo.AccNO + "',"
                + " '" + rptInfo.patientID + "', '" + report_status.ToString() + "', '" + rptInfo.modalityType + "', '0', '', '" + rptInfo.reportCreater_LocalName + "',"
                + " '" + rptInfo.reportApprover_LocalName + "', '" + rptInfo.reportCreateDt.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                + " '" + rptInfo.operationStep + "','" + rptInfo.wysText + "','" + rptInfo.wygText + "','" + rptInfo.techinfo + "')";
                  */


            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "Deliver report send to gateway MSG=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }
            return sql;
        }
    }

    internal class SetDrawerDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            return null;
        }
    }
}
