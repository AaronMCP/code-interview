using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.Platform.Application;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.EntityFramework;
using System.Globalization;
using Hys.CrossCutting.Common.Utils;
using Hys.CareRIS.Application.Dtos.Report;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class CommonService : DisposableServiceBase, ICommonService
    {
        private IProcedureCodeRepository _ProcedureCodeRepository;
        private IModalityTypeRepository _ModalityTypeRepository;
        private IModalityRepository _ModalityRepository;
        private IRisProContext _dbContext;

        public CommonService(IProcedureCodeRepository procedurecodeRepository,
            IModalityTypeRepository modalityTypeRepository, IModalityRepository modalityRespository, IRisProContext dbContext)
        {
            _ProcedureCodeRepository = procedurecodeRepository;

            _ModalityTypeRepository = modalityTypeRepository;
            _ModalityRepository = modalityRespository;
            _dbContext = dbContext;

            AddDisposableObject(procedurecodeRepository);

            AddDisposableObject(dbContext);
        }

        public IEnumerable<ProcedureCodeDto> GetProcedureCodes()
        {
            var procedurecodes = _ProcedureCodeRepository.Get().Select(p => Mapper.Map<Procedurecode, ProcedureCodeDto>(p)).ToList();
            return procedurecodes;
        }

        public IEnumerable<string> GetModalityTypes()
        {
            var modalityTypes = _ModalityTypeRepository.Get().Select(p => p.Modalitytype).Distinct().ToList();
            return modalityTypes;
        }

        public IEnumerable<string> GetBodyParts()
        {
            var bodyParts = _ProcedureCodeRepository.Get().Select(p => p.BodyPart).Distinct().ToList();
            CultureInfo PronoCi = new CultureInfo(2052);

            Thread.CurrentThread.CurrentCulture = PronoCi;

            bodyParts.Sort();
            return bodyParts;
        }

        public void WriteBroker(string dataID, PatientDto patient, OrderDto order, ProcedureDto procedure,
            ReportDto report, int event_type, int exam_status, int report_status, bool isDataSigned = false, string loginUserID = "")
        {
            //public static string MakeSQL4GateWay(tagReportInfo rptInfo, string guid, int event_type, int exam_status, int report_status, bool isDataSigned = false)
            //{
            //    return "insert GW_DataIndex(data_id, data_dt, event_type, RECORD_INDEX_1, Data_Source)"
            //        + " values('" + guid + "', getdate(), '" + event_type.ToString() + "', 'ReportGuid', 'Local')"
            //        + " insert GW_Patient(DATA_ID,DATA_DT,PATIENTID,OTHER_PID,PATIENT_NAME,PATIENT_LOCAL_NAME,"
            //        + "BIRTHDATE,SEX,PATIENT_ALIAS,ADDRESS,PHONENUMBER_HOME,MARITAL_STATUS,PATIENT_TYPE,"
            //        + "PATIENT_LOCATION,VISIT_NUMBER,BED_NUMBER,CUSTOMER_1,CUSTOMER_2,CUSTOMER_3,CUSTOMER_4,SSN_NUMBER,DRIVERLIC_NUMBER)"
            //        + "    values('" + guid + "', getdate(), '" + rptInfo.patientID + "','" + rptInfo.remotePID + "','"
            //        + rptInfo.patientName + "','" + rptInfo.patientLocalName + "','"
            //        + rptInfo.birthday.ToString("yyyy-MM-dd") + "','"
            //        + rptInfo.gender + "', '"
            //        + rptInfo.patientAlias + "', '"
            //        + rptInfo.patientAddress + "', '"
            //        //+ rptInfo.patientPhone + "', '" + rptInfo.patientMarriage + "','" + (rptInfo.patientType == "I" ? "I" : "O") + "','"
            //        + rptInfo.patientPhone + "', '" + rptInfo.patientMarriage + "','"
            //        + rptInfo.patientType + "','"
            //        + rptInfo.inHospitalRegion + "', '" + rptInfo.visitNo + "', '"
            //        + rptInfo.bedNo + "', '" + rptInfo.patientName + "', '"
            //        + rptInfo.isVIP + "', '" + rptInfo.inHospitalNo + "', '"
            //        + ServerPubFun.getSQLString(rptInfo.patientComment) + "', '"
            //        + rptInfo.MedicareNo + "', '" + rptInfo.ReferenceNo + "') "
            //        + " insert GW_Order(DATA_ID,DATA_DT,ORDER_NO,PLACER_NO,FILLER_NO,PATIENT_ID,EXAM_STATUS,"
            //        + "PLACER_DEPARTMENT, PLACER, FILLER_DEPARTMENT, FILLER, REF_PHYSICIAN, REQUEST_REASON, "
            //        + "REUQEST_COMMENTS, EXAM_REQUIREMENT, SCHEDULED_DT, MODALITY, STATION_NAME, EXAM_LOCATION, "
            //        + "EXAM_DT, DURATION, TECHNICIAN, BODY_PART, PROCEDURE_CODE, PROCEDURE_DESC, EXAM_COMMENT, "
            //        + "CHARGE_STATUS, CHARGE_AMOUNT,CUSTOMER_1,CUSTOMER_2,CUSTOMER_3) "
            //        + "    values('" + guid + "', getdate(), '" + rptInfo.orderGuid + "','" + rptInfo.remoteAccNo + "', '"
            //        + rptInfo.AccNO + "', '" + rptInfo.patientID + "', '" + exam_status.ToString() + "', '"
            //        + rptInfo.dept + "', '" + rptInfo.applyDoctor + "','"
            //        + rptInfo.dept + "', '" + rptInfo.applyDoctor + "', '"
            //        + rptInfo.applyDoctor + "', '"
            //        + ServerPubFun.getSQLString(rptInfo.observation) + "', '"
            //        + ServerPubFun.getSQLString(rptInfo.visitComments) + "', '"
            //        + ServerPubFun.getSQLString(rptInfo.orderComments) + "', '"
            //        + rptInfo.registerDt.ToBrokerString() + "', '"
            //        + rptInfo.modalityType + "', '" + rptInfo.modality + "', '', '"
            //        + rptInfo.examineDt.ToBrokerString() + "', '"
            //        + rptInfo.duration + "', '" + rptInfo.technician__LocalName + "', '"
            //        + rptInfo.bodypart + "', '" + rptInfo.procedureCode + "', '"
            //        + ServerPubFun.getSQLString(rptInfo.procedureDesc) + "', '"
            //        + ServerPubFun.getSQLString(rptInfo.orderComments) + "', '"
            //        + (rptInfo.isCharge == "1" ? "Y" : "N") + "', '"
            //        + rptInfo.charge + "','" + rptInfo.cardno + "','" + rptInfo.hisid + "','" + rptInfo.MedicareNo + "') "
            //        + " insert GW_Report(data_id, data_dt, report_no, ACCESSION_NUMBER, PATIENT_ID, REPORT_STATUS, MODALITY, "
            //        + " REPORT_TYPE, REPORT_FILE, REPORT_WRITER, REPORT_APPROVER, REPORTDT, OBSERVATIONMETHOD,DIAGNOSE,COMMENTS,CUSTOMER_4,CUSTOMER_1)"
            //        + " values('" + guid + "', getdate(), '" + rptInfo.reportGuid + "', '" + rptInfo.AccNO + "',"
            //        + " '" + rptInfo.patientID + "', '" + report_status.ToString() + "', '" + rptInfo.modalityType + "', '0', '', '" + rptInfo.reportCreater_LocalName + "',"
            //        + " '" + rptInfo.reportApprover_LocalName + "', '" + rptInfo.reportCreateDt.ToBrokerString() + "',"
            //        + " '"
            //        + ServerPubFun.getSQLString(rptInfo.operationStep) + "','"
            //        + ServerPubFun.getSQLString(rptInfo.wysText) + "','"
            //        + ServerPubFun.getSQLString(rptInfo.wygText) + "','"
            //        + ServerPubFun.getSQLString(rptInfo.techinfo) + "','"
            //        + (isDataSigned == true ? "Y" : "N") + "')";

            //}


            GWDataIndex gwDataIndex = new GWDataIndex();
            gwDataIndex.UniqueID = dataID;
            gwDataIndex.DataTime = DateTime.Now;
            gwDataIndex.EventType = event_type.ToString();
            //gwDataIndex.RecordIndex1 = "";
            //if (report != null)
            //{
            //    if (report.DeleteMark.HasValue && report.DeleteMark.Value)
            //    {
            //        gwDataIndex.RecordIndex1 = "ReportGuid";
            //    }
            //    else
            //    {
            //        gwDataIndex.RecordIndex1 = report.UniqueID;
            //    }
            //}
            gwDataIndex.RecordIndex1 = "ReportGuid";
            gwDataIndex.RecordIndex3 = "RIS";
            gwDataIndex.DataSource = "Local";
            _dbContext.Set<GWDataIndex>().Add(gwDataIndex);

            //patient
            if (patient != null)
            {
                GWPatient gwPatient = new GWPatient();
                gwPatient.UniqueID = dataID;
                gwPatient.DataTime = DateTime.Now;
                gwPatient.PatientID = patient.PatientNo;
                //dto not
                gwPatient.OtherPID = patient.RemotePID;
                gwPatient.PatientName = patient.EnglishName;
                gwPatient.PatientLocalName = patient.LocalName;
                gwPatient.BirthDate = patient.Birthday.Value.ToString("yyyy-MM-dd");
                gwPatient.Sex = patient.Gender;
                gwPatient.PatientAlias = patient.Alias;
                gwPatient.Address = patient.Address;
                //gwPatient.PhoneNumberHome = patient.p;
                gwPatient.MaritalStatus = patient.Marriage;
                gwPatient.PatientType = order.PatientType;
                gwPatient.PatientLocation = order.InhospitalRegion;
                gwPatient.VisitNumber = order.ClinicNo;
                gwPatient.BedNumber = order.BedNo;
                gwPatient.Customer1 = patient.EnglishName;
                gwPatient.Customer2 = patient.IsVip.Value == true ? "1" : "0";
                gwPatient.Customer3 = order.InhospitalNo;
                gwPatient.Customer4 = getSQLString(patient.Comments);
                gwPatient.SSNBumber = patient.MedicareNo;
                gwPatient.DriverLicNumber = patient.ReferenceNo;

                _dbContext.Set<GWPatient>().Add(gwPatient);
            }

            //patient
            if (order != null)
            {
                GWOrder gwOrder = new GWOrder();
                gwOrder.UniqueID = dataID;
                gwOrder.DataTime = DateTime.Now;
                gwOrder.OrderNo = order.UniqueID;
                gwOrder.PlacerNo = order.RemoteAccNo;
                gwOrder.FillerNo = order.AccNo;
                gwOrder.PatientID = patient.PatientNo;
                gwOrder.ExamStatus = exam_status.ToString();
                gwOrder.PlacerDepartment = order.ApplyDept;
                gwOrder.Placer = order.ApplyDoctor;
                gwOrder.FillerDepartment = order.ApplyDept;
                gwOrder.Filler = order.ApplyDoctor;

                gwOrder.RefPhysician = order.ApplyDoctor;
                gwOrder.RequestReason = getSQLString(order.Observation);
                gwOrder.ReuqestComments = getSQLString(order.VisitComment);
                gwOrder.ExamComment = getSQLString(order.Comments);
                gwOrder.ScheduledTime = procedure.RegisterTime.Value.ToBrokerString();
                gwOrder.Modality = procedure.ModalityType;
                gwOrder.StationName = procedure.Modality;
                gwOrder.ExamLocation = "";
                gwOrder.ExamTime = procedure.ExamineTime.Value.ToBrokerString();
                gwOrder.Duration = System.Convert.ToString(procedure.ExamineTime.Value - procedure.RegisterTime.Value);
                gwOrder.Technician = GetLocalName(procedure.Technician);
                gwOrder.BodyPart = procedure.BodyPart;
                gwOrder.ProcedureCode = procedure.ProcedureCode;
                gwOrder.ProcedureDesc = getSQLString(procedure.RPDesc);
                gwOrder.ExamComment = getSQLString(order.Comments);
                if (procedure.IsCharge.HasValue)
                {
                    gwOrder.ChargeStatus = procedure.IsCharge.Value == true ? "Y" : "N";
                }
                gwOrder.ChargeAmount = procedure.Charge.ToString();
                gwOrder.Customer1 = order.CardNo;
                gwOrder.Customer2 = order.HisID;
                gwOrder.Customer3 = patient.MedicareNo;

                _dbContext.Set<GWOrder>().Add(gwOrder);
            }

            if (report != null)
            {
                GWReport gwReport = new GWReport();
                gwReport.UniqueID = dataID;
                gwReport.DataTime = DateTime.Now;

                gwReport.ReportNo = report.UniqueID;
                if (order != null)
                {
                    gwReport.AccessionNumber = order.AccNo;
                }
                if (patient != null)
                {
                    gwReport.PatientID = patient.PatientNo;
                }
                gwReport.ReportStatus = report_status.ToString();

                gwReport.ReportType = 0;
                gwReport.ReportFile = "";
                gwReport.ReportWriter = GetLocalName(report.Creater);
                gwReport.ReportApprover = GetLocalName(report.FirstApprover);
                gwReport.ReportTime = report.CreateTime.Value.ToBrokerString();
                if (procedure != null)
                {
                    gwReport.Modality = procedure.ModalityType;
                    gwReport.ObservationMethod = getSQLString(procedure.OperationStep);
                }
                gwReport.Diagnose = getSQLString(report.WYSText);
                gwReport.Comments = getSQLString(report.WYGText);
                gwReport.Customer4 = getSQLString(report.TechInfo);
                gwReport.Customer1 = isDataSigned == true ? "Y" : "N";

                _dbContext.Set<GWReport>().Add(gwReport);
            }
            _dbContext.SaveChanges();
        }

        public void WriteBroker(PatientDto patient, OrderDto order, ProcedureDto procedure, ReportDto report, string patientEventType, string examStatus, int actionCode,
            string reportStatus, bool isPatientBroker = false, bool isDataSigned = false,string loginUserID = "")
        {
            GWDataIndex gwDataIndex = new GWDataIndex();
            var dataID = Guid.NewGuid().ToString();
            gwDataIndex.UniqueID = dataID;
            gwDataIndex.DataTime = DateTime.Now;
            gwDataIndex.RecordIndex1 = "";
            gwDataIndex.RecordIndex3 = "RIS";
            gwDataIndex.DataSource = "Local";
            GWOrder gwOrder = null;
            GWPatient gwPatient = null;
            GWReport gwReport = null;
            //patient
            if (patient != null)
            {
                gwDataIndex.EventType = patientEventType;
                gwPatient = new GWPatient
                {
                    UniqueID = dataID,
                    DataTime = DateTime.Now,
                    PatientID = patient.PatientNo,
                    OtherPID = patient.RemotePID,
                    PatientName = patient.EnglishName,
                    PatientLocalName = patient.LocalName,
                    BirthDate = patient.Birthday.Value.ToString("yyyy-MM-dd"),
                    Sex = patient.Gender,
                    PatientAlias = patient.Alias,
                    Address = patient.Address,
                    MaritalStatus = patient.Marriage,
                    PatientType = order.PatientType,
                    PatientLocation = order.InhospitalRegion,
                    VisitNumber = order.ClinicNo,
                    BedNumber = order.BedNo,
                    Customer1 = patient.EnglishName,
                    Customer2 = patient.IsVip.HasValue ? (patient.IsVip.Value == true ? "1" : "0") : "0",
                    Customer3 = order.InhospitalNo,
                    Customer4 = getSQLString(patient.Comments),
                    SSNBumber = patient.MedicareNo,
                    DriverLicNumber = patient.ReferenceNo,
                    PhoneNumberHome = patient.Telephone
                };
                //dto not
                //gwPatient.PhoneNumberHome = patient.procedure;
            }

            if (order != null && !isPatientBroker)
            {
                // order
                gwOrder = new GWOrder
                {
                    UniqueID = dataID,
                    DataTime = DateTime.Now,
                    OrderNo = procedure.UniqueID,
                    PlacerNo = order.RemoteAccNo,
                    FillerNo = order.AccNo,
                    PatientID = patient.PatientNo,
                    ExamStatus = examStatus,
                    PlacerDepartment = order.ApplyDept,
                    Placer = order.ApplyDoctor,
                    FillerDepartment = order.ApplyDept,
                    Filler = order.ApplyDoctor,
                    RefPhysician = order.ApplyDoctor,
                    RequestReason = getSQLString(order.Observation),
                    ReuqestComments = getSQLString(order.VisitComment),
                    ExamComment = getSQLString(order.Comments),
                    Modality = procedure.ModalityType,
                    StationName = procedure.Modality,
                    ExamLocation = "",
                    ExamTime = procedure.ExamineTime.HasValue ? procedure.ExamineTime.Value.ToBrokerString() : null,
                    Duration =
                        procedure.ExamineTime.HasValue && procedure.RegisterTime.HasValue
                            ? System.Convert.ToString(procedure.ExamineTime.Value - procedure.RegisterTime.Value)
                            : null,
                    Technician = GetLocalName(procedure.Technician),
                    BodyPart = procedure.BodyPart,
                    ProcedureCode = procedure.ProcedureCode,
                    ProcedureDesc = getSQLString(procedure.RPDesc)
                };
                gwOrder.ExamComment = getSQLString(order.Comments);
                gwOrder.StudyInstanceUID = order.StudyInstanceUID;

                gwOrder.ChargeStatus = procedure.IsCharge.HasValue ? (procedure.IsCharge.Value ? "Y" : "N") : "N";
                gwOrder.ChargeAmount = procedure.Charge.ToString();
                gwOrder.Customer1 = order.CardNo;
                gwOrder.Customer2 = order.HisID;
                gwOrder.Customer3 = patient.MedicareNo;

                if (order.CurrentAge != null && order.CurrentAge.Trim().Length > 3)
                {
                    var nIndex = order.CurrentAge.IndexOf(" ", StringComparison.Ordinal);
                    gwOrder.PlacerContact = order.CurrentAge.Substring(0, nIndex + 2);
                    gwOrder.PlacerContact = gwOrder.PlacerContact.Replace(" ", "");
                }
                if (procedure.Status > 20)
                {
                    gwOrder.ExamLocation = GetModalityRoom(gwOrder.Modality);
                }

                if (actionCode == (int)Enums.ActionCode.Update)
                {
                    gwDataIndex.EventType = "11";//update rp
                    if (procedure.Status < 50)
                    {
                        gwOrder.ExamStatus = "11";
                    }
                    else//The RP has examined
                    {
                        gwOrder.ExamStatus = "16";
                    }
                }
                else if (actionCode == (int)Enums.ActionCode.Create)
                {
                        gwDataIndex.EventType = "10";//Add new rp
                        gwOrder.ExamStatus = "10";
                }
                else if(actionCode == (int)Enums.ActionCode.Delete)
                {
                        gwOrder.FillerContact = GetFillerContact(loginUserID);
                        gwDataIndex.EventType = "13"; //delete rp
                        gwOrder.ExamStatus = "13";
                }
                else if (actionCode.Equals((int)Enums.ActionCode.BookingToReg))
                {
                    gwDataIndex.EventType = Contants.Broker.CreateRegistrationEventType;//transfert bookin to registration
                    gwOrder.ExamStatus = Contants.Broker.TransferBooking2RegExamStatus;
                    gwOrder.ScheduledTime = Convert.ToDateTime(procedure.BookingBeginTime).ToBrokerString();
                }
                else if (actionCode == (int)Enums.ActionCode.BookingCreate)
                {  //add new booking order
                    gwDataIndex.EventType = "20"; //delete rp
                    gwOrder.ExamStatus = "104";
                    gwOrder.ScheduledTime = Convert.ToDateTime(procedure.BookingBeginTime).ToBrokerString();
                }
                else if (actionCode == (int)Enums.ActionCode.BookingUpdate)
                {  //update booking order
                    gwDataIndex.EventType = "21"; //
                    gwOrder.ExamStatus = "101";
                    gwOrder.ScheduledTime = Convert.ToDateTime(procedure.BookingBeginTime).ToBrokerString();
                }
                else if (actionCode == (int)Enums.ActionCode.BookingDelete)
                {  //delete booking order
                    gwDataIndex.EventType = "23";
                    gwOrder.ExamStatus = "103";
                    gwOrder.ScheduledTime = Convert.ToDateTime(procedure.BookingBeginTime).ToBrokerString();
                }
                if (actionCode == (int)Enums.ActionCode.FinishExam)
                {
                    gwDataIndex.EventType = Contants.Broker.FinishExamEventType;//update rp
                    gwOrder.ExamStatus = Contants.Broker.FinishExamStatus;
                    if (procedure.Status == (int)RPStatus.NoCheck)
                    {
                        gwOrder.ScheduledTime = Convert.ToDateTime(procedure.BookingBeginTime).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        gwOrder.ScheduledTime = "";
                    }
                }
            }

            if (report != null)
            {
                gwReport = new GWReport();
                gwReport.UniqueID = dataID;
                gwReport.DataTime = DateTime.Now;

                gwReport.ReportNo = report.UniqueID;
                if (order != null)
                {
                    gwReport.AccessionNumber = order.AccNo;
                }
                if (patient != null)
                {
                    gwReport.PatientID = patient.PatientNo;
                }
                gwReport.ReportStatus = reportStatus.ToString();

                gwReport.ReportType = 0;
                gwReport.ReportFile = "";
                gwReport.ReportWriter = GetLocalName(report.Creater);
                gwReport.ReportApprover = GetLocalName(report.FirstApprover);
                gwReport.ReportTime = report.CreateTime.Value.ToBrokerString();
                if (procedure != null)
                {
                    gwReport.Modality = procedure.ModalityType;
                    gwReport.ObservationMethod = getSQLString(procedure.OperationStep);
                }
                gwReport.Diagnose = getSQLString(report.WYSText);
                gwReport.Comments = getSQLString(report.WYGText);
                gwReport.Customer4 = getSQLString(report.TechInfo);
                gwReport.Customer1 = isDataSigned == true ? "Y" : "N";
            }
            _dbContext.Set<GWDataIndex>().Add(gwDataIndex);
            if (gwPatient != null)
            {
                _dbContext.Set<GWPatient>().Add(gwPatient);
            }
            if (gwOrder != null)
            {
                _dbContext.Set<GWOrder>().Add(gwOrder);
            }
            if (gwReport != null)
            {
                _dbContext.Set<GWReport>().Add(gwReport);
            }
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Get palcer contact via current age
        /// </summary>
        /// <param name="currentAge"></param>
        /// <returns></returns>
        private string GetFillerContact(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty (id = id.Trim()))
                {
                    var query = (from u in _dbContext.Set<User>()
                                 where id.Equals(u.UniqueID)
                                 select u.LoginName +"|"+ u.LocalName).FirstOrDefault();
                    return query;
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }
        /// <summary>
        /// GetLocalName
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        private string GetLocalName(string userGuid)
        {
            try
            {
                if (userGuid != null && (userGuid = userGuid.Trim()).Length > 0)
                {
                    var query = (from u in _dbContext.Set<User>()
                                 where userGuid.Contains(u.UniqueID)
                                 select u.LocalName
                                 ).ToArray();
                    return string.Join(",", query);
                }
            }
            catch (Exception ex)
            {
            }

            return "";
        }

        
        /// <summary>
        /// convert Single quote to double Single quotes for SQL sentence
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private static string getSQLString(string src)
        {
            if (!string.IsNullOrEmpty(src))
            {
                return src.Replace("'", "''");
            }

            return string.Empty;
        }

        /// <summary>
        /// Get modality room
        /// </summary>
        /// <param name="modality"></param>
        /// <returns></returns>
        private string GetModalityRoom(string modality)
        {
            var _modality = _ModalityRepository.Get(m => m.ModalityName.Equals(modality, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (_modality == null)
            {
                return string.Empty;
            }
            return _modality.Room;
        }
    }
}
