using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.Report;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    /// <summary>
    /// for report
    /// </summary>
    public class ReportUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string LocalizeCurrentAge(string src, IEnumerable<DictionaryDto> dictionaries)
        {
            if (src == null)
                return "";

            try
            {
                string strTemp = src;
                string[] split = strTemp.Split(new Char[] { ' ' });
                if (split.Length < 2)
                {
                    throw new Exception("Invalid current age");
                }

                //src = split[0] + GetLanguage(split[1]);
                //src = split[0] + DictionaryManager.Instance.GetText((int)DictionaryTag.AgeUnit, split[1]);
                src = split[0] + ReportUtils.GetDictionaryText(dictionaries, (int)DictionaryTag.AgeUnit, split[1]); ;
            }
            catch (Exception ex)
            {
                //  logger.Error((long)ModuleEnum.Register_Client, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),

                //   (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }

            return src;
        }

        public static bool isNeedLocalizationAsUserName(string fieldName)
        {
            fieldName = fieldName.ToUpper();

            if (fieldName.Equals("PREVIOUSOWNER") || fieldName.Equals("CURRENTOWNER"))
            {
                return false;
            }

            return fieldName.EndsWith("ER")
                || fieldName.EndsWith("OR")
                || fieldName.EndsWith("CIAN")
                || fieldName.EndsWith("NURSE")
                || fieldName.EndsWith("REJECTTOOBJECT")
                || fieldName.EndsWith("REGISTRAR");
        }

        public static string GetStringFromDataTable(DataTable dt, string colName)
        {
            //
            //may be multi-rows
            if (dt == null || dt.Rows.Count < 1)
            {
                return "";
            }

            if (!dt.Columns.Contains(colName))
            {
                return "";
            }

            string ret = "";

            System.Collections.Specialized.StringCollection strCol = new System.Collections.Specialized.StringCollection();
            foreach (DataRow dr in dt.Rows)
            {
                string tmp = dr[colName].ToString();

                if (isNeedLocalizationAsUserName(colName))
                {
                    //tmp = DictionaryManager.Instance.GetUserLocalName(tmp);
                }

                if (!strCol.Contains(tmp))
                {
                    strCol.Add(tmp);

                    ret += tmp + ",";
                }
            }

            ret = ret.Trim(", ".ToCharArray());

            return ret;
        }

        public static string GetStringFromBytes(object buff)
        {
            try
            {
                if (buff != null && !(buff is DBNull))
                    return System.Text.Encoding.Default.GetString(buff as byte[]);
            }
            catch (Exception)
            {
            }

            return "";
        }

        public static string GetUnicodeStringFromBytes(object buff)
        {
            try
            {
                if (buff != null && !(buff is DBNull))
                    return System.Text.Encoding.Unicode.GetString(buff as byte[]);
            }
            catch (Exception)
            {
            }

            return "";
        }

        public static void DeleteOutdatedFolder(string rootPath)
        {
            try
            {
                string checkFolder = System.DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(rootPath);

                DirectoryInfo[] subs = dir.GetDirectories();

                for (int i = subs.Length - 1; i >= 0; --i)
                {
                    try
                    {
                        if (string.Compare(subs[i].Name, checkFolder) < 0)
                            subs[i].Delete(true);
                    }
                    catch (System.Exception ex)
                    {
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        public static string StringRight(string src, int count)
        {
            return src.Length >= count ? src.Substring(src.Length - count) : src;
        }

        public static string GetDictionaryText(IEnumerable<DictionaryDto> dictionaries, int tag, string value)
        {
            string ret = "";
            DictionaryDto dictionaryDto = dictionaries.Where(d => d.Tag == tag).FirstOrDefault();
            if (dictionaryDto != null)
            {
                DictionaryValueDto dictionaryValueDto = dictionaryDto.Values.Where(d => d.Value.Equals(value, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (dictionaryValueDto != null)
                {
                    ret = dictionaryValueDto.Text;
                }
            }
            return ret;
        }

        public static string GetFirstRowValueFromDataSet(DataSet ds, string colName)
        {
            // ignoring multi rows
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return "";
            }

            if (!ds.Tables[0].Columns.Contains(colName))
            {
                return "";
            }

            string ret = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ret = dr[colName].ToString();

                break;
            }

            return ret;
        }

        public static object GetBytesFromBase64(DataTable dt, string colName)
        {

            if (dt == null || dt.Rows.Count < 1 || !dt.Columns.Contains(colName) ||
                colName == null || colName.Length < 1)
            {
                System.Diagnostics.Debug.Assert(false);

                return DBNull.Value;
            }

            string key = colName.ToUpper().Trim();
            try
            {

                string objValue = Convert.ToString(dt.Rows[0][key]);

                return Convert.FromBase64String(objValue);
            }
            catch
            {
                return dt.Rows[0][key];
            }
        }

        public static string GetFieldValue(DataTable dt, int iRowIndex, int iColIndex)
        {
            string sRet = "";
            try
            {
                sRet = dt.Rows[iRowIndex][iColIndex].ToString();
            }
            catch (Exception)
            {

            }
            return sRet;
        }

        public static DataTable CreateDTReportTemplate()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PATIENTEXAMNO");

            //new
            //order
            dt.Columns.Add("tbRegOrder__OrderGuid");
            dt.Columns.Add("tbRegOrder__ACCNO");
            dt.Columns.Add("tbRegOrder__ApplyDept");
            dt.Columns.Add("tbRegOrder__ApplyDoctor");
            dt.Columns.Add("tbRegOrder__IsScan");
            dt.Columns.Add("tbRegOrder__Comments");
            dt.Columns.Add("tbRegOrder__RemoteAccNo");
            dt.Columns.Add("tbRegOrder__TotalFee");
            dt.Columns.Add("tbRegOrder__InitialDomain");
            dt.Columns.Add("tbRegOrder__HisID");
            dt.Columns.Add("tbRegOrder__CardNo");
            dt.Columns.Add("tbRegOrder__ReferralId");
            dt.Columns.Add("tbRegOrder__IsReferral");
            dt.Columns.Add("tbRegOrder__VisitGuid");
            dt.Columns.Add("tbRegOrder__InhospitalNo");
            dt.Columns.Add("tbRegOrder__ClinicNo");
            dt.Columns.Add("tbRegOrder__PatientType");
            dt.Columns.Add("tbRegOrder__Observation");
            dt.Columns.Add("tbRegOrder__Inspection");
            dt.Columns.Add("tbRegOrder__HealthHistory");
            dt.Columns.Add("tbRegOrder__InhospitalRegion");
            dt.Columns.Add("tbRegOrder__BedNo");
            dt.Columns.Add("tbRegOrder__BedSide");
            dt.Columns.Add("tbRegOrder__CreateDt");
            dt.Columns.Add("tbRegOrder__CurrentAge");
            dt.Columns.Add("tbRegOrder__VisitComment");
            dt.Columns.Add("tbRegOrder__ChargeType");
            dt.Columns.Add("tbRegOrder__curPatientName");
            dt.Columns.Add("tbRegOrder__BodyWeight");
            dt.Columns.Add("tbRegOrder__curGender");
            dt.Columns.Add("tbRegOrder__IsCharge");
            dt.Columns.Add("tbRegOrder__AgeInDays");
            dt.Columns.Add("tbRegOrder__BookingSite");
            dt.Columns.Add("tbRegOrder__ExamSite");
            dt.Columns.Add("tbRegOrder__RegSite");
            dt.Columns.Add("tbRegOrder__ThreeDRebuild");
            dt.Columns.Add("TBREGORDER__ORDERMESSAGE");
            dt.Columns.Add("TBREGORDER__PathologicalFindings");
            dt.Columns.Add("TBREGORDER__InternalOptional1");
            dt.Columns.Add("TBREGORDER__InternalOptional2");
            dt.Columns.Add("TBREGORDER__ExternalOptional1");
            dt.Columns.Add("TBREGORDER__ExternalOptional2");
            dt.Columns.Add("TBREGORDER__ExternalOptional3");
            dt.Columns.Add("tbRegOrder__InjectDose");
            dt.Columns.Add("tbRegOrder__InjectTime");
            dt.Columns.Add("tbRegOrder__BodyHeight");
            dt.Columns.Add("tbRegOrder__BloodSugar");
            dt.Columns.Add("tbRegOrder__Insulin");
            dt.Columns.Add("tbRegOrder__GoOnGoTime");
            dt.Columns.Add("tbRegOrder__InjectorRemnant");
            dt.Columns.Add("tbRegOrder__SubmitHospital");
            dt.Columns.Add("tbRegOrder__SubmitDept");
            dt.Columns.Add("tbRegOrder__SubmitDoctor");
            dt.Columns.Add("TBREGORDER__Optional1");
            dt.Columns.Add("TBREGORDER__Optional2");
            dt.Columns.Add("TBREGORDER__Optional3");
            dt.Columns.Add("tbRegOrder__Assign2site");
            dt.Columns.Add("tbRegOrder__CurrentSite");


            //patient
            dt.Columns.Add("tbRegPatient__PatientID");
            dt.Columns.Add("tbRegPatient__PatientGUID");
            dt.Columns.Add("tbRegPatient__MedicareNo");
            dt.Columns.Add("tbRegPatient__GlobalID");
            dt.Columns.Add("tbRegPatient__LocalName");
            dt.Columns.Add("tbRegPatient__EnglishName");
            dt.Columns.Add("tbRegPatient__ReferenceNo");
            dt.Columns.Add("tbRegPatient__Birthday");
            dt.Columns.Add("tbRegPatient__Gender");
            dt.Columns.Add("tbRegPatient__Address");
            dt.Columns.Add("tbRegPatient__Telephone");
            dt.Columns.Add("tbRegPatient__IsVIP");
            dt.Columns.Add("tbRegPatient__Comments");
            dt.Columns.Add("tbRegPatient__RemotePID");
            dt.Columns.Add("tbRegPatient__CreateDt");
            dt.Columns.Add("tbRegPatient__Alias");
            dt.Columns.Add("tbRegPatient__ParentName");
            dt.Columns.Add("tbRegPatient__Site");
            dt.Columns.Add("tbRegPatient__SocialSecurityNo");

            //procedure
            dt.Columns.Add("tbRegProcedure__ProcedureGuid");
            dt.Columns.Add("tbRegProcedure__ProcedureCode");
            dt.Columns.Add("tbRegProcedure__ExamSystem");
            dt.Columns.Add("tbRegProcedure__WarningTime");
            dt.Columns.Add("tbRegProcedure__FilmSpec");
            dt.Columns.Add("tbRegProcedure__FilmCount");
            dt.Columns.Add("tbRegProcedure__ContrastName");
            dt.Columns.Add("tbRegProcedure__ContrastDose");
            dt.Columns.Add("tbRegProcedure__ImageCount");
            dt.Columns.Add("tbRegProcedure__ExposalCount");
            dt.Columns.Add("tbRegProcedure__Deposit");
            dt.Columns.Add("tbRegProcedure__Charge");
            dt.Columns.Add("tbRegProcedure__ModalityType");
            dt.Columns.Add("tbRegProcedure__Modality");
            dt.Columns.Add("tbRegProcedure__Registrar");
            dt.Columns.Add("tbRegProcedure__RegisterDt");
            dt.Columns.Add("tbRegProcedure__Priority");
            dt.Columns.Add("tbRegProcedure__Technician");
            dt.Columns.Add("tbRegProcedure__TechDoctor");
            dt.Columns.Add("tbRegProcedure__TechNurse");
            dt.Columns.Add("tbRegProcedure__OperationStep");
            dt.Columns.Add("tbRegProcedure__ExamineDt");
            dt.Columns.Add("tbRegProcedure__Mender");
            dt.Columns.Add("tbRegProcedure__ModifyDt");
            dt.Columns.Add("tbRegProcedure__IsExistImage");
            dt.Columns.Add("tbRegProcedure__Status");
            dt.Columns.Add("tbRegProcedure__Comments");
            dt.Columns.Add("tbRegProcedure__IsCharge");
            dt.Columns.Add("tbRegProcedure__RemoteRPID");
            dt.Columns.Add("tbRegProcedure__QueueNo");
            dt.Columns.Add("tbRegProcedure__CreateDt");
            dt.Columns.Add("tbRegProcedure__Posture");
            dt.Columns.Add("tbRegProcedure__MedicineUsage");
            dt.Columns.Add("tbRegProcedure__Booker");
            dt.Columns.Add("tbRegProcedure__BookingBeginDt");
            dt.Columns.Add("tbRegProcedure__BookingEndDt");
            dt.Columns.Add("tbRegProcedure__BookerName");
            dt.Columns.Add("tbRegProcedure__RegistrarName");
            dt.Columns.Add("tbRegProcedure__TechnicianName");
            dt.Columns.Add("tbRegProcedure__UnwrittenCurrentOwner");
            dt.Columns.Add("tbRegProcedure__UnapprovedCurrentOwner");
            dt.Columns.Add("tbRegProcedure__RPDesc");
            dt.Columns.Add("tbRegProcedure__BodyPart");
            dt.Columns.Add("tbRegProcedure__CheckingItem");
            dt.Columns.Add("TBREGPROCEDURE__UNAPPROVEDASSIGNDATE");
            dt.Columns.Add("TBREGPROCEDURE__UNAPPROVEDPREVIOUSOWNER");
            dt.Columns.Add("TBREGPROCEDURE__UNWRITTENASSIGNDATE");
            dt.Columns.Add("TBREGPROCEDURE__UNWRITTENPREVIOUSOWNER");

            //report
            dt.Columns.Add("tbReport__ReportGuid");
            dt.Columns.Add("tbReport__WYSTEXT");
            dt.Columns.Add("tbReport__WYGTEXT");
            dt.Columns.Add("tbReport__ReportName");
            dt.Columns.Add("tbReport__WYS");
            dt.Columns.Add("tbReport__WYG");
            dt.Columns.Add("tbReport__AppendInfo");
            dt.Columns.Add("tbReport__TechInfo");
            dt.Columns.Add("tbReport__ReportText");
            dt.Columns.Add("tbReport__DoctorAdvice");
            dt.Columns.Add("tbReport__IsPositive");
            dt.Columns.Add("tbReport__AcrCode");
            dt.Columns.Add("tbReport__AcrAnatomic");
            dt.Columns.Add("tbReport__AcrPathologic");
            dt.Columns.Add("tbReport__Creater");
            dt.Columns.Add("tbReport__CreateDt");
            dt.Columns.Add("tbReport__Submitter");
            dt.Columns.Add("tbReport__SubmitDt");
            dt.Columns.Add("tbReport__FirstApprover");
            dt.Columns.Add("tbReport__FirstApproveDt");
            dt.Columns.Add("tbReport__SecondApprover");
            dt.Columns.Add("tbReport__SecondApproveDt");
            dt.Columns.Add("tbReport__IsDiagnosisRight");
            dt.Columns.Add("tbReport__KeyWord");
            dt.Columns.Add("tbReport__ReportQuality");
            dt.Columns.Add("tbReport__RejectToObject");
            dt.Columns.Add("tbReport__Rejecter");
            dt.Columns.Add("tbReport__RejectDt");
            dt.Columns.Add("tbReport__Status");
            dt.Columns.Add("tbReport__Comments");
            dt.Columns.Add("tbReport__DeleteMark");
            dt.Columns.Add("tbReport__Deleter");
            dt.Columns.Add("tbReport__DeleteDt");
            dt.Columns.Add("tbReport__Recuperator");
            dt.Columns.Add("tbReport__ReconvertDt");
            dt.Columns.Add("tbReport__Mender");
            dt.Columns.Add("tbReport__ModifyDt");
            dt.Columns.Add("tbReport__IsPrint");
            dt.Columns.Add("tbReport__CheckItemName");
            dt.Columns.Add("tbReport__Optional1");
            dt.Columns.Add("tbReport__Optional2");
            dt.Columns.Add("tbReport__Optional3");
            dt.Columns.Add("tbReport__IsLeaveWord");
            dt.Columns.Add("tbReport__IsDraw");
            dt.Columns.Add("tbReport__DrawerSign");
            dt.Columns.Add("tbReport__DrawTime");
            dt.Columns.Add("tbReport__IsLeaveSound");
            dt.Columns.Add("tbReport__TakeFilmDept");
            dt.Columns.Add("tbReport__TakeFilmRegion");
            dt.Columns.Add("tbReport__PrintTemplateGuid");
            dt.Columns.Add("tbReport__Domain");
            dt.Columns.Add("tbReport__TakeFilmComment");
            dt.Columns.Add("tbReport__RebuildMark");
            dt.Columns.Add("tbReport__PrintCopies");
            dt.Columns.Add("tbReport__SubmitDomain");
            dt.Columns.Add("tbReport__RejectDomain");
            dt.Columns.Add("tbReport__FirstApproveDomain");
            dt.Columns.Add("tbReport__FirstApproveSite");
            dt.Columns.Add("tbReport__RejectSite");
            dt.Columns.Add("tbReport__SecondApproveSite");
            dt.Columns.Add("tbReport__SubmitSite");
            dt.Columns.Add("tbReport__ReportQuality2");
            dt.Columns.Add("tbReport__ReportQualityComments");
            dt.Columns.Add("tbReport__CreaterName");
            dt.Columns.Add("tbReport__SubmitterName");
            dt.Columns.Add("tbReport__FirstApproverName");
            dt.Columns.Add("tbReport__SecondApproverName");
            dt.Columns.Add("tbReport__ScoringVersion");
            dt.Columns.Add("tbReport__AccordRate");
            dt.Columns.Add("tbReport__SubmitterSign");
            dt.Columns.Add("tbReport__FirstApproverSign");
            dt.Columns.Add("tbReport__SecondApproverSign");
            dt.Columns.Add("tbReport__SubmitterSignTimeStamp");
            dt.Columns.Add("tbReport__FirstApproverSignTimeStamp");
            dt.Columns.Add("tbReport__SecondApproverSignTimeStamp");
            dt.Columns.Add("tbReport__IsModified");
            dt.Columns.Add("tbReport__MenderName");


            //ProcedureCode
            dt.Columns.Add("tbProcedureCode__ProcedureCode");
            dt.Columns.Add("tbProcedureCode__Description");
            dt.Columns.Add("tbProcedureCode__EnglishDescription");
            dt.Columns.Add("tbProcedureCode__ModalityType");
            dt.Columns.Add("tbProcedureCode__BodyPart");
            dt.Columns.Add("tbProcedureCode__CheckingItem");
            dt.Columns.Add("tbProcedureCode__BodyCategory");

            return dt;
        }

        public static DataTable CreateDTBaseInfoTemplate()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("tbRegPatient__PatientID");
            dt.Columns.Add("tbRegPatient__PatientGUID");
            dt.Columns.Add("tbRegOrder__OrderGuid");
            dt.Columns.Add("tbRegOrder__ACCNO");
            dt.Columns.Add("tbRegProcedure__ProcedureGuid");
            dt.Columns.Add("tbRegProcedure__ProcedureCode");
            dt.Columns.Add("tbReport__ReportGuid");
            dt.Columns.Add("tbReport__WYSTEXT");
            dt.Columns.Add("tbReport__WYGTEXT");
            dt.Columns.Add("PATIENTEXAMNO");

            return dt;
        }

        public static void SetDTDataForProcedure(ProcedureDto procedure, ref DataRow dr)
        {
            dr["tbRegProcedure__ProcedureGuid"] = procedure.UniqueID;
            dr["tbRegProcedure__ProcedureCode"] = procedure.ProcedureCode;
            dr["tbRegProcedure__ExamSystem"] = procedure.ExamSystem;
            dr["tbRegProcedure__WarningTime"] = procedure.WarningTime;
            dr["tbRegProcedure__FilmSpec"] = procedure.FilmSpec;
            dr["tbRegProcedure__FilmCount"] = procedure.FilmCount;
            dr["tbRegProcedure__ContrastName"] = procedure.ContrastName;
            dr["tbRegProcedure__ContrastDose"] = procedure.ContrastDose;
            dr["tbRegProcedure__ImageCount"] = procedure.ImageCount;
            dr["tbRegProcedure__ExposalCount"] = procedure.ExposalCount;
            dr["tbRegProcedure__Deposit"] = procedure.Deposit;
            dr["tbRegProcedure__Charge"] = procedure.Charge;
            dr["tbRegProcedure__ModalityType"] = procedure.ModalityType;
            dr["tbRegProcedure__Modality"] = procedure.Modality;
            dr["tbRegProcedure__Registrar"] = procedure.Registrar;
            dr["tbRegProcedure__RegisterDt"] = procedure.RegisterTime;
            //dr["tbRegProcedure__Priority"] = procedure.pr;
            dr["tbRegProcedure__Technician"] = procedure.Technician;
            dr["tbRegProcedure__TechDoctor"] = procedure.TechDoctor;
            dr["tbRegProcedure__TechNurse"] = procedure.TechNurse;
            dr["tbRegProcedure__OperationStep"] = procedure.OperationStep;
            dr["tbRegProcedure__ExamineDt"] = procedure.ExamineTime;
            dr["tbRegProcedure__Mender"] = procedure.Mender;
            dr["tbRegProcedure__ModifyDt"] = procedure.ModifyTime;
            dr["tbRegProcedure__IsExistImage"] = procedure.IsExistImage;
            dr["tbRegProcedure__Status"] = procedure.Status;
            dr["tbRegProcedure__Comments"] = procedure.Comments;
            dr["tbRegProcedure__IsCharge"] = procedure.IsCharge;
            dr["tbRegProcedure__RemoteRPID"] = procedure.RemoteRPID;
            dr["tbRegProcedure__QueueNo"] = procedure.QueueNo;
            dr["tbRegProcedure__CreateDt"] = procedure.CreateTime;
            dr["tbRegProcedure__Booker"] = procedure.Booker;
            dr["tbRegProcedure__BookingBeginDt"] = procedure.BookingBeginTime;
            dr["tbRegProcedure__BookingEndDt"] = procedure.BookingEndTime;
            dr["tbRegProcedure__BookerName"] = procedure.BookerName;
            dr["tbRegProcedure__RegistrarName"] = procedure.RegistrarName;
            dr["tbRegProcedure__TechnicianName"] = procedure.TechnicianName;
            dr["tbRegProcedure__RPDesc"] = procedure.RPDesc;
            dr["tbRegProcedure__BodyPart"] = procedure.BodyPart;
            dr["tbRegProcedure__CheckingItem"] = procedure.CheckingItem;
        }

        public static void SetDTDataForReport(Report report, ref DataRow dr)
        {
            dr["tbReport__ReportGuid"] = report.UniqueID;
            dr["tbReport__WYSTEXT"] = report.WYSText;
            dr["tbReport__WYGTEXT"] = report.WYGText;
            dr["tbReport__ReportName"] = report.ReportName;
            dr["tbReport__WYS"] = report.WYS;
            dr["tbReport__WYG"] = report.WYG;
            dr["tbReport__AppendInfo"] = report.AppendInfo;
            dr["tbReport__TechInfo"] = report.TechInfo;
            dr["tbReport__ReportText"] = report.ReportText;
            dr["tbReport__DoctorAdvice"] = report.DoctorAdvice;
            dr["tbReport__IsPositive"] = report.IsPositive;
            dr["tbReport__AcrCode"] = report.AcrCode;
            dr["tbReport__AcrAnatomic"] = report.AcrAnatomic;
            dr["tbReport__AcrPathologic"] = report.AcrPathologic;
            dr["tbReport__Creater"] = report.Creater;
            dr["tbReport__CreateDt"] = report.CreateTime;
            dr["tbReport__Submitter"] = report.Submitter;
            dr["tbReport__SubmitDt"] = report.SubmitTime;
            dr["tbReport__FirstApprover"] = report.FirstApprover;
            dr["tbReport__FirstApproveDt"] = report.FirstApproveTime;
            dr["tbReport__SecondApprover"] = report.SecondApprover;
            dr["tbReport__SecondApproveDt"] = report.SecondApproveTime;
            dr["tbReport__IsDiagnosisRight"] = report.IsDiagnosisRight;
            dr["tbReport__KeyWord"] = report.KeyWord;
            dr["tbReport__ReportQuality"] = report.ReportQuality;
            dr["tbReport__RejectToObject"] = report.RejectToObject;
            dr["tbReport__Rejecter"] = report.Rejecter;
            dr["tbReport__RejectDt"] = report.RejectTime;
            dr["tbReport__Status"] = report.Status;
            dr["tbReport__Comments"] = report.Comments;
            dr["tbReport__Mender"] = report.Mender;
            dr["tbReport__ModifyDt"] = report.ModifyTime;
            dr["tbReport__IsPrint"] = report.IsPrint;
            dr["tbReport__CheckItemName"] = report.CheckItemName;
            dr["tbReport__Optional1"] = report.Optional1;
            dr["tbReport__Optional2"] = report.Optional2;
            dr["tbReport__Optional3"] = report.Optional3;
            dr["tbReport__IsLeaveWord"] = report.IsLeaveWord;
            dr["tbReport__IsDraw"] = report.IsDraw;
            dr["tbReport__DrawerSign"] = report.DrawerSign;
            dr["tbReport__DrawTime"] = report.DrawTime;
            dr["tbReport__IsLeaveSound"] = report.IsLeaveSound;
            dr["tbReport__TakeFilmDept"] = report.TakeFilmDept;
            dr["tbReport__TakeFilmRegion"] = report.TakeFilmRegion;
            dr["tbReport__PrintTemplateGuid"] = report.PrintTemplateID;
            dr["tbReport__Domain"] = report.Domain;
            dr["tbReport__TakeFilmComment"] = report.TakeFilmComment;
            dr["tbReport__RebuildMark"] = report.RebuildMark;
            dr["tbReport__PrintCopies"] = report.PrintCopies;
            dr["tbReport__SubmitDomain"] = report.SubmitDomain;
            dr["tbReport__RejectDomain"] = report.RejectDomain;
            dr["tbReport__FirstApproveDomain"] = report.FirstApproveDomain;
            dr["tbReport__FirstApproveSite"] = report.FirstApproveSite;
            dr["tbReport__RejectSite"] = report.RejectSite;
            dr["tbReport__SecondApproveSite"] = report.SecondApproveSite;
            dr["tbReport__SubmitSite"] = report.SubmitSite;
            dr["tbReport__ReportQuality2"] = report.ReportQuality2;
            dr["tbReport__ReportQualityComments"] = report.ReportQualityComments;
            dr["tbReport__CreaterName"] = report.CreaterName;
            dr["tbReport__SubmitterName"] = report.SubmitterName;
            dr["tbReport__FirstApproverName"] = report.FirstApproverName;
            dr["tbReport__SecondApproverName"] = report.SecondApproverName;
            dr["tbReport__ScoringVersion"] = report.ScoringVersion;
            dr["tbReport__AccordRate"] = report.AccordRate;
            dr["tbReport__IsModified"] = report.IsModified;
            dr["tbReport__MenderName"] = report.MenderName;
        }

        public static void SetDTDataForProcedurecode(List<Procedurecode> procedurecodes, ref DataRow dr)
        {
            foreach (Procedurecode procedurecode in procedurecodes)
            {
                dr["tbProcedureCode__ProcedureCode"] += procedurecode.ProcedureCode + ",";
                dr["tbProcedureCode__Description"] += procedurecode.Description + ",";
                dr["tbProcedureCode__EnglishDescription"] += procedurecode.EnglishDescription + ",";
                dr["tbProcedureCode__ModalityType"] += procedurecode.ModalityType + ",";
                dr["tbProcedureCode__BodyPart"] += procedurecode.BodyPart + ",";
                dr["tbProcedureCode__CheckingItem"] += procedurecode.CheckingItem + ",";
                dr["tbProcedureCode__BodyCategory"] += procedurecode.BodyCategory + ",";
            }

            dr["tbProcedureCode__ProcedureCode"] = dr["tbProcedureCode__ProcedureCode"].ToString().Trim(',');
            dr["tbProcedureCode__Description"] = dr["tbProcedureCode__Description"].ToString().Trim(',');
            dr["tbProcedureCode__EnglishDescription"] = dr["tbProcedureCode__EnglishDescription"].ToString().Trim(',');
            dr["tbProcedureCode__ModalityType"] = dr["tbProcedureCode__ModalityType"].ToString().Trim(',');
            dr["tbProcedureCode__BodyPart"] = dr["tbProcedureCode__BodyPart"].ToString().Trim(',');
            dr["tbProcedureCode__CheckingItem"] = dr["tbProcedureCode__CheckingItem"].ToString().Trim(',');
            dr["tbProcedureCode__BodyCategory"] = dr["tbProcedureCode__BodyCategory"].ToString().Trim(',');
        }

        public static void SetDTDataForOrder(Order order, ref DataRow dr)
        {
            dr["tbRegOrder__OrderGuid"] = order.UniqueID;
            dr["tbRegOrder__ACCNO"] = order.AccNo;
            dr["tbRegOrder__ApplyDept"] = order.ApplyDept;
            dr["tbRegOrder__ApplyDoctor"] = order.ApplyDoctor;
            dr["tbRegOrder__IsScan"] = order.IsScan;
            dr["tbRegOrder__Comments"] = order.Comments;
            dr["tbRegOrder__RemoteAccNo"] = order.RemoteAccNo;
            dr["tbRegOrder__TotalFee"] = order.TotalFee;
            dr["tbRegOrder__InitialDomain"] = order.InitialDomain;
            dr["tbRegOrder__HisID"] = order.HisID;
            dr["tbRegOrder__CardNo"] = order.CardNo;
            dr["tbRegOrder__ReferralId"] = order.ReferralID;
            dr["tbRegOrder__IsReferral"] = order.IsReferral;
            dr["tbRegOrder__VisitGuid"] = order.VisitID;
            dr["tbRegOrder__InhospitalNo"] = order.InhospitalNo;
            dr["tbRegOrder__ClinicNo"] = order.ClinicNo;
            dr["tbRegOrder__PatientType"] = order.PatientType;
            dr["tbRegOrder__Observation"] = order.Observation;
            dr["tbRegOrder__HealthHistory"] = order.HealthHistory;
            dr["tbRegOrder__InhospitalRegion"] = order.InhospitalRegion;
            dr["tbRegOrder__BedNo"] = order.BedNo;
            dr["tbRegOrder__CreateDt"] = order.CreateTime;
            dr["tbRegOrder__CurrentAge"] = order.CurrentAge;
            dr["tbRegOrder__VisitComment"] = order.VisitComment;
            dr["tbRegOrder__ChargeType"] = order.ChargeType;
            dr["tbRegOrder__curPatientName"] = order.CurPatientName;
            dr["tbRegOrder__BodyWeight"] = order.BodyWeight;
            dr["tbRegOrder__curGender"] = order.CurGender;
            dr["tbRegOrder__IsCharge"] = order.IsCharge;
            dr["tbRegOrder__AgeInDays"] = order.AgeInDays;
            dr["tbRegOrder__BookingSite"] = order.BookingSite;
            dr["tbRegOrder__ExamSite"] = order.ExamSite;
            dr["tbRegOrder__RegSite"] = order.RegSite;
            dr["TBREGORDER__ORDERMESSAGE"] = order.OrderMessage;
            dr["TBREGORDER__PathologicalFindings"] = order.PathologicalFindings;
            dr["TBREGORDER__InternalOptional1"] = order.InternalOptional1;
            dr["TBREGORDER__InternalOptional2"] = order.InternalOptional2;
            dr["TBREGORDER__ExternalOptional1"] = order.ExternalOptional1;
            dr["TBREGORDER__ExternalOptional2"] = order.ExternalOptional2;
            dr["TBREGORDER__ExternalOptional3"] = order.ExternalOptional3;
            dr["tbRegOrder__InjectDose"] = order.InjectDose;
            dr["tbRegOrder__InjectTime"] = order.InjectTime;
            dr["tbRegOrder__BodyHeight"] = order.BodyHeight;
            dr["tbRegOrder__BloodSugar"] = order.BloodSugar;
            dr["tbRegOrder__Insulin"] = order.Insulin;
            dr["tbRegOrder__GoOnGoTime"] = order.GoOnGoTime;
            dr["tbRegOrder__InjectorRemnant"] = order.InjectorRemnant;
            dr["tbRegOrder__SubmitHospital"] = order.SubmitHospital;
            dr["tbRegOrder__SubmitDept"] = order.SubmitDepartment;
            dr["tbRegOrder__SubmitDoctor"] = order.SubmitDoctor;
            dr["tbRegOrder__Assign2site"] = order.Assign2Site;
            dr["tbRegOrder__CurrentSite"] = order.CurrentSite;
        }

        public static void SetDTDataForPatient(Patient patient, ref DataRow dr)
        {
            dr["tbRegPatient__PatientID"] = patient.PatientNo;
            dr["tbRegPatient__PatientGUID"] = patient.UniqueID;
            dr["tbRegPatient__GlobalID"] = patient.GlobalID;
            dr["tbRegPatient__LocalName"] = patient.LocalName;
            dr["tbRegPatient__EnglishName"] = patient.EnglishName;
            dr["tbRegPatient__ReferenceNo"] = patient.ReferenceNo;
            dr["tbRegPatient__Birthday"] = patient.Birthday;
            dr["tbRegPatient__Gender"] = patient.Gender;
            dr["tbRegPatient__Address"] = patient.Address;
            dr["tbRegPatient__Telephone"] = patient.Telephone;
            dr["tbRegPatient__IsVIP"] = patient.IsVIP;
            dr["tbRegPatient__CreateDt"] = patient.CreateTime;
            dr["tbRegPatient__Site"] = patient.Site;
            dr["tbRegPatient__SocialSecurityNo"] = patient.SocialSecurityNo;
        }
    }
}