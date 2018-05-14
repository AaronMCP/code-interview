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
/*   Author : Paul Li                                                       */
/****************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DataAccessLayer;
using CommonGlobalSettings;
using LogServer;
using Server.Utilities.LogFacility;
using System.Diagnostics;
using System.Web;
using CommonGlobalSettings.HippaName;
using Common.ActionResult;

namespace Server.DAO.QualityControl.Impl
{
    //Default support SQLSERVER2005
    public abstract class AbstractQualityImpl : IDBProvider
    {
        protected Server.Utilities.LogFacility.LogManagerForServer logger = new Server.Utilities.LogFacility.LogManagerForServer("QualityControlServerLoglevel", "0D00");
        protected const int SYNC_TYPE_ORDEr = 2;
        #region Query
        public virtual bool QueryPatient(string strPatientName, string strPatientID, string strBeginDt, string strEndDt, bool bIsVIP, DataSet ds, ref string strError)
        {
            Debug.WriteLine("QueryPatient...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                //this sql is used to get basic information of patient
                //Selected is always false and this column will be used by client for multi selection
                string strSQL = string.Format("SELECT 1 as selected,0 as Target, A.PatientGuid,A.PatientID,A.LocalName,A.Birthday,A.EnglishName,A.ParentName,case when a.isvip= 1 then 'Y' else 'N' end as IsVIP,A.Comments,") +
                    "A.Gender," +
                    "A.Address,A.Telephone, A.CreateDt,A.ReferenceNo,A.MedicareNo,A.RemotePID,A.MedicareNo,A.Alias,A.Marriage,A.RelatedID FROM tRegPatient A WHERE  1 =1 ";


                if (strPatientName != null && strPatientName.Trim().Length > 0)
                //query with patient name
                {
                    strSQL += " and A.LocalName = '" + strPatientName.Trim() + "' ";
                }

                if (strPatientID != null && strPatientID.Trim().Length > 0)
                //query with patient id
                {
                    strSQL += " and A.PatientID = '" + strPatientID.Trim() + "' ";
                }

                if (strBeginDt != null && strBeginDt.Trim().Length > 0)
                //query with create date(begin date)
                {
                    strSQL += string.Format(" and A.CreateDt >  '{0} 00:00:00' ", strBeginDt);
                }

                if (strEndDt != null && strEndDt.Trim().Length > 0)
                //query with create date(begin date)
                {
                    strSQL += string.Format(" and A.CreateDt <  '{0} 23:59:59' ", strEndDt);
                }

                if (!bIsVIP)
                {
                    strSQL += " AND ";
                    strSQL += " A.IsVIP=0 ";
                }

                //add order by clause
                strSQL += " Order by A.CreateDt Desc ";


                Debug.WriteLine(strSQL);
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "Patient";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool QueryPatientList(string strPatientName, string strPatientID, string strBeginDt, string strEndDt, bool bIsVIP, DataSet ds, ref string strError)
        {
            Debug.WriteLine("QueryPatient...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                //this sql is used to get basic information of patient
                //Selected is always false and this column will be used by client for multi selection
                string strSQL = string.Format("SELECT 1 as selected,0 as Target, A.PatientGuid,A.PatientID,A.LocalName,A.Birthday,A.EnglishName,A.ParentName,case when a.isvip= 1 then 'Y' else 'N' end as IsVIP,A.Comments,") +
                    "A.Gender," +
                    "A.Address,A.Telephone, A.CreateDt,A.ReferenceNo,A.MedicareNo,A.RemotePID,A.MedicareNo,A.Alias,A.Marriage,A.RelatedID FROM tPatientList A WHERE  1 =1 ";

                if (strPatientName != null && strPatientName.Trim().Length > 0)
                //query with patient name
                {
                    strSQL += " and A.LocalName = '" + strPatientName.Trim() + "' ";
                }

                if (strPatientID != null && strPatientID.Trim().Length > 0)
                //query with patient id
                {
                    strSQL += " and A.PatientID = '" + strPatientID.Trim() + "' ";
                }

                if (strBeginDt != null && strBeginDt.Trim().Length > 0)
                //query with create date(begin date)
                {
                    strSQL += string.Format(" and A.CreateDt >  '{0} 00:00:00' ", strBeginDt);
                }

                if (strEndDt != null && strEndDt.Trim().Length > 0)
                //query with create date(begin date)
                {
                    strSQL += string.Format(" and A.CreateDt <  '{0} 23:59:59' ", strEndDt);
                }

                if (!bIsVIP)
                {
                    strSQL += " AND ";
                    strSQL += " A.IsVIP=0 ";
                }

                //add order by clause
                strSQL += " Order by A.CreateDt Desc ";


                Debug.WriteLine(strSQL);
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "Patient";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool QueryOrder(string strPatientGuid, DataSet ds, ref string strError)
        {
            Debug.WriteLine("QueryOrder...");
            bool bReturn = true;
            DataTable dt = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {
                if (strPatientGuid == null || strPatientGuid.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }

                string strSQL = "SELECT A.PatientID,C.* ,C.CreateDt as OrderRegDate,  " +
                     "C.PatientType,  " +
                     "C.InhospitalRegion,  " +
                     "C.ApplyDoctor,  " +
                     "C.ApplyDept,   " +
                     "C.ChargeType, " +
                     "(select top 1 ExamineDt from tRegProcedure B where C.OrderGuid = B.OrderGuid order by ExamineDt asc) as ExamineDt " +
                    "FROM tRegPatient A,tRegOrder C " +
                    "WHERE A.PatientGuid=C.PatientGuid ";
                strSQL += " and A.PatientGuid = '" + strPatientGuid.Trim() + "' ";
                //add order by clause
                strSQL += " Order by C.CreateDt Desc";

                Debug.WriteLine(strSQL);
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "Order";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        /// <summary>
        /// query order that big than the status
        /// </summary>
        /// <param name="strPatientGuid"></param>
        /// <param name="ds"></param>
        /// <param name="status"> the RP exam status</param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public virtual bool QueryOrder(string strPatientGuid, DataSet ds, string status, ref string strError)
        {
            Debug.WriteLine("QueryOrder...");
            bool bReturn = true;
            DataTable dt = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {
                if (strPatientGuid == null || strPatientGuid.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }

                string strSQL = "SELECT distinct A.PatientID,C.PatientGuid,C.AccNo,C.OrderGuid,C.CreateDt as OrderRegDate ," +
                     "C.PatientType,  " +
                     "C.InhospitalRegion," +
                     "C.InhospitalNo,  " +
                     "C.ClinicNo," +
                     "C.ApplyDoctor,  " +
                     "C.ApplyDept ,   " +
                     "C.BedNo," +
                     "C.ChargeType, " +
                     "PC.ExamineDt " +
                    " FROM tRegPatient A,tRegOrder C Left join  tRegProcedure PC on C.OrderGuid = PC.OrderGuid " +
                    "WHERE A.PatientGuid=C.PatientGuid ";
                strSQL += " and A.PatientGuid = '" + strPatientGuid.Trim() + "' ";
                strSQL += " and ( PC.Status >= " + status + " or PC.Status is null )";
                //add order by clause
                strSQL += " Order by C.CreateDt Desc";

                Debug.WriteLine(strSQL);
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "Order";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
        /// <summary>
        /// Query order with orderID
        /// </summary>
        /// <param name="strPatientGuid"></param>
        /// <param name="ds"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public virtual bool QueryOrderWithOrderID(string strOrderGUID, DataSet ds, ref string strError)
        {
            Debug.WriteLine("QueryOrderWithOrderID...");
            bool bReturn = true;
            DataTable dt = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {
                if (strOrderGUID == null || strOrderGUID.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }

                string strSQL = "SELECT A.PatientID,C.* ,C.CreateDt as OrderRegDate FROM tRegPatient A,tRegOrder C " +
                    "WHERE A.PatientGuid=C.PatientGuid ";
                strSQL += " and C.OrderGuid = '" + strOrderGUID.Trim() + "' ";

                Debug.WriteLine(strSQL);
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "Order";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        /// <summary>
        /// Query RP and return data table
        /// </summary>
        /// <param name="strOrderGuid"></param>
        /// <param name="dt"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public virtual bool QueryRP(string strOrderGuid, ref DataTable dt, ref string strError, ref string strRPGuid)
        {
            Debug.WriteLine("QueryRP...");
            bool bReturn = true;
            if (dt == null) return false;

            RisDAL oKodak = new RisDAL();
            try
            {
                if (strOrderGuid == null || strOrderGuid.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }
                string strSQL = " SELECT  P.PatientGuid,P.PatientID,P.Localname, a.ProcedureGuid, a.ExposalCount,a.FilmCount,a.ContrastName, " +
                " A.contrastdose,A.contrastname, A.charge,A.filmspec, a.RemoteRPID," +
                " A.techDoctor as DoctorGUID,A.techNurse as NurseGUID, A.technician as TechGUID , A.technician1 as TechGUID1 , A.technician2 as TechGUID2 , A.technician3 as TechGUID3 , A.technician4 as TechGUID4 ,A.Registrar as RegtGUID, " +
                " (select localname from tuser where tuser.Userguid = a.techDoctor) as  techDoctor, " +
                " (select localname from tuser where tuser.Userguid = a.techNurse) as  techNurse,  " +
                " (select localname from tuser where tuser.Userguid = a.technician) as  technician, " +
                " (select Room from tModality where tModality.Modality = a.Modality) as  ModalityRoom, " +
                " (select Modality from tuser where tuser.Userguid = a.Registrar) as  Registrar, " +
                " A.ContrastName, " +
                " B.*,A.Modality,A.ExamSystem,A.RegisterDt,A.examineDt, " +
                " A.PreStatus,A.Status ,a.OrderGUID ,F.AccNo, " +
                " (select Text from tDictionaryValue where tDictionaryValue.Value = cast(a.Status as varchar) AND tDictionaryValue.Tag=13 ) as  StatusText,"+
                " (select Text from tDictionaryValue where tDictionaryValue.Value = cast(a.PreStatus as varchar) AND tDictionaryValue.Tag=13) as  PreStatusText,"+
                " A.IsCharge, " +
                " A.Posture ," +
                " A.MedicineUsage " +
                " FROM tRegPatient P,tRegProcedure A,tProcedureCode B ,tRegOrder F" +
                " WHERE A.ProcedureCode=B.ProcedureCode   " +
                " and A.OrderGUID = F.OrderGUID  and P.PatientGuid = F.PatientGuid" +
                string.Format(" AND A.OrderGuid='{0}' ", strOrderGuid);
                if (strRPGuid != null && strRPGuid != string.Empty)
                {
                    strSQL += string.Format(" AND A.ProcedureGuid='{0}' ", strRPGuid);
                }

                strSQL += " Order by B.ModalityType Asc";

                Debug.WriteLine(strSQL);
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "RP";

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
        /// <summary>
        /// Query RP and return data set. The interface 
        /// </summary>
        /// <param name="strOrderGuid"></param>
        /// <param name="ds"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public virtual bool QueryRP(string strOrderGuid, DataSet ds, ref string strError, ref string strRPGuid)
        {
            Debug.WriteLine("QueryRP...");
            bool bReturn = true;
            DataTable dt = new DataTable();

            try
            {
                bReturn = QueryRP(strOrderGuid, ref dt, ref strError, ref strRPGuid);
                ds.Tables.Add(dt);
                return bReturn;

            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        public virtual bool QueryAccNoWithOrderID(string strOrderGuid, ref string strAccNo, ref string strError)
        {
            Debug.WriteLine("QueryAccNoWithOrderID...");
            bool bReturn = true;
            DataTable dt = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {
                if (strOrderGuid == null || strOrderGuid.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }
                string strSQL = string.Format(" SELECT AccNo from tRegOrder where orderGUID = '{0}'", strOrderGuid);

                Debug.WriteLine(strSQL);
                dt = oKodak.ExecuteQuery(strSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        strAccNo = dt.Rows[0]["AccNo"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool QueryRPwithDuplicatedReport(bool bExcludeDeletedReport, string pID, string patientName, string dt1, string dt2, DataSet ds, ref string strError)
        {
            Debug.WriteLine("QueryRPwithDuplicatedReport...");

            bool bReturn = true;

            RisDAL oKodak = new RisDAL();

            try
            {
                string strSQL = " select tRegPatient.patientID tRegPatient__PatientID,"
                    + " tRegPatient.localName tRegPatient__LocalName,"
                    + " tRegPatient.birthday tRegPatient__Birthday,"
                    + " tRegPatient.gender tRegPatient__Gender,"
                    + " tRegPatient.address tRegPatient__Address,"
                    + " cast(tRegPatient.isVIP as varchar(50)) tRegPatient__IsVIP,"
                    + " tRegPatient.telephone tRegPatient__Telephone,"
                    + " tRegVisit.PatientType tRegVisit__PatientType,"
                    + " tRegOrder.AccNo tRegOrder__AccNo,"
                    + " tProcedureCode.Description tProcedureCode__Description,"
                    + " tRegprocedure.procedureGuid tRegprocedure__procedureGuid,"
                    + "(select Room from tModality where tModality.Modality = tRegprocedure.Modality) as  ModalityRoom "
                    + " from tRegPatient, tRegVisit, tRegOrder, tProcedureCode, tRegProcedure"
                    + " where tRegPatient.PatientGuid = tRegVisit.PatientGuid "
                    + " and tRegVisit.VisitGuid = tRegOrder.VisitGuid "
                    + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid "
                    + " and tRegProcedure.ProcedureCode = tProcedureCode.ProcedureCode ";

                if (pID != null && pID.Trim().Length > 0)
                {
                    strSQL += " and tRegPatient.patientID = '" + pID.Trim() + "' ";
                }

                if (patientName != null && patientName.Trim().Length > 0)
                {
                    strSQL += " and tRegPatient.localName like '%" + patientName.Trim() + "%' ";
                }

                if (dt1 != null && dt2 != null && dt1.Trim().Length > 0 && dt2.Trim().Length > 0)
                {
                    try
                    {
                        DateTime date1 = System.Convert.ToDateTime(dt1);
                        DateTime date2 = System.Convert.ToDateTime(dt2);
                        strSQL += string.Format(" and tRegProcedure.ExamineDt between '{0} 00:00:00' and '{1} 23:59:59' ", date1 >= date2 ? date2.ToString("yyyy-MM-dd") : date1.ToString("yyyy-MM-dd"), date1 >= date2 ? date1.ToString("yyyy-MM-dd") : date2.ToString("yyyy-MM-dd"));
                    }
                    catch (Exception ex)
                    {
                        logger.Error(
                            (long)ModuleEnum.QualityControl_DA,
                            ModuleInstanceName.QualityControl,
                            0,
                            ex.Message,
                            Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    }
                }

                strSQL += " and tRegprocedure.ReportGuid in (select procedureGuid "
                    + " from tReport_related_rp, tReport "
                    + " where tReport_related_rp.reportGuid = tReport.reportGuid ";

                strSQL += " group by tReport_related_rp.procedureGuid having count(*) > 1)";

                Debug.WriteLine(strSQL);

                DataTable dt = new DataTable();
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "RP";
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool QueryDuplicatedReport(string rpGuid, DataSet ds, ref string strError)
        {
            Debug.WriteLine("QueryDuplicatedReport...");

            bool bReturn = true;
            if (rpGuid == null || rpGuid.Trim().Length < 1)
            {
                throw (new Exception("null dataset"));
                return false;
            }

            RisDAL oKodak = new RisDAL();

            try
            {
                string strSQL = " select cast(tReport.DeleteMark as varchar(50)) tReport__DeleteMark,"
                    + " tReport.reportGuid tReport__reportGuid,"
                    + " reportName tReport__reportName,"
                    + " cast(tReport.status as varchar(50)) tReport__status,"
                    + " cast(tReport.isPrint as varchar(50)) tReport__isPrint,"
                    + " creater tReport__creater, createDt tReport__createDt,"
                    + " submitter tReport__submitter, submitDt tReport__submitDt,"
                    + " firstApprover tReport__firstApprover, firstApproveDt tReport__firstApproveDt,"
                    + " tReport.mender tReport__mender, tReport.modifyDt tReport__modifyDt,"
                    + " WYS tReport__WYS, WYG tReport__WYG"
                    + " from tReport, tReport_related_rp, tRegProcedure"
                    + " where tRegProcedure.procedureGuid = tReport_related_rp.procedureGuid "
                    + " and tReport_related_rp.reportGuid = tReport.reportGuid "
                    + " and tReport_related_rp.procedureGuid = '" + rpGuid + "' ";

                //if (bExcludeDeletedReport)
                //    strSQL += " and tReport_related_rp.deletemark = 0 and tReport.deletemark = 0";

                Debug.WriteLine(strSQL);

                DataTable dt = new DataTable();
                dt = oKodak.ExecuteQuery(strSQL);
                dt.TableName = "Report";
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
        #region Update
        public virtual bool UpdatePatient(bool bSendToGateWay, string strPatientID, DataSet ds, ref string strError)
        {
            Debug.WriteLine("UpdatePatient...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {
                List<string> listSQL = new List<string>();
                DataTable dtPatient = ds.Tables["Patient"];
                StringBuilder strBuilder = new StringBuilder();

                //nothing need to do. 
                if (dtPatient.Rows.Count <= 0) return false;
                //Patient                
                //bool bSendToRisGateServer = false;
                DataRow drPatient = dtPatient.Rows[0];
                strPatientID = drPatient["PatientID"].ToString();
                string strPatientGuid = drPatient["PatientGuid"].ToString();

                if (!PatientExist(strPatientGuid, ref strError))
                //check patient ID
                {
                    throw new Exception("Can not find patient in database!");
                }


                strBuilder.AppendFormat("UPDATE tRegPatient set LocalName=N'{0}',EnglishName=N'{1}',Birthday='{2}',Gender='{3}',Address='{4}',Telephone='{5}',Comments='{7}',ReferenceNo='{8}',MedicareNo='{9}' where PatientGuid='{10}'",
                drPatient["LocalName"].ToString(), drPatient["EnglishName"].ToString(), ((DateTime)drPatient["Birthday"]).ToString("yyyy-MM-dd"),
                drPatient["Gender"].ToString(), drPatient["Address"].ToString(), drPatient["Telephone"].ToString(),  /*(int)drPatient["IsVIP"],*/"", drPatient["Comments"].ToString(), drPatient["ReferenceNo"].ToString(), drPatient["MedicareNo"].ToString(), drPatient["PatientGuid"].ToString());
                listSQL.Add(strBuilder.ToString());

                /*
                #region  EK_HI00061987 jameswei 2007-11-26
                StringBuilder sb = new StringBuilder();
                sb.Append(" declare @iAgeInDays int");
                sb.AppendFormat(" set @iAgeInDays = datediff(day,(select birthday from tregpatient where patientguid='{0}'),getdate())", drPatient["PatientGuid"].ToString());
                sb.Append(" if(@iAgeInDays < 0)");
                sb.Append(" begin");
                sb.Append(" set @iAgeInDays =0;	");
                sb.Append(" end");
                //to same as register module not plus 1
                //sb.Append(" else");
                //sb.Append(" begin");
                //sb.Append(" set @iAgeInDays = @iAgeInDays+1");
                //sb.Append(" end");
                sb.Append(" begin");
                sb.AppendFormat(" update tregorder set ageindays = @iAgeInDays where patientguid='{0}'", drPatient["PatientGuid"].ToString());
                sb.Append(" end");
                listSQL.Add(sb.ToString());
                Debug.WriteLine(sb.ToString());
                #endregion
                */
                if (bSendToGateWay)
                {
                    if (!UpdatePatientSendToGateServer(dtPatient, listSQL))
                    //fail to send update infomation to gateserver
                    {
                        logger.Warn((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                            "Fail to send update infomation to gateserver", Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    }
                }

                oKodak.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", strSQL), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();

                #region hipaa,log detail patient update info
                logDetailUpdatePatientInfoInHiaa(dtPatient);
                #endregion

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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


        private void logDetailUpdatePatientInfoInHiaa(DataTable dtUpdatedPatient)
        {
            if (dtUpdatedPatient == null)
                return;
            RisDAL dal = new RisDAL();
            string UserRole = HttpContext.Current.Session["RoleName"].ToString();
            string UserGuid = HttpContext.Current.Session["UserGuid"].ToString();
            string UserName = "";
            string IPAddress = "";
            string strMachineName = string.Empty;
            string strMACAddress = string.Empty;
            string strLocation = string.Empty;
            string PatientID = dtUpdatedPatient.Rows[0]["PatientID"].ToString();
            string PatientName = dtUpdatedPatient.Rows[0]["LocalName"].ToString();
            string sqlGetUserIP = string.Format("select MachineIP, MachineName, MACAddress, Location from  tOnlineClient where UserGuid='{0}' and (comments is null or comments != 'web login user') ", UserGuid);
            string sqlGetUserName = string.Format("select LoginName from tUser where UserGuid='{0}'", UserGuid);
            UserName = Convert.ToString(dal.ExecuteScalar(sqlGetUserName));
            DataTable dt = dal.ExecuteQuery(sqlGetUserIP);
            if (dt != null && dt.Rows.Count > 0)
            {
                IPAddress = dt.Rows[0]["MachineIP"].ToString();
                strMachineName = dt.Rows[0]["MachineName"].ToString();
                strMACAddress = dt.Rows[0]["MACAddress"].ToString();
                strLocation = dt.Rows[0]["Location"].ToString();
            }

            string actionDetail = string.Format("UserName:{0},IP:{1},MachineName:{2},MACAddress:{3},Location:{4},Patient:{5},Action:{6}", UserName, IPAddress, strMachineName, strMACAddress, strLocation, PatientName, "Update Patient");
            string updateInfo = "";
            try
            {
                foreach (DataRow dr in dtUpdatedPatient.Rows)
                {
                    foreach (DataColumn dc in dtUpdatedPatient.Columns)
                    {
                        if (dc.DataType == typeof(string))
                        {

                            if (dr[dc, DataRowVersion.Original] == DBNull.Value)//original is null and current is not null
                            {
                                updateInfo += dc.ColumnName + ":" + "NULL->" + dr[dc].ToString() + ",";
                            }
                            else if (dr[dc] == DBNull.Value)
                            {
                                updateInfo += dc.ColumnName + ":" + dr[dc].ToString() + "->NULL" + ",";
                            }
                            else if (dr[dc, DataRowVersion.Original].ToString() != dr[dc].ToString())//both have no null value
                            {
                                updateInfo += dc.ColumnName + ":" + dr[dc, DataRowVersion.Original] + "->" + dr[dc].ToString() + ",";
                            }

                        }
                        else if (dc.DataType == typeof(DateTime))
                        {
                            if (dr[dc, DataRowVersion.Original] == DBNull.Value)//original is null and current is not null
                            {
                                updateInfo += dc.ColumnName + ":" + "NULL->" + dr[dc].ToString() + ",";
                            }
                            else if (dr[dc] == DBNull.Value)
                            {
                                updateInfo += dc.ColumnName + ":" + dr[dc].ToString() + "->NULL" + ",";
                            }
                            else if (dr[dc, DataRowVersion.Original].ToString() != dr[dc].ToString())//both have no null value
                            {
                                updateInfo += dc.ColumnName + ":" + dr[dc, DataRowVersion.Original] + "->" + dr[dc].ToString() + ",";
                            }
                        }
                        else if (dc.DataType == typeof(int))
                        {
                            if (dr[dc, DataRowVersion.Original] == DBNull.Value)//original is null and current is not null
                            {
                                updateInfo += dc.ColumnName + ":" + "NULL->" + dr[dc].ToString() + ",";
                            }
                            else if (dr[dc] == DBNull.Value)
                            {
                                updateInfo += dc.ColumnName + ":" + dr[dc].ToString() + "->NULL" + ",";
                            }
                            else if (dr[dc, DataRowVersion.Original].ToString() != dr[dc].ToString())//both have no null value
                            {
                                updateInfo += dc.ColumnName + ":" + dr[dc, DataRowVersion.Original] + "->" + dr[dc].ToString() + ",";
                            }
                        }
                    }
                }
                if (updateInfo.Length > 0)
                {
                    updateInfo = updateInfo.TrimEnd(",".ToCharArray());
                }
                actionDetail += ",UpdateInfo:(" + updateInfo + ")";
                Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientRecordEvtMsg(CommonGlobalSettings.HippaName.ActionCode.Update, "Update Patient", UserGuid, UserName, UserRole, IPAddress, PatientID, PatientName, actionDetail, "Update Patient information in QC module", true);

            }
            catch (Exception ex)
            {
                logger.Error((long)ModuleEnum.Register_DA, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),
                (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientRecordEvtMsg(CommonGlobalSettings.HippaName.ActionCode.Update, "Update Patient", UserGuid, UserName, UserRole, IPAddress, PatientID, PatientName, actionDetail, "Update Patient information in QC module", true);
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
        }
        /// <summary>
        /// Implementition for Update Order interface
        /// </summary>
        /// <param name="bSendToGateWay"></param>
        /// <param name="strOrderGuid"></param>
        /// <param name="ds"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public virtual bool UpdateOrder(bool bSendToGateWay, string strOrderGuid, DataSet ds, ref string strError)
        {
            Debug.WriteLine("UpdateOrder...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            try
            {
                DataTable dtOrder;
                DataTable dtExt;

                List<string> listSQL = new List<string>();
                dtOrder = ds.Tables["Order"];
                StringBuilder strBuilder = new StringBuilder();

                //nothing need to do. 
                if (dtOrder.Rows.Count <= 0) return false;

                //Visit   
                bool patientChanged = false;
                string existsSql = string.Format("select 1 from tregorder where orderguid ='{0}' and patienttype ='{1}'", Convert.ToString(dtOrder.Rows[0]["OrderGuid"]),Convert.ToString(dtOrder.Rows[0]["PatientType"]));
                string selectWarningTimeSql = string.Format("select warningtime from twarningtime where modalitytype=(select top 1 modalitytype from tregprocedure where orderguid = '{0}' ) and patienttype='{1}' and site='{2}'", Convert.ToString(dtOrder.Rows[0]["OrderGuid"]), Convert.ToString(dtOrder.Rows[0]["PatientType"]), CommonGlobalSettings.Utilities.GetCurSite());
                string defaultWarningTimeSql = @"  if exists(select value from tSiteProfile where ModuleID ='0400' and Name ='DefaultWarningTime')
                                                   select value from tSiteProfile where ModuleID ='0400' and Name ='DefaultWarningTime'
                                                   else 
                                                   select value from tSystemProfile where ModuleID ='0400' and Name ='DefaultWarningTime'";
                string updateRPWarningTimeSql = "update tregprocedure set warningtime ='{0}' where orderguid ='{1}'";
                int warningTime = 0;
                DataTable dtWarningTime = new DataTable();
                bool needUpdateWarningTime = false;
                object obj = oKodak.ExecuteScalar(existsSql, RisDAL.ConnectionState.KeepOpen);
                patientChanged = Convert.ToInt32(obj) >= 1 ? false : true;

                if (patientChanged)
                {
                    oKodak.ExecuteQuery(selectWarningTimeSql, RisDAL.ConnectionState.KeepOpen, dtWarningTime);
                    if (dtWarningTime == null || dtWarningTime.Rows.Count == 0)
                    {
                        oKodak.ExecuteQuery(defaultWarningTimeSql, RisDAL.ConnectionState.KeepOpen, dtWarningTime);
                        if (dtWarningTime != null && dtWarningTime.Rows.Count > 0)
                        {
                            int.TryParse(Convert.ToString(dtWarningTime.Rows[0]["WarningTime"]), out warningTime);
                            needUpdateWarningTime = true;
                        }
                    }
                    else
                    {
                        int.TryParse(Convert.ToString(dtWarningTime.Rows[0]["WarningTime"]), out warningTime);
                        needUpdateWarningTime = true;
                    }
                }

                DataRow drVisit = dtOrder.Rows[0];
                strBuilder.Remove(0, strBuilder.Length);
                strBuilder.AppendFormat("UPDATE tRegOrder SET InhospitalNo='{0}',ClinicNo='{1}',PatientType='{2}',Observation='{3}',HealthHistory='{4}',InhospitalRegion='{5}',IsEmergency={6},BedNo='{7}',Comments='{8}',ChargeType ='{9}',BedSide={10} Where OrderGuid='{11}'",
                    drVisit["InhospitalNo"].ToString(), drVisit["ClinicNo"].ToString(), drVisit["PatientType"].ToString(), Convert.ToString(drVisit["Observation"]),
                    Convert.ToString(drVisit["HealthHistory"]), drVisit["InhospitalRegion"].ToString(), Convert.ToInt32(drVisit["IsEmergency"]), drVisit["BedNo"].ToString(),
                    Convert.ToString(drVisit["Comments"]), drVisit["ChargeType"].ToString(),Convert.ToInt32(drVisit["BedSide"]), drVisit["OrderGuid"].ToString());
                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                        strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                listSQL.Add(strBuilder.ToString());

                //Order                
                DataRow drOrder = drVisit;
                strBuilder.Remove(0, strBuilder.Length);
                strBuilder.AppendFormat("UPDATE tRegOrder SET ApplyDept='{0}',ApplyDoctor='{1}',Comments='{2}' WHERE OrderGuid='{3}'",
                    drOrder["ApplyDept"].ToString(), drOrder["ApplyDoctor"].ToString(), drOrder["Comments"].ToString(), drOrder["OrderGuid"].ToString());
                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                        strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                listSQL.Add(strBuilder.ToString());

                //if need update rp's warningtime
                if (needUpdateWarningTime)
                {
                    listSQL.Add(string.Format(updateRPWarningTimeSql, warningTime, Convert.ToString(drVisit["OrderGuid"])));
                }

                //Send to RIS Gateway Server  update information                               
                if (bSendToGateWay)
                {
                    if (!UpdateOrderSendToGateServer(dtOrder, listSQL))
                    //fail to send update infomation to gateserver
                    {
                        logger.Warn((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                            "Fail to send update infomation to gateserver", Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    }
                }
                listSQL.Add(string.Format("update tRegProcedure set ExamineDt = '{0}' where OrderGuid = '{1}' and Status >= 50", drOrder["ExamineDt"].ToString(), drOrder["OrderGuid"].ToString()));

                oKodak.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", strSQL), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        /// <summary>
        /// Implementation for UpdateRP interface
        /// </summary>
        /// <param name="bSendToGateWay"></param>
        /// <param name="strRPGuid"></param>
        /// <param name="ds"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public virtual bool UpdateRP(bool bSendToGateWay, string strRPGuid, DataSet ds, ref string strError)
        {
            Debug.WriteLine("UpdateRP...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            try
            {
                DataTable dtExt;


                List<string> listSQL = new List<string>();
                dtExt = ds.Tables["RP"];
                StringBuilder strBuilder = new StringBuilder();

                //Visit                
                DataRow drExt = dtExt.Rows[0];
                strBuilder.Remove(0, strBuilder.Length);
                strBuilder.AppendFormat("UPDATE tRegProcedure SET FilmSpec='{0}',FilmCount='{1}',ContrastName='{2}',ContrastDose='{3}',  ExposalCount='{4}',Charge='{5}',Technician='{6}',TechDoctor='{7}',TechNurse='{8}',Posture ='{9}',MedicineUsage ='{10}', Technician1='{11}',Technician2='{12}',Technician3='{13}',Technician4='{14}' Where ProcedureGuid='{15}'",
                    drExt["FilmSpec"].ToString(), drExt["FilmCount"].ToString(), drExt["ContrastName"].ToString(), drExt["ContrastDose"].ToString(),
                    drExt["ExposalCount"].ToString(), drExt["Charge"].ToString(), drExt["TechGUID"], drExt["DoctorGUID"].ToString(),
                    drExt["NurseGUID"].ToString(), drExt["Posture"].ToString(), drExt["MedicineUsage"].ToString(), drExt["TechGUID1"].ToString(), drExt["TechGUID2"].ToString(), drExt["TechGUID3"].ToString(), drExt["TechGUID4"].ToString(), drExt["ProcedureGuid"].ToString());
                listSQL.Add(strBuilder.ToString());

                //send to gateway server
                if (bSendToGateWay)
                {
                    if (!UpdateRPSendToGateServer(dtExt, listSQL))
                    //fail to send update infomation to gateserver
                    {
                        logger.Warn((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                            "Fail to send update infomation to gateserver", Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    }
                }

                //Send to RIS Gateway Server  update information                               
                oKodak.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                        string.Format("EXECUTE SQL IN QC: {0}", strSQL), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool UpdateActiveReport(string reportGuid, ref string strError)
        {
            Debug.WriteLine("UpdateActiveReport...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {
                // EK_HI00059995

                string strSQL = " begin tran \r\n"
                    + " update tReport set deletemark = 1"
                    + " where reportGuid in (select a.reportGuid from treport_related_rp a, treport_related_rp b"
                    + " where a.procedureGuid = b.procedureGuid"
                    + " and b.reportGuid = '" + reportGuid.Trim() + "'"
                    + " and a.reportGuid <> '" + reportGuid.Trim() + "') \r\n"
                    + " update tReport set deletemark = 0"
                    + " where reportGuid='" + reportGuid.Trim() + "'"
                    + " update tReport_related_rp set deletemark = 1"
                    + " where reportGuid in (select a.reportGuid from treport_related_rp a, treport_related_rp b"
                    + " where a.procedureGuid = b.procedureGuid"
                    + " and b.reportGuid = '" + reportGuid.Trim() + "'"
                    + " and a.reportGuid <> '" + reportGuid.Trim() + "') \r\n"
                    + " update tReport_related_rp set deletemark = 0"
                    + " where reportGuid='" + reportGuid.Trim() + "' \r\n"
                    + " update tRegProcedure set status = tReport.status from tRegProcedure, tReport_related_rp, tReport"
                    + " where tRegProcedure.procedureGuid=tReport_related_rp.procedureGuid and tReport_related_rp.reportGuid=tReport.reportGuid and tReport.reportGuid='" + reportGuid.Trim() + "' \r\n"
                    + " commit";

                Debug.WriteLine(strSQL);
                oKodak.ExecuteNonQuery(strSQL);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
        #region Delete
        /// <summary>
        /// Implementation for DeleteOrder interface
        /// </summary>
        /// <param name="SendToGateWay"></param>
        /// <param name="strOrderGuid"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public virtual bool DeleteOrder(bool SendToGateWay, string strOrderGuid,string strLoginName,string strLocalName,ref string strError, ref string strRPGuid)
        {
            Debug.WriteLine("DeleteOrder...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            string strAccNo = string.Empty;
            string strPatientID = string.Empty;
            string strPatientName = string.Empty;

            try
            {
                List<string> listSQL = new List<string>();
                string stVisitGUID;                
                //EK_HI00063904 jameswei 2007-12-12
                string strSQL = string.Format("Select A.VisitGUID,A.AccNo,B.PatientID,B.LocalName from tRegOrder A,tRegPatient B where A.PatientGuid = B.PatientGuid and A.OrderGUID ='{0}'", strOrderGuid);
                Debug.WriteLine(strSQL);
                oKodak.ExecuteQuery(strSQL, RisDAL.ConnectionState.KeepOpen, dt);
                if (dt.Rows.Count > 0)
                {
                    stVisitGUID = dt.Rows[0]["VisitGUID"].ToString();
                    strAccNo = dt.Rows[0]["AccNo"].ToString();
                    strPatientID = dt.Rows[0]["PatientID"].ToString();
                    strPatientName = dt.Rows[0]["LocalName"].ToString();
                    dt.Clear();
                }
                else
                //order not  found, maybe already removed
                {
                    return true;
                }


                strSQL = string.Format("select 1 from tReport A,tregprocedure B where	A.ReportGuid = B.ReportGuid ")
                        + string.Format(" and  B.OrderGUID ='{0}'", strOrderGuid);
                Debug.WriteLine(strSQL);
                oKodak.ExecuteQuery(strSQL, dt);
                if (dt.Rows.Count > 0)
                //if the order has been reported
                //it can not be removed
                {
                    strError = "Order can not be removed because it has been reported!";
                    return false;
                }

                if (SendToGateWay)
                {
                    DeleteOrderSendToGateServer(strOrderGuid, stVisitGUID, listSQL, strRPGuid ,strLoginName,strLocalName);
                }

                //delete 
                #region EK_HI00063904 jameswei 2007-12-12 delete the requisition
                //strSQL = string.Format("Select relativepath,filename from tRequisition where AccNo ='{0}'", strAccNo);
                //if(dt != null)
                //{
                //    dt.Clear();
                //}
                //query the rquistion's save path and filename
                //oKodak.ExecuteQuery(strSQL, dt);
                //if(dt.Rows.Count >0)
                //{
                //    FtpClient fcObject = new FtpClient();
                //    bool bDeleteSucc = false;
                //    //delete all requistion file belong to that acc
                //    foreach(DataRow dr in dt.Rows)
                //    {
                //       bDeleteSucc = fcObject.DeleteFile(dr["relativepath"].ToString() + "/" + dr["filename"].ToString());
                //       if(!bDeleteSucc)
                //       {
                //            throw new Exception("Requisition file delete fail!");
                //       }
                //    }
                //}
                strSQL = string.Format("DELETE FROM tRequisition WHERE AccNo='{0}'", strAccNo);
                listSQL.Add(strSQL);
                #endregion
                //                strSQL = string.Format("DELETE tReport_related_rp where procedureGuid in (select procedureGuid from tRegProcedure where OrderGuid='{0}')", strOrderGuid);
                //              listSQL.Add(strSQL);
                strSQL = string.Format("DELETE tRegProcedure where OrderGuid='{0}'", strOrderGuid);
                listSQL.Add(strSQL);
                strSQL = string.Format("DELETE tRegOrder where OrderGuid='{0}'", strOrderGuid);
                listSQL.Add(strSQL);

                strSQL = string.Format("DELETE tAccessionNumberList where OrderGuid='{0}'", strOrderGuid);
                listSQL.Add(strSQL);

                dt.Clear();

                //get data for gateway
              

                //delete visit wihout order
                strSQL = string.Format("Select count(*) as OrderNum from tRegOrder where VisitGUID ='{0}'", stVisitGUID);
                Debug.WriteLine(strSQL);
                oKodak.ExecuteQuery(strSQL, dt);

                if (dt.Rows.Count > 0)
                {
                    //if (Convert.ToInt32(dt.Rows[0]["OrderNum"].ToString()) == 1)
                    //{
                    //    strSQL = string.Format("DELETE tRegVisit where VisitGUID='{0}'", stVisitGUID);
                    //    listSQL.Add(strSQL);
                    //}
                }
                else
                //order not  found, maybe already removed
                {
                    return true;
                }

                oKodak.BeginTransaction();
                foreach (string strSQL1 in listSQL)
                {
                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL1, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();
                Server.Utilities.HippaLogTool.HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.Delete, strAccNo, strPatientID, strPatientName, "", true);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                Server.Utilities.HippaLogTool.HippaLogTool.AuditOrderRecordEvtMsg(ActionCode.Delete, strAccNo, strPatientID, strPatientName, "", false);
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

        #region EK_HI00063904 jameswei 2007-12-13 get the acc's requisition file name and relativepath
        public virtual bool GetRequisitionInfo(string strAccNo, DataSet ds)
        {
            RisDAL oKodak = new RisDAL();
            bool bReturn;
            string strSQL = string.Format("Select relativepath,filename from tRequisition where AccNo ='{0}'", strAccNo);
            Debug.WriteLine(strSQL);
            try
            {
                DataTable dt = oKodak.ExecuteQuery(strSQL);
                ds.Tables.Add(dt);
                bReturn = true;
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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


        public virtual bool DeleteRP(bool sendToGW, string strVisitGuid, string strOrderGuid, string strRPs, string strLoginName,string strLocalName,BaseActionResult bar)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            string strPatientID = string.Empty;
            string strPatientName = string.Empty;
            string strAccNo = string.Empty;
            string strProcedureCode = string.Empty;
            try
            {
                #region for hippa
                string strSQL = string.Format("SELECT tRegPatient.PatientID,tRegPatient.LocalName,tRegPatient.PatientGuid,tRegOrder.VisitGuid,tRegOrder.AccNo,tRegOrder.OrderGuid,tRegProcedure.Status,tRegProcedure.ProcedureGuid,tRegProcedure.ProcedureCode FROM tRegPatient,tRegOrder,tRegProcedure WHERE tRegPatient.PatientGuid=tRegOrder.PatientGuid and tRegOrder.OrderGuid=tRegProcedure.OrderGuid and tRegProcedure.ProcedureGuid='{0}'", strRPs);
                oKodak.ExecuteQuery(strSQL, RisDAL.ConnectionState.KeepOpen, dt);
                if (dt == null || dt.Rows.Count == 0)
                {
                    throw new Exception("This RP no exists");
                }

                            
                //{RC191 Bruce deng 20111114
                strSQL = string.Format("SELECT count(*) FROM tOrderCharge where OrderGuid='{0}' and laststatus!=21 and laststatus!=31 ", strOrderGuid);

                Object obj = oKodak.ExecuteScalar(strSQL);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {//exist the fee
                    strSQL = string.Format("SELECT count(*) FROM tRegProcedure where OrderGuid='{0}'", strOrderGuid);
                    obj = oKodak.ExecuteScalar(strSQL);
                    if (obj != null && Convert.ToInt32(obj) == 1)
                    {
                        bar.recode = -1;
                        bar.ReturnMessage = "Can not delete the last RP due to exist order fee items";
                        throw new Exception("Can not delete the last RP due to exist order fee items");
                    }


                }
                //}

                DataRow dr = dt.Rows[0];
                strPatientID = Convert.ToString(dr["PatientID"]);
                strPatientName = Convert.ToString(dr["LocalName"]);
                strAccNo = Convert.ToString(dr["AccNo"]);
                strProcedureCode = Convert.ToString(dr["ProcedureCode"]);
                #endregion

                #region EK_HI00073389
                List<string> listSQL = new List<string>();
                if (sendToGW)
                {
                    DeleteOrderSendToGateServer(strOrderGuid, strVisitGuid, listSQL, strRPs,strLoginName,strLocalName);
                }
                #endregion
                string[] rplist = strRPs.Split(',');

                string ss2 = "";
                foreach (string ss1 in rplist)
                {
                    ss2 += "'" + ss1 + "', ";
                }

                ss2 = ss2.Trim(", ".ToCharArray());
                #region  EK_HI00069464 2008-04-15
                //**old
                //string sql = "delete from tReport_related_rp where procedureGuid in (" + ss2 + ") \r\n"
                //    + " delete from tRegProcedure where procedureGuid in (" + ss2 + ")";
                #region EK_HI00073389
                /*
                string sql = "delete from tReShot where procedureGuid in (" + ss2 + ") \r\n"
                    + "delete from tReport_related_rp where procedureGuid in (" + ss2 + ") \r\n"
                    + " delete from tRegProcedure where procedureGuid in (" + ss2 + ")";
                 * */




                string SQLTemp = "delete from tRegProcedure where procedureGuid in (" + ss2 + ")";
                listSQL.Add(SQLTemp);
                SQLTemp = "delete from tReShot where procedureGuid in (" + ss2 + ")";
                listSQL.Add(SQLTemp);
                SQLTemp = "delete from tReport where ReportGuid in (select reportguid from tregprocedure where procedureguid in(" + ss2 + "))";
                listSQL.Add(SQLTemp);
                oKodak.BeginTransaction();
                foreach (string str in listSQL)
                {
                    oKodak.ExecuteNonQuery(str, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();
                #endregion
                #endregion
                //oKodak.ExecuteNonQuery(sql);
                Server.Utilities.HippaLogTool.HippaLogTool.AuditProcedureRecordEvtMsgQC(ActionCode.Delete, strAccNo, strProcedureCode, strPatientID, strPatientName, "PatientID(" + strPatientID + ") PatientName(" + strPatientName + ") ProcedureCode(" + strProcedureCode + ")", true);

                return true;
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                Server.Utilities.HippaLogTool.HippaLogTool.AuditProcedureRecordEvtMsgQC(ActionCode.Delete, strAccNo, strProcedureCode, strPatientID, strPatientName, "PatientID(" + strPatientID + ") PatientName(" + strPatientName + ") ProcedureCode(" + strProcedureCode + ")", false);
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

        public virtual bool DeletePatient(bool sendToGateway, string patientGuid)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            string patientName = string.Empty;
            string patientID = string.Empty;
            try
            {
                DataSet ds = new DataSet();
                string sqlHas2PatientRelated = "select relatedid from tPatientList where relatedid =(select distinct relatedid from tPatientList where patientguid ='{0}') and len(relatedid) >0";
                string sqlUpdate2PatientRelatedID = "update tregpatient set relatedid ='' where relatedid ='{0}'";
                string sql = " delete from tRegPatient where patientGuid = '" + patientGuid.Trim() + "'";
                string sqlpatientlist = "if exists(select * from tPatientList where patientGuid = '"+patientGuid.Trim()+"' and archive=0) "+ " delete from tPatientList where patientGuid = '" + patientGuid.Trim() + "'"  ;
                string sqlExistsOrder = "select count(1) from tRegOrder o where o.patientGuid ='" + patientGuid.Trim() + "'";
                string sqlPatient = "select PatientID,LocalName from tRegPatient where PatientGuid ='" + patientGuid.Trim() + "'";

                object obj = oKodak.ExecuteScalar(sqlExistsOrder, RisDAL.ConnectionState.KeepOpen);
                if (obj != null && Convert.ToInt32(obj) > 0)
                {
                    return false;
                }

                #region for hippa
                DataTable dtPatient = oKodak.ExecuteQuery(sqlPatient);
                if (dtPatient != null && dtPatient.Rows.Count > 0)
                {
                    patientID = dtPatient.Rows[0]["PatientID"].ToString();
                    patientName = dtPatient.Rows[0]["LocalName"].ToString();
                }
                #endregion
                #region EK_HI00073389
                if (sendToGateway)
                {
                    List<string> listSQL = new List<string>();
                    DeletePatientSendToGateServer(patientGuid, listSQL);
                    oKodak.BeginTransaction();
                    foreach (string str in listSQL)
                    {
                        oKodak.ExecuteNonQuery(str, RisDAL.ConnectionState.KeepOpen);
                    }
                    oKodak.CommitTransaction();
                }
                #endregion
                #region Updated RelatedID if has two less patient are related
                DataTable dt = oKodak.ExecuteQuery(string.Format(sqlHas2PatientRelated, patientGuid), RisDAL.ConnectionState.KeepOpen);
                if(dt != null && dt.Rows.Count >0)
                {
                    if (dt.Rows.Count > 0 && dt.Rows.Count <= 2)
                    {
                        string relatedID = dt.Rows[0][0].ToString();
                        oKodak.ExecuteNonQuery(string.Format(sqlUpdate2PatientRelatedID, relatedID), RisDAL.ConnectionState.KeepOpen);
                    }
                }
                #endregion
                oKodak.ExecuteNonQuery(sql, RisDAL.ConnectionState.KeepOpen);
                oKodak.ExecuteNonQuery(sqlpatientlist, RisDAL.ConnectionState.KeepOpen);
                Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientRecordEvtMsg(ActionCode.Delete, patientID, patientName, "", true);
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientRecordEvtMsg(ActionCode.Delete, patientID, patientName, "", false);
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
        #region Merge
        //Assign one order belongs to one patient to another
        //This occurs when the order is registered to incorrect patient.
        public virtual bool AssignOrderToPatient(bool bSendToGateWay, string strVisitGuid, string strOrderGuid, string strPatientGUID, string bDelAfterMerge, ref string strError)
        {
            Debug.WriteLine("AssignOrderToPatient...");
            bool bReturn = true;
            DataSet dsTemp = new DataSet();
            DataTable dt = new DataTable();
            DataTable dtPatient = new DataTable();
            //DataTable dtCountVisits = new DataTable();
            DataTable dtCoutOrders = new DataTable();
            DataTable dtRPMerge;    //RP of all other pateints
            DataTable dtPatientSrc = new DataTable();
            string stSQL = "";
            dtRPMerge = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {
                if (strVisitGuid == null || strVisitGuid.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }
                if (strOrderGuid == null || strOrderGuid.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }
                if (strPatientGUID == null || strPatientGUID.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }

                string strPatientID = "";
                string strPatientName = "";
                //valid patient
                if (!PatientExist(strPatientGUID, ref dtPatient, ref strError))
                //check patient ID
                {
                    throw new Exception("Can not find patient in database!");
                }

                List<string> listSQL = new List<string>();

                //get count of visits those belongs to this patient
                //string stSQL = "select count(*) as VisitCounts,patientGUID from tRegVisit where patientGUID = (select patientGUID from tRegVisit where VisitGuid  = '" + strVisitGuid + "')  group by patientGUID";
                //Debug.WriteLine(stSQL);
                //dtCountVisits = oKodak.ExecuteQuery(stSQL);

                //get cout of order not = this order for delete use
                if (PatientExist(strPatientGUID, ref dtPatientSrc, ref strError))
                {
                    stSQL = "select count(*) as OrderCounts,patientGUID from tRegOrder where patientGUID = (select patientGUID from tRegOrder where OrderGuid  = '" + strOrderGuid + "')  group by patientGUID";
                    Debug.WriteLine(stSQL);
                    dtCoutOrders = oKodak.ExecuteQuery(stSQL);
                }
                //string stSQL = "select count(*) as VisitCounts,patientGUID from tRegVisit where patientGUID = (select patientGUID from tRegVisit where VisitGuid  = '" + strVisitGuid + "')  group by patientGUID";
                //Debug.WriteLine(stSQL);
                //dtCountVisits = oKodak.ExecuteQuery(stSQL);

                stSQL = string.Format("Update tRegOrder set PatientGuid = '{0}' where OrderGuid = '{1}'", strPatientGUID, strOrderGuid);
                //stSQL = string.Format("Update tRegVisit set PatientGuid = '{0}' where VisitGuid = '{1}'", strPatientGUID, strVisitGuid);
                //stSQL = string.Format("Update tRegOrder set VisitGuid = (select top(1)VisitGuid from tregvisit where patientguid='{0}') where OrderGuid = '{1}'", strPatientGUID, strOrderGuid);
                listSQL.Add(stSQL);

                if (bSendToGateWay)
                //get all dataset of all RP already belongs to source order
                {
                    //QueryRPWithVisitGUIDForGateWay(strVisitGuid, dtRPMerge);
                    string str = "";
                    QueryRP(strOrderGuid, dsTemp, ref strError, ref str);
                    MergePatientSendToGateServer(dsTemp.Tables["RP"], dtPatient, listSQL, true);
                }

                //remove patient if all orders have been removed
                if ((dtCoutOrders.Rows[0]["OrderCounts"].ToString() == "1")
                    && (bDelAfterMerge == "True"))
                {
                    stSQL = string.Format("delete tRegPatient where PatientGuid = '{0}'", dtCoutOrders.Rows[0]["PatientGuid"]);
                    listSQL.Add(stSQL);

                    stSQL = string.Format("if exists(select * from tPatientList where patientGuid = '{0}' and archive=0)  delete tPatientList where PatientGuid = '{1}'", dtCoutOrders.Rows[0]["PatientGuid"],dtCoutOrders.Rows[0]["PatientGuid"]);
                    listSQL.Add(stSQL);

                    if (bSendToGateWay)
                    {
                        DeletePatientSendToGateServer(dtCoutOrders.Rows[0]["patientGUID"].ToString(), listSQL);
                    }
                }



                oKodak.BeginTransaction();
                //update database, seperate the visit from old patient and merge it to new patient
                foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", strSQL), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }


                oKodak.CommitTransaction();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
                if (dtCoutOrders != null)
                {
                    dtCoutOrders.Dispose();
                }
                if (dtRPMerge != null)
                {
                    dtRPMerge.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        public bool MergePatients(bool bSendToGateWay, Dictionary<int, string> dicPatients, string bDelAfterMerge, ref string strTargetPatientGUID, ref string strTargetPatientName, ref string strPatientsList, ref string strError)
        {
            Debug.WriteLine("MergePatients...");
            bool bReturn = true;
            DataTable dt = new DataTable();
            DataTable dtPatient = new DataTable();
            DataTable dtCountVisits = new DataTable();
            DataTable dtRPMerge;    //RP of all other pateints
            dtRPMerge = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {
                if (dicPatients == null)
                //no input
                {
                    throw new Exception("Invalid input para");
                }

                if (dicPatients.Count < 2)
                //no enough patients
                {
                    throw new Exception("Invalid input para");
                }


                //get target patient
                string strTempPatientName = "";
                string strTargetPatientID = "";
                dicPatients.TryGetValue(1, out strTargetPatientGUID);
                //valid patient
                if (!PatientExist(strTargetPatientGUID, ref dtPatient, ref strError))
                //check patient ID
                {
                    throw new Exception("Can not find patient in database!");
                }

                //build sql
                StringBuilder strBuilder = new StringBuilder();
                List<string> listSQL = new List<string>();
                string outPatientGUID;
                Dictionary<int, string> dicVisitGUIDs = new Dictionary<int, string>();
                strPatientsList = "";

                for (int i = 2; i < dicPatients.Count + 1; i++)
                {
                    dicPatients.TryGetValue(i, out outPatientGUID);
                    if (outPatientGUID.Length == 0)
                    {
                        throw new Exception("Invalid input para");
                    }
                    if (PatientExist(outPatientGUID, ref strTempPatientName, ref strError))
                    {
                        strPatientsList = strPatientsList + string.Format("{0}({1}) ", outPatientGUID, strTempPatientName);
                        strBuilder.Remove(0, strBuilder.Length);
                        strBuilder.AppendFormat("UPDATE tRegOrder SET PatientGuid='{0}' Where PatientGuid='{1}'", strTargetPatientGUID, outPatientGUID);
                        listSQL.Add(strBuilder.ToString());
                        if (bDelAfterMerge == "True")
                        {
                            strBuilder.Remove(0, strBuilder.Length);
                            strBuilder.AppendFormat("Delete tRegPatient  Where PatientGuid='{0}'", outPatientGUID);
                            listSQL.Add(strBuilder.ToString());

                            string stSQL = string.Format("if exists(select * from tPatientList where patientGuid = '{0}' and archive=0)  delete tPatientList where PatientGuid = '{1}'", outPatientGUID,outPatientGUID);
                            listSQL.Add(stSQL);

                            if (bSendToGateWay)
                            {
                                DeletePatientSendToGateServer(outPatientGUID, listSQL);
                            }
                        }
                        if (bSendToGateWay)
                        //get all dataset of all RP already belongs to target patients
                        {
                            QueryRPWithPatientGUIDForGateWay(outPatientGUID, dtRPMerge);
                        }
                    }
                }

                if (bSendToGateWay)
                {
                    MergePatientSendToGateServer(dtRPMerge, dtPatient, listSQL, false);
                }
                //update database
                oKodak.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", strSQL), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();

                //send to ris gateway server

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
                if (dtCountVisits != null)
                {
                    dtCountVisits.Dispose();
                }
                if (dtRPMerge != null)
                {
                    dtRPMerge.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        #region new add merge EK_HI00085711
        //merge some source patients to one target patient
        /// <summary>
        /// merge patients to target patient
        /// </summary>
        /// <param name="dicPatients">the source patients to be merged</param>
        /// <param name="bDelAfterMerge">wether delete the source patients after merge</param>
        /// <param name="strTargetPatientGUID">the destination patient guid to merged to</param>
        /// <param name="bSendToGateWay">wether to notify gateway</param>
        /// <param name="strError">the function isued error</param>
        /// <returns>if successfull return true else false</returns>
        public bool MergePatients(Dictionary<string, string> dicPatients, string bDelAfterMerge, string strTargetPatientGUID, bool bSendToGateWay, ref string strError)
        {
            Debug.WriteLine("MergePatients...");
            bool bReturn = true;
            DataTable dtTargetPatient = new DataTable();
            DataTable dtRPMerge = new DataTable();
            StringBuilder strBuilder = new StringBuilder();
            List<string> listSQL = new List<string>();
            string srcPatientGUID;
            DataTable dtTemp = new DataTable();
            RisDAL oKodak = new RisDAL();
           
            string sqlUpdate2PatientRelatedID = "update tregpatient set relatedid ='' where patientguid ='{0}'";

            try
            {
                //do sync check
                bReturn = CheckOperationSync(dicPatients, ref strError, 0, strTargetPatientGUID, ref dtTemp);
                if (!bReturn) return false;

                //get the target patient datatable
                QueryPatientWithPatientGuid(strTargetPatientGUID, ref dtTargetPatient, ref strError);

                //merge all RP in patient dictionary to target patient
                foreach (string strKey in dicPatients.Keys)
                {
                    srcPatientGUID = strKey;
                    strBuilder.Remove(0, strBuilder.Length);
                    strBuilder.AppendFormat("UPDATE tRegOrder SET PatientGuid='{0}' Where PatientGuid='{1}'", strTargetPatientGUID, srcPatientGUID);
                    listSQL.Add(strBuilder.ToString());
                    if (bDelAfterMerge == "True")
                    {
                        #region Updated RelatedID to empty if the two patient only related
                        if (HasJustTwoPatientRelated(srcPatientGUID, strTargetPatientGUID))
                        {
                            oKodak.ExecuteNonQuery(string.Format(sqlUpdate2PatientRelatedID, strTargetPatientGUID), RisDAL.ConnectionState.KeepOpen);
                        }
                        #endregion
                        strBuilder.Remove(0, strBuilder.Length);
                        strBuilder.AppendFormat("Delete tRegPatient  Where PatientGuid='{0}'", srcPatientGUID);
                        listSQL.Add(strBuilder.ToString());

                        string stSQL = string.Format("if exists(select * from tPatientList where patientGuid = '{0}' and archive=0) delete tPatientList where PatientGuid = '{1}'",srcPatientGUID, srcPatientGUID);
                        listSQL.Add(stSQL);

                        if (bSendToGateWay)
                        {
                            DeletePatientSendToGateServer(srcPatientGUID, listSQL);
                        }
                    }
                    if (bSendToGateWay)
                    //get all dataset of all RP already belongs to target patients
                    {
                        QueryRPWithPatientGUIDForGateWay(srcPatientGUID, dtRPMerge);
                    }
                }

                if (bSendToGateWay)
                {
                    MergePatientSendToGateServer(dtRPMerge, dtTargetPatient, listSQL, false);
                }
                //update database
                oKodak.BeginTransaction();

                // 2015-08-26, Oscar added (US26267), Need to run "Update" first.
                int updateIndex;
                while ((updateIndex = listSQL.FindIndex(s => s.StartsWith("UPDATE tRegOrder", StringComparison.OrdinalIgnoreCase))) >= 0)
                {
                    var statement = listSQL[updateIndex];
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", statement), Application.StartupPath,
                        (new StackFrame(true)).GetFileName(), (new StackFrame(true)).GetFileLineNumber());
                    Debug.WriteLine(statement);
                    oKodak.ExecuteNonQuery(statement, RisDAL.ConnectionState.KeepOpen);
                    listSQL.RemoveAt(updateIndex);
                }

                foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", strSQL), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();

                //send to ris gateway server

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dtTemp != null)
                {
                    dtTemp.Dispose();
                }
                if (dtTargetPatient != null)
                {
                    dtTargetPatient.Dispose();
                }
                if (dtRPMerge != null)
                {
                    dtRPMerge.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        /// <summary>
        /// HasJustTwoPatientRelated
        /// </summary>
        /// <param name="patientOneGuid"></param>
        /// <param name="patientTwoGuid"></param>
        /// <returns></returns>
        public bool HasJustTwoPatientRelated(string patientOneGuid, string patientTwoGuid)
        {
            //KodakDAL oKodak = new KodakDAL();
            try
            {
                string sqlHas2PatientRelated = "select count(1) from tPatientList where relatedid in(select relatedid from tPatientList where patientguid in('{0}','{1}') and len(relatedid)>0) and len(relatedid) >0";
                using (var oKodak = new RisDAL())
                {
                    object objResult = oKodak.ExecuteScalar(string.Format(sqlHas2PatientRelated, patientOneGuid, patientTwoGuid), RisDAL.ConnectionState.KeepOpen);
                    if (objResult != null && Convert.ToInt32(objResult) == 2)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            return false;
        }

        public bool HasOtherPatientRelated(string patientOneGuid, string patientTwoGuid)
        {
            //KodakDAL oKodak = new KodakDAL();
            try
            {
                string sql = "select RelatedID from tPatientList where PatientGuid in ('{0}','{1}') and LEN(RelatedID)>0";
                using (var oKodak = new RisDAL())
                {
                    DataTable dt = oKodak.ExecuteQuery(string.Format(sql, patientOneGuid, patientTwoGuid), RisDAL.ConnectionState.KeepOpen);
                    if (dt != null)
                    {
                        switch (dt.Rows.Count)
                        {
                            case 1: //one patient is related with others
                                return true;
                            case 2:
                            {
                                //judge the relatedid is same or not
                                if (dt.DefaultView.ToTable(true, new string[] { "RelatedID" }).Rows.Count == 1)
                                {
                                    return false; //same
                                }
                                else
                                {
                                    return true; //different related id
                                }
                            }
                            default:
                                return false;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                return false;
            }
            return false;
        }

        /// <summary>
        /// merge all gived orders's rps to target order and delete the gived orders
        /// </summary>
        /// <param name="dicOrders">the orders to be merged</param>
        /// <param name="bDelAfterMerge">current always true</param>
        /// <param name="strTargetOrderGUID">the target order to be merged to</param>
        /// <param name="bSendToGateWay">whether to send to gateway</param>
        /// <param name="bChargeFuncActive">whether to update charge associated info</param>/// 
        /// <param name="strError">the error message will be return</param>
        /// <param name="dtInfo">the detail info of why do false,current only use for lock info</param>
        public bool MergeOrders(Dictionary<string, string> dicOrders, string bDelAfterMerge, string strTargetOrderGUID, bool bSendToGateWay, bool bChargeFuncActive, ref string strError, ref DataTable dtInfo)
        {
            Debug.WriteLine("MergeOrders...");
            bool bReturn = true;
            DataSet dsTemp = new DataSet();
            DataTable dtTargetPatient = new DataTable();
            DataTable dtTargetOrder = new DataTable();
            DataTable dtRPMerge = new DataTable();
            StringBuilder strBuilder = new StringBuilder();
            List<string> listSQL = new List<string>();
            string srcOrderGUID;
            RisDAL oKodak = new RisDAL();
            try
            {
                //do sync check
                bReturn = CheckOperationSync(dicOrders, ref strError, 1, strTargetOrderGUID, ref dtInfo);
                if (!bReturn) return false;

                //get the target order datatable
                QueryOrderWithOrderID(strTargetOrderGUID, dsTemp, ref strError);
                dtTargetOrder = dsTemp.Tables["Order"];

                //get the target patient datatable
                QueryPatientWithPatientGuid(dtTargetOrder.Rows[0]["PatientGuid"].ToString(), ref dtTargetPatient, ref strError);

                //merge all RP in patient dictionary to target patient
                foreach (string strKey in dicOrders.Keys)
                {
                    srcOrderGUID = strKey;
                    strBuilder.Remove(0, strBuilder.Length);
                    strBuilder.AppendFormat("UPDATE tRegProcedure SET OrderGuid='{0}'  Where OrderGuid='{1}' ",
                    strTargetOrderGUID, srcOrderGUID);
                    listSQL.Add(strBuilder.ToString());

                    #region update by level, US26313 defect, 2015-08-12
                    //if (bChargeFuncActive)//the charge function is active by dog
                    //{
                    //    //1.update the totalfee for destination order plusing source order total fee
                    //    //2.update the orderguid for orderchage using destionation order guid
                    //    string strUpdateTotalFee = string.Format("Update tRegOrder set TotalFee = COALESCE(TotalFee,0.00) +(select COALESCE(TotalFee,0.00) from tRegOrder where orderguid ='{0}') where orderguid ='{1}'", srcOrderGUID, strTargetOrderGUID);
                    //    string strUpdateOrderChargeOrderGuid = string.Format("Update tOrderCharge set OrderGuid ='{0}' where OrderGuid ='{1}'", strTargetOrderGUID, srcOrderGUID);
                    //    listSQL.Add(strUpdateTotalFee);
                    //    listSQL.Add(strUpdateOrderChargeOrderGuid);
                    //} 
                    #endregion

                    if (bDelAfterMerge == "True")
                    {
                        strBuilder.Remove(0, strBuilder.Length);
                        strBuilder.AppendFormat("Delete tRegOrder  Where OrderGuid='{0}'", srcOrderGUID);
                        listSQL.Add(strBuilder.ToString());

                        string stSQL = string.Format("delete tAccessionNumberList where OrderGuid = '{0}'", srcOrderGUID);
                        listSQL.Add(stSQL);

                        if (bSendToGateWay)
                        {
                            //DeleteOrderSendToGateServer(srcOrderGUID,string.Empty, listSQL,string.Empty);//delete all rp under the order
                        }
                    }
                    if (bSendToGateWay)
                    //get all dataset of all RP already belongs to target patients
                    {
                        QueryRPWithOrderGUIDForGateWay(srcOrderGUID, dtRPMerge);
                    }
                }

                if (bSendToGateWay)
                {
                    MoveRPSendToGateServer(dtRPMerge, dtTargetPatient, strTargetOrderGUID, listSQL,true);
                }

                //update database
                oKodak.BeginTransaction();

                // 2015-08-26, Oscar added (US26267), Need to run "Update" first.
                int updateIndex;
                while ((updateIndex = listSQL.FindIndex(s => s.StartsWith("UPDATE tRegProcedure", StringComparison.OrdinalIgnoreCase))) >= 0)
                {
                    var statement = listSQL[updateIndex];
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", statement), Application.StartupPath,
                        (new StackFrame(true)).GetFileName(), (new StackFrame(true)).GetFileLineNumber());
                    Debug.WriteLine(statement);
                    oKodak.ExecuteNonQuery(statement, RisDAL.ConnectionState.KeepOpen);
                    listSQL.RemoveAt(updateIndex);
                }

                for (int i = listSQL.Count-1; i >= 0; i--) 
                //foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", listSQL[i]), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    //Debug.WriteLine(strSQL);
                    //oKodak.ExecuteNonQuery(strSQL, KodakDAL.ConnectionState.KeepOpen);
                    Debug.WriteLine(listSQL[i].ToString());
                    oKodak.ExecuteNonQuery(listSQL[i], RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();

                //send to ris gateway server

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dtTargetPatient != null)
                {
                    dtTargetPatient.Dispose();
                }
                if (dtRPMerge != null)
                {
                    dtRPMerge.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        /// <summary>
        /// move orders to a target patient
        /// </summary>
        /// <param name="dicOrders">the orders' guid dictionary</param>
        /// <param name="strTargetPatientGUID">the target patient guid</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="strError">the error message to be returned</param>
        public bool MoveOrders(Dictionary<string, string> dicOrders, string strTargetPatientGUID, bool bSendToGateWay, ref string strError)
        {
            Debug.WriteLine("MergeOrders...");
            bool bReturn = true;
            DataSet dsTemp = new DataSet();
            DataTable dtTargetPatient = new DataTable();
            DataTable dtTargetOrder = new DataTable();
            DataTable dtRPMerge = new DataTable();
            StringBuilder strBuilder = new StringBuilder();
            List<string> listSQL = new List<string>();
            DataTable dtTemp = new DataTable();
            string srcOrderGUID;
            RisDAL oKodak = new RisDAL();
            try
            {

                //do sync check
                bReturn = CheckOperationSync(dicOrders, ref strError, 2, strTargetPatientGUID, ref dtTemp);
                if (!bReturn) return false;

                //get the target patient datatable
                QueryPatientWithPatientGuid(strTargetPatientGUID, ref dtTargetPatient, ref strError);

                //merge all RP in patient dictionary to target patient
                foreach (string strKey in dicOrders.Keys)
                {
                    srcOrderGUID = strKey;
                    strBuilder.Remove(0, strBuilder.Length);
                    strBuilder.AppendFormat("UPDATE tRegOrder SET PatientGuid='{0}' Where OrderGuid='{1}'",
                    strTargetPatientGUID, srcOrderGUID);
                    listSQL.Add(strBuilder.ToString());
                    if (bSendToGateWay)
                    //get all dataset of all RP already belongs to target patients
                    {
                        QueryRPWithOrderGUIDForGateWay(srcOrderGUID, dtRPMerge);
                    }
                }

                if (bSendToGateWay)
                {
                    MergePatientSendToGateServer(dtRPMerge, dtTargetPatient, listSQL, true);
                }
                //update database
                oKodak.BeginTransaction();

                // 2015-08-26, Oscar added (US26267), Need to run "Update" first.
                int updateIndex;
                while ((updateIndex = listSQL.FindIndex(s => s.StartsWith("UPDATE tRegOrder", StringComparison.OrdinalIgnoreCase))) >= 0)
                {
                    var statement = listSQL[updateIndex];
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", statement), Application.StartupPath,
                        (new StackFrame(true)).GetFileName(), (new StackFrame(true)).GetFileLineNumber());
                    Debug.WriteLine(statement);
                    oKodak.ExecuteNonQuery(statement, RisDAL.ConnectionState.KeepOpen);
                    listSQL.RemoveAt(updateIndex);
                }

                foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", strSQL), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();

                //send to ris gateway server

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dtTemp != null)
                {
                    dtTemp.Dispose();
                }
                if (dtTargetPatient != null)
                {
                    dtTargetPatient.Dispose();
                }
                if (dtRPMerge != null)
                {
                    dtRPMerge.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        /// <summary>
        /// move all rps to a target order
        /// </summary>
        /// <param name="dicRPs">the rps' guid dictionary</param>
        /// <param name="strTargetOrderGUID">the target order guid</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="strError">the error message to be returned</param>
        /// <returns>if successfull return true else false</returns>
        public bool MoveRPs(Dictionary<string, string> dicRPs, string strTargetOrderGUID, bool bSendToGateWay, ref string strError)
        {
            Debug.WriteLine("MoveRPs...");
            bool bReturn = true;
            DataSet dsTemp = new DataSet();
            DataTable dtTargetPatient = new DataTable();
            DataTable dtTargetOrder = new DataTable();
            DataTable dtRPMerge = new DataTable();
            StringBuilder strBuilder = new StringBuilder();
            List<string> listSQL = new List<string>();
            DataTable dtTemp = new DataTable();
            string srcRPGUID;
            RisDAL oKodak = new RisDAL();
            try
            {

                //do sync check
                bReturn = CheckOperationSync(dicRPs, ref strError, 3, strTargetOrderGUID, ref dtTemp);
                if (!bReturn) return false;

                //get the target patient datatable
                QueryPatientWithOrderID(strTargetOrderGUID, dsTemp, ref strError);
                dtTargetPatient = dsTemp.Tables["Patient"];
                //merge all RP in patient dictionary to target patient
                foreach (string strKey in dicRPs.Keys)
                {
                    srcRPGUID = strKey;
                    strBuilder.Remove(0, strBuilder.Length);
                    strBuilder.AppendFormat("UPDATE tRegProcedure SET OrderGuid='{0}' Where ProcedureGuid='{1}'",
                    strTargetOrderGUID, srcRPGUID);
                    listSQL.Add(strBuilder.ToString());
                    if (bSendToGateWay)
                    //get all dataset of all RP already belongs to target patients
                    {
                        QueryRPWithRPGUIDForGateWay(srcRPGUID, dtRPMerge);
                    }
                }

                if (bSendToGateWay)
                {
                    MoveRPSendToGateServer(dtRPMerge, dtTargetPatient, strTargetOrderGUID, listSQL,false);
                    //MergePatientSendToGateServer(dtRPMerge, dtTargetPatient, listSQL, false);
                }
                //update database
                oKodak.BeginTransaction();

                // 2015-08-26, Oscar added (US26267), Need to run "Update" first.
                int updateIndex;
                while ((updateIndex = listSQL.FindIndex(s => s.StartsWith("UPDATE tRegProcedure", StringComparison.OrdinalIgnoreCase))) >= 0)
                {
                    var statement = listSQL[updateIndex];
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", statement), Application.StartupPath,
                        (new StackFrame(true)).GetFileName(), (new StackFrame(true)).GetFileLineNumber());
                    Debug.WriteLine(statement);
                    oKodak.ExecuteNonQuery(statement, RisDAL.ConnectionState.KeepOpen);
                    listSQL.RemoveAt(updateIndex);
                }

                foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", strSQL), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();

                //send to ris gateway server

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dtTemp != null)
                {
                    dtTemp.Dispose();
                }
                if (dtTargetPatient != null)
                {
                    dtTargetPatient.Dispose();
                }
                if (dtRPMerge != null)
                {
                    dtRPMerge.Dispose();
                }
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }
            }
            return bReturn;
        }

        #region merge order by level, US26313, 2015-07-30

        /// <summary>
        /// check whether these guids are exists in DB
        /// </summary>
        /// <param name="dicGuids">the guid dictionary which should be check</param>
        /// <param name="strGuid">the guid of the record which is not exists in DB</param>
        /// <param name="operationType">merge patient(0), merge order(1), move order(2) or move RP(3)</param>
        /// <param name="strTargetGuid">the target guid which should be check</param>
        /// <param name="dtLockInfo">the locked info if order locked</param>
        /// <returns>true if ok else false , and the false return the check false guid</returns>
        public bool CheckOperationSyncforRequisition(Dictionary<string, string> dicRequisition, ref string AccNo, string strTargetAccNo, ref DataTable dtLockInfo)
        {
            RisDAL dal = new RisDAL();
            int orderCount = 0;
            bool bReturn = true;

            string sqlCheckSrc = "select count(*) from tRequisition where AccNo = '{0}'";
            string sqlCheckTarget = string.Format("select count(*) from tRequisition where AccNo = '{0}'", strTargetAccNo);

            try
            {
                //check  wether target guid exists
                object objTargetCount = dal.ExecuteScalar(sqlCheckTarget, RisDAL.ConnectionState.KeepOpen);
                if (objTargetCount != null)
                {
                    orderCount = Convert.ToInt32(objTargetCount);
                    if (orderCount == 0)
                    {
                        AccNo = strTargetAccNo;
                        return false;
                    }
                }

                //check  wether source guid exists
                foreach (string guid in dicRequisition.Values)
                {
                    sqlCheckSrc = string.Format(sqlCheckSrc, guid);
                    object objSrcCount = dal.ExecuteScalar(sqlCheckSrc, RisDAL.ConnectionState.KeepOpen);
                    if (objSrcCount != null)
                    {
                        orderCount = Convert.ToInt32(objSrcCount);
                        if (orderCount == 0)
                        {
                            AccNo = guid;
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AccNo = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return bReturn;
        }


        /// <summary>
        /// merge Requisition to a target order
        /// </summary>
        /// <param name="dicRequisition">the Requisition guid dictionary</param>
        /// <param name="strTargetAccNo">the target AccNo</param>
        /// <param name="bSendToGateWay">wether to send to gateway</param>
        /// <param name="strError">the error message to be returned</param>
        /// <returns>if successfull return true else false</returns>
        public bool MergeRequisition(Dictionary<string, string> dicRequisition, string strTargetAccNo, bool bSendToGateWay, ref string strError) 
        {
            
            logger.Debug((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, "MergeRequisition...", Application.StartupPath.ToString(),
       (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            bool bReturn = true;
            DataSet dsTemp = new DataSet();
            StringBuilder strBuilder = new StringBuilder();
            List<string> listSQL = new List<string>();
            DataTable dtTemp = new DataTable();
            string srcRequisitionAccNo;
            RisDAL oKodak = new RisDAL();
            try
            {
                ////do sync check
                //bReturn = CheckOperationSyncforRequisition(dicRequisition, ref strError, strTargetAccNo, ref dtTemp);
                //if (!bReturn) return false;

                ////get the target patient datatable
                //QueryPatientWithPatientGuid(strTargetAccNo, ref dtTargetPatient, ref strError);

                //merge all RP in patient dictionary to target patient
                foreach (string strVal in dicRequisition.Values)
                {
                    srcRequisitionAccNo = strVal;
                    strBuilder.Remove(0, strBuilder.Length);
                    strBuilder.AppendFormat("UPDATE tRequisition SET AccNo='{0}' Where AccNo='{1}'", strTargetAccNo, srcRequisitionAccNo);
                    listSQL.Add(strBuilder.ToString());
                    //if (bSendToGateWay)//get all dataset of all RP already belongs to target patients
                    //    QueryRPWithOrderGUIDForGateWay(srcRequisitionGUID, dtRPMerge);
                }

                //if (bSendToGateWay)
                //{
                //    MergePatientSendToGateServer(dtRPMerge, dtTargetPatient, listSQL, true);
                //}
                //update database
                oKodak.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", strSQL), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();

                //send to ris gateway server
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dtTemp != null)  dtTemp.Dispose();
                if (oKodak != null)  oKodak.Dispose();
            }
            return bReturn;
        
        }

        public bool MergeCharge(Dictionary<string, string> dicCharge, string strTargetOrderGUID, bool bSendToGateWay, ref string strError)
        {

            logger.Debug((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, "MergeCharge...", Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            bool bReturn = true;
            DataSet dsTemp = new DataSet();
            StringBuilder strBuilder = new StringBuilder();
            List<string> listSQL = new List<string>();
            DataTable dtTemp = new DataTable();
            string srcOrderGUID;
            RisDAL oKodak = new RisDAL();
            try
            {

                ////do sync check
                //bReturn = CheckOperationSync(dicCharge, ref strError, 4, strTargetOrderGUID, ref dtTemp); 
                //if (!bReturn) return false;

                ////get the target patient datatable
                //QueryPatientWithPatientGuid(strTargetOrderGUID, ref dtTargetPatient, ref strError);

                //merge all RP in patient dictionary to target patient
                foreach (string strVal in dicCharge.Keys)
                {
                    srcOrderGUID = strVal;
                    strBuilder.Remove(0, strBuilder.Length);
                    strBuilder.AppendFormat("UPDATE tOrderCharge SET OrderGuid='{0}' Where OrderGuid='{1}'", strTargetOrderGUID, srcOrderGUID);
                    listSQL.Add(strBuilder.ToString());
                    //if (bSendToGateWay)//get all dataset of all RP already belongs to target patients
                    //    QueryRPWithOrderGUIDForGateWay(srcOrderGUID, dtRPMerge);
                }

                //if (bSendToGateWay)
                //{
                //    MergePatientSendToGateServer(dtRPMerge, dtTargetPatient, listSQL, true);
                //}
                //update database
                oKodak.BeginTransaction();
                foreach (string strSQL in listSQL)
                {
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, string.Format("EXECUTE SQL IN QC: {0}", strSQL), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL, RisDAL.ConnectionState.KeepOpen);
                }
                oKodak.CommitTransaction();

                //send to ris gateway server
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dtTemp != null) dtTemp.Dispose();
                if (oKodak != null) oKodak.Dispose();
            }
            return bReturn;

        }

        #endregion
        public bool ExistQuashStatus(Dictionary<string, string> dicRPs, Dictionary<string, string> dicOrders, string strTargetOrder, ref string strError)
        {
            bool bExist = false;
            RisDAL oKodak = new RisDAL();
            try
            {
               
                Object obj = null;
                string strSQL="";
                if (dicRPs != null && dicRPs.Count > 0)
                {
                    foreach (KeyValuePair<string, string> kvp in dicRPs)
                    {
                        strSQL = string.Format("select count(*) from tregprocedure where procedureguid='{0}' and status=0", kvp.Key);
                        obj = oKodak.ExecuteScalar(strSQL);
                        if (obj != null && Convert.ToInt32(obj) > 0)
                        {
                            bExist = true;
                            break;
                        }
                    }               
                   
                }

                if (!bExist&&dicOrders != null && dicOrders.Count > 0)
                {
                    foreach (KeyValuePair<string, string> kvp in dicOrders)
                    {
                        strSQL = string.Format("select count(*) from tregprocedure where orderguid='{0}' and status=0", kvp.Key);
                        obj = oKodak.ExecuteScalar(strSQL);
                        if (obj != null && Convert.ToInt32(obj) > 0)
                        {
                            bExist = true;
                            break;
                        }
                    }
                }

                if (!bExist && strTargetOrder != null && strTargetOrder.Trim().Length > 0)
                {
                    strSQL = string.Format("select count(*) from tregprocedure where orderguid='{0}' and status=0", strTargetOrder);
                    obj = oKodak.ExecuteScalar(strSQL);
                    if (obj != null && Convert.ToInt32(obj) > 0)
                    {
                        bExist = true;                     
                    }
                    
                }
              

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bExist;
        }


        #endregion
        #endregion
        #region Lock
        public virtual bool QueryLock(string strObjectType, string strObjectGuid, DataSet ds)
        {
            Debug.WriteLine("QueryLock info...");
            bool bReturn = true;
            DataTable dt = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {

                string strSQL = string.Format("SELECT  * from tSync where 1=1 ");
                if (!string.IsNullOrEmpty(strObjectType))
                {
                    strSQL += string.Format(" and SyncType ='{0}'", strObjectType.ToString());
                }

                if (!string.IsNullOrEmpty(strObjectGuid))
                {
                    strSQL += string.Format(" and Guid = '{0}' ", strObjectGuid);
                }


                Debug.WriteLine(strSQL);
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

                if (dt != null && dt.Rows.Count == 1)
                {
                    ds.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool QueryLock(string stOwner, string stBeginTime, string stEndTime, DataSet ds, ref string strError)
        {
            Debug.WriteLine("QueryLock...");
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

                Debug.WriteLine(strSQL);
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
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
        public virtual bool LockObject(int nObjectType, string strObjectGuid, string strOwner, string strOwnerIP, ref string strLockInfo, ref string strError)
        {
            Debug.WriteLine("LockObject...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                string stSQL = "";
                string stInsert = "";
                string stInsert2 = "";
                if (Convert.ToInt32(LockEnum.LockPatient) == nObjectType)
                {
                    // stSQL = "select synctype,guid,tuser.localname as owner,ownerip,createdt from tsync inner join tuser on tuser.userguid = owner where guid in (select orderguid from tregorder a,tregvisit b,tregpatient c ";
                    // stSQL += "where a.visitguid = b.visitguid and b.patientguid = c.patientguid ";
                    // stSQL += string.Format("and c.patientguid = '{0}')", strObjectGuid);
                    #region EK_HI00072287 By Foman Liang 2008-6-19
                    // stSQL = "select * from tsync where guid in (select orderguid from tregorder a,tregvisit b,tregpatient c ";
                    // stSQL += "where a.visitguid = b.visitguid and b.patientguid = c.patientguid ";
                    // stSQL += string.Format("and c.patientguid = '{0}')", strObjectGuid);
                    stSQL = string.Format("select * from tsync where guid ='{0}'", strObjectGuid);
                    #endregion
                }
                else if (Convert.ToInt32(LockEnum.LockVisit) == nObjectType)
                {
                    //   stSQL = "select synctype,guid,tuser.localname as owner,ownerip,createdt from tsync inner join tuser on tuser.userguid = owner  where guid in (select orderguid from tregorder a ";
                    //   stSQL += string.Format("where a.visitguid = '{0}')", strObjectGuid);
                    stSQL = "select * from tsync where guid in (select orderguid from tregorder a ";
                    stSQL += string.Format("where a.visitguid = '{0}')", strObjectGuid);

                }
                else if (Convert.ToInt32(LockEnum.LockOrder) == nObjectType)
                {
                    //{Bruce Deng 20080122  lock by rp from report panel
                    stSQL = string.Format("select * from tSync where guid='{0}'", strObjectGuid);

                    //stSQL = "select synctype,guid,tuser.localname as owner,ownerip,createdt from tsync inner join tuser on tuser.userguid = owner   ";
                    //stSQL += string.Format("where guid = '{0}'", strObjectGuid);
                    //}
                }
                else if (Convert.ToInt32(LockEnum.LockExam) == nObjectType)
                {
                    //stSQL = "select synctype,guid,tuser.localname as owner,ownerip,createdt from tsync inner join tuser on tuser.userguid = owner  where guid in (select orderguid from tregprocedure a ";
                    //stSQL += string.Format("where a.ProcedureGuid = '{0}')", strObjectGuid);
                    //stSQL = "select * from tsync where guid in (select orderguid from tregprocedure a ";
                    stSQL = "select * from tsync where guid ='" + strObjectGuid + "'";
                    //stSQL += string.Format("where a.ProcedureGuid = '{0}')", strObjectGuid);

                }
                else
                //by default,assume it is order
                {
                    // stSQL = "select synctype,guid,tuser.localname as owner,ownerip,createdt from tsync inner join tuser on tuser.userguid = owner ";
                    // stSQL += string.Format("where guid = '{0}'", strObjectGuid);
                    stSQL = "select * from tsync ";
                    stSQL += string.Format("where guid = '{0}'", strObjectGuid);

                }

                Debug.WriteLine(stSQL);
                oKodak.ExecuteQuery(stSQL, dt);
                if (dt.Rows.Count > 0)
                {
                    bReturn = false;
                    DataRow dr = dt.Rows[0];
                    //{Bruce Deng 20080122 locked by rp from report module
                    string strLocalNames = "";
                    string strOwners = dr["Owner"].ToString();
                    char[] sep = { '|' };
                    string[] arrItems = strOwners.Split(sep);
                    foreach (string str in arrItems)
                    {
                        Object obj = oKodak.ExecuteScalar(string.Format("select localname from tUser where userguid='{0}'", str));
                        strLocalNames += obj.ToString();
                        strLocalNames += "|";
                    }
                    strLocalNames = strLocalNames.TrimEnd(sep);
                    strLockInfo = string.Format("{0}&{1}", strLocalNames, dr["OwnerIP"].ToString());



                }
                else
                {
                    if (Convert.ToInt32(LockEnum.LockPatient) == nObjectType)
                    {
                        #region  EK_HI00061987 jameswei 2007-11-26
                        //stInsert = "insert into tsync (synctype,guid,owner,ownerip) ";
                        stInsert = "insert into tsync (synctype,guid,owner,ownerip,createdt,moduleid,patientid,patientname,accno) ";
                        #endregion
                        stInsert += string.Format("select top(1) '{0}' as synctype, c.patientguid as guid,'{1}' as owner,'{2}' as ownerip ,getdate() as createdt,'0D00' as moduleid,c.patientid,c.localname as patientname,a.accno ", '2', strOwner, strOwnerIP);
                        stInsert += string.Format("from tregorder a,tregpatient c ");
                        stInsert += string.Format("where a.patientguid = c.patientguid ");
                        stInsert += string.Format("and c.patientguid = '{0}'", strObjectGuid);
                        #region  EK_HI00061987 jameswei 2007-11-26 delete unknow the meaning of insert twice
                        stInsert2 = "insert into tsync (synctype,guid,owner,ownerip,createdt,moduleid,patientid,patientname,accno) ";
                        stInsert2 += string.Format(" select '{0}' as synctype, patientguid as guid,'{1}' as owner,'{2}' as ownerip ,getdate() as createdt,'0D00' as moduleid,patientid,localname as patientname ,'' as accno ", '2', strOwner, strOwnerIP);
                        stInsert2 += string.Format(" from tregpatient");
                        stInsert2 += string.Format(" where patientguid = '{0}'", strObjectGuid);
                        #endregion
                    }
                    else if (Convert.ToInt32(LockEnum.LockVisit) == nObjectType)
                    {
                        #region  EK_HI00061987 jameswei 2007-11-26
                        //stInsert = "insert into tsync (synctype,guid,owner,ownerip) ";
                        stInsert = "insert into tsync (synctype,guid,owner,ownerip,createdt,moduleid,patientid,patientname,accno) ";
                        stInsert += string.Format("select top(1) '{0}' as synctype, a.visitguid as guid,'{1}'as owner,'{2}' as ownerip,getdate() as createdt,'0D00' as moduleid,c.patientid,c.localname as patientname,a.accno ", '2', strOwner, strOwnerIP);
                        stInsert += string.Format("from tregorder a,tregpatient c ");
                        stInsert += string.Format("where c.patientguid = a.patientguid ");
                        #endregion
                        stInsert += string.Format("and a.visitguid = '{0}'", strObjectGuid);
                    }
                    else if (Convert.ToInt32(LockEnum.LockOrder) == nObjectType)
                    {
                        #region  EK_HI00061987 jameswei 2007-11-26
                        //stInsert = "insert into tsync (synctype,guid,owner,ownerip) ";
                        stInsert = "insert into tsync (synctype,guid,owner,ownerip,createdt,moduleid,patientid,patientname,accno) ";
                        stInsert += string.Format("select '{0}' as synctype, orderguid as guid,'{1}'as owner,'{2}' as ownerip,getdate() as createdt,'0D00' as moduleid,c.patientid,c.localname as patientname,a.accno ", '2', strOwner, strOwnerIP);
                        stInsert += string.Format("from tregorder a,tregpatient c ");
                        stInsert += string.Format("where a.orderguid = '{0}' and c.patientguid = a.patientguid ", strObjectGuid);
                        //stInsert += string.Format("values ('{0}','{1}','{2}','{3}') ", "2", strObjectGuid, strOwner, strOwnerIP);
                        #endregion
                    }
                    else if (Convert.ToInt32(LockEnum.LockExam) == nObjectType)
                    {
                        #region  EK_HI00061987 jameswei 2007-11-26
                        //stInsert = "insert into tsync (synctype,guid,owner,ownerip) ";
                        stInsert = "insert into tsync (synctype,guid,owner,ownerip,createdt,moduleid,patientid,patientname,accno) ";
                        stInsert += string.Format("select '{0}' as synctype, b.procedureguid as guid,'{1}'as owner,'{2}' as ownerip,getdate() as createdt,'0D00' as moduleid,p.patientid,p.localname as patientname,a.accno ", '2', strOwner, strOwnerIP);
                        stInsert += string.Format("from tregorder a,tregprocedure b,tregpatient p  ");
                        stInsert += string.Format("where a.orderguid = b.orderguid and a.patientguid = p.patientguid  ");
                        stInsert += string.Format("and b.ProcedureGuid = '{0}'", strObjectGuid);
                        #endregion
                    }
                    else
                    {
                        stInsert = "insert into tsync (synctype,guid,owner,ownerip) ";
                        stInsert += string.Format("values ('{0}','{1}','{2}','{3}') ", "2", strObjectGuid, strOwner, strOwnerIP);
                    }
                }
                oKodak.BeginTransaction();
                Debug.WriteLine(stInsert);
                int iInserted = oKodak.ExecuteNonQuery(stInsert, RisDAL.ConnectionState.KeepOpen);
                #region  EK_HI00061987 jameswei 2007-11-26
                if (iInserted == 0 && stInsert2.Length > 0)
                {
                    Debug.WriteLine(stInsert2);
                    oKodak.ExecuteNonQuery(stInsert2, RisDAL.ConnectionState.KeepOpen);
                }
                #endregion
                oKodak.CommitTransaction();
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
        public virtual bool UnLockObject(int nObjectType, int nSyncType, string strObjectGuid, string strOwner, ref string strError)
        {
            Debug.WriteLine("UnLockObject...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();

            try
            {
                string strSQL = "";
                string strSQL2 = "";
                strObjectGuid = strObjectGuid.Trim();

                if (strObjectGuid.Length > 0)
                //unlock one reocrd
                {
                    if (Convert.ToInt32(LockEnum.LockPatient) == nObjectType)
                    {
                        //strSQL = string.Format("DELETE FROM tSync WHERE  SyncType={0} and Owner='{2}' and Guid in (select orderguid from tregorder a where a.patientguid = '{1}')",
                        strSQL = string.Format("DELETE FROM tSync WHERE  SyncType={0} and Owner='{2}' and Guid = '{1}'",
                          nSyncType, strObjectGuid, strOwner);

                        strSQL2 = string.Format("DELETE FROM tSync WHERE SyncType={0} and Owner='{2}' and Guid='{1}' ", nSyncType, strObjectGuid, strOwner);
                    }
                    else if (Convert.ToInt32(LockEnum.LockVisit) == nObjectType)
                    {
                        //strSQL = string.Format("DELETE FROM tSync WHERE  SyncType={0} and Owner='{2}' and Guid in (select orderguid from tregorder  where Visitguid = '{1}')",
                        //nSyncType, strObjectGuid, strOwner);
                        strSQL = string.Format("DELETE FROM tSync WHERE  SyncType={0} and Owner='{2}' and Guid='{1}' ",
                        nSyncType, strObjectGuid, strOwner);
                    }
                    else if (Convert.ToInt32(LockEnum.LockOrder) == nObjectType)
                    {
                        strSQL = string.Format("DELETE FROM tSync WHERE SyncType={0} and Owner='{2}' and Guid='{1}'",
                            nSyncType, strObjectGuid, strOwner);
                    }
                    else if (Convert.ToInt32(LockEnum.LockExam) == nObjectType)
                    {
                        #region  EK_HI00061987 jameswei 2007-11-26
                        strSQL = string.Format("DELETE FROM tSync WHERE  SyncType={0} and Owner='{2}' and Guid= '{1}'",
                        #endregion
                            //strSQL = string.Format("DELETE FROM tSync WHERE  SyncType={0} and Owner='{2}' and Guid= (select orderguid from tregprocedure where ProcedureGuid = '{1}')",
                            nSyncType, strObjectGuid, strOwner);
                    }
                    else
                    {
                        strSQL = string.Format("DELETE FROM tSync WHERE SyncType={0} and Owner='{2}' and Guid='{1}'",
                            nSyncType, strObjectGuid, strOwner);
                    }

                }
                else
                //no GUID is input, all records belong to this owner will be unlocked
                {
                    strSQL = string.Format("DELETE FROM tSync WHERE SyncType={0} AND Owner='{1}'", nSyncType, strOwner);
                }

                if (strSQL.Length > 0)
                {
                    Debug.WriteLine(strSQL);
                    oKodak.ExecuteNonQuery(strSQL);
                }
                if (strSQL2.Length > 0)
                {
                    Debug.WriteLine(strSQL2);
                    oKodak.ExecuteNonQuery(strSQL2);
                }

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
        #endregion
        #region integration
        public virtual bool IsSendInfo(DataTable dtInfo)
        {
            bool bSendInfo = false;
            try
            {
                foreach (DataRow dr in dtInfo.Rows)
                {
                    if (Convert.ToInt32(dr["Status"]) > 10 ||
                        dr["4"].ToString().Trim().Length > 0)
                    {
                        bSendInfo = true;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                bSendInfo = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            return bSendInfo;
        }
        /// <summary>
        /// Send the information about updating patient to Gateway Server
        /// </summary>
        /// <param name="dtOrder">
        /// Contains all information of this patient
        /// </param>
        /// <param name="listSQL">
        /// Return list of SQL string used in this feature
        /// </param>
        /* old
        public virtual bool UpdatePatientSendToGateServer(DataTable dtPatient, List<string> listSQL)
        {
            Debug.WriteLine("UpdatePatientSendToGateServer...");
            KodakDAL oKodak = new KodakDAL();
            bool bRet = false;

            try
            {
                DataRow drPatient = dtPatient.Rows[0];

                string stGUID = drPatient["PatientGuid"].ToString();
                DataSet dsOrder = new DataSet();
                if (QueryLatestVisitWithPatientGUID(stGUID, dsOrder))
                {
                    DataTable dtOrder = dsOrder.Tables["Order"];
                    if (dtOrder == null) return false;
                    DataRow drOrder = dtOrder.Rows[0];

                    StringBuilder strBuilder = new StringBuilder();

                    string strGWGuid = Guid.NewGuid().ToString();
                    //Here the event type of update patient info includes patient info and visit info and order info in GCRIS
                    strBuilder.Remove(0, strBuilder.Length);
                    strBuilder.AppendFormat("INSERT INTO GW_DATAINDEX(DATA_ID,DATA_DT,EVENT_TYPE,RECORD_INDEX_1,DATA_SOURCE) ")
                            .AppendFormat("VALUES('{0}','{1}','01','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    listSQL.Add(strBuilder.ToString());
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    #region get text table data 2008-07-21
                    DataSet ds = new DataSet();
                    GWInfo gwInfo = new GWInfo();
                    string errInfo = "";
                    QueryExt(drPatient["PatientGuid"].ToString(), "", "", ds, ref errInfo);
                    GetGWInfo(ref ds, ref gwInfo);
                    #endregion
                    //Patient                                    
                    {
                        string stCols = "";
                        string stVals = "";
                        strBuilder.Remove(0, strBuilder.Length);
                        AddColForInsert("PATIENTID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("OTHER_PID", drPatient["RemotePID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_NAME", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCAL_NAME", drPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                        StringBuilder sbBirthdate = new StringBuilder();
                        sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]));
                        AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SEX", drPatient["Gender"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_ALIAS", gwInfo.Alias, ref stCols, ref  stVals);//new added
                        AddColForInsert("MARITAL_STATUS", gwInfo.Marital, ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_1", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_3", drOrder["InHospitalNo"].ToString(), ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_4", gwInfo.PatientComment, ref stCols, ref  stVals);//new added
                        AddColForInsert("ADDRESS", drPatient["Address"].ToString(), ref stCols, ref  stVals);
                        int iVIP = string.Compare(drPatient["IsVIP"].ToString(), "N", true) == 0 ? 0 : 1;
                        AddColForInsert("CUSTOMER_2", iVIP.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PHONENUMBER_HOME", drPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_TYPE", drOrder["PatientType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCATION", drOrder["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("VISIT_NUMBER", drOrder["ClinicNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BED_NUMBER", drOrder["BedNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                        strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                        listSQL.Add(strBuilder.ToString());
                        logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    }


                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    bRet = true;
                }

            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bRet;
        }
         * 
         * */
        public virtual bool UpdatePatientSendToGateServer(DataTable dtPatient, List<string> listSQL)
        {
            Debug.WriteLine("UpdatePatientSendToGateServer...");
            RisDAL oKodak = new RisDAL();
            bool bRet = false;
            string strError = "";

            try
            {
                DataRow drPatient = dtPatient.Rows[0];

                string stGUID = drPatient["PatientGuid"].ToString();

                DataSet dsOrder = new DataSet();
                if (!QueryOrder(stGUID, dsOrder, ref strError))
                {
                    return false;
                }

                DataTable dtOrder = dsOrder.Tables["Order"];
                if (dtOrder == null) return false;
                DataRow drOrder = dtOrder.Rows[0];

                StringBuilder strBuilder = new StringBuilder();

                string strGWGuid = Guid.NewGuid().ToString();
                //Here the event type of update patient info includes patient info and visit info and order info in GCRIS
                strBuilder.Remove(0, strBuilder.Length);
                strBuilder.AppendFormat("INSERT INTO GW_DATAINDEX(DATA_ID,DATA_DT,EVENT_TYPE,RECORD_INDEX_1,DATA_SOURCE) ")
                        .AppendFormat("VALUES('{0}','{1}','01','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                listSQL.Add(strBuilder.ToString());
                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                //Patient                                    
                {
                    string stCols = "";
                    string stVals = "";
                    strBuilder.Remove(0, strBuilder.Length);
                    AddColForInsert("PATIENTID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("OTHER_PID", drPatient["RemotePID"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_NAME", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_LOCAL_NAME", drPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                    StringBuilder sbBirthdate = new StringBuilder();
                    sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]));
                    AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                    AddColForInsert("SEX", drPatient["Gender"].ToString(), ref stCols, ref  stVals);
                    // AddColForInsert("PATIENT_ALIAS", gwInfo.Alias, ref stCols, ref  stVals);//new added
                    //  AddColForInsert("MARITAL_STATUS", gwInfo.Marital, ref stCols, ref  stVals);//new added
                    AddColForInsert("CUSTOMER_1", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);//new added
                    AddColForInsert("CUSTOMER_3", drOrder["InHospitalNo"].ToString(), ref stCols, ref  stVals);//new added
                    //  AddColForInsert("CUSTOMER_4", gwInfo.PatientComment, ref stCols, ref  stVals);//new added
                    AddColForInsert("ADDRESS", drPatient["Address"].ToString(), ref stCols, ref  stVals);
                    int iVIP = string.Compare(drPatient["IsVIP"].ToString(), "N", true) == 0 ? 0 : 1;
                    AddColForInsert("CUSTOMER_2", iVIP.ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PHONENUMBER_HOME", drPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_TYPE", drOrder["PatientType"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_LOCATION", drOrder["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("VISIT_NUMBER", drOrder["ClinicNo"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("BED_NUMBER", drOrder["BedNo"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                    AddColForInsert("DRIVERLIC_NUMBER", drPatient["ReferenceNo"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("SSN_NUMBER", drPatient["MedicareNo"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_ALIAS", drPatient["Alias"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("MARITAL_STATUS", drPatient["Marriage"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                    strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                    listSQL.Add(strBuilder.ToString());
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                }


                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                bRet = true;
            }

            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bRet;
        }



        /// <summary>
        /// Send the information about updating patient to Gateway Server
        /// </summary>
        /// <param name="dtOrder">
        /// Contains all information of this patient
        /// </param>
        /// <param name="listSQL">
        /// Return list of SQL string used in this feature
        /// </param>

        public virtual bool DeletePatientSendToGateServer(string stPatientGUID, List<string> listSQL)
        {
            Debug.WriteLine("DeletePatientSendToGateServer...");
            RisDAL oKodak = new RisDAL();
            bool bRet = false;

            try
            {
                DataTable dtPatient = new DataTable();
                string stQueryPatient = string.Format("select * from tRegPatient where patientguid = '{0}'", stPatientGUID);
                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, stQueryPatient, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                Debug.WriteLine(stQueryPatient);
                oKodak.ExecuteQuery(stQueryPatient, dtPatient);
                if (dtPatient.Rows.Count > 0)
                {
                    DataRow drPatient = dtPatient.Rows[0];

                    //In case the integration force us to input patient type
                    //we can read it from visit table
                    //Commented because our table set patient_type column as null-available now
                    //DataSet dsOrder = new DataSet();                    
                    //DataTable dtOrder;
                    //DataRow drOrder = null;
                    ////read the patient 
                    //if (QueryLatestVisitWithPatientGUID(stGUID, dsOrder))
                    //{
                    //    dtOrder = dsOrder.Tables["Order"];
                    //    if (dtOrder != null)
                    //    {
                    //        drOrder = dtOrder.Rows[0];
                    //    }
                    //}


                    StringBuilder strBuilder = new StringBuilder();
                    string strGWGuid = Guid.NewGuid().ToString();
                    //Here the event type of update patient info includes patient info and visit info and order info in GCRIS
                    strBuilder.Remove(0, strBuilder.Length);
                    strBuilder.AppendFormat("INSERT INTO GW_DATAINDEX(DATA_ID,DATA_DT,EVENT_TYPE,RECORD_INDEX_1,DATA_SOURCE) ")
                            .AppendFormat("VALUES('{0}','{1}','03','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    listSQL.Add(strBuilder.ToString());
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    //Patient                                    
                    {
                        string stCols = "";
                        string stVals = "";
                        strBuilder.Remove(0, strBuilder.Length);
                        AddColForInsert("PATIENTID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("OTHER_PID", drPatient["RemotePID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_NAME", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCAL_NAME", drPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                        StringBuilder sbBirthdate = new StringBuilder();
                        sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]));
                        AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SEX", drPatient["Gender"].ToString(), ref stCols, ref  stVals);
                        // AddColForInsert("PATIENT_ALIAS", gwInfo.Alias, ref stCols, ref  stVals);//new added
                        //  AddColForInsert("MARITAL_STATUS", gwInfo.Marital, ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_1", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);//new added
                        //AddColForInsert("CUSTOMER_3", drOrder["InHospitalNo"].ToString(), ref stCols, ref  stVals);//new added
                        //  AddColForInsert("CUSTOMER_4", gwInfo.PatientComment, ref stCols, ref  stVals);//new added                        AddColForInsert("ADDRESS", drPatient["Address"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PHONENUMBER_HOME", drPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("PATIENT_TYPE", drOrder["PatientType"].ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("PATIENT_LOCATION", drOrder["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("VISIT_NUMBER", drOrder["ClinicNo"].ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("BED_NUMBER", drOrder["BedNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("ADDRESS", drPatient["Address"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                        strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                        listSQL.Add(strBuilder.ToString());
                        logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    }


                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    bRet = true;
                }

            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bRet;
        }
        /// <summary>
        /// Send the information about updating order to Gateway Server
        /// Here order means order in GCRis
        /// It will be stored in patient table in gateway
        /// </summary>
        /// <param name="dtOrder">
        /// Contains all information of this order
        /// </param>
        /// <param name="listSQL">
        /// Return list of SQL string used in this feature
        /// </param>
        /// <returns></returns>
        /// 
        /* old
        public virtual bool UpdateOrderSendToGateServer(DataTable dtOrder, List<string> listSQL)
        {
            Debug.WriteLine("UpdateOrderSendToGateServer...");
            KodakDAL oKodak = new KodakDAL();
            bool bRet = false;

            try
            {
                DataRow drOrder = dtOrder.Rows[0];

                string stVisitGUID = drOrder["VisitGuid"].ToString();
                string stOrderGUID = drOrder["OrderGuid"].ToString();

                StringBuilder strBuilder = new StringBuilder();

                DataSet dsPatient = new DataSet();
                DataRow drPatient = null;
                DataTable dtPatient = null;
                //query patient information for gateway
                if (QueryPatientWithVisitGUID(stVisitGUID, dsPatient))
                {
                    dtPatient = dsPatient.Tables["Patient"];
                    if (dtPatient == null) return false;
                    drPatient = dtPatient.Rows[0];
                }

                //query RP information for gateway
                DataTable dtRP = new DataTable();
                string strError = "";
                string strRPGuid = "";
                if (QueryRP(stOrderGUID, ref dtRP, ref strError,ref strRPGuid) && drPatient != null)
                {
                    try
                    {
                        strBuilder.Remove(0, strBuilder.Length);
                        foreach (DataRow drRP in dtRP.Rows)
                        {

                            ///////////////////////////// First give one new GUID
                            string strGWGuid = Guid.NewGuid().ToString();
                            ///////////////////////////// 2, get event type
                            int nEventType = 0;
                            int nExamStatus = 0;
                            if (drRP["RemoteRPID"].ToString().Trim().Length == 0)//
                            {
                                #region DEFECT EK_HI00073389

                                if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                                {
                                    nExamStatus = 101;
                                    nEventType = 21;
                                }
                                else
                                {
                                #endregion
                                    nEventType = 11;
                                    nExamStatus = 11;
                                }
                            }
                            else//From Electronic requisition
                            {
                                if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                                {
                                    nEventType = 21;
                                }
                                else
                                {
                                    nEventType = 11;
                                }
                                nExamStatus = 101;
                            }
                            ///////////////////////////// 3, insert index
                            //Here the event type of update patient info includes patient info and visit info and order info in GCRIS
                            strBuilder.Remove(0, strBuilder.Length);
                            //Data Index
                            strBuilder.AppendFormat("INSERT INTO GW_DATAINDEX(DATA_ID,DATA_DT,EVENT_TYPE,RECORD_INDEX_1,DATA_SOURCE) ")
                                    .AppendFormat("VALUES('{0}','{1}','{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), nEventType);
                            listSQL.Add(strBuilder.ToString());

                            logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                            #region get text table data 2008-07-21
                            DataSet ds = new DataSet();
                            string errInfo = "";
                            QueryOrderWithOrderID(stOrderGUID, ds,ref strError);
                            DataRow drOrderQueried = ds.Tables["Order"].Rows[0];
                            GWInfo gwInfo = new GWInfo();
                            QueryExt(drOrderQueried["PatientGuid"].ToString(), drOrderQueried["VisitGuid"].ToString(), drOrderQueried["OrderGuid"].ToString(), ds, ref errInfo);
                            GetGWInfo(ref ds, ref gwInfo);
                            #endregion

                            ///////////////////////////// 4, insert patient
                            //Patient                                    
                            {
                                string stCols = "";
                                string stVals = "";
                                strBuilder.Remove(0, strBuilder.Length);
                                AddColForInsert("PATIENTID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("OTHER_PID", drPatient["RemotePID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_NAME", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_LOCAL_NAME", drPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                                StringBuilder sbBirthdate = new StringBuilder();
                                sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]));
                                AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                                AddColForInsert("SEX", drPatient["Gender"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_ALIAS", gwInfo.Alias, ref stCols, ref  stVals);//new added
                                AddColForInsert("MARITAL_STATUS", gwInfo.Marital, ref stCols, ref  stVals);//new added
                                AddColForInsert("CUSTOMER_1", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);//new added
                                AddColForInsert("CUSTOMER_2", drPatient["IsVip"].ToString(), ref stCols, ref  stVals);//new added
                                AddColForInsert("CUSTOMER_3", drOrderQueried["InHospitalNo"].ToString(), ref stCols, ref  stVals);//new added
                                AddColForInsert("CUSTOMER_4", gwInfo.PatientComment, ref stCols, ref  stVals);//new added    
                                AddColForInsert("ADDRESS", drPatient["Address"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PHONENUMBER_HOME", drPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_TYPE", drOrderQueried["PatientType"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_LOCATION", drOrderQueried["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("VISIT_NUMBER", drOrderQueried["ClinicNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("BED_NUMBER", drOrderQueried["BedNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                                AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                                strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                                listSQL.Add(strBuilder.ToString());
                                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                                    (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                            }


                            ///////////////////////////// 5, insert order
                            strBuilder.Remove(0, strBuilder.Length);
                            //get string for update order
                            {
                                ////build sql
                                string stCols = "";
                                string stVals = "";
                                AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                                AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                                AddColForInsert("ORDER_NO", drRP["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PLACER_NO", drOrderQueried["RemoteAccNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("FILLER_NO", drOrderQueried["AccNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_ID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("EXAM_STATUS", nExamStatus.ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PLACER_DEPARTMENT", drOrderQueried["ApplyDept"].ToString(), ref stCols, ref  stVals);
                                StringBuilder sbScheduleddt = new StringBuilder();
                                sbScheduleddt.AppendFormat(((DateTime)drRP["RegisterDt"]).ToString("yyyy-MM-dd HH:mm:ss"));
                                AddColForInsert("SCHEDULED_DT", sbScheduleddt.ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PLACER", drOrderQueried["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("MODALITY", drRP["ModalityType"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("STATION_NAME", drRP["Modality"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("BODY_PART", drRP["BodyPart"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PROCEDURE_NAME", "", ref stCols, ref  stVals);
                                AddColForInsert("PROCEDURE_CODE", drRP["ProcedureCode"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PROCEDURE_DESC", drRP["Description"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CHARGE_STATUS", (Convert.ToInt32(drRP["IsCharge"]) == 0 ? "N" : "Y"), ref stCols, ref  stVals);
                                AddColForInsert("CHARGE_AMOUNT", drRP["Charge"].ToString(), ref stCols, ref  stVals);

                                AddColForInsert("FILLER", drOrderQueried["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("REF_PHYSICIAN", drOrderQueried["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("TECHNICIAN", drRP["TechGUID"].ToString(), ref stCols, ref  stVals);

                                AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);
                                AddColForInsert("FILLER_DEPARTMENT", drOrderQueried["ApplyDept"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                                AddColForInsert("FILLER_CONTACT", "", ref stCols, ref  stVals);
                                AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                                AddColForInsert("REQUEST_REASON", gwInfo.Observation, ref stCols, ref  stVals);
                                AddColForInsert("REUQEST_COMMENTS", gwInfo.VisitComment, ref stCols, ref  stVals);
                                AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                                AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_LOCATION", drRP["ModalityRoom"].ToString(), ref stCols, ref  stVals);//modality room
                                AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                                AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                                AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                                AddColForInsert("STUDY_INSTANCE_UID", drOrderQueried["StudyInstanceUID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                                AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_COMMENT", gwInfo.OrderComment, ref stCols, ref  stVals);
                                AddColForInsert("CNT_AGENT", "", ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_1", drOrderQueried["CardNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_2", drOrderQueried["HisID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_3", drOrderQueried["MedicareNo"].ToString(), ref stCols, ref  stVals);
                                strBuilder.AppendFormat("INSERT INTO GW_ORDER({0}) Values ({1})", stCols, stVals);

                                listSQL.Add(strBuilder.ToString());
                            }
                        }
                        bRet = true;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(
                            (long)ModuleEnum.QualityControl_DA,
                            ModuleInstanceName.QualityControl,
                            0,
                            ex.Message,
                            Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bRet;
        }

        **/

        public virtual bool UpdateOrderSendToGateServer(DataTable dtOrder, List<string> listSQL)
        {
            Debug.WriteLine("UpdateOrderSendToGateServer...");
            RisDAL oKodak = new RisDAL();
            bool bRet = false;

            try
            {
                DataRow drOrder = dtOrder.Rows[0];

                string stPatientGUID = drOrder["PatientGuid"].ToString();
                string stOrderGUID = drOrder["OrderGuid"].ToString();
                string strError = "";

                StringBuilder strBuilder = new StringBuilder();

                DataSet dsPatient = new DataSet();
                DataRow drPatient = null;
                DataTable dtPatient = new DataTable();

                //query patient information for gateway
                if (QueryPatientWithPatientGuid(stPatientGUID, ref dtPatient, ref strError))
                {
                    if (dtPatient == null) return false;
                    drPatient = dtPatient.Rows[0];
                }

                //query RP information for gateway
                DataTable dtRP = new DataTable();
                string strRPGuid = "";
                if (QueryRP(stOrderGUID, ref dtRP, ref strError, ref strRPGuid) && drPatient != null)
                {
                    try
                    {
                        strBuilder.Remove(0, strBuilder.Length);
                        foreach (DataRow drRP in dtRP.Rows)
                        {

                            ///////////////////////////// First give one new GUID
                            string strGWGuid = Guid.NewGuid().ToString();
                            ///////////////////////////// 2, get event type
                            int nEventType = 0;
                            int nExamStatus = 0;
                            if (drRP["RemoteRPID"].ToString().Trim().Length == 0)//
                            {
                                #region DEFECT EK_HI00073389

                                if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                                {
                                    nExamStatus = 101;
                                    nEventType = 21;
                                }
                                else
                                {
                                #endregion
                                    nEventType = 11;
                                    nExamStatus = 11;
                                }
                            }
                            else//From Electronic requisition
                            {
                                if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                                {
                                    nEventType = 21;
                                }
                                else
                                {
                                    nEventType = 11;
                                }
                                nExamStatus = 101;
                            }
                            ///////////////////////////// 3, insert index
                            //Here the event type of update patient info includes patient info and visit info and order info in GCRIS
                            strBuilder.Remove(0, strBuilder.Length);
                            //Data Index
                            strBuilder.AppendFormat("INSERT INTO GW_DATAINDEX(DATA_ID,DATA_DT,EVENT_TYPE,RECORD_INDEX_1,DATA_SOURCE) ")
                                    .AppendFormat("VALUES('{0}','{1}','{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), nEventType);
                            listSQL.Add(strBuilder.ToString());

                            logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                            #region get text table data 2008-07-21
                            DataSet ds = new DataSet();
                            string errInfo = "";
                            QueryOrderWithOrderID(stOrderGUID, ds, ref strError);
                            DataRow drOrderQueried = ds.Tables["Order"].Rows[0];


                            #endregion

                            ///////////////////////////// 4, insert patient
                            //Patient                                    
                            {
                                string stCols = "";
                                string stVals = "";
                                strBuilder.Remove(0, strBuilder.Length);
                                AddColForInsert("PATIENTID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("OTHER_PID", drPatient["RemotePID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_NAME", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_LOCAL_NAME", drPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                                StringBuilder sbBirthdate = new StringBuilder();
                                sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]));
                                AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                                AddColForInsert("SEX", drPatient["Gender"].ToString(), ref stCols, ref  stVals);
                                // AddColForInsert("PATIENT_ALIAS", gwInfo.Alias, ref stCols, ref  stVals);//new added
                                //  AddColForInsert("MARITAL_STATUS", gwInfo.Marital, ref stCols, ref  stVals);//new added
                                AddColForInsert("CUSTOMER_1", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);//new added
                                AddColForInsert("CUSTOMER_2", drPatient["IsVip"].ToString(), ref stCols, ref  stVals);//new added
                                AddColForInsert("CUSTOMER_3", drOrderQueried["InHospitalNo"].ToString(), ref stCols, ref  stVals);//new added
                                // AddColForInsert("CUSTOMER_4", gwInfo.PatientComment, ref stCols, ref  stVals);//new added    
                                AddColForInsert("ADDRESS", drPatient["Address"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PHONENUMBER_HOME", drPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_TYPE", drOrderQueried["PatientType"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_LOCATION", drOrderQueried["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("VISIT_NUMBER", drOrderQueried["ClinicNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("BED_NUMBER", drOrderQueried["BedNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                                AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                                strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                                listSQL.Add(strBuilder.ToString());
                                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                                    (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                            }


                            ///////////////////////////// 5, insert order
                            strBuilder.Remove(0, strBuilder.Length);
                            //get string for update order
                            {
                                ////build sql
                                string stCols = "";
                                string stVals = "";
                                AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                                AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                                AddColForInsert("ORDER_NO", drRP["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PLACER_NO", drOrderQueried["RemoteAccNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("FILLER_NO", drOrderQueried["AccNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_ID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("EXAM_STATUS", nExamStatus.ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PLACER_DEPARTMENT", drOrderQueried["ApplyDept"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("SCHEDULED_DT", drRP.getSafeDateTimeString("RegisterDt", "yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                                AddColForInsert("PLACER", drOrderQueried["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("MODALITY", drRP["ModalityType"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("STATION_NAME", drRP["Modality"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("BODY_PART", drRP["BodyPart"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PROCEDURE_NAME", "", ref stCols, ref  stVals);
                                AddColForInsert("PROCEDURE_CODE", drRP["ProcedureCode"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PROCEDURE_DESC", drRP["Description"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CHARGE_STATUS", (Convert.ToInt32(drRP["IsCharge"]) == 0 ? "N" : "Y"), ref stCols, ref  stVals);
                                AddColForInsert("CHARGE_AMOUNT", drRP["Charge"].ToString(), ref stCols, ref  stVals);

                                AddColForInsert("FILLER", drOrderQueried["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("REF_PHYSICIAN", drOrderQueried["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("TECHNICIAN", drRP["TechGUID"].ToString(), ref stCols, ref  stVals);

                                AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);
                                AddColForInsert("FILLER_DEPARTMENT", drOrderQueried["ApplyDept"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                                AddColForInsert("FILLER_CONTACT", "", ref stCols, ref  stVals);
                                AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                                //  AddColForInsert("REQUEST_REASON", gwInfo.Observation, ref stCols, ref  stVals);
                                //  AddColForInsert("REUQEST_COMMENTS", gwInfo.VisitComment, ref stCols, ref  stVals);
                                AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                                AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_LOCATION", drRP["ModalityRoom"].ToString(), ref stCols, ref  stVals);//modality room
                                AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                                AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                                AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                                AddColForInsert("STUDY_INSTANCE_UID", drOrderQueried["StudyInstanceUID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                                AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                                //  AddColForInsert("EXAM_COMMENT", gwInfo.OrderComment, ref stCols, ref  stVals);
                                AddColForInsert("CNT_AGENT", "", ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_1", drOrderQueried["CardNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_2", drOrderQueried["HisID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_3", drPatient["MedicareNo"].ToString(), ref stCols, ref  stVals);
                                strBuilder.AppendFormat("INSERT INTO GW_ORDER({0}) Values ({1})", stCols, stVals);

                                listSQL.Add(strBuilder.ToString());
                            }
                        }
                        bRet = true;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(
                            (long)ModuleEnum.QualityControl_DA,
                            ModuleInstanceName.QualityControl,
                            0,
                            ex.Message,
                            Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bRet;
        }
        /// <summary>
        /// Send the information about updating registered procedure(Order in Gateway) to Gateway Server
        /// </summary>
        /// <param name="dtRP">
        /// Contains all procedure(In gateway, it is order)
        /// </param>
        /// <param name="listSQL">
        /// Return list of SQL string used in this feature
        /// </param>
        /// <returns></returns>
        public virtual bool UpdateRPSendToGateServer(DataTable dtRP, List<string> listSQL)
        {
            Debug.WriteLine("UpdateRPSendToGateServer...");
            RisDAL oKodak = new RisDAL();
            StringBuilder strBuilder = new StringBuilder();
            bool bRet = false;

            try
            {
                DataRow drRP = dtRP.Rows[0];
                int nEventType = 0;
                int nExamStatus = 0;
                if (drRP["RemoteRPID"].ToString().Trim().Length == 0)//
                {
                    #region DEFECT EK_HI00073389

                    if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                    {
                        nEventType = 21;
                        nExamStatus = 101;
                    }
                    else
                    {
                    #endregion
                        nEventType = 11;//Update RP
                        nExamStatus = 11;
                    }
                }
                else//From Electronic requisition
                {
                    if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                    {
                        nEventType = 21;
                    }
                    else
                    {
                        nEventType = 11;
                    }
                    nExamStatus = 101;
                }


                //get other register infomation
                DataSet dsRegInfo = new DataSet();
                if (QueryRegInfoWithRPGUID(drRP["ProcedureGuid"].ToString(), dsRegInfo) == true)
                {

                    DataTable dtRegInfo = dsRegInfo.Tables["RegInfo"];
                    DataRow drRegInfo = dtRegInfo.Rows[0];
                    ////build sql
                    strBuilder.Remove(0, strBuilder.Length);
                    string strGWGuid = Guid.NewGuid().ToString();

                    //data index
                    strBuilder.Remove(0, strBuilder.Length);
                    strBuilder.AppendFormat("INSERT INTO GW_DATAINDEX(DATA_ID,DATA_DT,EVENT_TYPE,RECORD_INDEX_1,DATA_SOURCE) ")
                            .AppendFormat("VALUES('{0}','{1}','{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), nEventType);

                    listSQL.Add(strBuilder.ToString());


                    //Patient

                    string stCols = "";
                    string stVals = "";
                    strBuilder.Remove(0, strBuilder.Length);
                    AddColForInsert("PATIENTID", drRegInfo["PatientID"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("OTHER_PID", drRegInfo["RemotePID"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_NAME", drRegInfo["EnglishName"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_LOCAL_NAME", drRegInfo["LocalName"].ToString(), ref stCols, ref  stVals);
                    StringBuilder sbBirthdate = new StringBuilder();
                    sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drRegInfo["Birthday"]), ((DateTime)drRegInfo["Birthday"]), ((DateTime)drRegInfo["Birthday"]));
                    AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                    AddColForInsert("SEX", drRegInfo["Gender"].ToString(), ref stCols, ref  stVals);
                    // AddColForInsert("PATIENT_ALIAS", gwInfo.Alias, ref stCols, ref  stVals);//new added
                    //  AddColForInsert("MARITAL_STATUS", gwInfo.Marital, ref stCols, ref  stVals);//new added
                    AddColForInsert("CUSTOMER_1", drRegInfo["EnglishName"].ToString(), ref stCols, ref  stVals);//new added
                    AddColForInsert("CUSTOMER_2", drRegInfo["IsVip"].ToString(), ref stCols, ref  stVals);//new added
                    AddColForInsert("CUSTOMER_3", drRegInfo["InHospitalNo"].ToString(), ref stCols, ref  stVals);//new added
                    // AddColForInsert("CUSTOMER_4", gwInfo.PatientComment, ref stCols, ref  stVals);//new added
                    AddColForInsert("ADDRESS", drRegInfo["Address"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PHONENUMBER_HOME", drRegInfo["Telephone"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_TYPE", drRegInfo["PatientType"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_LOCATION", drRegInfo["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("VISIT_NUMBER", drRegInfo["ClinicNo"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("BED_NUMBER", drRegInfo["BedNo"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                    AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                    strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                    listSQL.Add(strBuilder.ToString());
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


                    //order
                    strBuilder.Remove(0, strBuilder.Length);
                    stCols = "";
                    stVals = "";
                    AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                    AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                    AddColForInsert("ORDER_NO", drRegInfo["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PLACER_NO", drRegInfo["RemoteAccNo"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("FILLER_NO", drRegInfo["AccNo"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_ID", drRegInfo["PatientID"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("EXAM_STATUS", nExamStatus.ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PLACER_DEPARTMENT", drRegInfo["ApplyDept"].ToString(), ref stCols, ref  stVals);

                    AddColForInsert("SCHEDULED_DT", drRP.getSafeDateTimeString("RegisterDt", "yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);

                    AddColForInsert("PLACER", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("MODALITY", drRegInfo["ModalityType"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("STATION_NAME", drRegInfo["Modality"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("BODY_PART", drRegInfo["BodyPart"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PROCEDURE_NAME", "", ref stCols, ref  stVals);
                    AddColForInsert("PROCEDURE_CODE", drRegInfo["ProcedureCode"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PROCEDURE_DESC", drRegInfo["Description"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("CHARGE_STATUS", (Convert.ToInt32(drRP["IsCharge"]) == 0 ? "N" : "Y"), ref stCols, ref  stVals);
                    AddColForInsert("CHARGE_AMOUNT", drRP["Charge"].ToString(), ref stCols, ref  stVals);

                    AddColForInsert("FILLER", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("REF_PHYSICIAN", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("TECHNICIAN", drRP["technician"].ToString(), ref stCols, ref  stVals);

                    AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);
                    AddColForInsert("FILLER_DEPARTMENT", drRegInfo["ApplyDept"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                    AddColForInsert("FILLER_CONTACT", "", ref stCols, ref  stVals);
                    AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                    // AddColForInsert("REQUEST_REASON", gwInfo.Observation, ref stCols, ref  stVals);
                    // AddColForInsert("REUQEST_COMMENTS", gwInfo.VisitComment, ref stCols, ref  stVals);
                    AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                    AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                    AddColForInsert("EXAM_LOCATION", drRP["ModalityRoom"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                    AddColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                    AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                    AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                    AddColForInsert("STUDY_INSTANCE_UID", drRegInfo["StudyInstanceUID"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                    AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                    // AddColForInsert("EXAM_COMMENT", gwInfo.OrderComment, ref stCols, ref  stVals);
                    AddColForInsert("CNT_AGENT", "", ref stCols, ref  stVals);
                    AddColForInsert("CUSTOMER_1", drRegInfo["CardNo"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("CUSTOMER_2", drRegInfo["HisID"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("CUSTOMER_3", drRegInfo["MedicareNo"].ToString(), ref stCols, ref  stVals);

                    strBuilder.AppendFormat("INSERT INTO GW_ORDER({0}) Values ({1})", stCols, stVals);

                    listSQL.Add(strBuilder.ToString());
                }
                bRet = true;
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bRet;
        }
        /// <summary>
        /// Send the information about order deleting
        /// </summary>
        /// <param name="stOrderGUID">
        /// Order GUID
        /// </param>
        /// <param name="stVisitGUID">
        /// Visit GUID
        /// </param>
        /// <param name="listSQL">
        /// Return list of SQL string used in this feature
        /// </param>
        /// <returns></returns>
        public virtual bool DeleteOrderSendToGateServer(string stOrderGUID, string stVisitGUID, List<string> listSQL, string strRPGuid,string strLoginName,string strLocalName)
        {
            Debug.WriteLine("DeleteOrderSendToGateServer...");
            RisDAL oKodak = new RisDAL();
            bool bRet = false;
            string stError = "";

            try
            {
                //
                StringBuilder strBuilder = new StringBuilder();

                //get patient information with visitID
                DataSet dsPatient = new DataSet();
                DataRow drPatient = null;
                DataTable dtPatient = null;
                string strPatientGuid = "";
                string strPatientName = "";

                //query patient information for gateway
                //if (QueryPatientWithVisitGUID(stVisitGUID, dsPatient))
                if (QueryPatientWithOrderID(stOrderGUID, dsPatient, ref stError))
                {
                    dtPatient = dsPatient.Tables["Patient"];
                    if (dtPatient == null)
                    //If the patient can not be found
                    //the visit GUID must be incorrect
                    {
                        return false;
                    }
                    //get information
                    if (dtPatient.Rows.Count > 0)
                    {
                        drPatient = dtPatient.Rows[0];
                    }
                    else
                    //no data return, 
                    {
                        return false;
                    }
                }

                //query order information for gateway
                DataSet dsOrder = new DataSet();
                DataRow drOrder = null;
                DataTable dtOrder = null;
                if (QueryOrderWithOrderID(stOrderGUID, dsOrder, ref stError))
                {
                    dtOrder = dsOrder.Tables["Order"];
                    if (dtOrder == null)
                    //order not found only when it has been removed by another session
                    //or the GUID is incorrect
                    {
                        return false;
                    }
                    if (dtOrder.Rows.Count > 0)
                    //to avoid exception
                    {
                        drOrder = dtOrder.Rows[0];
                    }
                    else
                    {
                        return false;
                    }
                }


                //query RP information for gateway
                DataTable dtRP = new DataTable();
                string strError = "";
                if (QueryRP(stOrderGUID, ref dtRP, ref strError, ref strRPGuid))
                {
                    try
                    //add try catch here, so that even when one RP is not handled properly, other RP not be affected
                    {
                        strBuilder.Remove(0, strBuilder.Length);
                        foreach (DataRow drRP in dtRP.Rows)
                        {

                            ///////////////////////////// First give one new GUID
                            string strGWGuid = Guid.NewGuid().ToString();
                            ///////////////////////////// 2, get event type
                            int nEventType = 0;
                            int nExamStatus = 0;
                            if (drRP["RemoteRPID"].ToString().Trim().Length == 0)//
                            {
                                #region DEFECT EK_HI00073389

                                if (Convert.ToInt32(drRP["Status"]) == 10)
                                //Only send the RP that has checked in
                                {
                                    //continue;
                                    nEventType = 23;
                                    nExamStatus = 103;
                                }
                                else
                                {
                                #endregion
                                    nEventType = 13;
                                    nExamStatus = 13;
                                }
                            }
                            else//From Electronic requisition
                            {
                                nEventType = 23;
                                nExamStatus = 103;
                            }
                            ///////////////////////////// 3, insert index
                            //Here the event type of update patient info includes patient info and visit info and order info in GCRIS
                            strBuilder.Remove(0, strBuilder.Length);
                            //Data Index
                            strBuilder.AppendFormat("INSERT INTO GW_DATAINDEX(DATA_ID,DATA_DT,EVENT_TYPE,RECORD_INDEX_1,DATA_SOURCE) ")
                                    .AppendFormat("VALUES('{0}','{1}','{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), nEventType);
                            listSQL.Add(strBuilder.ToString());

                            logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                            ///////////////////////////// 4, insert patient

                            //Patient

                            string stCols = "";
                            string stVals = "";
                            strBuilder.Remove(0, strBuilder.Length);
                            AddColForInsert("PATIENTID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("OTHER_PID", drPatient["RemotePID"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("PATIENT_NAME", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("PATIENT_LOCAL_NAME", drPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                            StringBuilder sbBirthdate = new StringBuilder();
                            sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]));
                            AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                            AddColForInsert("SEX", drPatient["Gender"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("CUSTOMER_1", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);//new added
                            AddColForInsert("CUSTOMER_3", drOrder["InHospitalNo"].ToString(), ref stCols, ref  stVals);//new added
                            // AddColForInsert("CUSTOMER_4", gwInfo.PatientComment, ref stCols, ref  stVals);//new added
                            //  AddColForInsert("PATIENT_ALIAS", gwInfo.Alias, ref stCols, ref  stVals);//new added
                            //  AddColForInsert("MARITAL_STATUS", gwInfo.Marital, ref stCols, ref  stVals);//new added
                            //AddColForInsert("PatientComment", gwInfo.PatientComment, ref stCols, ref  stVals);//new added
                            AddColForInsert("ADDRESS", drPatient["Address"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("PHONENUMBER_HOME", drPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("PATIENT_TYPE", drOrder["PatientType"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("PATIENT_LOCATION", drOrder["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("VISIT_NUMBER", drOrder["ClinicNo"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("BED_NUMBER", drOrder["BedNo"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("CUSTOMER_2", drPatient["IsVIP"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                            AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                            strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                            listSQL.Add(strBuilder.ToString());
                            logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


                            ///////////////////////////// 5, insert order
                            strBuilder.Remove(0, strBuilder.Length);
                            //get string for update order
                            {
                                ////build sql
                                stCols = "";
                                stVals = "";
                                AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                                AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                                AddColForInsert("ORDER_NO", drRP["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PLACER_NO", drOrder["RemoteAccNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("FILLER_NO", drOrder["AccNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_ID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("EXAM_STATUS", nExamStatus.ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PLACER_DEPARTMENT", drOrder["ApplyDept"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PLACER", drOrder["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("MODALITY", drRP["ModalityType"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("STATION_NAME", drRP["Modality"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("BODY_PART", drRP["BodyPart"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PROCEDURE_NAME", "", ref stCols, ref  stVals);
                                AddColForInsert("PROCEDURE_CODE", drRP["ProcedureCode"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PROCEDURE_DESC", drRP["Description"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CHARGE_STATUS", (Convert.ToInt32(drRP["IsCharge"]) == 0 ? "N" : "Y"), ref stCols, ref  stVals);
                                AddColForInsert("CHARGE_AMOUNT", drRP["Charge"].ToString(), ref stCols, ref  stVals);

                                AddColForInsert("FILLER", drOrder["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("REF_PHYSICIAN", drOrder["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("TECHNICIAN", drRP["TechGUID"].ToString(), ref stCols, ref  stVals);

                                AddColForInsert("SCHEDULED_DT", drRP.getSafeDateTimeString("RegisterDt", "yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);

                                AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);

                                AddColForInsert("FILLER_DEPARTMENT", drOrder["ApplyDept"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                                AddColForInsert("FILLER_CONTACT", strLoginName + "|" + strLocalName, ref stCols, ref  stVals);
                                AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                                // AddColForInsert("REQUEST_REASON", gwInfo.Observation, ref stCols, ref  stVals);
                                // AddColForInsert("REUQEST_COMMENTS", gwInfo.VisitComment, ref stCols, ref  stVals);
                                AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                                AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_LOCATION", drRP["ModalityRoom"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                                AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                                AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                                AddColForInsert("STUDY_INSTANCE_UID", drOrder["StudyInstanceUID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                                AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                                // AddColForInsert("EXAM_COMMENT", gwInfo.OrderComment, ref stCols, ref  stVals);
                                AddColForInsert("CNT_AGENT", "", ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_1", drOrder["CardNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_2", drOrder["HisID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_3", drPatient["MedicareNo"].ToString(), ref stCols, ref  stVals);
                                


                                strBuilder.AppendFormat("INSERT INTO GW_ORDER({0}) Values ({1})", stCols, stVals);

                                listSQL.Add(strBuilder.ToString());
                            }
                            bRet = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(
                            (long)ModuleEnum.QualityControl_DA,
                            ModuleInstanceName.QualityControl,
                            0,
                            ex.Message,
                            Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(),
                            (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                    }
                }
                else
                {
                    //it is reasonable that no data returned because the order might does not contain a procedure
                }
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bRet;
        }

        /// <summary>
        /// Send the information about patient merging to Gateway Server
        /// </summary>
        /// <param name="dtGWOrders">
        /// Contains information of all gateway order affected by this operation
        /// </param>
        /// <param name="dtPatient">
        /// information of the target patient merged to
        /// </param>
        /// /// 
        /// <param name="listSQL">
        /// Return list of SQL string used in this feature
        /// </param>
        /// <returns>if successfull return true else false</returns>
        public virtual bool MergePatientSendToGateServer(DataTable dtGWOrders, DataTable dtTargetPatient, List<string> listSQL, bool bMergeOrder)
        {
            Debug.WriteLine("MergePatientSendToGateServer...");
            //the function will be done next build
            RisDAL oKodak = new RisDAL();
            StringBuilder strBuilder = new StringBuilder();
            bool bRet = false;

            try
            {
                //verify input
                if (dtTargetPatient == null) return false;
                if (dtTargetPatient.Rows.Count == 0) return false;

                DataRow drTargetPatient = dtTargetPatient.Rows[0];
                foreach (DataRow drRP in dtGWOrders.Rows)
                {
                    string stEventType = "";
                    int nExamStatus = 0;

                    //get other register information
                    DataSet dsRegInfo = new DataSet();
                    if (QueryRegInfoWithRPGUID(drRP["ProcedureGuid"].ToString(), dsRegInfo) == true)
                    {
                        DataTable dtRegInfo = dsRegInfo.Tables["RegInfo"];
                        DataRow drRegInfo = dtRegInfo.Rows[0];
                        if (drRegInfo == null)
                        {
                            logger.Error((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                                String.Format("Can not find register infomation of RP with ProcedureGUID: ") + drRP["ProcedureGuid"].ToString(), Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                            continue;
                        }
                        ////build SQL
                        ////////////////////////////1, Create GUID                        
                        string strGWGuid = Guid.NewGuid().ToString();

                        ////////////////////////////2, Find Event Type

                        if (drRP["RemoteRPID"].ToString().Trim().Length == 0)//
                        {
                            if (bMergeOrder == true)
                            {
                                //request by integration group. Merge one order should use '11'
                                stEventType = "19";//Merge RP
                                nExamStatus = 12;
                            }
                            else
                            {
                                stEventType = "02";//Merge patient
                            }
                            #region DEFECT EK_HI00073389 not check in order also can be send to GW

                            if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                            {
                                stEventType = "19";//Merge RP
                                nExamStatus = 102;
                            }
                            else
                            {
                            #endregion
                                //stEventType = "11";//Merge Paient
                                nExamStatus = 12;
                            }
                        }
                        else//From Electronic requisition
                        {
                            if (bMergeOrder == true)
                            {
                                //request by integration group. Merge one order should use '11'
                                if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                                {
                                    stEventType = "19";//Merge RP
                                    nExamStatus = 102;
                                }
                                else
                                {
                                    stEventType = "19";//Merge RP
                                    nExamStatus = 12;
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                                {
                                    stEventType = "02";//Merge patient
                                    nExamStatus = 102;
                                }
                                else
                                {
                                    stEventType = "02";//Merge patient
                                    nExamStatus = 12;
                                }
                            }

                        } 
                        ////////////////////////////3, Insert Index
                        //
                        strBuilder.Remove(0, strBuilder.Length);
                        strBuilder.AppendFormat("INSERT INTO GW_DATAINDEX(DATA_ID,DATA_DT,EVENT_TYPE,RECORD_INDEX_1,DATA_SOURCE) ")
                                .AppendFormat("VALUES('{0}','{1}','{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), stEventType);
                        listSQL.Insert(0, strBuilder.ToString());

                        ////////////////////////////4, Insert Patient

                        string stCols = "";
                        string stVals = "";
                        strBuilder.Remove(0, strBuilder.Length);
                        AddColForInsert("PATIENTID", drTargetPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("OTHER_PID", drTargetPatient["RemotePID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_NAME", drTargetPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCAL_NAME", drTargetPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PRIOR_PATIENT_ID", drRP["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PRIOR_PATIENT_NAME", drRP["LocalName"].ToString(), ref stCols, ref  stVals);
                        StringBuilder sbBirthdate = new StringBuilder();
                        sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drTargetPatient["Birthday"]), ((DateTime)drTargetPatient["Birthday"]), ((DateTime)drTargetPatient["Birthday"]));
                        AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SEX", drTargetPatient["Gender"].ToString(), ref stCols, ref  stVals);
                        // AddColForInsert("PATIENT_ALIAS", gwInfo.Alias, ref stCols, ref  stVals);//new added
                        // AddColForInsert("MARITAL_STATUS", gwInfo.Marital, ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_1", drTargetPatient["EnglishName"].ToString(), ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_3", drRegInfo["InHospitalNo"].ToString(), ref stCols, ref  stVals);//new added
                        // AddColForInsert("CUSTOMER_4", gwInfo.PatientComment, ref stCols, ref  stVals);//new added                        AddColForInsert("ADDRESS", drTargetPatient["Address"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_2", drTargetPatient["IsVIP"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PHONENUMBER_HOME", drTargetPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_TYPE", drRegInfo["PatientType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCATION", drRegInfo["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("VISIT_NUMBER", drRegInfo["ClinicNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BED_NUMBER", drRegInfo["BedNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("DRIVERLIC_NUMBER", drTargetPatient["ReferenceNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SSN_NUMBER", drTargetPatient["MedicareNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_ALIAS", drTargetPatient["Alias"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("MARITAL_STATUS", drTargetPatient["Marriage"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);


                        strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                        listSQL.Insert(0, strBuilder.ToString());
                        logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                        ////////////////////////////5, Insert Order
                        strBuilder.Remove(0, strBuilder.Length);
                        //get string for update order

                        ////build sql
                        stCols = "";
                        stVals = "";
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                        AddColForInsert("ORDER_NO", drRP["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER_NO", drRegInfo["RemoteAccNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("FILLER_NO", drRegInfo["AccNo"].ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("PATIENT_ID", drRegInfo["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_ID", drTargetPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("EXAM_STATUS", nExamStatus.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER_DEPARTMENT", drRegInfo["ApplyDept"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("MODALITY", drRP["ModalityType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("STATION_NAME", drRP["Modality"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BODY_PART", drRegInfo["BodyPart"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_NAME", "", ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_CODE", drRP["ProcedureCode"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_DESC", drRegInfo["Description"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CHARGE_STATUS", (Convert.ToInt32(drRP["IsCharge"]) == 0 ? "N" : "Y"), ref stCols, ref  stVals);
                        AddColForInsert("CHARGE_AMOUNT", drRegInfo["Charge"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SCHEDULED_DT", drRP.getSafeDateTimeString("RegisterDt", "yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                        AddColForInsert("FILLER", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("REF_PHYSICIAN", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("TECHNICIAN", drRegInfo["Technician"].ToString(), ref stCols, ref  stVals);

                        AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("FILLER_DEPARTMENT", drRegInfo["ApplyDept"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                        AddColForInsert("FILLER_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                        // AddColForInsert("REQUEST_REASON", gwInfo.Observation, ref stCols, ref  stVals);
                        //  AddColForInsert("REUQEST_COMMENTS", gwInfo.VisitComment, ref stCols, ref  stVals);
                        AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                        AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_LOCATION", drRP["ModalityRoom"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                        AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                        AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                        AddColForInsert("STUDY_INSTANCE_UID", drRegInfo["StudyInstanceUID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                        AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                        // AddColForInsert("EXAM_COMMENT", gwInfo.OrderComment, ref stCols, ref  stVals);
                        AddColForInsert("CNT_AGENT", "", ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_1", drRegInfo["CardNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_2", drRegInfo["HisID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_3", drRegInfo["MedicareNo"].ToString(), ref stCols, ref  stVals);

                        strBuilder.AppendFormat("INSERT INTO GW_ORDER({0}) Values ({1})", stCols, stVals);

                        listSQL.Insert(0, strBuilder.ToString());//delete operation should be at last
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bRet;
        }


        /// <summary>
        /// Send the information about RP move to Gateway Server
        /// </summary>
        /// <param name="dtGWOrders">
        /// Contains information of all gateway order affected by this operation
        /// </param>
        /// <param name="dtPatient">
        /// information of the target patient merged to
        /// </param>
        /// /// 
        /// <param name="listSQL">
        /// Return list of SQL string used in this feature
        /// </param>
        /// <returns>if successfull return true else false</returns>
        public virtual bool MoveRPSendToGateServer(DataTable dtGWOrders, DataTable dtTargetPatient, string targetOrderGuid, List<string> listSQL, bool bMergeOrder)
        {
            Debug.WriteLine("MergePatientSendToGateServer...");
            //the function will be done next build
            RisDAL oKodak = new RisDAL();
            StringBuilder strBuilder = new StringBuilder();
            bool bRet = false;

            try
            {
                //verify input
                if (dtTargetPatient == null) return false;
                if (dtTargetPatient.Rows.Count == 0) return false;

                DataRow drTargetPatient = dtTargetPatient.Rows[0];
                string strGWGuid = "";
                foreach (DataRow drRP in dtGWOrders.Rows)
                {
                    string NewEventType = "";
                    int NewExamStatus = 0;
                    string CancelEventType = "";
                    int CancelExamStatus = 0;
                    string stEventType = "";
                    int nExamStatus = 0;
                    DataRow drRegInfo = null;
                    DataSet ds = new DataSet();
                    GWInfo gwInfo = new GWInfo();

                    //get other register information
                    DataSet dsRegInfo = new DataSet();
                    if (QueryRegInfoWithRPGUID(drRP["ProcedureGuid"].ToString(), dsRegInfo) == true)
                    {
                        DataTable dtRegInfo = dsRegInfo.Tables["RegInfo"];
                        drRegInfo = dtRegInfo.Rows[0];
                        if (drRegInfo == null)
                        {
                            logger.Error((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                                String.Format("Can not find register infomation of RP with ProcedureGUID: ") + drRP["ProcedureGuid"].ToString(), Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(),
                                (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                            continue;
                        }
                        ////build SQL
                        ////////////////////////////1, Create GUID                        
                        strGWGuid = Guid.NewGuid().ToString();

                        ////////////////////////////2, Find Event Type


                        if (Convert.ToInt32(drRP["Status"]) < 20)//appointment confirmed
                        {
                            if (bMergeOrder == false)
                            {
                                stEventType = "19";
                                nExamStatus = 102;
                            }
                            else
                            {
                                NewEventType = "17";
                                NewExamStatus = 101;//appointment confirmed
                                CancelEventType = "18";
                                CancelExamStatus = 101;
                            }
                        }
                        else
                        {
                            if (bMergeOrder == false)
                            {
                                stEventType = "19";
                                nExamStatus = 12;
                            }
                            else
                            {
                                NewEventType = "17";//new order
                                NewExamStatus = 11;
                                CancelEventType = "18";
                                CancelExamStatus = 11;
                            }
                        }

                    }
                    DataSet dsTarget = new DataSet();
                    DataTable dtTargetOrderInfo = null;
                    DataRow drTargetOrder = null;
                    string error = "";
                    QueryOrderWithOrderID(targetOrderGuid, ds, ref error);
                    if (ds.Tables.Contains("Order"))
                    {
                        dtTargetOrderInfo = ds.Tables["Order"];
                        drTargetOrder = dtTargetOrderInfo.Rows[0];
                    }
                    if (bMergeOrder == false)
                    {
                        #region move RP
                        ////////////////////////////3, Insert Index
                        //
                        strBuilder.Remove(0, strBuilder.Length);
                        strBuilder.AppendFormat("INSERT INTO GW_DATAINDEX(DATA_ID,DATA_DT,EVENT_TYPE,RECORD_INDEX_1,DATA_SOURCE) ")
                                .AppendFormat("VALUES('{0}','{1}','{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), stEventType);
                        listSQL.Insert(0, strBuilder.ToString());

                        ////////////////////////////4, Insert Patient

                        string stCols = "";
                        string stVals = "";
                        strBuilder.Remove(0, strBuilder.Length);
                        AddColForInsert("PATIENTID", drTargetPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("OTHER_PID", drTargetPatient["RemotePID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_NAME", drTargetPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCAL_NAME", drTargetPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PRIOR_PATIENT_ID", drRP["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PRIOR_PATIENT_NAME", drRP["LocalName"].ToString(), ref stCols, ref  stVals);
                        StringBuilder sbBirthdate = new StringBuilder();
                        sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drTargetPatient["Birthday"]), ((DateTime)drTargetPatient["Birthday"]), ((DateTime)drTargetPatient["Birthday"]));
                        AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SEX", drTargetPatient["Gender"].ToString(), ref stCols, ref  stVals);
                        // AddColForInsert("PATIENT_ALIAS", gwInfo.Alias, ref stCols, ref  stVals);//new added
                        // AddColForInsert("MARITAL_STATUS", gwInfo.Marital, ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_1", drTargetPatient["EnglishName"].ToString(), ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_3", drRegInfo["InHospitalNo"].ToString(), ref stCols, ref  stVals);//new added
                        // AddColForInsert("CUSTOMER_4", gwInfo.PatientComment, ref stCols, ref  stVals);//new added                        AddColForInsert("ADDRESS", drTargetPatient["Address"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_2", drTargetPatient["IsVIP"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PHONENUMBER_HOME", drTargetPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_TYPE", drRegInfo["PatientType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCATION", drRegInfo["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("VISIT_NUMBER", drRegInfo["ClinicNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BED_NUMBER", drRegInfo["BedNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("DRIVERLIC_NUMBER", drTargetPatient["ReferenceNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SSN_NUMBER", drTargetPatient["MedicareNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_ALIAS", drTargetPatient["Alias"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("MARITAL_STATUS", drTargetPatient["Marriage"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);


                        strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                        listSQL.Insert(0, strBuilder.ToString());
                        logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                        ////////////////////////////5, Insert Order
                        strBuilder.Remove(0, strBuilder.Length);
                        //get string for update order

                        ////build sql
                        stCols = "";
                        stVals = "";
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                        AddColForInsert("ORDER_NO", drRP["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER_NO", drRegInfo["RemoteAccNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("FILLER_NO", drTargetOrder["AccNo"].ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("PATIENT_ID", drRegInfo["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_ID", drTargetPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("EXAM_STATUS", nExamStatus.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER_DEPARTMENT", drRegInfo["ApplyDept"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("MODALITY", drRP["ModalityType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("STATION_NAME", drRP["Modality"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BODY_PART", drRegInfo["BodyPart"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_NAME", "", ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_CODE", drRP["ProcedureCode"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_DESC", drRegInfo["Description"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CHARGE_STATUS", (Convert.ToInt32(drRP["IsCharge"]) == 0 ? "N" : "Y"), ref stCols, ref  stVals);
                        AddColForInsert("CHARGE_AMOUNT", drRegInfo["Charge"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SCHEDULED_DT", drRP.getSafeDateTimeString("RegisterDt", "yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                        AddColForInsert("FILLER", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("REF_PHYSICIAN", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("TECHNICIAN", drRegInfo["Technician"].ToString(), ref stCols, ref  stVals);

                        AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("FILLER_DEPARTMENT", drRegInfo["ApplyDept"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                        AddColForInsert("FILLER_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                        // AddColForInsert("REQUEST_REASON", gwInfo.Observation, ref stCols, ref  stVals);
                        //  AddColForInsert("REUQEST_COMMENTS", gwInfo.VisitComment, ref stCols, ref  stVals);
                        AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                        AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_LOCATION", drRP["ModalityRoom"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                        AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                        AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                        AddColForInsert("STUDY_INSTANCE_UID", drRegInfo["StudyInstanceUID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                        AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                        // AddColForInsert("EXAM_COMMENT", gwInfo.OrderComment, ref stCols, ref  stVals);
                        AddColForInsert("CNT_AGENT", "", ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_1", drRegInfo["CardNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_2", drRegInfo["HisID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_3", drRegInfo["MedicareNo"].ToString(), ref stCols, ref  stVals);

                        strBuilder.AppendFormat("INSERT INTO GW_ORDER({0}) Values ({1})", stCols, stVals);

                        listSQL.Insert(0, strBuilder.ToString());//delete operation should be at last
                        #endregion
                    }
                    else
                    {
                        #region 1. delete the old order to gateway

                        ////////////////////////////3, Insert Index
                        //
                        strBuilder.Remove(0, strBuilder.Length);
                        strBuilder.AppendFormat("INSERT INTO GW_DATAINDEX(DATA_ID,DATA_DT,EVENT_TYPE,RECORD_INDEX_1,DATA_SOURCE) ")
                                .AppendFormat("VALUES('{0}','{1}','{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), CancelEventType);
                        listSQL.Add(strBuilder.ToString());

                        ////////////////////////////4, Insert Patient

                        string stCols = "";
                        string stVals = "";
                        strBuilder.Remove(0, strBuilder.Length);
                        AddColForInsert("PATIENTID", drTargetPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("OTHER_PID", drTargetPatient["RemotePID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_NAME", drTargetPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCAL_NAME", drTargetPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PRIOR_PATIENT_ID", drRP["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PRIOR_PATIENT_NAME", drRP["LocalName"].ToString(), ref stCols, ref  stVals);
                        StringBuilder sbBirthdate = new StringBuilder();
                        sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drTargetPatient["Birthday"]), ((DateTime)drTargetPatient["Birthday"]), ((DateTime)drTargetPatient["Birthday"]));
                        AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SEX", drTargetPatient["Gender"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_ALIAS", gwInfo.Alias, ref stCols, ref  stVals);//new added
                        AddColForInsert("MARITAL_STATUS", gwInfo.Marital, ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_1", drTargetPatient["EnglishName"].ToString(), ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_3", drTargetOrder["InHospitalNo"].ToString(), ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_4", gwInfo.PatientComment, ref stCols, ref  stVals);//new added                        AddColForInsert("ADDRESS", drTargetPatient["Address"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_2", drTargetPatient["IsVIP"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PHONENUMBER_HOME", drTargetPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_TYPE", drTargetOrder["PatientType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCATION", drTargetOrder["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("VISIT_NUMBER", drTargetOrder["ClinicNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BED_NUMBER", drTargetOrder["BedNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);


                        strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                        listSQL.Add(strBuilder.ToString());
                        logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                        ////////////////////////////5, Insert Order
                        strBuilder.Remove(0, strBuilder.Length);
                        //get string for update order

                        ////build sql
                        stCols = "";
                        stVals = "";
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                        AddColForInsert("ORDER_NO", drRP["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER_NO", drTargetOrder["RemoteAccNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("FILLER_NO", drTargetOrder["AccNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_ID", drTargetPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("EXAM_STATUS", CancelExamStatus.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER_DEPARTMENT", drTargetOrder["ApplyDept"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER", drTargetOrder["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("MODALITY", drRP["ModalityType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("STATION_NAME", drRP["Modality"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BODY_PART", drRP["BodyPart"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_NAME", "", ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_CODE", drRP["ProcedureCode"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_DESC", drRP["Description"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CHARGE_STATUS", (Convert.ToInt32(drRP["IsCharge"]) == 0 ? "N" : "Y"), ref stCols, ref  stVals);
                        AddColForInsert("CHARGE_AMOUNT", drRP["Charge"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SCHEDULED_DT", drRP.getSafeDateTimeString("RegisterDt", "yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                        AddColForInsert("FILLER", drTargetOrder["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("REF_PHYSICIAN", drTargetOrder["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("TECHNICIAN", drRP["Technician"].ToString(), ref stCols, ref  stVals);

                        AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("FILLER_DEPARTMENT", drTargetOrder["ApplyDept"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                        AddColForInsert("FILLER_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("REQUEST_REASON", gwInfo.Observation, ref stCols, ref  stVals);
                        AddColForInsert("REUQEST_COMMENTS", gwInfo.VisitComment, ref stCols, ref  stVals);
                        AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                        AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_LOCATION", drRP["ModalityRoom"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                        AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                        AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                        AddColForInsert("STUDY_INSTANCE_UID", drTargetOrder["StudyInstanceUID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                        AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_COMMENT", gwInfo.OrderComment, ref stCols, ref  stVals);
                        AddColForInsert("CNT_AGENT", "", ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_1", drTargetOrder["CardNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_2", drTargetOrder["HisID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_3", drTargetPatient["MedicareNo"].ToString(), ref stCols, ref  stVals);

                        strBuilder.AppendFormat("INSERT INTO GW_ORDER({0}) Values ({1})", stCols, stVals);

                        listSQL.Add(strBuilder.ToString());

                        #endregion

                        #region 2. create an order to gateway 
                        strGWGuid = Guid.NewGuid().ToString();//new one again
                        ////////////////////////////3, Insert Index
                        //
                        strBuilder.Remove(0, strBuilder.Length);
                        strBuilder.AppendFormat("INSERT INTO GW_DATAINDEX(DATA_ID,DATA_DT,EVENT_TYPE,RECORD_INDEX_1,DATA_SOURCE) ")
                                .AppendFormat("VALUES('{0}','{1}','{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), NewEventType);
                        listSQL.Add(strBuilder.ToString());

                        ////////////////////////////4, Insert Patient

                        stCols = "";
                        stVals = "";
                        strBuilder.Remove(0, strBuilder.Length);
                        AddColForInsert("PATIENTID", drRegInfo["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("OTHER_PID", drRegInfo["RemotePID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_NAME", drRegInfo["EnglishName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCAL_NAME", drRegInfo["LocalName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PRIOR_PATIENT_ID", "", ref stCols, ref  stVals);
                        AddColForInsert("PRIOR_PATIENT_NAME", "", ref stCols, ref  stVals);
                        sbBirthdate.Remove(0, sbBirthdate.Length);
                        sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drRegInfo["Birthday"]), ((DateTime)drRegInfo["Birthday"]), ((DateTime)drRegInfo["Birthday"]));
                        AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SEX", drRegInfo["Gender"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_ALIAS", gwInfo.Alias, ref stCols, ref  stVals);//new added
                        AddColForInsert("MARITAL_STATUS", gwInfo.Marital, ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_1", drRegInfo["EnglishName"].ToString(), ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_3", drRegInfo["InHospitalNo"].ToString(), ref stCols, ref  stVals);//new added
                        AddColForInsert("CUSTOMER_4", gwInfo.PatientComment, ref stCols, ref  stVals);//new added                        AddColForInsert("ADDRESS", drTargetPatient["Address"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_2", drRegInfo["IsVIP"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PHONENUMBER_HOME", drRegInfo["Telephone"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_TYPE", drRegInfo["PatientType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCATION", drRegInfo["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("VISIT_NUMBER", drRegInfo["ClinicNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BED_NUMBER", drRegInfo["BedNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);


                        strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                        listSQL.Add(strBuilder.ToString());
                        logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                            (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                        ////////////////////////////5, Insert Order
                        strBuilder.Remove(0, strBuilder.Length);
                        //get string for update order

                        ////build sql
                        stCols = "";
                        stVals = "";
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                        AddColForInsert("ORDER_NO", drRP["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER_NO", drRegInfo["RemoteAccNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("FILLER_NO", drRegInfo["AccNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_ID", drRegInfo["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("EXAM_STATUS", NewExamStatus.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER_DEPARTMENT", drRegInfo["ApplyDept"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("MODALITY", drRP["ModalityType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("STATION_NAME", drRP["Modality"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BODY_PART", drRegInfo["BodyPart"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_NAME", "", ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_CODE", drRP["ProcedureCode"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_DESC", drRegInfo["Description"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CHARGE_STATUS", (Convert.ToInt32(drRP["IsCharge"]) == 0 ? "N" : "Y"), ref stCols, ref  stVals);
                        AddColForInsert("CHARGE_AMOUNT", drRegInfo["Charge"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SCHEDULED_DT", drRP.getSafeDateTimeString("RegisterDt", "yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                        AddColForInsert("FILLER", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("REF_PHYSICIAN", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("TECHNICIAN", drRegInfo["Technician"].ToString(), ref stCols, ref  stVals);

                        AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("FILLER_DEPARTMENT", drRegInfo["ApplyDept"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                        AddColForInsert("FILLER_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("REQUEST_REASON", gwInfo.Observation, ref stCols, ref  stVals);
                        AddColForInsert("REUQEST_COMMENTS", gwInfo.VisitComment, ref stCols, ref  stVals);
                        AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                        AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_LOCATION", drRP["ModalityRoom"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                        AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                        AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                        AddColForInsert("STUDY_INSTANCE_UID", drRegInfo["StudyInstanceUID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                        AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_COMMENT", gwInfo.OrderComment, ref stCols, ref  stVals);
                        AddColForInsert("CNT_AGENT", "", ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_1", drRegInfo["CardNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_2", drRegInfo["HisID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_3", drRegInfo["MedicareNo"].ToString(), ref stCols, ref  stVals);

                        strBuilder.AppendFormat("INSERT INTO GW_ORDER({0}) Values ({1})", stCols, stVals);

                        listSQL.Add(strBuilder.ToString());
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bRet;
        }

        #endregion
        #region utility
        /// <summary>
        /// Create sql clause
        /// </summary>
        /// <param name="stCol"></param>
        /// <param name="stVal"></param>
        /// <param name="stColList"></param>
        /// <param name="stValList"></param>
        protected void AddColForInsert(string stCol, string stVal, ref string stColList, ref string stValList)
        {
            //get col name
            if (stColList.Length == 0)
            {
                stColList = stCol;
            }
            else
            {
                stColList = stColList + "," + stCol;
            }

            //Update ' with ''
            stVal = stVal.Replace("'", "''");

            //get value
            if (stValList.Length == 0)
            {
                stValList = "N'" + stVal + "'";
            }
            else
            {
                stValList = stValList + "," + "N'" + stVal + "'";
            }

        }

        public virtual bool PatientExist(string strPatientGUID, ref string strError)
        {
            bool bReturn = false;
            DataTable dt = new DataTable();
            RisDAL oKodak = new RisDAL();
            try
            {
                strPatientGUID = strPatientGUID.Trim();
                if (strPatientGUID.Length == 0)
                {
                    throw new Exception("PatientID is empty");
                }

                string strSQL = string.Format("SELECT tRegPatient.PatientGuid from tRegPatient WHERE tRegPatient.PatientGuid='{0}' ", strPatientGUID);
                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strSQL, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                Debug.WriteLine(strSQL);
                oKodak.ExecuteQuery(strSQL, dt);

                if (dt != null && dt.Rows.Count > 0)
                {

                    bReturn = true;
                }

            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bReturn; ;
        }
        public virtual bool PatientExist(string strPatientGUID, ref string strPatientName, ref string strError)
        {
            bool bReturn = false;
            DataTable dt = new DataTable();
            RisDAL oKodak = new RisDAL();
            try
            {
                strPatientGUID = strPatientGUID.Trim();
                if (strPatientGUID.Length == 0)
                {
                    throw new Exception("PatientID is empty");
                }

                string strSQL = string.Format("SELECT tRegPatient.PatientGuid,LocalName from tRegPatient WHERE tRegPatient.PatientGuid='{0}' ", strPatientGUID);
                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strSQL, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                Debug.WriteLine(strSQL);
                oKodak.ExecuteQuery(strSQL, dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    strPatientName = dt.Rows[0]["LocalName"].ToString();
                    bReturn = true;
                }

            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bReturn; ;
        }
        public virtual bool PatientExist(string strPatientGUID, ref DataTable dtPatient, ref string strError)
        {
            if (dtPatient == null)
            {
                strError = "Internal error";
                return false;
            }

            bool bReturn = false;
            RisDAL oKodak = new RisDAL();
            try
            {
                strPatientGUID = strPatientGUID.Trim();
                if (strPatientGUID.Length == 0)
                {
                    throw new Exception("PatientID is empty");
                }

                string strSQL = string.Format("SELECT * from tRegPatient WHERE tRegPatient.PatientGuid='{0}' ", strPatientGUID);
                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strSQL, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                Debug.WriteLine(strSQL);
                oKodak.ExecuteQuery(strSQL, dtPatient);

                if (dtPatient.Rows.Count > 0)
                {
                    bReturn = true;
                }
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bReturn; ;
        }

        public bool QueryRegInfoWithRPGUID(string stRPGUID, DataSet dsRegInfo)
        {
            bool bReturn = false;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                stRPGUID = stRPGUID.Trim();


                string strSQL = string.Format(" SELECT tRegPatient.*, tRegOrder.* ,tRegProcedure.*,tProcedureCode.* ");
                strSQL += string.Format(" FROM tRegProcedure ,tProcedureCode  ,tRegPatient ,tRegOrder  ");
                strSQL += string.Format(" WHERE tRegProcedure.ProcedureCode=tProcedureCode.ProcedureCode   ");
                strSQL += string.Format(" and tRegProcedure.OrderGUID = tRegOrder.OrderGUID  ");
                strSQL += string.Format(" and tRegPatient.PatientGuid  = tRegOrder.PatientGUID");
                //strSQL += string.Format(" and tRegOrder.VisitGUID  = tRegVisit.VisitGUID ");
                strSQL += string.Format(" and tRegOrder.OrderGUID  = tRegProcedure.OrderGUID ");
                strSQL += string.Format(" and tRegProcedure.ProcedureGuid = '{0}' ", stRPGUID);

                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strSQL, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                Debug.WriteLine(strSQL);
                oKodak.ExecuteQuery(strSQL, dt);

                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        dt.TableName = "RegInfo";
                        dsRegInfo.Tables.Add(dt);
                        bReturn = true;
                    }
                }
                if (!bReturn)
                {
                    logger.Warn((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                        "Can not find patient of this order", Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                }

            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bReturn; ;
        }
        public bool QueryPatientWithVisitGUID(string stVisitGUID, DataSet dsPatient)
        {
            bool bReturn = false;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                stVisitGUID = stVisitGUID.Trim();

                string strSQL = string.Format("SELECT tRegPatient.* from tRegPatient,tRegVisit WHERE tRegPatient.PatientGuid ");
                strSQL += string.Format(" = tRegVisit.PatientGUID and tRegVisit.VisitGUID = '{0}' ", stVisitGUID);

                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strSQL, Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                Debug.WriteLine(strSQL);
                oKodak.ExecuteQuery(strSQL, dt);

                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        dt.TableName = "Patient";
                        dsPatient.Tables.Add(dt);
                        bReturn = true;
                    }
                }
                if (!bReturn)
                {
                    logger.Warn((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                        "Can not find patient of this order", Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                }

            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bReturn; ;
        }
        public virtual bool QueryLatestVisitWithPatientGUID(string stPatientGUID, DataSet dsVisit)
        {
            logger.Info(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    "QueryLatestVisitWithPatientGUID",
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            bool bReturn = false;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                stPatientGUID = stPatientGUID.Trim();
                string strSQL = string.Format("SELECT top 1 tRegVisit.* from tRegPatient,tRegVisit WHERE tRegPatient.PatientGuid ");
                strSQL += string.Format(" = tRegVisit.PatientGUID and tRegPatient.PatientGUID = '{0}' order by tRegVisit.CreateDt desc ", stPatientGUID);

                logger.Info(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    strSQL,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                oKodak.ExecuteQuery(strSQL, dt);

                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        dt.TableName = "Order";
                        dsVisit.Tables.Add(dt);
                        bReturn = true;
                    }
                }
                if (!bReturn)
                {
                    logger.Warn((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                        "Can not find patient of this order", Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                }


            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
            return bReturn; ;
        }
        public virtual bool QueryPatientWithOrderID(string strOrderGuid, ref string strPatientGUID, ref string strPatientName, ref string strError)
        {
            bool bReturn = true;
            DataTable dt = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {
                if (strOrderGuid == null || strOrderGuid.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }
                string strSQL = " SELECT A.PatientGUID, A.LocalName" +
                " FROM tRegPatient A,tRegOrder B " +
                " WHERE A.PatientGUID=B.PatientGUID   " +
                string.Format(" AND B.OrderGuid='{0}' ", strOrderGuid);

                logger.Info(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    string.Format("Query Patient With Order ID: ") + strSQL,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                Debug.WriteLine(strSQL);
                dt = oKodak.ExecuteQuery(strSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        strPatientGUID = dt.Rows[0]["PatientGUID"].ToString();
                        strPatientName = dt.Rows[0]["LocalName"].ToString();
                    }
                }

                logger.Info(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    string.Format("Patient ID: ") + strPatientGUID + "  PatientName: " + strPatientName,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool QueryPatientWithOrderID(string strOrderGuid, DataSet ds, ref string strError)
        {
            bool bReturn = false;
            DataTable dt = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {
                if (strOrderGuid == null || strOrderGuid.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }
                string strSQL = " SELECT tRegPatient.* FROM tRegPatient,tRegOrder WHERE tRegPatient.PatientGuid = tRegOrder.PatientGuid and  tRegOrder.OrderGuid ='" + strOrderGuid + "'";

                logger.Info(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    string.Format("Query Patient With Order ID: ") + strSQL,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                Debug.WriteLine(strSQL);
                dt = oKodak.ExecuteQuery(strSQL);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        dt.TableName = "Patient";
                        ds.Tables.Add(dt);
                        bReturn = true;
                    }
                }

            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        private bool QueryPatientWithPatientGuid(string strPatientGuid, ref DataTable dtPatient, ref string strError)
        {
            bool bReturn = true;

            RisDAL oKodak = new RisDAL();
            try
            {

                string strSQL = " SELECT * from tRegPatient Where PatientGuid = '" + strPatientGuid + "'";

                Debug.WriteLine(strSQL);
                dtPatient = oKodak.ExecuteQuery(strSQL);
                dtPatient.TableName = "Patient";
                if (dtPatient != null)
                {
                    if (dtPatient.Rows.Count > 0)
                    {
                        bReturn = true;
                    }
                    else
                    {
                        strError = "No This Patient";
                        bReturn = false;
                    }
                }
                else
                {
                    strError = "No This Patient";
                }

            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
        /// <summary>
        /// Used in MergePatient, to get all RPs belongs to one patient
        /// </summary>
        /// <param name="strPatientGuid"></param>
        /// <param name="dtRP"></param>
        /// <returns></returns>
        public virtual bool QueryRPWithPatientGUIDForGateWay(string strPatientGuid, DataTable dtRP)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            try
            {
                string strSQL = " select tregprocedure.*,tregorder.ApplyDoctor,tregorder.ApplyDept,tregorder.AccNo,tregpatient.patientid,tregpatient.patientguid,tregpatient.localname,(select Room from tModality where tModality.Modality = tRegprocedure.Modality) as  ModalityRoom from tregpatient,tregprocedure,tregorder where tregprocedure.orderguid = tregorder.orderguid  and tregorder.patientguid = tregpatient.patientguid ";
                strSQL += string.Format(" and tregorder.patientguid = '{0}'", strPatientGuid);

                Debug.WriteLine(strSQL);
                dtRP.Merge(oKodak.ExecuteQuery(strSQL));
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        /// <summary>
        /// Used in MergePatient, to get all RPs belongs to one order
        /// </summary>
        /// <param name="strOrderGuid"></param>
        /// <param name="dtRP"></param>
        /// <returns></returns>
        public virtual bool QueryRPWithOrderGUIDForGateWay(string strOrderGuid, DataTable dtRP)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            try
            {
                string strSQL = " select tregprocedure.*,tprocedurecode.*,tregorder.ApplyDoctor,tregorder.ApplyDept,tregorder.AccNo,tregpatient.patientid,tregpatient.patientguid,tregpatient.localname,(select Room from tModality where tModality.Modality = tRegprocedure.Modality) as  ModalityRoom from tregpatient,tregprocedure,tregorder,tprocedurecode where tregprocedure.orderguid = tregorder.orderguid  and tregorder.patientguid = tregpatient.patientguid and tregprocedure.procedurecode = tprocedurecode.procedurecode";
                strSQL += string.Format(" and tregorder.orderguid = '{0}'", strOrderGuid);

                Debug.WriteLine(strSQL);
                dtRP.Merge(oKodak.ExecuteQuery(strSQL));
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        /// <summary>
        /// Used in MergePatient, to get one RP
        /// </summary>
        /// <param name="strOrderGuid"></param>
        /// <param name="dtRP"></param>
        /// <returns></returns>
        public virtual bool QueryRPWithRPGUIDForGateWay(string strRPGuid, DataTable dtRP)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            try
            {
                string strSQL = " select tprocedurecode.*, tregprocedure.*,tregorder.ApplyDoctor,tregorder.ApplyDept,tregorder.AccNo,tregpatient.patientid,tregpatient.patientguid,tregpatient.localname,(select Room from tModality where tModality.Modality = tRegprocedure.Modality) as  ModalityRoom from tregpatient,tregprocedure,tregorder,tprocedurecode where tregprocedure.procedurecode = tprocedurecode.procedurecode and tregprocedure.orderguid = tregorder.orderguid  and tregorder.patientguid = tregpatient.patientguid ";
                strSQL += string.Format(" and tregProcedure.ProcedureGuid = '{0}'", strRPGuid);

                Debug.WriteLine(strSQL);
                dtRP.Merge(oKodak.ExecuteQuery(strSQL));
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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
        /// <summary>
        /// Used in MergePatient, to get all RPs belongs to one visit
        /// </summary>
        /// <param name="strVisitGuid"></param>
        /// <param name="dtRP"></param>
        /// <returns></returns>
        public virtual bool QueryRPWithVisitGUIDForGateWay(string strVisitGuid, DataTable dtRP)
        {
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            try
            {
                string strSQL = " select tregpatient.patientGUID, tregpatient.LocalName,tregpatient.patientid,tregprocedure.*,tregvisit.visitguid,tregorder.ApplyDoctor,tregorder.ApplyDept,tregorder.AccNo,(select Room from tModality where tModality.Modality = tRegprocedure.Modality) as  ModalityRoom  from tregprocedure,tregorder,tregvisit,tregpatient where tregprocedure.orderguid = tregorder.orderguid and tregorder.visitguid = tregvisit.visitguid and tregvisit.patientguid = tregpatient.patientguid ";
                strSQL += string.Format(" and tregvisit.visitGUID = '{0}'", strVisitGuid);
                Debug.WriteLine(strSQL);
                dtRP.Merge(oKodak.ExecuteQuery(strSQL));
            }
            catch (Exception ex)
            {
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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




        /// <summary>
        /// check whether these orders have reports
        /// </summary>
        /// <param name="dicOrdersGuids">the order guid dictionary</param>
        /// <param name="strOrderGuid">if check false, return the false order guid</param>
        /// <returns>true if ok, alse false, and the false order</returns>
        public bool CheckOrdersHaveReports(Dictionary<string, string> dicOrdersGuids, ref string strOrderGuid)
        {
            RisDAL dal = new RisDAL();
            string sqlOrderExistsReport = "";
            int orderCount = 0;
            bool bReturn = true;

            try
            {
                foreach (string orderGuid in dicOrdersGuids.Keys)
                {
                    sqlOrderExistsReport = string.Format("select count(*) from tReport A ,tRegProcedure B where  A.ReportGuid = B.ReportGuid  and B.OrderGuid", orderGuid);
                    object oReturn = dal.ExecuteScalar(sqlOrderExistsReport, RisDAL.ConnectionState.KeepOpen);
                    if (oReturn != null)
                    {
                        orderCount = Convert.ToInt32(oReturn);
                        if (orderCount > 0)
                        {
                            strOrderGuid = orderGuid;
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strOrderGuid = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return bReturn;
        }

        /// <summary>
        /// check whether these guids are exists in DB
        /// </summary>
        /// <param name="dicGuids">the guid dictionary which should be check</param>
        /// <param name="strGuid">the guid of the record which is not exists in DB</param>
        /// <param name="operationType">merge patient(0), merge order(1), move order(2) or move RP(3)</param>
        /// <param name="strTargetGuid">the target guid which should be check</param>
        /// <param name="dtLockInfo">the locked info if order locked</param>
        /// <returns>true if ok else false , and the false return the check false guid</returns>
        public bool CheckOperationSync(Dictionary<string, string> dicGuids, ref string strGuid, int operationType, string strTargetGuid, ref DataTable dtLockInfo)
        {
            RisDAL dal = new RisDAL();
            string sqlCheckSrc = "";
            string sqlCheckTarget = "";
            int orderCount = 0;
            bool bReturn = true;

            switch (operationType)
            {
                case 0://merge patient
                    sqlCheckSrc = "select count(*) from tRegPatient where PatientGuid = '{0}'";
                    sqlCheckTarget = string.Format("select count(*) from tRegPatient where PatientGuid = '{0}'", strTargetGuid);
                    break;
                case 1://merge order
                    sqlCheckSrc = "select count(*) from tRegOrder where OrderGuid = '{0}'";
                    sqlCheckTarget = string.Format("select count(*) from tRegOrder where OrderGuid = '{0}'", strTargetGuid);
                    break;
                case 2://move order
                    sqlCheckSrc = "select count(*) from tRegOrder where OrderGuid = '{0}'";
                    sqlCheckTarget = string.Format("select count(*) from tRegPatient where PatientGuid = '{0}'", strTargetGuid);
                    break;
                case 3://move rp
                    sqlCheckSrc = "select count(*) from tRegProcedure where ProcedureGuid = '{0}'";
                    sqlCheckTarget = string.Format("select count(*) from tRegOrder where OrderGuid = '{0}'", strTargetGuid);
                    break;

                #region merge order by level, US26313, 2015-07-30
                case 4: //merge charge
                    sqlCheckSrc = "select count(*) from tOrderCharge where OrderGuid = '{0}'";
                    sqlCheckTarget = string.Format("select count(*) from tOrderCharge where OrderGuid = '{0}'", strTargetGuid);
                    break; 
                #endregion
                default:
                    break;
            }

            try
            {
                //check  wether target guid exists
                object objTargetCount = dal.ExecuteScalar(sqlCheckTarget, RisDAL.ConnectionState.KeepOpen);
                if (objTargetCount != null)
                {
                    orderCount = Convert.ToInt32(objTargetCount);
                    if (orderCount == 0)
                    {
                        strGuid = strTargetGuid;
                        return false;
                    }
                }

                //check  wether source guid exists
                foreach (string guid in dicGuids.Keys)
                {
                    sqlCheckSrc = string.Format(sqlCheckSrc, guid);
                    object objSrcCount = dal.ExecuteScalar(sqlCheckSrc, RisDAL.ConnectionState.KeepOpen);
                    if (objSrcCount != null)
                    {
                        orderCount = Convert.ToInt32(objSrcCount);
                        if (orderCount == 0)
                        {
                            strGuid = guid;
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strGuid = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
            }
            finally
            {
                if (dal != null)
                {
                    dal.Dispose();
                }
            }
            return bReturn;
        }

        #endregion

        #region PatientRelation
        public virtual bool RelatePatient(string strPatientGuids,ref string strError)
        {
            Debug.WriteLine("RelatePatient...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {
                string newRelatedID = Guid.NewGuid().ToString();
                string sqlOldRelatedID = string.Format("select distinct RelatedID from tPatientList where len(RelatedID)>0 and PatientGuid in({0})", strPatientGuids);
                string sqlUpdateRelatePatient = string.Format("Update tRegPatient set RelatedID ='{0}' where PatientGuid in({1}) ", newRelatedID,strPatientGuids);
                       sqlUpdateRelatePatient += string.Format("Update tPatientList set RelatedID ='{0}' where PatientGuid in({1})", newRelatedID, strPatientGuids);
                oKodak.BeginTransaction();
                DataTable dt = oKodak.ExecuteQuery(sqlOldRelatedID,RisDAL.ConnectionState.KeepOpen);
                string oldRelatedIDs = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        oldRelatedIDs += "'"+dr[0].ToString()+"',";
                    }
                    oldRelatedIDs = oldRelatedIDs.TrimEnd(",".ToCharArray());
                    string sqlUpdateRelatePatientByRelatedID = string.Format(" Update tRegPatient set RelatedID ='{0}' where RelatedID in({1}) ", newRelatedID, oldRelatedIDs);
                    sqlUpdateRelatePatientByRelatedID += string.Format(" Update tPatientList set RelatedID ='{0}' where RelatedID in({1}) ", newRelatedID, oldRelatedIDs);
                    oKodak.ExecuteNonQuery(sqlUpdateRelatePatientByRelatedID,RisDAL.ConnectionState.KeepOpen);
                                        
                }
                int result = oKodak.ExecuteNonQuery(sqlUpdateRelatePatient,RisDAL.ConnectionState.KeepOpen);                
                //some patients have not exists yet.
                if (result < 2)
                {
                    oKodak.RollbackTransaction();
                    bReturn = false;
                }
                else
                {
                    oKodak.CommitTransaction();

                    //Relate patient Hippa log
                    string sqlGetPatientInfo = string.Format("select localname,patientid from tpatientlist where relatedid ='{0}'", newRelatedID);
                    DataTable dtPatients = new DataTable();
                    dtPatients = oKodak.ExecuteQuery(string.Format(sqlGetPatientInfo, strPatientGuids), RisDAL.ConnectionState.KeepOpen);
                    foreach (DataRow drPatient in dtPatients.Rows)
                    {
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientRecordEvtMsg(
                           "RP", Convert.ToString(drPatient["patientid"]), Convert.ToString(drPatient["localname"]), newRelatedID, true);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool UnRelatePatient(string strPatientGuid,ref string strError)
        {
            Debug.WriteLine("UnRelatePatient...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();

            try
            {
                Object obj = oKodak.ExecuteScalar(string.Format("select relatedid from tregpatient where patientguid='{0}'", strPatientGuid));
                string strRelatedId = Convert.ToString(obj);

                string sqlOnly2Related = string.Format("Select count(1) from tPatientList Where RelatedID =(select distinct RelatedID from tPatientList where PatientGuid ='{0}') and len(RelatedID) >0", strPatientGuid);
                string sqlUnRelate2Patient = string.Format("Update tRegPatient set RelatedID ='' where  RelatedID =(select distinct RelatedID from tregpatient where PatientGuid ='{0}')", strPatientGuid);
                       sqlUnRelate2Patient += string.Format(" Update tPatientList set RelatedID ='' where  RelatedID ='{0}' ", strRelatedId);
                string sqlUnRelatedPatient = string.Format("Update tRegPatient set RelatedID ='' where PatientGuid ='{0}'", strPatientGuid);
                       sqlUnRelatedPatient += string.Format("Update tPatientList set RelatedID ='' where PatientGuid ='{0}'", strPatientGuid);
                object objResult = oKodak.ExecuteScalar(sqlOnly2Related, RisDAL.ConnectionState.KeepOpen);
                if (objResult != null)
                 {
                     if (Convert.ToInt32(objResult) <= 2)
                     {
                         oKodak.ExecuteNonQuery(sqlUnRelate2Patient);
                     }
                     else if (Convert.ToInt32(objResult) > 2)
                     {
                         oKodak.ExecuteNonQuery(sqlUnRelatedPatient);
                     }
                 }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool QueryPatientByRelatedID(string strPatientGuid,string strRelatedID, DataSet ds, ref string strError)
        {
            Debug.WriteLine("QueryPatientByRelatedID...");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                //this sql is used to get basic information of patient
                //Selected is always false and this column will be used by client for multi selection
                string strSQLByRelateID = "SELECT 1 as selected,0 as Target, A.PatientGuid,A.PatientID,A.LocalName,A.Birthday,A.EnglishName,A.ParentName,case when a.isvip= 1 then 'Y' else 'N' end as IsVIP,A.Comments," +
                    "A.Gender," +
                    "A.Address,A.Telephone, A.CreateDt,A.ReferenceNo,A.MedicareNo,A.RemotePID,A.MedicareNo,A.Alias,A.Marriage,A.RelatedID FROM tPatientList A WHERE  RelatedID='"+strRelatedID+"'";

                string strSQLByPatientGuid = "SELECT 1 as selected,0 as Target, A.PatientGuid,A.PatientID,A.LocalName,A.Birthday,A.EnglishName,A.ParentName,case when a.isvip= 1 then 'Y' else 'N' end as IsVIP,A.Comments," +
                    "A.Gender," +
                    "A.Address,A.Telephone, A.CreateDt,A.ReferenceNo,A.MedicareNo,A.RemotePID,A.MedicareNo,A.Alias,A.Marriage,A.RelatedID FROM tPatientList A WHERE  RelatedID=(Select RelatedID From tPatientList Where PatientGuid ='" + strPatientGuid + "') and len(RelatedID) >0";
                Debug.WriteLine(strSQLByRelateID);
                string strSQLSelection = string.IsNullOrWhiteSpace(strRelatedID) ? strSQLByPatientGuid : strSQLByRelateID;
                dt = oKodak.ExecuteQuery(strSQLSelection);
                dt.TableName = "Patient";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool RelatePatientsByCondition(string patientGuids, string birthDateOffset, string createDateRange, DataSet dsResult, ref string strError)
        {
            Debug.WriteLine("RelatePatient...");
            bool bReturn = true;
            int birthDateOffsetYear = 0;
            Int32.TryParse(birthDateOffset, out birthDateOffsetYear);
            RisDAL oKodak = new RisDAL();
            DataTable dtRelated = new DataTable("tRelated");
            dtRelated.Columns.Add("PatientGuid");
            try
            {
                string cmpPatientInfo = "select LocalName,Gender,Birthday,Telephone,RelatedID from tPatientList where PatientGuid ='{0}'";
                string countRelateSql = @" select PatientGuid from tPatientList where Archive = 0 and LocalName = '{0}' and Gender = '{1}' and
                                           Telephone ='{2}'  and len(Telephone) >0 and len('{2}') > 0 and 
                                           ";
                string birthdaySql = birthDateOffset == "0" ? " abs(DATEDIFF(day,Birthday,'{3}')) = 0" : " abs(DATEDIFF(day,Birthday,'{3}')) <= {4}";
                countRelateSql += birthdaySql;
                countRelateSql += " and CreateDt between '" + createDateRange.Split(",".ToCharArray())[0] + "' and '" + createDateRange.Split(",".ToCharArray())[1]+"'";
                foreach (string onePatientGuid in patientGuids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    DataTable dt = oKodak.ExecuteQuery(string.Format(cmpPatientInfo, onePatientGuid));
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        continue;
                    }

                    dt = oKodak.ExecuteQuery(string.Format(countRelateSql, dt.Rows[0]["LocalName"].ToString(), dt.Rows[0]["Gender"].ToString(), dt.Rows[0]["Telephone"].ToString(), dt.Rows[0]["Birthday"].ToString(), birthDateOffsetYear *365));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 1)//two or more patients are saw likely!
                        {
                            string needRelatedPatients = "";
                            foreach (DataRow dr in dt.Rows)
                            {
                                 needRelatedPatients += "'" + dr["PatientGuid"].ToString() + "',";
                            }
                            needRelatedPatients = needRelatedPatients.TrimEnd(",".ToCharArray());
                            if (needRelatedPatients.Length > 0)
                            {
                                if (RelatePatient(needRelatedPatients, ref strError))
                                {
                                    DataRow dr = dtRelated.NewRow();
                                    dr["PatientGuid"] = onePatientGuid;
                                    dtRelated.Rows.Add(dr);
                                }
                                dtRelated.AcceptChanges();
                            }
                        }
                    }
                }
                dsResult.Tables.Add(dtRelated);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        #region Charge Data Management
        public virtual bool ChargeQueryOrderList(string strCondition, DataSet ds, ref string strError)
        {
            Debug.WriteLine("ChargeQueryOrderList...");
            bool bReturn = true;
            DataTable dt = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {
                if (strCondition == null || strCondition.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("Select distinct tRegPatient.LocalName,tRegPatient.Gender,tRegPatient.Birthday,tRegPatient.PatientID,tRegOrder.HISID,tRegOrder.PatientType,tRegOrder.OrderGuid,");
                sb.Append("tRegOrder.AccNo,tRegOrder.RemoteAccNo,tRegOrder.InhospitalNo,tRegOrder.InhospitalRegion,tRegOrder.ChargeType,tRegOrder.CreateDt as RegisterDt,");
                sb.Append("tRegOrder.TotalFee,tRegOrder.ApplyDoctor,tRegOrder.ApplyDept, dbo.GetDescriptions(tRegOrder.OrderGuid) as Descriptions,");
                sb.Append("dbo.GetChargeStatus(tRegOrder.OrderGuid) as ChargeStatuses,dbo.GetModalityTypes(tRegOrder.OrderGuid) as ModalityType ");
                sb.Append(" from tRegPatient inner join tRegOrder on tRegPatient.PatientGuid = tRegOrder.PatientGuid ");
                //strCondition = strCondition.Length > 0 ? "And " + strCondition : strCondition;
                if (strCondition.Contains("tOrderCharge"))
                {
                    sb.Append(" left join tOrderCharge on tOrderCharge.OrderGuid = tRegOrder.OrderGuid ");
                }
                if (strCondition.Contains("tRegprocedure"))
                {
                    sb.Append(" left join tRegprocedure on tRegprocedure.OrderGuid = tRegOrder.OrderGuid ");
                }
                sb.Append(" where " + strCondition);
                dt = oKodak.ExecuteQuery(sb.ToString());
                dt.TableName = "ChargeOrderInfo";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool ChargeQueryItemList(string orderGuid, DataSet ds, ref string strError)
        {
            Debug.WriteLine("ChargeQueryItemList...");
            bool bReturn = true;
            DataTable dt = new DataTable();

            RisDAL oKodak = new RisDAL();
            try
            {
                if (orderGuid == null || orderGuid.Trim().Length == 0)
                //no input
                {
                    throw new Exception("Invalid input para");
                }

                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("OrderGuid", orderGuid);
                string sqlSelect = @"Select ChargeGuid,OrderGuid,Code,Description,Amount,Price,Unit,Confirm,(select localName from tUser where UserGuid=Confirmer) as Confirmer,ConfirmDt,ConfirmReason,
                                Deduct,(select localName from tUser where UserGuid=Deducter) as Deducter,DeductDt,DeductReason,Refund,(select localName from tUser where UserGuid=Refunder) as Refunder,RefundDt,RefundReason,Cancel,(select localName from tUser where UserGuid=canceler) as Canceler,CancelDt,
                                CancelReason, LastAction,LastStatus,optional from tOrderCharge Where OrderGuid=@OrderGuid order by CreateDt desc";
                dt = oKodak.ExecuteQuery(sqlSelect);
                dt.TableName = "ChargeItemInfo";
                ds.Tables.Add(dt);

                sqlSelect = @"select rp.ProcedureGuid, rp.ModalityType,rp.Modality,rp.Charge,rp.ProcedureCode,pc.Description 
                                from tRegProcedure rp inner join tProcedureCode pc on rp.ProcedureCode=pc.ProcedureCode 
                                where rp.OrderGuid =@OrderGuid";
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("OrderGuid", orderGuid);
                dt = oKodak.ExecuteQuery(sqlSelect);
                dt.TableName = "StudyItems";
                ds.Tables.Add(dt);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
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

        public virtual bool AddCharge(ChargeModel chargeModel, decimal totalFee, ref string strError)
        {
            Debug.WriteLine("ChargeQueryItemList...");
            bool bReturn = true;

            RisDAL oKodak = new RisDAL();
            try
            {
                string curDomain = CommonGlobalSettings.Utilities.GetCurDomain();
                string sqlInsert = @"Insert tOrderCharge(ChargeGuid,OrderGuid,Code,Description,Amount,Price,Unit,Confirm,Confirmer,ConfirmDt,ConfirmReason,Deduct,Deducter,DeductDt,DeductReason,
                                    Refund,Refunder,RefundDt,RefundReason,Cancel,Canceler,CancelDt,CancelReason,LastAction,LastStatus,Optional,Domain)Values
                                    (@ChargeGuid,@OrderGuid,@Code,@Description,@Amount,@Price,@Unit,@Confirm,@Confirmer,@ConfirmDt,@ConfirmReason,@Deduct,@Deducter,@DeductDt,@DeductReason,
                                    @Refund,@Refunder,@RefundDt,@RefundReason,@Cancel,@Canceler,@CancelDt,@CancelReason,@LastAction,@LastStatus,@Optional,@Domain)";

                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("ChargeGuid", chargeModel.ChargeGuid);
                oKodak.Parameters.AddVarChar("OrderGuid", chargeModel.OrderGuid);
                oKodak.Parameters.AddVarChar("Code", chargeModel.Code);
                oKodak.Parameters.AddVarChar("Description", chargeModel.Description);
                oKodak.Parameters.AddInt("Amount", chargeModel.Amount);
                oKodak.Parameters.AddDecimal("Price", chargeModel.Price);
                oKodak.Parameters.AddVarChar("Unit", chargeModel.Unit);
                oKodak.Parameters.AddInt("Confirm", chargeModel.Confirm);
                oKodak.Parameters.AddVarChar("Confirmer", chargeModel.Confirmer);
                if (chargeModel.ConfirmDt.HasValue)
                {
                    oKodak.Parameters.AddDateTime("ConfirmDt", chargeModel.ConfirmDt.Value);
                }
                else
                {
                    oKodak.Parameters.AddDateTime("ConfirmDt", null);
                }
                oKodak.Parameters.AddVarChar("ConfirmReason", chargeModel.ConfirmReason);
                oKodak.Parameters.AddInt("Deduct", chargeModel.Deduct);
                oKodak.Parameters.AddVarChar("Deducter", chargeModel.Deducter);
                if (chargeModel.DeductDt.HasValue)
                {
                    oKodak.Parameters.AddDateTime("DeductDt", chargeModel.DeductDt.Value);
                }
                else
                {
                    oKodak.Parameters.AddDateTime("DeductDt", null);
                }
                oKodak.Parameters.AddVarChar("DeductReason", chargeModel.DeductReason);
                oKodak.Parameters.AddInt("Refund", chargeModel.Refund);
                oKodak.Parameters.AddVarChar("Refunder", chargeModel.Refunder);
                if (chargeModel.RefundDt.HasValue)
                {
                    oKodak.Parameters.AddDateTime("RefundDt", chargeModel.RefundDt.Value);
                }
                else
                {
                    oKodak.Parameters.AddDateTime("RefundDt", null);
                }
                oKodak.Parameters.AddVarChar("RefundReason", chargeModel.RefundReason);
                oKodak.Parameters.AddInt("Cancel", chargeModel.Cancel);
                oKodak.Parameters.AddVarChar("Canceler", chargeModel.Canceler);
                if (chargeModel.CancelDt.HasValue)
                {
                    oKodak.Parameters.AddDateTime("CancelDt", chargeModel.CancelDt.Value);
                }
                else
                {
                    oKodak.Parameters.AddDateTime("CancelDt", null);
                }
                oKodak.Parameters.AddVarChar("CancelReason", chargeModel.CancelReason);
                oKodak.Parameters.AddInt("LastAction", chargeModel.LastAction);
                oKodak.Parameters.AddInt("LastStatus", chargeModel.LastStatus);
                oKodak.Parameters.AddVarChar("Optional", chargeModel.Optional);
                oKodak.Parameters.AddVarChar("Domain", curDomain);

                oKodak.ExecuteNonQuery(sqlInsert);

                if (totalFee > 0)
                {
                    string sqlUpdate = @"Update tRegOrder set TotalFee=@TotalFee where OrderGuid =@OrderGuid";
                    oKodak.Parameters.Clear();
                    oKodak.Parameters.AddDecimal("TotalFee", totalFee);
                    oKodak.Parameters.AddVarChar("OrderGuid", chargeModel.OrderGuid);
                    oKodak.ExecuteNonQuery(sqlUpdate);
                }
                WriteHippaLog(CommonGlobalSettings.HippaName.ActionCode.Create, "Create charge ", chargeModel.OrderGuid,
                    chargeModel.ChargeGuid, chargeModel.Description, true);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                WriteHippaLog(CommonGlobalSettings.HippaName.ActionCode.Create, "Create charge ", chargeModel.OrderGuid,
    chargeModel.ChargeGuid, chargeModel.Description, false);
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

        public virtual bool ChargeOperation(Charge_Action action, ChargeModel chargeModel, decimal totalFee, ref string strError)
        {
            Debug.WriteLine("ChargeOperation...");
            bool bReturn = true;

            RisDAL oKodak = new RisDAL();
            try
            {
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("ChargeGuid", chargeModel.ChargeGuid);
                oKodak.Parameters.AddVarChar("Code", chargeModel.Code);
                oKodak.Parameters.AddVarChar("Description", chargeModel.Description);
                oKodak.Parameters.AddInt("Amount", chargeModel.Amount);
                oKodak.Parameters.AddDecimal("Price", chargeModel.Price);

                oKodak.Parameters.AddInt("LastAction", chargeModel.LastAction);
                oKodak.Parameters.AddInt("LastStatus", chargeModel.LastStatus);
                oKodak.Parameters.AddVarChar("Optional", chargeModel.Optional);
                string actionSql = "";
                switch (action)
                {
                    case Charge_Action.Cancel:
                        oKodak.Parameters.AddInt("Cancel", chargeModel.Cancel);
                        oKodak.Parameters.AddVarChar("Canceler", chargeModel.Canceler);
                        if (chargeModel.CancelDt.HasValue)
                        {
                            oKodak.Parameters.AddDateTime("CancelDt", chargeModel.CancelDt.Value);
                        }
                        else
                        {
                            oKodak.Parameters.AddDateTime("CancelDt", null);
                        }
                        oKodak.Parameters.AddVarChar("CancelReason", chargeModel.CancelReason);
                        actionSql = "Cancel=@Cancel,Canceler=@Canceler,CancelDt=@CancelDt,CancelReason=@CancelReason";
                        break;
                    case Charge_Action.Confirm:
                        oKodak.Parameters.AddInt("Confirm", chargeModel.Confirm);
                        oKodak.Parameters.AddVarChar("Confirmer", chargeModel.Confirmer);
                        if (chargeModel.ConfirmDt.HasValue)
                        {
                            oKodak.Parameters.AddDateTime("ConfirmDt", chargeModel.ConfirmDt.Value);
                        }
                        else
                        {
                            oKodak.Parameters.AddDateTime("ConfirmDt", null);
                        }
                        oKodak.Parameters.AddVarChar("ConfirmReason", chargeModel.ConfirmReason);
                        actionSql = "Confirm=@Confirm,Confirmer=@Confirmer,ConfirmDt=@ConfirmDt,ConfirmReason=@ConfirmReason";
                        break;
                    case Charge_Action.Deduct:
                        oKodak.Parameters.AddInt("Deduct", chargeModel.Deduct);
                        oKodak.Parameters.AddVarChar("Deducter", chargeModel.Deducter);
                        if (chargeModel.DeductDt.HasValue)
                        {
                            oKodak.Parameters.AddDateTime("DeductDt", chargeModel.DeductDt.Value);
                        }
                        else
                        {
                            oKodak.Parameters.AddDateTime("DeductDt", null);
                        }
                        oKodak.Parameters.AddVarChar("DeductReason", chargeModel.DeductReason);

                        actionSql = "Deduct=@Deduct,Deducter=@Deducter,DeductDt=@DeductDt,DeductReason=@DeductReason";
                        break;
                    case Charge_Action.Refund:
                        oKodak.Parameters.AddInt("Refund", chargeModel.Refund);
                        oKodak.Parameters.AddVarChar("Refunder", chargeModel.Refunder);
                        if (chargeModel.RefundDt.HasValue)
                        {
                            oKodak.Parameters.AddDateTime("RefundDt", chargeModel.RefundDt.Value);
                        }
                        else
                        {
                            oKodak.Parameters.AddDateTime("RefundDt", null);
                        }
                        oKodak.Parameters.AddVarChar("RefundReason", chargeModel.RefundReason);
                        actionSql = "Refund=@Refund,Refunder=@Refunder,RefundDt=@RefundDt,RefundReason=@RefundReason";
                        break;
                    case Charge_Action.Unknow:
                        break;
                    default:
                        throw new Exception("The action is not correct!");
                }

                string sqlUpdate = @"Update tOrderCharge set Code=@Code,Description=@Description,Amount=@Amount,Price=@Price," + actionSql
                                    + ",LastAction=@LastAction,LastStatus=@LastStatus,Optional=@Optional  Where ChargeGuid=@ChargeGuid";
                oKodak.ExecuteNonQuery(sqlUpdate);

                if (totalFee > 0)
                {
                    sqlUpdate = @"Update tRegOrder set TotalFee=@TotalFee where OrderGuid =@OrderGuid";
                    oKodak.Parameters.Clear();
                    oKodak.Parameters.AddDecimal("TotalFee", totalFee);
                    oKodak.Parameters.AddVarChar("OrderGuid", chargeModel.OrderGuid);
                    oKodak.ExecuteNonQuery(sqlUpdate);
                }
                WriteHippaLog(CommonGlobalSettings.HippaName.ActionCode.Update, "Update charge action:" + action.ToString(), chargeModel.OrderGuid,
                    chargeModel.ChargeGuid, chargeModel.Description, true);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                bReturn = false;
                logger.Error(
                    (long)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    ex.Message,
                    Application.StartupPath.ToString(),
                    (new System.Diagnostics.StackFrame(true)).GetFileName(),
                    (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                WriteHippaLog(CommonGlobalSettings.HippaName.ActionCode.Update, "Update charge action:" + action.ToString(), chargeModel.OrderGuid,
    chargeModel.ChargeGuid, chargeModel.Description, false);
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

        /// <summary>
        /// Chekc lock with orderid,if does,fill lock info in _lockInfo
        /// </summary>
        /// <param name="_orderId"></param>
        /// <param name="_lockInfo"></param>
        /// <returns>True: No lock;False:Have lock</returns>
        public virtual bool CheckOrderLock(string _orderId, ref string _lockInfo)
        {
            using (RisDAL okodak = new RisDAL())
            {
                string SQL_GETLOCKS = "Select [tSYnc].* from [tSYnc] inner join [tRegOrder] on [tSYnc].AccNo = [tRegOrder].AccNo where [tRegOrder].OrderGuid ='{0}'";
                DataTable dt = okodak.ExecuteQuery(string.Format(SQL_GETLOCKS, _orderId), RisDAL.ConnectionState.KeepOpen);
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
                string lockUserName = GetUserName(Convert.ToString(dt.Rows[0]["Owner"]), okodak, RisDAL.ConnectionState.KeepOpen);
                _lockInfo = string.Format("lockUser={0}&IP={1}", lockUserName, dt.Rows[0]["OwnerIp"]);
                return true;
            }
        }


        private string GetUserName(string userId, RisDAL okodak, RisDAL.ConnectionState connectionState)
        {
            string SQL_GETUSERNAM = "Select LocalName From [tUSer] with(nolock) where userGuid ='{0}'";
            object result = okodak.ExecuteScalar(string.Format(SQL_GETUSERNAM, userId), connectionState);
            if (result == null)
            {
                throw new Exception(string.Format("There is no user whose id is {0}", userId));
            }
            return Convert.ToString(result);
        }


        private void WriteHippaLog(string action, string actionDetail, string orderGuid, string chargeID, string chargeDescription, bool isSuccess)
        {
            using (RisDAL oKodak = new RisDAL())
            {
                string sql = "select AccNo,PatientID,LocalName from tregorder ro,tRegPatient rp where ro.PatientGuid =rp.PatientGuid and ro.OrderGuid =@OrderGuid";
                oKodak.Parameters.Clear();
                oKodak.Parameters.AddVarChar("OrderGuid", orderGuid);

                DataTable dt = oKodak.ExecuteQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    Server.Utilities.HippaLogTool.HippaLogTool.AuditChargeRecordEvtMsg(
                        action, Convert.ToString(dt.Rows[0]["AccNo"]), Convert.ToString(dt.Rows[0]["PatientID"]), Convert.ToString(dt.Rows[0]["LocalName"]), chargeID, chargeDescription, actionDetail, isSuccess);
                }
            }
        }
        #endregion

        #region QC_QueryNotRefundedOrderCharge, 2015-07-20, Oscar added (US26283)

        /// <summary>
        /// Get the count of charges that haven't been refunded.
        /// </summary>
        /// <param name="patientGuid"></param>
        /// <param name="orderGuid"></param>
        /// <param name="ds"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public virtual bool QueryNotRefundedOrderCharge(string patientGuid, string orderGuid, DataSet ds, ref string error)
        {
            try
            {
                using (var dal = new RisDAL())
                {
                    var orderCondition = "";
                    if (!string.IsNullOrWhiteSpace(orderGuid))
                        orderCondition = string.Format(" AND ro.OrderGuid = '{0}'", orderGuid);

                    var sql = new StringBuilder();
                    sql.AppendFormat("SELECT SourceCount = (SELECT COUNT(1) FROM dbo.tRegPatient rp LEFT JOIN dbo.tRegOrder ro ON ro.PatientGuid = rp.PatientGuid WHERE rp.PatientGuid = '{0}'{1}),", patientGuid, orderCondition);
                    sql.AppendLine();
                    sql.AppendFormat("NotRefundedCount = (SELECT COUNT(1) FROM dbo.tRegOrder ro JOIN dbo.tOrderCharge oc ON oc.OrderGuid = ro.OrderGuid WHERE oc.LastStatus != 21 AND ro.PatientGuid = '{0}'{1})", patientGuid, orderCondition);

                    using (var dt = dal.ExecuteQuery(sql.ToString()))
                    {
                        dt.TableName = "QueryNotRefundedOrderCharge";
                        ds.Tables.Add(dt);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message.Trim();
                var frame = new StackFrame(true);
                logger.Error(
                    (int)ModuleEnum.QualityControl_DA,
                    ModuleInstanceName.QualityControl,
                    0,
                    error,
                    Application.StartupPath,
                    frame.GetFileName(),
                    frame.GetFileLineNumber());
            }
            return false;
        }

        #endregion
    }

    public static class DataTableHelper
    {
        public static string getSafeDateTimeString(this DataRow row, string columnName, string format)
        {
            if (row != null && row.Table.Columns.Contains(columnName))
            {
                if (row[columnName] is DBNull)
                {
                }
                else if (row[columnName] is System.DateTime)
                {
                    DateTime dtime = (DateTime)row[columnName];
                    if (dtime != null)
                        return dtime.ToString(format);
                }
                else
                {
                    DateTime dtime;
                    if (DateTime.TryParse(System.Convert.ToString(row[columnName]), out dtime))
                    {
                        return dtime.ToString(format);
                    }
                }
            }

            return string.Empty;
        }
    }
}
