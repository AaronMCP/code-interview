#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2007                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/*   Author : Andy Bu                                                       */
/*   Created : Monday, March 05, 2007                                                       */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;
using System.Windows.Forms;
using DataAccessLayer;
using LogServer;
using Server.Utilities.LogFacility;
using System.Diagnostics;

namespace Server.DAO.QualityControl.Impl
{
    public class OracleProvider : AbstractQualityImpl
    {
        public override bool QueryPatient(string strPatientName, string strPatientID, string strBeginDt, string strEndDt, bool bIsVIP, DataSet ds, ref string strError)
        {
            System.Diagnostics.Debug.WriteLine("welcome to QueryPatient");
            bool bReturn = true;
            RisDAL oKodak = new RisDAL();
            DataTable dt = new DataTable();
            try
            {
                //this sql is used to get basic information of patient
                //Selected is always false and this column will be used by client for multi selection
                string strSQL = string.Format("SELECT 1 as selected,0 as Target, A.PatientGuid,A.PatientID,A.LocalName,A.Birthday,A.EnglishName,case when a.isvip= 1 then 'Y' else 'N' end as IsVIP,A.Comments,") +
                    "(SELECT Description from tDictionaryValue where tDictionaryValue.DictionaryValue=A.Gender and tDictionaryValue.Tag=1) as Gender," +
                    "A.Address,A.Telephone, A.CreateDt FROM tRegPatient A WHERE  1 =1 ";


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

                DateTime dttmp;
                if (strBeginDt != null && strBeginDt.Trim().Length > 0)
                //query with create date(begin date)
                {
                    dttmp = Convert.ToDateTime(strBeginDt);
                    strSQL += string.Format(" and A.CreateDt >  to_date('{0} 00:00:00','YYYY-MM-DD HH24:MI:SS.') ", dttmp.ToString("yyyy-MM-dd"));
                }

                if (strEndDt != null && strEndDt.Trim().Length > 0)
                //query with create date(begin date)
                {
                    dttmp = Convert.ToDateTime(strEndDt);
                    strSQL += string.Format(" and A.CreateDt <  to_date('{0} 23:59:59','YYYY-MM-DD HH24:MI:SS.') ", dttmp.ToString("yyyy-MM-dd"));
                }

                if (!bIsVIP)
                {
                    strSQL += " AND ";
                    strSQL += " A.IsVIP=0 ";
                }

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
        public override bool QueryLatestVisitWithPatientGUID(string stPatientGUID, DataSet dsVisit)
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
                string strSQL = string.Format("SELECT tRegVisit.*,rownum from tRegPatient,tRegVisit WHERE rownum<=1 and tRegPatient.PatientGuid ");
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
        public override bool UpdatePatient(bool bSendToGateWay, string strPatientID, DataSet ds, ref string strError)
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


                strBuilder.AppendFormat("UPDATE tRegPatient set LocalName=N'{0}',EnglishName='{1}',Birthday=to_date('{2}','yyyy-MM-dd'),Gender='{3}',Address='{4}',Telephone='{5}',Comments='{7}' where PatientGuid='{8}'",
                drPatient["LocalName"].ToString(), drPatient["EnglishName"].ToString(), ((DateTime)drPatient["Birthday"]).ToString("yyyy-MM-dd"),
                drPatient["Gender"].ToString(), drPatient["Address"].ToString(), drPatient["Telephone"].ToString(),  /*(int)drPatient["IsVIP"],*/"", drPatient["Comments"].ToString(), drPatient["PatientGuid"].ToString());
                listSQL.Add(strBuilder.ToString());

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
        /// Create sql clause
        /// </summary>
        /// <param name="stCol"></param>
        /// <param name="stVal"></param>
        /// <param name="stColList"></param>
        /// <param name="stValList"></param>
        private void AddDateColForInsert(string stCol, string stVal, ref string stColList, ref string stValList)
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
                stValList = "'" + stVal + "'";
            }
            else
            {
                stValList = stValList + "," + "to_date('" + stVal + "','YYYY-MM-DD HH24:MI:SS.')";
            }

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

        public override bool UpdatePatientSendToGateServer(DataTable dtPatient, List<string> listSQL)
        {
            Debug.WriteLine("UpdatePatientSendToGateServer...");
            RisDAL oKodak = new RisDAL();
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
                            .AppendFormat("VALUES('{0}',to_date('{1}','YYYY-MM-DD HH24:MI:SS.'),'01','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    listSQL.Add(strBuilder.ToString());
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    //Patient                                    
                    {
                        string stCols = "";
                        string stVals = "";
                        strBuilder.Remove(0, strBuilder.Length);
                        AddColForInsert("PATIENTID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_NAME", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCAL_NAME", drPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                        StringBuilder sbBirthdate = new StringBuilder();
                        sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]));
                        AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SEX", drPatient["Gender"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("ADDRESS", drPatient["Address"].ToString(), ref stCols, ref  stVals);
                        int iVIP = string.Compare(drPatient["IsVIP"].ToString(), "N", true) == 0 ? 0 : 1;
                        AddColForInsert("CUSTOMER_2", iVIP.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PHONENUMBER_HOME", drPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_TYPE", drOrder["PatientType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCATION", drOrder["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("VISIT_NUMBER", drOrder["VisitGuid"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BED_NUMBER", drOrder["BedNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddDateColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
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
        public override bool UpdateOrderSendToGateServer(DataTable dtOrder, List<string> listSQL)
        {
            Debug.WriteLine("UpdateOrderSendToGateServer...");
            RisDAL oKodak = new RisDAL();
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
                                if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                                {
                                    continue;
                                }
                                nEventType = 11;
                                nExamStatus = 11;
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
                                    .AppendFormat("VALUES('{0}',to_date('{1}','YYYY-MM-DD HH24:MI:SS.'),'{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), nEventType);
                            listSQL.Add(strBuilder.ToString());

                            logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                            ///////////////////////////// 4, insert patient
                            //Patient                                    
                            {
                                string stCols = "";
                                string stVals = "";
                                strBuilder.Remove(0, strBuilder.Length);
                                AddColForInsert("PATIENTID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_NAME", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_LOCAL_NAME", drPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                                StringBuilder sbBirthdate = new StringBuilder();
                                sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]));
                                AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                                AddColForInsert("SEX", drPatient["Gender"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("ADDRESS", drPatient["Address"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PHONENUMBER_HOME", drPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_TYPE", drOrder["PatientType"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PATIENT_LOCATION", drOrder["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("VISIT_NUMBER", drOrder["VisitGuid"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("BED_NUMBER", drOrder["BedNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                                AddDateColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
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
                                AddDateColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                                AddColForInsert("ORDER_NO", drRP["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PLACER_NO", drRP["RemoteRPID"].ToString(), ref stCols, ref  stVals);
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
                                //AddColForInsert("CHARGE_STATUS", drRP["IsCharge"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CHARGE_AMOUNT", drRP["Charge"].ToString(), ref stCols, ref  stVals);

                                //AddColForInsert("FILLER", drRP["RegGUID"].ToString(), ref stCols,ref  stVals);
                                AddColForInsert("REF_PHYSICIAN", drRP["DoctorGUID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("TECHNICIAN", drRP["TechGUID"].ToString(), ref stCols, ref  stVals);

                                AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);
                                AddColForInsert("FILLER_DEPARTMENT", "", ref stCols, ref  stVals);
                                AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                                AddColForInsert("FILLER_CONTACT", "", ref stCols, ref  stVals);
                                AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                                AddColForInsert("REQUEST_REASON", "", ref stCols, ref  stVals);
                                AddColForInsert("REUQEST_COMMENTS", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                                AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_LOCATION", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                                AddDateColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                                AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                                AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                                AddColForInsert("STUDY_INSTANCE_UID", "", ref stCols, ref  stVals);
                                AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                                AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_COMMENT", "", ref stCols, ref  stVals);
                                AddColForInsert("CNT_AGENT", "", ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_1", drOrder["CardNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_2", drOrder["HisID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_3", drOrder["MedicareNo"].ToString(), ref stCols, ref  stVals);

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
        /// Implementition for Update Order interface
        /// </summary>
        /// <param name="bSendToGateWay"></param>
        /// <param name="strOrderGuid"></param>
        /// <param name="ds"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public override bool UpdateOrder(bool bSendToGateWay, string strOrderGuid, DataSet ds, ref string strError)
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
                DataRow drVisit = dtOrder.Rows[0];
                strBuilder.Remove(0, strBuilder.Length);
                strBuilder.AppendFormat("UPDATE tRegVisit SET InhospitalNo='{0}',ClinicNo='{1}',PatientType='{2}',Observation='{3}',HealthHistory='{4}',InhospitalRegion='{5}',IsEmergency={6},BedNo='{7}',Comments='{8}' Where VisitGuid='{9}'",
                    drVisit["InhospitalNo"].ToString(), drVisit["ClinicNo"].ToString(), drVisit["PatientTypeVal"].ToString(), Convert.ToString(drVisit["Observation"]),
                    Convert.ToString(drVisit["HealthHistory"]), drVisit["InhospRegionVal"].ToString(), Convert.ToInt32(drVisit["IsEmergency"]), drVisit["BedNo"].ToString(),
                    Convert.ToString(drVisit["Comments"]), drVisit["VisitGuid"].ToString());
                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                        strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                listSQL.Add(strBuilder.ToString());

                //Order                
                DataRow drOrder = drVisit;
                strBuilder.Remove(0, strBuilder.Length);
                strBuilder.AppendFormat("UPDATE tRegOrder SET ApplyDept='{0}',ApplyDoctor='{1}',Comments='{2}' WHERE OrderGuid='{3}'",
                    drOrder["ApplyDeptVal"].ToString(), drOrder["ApplyDoctVal"].ToString(), drOrder["Comments"].ToString(), drOrder["OrderGuid"].ToString());
                logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0,
                        strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                listSQL.Add(strBuilder.ToString());

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
        /// Send the information about updating registered procedure(Order in Gateway) to Gateway Server
        /// </summary>
        /// <param name="dtRP">
        /// Contains all procedure(In gateway, it is order)
        /// </param>
        /// <param name="listSQL">
        /// Return list of SQL string used in this feature
        /// </param>
        /// <returns></returns>
        public override bool UpdateRPSendToGateServer(DataTable dtRP, List<string> listSQL)
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
                    if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                    {
                        return true;
                    }
                    nEventType = 11;//Update RP
                    nExamStatus = 11;
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
                            .AppendFormat("VALUES('{0}',to_date('{1}','YYYY-MM-DD HH24:MI:SS.'),'{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), nEventType);

                    listSQL.Add(strBuilder.ToString());

                    //Patient

                    string stCols = "";
                    string stVals = "";
                    strBuilder.Remove(0, strBuilder.Length);
                    AddColForInsert("PATIENTID", drRegInfo["PatientID"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_NAME", drRegInfo["EnglishName"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_LOCAL_NAME", drRegInfo["LocalName"].ToString(), ref stCols, ref  stVals);
                    StringBuilder sbBirthdate = new StringBuilder();
                    sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drRegInfo["Birthday"]), ((DateTime)drRegInfo["Birthday"]), ((DateTime)drRegInfo["Birthday"]));
                    AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                    AddColForInsert("SEX", drRegInfo["Gender"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("ADDRESS", drRegInfo["Address"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PHONENUMBER_HOME", drRegInfo["Telephone"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_TYPE", drRegInfo["PatientType"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_LOCATION", drRegInfo["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("VISIT_NUMBER", drRegInfo["VisitGuid"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("BED_NUMBER", drRegInfo["BedNo"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                    AddDateColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                    strBuilder.AppendFormat("INSERT INTO GW_PATIENT({0}) Values ({1})", stCols, stVals);
                    listSQL.Add(strBuilder.ToString());
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());


                    //order
                    strBuilder.Remove(0, strBuilder.Length);
                    stCols = "";
                    stVals = "";
                    AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                    AddDateColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                    AddColForInsert("ORDER_NO", drRegInfo["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PLACER_NO", drRegInfo["RemoteRPID"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("FILLER_NO", drRegInfo["AccNo"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PATIENT_ID", drRegInfo["PatientID"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("EXAM_STATUS", nExamStatus.ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PLACER_DEPARTMENT", drRegInfo["ApplyDept"].ToString(), ref stCols, ref  stVals);

                    StringBuilder sbScheduleddt = new StringBuilder();
                    sbScheduleddt.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drRP["RegisterDt"]), ((DateTime)drRP["RegisterDt"]), ((DateTime)drRP["RegisterDt"]));
                    AddDateColForInsert("SCHEDULED_DT", sbScheduleddt.ToString(), ref stCols, ref  stVals);

                    AddColForInsert("PLACER", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("MODALITY", drRegInfo["ModalityType"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("STATION_NAME", drRegInfo["Modality"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("BODY_PART", drRegInfo["BodyPart"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PROCEDURE_NAME", "", ref stCols, ref  stVals);
                    AddColForInsert("PROCEDURE_CODE", drRegInfo["ProcedureCode"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("PROCEDURE_DESC", drRegInfo["Description"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("CHARGE_STATUS", drRegInfo["IsCharge"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("CHARGE_AMOUNT", drRP["Charge"].ToString(), ref stCols, ref  stVals);

                    //AddColForInsert("FILLER", drRP["RegGUID"].ToString(), ref stCols,ref  stVals);
                    AddColForInsert("REF_PHYSICIAN", drRP["techdoctor"].ToString(), ref stCols, ref  stVals);
                    AddColForInsert("TECHNICIAN", drRP["technician"].ToString(), ref stCols, ref  stVals);

                    AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);
                    AddColForInsert("FILLER_DEPARTMENT", "", ref stCols, ref  stVals);
                    AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                    AddColForInsert("FILLER_CONTACT", "", ref stCols, ref  stVals);
                    AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                    AddColForInsert("REQUEST_REASON", "", ref stCols, ref  stVals);
                    AddColForInsert("REUQEST_COMMENTS", "", ref stCols, ref  stVals);
                    AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                    AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                    AddColForInsert("EXAM_LOCATION", "", ref stCols, ref  stVals);
                    AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                    AddDateColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                    AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                    AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                    AddColForInsert("STUDY_INSTANCE_UID", "", ref stCols, ref  stVals);
                    AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                    AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                    AddColForInsert("EXAM_COMMENT", "", ref stCols, ref  stVals);
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
        public override bool DeleteOrderSendToGateServer(string stOrderGUID, string stVisitGUID, List<string> listSQL, string strRPGuid,string strLoginName,string strLocalName)
        {
            Debug.WriteLine("DeleteOrderSendToGateServer...");
            RisDAL oKodak = new RisDAL();
            bool bRet = false;

            try
            {
                //
                StringBuilder strBuilder = new StringBuilder();

                //get patient information with visitID
                DataSet dsPatient = new DataSet();
                DataRow drPatient = null;
                DataTable dtPatient = null;
                //query patient information for gateway
                if (QueryPatientWithVisitGUID(stVisitGUID, dsPatient))
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
                string stError = "";
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
                if (QueryRP(stOrderGUID, ref dtRP, ref strError,ref strRPGuid))
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
                                if (Convert.ToInt32(drRP["Status"]) == 10)
                                //Only send the RP that has checked in
                                {
                                    continue;
                                }
                                nEventType = 13;
                                nExamStatus = 13;
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
                                    .AppendFormat("VALUES('{0}',to_date('{1}','YYYY-MM-DD HH24:MI:SS.'),'{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), nEventType);
                            listSQL.Add(strBuilder.ToString());

                            logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                                (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());
                            ///////////////////////////// 4, insert patient

                            //Patient

                            string stCols = "";
                            string stVals = "";
                            strBuilder.Remove(0, strBuilder.Length);
                            AddColForInsert("PATIENTID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("PATIENT_NAME", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("PATIENT_LOCAL_NAME", drPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                            StringBuilder sbBirthdate = new StringBuilder();
                            sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]));
                            AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                            AddColForInsert("SEX", drPatient["Gender"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("ADDRESS", drPatient["Address"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("PHONENUMBER_HOME", drPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("PATIENT_TYPE", drOrder["PatientType"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("PATIENT_LOCATION", drOrder["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("VISIT_NUMBER", drOrder["VisitGuid"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("BED_NUMBER", drOrder["BedNo"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("CUSTOMER_2", drPatient["IsVIP"].ToString(), ref stCols, ref  stVals);
                            AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                            AddDateColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
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
                                AddDateColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                                AddColForInsert("ORDER_NO", drRP["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("PLACER_NO", drRP["RemoteRPID"].ToString(), ref stCols, ref  stVals);
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
                                //AddColForInsert("CHARGE_STATUS", drRP["IsCharge"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CHARGE_AMOUNT", drRP["Charge"].ToString(), ref stCols, ref  stVals);

                                //AddColForInsert("FILLER", drRP["RegGUID"].ToString(), ref stCols,ref  stVals);
                                AddColForInsert("REF_PHYSICIAN", drRP["DoctorGUID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("TECHNICIAN", drRP["TechGUID"].ToString(), ref stCols, ref  stVals);
                                StringBuilder sbScheduleddt = new StringBuilder();
                                sbScheduleddt.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drRP["RegisterDt"]), ((DateTime)drRP["RegisterDt"]), ((DateTime)drRP["RegisterDt"]));
                                AddDateColForInsert("SCHEDULED_DT", sbScheduleddt.ToString(), ref stCols, ref  stVals);

                                AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);
                                AddColForInsert("FILLER_DEPARTMENT", "", ref stCols, ref  stVals);
                                AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                                AddColForInsert("FILLER_CONTACT", "", ref stCols, ref  stVals);
                                AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                                AddColForInsert("REQUEST_REASON", "", ref stCols, ref  stVals);
                                AddColForInsert("REUQEST_COMMENTS", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                                AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_LOCATION", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                                AddDateColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                                AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                                AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                                AddColForInsert("STUDY_INSTANCE_UID", "", ref stCols, ref  stVals);
                                AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                                AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                                AddColForInsert("EXAM_COMMENT", "", ref stCols, ref  stVals);
                                AddColForInsert("CNT_AGENT", "", ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_1", drOrder["CardNo"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_2", drOrder["HisID"].ToString(), ref stCols, ref  stVals);
                                AddColForInsert("CUSTOMER_3", drOrder["MedicareNo"].ToString(), ref stCols, ref  stVals);

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
        /// Send the information about updating patient to Gateway Server
        /// </summary>
        /// <param name="dtOrder">
        /// Contains all information of this patient
        /// </param>
        /// <param name="listSQL">
        /// Return list of SQL string used in this feature
        /// </param>

        public override bool DeletePatientSendToGateServer(string stPatientGUID, List<string> listSQL)
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
                            .AppendFormat("VALUES('{0}',to_date('{1}','YYYY-MM-DD HH24:MI:SS.'),'03','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    listSQL.Add(strBuilder.ToString());
                    logger.Info((long)ModuleEnum.QualityControl_DA, ModuleInstanceName.QualityControl, 0, strBuilder.ToString(), Application.StartupPath.ToString(),
                        (new System.Diagnostics.StackFrame(true)).GetFileName(), (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

                    //Patient                                    
                    {
                        string stCols = "";
                        string stVals = "";
                        strBuilder.Remove(0, strBuilder.Length);
                        AddColForInsert("PATIENTID", drPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_NAME", drPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCAL_NAME", drPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                        StringBuilder sbBirthdate = new StringBuilder();
                        sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]), ((DateTime)drPatient["Birthday"]));
                        AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SEX", drPatient["Gender"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("ADDRESS", drPatient["Address"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PHONENUMBER_HOME", drPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("PATIENT_TYPE", drOrder["PatientType"].ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("PATIENT_LOCATION", drOrder["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("VISIT_NUMBER", drOrder["VisitGuid"].ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("BED_NUMBER", drOrder["BedNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddDateColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
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
        /// Send the information about patient merging to Gateway Server
        /// </summary>
        /// <param name="dtGWOrders">
        /// Contains information of all gateway order affected by this operation
        /// </param>
        /// <param name="dtPatient">
        /// information of the target patient merged to
        /// </param>/// 
        /// <param name="listSQL">
        /// Return list of SQL string used in this feature
        /// </param>
        /// <returns></returns>
        public override bool MergePatientSendToGateServer(DataTable dtGWOrders, DataTable dtTargetPatient, List<string> listSQL, bool bMergeOneOrder)
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
                            if (bMergeOneOrder == true)
                            {
                                //request by integration group. Merge one order should use '11'
                                stEventType = "11";//Merge RP
                            }
                            else
                            {
                                stEventType = "02";//Merge patient
                            }
                            if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                            {
                                continue;
                            }
                            nExamStatus = 12;
                        }
                        else//From Electronic requisition
                        {
                            if (bMergeOneOrder == true)
                            {
                                //request by integration group. Merge one order should use '11'
                                if (Convert.ToInt32(drRP["Status"]) == 10)//Only send the RP that has checked in
                                {
                                    stEventType = "21";//Merge RP
                                    nExamStatus = 102;
                                }
                                else
                                {
                                    stEventType = "11";//Merge RP
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
                                .AppendFormat("VALUES('{0}',to_date('{1}','YYYY-MM-DD HH24:MI:SS.'),'{2}','','Local')", strGWGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), stEventType);
                        listSQL.Add(strBuilder.ToString());

                        ////////////////////////////4, Insert Patient

                        string stCols = "";
                        string stVals = "";
                        strBuilder.Remove(0, strBuilder.Length);
                        AddColForInsert("PATIENTID", drTargetPatient["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_NAME", drTargetPatient["EnglishName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCAL_NAME", drTargetPatient["LocalName"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PRIOR_PATIENT_ID", drRP["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PRIOR_PATIENT_NAME", drRP["LocalName"].ToString(), ref stCols, ref  stVals);
                        StringBuilder sbBirthdate = new StringBuilder();
                        sbBirthdate.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drTargetPatient["Birthday"]), ((DateTime)drTargetPatient["Birthday"]), ((DateTime)drTargetPatient["Birthday"]));
                        AddColForInsert("BIRTHDATE", sbBirthdate.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("SEX", drTargetPatient["Gender"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("ADDRESS", drTargetPatient["Address"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_2", drTargetPatient["IsVIP"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PHONENUMBER_HOME", drTargetPatient["Telephone"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_TYPE", drRegInfo["PatientType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_LOCATION", drRegInfo["InhospitalRegion"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("VISIT_NUMBER", drRP["VisitGuid"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BED_NUMBER", drRegInfo["BedNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("DATA_ID", strGWGuid, ref stCols, ref  stVals);
                        AddDateColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);


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
                        AddDateColForInsert("DATA_DT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ref stCols, ref  stVals);
                        AddColForInsert("ORDER_NO", drRP["ProcedureGuid"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER_NO", drRP["RemoteRPID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("FILLER_NO", drRegInfo["AccNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PATIENT_ID", drRegInfo["PatientID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("EXAM_STATUS", nExamStatus.ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER_DEPARTMENT", drRegInfo["ApplyDept"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PLACER", drRegInfo["ApplyDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("MODALITY", drRP["ModalityType"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("STATION_NAME", drRP["Modality"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("BODY_PART", drRegInfo["BodyPart"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_NAME", "", ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_CODE", drRP["ProcedureCode"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("PROCEDURE_DESC", drRegInfo["Description"].ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("CHARGE_STATUS", drRP["IsCharge"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CHARGE_AMOUNT", drRegInfo["Charge"].ToString(), ref stCols, ref  stVals);
                        StringBuilder sbScheduleddt = new StringBuilder();
                        sbScheduleddt.AppendFormat("{0:yyyy}-{1:MM}-{2:dd}", ((DateTime)drRP["RegisterDt"]), ((DateTime)drRP["RegisterDt"]), ((DateTime)drRP["RegisterDt"]));
                        AddDateColForInsert("SCHEDULED_DT", sbScheduleddt.ToString(), ref stCols, ref  stVals);
                        //AddColForInsert("FILLER", drRP["RegGUID"].ToString(), ref stCols,ref  stVals);
                        AddColForInsert("REF_PHYSICIAN", drRegInfo["TechDoctor"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("TECHNICIAN", drRegInfo["Technician"].ToString(), ref stCols, ref  stVals);

                        AddColForInsert("PLACER_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("FILLER_DEPARTMENT", "", ref stCols, ref  stVals);
                        AddColForInsert("REF_ORGANIZATION", "", ref stCols, ref  stVals);
                        AddColForInsert("FILLER_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("REF_CONTACT", "", ref stCols, ref  stVals);
                        AddColForInsert("REQUEST_REASON", "", ref stCols, ref  stVals);
                        AddColForInsert("REUQEST_COMMENTS", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_REQUIREMENT", "", ref stCols, ref  stVals);
                        AddColForInsert("STATION_AETITLE", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_LOCATION", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_VOLUME", "", ref stCols, ref  stVals);
                        AddDateColForInsert("EXAM_DT", "", ref stCols, ref  stVals);
                        AddColForInsert("DURATION", "", ref stCols, ref  stVals);
                        AddColForInsert("TRANSPORT_ARRANGE", "", ref stCols, ref  stVals);
                        AddColForInsert("STUDY_INSTANCE_UID", "", ref stCols, ref  stVals);
                        AddColForInsert("STUDY_ID", "", ref stCols, ref  stVals);
                        AddColForInsert("REF_CLASS_UID", "", ref stCols, ref  stVals);
                        AddColForInsert("EXAM_COMMENT", "", ref stCols, ref  stVals);
                        AddColForInsert("CNT_AGENT", "", ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_1", drRegInfo["CardNo"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_2", drRegInfo["HisID"].ToString(), ref stCols, ref  stVals);
                        AddColForInsert("CUSTOMER_3", drRegInfo["MedicareNo"].ToString(), ref stCols, ref  stVals);

                        strBuilder.AppendFormat("INSERT INTO GW_ORDER({0}) Values ({1})", stCols, stVals);

                        listSQL.Add(strBuilder.ToString());
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
    }
}
