using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using Hys.Platform.Application;
using Hys.CareRIS.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using Hys.CareRIS.EntityFramework;
using Hys.Platform.Domain.Ris;
using System.ServiceModel;
using Hys.CrossCutting.Common.Utils;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;
using Hys.CareRIS.Application.Dtos.Report;
using Hys.CareRIS.EnterpriseLib;
using Hys.Common;
using Hys.Platform.CrossCutting.LogContract;


namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class RegistrationService : DisposableServiceBase, IRegistrationService
    {
        private IPatientRepository _PatientRepository;
        private IOrderRepository _OrderRepository;
        private IProcedureRepository _ProcedureRepository;
        private IAccessionNumberListRepository _accessionNumberListRepository;
        private IGWDataIndexRepository _GWDataIndexRepository;
        private IGWPatientRepository _GWPatientRepository;
        private IGWOrderRepository _GWOrderRepository;
        private IUserRepository _userRepository;
        private IModalityRepository _modalityRespository;
        private IBodySystemMapRepository _bodySystemMapRepository;
        private IProcedureCodeRepository _ProcedureCodeRepository;
        private IRequestRepository _requestRepository;
        private IRequestItemRepository _requestItemRepository;
        private IRequestChargeRepository _requestChargeRepository;
        private IRequestListRepository _requestListRepository;
        private ICommonService _CommonService;
        private IUserManagementService _userManagementService;
        private IConfigurationService _configurationService;
        private IRisProContext _risProContext;
        private IRequisitionRepository _requisitionRepository;
        private ICommonLog _logger;

        public RegistrationService(
            IPatientRepository patientRepository,
            IOrderRepository orderRepository,
            IProcedureRepository procedureRepository,
            IAccessionNumberListRepository accessionNumberListRepository,
            IGWDataIndexRepository GWDataIndexRepository,
            IGWPatientRepository GWPatientRepository,
             IGWOrderRepository GWOrderRepository,
            IUserRepository userRepository,
            IModalityRepository modalityRespository,
            IBodySystemMapRepository bodySystemMapRepository,
            IProcedureCodeRepository procedureCodeRepository,
            IRisProContext risProContext,
            ICommonService commonService,
            IUserManagementService userManagementService,
            IConfigurationService configurationService,
            IRequestRepository requestRepository,
            IRequestItemRepository requestItemRepository,
            IRequestChargeRepository requestChargeRepository,
            IRequestListRepository requestListRepository,
            IRequisitionRepository requisitionRepository,
            ICommonLog logger)
        {
            _PatientRepository = patientRepository;
            _OrderRepository = orderRepository;
            _ProcedureRepository = procedureRepository;
            _accessionNumberListRepository = accessionNumberListRepository;
            _GWDataIndexRepository = GWDataIndexRepository;
            _GWPatientRepository = GWPatientRepository;
            _userRepository = userRepository;
            _GWOrderRepository = GWOrderRepository;
            _modalityRespository = modalityRespository;
            _bodySystemMapRepository = bodySystemMapRepository;
            _ProcedureCodeRepository = procedureCodeRepository;
            _risProContext = risProContext;
            _CommonService = commonService;
            _userManagementService = userManagementService;
            _configurationService = configurationService;
            _requestRepository = requestRepository;
            _requestItemRepository = requestItemRepository;
            _requestChargeRepository = requestChargeRepository;
            _requestListRepository = requestListRepository;
            _requisitionRepository = requisitionRepository;
            _logger = logger;

            AddDisposableObject(patientRepository);
            AddDisposableObject(orderRepository);
            AddDisposableObject(procedureRepository);
            AddDisposableObject(accessionNumberListRepository);
            AddDisposableObject(GWDataIndexRepository);
            AddDisposableObject(GWPatientRepository);
            AddDisposableObject(risProContext);

            AddDisposableObject(userRepository);
            AddDisposableObject(GWOrderRepository);
            AddDisposableObject(modalityRespository);
            AddDisposableObject(bodySystemMapRepository);
            AddDisposableObject(procedureCodeRepository);
            AddDisposableObject(commonService);
            AddDisposableObject(userManagementService);

            AddDisposableObject(configurationService);
            AddDisposableObject(requestRepository);
            AddDisposableObject(requestItemRepository);
            AddDisposableObject(requestChargeRepository);
            AddDisposableObject(requestListRepository);
            AddDisposableObject(requisitionRepository);
        }

        public IEnumerable<PatientDto> GetPatients()
        {
            var patients = _PatientRepository.Get().Select(p => Mapper.Map<Patient, PatientDto>(p)).ToList();
            return patients;
        }

        public PatientDto GetPatient(string patientId)
        {
            var patient = _PatientRepository.Get(p => p.UniqueID.Equals(patientId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (patient != null)
            {
                return Mapper.Map<Patient, PatientDto>(patient);
            }
            return null;
        }

        public PatientDto GetPatientByNo(string patientNo)
        {
            var patient = _PatientRepository.Get(p => p.PatientNo.Equals(patientNo, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (patient != null)
            {
                return Mapper.Map<Patient, PatientDto>(patient);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientName"></param>
        /// <returns></returns>
        public IEnumerable<PatientDto> GetPatientsByName(string patientName)
        {
            var reg = new Regex("[\u4e00-\u9fa5]");
            var isLocalName = reg.IsMatch(patientName);
            var patient = new List<Patient>();
            if (isLocalName)
            {
                patient = _PatientRepository.Get(p => p.LocalName.Equals(patientName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                patient = _PatientRepository.Get(p => p.EnglishName.Equals(patientName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (patient.Any())
            {
                return patient.Select(i => Mapper.Map<Patient, PatientDto>(i));
            }
            return null;
        }

        public void AddPatient(PatientDto patient)
        {
            _PatientRepository.Add(Mapper.Map<PatientDto, Patient>(patient));
            _PatientRepository.SaveChanges();
        }

        //case 1:update patient when creating new order
        public void UpdatePatient(PatientDto patientUpdate)
        {
            if (patientUpdate != null)
            {
                var patient = _PatientRepository.Get(p => p.UniqueID.Equals(patientUpdate.UniqueID)).FirstOrDefault();
                if (patient != null)
                {
                    ((System.Data.Entity.DbContext)_risProContext).Entry(patient).State = System.Data.Entity.EntityState.Detached;
                    _PatientRepository.Update(Mapper.Map<PatientDto, Patient>(patientUpdate));
                }
                else
                {
                    _PatientRepository.Add(Mapper.Map<PatientDto, Patient>(patientUpdate));
                }
                _PatientRepository.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("Not found patient or order.");
            }
        }

        /// <summary>
        /// Add patient in registration view edit
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public PatientDto AddPatient(PatientEditDto patientEdit)
        {
            var patient = patientEdit.Patient;
            var order = GetOrder(patientEdit.OrderID);
            patient.UniqueID = Guid.NewGuid().ToString();
            patient.CreateTime = DateTime.Now;
            patient.UpdateTime = DateTime.Now;
            patient.IsUploaded = false;
            AddPatient(patient);
            _CommonService.WriteBroker(patient, order, null, null, "00", null, 0, null, true);
            return GetPatient(patient.UniqueID);
        }

        /// <summary>
        /// Update patient in registration view edit
        /// case 2:update patient when editting order 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public PatientDto UpdatePatient(PatientEditDto patientEdit)
        {
            var patient = patientEdit.Patient;
            if (patient == null)
            {
                throw new ArgumentNullException();
            }


            var order = _OrderRepository.Get(o => o.UniqueID.Equals(patientEdit.OrderID)).FirstOrDefault();
            if (order == null)
            {
                order = _OrderRepository.Get(o => o.PatientID.Equals(patientEdit.Patient.UniqueID)).FirstOrDefault();
            }
            else
            {
                order.CurrentAge = patientEdit.CurrentAge;
            }
            patient.UpdateTime = DateTime.Now;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                 new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                _OrderRepository.SaveChanges();
                UpdatePatient(patient);
                if (order != null)
                {
                    _CommonService.WriteBroker(patient, Mapper.Map<Order, OrderDto>(order), null, null, "01", null, 0, null, true);
                }
                ts.Complete();
            }
            return GetPatient(patient.UniqueID);
        }

        public void DeletePatient(string patientId)
        {
            var patient = _PatientRepository.Get(p => p.UniqueID.Equals(patientId.ToString(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (patient != null)
            {
                _PatientRepository.Delete(patient);
                _PatientRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Get patient's procedures with report info.
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public IQueryable GetProcedures(string patientID, string orderID = null)
        {
            var query = from p in _risProContext.Set<Procedure>()
                        join o in _risProContext.Set<Order>() on p.OrderID equals o.UniqueID
                        join u in _risProContext.Set<User>() on p.Registrar equals u.UniqueID into users
                        from u in users.DefaultIfEmpty()
                        join r in _risProContext.Set<Report>() on p.ReportID equals r.UniqueID into reports
                        from r in reports.DefaultIfEmpty()
                        where o.PatientID.Equals(patientID) && !p.OrderID.Equals(orderID)
                        orderby p.CreateTime descending
                        select new
                        {
                            o.AccNo,
                            p.CreateTime,
                            p.RegisterTime,
                            p.ExamineTime,
                            p.Modality,
                            p.ModalityType,
                            p.Status,
                            p.RPDesc,
                            Registrar = u.LocalName,
                            UniqueID = p.UniqueID,
                            ReportID = r.UniqueID,
                            r.WYGText,
                            r.WYSText,
                            r.IsPositive
                        };
            return query;
        }

        #region Order Operation Service
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderDto> GetAllOrders()
        {
            var orders = _OrderRepository.Get().Select(r => Mapper.Map<Order, OrderDto>(r)).ToList();
            return orders;
        }

        /// <summary>
        /// Get order by Id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderDto GetOrder(string orderId)
        {
            var order = _OrderRepository.Get(r => r.UniqueID.Equals(orderId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (order != null)
            {
                return Mapper.Map<Order, OrderDto>(order);
            }
            return null;
        }

        /// <summary>
        /// Add new order 
        /// </summary>
        /// <param name="orderDto"></param>
        public void AddOrder(OrderDto orderDto)
        {
            _OrderRepository.Add(Mapper.Map<OrderDto, Order>(orderDto));
            _OrderRepository.SaveChanges();
        }

        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="orderDto"></param>
        public void UpdateOrder(OrderDto orderDto, bool IsRegistrationView = false)
        {
            if (orderDto == null)
            {
                throw new ArgumentNullException();
            }
            var order = _OrderRepository.Get(o => o.UniqueID.Equals(orderDto.UniqueID)).FirstOrDefault();
            if (order != null)
            {
                var orderUpdate = Mapper.Map<OrderDto, Order>(orderDto);

                var patient = GetPatient(order.PatientID);
                var procedures = _ProcedureRepository.Get(p => p.OrderID.Equals(order.UniqueID)).Select(p => Mapper.Map<Procedure, ProcedureDto>(p)).ToList();
                order.PatientType = orderUpdate.PatientType;
                order.ChargeType = orderUpdate.ChargeType;
                order.ApplyDept = orderUpdate.ApplyDept;
                order.ApplyDoctor = orderUpdate.ApplyDoctor;
                order.Observation = orderUpdate.Observation;
                order.HealthHistory = orderUpdate.HealthHistory;
                order.BedNo = string.IsNullOrEmpty(orderUpdate.BedNo) ? order.BedNo : orderUpdate.BedNo;
                order.InhospitalNo = string.IsNullOrEmpty(orderUpdate.InhospitalNo) ? order.InhospitalNo : orderUpdate.InhospitalNo;
                order.ClinicNo = string.IsNullOrEmpty(orderUpdate.ClinicNo) ? order.ClinicNo : orderUpdate.ClinicNo;
                order.Domain = string.IsNullOrEmpty(orderUpdate.Domain) ? order.Domain : orderUpdate.Domain;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                     new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    _OrderRepository.SaveChanges();
                    if (IsRegistrationView)
                    {
                        procedures.ForEach(p =>
                        {
                            _CommonService.WriteBroker(patient, Mapper.Map<Order, OrderDto>(order), p, null, null, "00", (int)Enums.ActionCode.Update, null);
                        });
                    }
                    ts.Complete();
                }
            }
        }

        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="orderDto"></param>
        public void UpdateOrder(string orderID, string examSite, string examDomain, string examAccNo)
        {
            var order = _OrderRepository.Get(o => o.UniqueID.Equals(orderID, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (order != null)
            {
                order.ExamSite = examSite;
                order.ExamDomain = examDomain;
                order.ExamAccNo = examAccNo;
                _OrderRepository.Update(order);
                _OrderRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Delete order
        /// </summary>
        /// <param name="orderId"></param>
        public void DeleteOrder(string orderId)
        {
            var order = _OrderRepository.Get(r => r.UniqueID.Equals(orderId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (order != null)
            {
                _OrderRepository.Delete(order);
                _OrderRepository.SaveChanges();
            }
        }

        /// <summary>
        /// Transfer registration
        /// </summary>
        /// <returns></returns>
        public RegistrationDto TransferRegistration(List<RegistrationDto> registrations, string domain, string userName, string site)
        {
            var returnRegistration = new RegistrationDto { Orders = new List<OrderDto>(), Procedures = new List<ProcedureDto>(), Requests = null };
            var id = string.Empty;
            registrations.ForEach(r =>
            {
                var result = new RegistrationDto();
                if (string.IsNullOrEmpty(id))
                {
                    result = AddNewRegistration(r, domain, userName, site);
                    id = result.Patient.UniqueID;
                }
                else
                {
                    r.Patient.UniqueID = id;
                    result = AddNewRegistration(r, domain, userName, site);
                }
                returnRegistration.Orders = returnRegistration.Orders.Concat(result.Orders).ToList();
                returnRegistration.Procedures = returnRegistration.Procedures.Concat(result.Procedures).ToList();
            });
            return returnRegistration;
        }

        /// <summary>
        /// Add new Registration
        /// </summary>
        public RegistrationDto AddNewRegistration(RegistrationDto registration, string domain, string userName, string site)
        {
            var order = registration.Orders.FirstOrDefault();
            var patient = registration.Patient;
            var procedures = registration.Procedures.ToList();
            var requisitions = registration.RequisitionFiles;
            var registrationResult = new RegistrationDto
            {
                Procedures = new List<ProcedureDto>(),
                Orders = new List<OrderDto>()
            };
            if (registration == null)
            {
                throw new ArgumentNullException();
            }

            if (patient == null)
            {
                throw new ArgumentException("Patient");
            }

            if (string.IsNullOrEmpty(patient.Domain))
            {
                throw new ArgumentNullException("Patient.Domain");
            }

            if (procedures == null)
            {
                throw new ArgumentNullException("Procedures");
            }

            if (order == null)
            {
                throw new ArgumentNullException("Order");
            }

            if (string.IsNullOrEmpty(patient.PatientNo))
            {
                throw new ArgumentNullException("PatientNo");
            }
            else
            {
                if (string.IsNullOrEmpty(patient.UniqueID) && CheckDuplicatePatientNo(patient.PatientNo))
                {
                    throw new Exception("Duplicate Patient No.");
                }
            }
            string eventType;
            // save patient or update patient
            var accnos = new List<string>();
            // update
            if (!string.IsNullOrEmpty(patient.UniqueID))
            {
                eventType = Contants.Broker.UpdatePatientEventType;
            }
            else
            {
                patient.UniqueID = Guid.NewGuid().ToString();
                patient.CreateTime = DateTime.Now;
                patient.UpdateTime = DateTime.Now;
                patient.IsUploaded = false;
                eventType = Contants.Broker.CreatePatientEventType;
            }
            //patient.SocialSecurityNo = "";
            registrationResult.Patient = patient;

            // save orders and procedures
            var modalityTypes = procedures.GroupBy(c => c.ModalityType, c => c, (key, g) => key);
            //order base info
            order.PatientID = patient.UniqueID;
            order.CreateTime = DateTime.Now;
            order.AgeInDays = GetAgeInDays(order.CreateTime, (DateTime)patient.Birthday);
            order.ExamDomain = order.InitialDomain = order.Domain;
            var procedureFirst = procedures.FirstOrDefault();
            // from booking
            if (procedureFirst != null && procedureFirst.Status.Equals((int)Enums.Status.Booking))
            {
                order.BookingSite = order.CurrentSite = site;
                var modality = _modalityRespository.Get(m => m.ModalityName.Equals(procedureFirst.Modality)).FirstOrDefault();
                if (modality != null && !string.IsNullOrEmpty(modality.Site) && modality.Site != site)
                {
                    order.Assign2Site = modality.Site;
                }
            }
            else
            {
                order.CurrentSite = order.ExamSite = order.RegSite = site;
            }
            order.IsScan = order.IsScan ?? false;
            order.IsEmergency = order.IsEmergency ?? false;
            order.IsReferral = order.IsReferral ?? false;
            order.IsCharge = order.IsCharge ?? false;
            // when transfer registration
            if (string.IsNullOrEmpty(order.CurrentAge))
            {
                order.CurrentAge = CalCurrentAge((DateTime)patient.Birthday);
            }
            foreach (var m in modalityTypes)
            {
                var newOrder = new OrderDto();
                var ts = newOrder.GetType();
                var us = order.GetType();
                // refrection set value
                foreach (var prop in ts.GetProperties())
                {
                    if (ts.GetProperties().Contains(prop))
                    {
                        var value = us.GetProperty(prop.Name).GetValue(order, null);
                        ts.GetProperty(prop.Name).SetValue(newOrder, value, null);
                    }
                }
                newOrder.UniqueID = Guid.NewGuid().ToString();
                newOrder.VisitID = Guid.NewGuid().ToString();// to be valified
                newOrder.ExamAccNo = newOrder.AccNo = GetAccNo(string.Empty, m, site);
                //AccNo validation
                if (string.IsNullOrEmpty(newOrder.AccNo))
                {
                    throw new ArgumentNullException("Accno can not be null.");
                }
                if (CheckDuplicateAccNo(newOrder.AccNo))
                {
                    throw new Exception("Duplicate AccNo.");
                }
                if (!accnos.Contains(newOrder.AccNo))
                {
                    accnos.Add(newOrder.AccNo);
                }
                else
                {
                    throw new Exception("Different order can not have the same AccNo.");
                }
                var strStudyInstanceUID = GenerateStudyInstance.GetDicomGUID(System.Diagnostics.Process.GetCurrentProcess().Id.ToString());
                newOrder.StudyInstanceUID = strStudyInstanceUID;
                var proceduresAdd = procedures.Where(p => p.ModalityType == m).ToList();
                var totalFee = newOrder.TotalFee ?? proceduresAdd.Sum(p => p.Charge ?? 0);
                newOrder.TotalFee = totalFee;
                var warningTime = GetWarningTime(newOrder.RegSite, m, newOrder.PatientType);
                registrationResult.Orders.Add(newOrder);
                //procedure
                foreach (var p in proceduresAdd)
                {
                    p.UniqueID = Guid.NewGuid().ToString();
                    p.OrderID = newOrder.UniqueID;
                    p.WarningTime = warningTime;
                    // default value
                    p.IsExistImage = false;
                    p.IsCharge = p.IsCharge ?? false;
                    p.Optional1 = p.Optional1 ?? "0";
                    p.Optional3 = p.Optional3 ?? "0";
                    p.FilmCount = p.FilmCount ?? 0;
                    p.ImageCount = p.ImageCount ?? 0;
                    p.ExposalCount = p.ExposalCount ?? 0;
                    p.Charge = p.Charge ?? 0;
                    if (p.Status.Equals((int)Enums.Status.Booking))
                    {
                        p.RegisterTime = null;
                        p.IsPost = 100;//default value for schedule splice in smart client in booking module
                        p.UpdateTime = DateTime.Now;
                    }
                    else
                    {
                        p.RegisterTime = DateTime.Now;
                        p.IsPost = 0;//default value for schedule splice in smart client in registration module
                    }
                    p.CreateTime = DateTime.Now;

                    registrationResult.Procedures.Add(p);
                }
            }

            using (var ts = new TransactionScope(TransactionScopeOption.Required,
                 new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                if (eventType == Contants.Broker.CreatePatientEventType)
                {
                    AddPatient(patient);
                }
                else
                {
                    UpdatePatient(patient);
                }
                var processAccNoDics = new Dictionary<string, string>();
                registrationResult.Orders.ForEach(o =>
                {
                    AddOrder(o);

                    var proceduresAdd = registrationResult.Procedures.Where(p => p.OrderID.Equals(o.UniqueID)).ToList();
                    proceduresAdd.ForEach(p =>
                    {
                        AddProcedure(p);
                        processAccNoDics[p.ProcedureCode] = o.AccNo;
                    });
                });
                //request handle
                if (registration.Requests != null)
                {
                    bool sucess = ProcessReuqestInfo(registration.Requests.ToList(), patient.UniqueID, domain, site, processAccNoDics);
                    if (!sucess)
                    {
                        ts.Dispose();
                        throw new ArgumentNullException();
                    }
                }
                //requisition handle
                if (requisitions != null && requisitions.RequisitionFiles.Count() != 0)
                {
                    var result = ProcessRequisitionFiles(accnos, requisitions.ErNo, requisitions.ImageQualityLevel, domain, userName);
                    if (!result.Equals(-0))
                    {
                        ts.Dispose();
                        throw new ArgumentNullException();
                    }
                }
                // for patient broker
                _CommonService.WriteBroker(patient, order, null, null, eventType, "", 0, "", true);
                registrationResult.Orders.ToList().ForEach(o =>
                {
                    var proceduresAdd = registrationResult.Procedures.Where(p => p.OrderID.Equals(o.UniqueID)).ToList();
                    proceduresAdd.ForEach(p =>
                    {
                        if (p.Status.Equals((int)Enums.Status.Booking))
                        {   // booking
                            _CommonService.WriteBroker(patient, o, p, null, null, null, (int)Enums.ActionCode.BookingCreate, null);
                        }
                        else
                        {  //registration
                            _CommonService.WriteBroker(patient, o, p, null, null, null, (int)Enums.ActionCode.Create, null);
                        }
                    });
                });
                // SAVE DATA
                ts.Complete();
            }
            return registrationResult;
        }

        /// <summary>
        /// Process Request info,TRANSFER OR REJECT
        /// </summary>
        public bool ProcessReuqestInfo(List<RequestDto> requests, string domain, string site, string patientID = null, Dictionary<string, string> dics = null)
        {
            try
            {
                var rejectEvent = "17";
                var rejectStatus = "rejected";
                // determine weather the request has been rejected or not in new operation
                var rejectFlag = requests.Any(r => (r.RequestItems != null && r.RequestItems.Count() > 0) ? r.RequestItems.FirstOrDefault().Status.Equals(rejectStatus, StringComparison.OrdinalIgnoreCase) : false);
                requests.ForEach(p =>
                {
                    //old status of request in database
                    var isRejectedRequest = false;
                    // ErNo exist
                    p.RISPatientID = patientID;
                    p.Domain = domain;
                    p.Site = site;
                    var remoteRPIDs = new List<string>();
                    if (!string.IsNullOrEmpty(p.UniqueID))
                    {
                        var oldRequest = GetRequestWithErNo(p.ErNo);
                        isRejectedRequest = oldRequest.RequestItems.Where(i => i.Status.Equals(rejectStatus, StringComparison.OrdinalIgnoreCase)).ToList().Count() > 0;
                        if (isRejectedRequest)
                        {
                            AddRequestList(oldRequest);
                            oldRequest.RequestItems.ForEach(o => DeleteRequestItem(o));
                        }
                        UpdateRequest(p);
                    }
                    else
                    {
                        p.UniqueID = Guid.NewGuid().ToString();
                        AddRequest(p);
                    }
                    if (p.RequestItems != null)
                    {
                        p.RequestItems.ForEach(r =>
                        {
                            // get accNo when the interface from  new registration module
                            if (dics != null)
                            {
                                if (dics.ContainsKey(r.ProcedureCode))
                                {
                                    r.AccNo = dics[r.ProcedureCode];
                                }
                            }
                            if (string.IsNullOrEmpty(r.UniqueID))
                            {
                                r.UniqueID = Guid.NewGuid().ToString();
                                r.RequestID = p.UniqueID;
                                AddRequestItem(r);
                            }
                            else
                            {
                                UpdateRequestItem(r);
                            }
                            if (r.ChargeItems != null)
                            {
                                r.ChargeItems.ForEach(c =>
                                {
                                    if (string.IsNullOrEmpty(c.UniqueID))
                                    {
                                        c.UniqueID = Guid.NewGuid().ToString();
                                        c.RequestID = p.UniqueID;
                                        c.RequestItemID = r.UniqueID;
                                        AddRequestCharge(c);
                                    }
                                    else
                                    {
                                        UpdateRequestCharge(c);
                                    }
                                });
                            }
                            // Broler index4
                            if (rejectFlag && !string.IsNullOrEmpty(r.RemoteRPID))
                            {
                                remoteRPIDs.Add(r.RemoteRPID);
                            }
                        });
                    }

                    if (rejectFlag)
                    {
                        SendRejectBroker(rejectEvent, p.Reason, p.Comments, p.ErNo, string.Join("|", remoteRPIDs.ToArray()));
                    }
                });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accNos"></param>
        /// <param name="erNo"></param>
        /// <param name="imageQualityLevel"></param>
        /// <returns></returns>
        public int ProcessRequisitionFiles(List<string> accNos, string erNo, string imageQualityLevel, string domain, string userName)
        {
            var relativePath = Path.Combine(domain, "Requisition", DateTime.Now.ToString("yyyy-MM-dd"), erNo);
            relativePath = relativePath.Replace('\\', '/');
            return UploadRequisitionFile(accNos, erNo, relativePath, imageQualityLevel, userName, domain);
        }

        /// <summary>
        /// Send Broker when reject request
        /// </summary>
        private void SendRejectBroker(string eventType, string index1, string index2, string index3, string index4)
        {
            GWDataIndex data = new GWDataIndex
            {
                UniqueID = Guid.NewGuid().ToString(),
                DataTime = DateTime.Now.ToLocalTime(),
                EventType = eventType,
                RecordIndex1 = index1,
                RecordIndex2 = index2,
                RecordIndex3 = index3,
                RecordIndex4 = index4,
            };

            _GWDataIndexRepository.Add(data);
            _GWDataIndexRepository.SaveChanges();
        }
        /// <summary>
        /// Get AccNo 
        /// </summary>
        /// <param name="locationAccnoPrefix"></param>
        /// <param name="modalityType"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        private string GetAccNo(string locationAccnoPrefix, string modalityType, string site)
        {
            var accNo = string.Empty;
            RisDBService.GenerateAccNo(locationAccnoPrefix, modalityType, site, ref accNo);
            return accNo;
        }

        /// <summary>
        /// Get PatientNo
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public string GetPatientNo(string site)
        {
            var patientNo = string.Empty;
            RisDBService.GeneratePatientID(site, ref patientNo);
            return patientNo;
        }

        public RegistrationViewDto GetRegistrationInfo(string orderId)
        {
            var procedures = from p in _risProContext.Set<Procedure>()
                             join o in _risProContext.Set<Order>() on p.OrderID equals o.UniqueID
                             join u in _risProContext.Set<User>() on p.Registrar equals u.UniqueID into users
                             from u in users.DefaultIfEmpty()
                             join r in _risProContext.Set<Report>() on p.ReportID equals r.UniqueID into reports
                             from r in reports.DefaultIfEmpty()
                             where o.UniqueID.Equals(orderId)
                             orderby p.CreateTime descending
                             select new
                             {
                                 p.UniqueID,
                                 p.OrderID,
                                 p.CreateTime,
                                 p.RegisterTime,
                                 p.ExamineTime,
                                 p.ProcedureCode,
                                 p.CheckingItem,
                                 p.ExamSystem,
                                 p.BodyCategory,
                                 p.BodyPart,
                                 p.Modality,
                                 p.ModalityType,
                                 p.Status,
                                 p.RPDesc,
                                 p.BookingBeginTime,
                                 p.BookingEndTime,
                                 p.BookingTimeAlias,
                                 RegistrarName = u.LocalName,
                                 ReportID = r.UniqueID,
                                 r.WYGText,
                                 r.WYSText
                             };
            var registration = (from o in _risProContext.Set<Order>()
                                join p in _risProContext.Set<Patient>() on o.PatientID equals p.UniqueID
                                where o.UniqueID == orderId
                                select new
                                {
                                    Patient = p,
                                    OrderId = o.UniqueID,
                                    o.AccNo,
                                    o.PatientType,
                                    o.ApplyDept,
                                    o.ApplyDoctor,
                                    o.CreateTime,
                                    o.ChargeType,
                                    o.HealthHistory,
                                    o.Observation,
                                    o.CurrentAge,
                                    o.RegSite,
                                    o.CurrentSite,
                                    o.ExamSite,
                                    o.AgeInDays,
                                    o.IsScan,
                                    Procedures = procedures
                                }).Take(1).FirstOrDefault();

            if (registration != null)
            {
                var result = new RegistrationLiteDto
                {
                    Patient = Mapper.Map<Patient, PatientDto>(registration.Patient),
                    Order = new OrderLiteDto
                    {
                        UniqueID = registration.OrderId,
                        PatientID = registration.Patient.UniqueID,
                        AccNo = registration.AccNo,
                        PatientType = registration.PatientType,
                        CurrentSite = registration.CurrentSite,
                        ExamSite = registration.ExamSite,
                        ApplyDept = registration.ApplyDept,
                        ApplyDoctor = registration.ApplyDoctor,
                        CreateTime = registration.CreateTime.Value,
                        ChargeType = registration.ChargeType,
                        HealthHistory = registration.HealthHistory,
                        Observation = registration.Observation,
                        CurrentAge = registration.CurrentAge,
                        AgeInDays = registration.AgeInDays,
                        IsScan = registration.IsScan == 1
                    },
                    Procedures = registration.Procedures.ToList().Select(p => new ProcedureLiteDto
                    {
                        UniqueID = p.UniqueID,
                        OrderID = registration.OrderId,
                        ReportID = p.ReportID,
                        Status = p.Status,
                        ProcedureCode = p.ProcedureCode,
                        CheckingItem = p.CheckingItem,
                        RPDesc = p.RPDesc,
                        ExamSystem = p.ExamSystem,
                        ModalityType = p.ModalityType,
                        Modality = p.Modality,
                        BodyCategory = p.BodyCategory,
                        BodyPart = p.BodyPart,
                        RegistrarName = p.RegistrarName,
                        RegisterTime = p.RegisterTime,
                        BookingBeginTime = p.BookingBeginTime,
                        BookingEndTime = p.BookingEndTime,
                        BookingTimeAlias = p.BookingTimeAlias,
                        WYGText = p.WYGText,
                        WYSText = p.WYSText,
                    }).ToList()
                };

                var procedureItems = procedures.Select(p => new ProcedureItemDto
                {
                    ProcedureID = p.UniqueID,
                    Status = p.Status,
                    ModalityType = p.ModalityType,
                    RPDesc = p.RPDesc,
                    Modality = p.Modality,
                    ReportID = p.ReportID,
                    ExamSystem = p.ExamSystem
                });
                var item = new OrderItemDto
                {
                    PatientID = result.Patient.UniqueID,
                    Birthday = result.Patient.Birthday,
                    PatientName = result.Patient.LocalName,
                    PatientNo = result.Patient.PatientNo,
                    OrderID = result.Order.UniqueID,
                    AccNo = result.Order.AccNo,
                    PatientType = result.Order.PatientType,
                    CurrentSite = result.Order.CurrentSite,
                    ExamSite = result.Order.ExamSite,
                    CreatedTime = result.Order.CreateTime,
                    CurrentAge = result.Order.CurrentAge,
                    AgeInDays = result.Order.AgeInDays,
                    Procedures = procedureItems
                };
                return new RegistrationViewDto { Registration = result, OrderItem = item };
            }
            return null;
        }

        /// <summary>
        /// Transfer booking status to registered
        /// </summary>
        /// <param name="orderId"></param>
        public void TransferBooking2Registration(string orderId, string userId, string userName)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                              new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    var procedures = _ProcedureRepository.Get(p => p.OrderID.Equals(orderId)).ToList();
                    var order = GetOrder(orderId);
                    var patient = GetPatient(order.PatientID);
                    procedures.ForEach(p =>
                    {
                        if (p.Status.Equals((int)Enums.Status.Booking))
                        {
                            p.Status = (int)Enums.Status.Registered;
                            p.Registrar = userId;
                            p.RegistrarName = userName;
                            p.RegisterTime = DateTime.Now;
                            p.UpdateTime = DateTime.Now;
                        }
                        _ProcedureRepository.SaveChanges();
                        _CommonService.WriteBroker(patient, order, Mapper.Map<ProcedureDto>(p), null, null, null, (int)Enums.ActionCode.BookingToReg, null);
                    });
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// validate wheather patient no is duplicate or not
        /// </summary>
        /// <param name="ptientId"></param>
        /// <returns></returns>
        private bool CheckDuplicatePatientNo(string patientNo)
        {
            var patient = GetPatientByNo(patientNo);
            if (patient != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检测AccNo是否重复
        /// </summary>
        /// <param name="accNo"></param>
        /// <returns></returns>
        private bool CheckDuplicateAccNo(string accNo)
        {
            var accessionNumberList = GetAccessionNumberList(accNo);
            if (accessionNumberList != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// get AgeInDays based on birthday
        /// 1.if the order is exit，based on registrated time
        /// 2.if the order is new ,based on current create time
        /// </summary>
        /// <returns></returns>
        private int GetAgeInDays(DateTime? createDt, DateTime birthday)
        {
            if (birthday == null)
            {
                return 0;
            }
            var nowDate = createDt.HasValue ? createDt.Value : DateTime.Now;
            var timeSpan = nowDate - birthday;
            return timeSpan.Days;
        }
        #endregion

        #region Procedure Operation Service
        /// <summary>
        /// Get all procedures
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProcedureDto> GetAllProcedures()
        {
            var procedures = _ProcedureRepository.Get().Select(r => Mapper.Map<Procedure, ProcedureDto>(r)).ToList();
            return procedures;
        }

        /// <summary>
        /// Get procedures via order id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void FinishExam(string orderID, string examSite, string examDomain, string examAccNo, string userId, string userName)
        {
            //update order
            var procedures = _ProcedureRepository.Get(p => p.OrderID.Equals(orderID) && p.Status < 50);
            if (procedures.Count() == 0)
            {
                return;
            }

            //prepare data
            var order = Mapper.Map<Order, OrderDto>(_OrderRepository.Get(o => o.UniqueID == orderID).FirstOrDefault());
            order.ExamSite = examSite;
            order.ExamDomain = examDomain;
            order.ExamAccNo = examAccNo;

            List<ProcedureDto> procedureDtoList = procedures.Select(s => Mapper.Map<Procedure, ProcedureDto>(s)).ToList();
            procedureDtoList.ForEach(p =>
            {
                p.ExamineTime = DateTime.Now;
            });

            var patient = Mapper.Map<Patient, PatientDto>(_PatientRepository.Get(o => o.UniqueID == order.PatientID).FirstOrDefault());

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                 new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
            {
                UpdateOrder(orderID, examSite, examDomain, examAccNo);
                //update procedure's tatus 
                UpdateProcedure(orderID, userId, userName);

                procedureDtoList.ForEach(p =>
                {
                    _CommonService.WriteBroker(patient, order, p, null, Contants.Broker.FinishExamEventType, Contants.Broker.FinishExamStatus, (int)Enums.ActionCode.FinishExam, null);
                });

                // SAVE DATA
                ts.Complete();
            }
        }

        /// <summary>
        /// Get procedure by Id
        /// </summary>
        /// <param name="procedureId"></param>
        /// <returns></returns>
        public ProcedureDto GetProcedure(string procedureId)
        {
            var procedure = _ProcedureRepository.Get(r => r.UniqueID.Equals(procedureId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (procedure != null)
            {
                return Mapper.Map<Procedure, ProcedureDto>(procedure);
            }
            return null;
        }

        /// <summary>
        /// Get procedure by orderId
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public IQueryable GetProceduresByOrderID(string orderID)
        {
            var procedures = from p in _risProContext.Set<Procedure>()
                             join o in _risProContext.Set<Order>() on p.OrderID equals o.UniqueID
                             join u in _risProContext.Set<User>() on p.Registrar equals u.UniqueID into users
                             from u in users.DefaultIfEmpty()
                             join r in _risProContext.Set<Report>() on p.ReportID equals r.UniqueID into reports
                             from r in reports.DefaultIfEmpty()
                             where o.UniqueID.Equals(orderID)
                             orderby p.CreateTime descending
                             select new
                             {
                                 p.UniqueID,
                                 p.OrderID,
                                 p.CreateTime,
                                 p.RegisterTime,
                                 p.ExamineTime,
                                 p.ProcedureCode,
                                 p.CheckingItem,
                                 p.ExamSystem,
                                 p.BodyCategory,
                                 p.BodyPart,
                                 p.Modality,
                                 p.ModalityType,
                                 p.Status,
                                 p.RPDesc,
                                 RegistrarName = u.LocalName,
                                 ReportID = r.UniqueID,
                                 r.WYGText,
                                 r.WYSText
                             };
            return procedures;
        }

        public IEnumerable<ProcedureCodeDto> GetProcedureCodes(string site)
        {
            var procedurecodes = _ProcedureCodeRepository.Get(p =>
                    (p.Site == site || string.IsNullOrEmpty(p.Site)) &&
                    p.Effective == 1
                ).ToList();

            var result = Mapper.Map<List<ProcedureCodeDto>>(procedurecodes);
            return result;
        }

        /// <summary>
        /// Get procedurecode by code
        /// </summary>
        /// <returns></returns>
        public ProcedureDto GetProcedureByCode(string code, string modality, string domain)
        {
            var procedureCode = _ProcedureCodeRepository.Get(p => p.ProcedureCode.Equals(code)).Select(p => Mapper.Map<ProcedureCodeDto>(p)).FirstOrDefault();
            var procedure = new ProcedureDto();
            if (procedureCode != null)
            {
                procedure.ProcedureCode = procedureCode.ProcedureCode;
                procedure.ModalityType = procedureCode.ModalityType;
                procedure.BodyCategory = procedureCode.BodyCategory;
                procedure.BodyPart = procedureCode.BodyPart;
                procedure.CheckingItem = procedureCode.CheckingItem;
                procedure.ModalityType = procedure.ModalityType;
                procedure.FilmCount = procedureCode.FilmCount;
                procedure.FilmSpec = procedureCode.FilmSpec;
                procedure.ContrastName = procedureCode.ContrastName;
                procedure.ContrastDose = procedureCode.ContrastDose;
                procedure.ImageCount = procedureCode.ImageCount;
                procedure.ExposalCount = procedureCode.ExposalCount;
                procedure.Charge = procedureCode.Charge;
                procedure.BookingNotice = procedureCode.BookingNotice;
                procedure.RPDesc = procedureCode.Description;
                procedure.Domain = domain;
                var system = _bodySystemMapRepository.Get(m => m.BodyPart.Equals(procedureCode.BodyPart)).FirstOrDefault();
                procedure.ExamSystem = system == null ? null : system.ExamSystem;
                if (!string.IsNullOrEmpty(modality))
                {
                    var existModality = _modalityRespository.Get(m => m.ModalityName.Equals(modality) && m.ModalityType.Equals(procedureCode.ModalityType)).FirstOrDefault();
                    if (existModality != null)
                    {
                        procedure.Modality = modality;
                    }
                }
                if (string.IsNullOrEmpty(procedure.Modality))
                {
                    if (string.IsNullOrEmpty(procedureCode.DefaultModality))
                    {
                        var existModality = _modalityRespository.Get(t => t.ModalityType.Equals(procedureCode.ModalityType)).FirstOrDefault();
                        procedure.Modality = existModality == null ? null : existModality.ModalityName;
                    }
                    else
                    {
                        procedure.Modality = procedureCode.DefaultModality;
                    }
                }
                return procedure;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Add new procedure 
        /// </summary>
        /// <param name="procedureDto"></param>
        /// case1: add procedure when creating new order
        public void AddProcedure(ProcedureDto procedureDto)
        {
            if (procedureDto != null)
            {
                _ProcedureRepository.Add(Mapper.Map<ProcedureDto, Procedure>(procedureDto));
                _ProcedureRepository.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Add new procedure 
        /// </summary>
        /// <param name="procedureDto"></param>
        /// case1: add procedure when creating new order
        /// new booking 
        public List<ProcedureDto> AddProcedure(ProcedureInsertDto procedureInsert)
        {
            if (procedureInsert.Procedures != null && !string.IsNullOrEmpty(procedureInsert.OrderID))
            {
                var order = GetOrder(procedureInsert.OrderID);
                var patient = GetPatient(order.PatientID);
                procedureInsert.Procedures.ForEach(p =>
                {
                    // default value
                    p.UniqueID = Guid.NewGuid().ToString();
                    p.IsCharge = false;
                    p.IsExistImage = false;
                    p.Optional1 = "0";
                    p.Optional3 = "0";
                    p.RPDesc = p.CheckingItem;
                    if (p.Status.Equals((int)Enums.Status.Booking))
                    {
                        p.RegisterTime = null;
                        p.IsPost = 100;//default value for schedule splice in smart client in booking module
                        p.UpdateTime = DateTime.Now;
                    }
                    else
                    {
                        p.RegisterTime = DateTime.Now;
                    }
                    p.CreateTime = DateTime.Now;
                    p.OrderID = order.UniqueID;
                    p.WarningTime = GetWarningTime(order.RegSite, p.ModalityType, order.PatientType);
                    if (p.Status.Equals((int)Enums.Status.Examined))
                    {
                        p.ExamineTime = DateTime.Now;
                    }
                    _ProcedureRepository.Add(Mapper.Map<ProcedureDto, Procedure>(p));
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                        new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                    {
                        _ProcedureRepository.SaveChanges();
                        if (p.Status.Equals((int)Enums.Status.Booking))
                        {   // booking
                            _CommonService.WriteBroker(patient, order, p, null, null, null, (int)Enums.ActionCode.BookingCreate, null);
                        }
                        else
                        {  //registration
                            _CommonService.WriteBroker(patient, order, p, null, null, null, (int)Enums.ActionCode.Create, null);
                        }
                        ts.Complete();
                    }
                }
            );
                return _ProcedureRepository.Get(p => p.OrderID.Equals(procedureInsert.OrderID))
                    .Select(p => Mapper.Map<Procedure, ProcedureDto>(p)).ToList();
            }
            else
            {
                throw new ArgumentNullException("Procedure or order is null.");
            }
        }


        /// <summary>
        /// Update procedure
        /// </summary>
        /// <param name="procedureDto"></param>
        public void UpdateProcedure(ProcedureDto procedureDto)
        {
            _ProcedureRepository.Update(Mapper.Map<ProcedureDto, Procedure>(procedureDto));
            _ProcedureRepository.SaveChanges();
        }

        /// <summary>
        /// Update procedure
        /// case2:update procedure in order edit
        /// </summary>
        /// <param name="procedureDto"></param>
        public ProcedureDto UpdateProcedure(string id, ProcedureDto procedureUpdate, string userId)
        {
            var oldProcedure = _ProcedureRepository.Get(r => r.UniqueID.Equals(id, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (procedureUpdate != null && !string.IsNullOrEmpty(procedureUpdate.OrderID) && oldProcedure != null)
            {

                var order = GetOrder(procedureUpdate.OrderID);
                var patient = GetPatient(order.PatientID);

                oldProcedure.RPDesc = oldProcedure.CheckingItem = procedureUpdate.CheckingItem;
                oldProcedure.WarningTime = GetWarningTime(order.RegSite, procedureUpdate.ModalityType, order.PatientType);
                oldProcedure.ModifyTime = DateTime.Now;
                oldProcedure.ProcedureCode = procedureUpdate.ProcedureCode;
                oldProcedure.FilmCount = procedureUpdate.FilmCount;
                oldProcedure.FilmSpec = procedureUpdate.FilmSpec;
                oldProcedure.ImageCount = procedureUpdate.ImageCount;
                oldProcedure.ExamSystem = procedureUpdate.ExamSystem;
                oldProcedure.ContrastName = procedureUpdate.ContrastName;
                oldProcedure.ExposalCount = procedureUpdate.ExposalCount;
                oldProcedure.Deposit = procedureUpdate.Deposit;
                oldProcedure.Charge = procedureUpdate.Charge;
                oldProcedure.Modality = procedureUpdate.Modality;
                oldProcedure.Mender = userId;
                oldProcedure.BodyCategory = procedureUpdate.BodyCategory;
                oldProcedure.BodyPart = procedureUpdate.BodyPart;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    _ProcedureRepository.SaveChanges();
                    if (oldProcedure.Status.Equals((int)Enums.Status.Booking))
                    {
                        _CommonService.WriteBroker(patient, order, Mapper.Map<Procedure, ProcedureDto>(oldProcedure), null, null, null, (int)Enums.ActionCode.BookingUpdate, null);
                    }
                    else
                    {
                        _CommonService.WriteBroker(patient, order, Mapper.Map<Procedure, ProcedureDto>(oldProcedure), null, null, null, (int)Enums.ActionCode.Update, null);
                    }
                    ts.Complete();
                }
                return _ProcedureRepository.Get(p => p.UniqueID.Equals(oldProcedure.UniqueID))
                    .Select(p => Mapper.Map<Procedure, ProcedureDto>(p)).FirstOrDefault();
            }
            else
            {
                throw new ArgumentNullException("Procedure or order is null.");
            }
        }

        /// <summary>
        /// Update procedure
        /// </summary>
        /// <param name="procedureID"></param>
        /// <param name="status"></param>
        /// <param name="reportID"></param>
        public void UpdateProcedure(string procedureID, int status, string reportID)
        {
            var procedure = _ProcedureRepository.Get(p => p.UniqueID == procedureID).FirstOrDefault();
            procedure.Status = status;
            procedure.ReportID = reportID;
            _ProcedureRepository.Update(procedure);
            _ProcedureRepository.SaveChanges();
        }

        /// <summary>
        /// Update procedure
        /// </summary>
        /// <param name="procedureID"></param>
        /// <param name="status"></param>
        /// <param name="reportID"></param>
        public void UpdateProcedure(string orderID, string userId, string userName)
        {
            var query = _ProcedureRepository.Get(p => p.OrderID.Equals(orderID)).Where(p => p.Status < 50).Select(p => p).ToList();
            query.ForEach(p =>
            {
                if (p.Status == 10)
                {
                    p.RegisterTime = DateTime.Now;
                    p.RegistrarName = userName;
                    p.Registrar = userId;
                }
                p.Status = 50;
                p.ExamineTime = DateTime.Now;
            });
            _ProcedureRepository.SaveChanges();
        }

        /// <summary>
        /// Update procedure
        /// </summary>
        /// <param name="procedureID"></param>
        /// <param name="status"></param>
        /// <param name="reportID"></param>
        public void UpdateProcedure(string procedureID, int status, DateTime examineTime)
        {
            var procedure = _ProcedureRepository.Get(p => p.UniqueID == procedureID).FirstOrDefault();
            procedure.Status = status;
            procedure.ExamineTime = examineTime;
            _ProcedureRepository.Update(procedure);
            _ProcedureRepository.SaveChanges();
        }

        /// <summary>
        /// Delete procedure
        /// 0:success
        /// -1:has report
        /// -2:the last item can not be deleted
        /// -3:has been deleted
        /// </summary>
        /// <param name="procedureId"></param>
        /// <param name="loginUserID"></param>
        /// <param name="isFromRegistrationView"></param>
        /// <returns></returns>
        public int DeleteProcedure(string procedureId, string loginUserID, bool isFromRegistrationView = false)
        {
            var procedure = _ProcedureRepository.Get(r => r.UniqueID.Equals(procedureId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (procedure != null)
            {
                if (procedure.Status > 50)
                {
                    return (int)Enums.DelProcedureError.ExamCanNotDel;
                }
                var order = GetOrder(procedure.OrderID);
                var procedures = _ProcedureRepository.Get(p => p.OrderID.Equals(procedure.OrderID));
                if (procedures.Count() < 2)
                {
                    return (int)Enums.DelProcedureError.OnlyOneCanNotDel;
                }

                var patient = GetPatient(order.PatientID);
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    _ProcedureRepository.Delete(procedure);
                    if (isFromRegistrationView)
                    {
                        if (procedure.Status.Equals((int)Enums.Status.Booking))
                        {
                            _CommonService.WriteBroker(patient, order, Mapper.Map<Procedure, ProcedureDto>(procedure), null, null, null, (int)Enums.ActionCode.BookingDelete, null, false, false, loginUserID);
                        }
                        else
                        {
                            _CommonService.WriteBroker(patient, order, Mapper.Map<Procedure, ProcedureDto>(procedure), null, null, null, (int)Enums.ActionCode.Delete, null, false, false, loginUserID);
                        }
                    }
                    _ProcedureRepository.SaveChanges();
                    ts.Complete();
                    return 0;
                }
            }
            else
            {
                // has been deleted
                return (int)Enums.DelProcedureError.HasBeenDeleted;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="slice"></param>
        /// <returns></returns>
        public void UpdateProcedureSlice(string orderId, SliceDto slice)
        {
            var order = GetOrder(orderId);
            var patient = GetPatient(order.PatientID);
            var procedures = _ProcedureRepository.Get(p => p.OrderID.Equals(orderId));
            var procedureList = procedures as List<Procedure> ?? procedures.ToList();
            if (procedures != null && procedureList.Any())
            {
                using (var ts = new TransactionScope(
                      TransactionScopeOption.Required,
                      new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }))
                {
                    procedureList.ForEach(p =>
                    {
                        p.BookingBeginTime = slice.BookingBeginTime;
                        p.BookingEndTime = slice.BookingEndTime;
                        p.BookingTimeAlias = slice.BookingTimeAlias;
                        p.Modality = slice.Modality;
                        _ProcedureRepository.SaveChanges();
                        if (p.Status.Equals((int)Enums.Status.Booking))
                        {   // booking
                            _CommonService.WriteBroker(patient, order, Mapper.Map<ProcedureDto>(p), null, null, null, (int)Enums.ActionCode.BookingUpdate, null);
                        }
                    });
                    ts.Complete();
                }
            }
        }
        #endregion

        // AccessionNumberList
        public AccessionNumberListDto GetAccessionNumberList(string accNo)
        {
            var accessionNumberList = _accessionNumberListRepository.Get(p => p.AccNo.Equals(accNo, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (accessionNumberList != null)
            {
                return Mapper.Map<AccessionNumberList, AccessionNumberListDto>(accessionNumberList);
            }
            return null;
        }

        /// <summary>
        /// Get user's localname+loginname
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetUserName(string id)
        {
            var user = _userRepository.Get(u => u.UniqueID.Equals(id, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            return user.LocalName + ";" + user.LocalName;
        }

        public string GetRequisitionUrl(string accNo, string modalityType, string genPDFServiceURL)
        {
            var url = RisDBService.GetPDFURL(genPDFServiceURL, "2", accNo, modalityType);
            return url;
        }

        public string GetBarCodeUrl(string accNo, string modalityType, string genPDFServiceURL)
        {
            var url = RisDBService.GetPDFURL(genPDFServiceURL, "5", accNo, modalityType);
            return url;
        }

        /// <summary>
        /// Get body system map by site
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BodySystemMapDto> GetBodySystemMaps(string site)
        {
            var data = _bodySystemMapRepository.Get(p => p.Site.Equals(site) || string.IsNullOrEmpty(p.Site))
                .Select(r => Mapper.Map<BodySystemMap, BodySystemMapDto>(r)).ToList();
            //if (data.Count() == 0)
            //{
            //    data = _bodySystemMapRepository.Get(p => string.IsNullOrEmpty(p.Site))
            //    .Select(r => Mapper.Map<BodySystemMap, BodySystemMapDto>(r)).ToList();
            //}
            return data;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetWarningTime(string site, string modalityType, string patientType)
        {
            int warningTime = 0;
            var temp = _risProContext.Set<WarningTime>().Where(wt => wt.Site.Equals(site, StringComparison.OrdinalIgnoreCase) &&
                wt.ModalityType.Equals(modalityType, StringComparison.OrdinalIgnoreCase) &&
                wt.PatientType.Equals(patientType, StringComparison.OrdinalIgnoreCase))
                .Select(w => w.WarningTimeValue).ToList();
            if (temp.Count > 0)
            {
                warningTime = Convert.ToInt32(temp[0]);
            }
            else
            {
                var temp1 = _risProContext.Set<SystemProfile>().Where(sp => sp.Name == "DefaultWarningTime").Select(s => s.Value).ToList();
                if (temp1.Count > 0)
                {
                    warningTime = Convert.ToInt32(temp1[0]);
                }
            }
            return warningTime;

        }

        public string SimplifiedToEnglish(SimplifyEnglishDto simplify)
        {
            simplify.LocalName = simplify.LocalName.Trim();
            string strEnglish = PinyinUtil.ToPinyin(simplify.LocalName, simplify.UpperFirstLetter, simplify.SeparatePolicy, simplify.Separator);
            return strEnglish;
        }

        #region Intergration Operation Service

        /// <summary>
        /// Get request info via intergration service
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="cardType"></param>
        /// <param name="hisconnURL"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        public async Task<RequestInfoDto> GetRequestInfo(string cardNumber, string cardType, string hisconnURL, string domain, string site)
        {
            var input = new Hys.CareRIS.Application.HISConService.GetRequestInfoBodyInputModel();
            input.CardNumber = cardNumber;
            input.CardType = cardType;
            input.OperatorLocalName = "";
            input.OperatorLoginName = "";
            input.TerminalName = "";

            Hys.CareRIS.Application.HISConService.ReturnModel returnModel = new Hys.CareRIS.Application.HISConService.ReturnModel();
            Hys.CareRIS.Application.HISConService.InputModel inputModel = new Hys.CareRIS.Application.HISConService.InputModel();
            Hys.CareRIS.Application.HISConService.GetRequestInfoBodyReturnModel output = null;

            inputModel.MessageID = "GetRequestInfo";
            inputModel.MessageBodyFormat = "JSON";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            inputModel.MessageBody = serializer.Serialize(input);

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(hisconnURL);
            Hys.CareRIS.Application.HISConService.HISConServiceSoapClient hisCon = new Hys.CareRIS.Application.HISConService.HISConServiceSoapClient(binding, endpoint);
            hisCon.DoCommand("RISGC", inputModel, ref returnModel);
            if (returnModel.CalledResult == 0)
            {
                output = serializer.Deserialize(returnModel.ResultBody, typeof(Hys.CareRIS.Application.HISConService.GetRequestInfoBodyReturnModel)) as Hys.CareRIS.Application.HISConService.GetRequestInfoBodyReturnModel;
            }
            var result = new RequestInfoDto();
            result.IsHasNoRequestItem = true;
            if (output == null || output.PatientInfo == null)
            {
                throw new Exception("获取检查申请信息失败.");
            }

            if (output != null)
            {
                var patient = new PatientDto();
                patient.PatientNo = output.PatientInfo.PatientID;
                patient.LocalName = output.PatientInfo.LocalName;
                patient.GlobalID = output.PatientInfo.GlobalID;
                patient.Birthday = output.PatientInfo.BirthDate.ToLocalTime();
                patient.Gender = output.PatientInfo.Gender;
                patient.Address = output.PatientInfo.Address;
                patient.Telephone = output.PatientInfo.Telephone;
                patient.ReferenceNo = output.PatientInfo.ReferenceNo;
                patient.IsVip = output.PatientInfo.IsVIP == "1" ? true : false;
                patient.SocialSecurityNo = output.PatientInfo.SocialSecurityNo;
                patient.MedicareNo = output.PatientInfo.MedicareNo;
                patient.MatchKey = output.PatientInfo.MatchKey;
                patient.HisID = output.PatientInfo.HISID;
                patient.Marriage = output.PatientInfo.Patient.Marriage;
                patient.EnglishName = string.IsNullOrEmpty(output.PatientInfo.Patient.EnglishName) ? CalEnglishName(patient.LocalName) : output.PatientInfo.Patient.EnglishName;
                patient.Comments = output.PatientInfo.Patient.Comments;
                patient.Optional1 = output.PatientInfo.Patient.Optional1;
                patient.Optional2 = output.PatientInfo.Patient.Optional2;
                patient.Optional3 = output.PatientInfo.Patient.Optional3;
                patient.Alias = output.PatientInfo.Patient.Alias;
                patient.ParentName = output.PatientInfo.Patient.ParentName;
                patient.RemotePID = output.PatientInfo.Patient.RemotePID;
                result.Patient = patient;
                var requests = new List<RequestDto>();
                if (output.Requests != null && output.Requests.Length > 0)
                {
                    foreach (var item in output.Requests)
                    {
                        if (item.RequestInfo.ERNo == "")
                        {
                            return result;
                        }
                        else if (item.RequestInfo.ERNo != "" && item.RequestDetailInfo.RequestItems != null && item.RequestDetailInfo.RequestItems.Count() == 0) // has no request item
                        {
                            if (item.RequestInfo.Order != null)
                            {
                                var requestItem = new RequestDto();
                                var requestExist = GetRequestWithErNo(item.RequestInfo.ERNo);
                                if (requestExist != null && item.RequestInfo.ApplyDateTime.ToLocalTime().AddMilliseconds(-item.RequestInfo.ApplyDateTime.Millisecond) < requestExist.RequestTime)
                                {
                                    ProcessRequest(ref requestItem, item.RequestInfo, output.PatientInfo.HISID, requestExist);
                                }
                                else
                                {
                                    if (requestExist != null)
                                    {
                                        requestItem.UniqueID = requestExist.UniqueID;
                                        requestItem.RISPatientID = requestExist.RISPatientID;
                                    }
                                    ProcessRequest(ref requestItem, item.RequestInfo, output.PatientInfo.HISID);
                                }
                                // IsBedSide IsThreedReBuild Comments did not exist in database,get the data from hiscon every time
                                requests.Add(requestItem);
                                result.Requests = requests;
                                return result;
                            }
                        }
                        var request = new RequestDto();
                        var requestitems = new List<RequestItemDto>();
                        var requestlocal = GetRequestWithErNo(item.RequestInfo.ERNo);
                        // site domain info is from current request authorization
                        request.Domain = domain;
                        request.Site = site;

                        #region ERNO Exist
                        if (requestlocal != null)
                        {
                            // the request is later than local data
                            if (item.RequestInfo.ApplyDateTime.ToLocalTime().AddMilliseconds(-item.RequestInfo.ApplyDateTime.Millisecond) > requestlocal.RequestTime)
                            {
                                request.UniqueID = requestlocal.UniqueID;
                                request.RISPatientID = requestlocal.RISPatientID;
                                ProcessRequest(ref request, item.RequestInfo, output.PatientInfo.HISID);
                                // UpdateRequest(patient, request, requestlocal[0].UniqueID);
                                foreach (var rd in item.RequestDetailInfo.RequestItems)
                                {
                                    var requestItem = new RequestItemDto();
                                    ProcessRequestItem(requestItem, rd);
                                    var tempItem = GetRequestItem(rd.RequestItemUID);
                                    if (tempItem != null)
                                    {
                                        requestItem.UniqueID = tempItem.UniqueID;
                                        requestItem.ChargeItems = GetChargeItems(rd.ChargeInfo.ChargeItems, tempItem.UniqueID);
                                    }
                                    else
                                    {
                                        requestItem.ChargeItems = GetChargeItems(rd.ChargeInfo.ChargeItems);
                                    }
                                    string tempstatus = tempItem == null ? string.Empty : tempItem.Status;
                                    if (tempstatus.ToLower() == "reged" || tempstatus.ToLower() == "booked")
                                    {
                                        requestItem.Status = tempstatus;
                                    }
                                    else if (tempstatus.ToLower() == "pending")
                                    {
                                        requestItem.Status = "Pending";
                                    }
                                    else if (tempstatus.ToLower() == "rejected")
                                    {
                                        requestItem.Status = "Pending";
                                        // make the rejectted request status to be new request
                                        requestItem.UniqueID = string.Empty;
                                        requestItem.ChargeItems.ForEach(i => { i.UniqueID = string.Empty; });
                                    }
                                    else
                                    {
                                        requestItem.Status = "Pending";
                                    }
                                    requestItem.RequestID = requestlocal.UniqueID;
                                    requestitems.Add(requestItem);
                                }
                                request.RequestItems = requestitems;
                            }
                            else
                            {
                                ProcessRequest(ref request, item.RequestInfo, output.PatientInfo.HISID, requestlocal);
                                request.RequestItems = requestitems;
                                foreach (var rd in item.RequestDetailInfo.RequestItems)
                                {
                                    var requestItem = new RequestItemDto();
                                    var tempItem = GetRequestItem(rd.RequestItemUID);
                                    if (tempItem != null)
                                    {
                                        requestItem = tempItem;
                                    }
                                    else
                                    {
                                        requestItem.Status = "Pending";
                                        requestItem.ChargeItems = GetChargeItems(rd.ChargeInfo.ChargeItems);
                                        ProcessRequestItem(requestItem, rd);
                                    }
                                    requestitems.Add(requestItem);
                                }
                            }
                            requests.Add(request);
                        }
                        #endregion

                        #region ERNO not Exist
                        else
                        {
                            ProcessRequest(ref request, item.RequestInfo, output.PatientInfo.HISID);

                            //AddRequestFromIntegration(patient, request);
                            foreach (var rd in item.RequestDetailInfo.RequestItems)
                            {
                                var requestItem = new RequestItemDto();
                                ProcessRequestItem(requestItem, rd);
                                requestItem.Status = "Pending";
                                requestItem.ChargeItems = GetChargeItems(rd.ChargeInfo.ChargeItems);
                                requestitems.Add(requestItem);
                            }
                            request.RequestItems = requestitems;
                            requests.Add(request);
                        }
                        #endregion
                    }
                }
                result.Requests = requests;
                result.IsHasNoRequestItem = requests.Count() == 0;
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Process request data,local or from his
        /// </summary>
        private void ProcessRequest(ref RequestDto request, HISConService.RequestInfo data, string hisID, RequestDto requestLocal = null)
        {
            decimal totalFee;
            // in tbRequest table
            if (requestLocal == null)
            {

                request.ErNo = data.ERNo;
                request.Observation = data.Observation;
                request.ApplyDept = data.ApplyDept;
                request.ApplyDoctor = data.ApplyDoctor;
                request.HealthHistory = data.HealthHistory;
                request.PatientType = data.PatientType;
                request.ChargeType = data.ChargeType;
                request.InhospitalRegion = data.Order.InhospitalRegion;
                request.RequestTime = data.ApplyDateTime.ToLocalTime();
                request.RequestType = data.Order.RequestType;
                request.InhospitalNo = data.InhospitalNo;
                request.BedNo = data.Order.BedNo;
                request.ClinicNo = data.ClinicNo;
                request.WebAcquisitionURL = data.EAcquisitionURL;
                request.HisID = hisID;
                request.EAcquisition = data.EAcquisition;
            }
            else
            {
                request = requestLocal;
                request.HisID = string.IsNullOrEmpty(requestLocal.HisID) ? hisID : requestLocal.HisID;
            }

            //  bussiness dto for order additional info
            request.ApplyDeptNo = data.ApplyDeptNo;
            request.ApplyDoctorID = data.ApplyDoctorID;
            request.Comments = data.Comments;
            request.IsThreedReBuild = data.Order.ThreedRebuild == "1";
            request.IsBedSide = data.Order.BedSide == "1";
            request.RemoteAccNo = data.Order.RemoteAccNo;
            decimal.TryParse(data.Order.TotalFee, out totalFee);
            request.TotalFee = totalFee;
            request.Optional1 = data.Order.Optional1;
            request.Optional2 = data.Order.Optional2;
            request.Optional3 = data.Order.Optional3;
            request.IsEmergency = data.Order.IsEmergency == "1";
            request.VisitComment = data.Order.VisitComment;
            request.IsCharge = data.Order.IsCharge == "1";
            request.BodyHeight = data.Order.BodyHeight;
            request.BodyWeight = data.Order.BodyWeight;
            request.FilmFee = data.Order.FilmFee;
            request.PathologicalFindings = data.Order.PathologicalFindings;
            request.InternalOptional1 = data.Order.InternalOptional1;
            request.InternalOptional2 = data.Order.InternalOptional2;
            request.ExternalOptional1 = data.Order.ExternalOptional1;
            request.ExternalOptional2 = data.Order.ExternalOptional2;
            request.ExternalOptional3 = data.Order.ExternalOptional3;
            request.BloodSugar = data.Order.BloodSugar;
            request.CardNo = data.Order.CardNo;
            request.Insulin = data.Order.Insulin;
        }

        /// <summary>
        /// Process request item
        /// </summary>
        /// <param name="requestItem"></param>
        /// <param name="item"></param>
        private void ProcessRequestItem(RequestItemDto requestItem, HISConService.RequestItem item)
        {
            requestItem.ModalityType = item.ModalityType;
            requestItem.ProcedureCode = item.RequestItemCode;
            requestItem.RPDesc = item.RequestItemName;
            requestItem.Modality = item.Modality;
            requestItem.ExamSystem = string.Empty;
            requestItem.Comment = item.RequestItemComments;
            requestItem.TeethName = item.RequestTeethName;
            requestItem.TeethCode = item.RequestTeethCode;
            requestItem.TeethCount = int.Parse(item.RequestTeethCount);
            requestItem.AccNo = string.Empty;
            requestItem.RequestItemUID = item.RequestItemUID;
            requestItem.RemoteRPID = item.Procedure.RemoteRPID;
            requestItem.ContrastDose = item.Procedure.ContrastDose;
            requestItem.ContrastName = item.Procedure.ContrastName;
            requestItem.IsCharge = item.Procedure.IsCharge == "1";
            requestItem.Optional1 = item.Procedure.Optional1;
            requestItem.Optional2 = item.Procedure.Optional2;
            requestItem.Optional3 = item.Procedure.Optional3;
        }

        /// <summary>
        /// Get patient info from intergration service(HIS)
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="cardType"></param>
        /// <param name="hisconnURL"></param>
        /// <param name="site"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public PatientDto GetIntergrationPatientInfo(string cardNumber, string cardType, string hisconnURL)
        {
            var input = new Hys.Platform.Domain.Integration.GetRequestInfoBodyInputModel();
            input.CardNumber = cardNumber;
            input.CardType = cardType;
            input.OperatorLocalName = "";
            input.OperatorLoginName = "";
            input.TerminalName = "";
            var output = Hys.Platform.Domain.Integration.IntegrationService.GetRequestInfo(input, hisconnURL);
            if (output == null || output.PatientInfo == null || output.Requests == null || output.Requests.Count() == 0
                || output.Requests[0].RequestInfo == null || output.Requests[0].RequestDetailInfo == null
                || output.Requests[0].RequestDetailInfo.RequestItems == null)
            {
                throw new Exception("获取检查申请信息失败.");
            }

            if (output != null)
            {
                var patient = new PatientDto();
                patient.PatientNo = output.PatientInfo.PatientID;
                patient.LocalName = output.PatientInfo.LocalName;
                patient.GlobalID = output.PatientInfo.GlobalID;
                string strenglish = string.Empty;

                // var separatePolicy = ProfileManager.Instance.GetProfileValue("SeparatePolicy") ?? "1";
                // Utilities.SimplifiedToEnglish(output.PatientInfo.LocalName, ref strenglish, true, int.Parse(separatePolicy), ProfileManager.Instance.GetProfileValue("Separator"));
                //patient.EnglishName = strenglish;

                patient.Birthday = output.PatientInfo.BirthDate.ToLocalTime();
                patient.Gender = output.PatientInfo.Gender;
                patient.Address = output.PatientInfo.Address;
                patient.Telephone = output.PatientInfo.Telephone;
                patient.ReferenceNo = output.PatientInfo.ReferenceNo;
                patient.IsVip = output.PatientInfo.IsVIP == "1" ? true : false;
                // patient.PatientGuid = patientguid == "" ? Guid.NewGuid().ToString() : patientguid;
                patient.RemotePID = output.PatientInfo.HISID;
                patient.SocialSecurityNo = output.PatientInfo.SocailSecurityNo;
                patient.MedicareNo = output.PatientInfo.MedicareNo;
                patient = Mapper.Map<PatientDto>(patient);
                return patient;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get request by ERNO
        /// </summary>
        /// <param name="ErNo"></param>
        /// <returns></returns>
        private RequestDto GetRequestWithErNo(string ErNo)
        {
            var request = _requestRepository.Get(p => p.ErNo.Equals(ErNo)).Select(p => Mapper.Map<RequestDto>(p)).FirstOrDefault();
            if (request != null)
            {
                request.RequestItems = _requestItemRepository.Get(p => p.RequestID.Equals(request.UniqueID))
                    .Select(p => Mapper.Map<RequestItemDto>(p)).ToList();
                request.RequestItems.ForEach(m =>
                {
                    m.ChargeItems = _requestChargeRepository.Get(c => c.RequestItemID.Equals(m.UniqueID) && c.RequestID.Equals(request.UniqueID))
                        .Select(p => Mapper.Map<RequestChargeDto>(p)).ToList();
                });
            }
            return request;
        }

        /// <summary>
        /// Get request item status via uid
        /// </summary>
        /// <param name="requestItemUID"></param>
        /// <returns></returns>
        private string GetRequestItemStatusByuid(string requestItemUID)
        {
            var requestItem = _requestItemRepository.Get(r => r.RequestItemUID.Equals(requestItemUID)).FirstOrDefault();
            return requestItem == null ? "" : requestItem.Status;
        }

        private List<RequestChargeDto> GetChargeItems(HISConService.ChargeItem[] chargeItems, string requestItemID = null)
        {
            var requestCharges = new List<RequestChargeDto>();
            chargeItems.ToList().ForEach(c =>
            {
                var requestCharge = new RequestChargeDto();
                if (!string.IsNullOrEmpty(requestItemID))
                {
                    var chargeExist = _requestChargeRepository.Get(p => p.RequestItemID.Equals(requestItemID) && p.ItemCode.Equals(c.ChargeItemCode))
                      .Select(p => Mapper.Map<RequestChargeDto>(p)).FirstOrDefault();
                    requestCharge.UniqueID = chargeExist == null ? null : chargeExist.UniqueID;
                    requestCharge.RequestID = chargeExist == null ? null : chargeExist.RequestID;
                    requestCharge.RequestItemID = chargeExist == null ? null : chargeExist.RequestItemID;
                }
                requestCharge.Amount = Convert.ToInt32(c.ChargeItemAmount);
                requestCharge.IsItemCharged = c.IsItemCharged.Trim().Equals("1");
                requestCharge.ItemCode = c.ChargeItemCode;
                requestCharge.Price = float.Parse(c.ChargeItemUnitPrice, CultureInfo.InvariantCulture.NumberFormat);
                requestCharges.Add(requestCharge);
            });
            return requestCharges;
        }

        /// <summary>
        /// Get similar patients via storage 
        /// </summary>
        /// <param name="globalID"></param>
        /// <param name="risPatientID"></param>
        /// <param name="hisID"></param>
        /// <param name="patientName"></param>
        /// <param name="site"></param>
        /// <returns></returns>
        public List<PatientDto> GetSimilarPatient(string globalID, string risPatientID, string hisID, string patientName, string site)
        {
            List<PatientDto> list = new List<PatientDto>();
            DataTable dt = RisDBService.GetSimilarPatient(globalID, risPatientID, hisID, patientName, site);
            if (dt != null)
            {
                Patient patient = null;
                foreach (DataRow dr in dt.Rows)
                {
                    patient = new Patient();
                    patient.UniqueID = (dr["PatientGuid"] == null || dr["PatientGuid"] == DBNull.Value) ? "" : dr["PatientGuid"].ToString();
                    patient.PatientNo = (dr["PatientID"] == null || dr["PatientID"] == DBNull.Value) ? "" : dr["PatientID"].ToString();
                    patient.LocalName = (dr["LocalName"] == null || dr["LocalName"] == DBNull.Value) ? "" : dr["LocalName"].ToString();
                    patient.EnglishName = (dr["EnglishName"] == null || dr["EnglishName"] == DBNull.Value) ? "" : dr["EnglishName"].ToString();
                    patient.ReferenceNo = (dr["ReferenceNo"] == null || dr["ReferenceNo"] == DBNull.Value) ? "" : dr["ReferenceNo"].ToString();
                    patient.Birthday = (dr["Birthday"] == null || dr["Birthday"] == DBNull.Value) ? null : dr["Birthday"] as DateTime?;
                    patient.Gender = (dr["Gender"] == null || dr["Gender"] == DBNull.Value) ? "" : dr["Gender"].ToString();
                    patient.Address = (dr["Address"] == null || dr["Address"] == DBNull.Value) ? "" : dr["Address"].ToString();
                    patient.Telephone = (dr["Telephone"] == null || dr["Telephone"] == DBNull.Value) ? "" : dr["Telephone"].ToString();
                    patient.IsVIP = (dr["IsVIP"] == null || dr["IsVIP"] == DBNull.Value) ? 0 : dr["IsVIP"] as int?;
                    patient.CreateTime = (dr["CreateDt"] == null || dr["CreateDt"] == DBNull.Value) ? null : dr["CreateDt"] as DateTime?;
                    patient.Comments = (dr["Comments"] == null || dr["Comments"] == DBNull.Value) ? "" : dr["Comments"].ToString();
                    patient.RemotePID = (dr["RemotePID"] == null || dr["RemotePID"] == DBNull.Value) ? "" : dr["RemotePID"].ToString();
                    patient.Alias = (dr["Alias"] == null || dr["Alias"] == DBNull.Value) ? "" : dr["Alias"].ToString();
                    patient.Marriage = (dr["Marriage"] == null || dr["Marriage"] == DBNull.Value) ? "" : dr["Marriage"].ToString();
                    patient.Domain = (dr["Domain"] == null || dr["Domain"] == DBNull.Value) ? "" : dr["Domain"].ToString();
                    patient.GlobalID = (dr["GlobalID"] == null || dr["GlobalID"] == DBNull.Value) ? "" : dr["GlobalID"].ToString();
                    patient.MedicareNo = (dr["MedicareNo"] == null || dr["MedicareNo"] == DBNull.Value) ? "" : dr["MedicareNo"].ToString();
                    patient.ParentName = (dr["ParentName"] == null || dr["ParentName"] == DBNull.Value) ? "" : dr["ParentName"].ToString();
                    patient.RelatedID = (dr["RelatedID"] == null || dr["RelatedID"] == DBNull.Value) ? "" : dr["RelatedID"].ToString();
                    patient.Site = (dr["Site"] == null || dr["Site"] == DBNull.Value) ? "" : dr["Site"].ToString();
                    patient.SocialSecurityNo = (dr["SocialSecurityNo"] == null || dr["SocialSecurityNo"] == DBNull.Value) ? "" : dr["SocialSecurityNo"].ToString();
                    patient.UpdateTime = (dr["UpdateTime"] == null || dr["UpdateTime"] == DBNull.Value) ? null : dr["UpdateTime"] as DateTime?;
                    patient.IsUploaded = (dr["Uploaded"] == null || dr["Uploaded"] == DBNull.Value) ? 0 : dr["Uploaded"] as int?;

                    var _patient = Mapper.Map<PatientDto>(patient);
                    _patient.HisID = GetSimilarHisID(patient.UniqueID, hisID);
                    list.Add(_patient);
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetSimilarHisID(string patientid, string hisid)
        {
            var result = (from p in _risProContext.Set<AccessionNumberList>()
                          join c in _risProContext.Set<PatientList>()
                          on p.PatientID equals c.PatientID
                          where c.PatientID.Equals(patientid)
                          select p.HisID).Distinct();
            return result.FirstOrDefault().Trim();

        }
        /// <summary>
        /// Add requestlist 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        private void AddRequestList(RequestDto request)
        {
            var requestList = Mapper.Map<RequestList>(request);
            requestList.RequestID = request.UniqueID;
            requestList.UniqueID = Guid.NewGuid().ToString();
            _requestListRepository.Add(requestList);
            _requestListRepository.SaveChanges();

        }


        /// <summary>
        /// Get request item by uid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public RequestItemDto GetRequestItem(string uid)
        {
            var item = _requestItemRepository.Get(p => p.RequestItemUID.Equals(uid)).Select(p => Mapper.Map<RequestItemDto>(p)).FirstOrDefault();
            return item;
        }


        /// <summary>
        /// Get request by id
        /// </summary>
        /// <returns></returns>
        public RequestDto GetRequest(string id)
        {
            var request = _requestRepository.Get(p => p.UniqueID.Equals(id)).Select(p => Mapper.Map<RequestDto>(p)).FirstOrDefault();
            return request;
        }

        /// <summary>
        /// Add request 
        /// </summary>
        /// <returns></returns>
        public RequestDto AddRequest(RequestDto request)
        {
            _requestRepository.Add(Mapper.Map<Request>(request));
            _requestRepository.SaveChanges();
            return _requestRepository.Get(p => p.UniqueID.Equals(request.UniqueID)).Select(p => Mapper.Map<RequestDto>(p)).FirstOrDefault();
        }

        /// <summary>
        /// UPDATE request 
        /// </summary>
        /// <returns></returns>
        public RequestDto UpdateRequest(RequestDto updateRequest)
        {
            //_requestRepository.Update(Mapper.Map<Request>(request));
            var request = _requestRepository.Get(p => p.UniqueID.Equals(updateRequest.UniqueID)).FirstOrDefault();
            var ts = request.GetType();
            var up = Mapper.Map<Request>(updateRequest).GetType();
            foreach (var prop in ts.GetProperties())
            {
                if (prop.Name != "UniqueId")
                {
                    var value = up.GetProperty(prop.Name).GetValue(Mapper.Map<Request>(updateRequest), null);
                    ts.GetProperty(prop.Name).SetValue(request, value, null);
                }
            }
            _requestRepository.SaveChanges();
            return _requestRepository.Get(p => p.UniqueID.Equals(request.UniqueID)).Select(p => Mapper.Map<RequestDto>(p)).FirstOrDefault();
        }

        /// <summary>
        /// Add request item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public RequestItemDto AddRequestItem(RequestItemDto item)
        {
            _requestItemRepository.Add(Mapper.Map<RequestItem>(item));
            _requestItemRepository.SaveChanges();
            return _requestItemRepository.Get(p => p.UniqueID.Equals(item.UniqueID)).Select(p => Mapper.Map<RequestItemDto>(p)).FirstOrDefault();
        }

        /// <summary>
        /// Add request item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void DeleteRequestItem(RequestItemDto item)
        {
            var requestItem = _requestItemRepository.Get(p => p.UniqueID.Equals(item.UniqueID)).FirstOrDefault();
            ((System.Data.Entity.DbContext)_risProContext).Entry(requestItem).State = System.Data.Entity.EntityState.Detached;
            item.ChargeItems.ForEach(p =>
            {
                var charge = _requestChargeRepository.Get(i => i.UniqueID.Equals(p.UniqueID)).FirstOrDefault();
                ((System.Data.Entity.DbContext)_risProContext).Entry(charge).State = System.Data.Entity.EntityState.Detached;
                DeleteCharge(charge);
            });
            _requestItemRepository.Delete(requestItem);
            _requestItemRepository.SaveChanges();
        }

        private void DeleteCharge(RequestCharge charge)
        {
            _requestChargeRepository.Delete(charge);
            _requestChargeRepository.SaveChanges();
        }

        /// <summary>
        /// Update RequestItem 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public RequestItemDto UpdateRequestItem(RequestItemDto updateItem)
        {

            var item = _requestItemRepository.Get(p => p.UniqueID.Equals(updateItem.UniqueID)).FirstOrDefault();
            var up = Mapper.Map<RequestItem>(updateItem).GetType();
            var ts = item.GetType();
            foreach (var prop in ts.GetProperties())
            {
                if (!prop.Name.Equals("UniqueId"))
                {
                    var value = up.GetProperty(prop.Name).GetValue(Mapper.Map<RequestItem>(updateItem), null);
                    ts.GetProperty(prop.Name).SetValue(item, value, null);
                }
            }
            _requestItemRepository.SaveChanges();
            return _requestItemRepository.Get(p => p.UniqueID.Equals(item.UniqueID)).Select(p => Mapper.Map<RequestItemDto>(p)).FirstOrDefault();

        }

        /// <summary>
        /// Add request chage
        /// </summary>
        /// <returns></returns>
        public RequestChargeDto AddRequestCharge(RequestChargeDto charge)
        {
            _requestChargeRepository.Add(Mapper.Map<RequestCharge>(charge));
            _requestChargeRepository.SaveChanges();
            return _requestChargeRepository.Get(p => p.UniqueID.Equals(charge.UniqueID)).Select(P => Mapper.Map<RequestChargeDto>(P)).FirstOrDefault();
        }

        /// <summary>
        /// Update request chage
        /// </summary>
        /// <returns></returns>
        public RequestChargeDto UpdateRequestCharge(RequestChargeDto updateCharge)
        {
            var charge = _requestChargeRepository.Get(p => p.UniqueID.Equals(updateCharge.UniqueID)).FirstOrDefault();
            var up = Mapper.Map<RequestCharge>(updateCharge).GetType();
            var ts = charge.GetType();
            foreach (var prop in ts.GetProperties())
            {
                if (!prop.Name.Equals("UniqueId"))
                {
                    var value = up.GetProperty(prop.Name).GetValue(Mapper.Map<RequestCharge>(updateCharge), null);
                    ts.GetProperty(prop.Name).SetValue(charge, value, null);
                }
            }
            _requestChargeRepository.SaveChanges();
            return _requestChargeRepository.Get(p => p.UniqueID.Equals(charge.UniqueID)).Select(P => Mapper.Map<RequestChargeDto>(P)).FirstOrDefault();
        }


        /// <summary>
        /// Calcute the current age by birthday of patient
        /// </summary>
        /// <param name="birthday">birthday of patient</param>
        /// <param name="minYearNumber">from system configure</param>
        /// <param name="minMonthNumber">from system configure</param>
        /// <param name="minWeekNumber">NA</param>
        /// <param name="minDayNumber">from system configure</param>             
        /// <returns>current age  as  12Year, 3Month or 16Hour</returns>
        private string CalCurrentAge(DateTime birthday)
        {
            int minYearNumber = 0, minMonthNumber = 0, minWeekNumber = 0, minDayNumber = 0;
            var year = _risProContext.Set<SystemProfile>().Where(p => p.Name.Equals("YearNumber")).FirstOrDefault();
            var month = _risProContext.Set<SystemProfile>().Where(p => p.Name.Equals("MonthNumber")).FirstOrDefault();
            var day = _risProContext.Set<SystemProfile>().Where(p => p.Name.Equals("DayNumber")).FirstOrDefault();
            //default value 2,1,2
            minYearNumber = Convert.ToInt32(year != null ? year.Value : "2");
            minMonthNumber = Convert.ToInt32(month != null ? month.Value : "1");
            minDayNumber = Convert.ToInt32(day != null ? day.Value : "2");
            string currentAge = string.Empty;
            //计算年龄的基准时间,如果是修改，用order的Create时间， 如果是新建,用当前时间
            DateTime baseTime = DateTime.Now.ToLocalTime();
            if (birthday.CompareTo(baseTime) > 0)
            {
                birthday = baseTime;

            }
            else
            {
                int nYear = GetYearDefference(birthday, baseTime);
                int nMonth = GetMonthDefference(birthday, baseTime);
                if (nYear >= minYearNumber)
                {
                    currentAge = nYear.ToString() + " Year";
                }
                else if (nMonth >= minMonthNumber)
                {
                    currentAge = nMonth.ToString() + " Month";
                }
                else
                {
                    TimeSpan ts = baseTime.Subtract(birthday);
                    int nHour = (int)ts.TotalHours;
                    int nDay = nHour / 24;
                    if (nDay >= minDayNumber)
                    {
                        currentAge = nDay.ToString() + " Day";
                    }
                    else
                    {
                        currentAge = nHour.ToString() + " Hour";
                    }
                }
            }
            return currentAge;
        }

        /// <summary>
        /// Get English Name
        /// </summary>
        /// <param name="localName"></param>
        /// <returns></returns>
        private string CalEnglishName(string localName)
        {
            bool upperFirstLetter = false;
            int separatePolicy = 1;
            string separator = string.Empty;
            var _firstLetter = _risProContext.Set<SystemProfile>().Where(p => p.Name.Equals("UpperFirstLetter")).FirstOrDefault();
            var _policy = _risProContext.Set<SystemProfile>().Where(p => p.Name.Equals("SeparatePolicy")).FirstOrDefault();
            var _separator = _risProContext.Set<SystemProfile>().Where(p => p.Name.Equals("Separator")).FirstOrDefault();

            upperFirstLetter = _firstLetter != null ? _firstLetter.Value.Trim().Equals("1") : false;
            if (!int.TryParse(_policy != null ? _policy.Value : "1", out separatePolicy))
            {
                separatePolicy = 1;
            }
            separator = _separator != null ? _separator.Value : string.Empty;
            string strEnglish = PinyinUtil.ToPinyin(localName, upperFirstLetter, separatePolicy, separator);
            return strEnglish;
        }

        /// <summary>
        /// Get the year defference between dtbegin and dtend
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        private static int GetYearDefference(DateTime dtBegin, DateTime dtEnd)
        {
            int nYear = 0;
            try
            {
                TimeSpan ts = dtEnd - dtBegin;
                nYear = dtEnd.Year - dtBegin.Year;

                if (dtEnd.Month < dtBegin.Month)
                {
                    nYear--;
                }
                else if (dtEnd.Month == dtBegin.Month)
                {
                    if (dtEnd.Day < dtBegin.Day)
                    {
                        nYear--;
                    }
                }

                if (nYear < 0)
                {
                    nYear = 0;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return nYear;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        private static int GetMonthDefference(DateTime dtBegin, DateTime dtEnd)
        {
            int nMonth = 0;
            try
            {
                TimeSpan ts = dtEnd - dtBegin;
                nMonth = (dtEnd.Year - dtBegin.Year) * 12;

                nMonth += dtEnd.Month - dtBegin.Month;

                if (dtEnd.Day < dtBegin.Day)
                {
                    nMonth--;
                }

                if (nMonth < 0)
                {
                    nMonth = 0;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return nMonth;
        }

        private string Object2XML(object obj, Type type)
        {
            if (obj == null || type == null)
            {
                return "";
            }

            try
            {
                //this avoids xml document declaration
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    OmitXmlDeclaration = true
                };
                var xmlAttributes = new XmlAttributes();
                var xmlAttributeOverrides = new XmlAttributeOverrides();

                xmlAttributes.Xmlns = false;
                xmlAttributes.XmlType = new XmlTypeAttribute() { Namespace = "" };
                xmlAttributeOverrides.Add(type, xmlAttributes);
                XmlSerializer xsSubmit = new XmlSerializer(type, xmlAttributeOverrides);

                StringWriter sww = new StringWriter();

                using (var writer = XmlWriter.Create(sww, settings))
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                    //Add an empty namespace and empty value
                    xsSubmit.Serialize(writer, obj, ns);
                    return sww.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }
        #endregion

        #region Requisition bussiness
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullOutputPath"></param>
        /// <param name="base64String"></param>
        public ImageData SaveTempImage(ImageData imgData, string userName)
        {
            var tempPath = GetTempDirectoryPath(imgData.FileName, userName);
            var base64Str = imgData.Base64Str.Substring(imgData.Base64Str.IndexOf(',') + 1);
            byte[] bytes = Convert.FromBase64String(base64Str);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image bitmap = Bitmap.FromStream(ms);
                // Bitmap bitmap = new Bitmap(ms);
                if (!System.IO.Directory.Exists(tempPath))
                {
                    System.IO.Directory.CreateDirectory(tempPath);
                }
                if (imgData.IsUpdate)
                {
                    // if update need not save with ImageQualityLevel
                    bitmap.Save(Path.Combine(tempPath, imgData.FileName));
                }
                else
                {
                    // save temp image first with ImageQualityLevel
                    // if level is -1,set the default quality 80%;
                    var defaultLevelValue = -1;
                    if (imgData.ImageQualityLevel.Equals(defaultLevelValue))
                    {
                        imgData.ImageQualityLevel = 80;
                    }
                    ImageCodecInfo jgpEncoder = FtpClient.GetEncoder(ImageFormat.Jpeg);
                    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, imgData.ImageQualityLevel);
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    bitmap.Save(Path.Combine(tempPath, imgData.FileName), jgpEncoder, myEncoderParameters);
                    using (MemoryStream newMs = new MemoryStream())
                    {
                        bitmap.Save(newMs, jgpEncoder, myEncoderParameters);
                        //var bm = Bitmap.FromFile(Path.Combine(tempPath, imgData.FileName));
                        //bm.Save(newMs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] imageBytes = newMs.ToArray();
                        // Convert byte[] to Base64 String
                        imgData.Base64Str = @"data:image/png;base64," + Convert.ToBase64String(imageBytes);
                    }
                }
                // avoid memory leak
                bitmap.Dispose();
            }
            return imgData;
        }

        public List<ImageData> DownLoadRequisitionFiles(string accNo, string domain, string userName)
        {
            var imageList = new List<ImageData>();
            var requisitions = _requisitionRepository.Get(p => p.AccNo.Equals(accNo)).Select(c => Mapper.Map<RequisitionDto>(c)).ToList();
            if (requisitions.Count() == 0)
            {
                return imageList;
            }
            var tempPath = GetTempDirectoryPath(requisitions.First().FileName, userName);
            // Start to Ftp
            FtpClient ftpClient = GetFTPObject(domain);
            requisitions.ForEach(p =>
            {
                var rpPath = p.RelativePath + '/' + p.FileName;
                var localPath = Path.Combine(tempPath, p.FileName);
                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }
                var isDownLoaded = ftpClient.DownloadFile(rpPath, localPath);
                if (isDownLoaded)
                {
                    var image = new ImageData();
                    image.Base64Str = @"data:image/jpeg;base64," + Convert.ToBase64String(File.ReadAllBytes(localPath));
                    image.FileName = p.FileName;
                    image.RelativePath = p.RelativePath;
                    image.RequisitionID = p.UniqueID;
                    imageList.Add(image);
                }
            });
            return imageList;
        }

        /// <summary>
        /// Clear temp file
        /// </summary>
        public void ClearTempFile(string erNo, string userName)
        {
            var delePath = Path.Combine(Contants.ProfileKey.TEMP_PATH, erNo + "_" + userName);
            Array.ForEach(Directory.GetFiles(delePath), File.Delete);
            Directory.Delete(delePath);
        }

        /// <summary>
        /// Delete scanned image ,if the relative path is not null,need to delete tht file in FTP
        /// </summary>
        /// <param name="accNo"></param>
        /// <param name="fileName"></param>
        public bool DeleteImage(string fileName, string relativePath, string requisitionID, string domain, string userName)
        {
            try
            {
                var delePath = Path.Combine(GetTempDirectoryPath(fileName, userName), fileName);
                if (File.Exists(delePath))
                {
                    File.Delete(delePath);
                }
                // delete ftp server file
                if (!string.IsNullOrEmpty(requisitionID))
                {
                    var requisition = _requisitionRepository.Get(p => p.UniqueID.Equals(requisitionID)).FirstOrDefault();
                    var accNo = requisition.AccNo;
                    _requisitionRepository.Delete(requisition);
                    _requisitionRepository.SaveChanges();
                    var requisitions = _requisitionRepository.Get(p => p.AccNo.Equals(accNo));
                    // order has no requisition
                    if (!requisitions.Any())
                    {
                        // IsScan=false->value is 0
                        var isNotScanValue = 0;
                        var order = _OrderRepository.Get(o => o.AccNo.Equals(accNo)).FirstOrDefault();
                        order.IsScan = isNotScanValue;
                        _OrderRepository.SaveChanges();
                    }
                    var existRequisition = _requisitionRepository.Get(p => p.FileName.Equals(fileName));
                    if (!existRequisition.Any())
                    {
                        var remotePath = Path.Combine(relativePath, fileName);
                        var ftpClient = GetFTPObject(domain);
                        ftpClient.DeleteFile(remotePath);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }



        /// <summary>
        /// Upload requisition files to FTP server
        /// </summary>
        /// <param name="accNos"></param>
        /// <param name="erNo"></param>
        /// <param name="relativePath"></param>
        /// <param name="imageQualityLevel"></param>
        /// <returns>
        /// 0 success
        /// -1 database error or file create error
        /// -2 ftp server is not valid
        /// </returns>
        public int UploadRequisitionFile(List<string> accNos, string erNo, string relativePath, string imageQualityLevel, string userName, string domain)
        {
            var isValidFtp = ValidateFTP(domain);
            if (!isValidFtp)
            {
                return -2;
            }
            FtpClient ftpClient = GetFTPObject(domain);

            //System.Text.StringBuilder strB = new System.Text.StringBuilder(erNo);
            //strB.Append("_").Append(ServiceContext.Current.UserName);
            var tempDirectoryPath = Path.Combine(Contants.ProfileKey.TEMP_PATH, erNo + "_" + userName);
            var fileList = Directory.GetFiles(tempDirectoryPath).Select(p => Path.GetFileName(p)).ToList();
            fileList.ForEach(p =>
            {
                var localPath = Path.Combine(tempDirectoryPath, p);
                ftpClient.UploadFile(localPath, relativePath, Convert.ToInt64(imageQualityLevel));
                accNos.ForEach(n =>
                {
                    var existRequisition = _requisitionRepository.Get(m => m.FileName.Equals(p) && m.AccNo.Equals(n)).FirstOrDefault();
                    if (existRequisition == null)
                    {
                        var requisition = new Requisition();
                        requisition.UniqueID = Guid.NewGuid().ToString();
                        requisition.AccNo = n;
                        requisition.FileName = p;
                        requisition.RelativePath = relativePath;
                        requisition.Uploaded = 0;
                        requisition.Domain = domain;
                        requisition.ScanTime = requisition.CreateTime = requisition.UpdateTime = DateTime.Now;
                        _requisitionRepository.Add(requisition);
                    }
                    else
                    {
                        existRequisition.UpdateTime = DateTime.Now;
                    }
                });
            });
            _requisitionRepository.SaveChanges();
            ClearTempFile(erNo, userName);
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accNo"></param>
        /// <param name="erNo"></param>
        /// <param name="relativePath"></param>
        /// <param name="imageQualityLevel"></param>
        /// <returns>
        /// 0 success
        /// -1 database error or file create error
        /// -2 ftp server is not valid
        /// </returns>
        public int ProcessRequisitionInOrder(string accNo, string erNo, string relativePath, string imageQualityLevel, string userName, string domain)
        {
            var accNos = new List<string>();
            var isScanValue = 1;
            accNos.Add(accNo);
            var result = UploadRequisitionFile(accNos, erNo, relativePath, imageQualityLevel, userName, domain);
            if (result.Equals(0))
            {
                var order = _OrderRepository.Get(p => p.AccNo.Equals(accNo)).FirstOrDefault();
                if (!order.IsScan.Equals(isScanValue))
                {
                    order.IsScan = isScanValue;
                    _OrderRepository.SaveChanges();
                }
                return 0;
            }
            else if (result.Equals(-2))
            {
                return -2;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Generate Erno
        /// </summary>
        /// <returns></returns>
        public string GenerateERNo(string site)
        {
            return site + "E" + GetDatetimeFormat().ToString();
        }

        /// <summary>
        /// Validate FTP server
        /// </summary>
        /// <returns></returns>
        public bool ValidateFTP(string domain)
        {
            try
            {
                var ftpClient = GetFTPObject(domain);
                if (ftpClient == null)
                {
                    return false;
                }
                string url = "ftp://" + ftpClient.FTPServer + ":" + ftpClient.Port;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(ftpClient.UserName, ftpClient.Password);
                request.UsePassive = true;
                request.KeepAlive = false;
                WebResponse response = request.GetResponse();
                response.Close();
            }
            catch (WebException ex)
            {
                _logger.Log(LogLevel.Error, ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get unique ErNo based on datetime
        /// </summary>
        /// <returns></returns>
        private Int64 GetDatetimeFormat()
        {
            return System.Threading.Interlocked.Increment(ref Contants.ProfileKey.seed);
        }

        /// <summary>
        /// Get special temp directory path by fileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetTempDirectoryPath(string fileName, string userName)
        {
            var erNo = fileName.Substring(0, fileName.Length - 7);
            var path = Path.Combine(Contants.ProfileKey.TEMP_PATH, erNo + "_" + userName);
            return path;
        }

        /// <summary>
        /// Get Ftp userName PWD service url
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        private FtpClient GetFTPObject(string domain)
        {
            DomainList domainList = _risProContext.Set<DomainList>().Where(p => p.DomainName == domain).FirstOrDefault();
            if (domainList != null)
            {
                return new FtpClient(
                    domainList.FtpServer,
                    Convert.ToInt32(domainList.FtpPort),
                    domainList.FtpUser,
                    domainList.FtpPassword);
            }
            return null;
        }

        public async Task<IEnumerable<ModalityDto>> GetBookingModalities(string modalityType, string site)
        {
            var task = await (from m in _risProContext.Set<Modality>()
                              where (m.Site == site || String.IsNullOrEmpty(m.Site)) && m.ModalityType == modalityType
                              select new { m.ModalityName, Priority = String.IsNullOrEmpty(m.Site) ? 2 : 1 })
                              .Union(
                              from s in _risProContext.Set<ModalityShare>()
                              join t in _risProContext.Set<ModalityTimeSlice>() on s.TimeSliceGuid equals t.TimeSliceGuid
                              where t.ModalityType == modalityType && s.ShareTarget == site && s.TargetType == 1
                              select new { ModalityName = t.Modality, Priority = 3 }).ToListAsync();

            var modalities = await _risProContext.Set<Modality>().ToListAsync();

            var targetModalities = task.GroupBy(m => m.ModalityName).Select(m => m.First());

            var result =
                modalities.Join(targetModalities, m1 => m1.ModalityName, m2 => m2.ModalityName, (m1, m2) => new { m1, m2 })
                    .OrderBy(@t => @t.m2.Priority)
                    .ThenBy(@t => @t.m1.ModalityName)
                    .Select(@t => Mapper.Map<ModalityDto>(@t.m1));

            return result;
        }
        public async Task<IEnumerable<ModalityTimeSliceDto>> GetModalitySchedule(string modality, DateTime date, string site, string userId, string role)
        {
            var timeslices = new List<ModalityTimeSliceDto>();

            var now = DateTime.Now;
            var availableDate = new DateTime();
            var dateType = "1";
            var dateString = date.ToString("yyyy-MM-dd");
            var timeFormat = dateString + " HH:mm:ss";
            var start = Convert.ToDateTime(dateString + " 00:00:00");
            var end = Convert.ToDateTime(dateString + " 23:59:59");

            if (date.Date < now.Date)
            {
                return timeslices;
            }

            var reportDbService = new ReportDBService();
            reportDbService.GetDateTypeAvailableDate(modality, date, ref dateType, ref availableDate);
            var dateTypeInt = Convert.ToInt32(dateType);

            var procedures = await (from p in _risProContext.Set<Procedure>()
                                    join o in _risProContext.Set<Order>() on p.OrderID equals o.UniqueID
                                    where p.Modality == modality && p.BookingBeginTime >= start && p.BookingEndTime <= end && o.BookingSite == site
                                    select p).ToListAsync();

            //1 for order，2 for procedure
            var countConfigList = await _configurationService.GetProfile(userId, site, role, "BookingScheduleTakeCountMode");
            var countConfigObj = countConfigList.FirstOrDefault();
            var countConfig = "1";
            if (countConfigObj != null)
            {
                countConfig = countConfigObj.Value;
            }

            Func<Procedure, string> procedureSelector;
            if (countConfig == "1")
            {
                procedureSelector = p => p.OrderID;
            }
            else
            {
                procedureSelector = p => p.UniqueID;
            }

            Action<ModalityTimeSliceDto> timeSliceProcess = tmpSlice =>
            {
                start = Convert.ToDateTime(tmpSlice.StartDt.Value.ToString(timeFormat));
                end = Convert.ToDateTime(tmpSlice.EndDt.Value.ToString(timeFormat));

                if (end < now) return;

                var count = procedures.Where(pc => pc.BookingBeginTime >= start && pc.BookingEndTime <= end)
                    .Select(procedureSelector).Distinct().Count();

                tmpSlice.StartDt = start;
                tmpSlice.EndDt = end;
                tmpSlice.TotalUsedQuota = count;
            };

            var modalityShareExists = _risProContext.Set<ModalityShare>().Any();
            if (modalityShareExists)
            {
                #region for ris db version>S03.1
                var baseQuery = await (from s in _risProContext.Set<ModalityShare>()
                                       join t in _risProContext.Set<ModalityTimeSlice>() on s.TimeSliceGuid equals t.TimeSliceGuid
                                       where
                                           t.Modality == modality && s.TargetType == 1 &&
                                           (s.Date == null || DbFunctions.DiffDays(s.Date, date) == 0)
                                           && s.ShareTarget == site && t.DateType == dateTypeInt && t.AvailableDate == availableDate
                                       select new { Share = s, TimeSlice = t }).ToListAsync();
                var currentDateTimeSlice =
                    baseQuery.Where(b => b.Share.Date.HasValue && b.Share.Date.Value.Date == date.Date).ToList();

                if (!currentDateTimeSlice.Any())
                {
                    currentDateTimeSlice = baseQuery.Where(b => b.Share.Date == null).ToList();
                }

                currentDateTimeSlice.GroupBy(c => c.TimeSlice.UniqueId).ToList().ForEach(g =>
                {
                    var tmpSlice = Mapper.Map<ModalityTimeSliceDto>(g.First().TimeSlice);
                    g.GroupBy(
                        s =>
                            (s.Share.GroupId == "Default_Hide" || string.IsNullOrEmpty(s.Share.GroupId))
                                ? "Private"
                                : "Share").ToList().ForEach(t =>
                                {
                                    if (t.Key == "Private")
                                    {
                                        tmpSlice.TotalPrivateQuota = t.Sum(o => o.Share.MaxCount) ?? 0;
                                    }
                                    else
                                    {
                                        tmpSlice.TotalSharedQuota = t.Sum(o => o.Share.AvailableCount) ?? 0;
                                    }
                                });
                    tmpSlice.TotalAvailableQuota = g.Sum(s => s.Share.AvailableCount) ?? 0;

                    timeSliceProcess(tmpSlice);

                    timeslices.Add(tmpSlice);
                });
                #endregion
            }
            else
            {
                #region for ris db version=S03.1

                var timesliceQuery = await (from t in _risProContext.Set<ModalityTimeSlice>()
                                            where t.Modality == modality && t.DateType == dateTypeInt && t.AvailableDate == availableDate
                                            select t).ToListAsync();
                timesliceQuery.ForEach(t =>
                {
                    var tslice = Mapper.Map<ModalityTimeSliceDto>(t);
                    tslice.TotalPrivateQuota = t.MaxNumber ?? 0;
                    timeSliceProcess(tslice);
                    tslice.TotalAvailableQuota = (t.MaxNumber ?? 0) - tslice.TotalUsedQuota;
                    tslice.TotalAvailableQuota = tslice.TotalAvailableQuota > 0 ? tslice.TotalAvailableQuota : 0;
                    timeslices.Add(tslice);
                });
                #endregion
            }

            return timeslices;
        }
        public async Task<string> LockModalityQuota(ModalityTimeSliceDto timeSlice, string site)
        {
            var reportDbService = new ReportDBService();
            var dateType = timeSlice.DateType.ToString();
            var availableDate = timeSlice.AvailableDate.Value;
            var bookingDate = timeSlice.StartDt.Value;
            var lockGuid = "";
            var modalityShareExists = _risProContext.Set<ModalityShare>().Any();
            if (!modalityShareExists)
            {
                return "";
            }
            var timesliceQuery = await (from s in _risProContext.Set<ModalityShare>()
                                        join t in _risProContext.Set<ModalityTimeSlice>() on s.TimeSliceGuid equals t.TimeSliceGuid
                                        where t.TimeSliceGuid == timeSlice.UniqueID && DbFunctions.DiffDays(s.Date, bookingDate) == 0
                                              && s.ShareTarget == site && s.TargetType == 1
                                        select s).ToListAsync();
            if (timesliceQuery.Count < 1)
            {
                timesliceQuery = await (from s in _risProContext.Set<ModalityShare>()
                                        join t in _risProContext.Set<ModalityTimeSlice>() on s.TimeSliceGuid equals t.TimeSliceGuid
                                        where t.TimeSliceGuid == timeSlice.UniqueID && s.Date == null
                                              && s.ShareTarget == site && s.TargetType == 1
                                        select s).ToListAsync();
            }
            if (timesliceQuery.Count < 1)
            {
                return "Error:TimeSliceNotExist";
            }
            var privateNumber =
                timesliceQuery.Where(s => s.GroupId == "Default_Hide" || string.IsNullOrEmpty(s.GroupId))
                    .Sum(s => s.AvailableCount);
            var sharedNumber =
                timesliceQuery.Where(s => s.GroupId != "Default_Hide" && !string.IsNullOrEmpty(s.GroupId))
                    .Sum(s => s.AvailableCount);

            if (privateNumber + sharedNumber <= 0)
            {
                return "Error:MaxNumberReached";
            }

            if (timeSlice.EndDt < DateTime.Now)
            {
                return "Error:Earlier";
            }

            lockGuid = await Task.Run(() => reportDbService.LockModalityQuota(timeSlice.Modality, dateType, availableDate, bookingDate, timeSlice.UniqueID, site));

            return lockGuid;
        }
        public async Task UnlockModalityQuota(string unlockGuid, string modality, DateTime? start, DateTime? end, string site)
        {
            if (string.IsNullOrEmpty(unlockGuid))
            {
                if (!start.HasValue || !end.HasValue) return;
                var diffTimeSpan = end.Value - start.Value;
                if (diffTimeSpan.Days != 0) return;
                if (diffTimeSpan.TotalSeconds < 0) return;
                if (string.IsNullOrEmpty(modality)) return;

                var date = start.Value.Date;
                var dateString = date.ToString("yyyy-MM-dd");
                var timeFormat = dateString + " HH:mm:ss";
                var dateType = "1";
                var availableDate = new DateTime();

                var reportDbService = new ReportDBService();
                reportDbService.GetDateTypeAvailableDate(modality, date, ref dateType, ref availableDate);
                var dateTypeInt = Convert.ToInt32(dateType);

                var baseQuery = await (from s in _risProContext.Set<ModalityShare>()
                                       join t in _risProContext.Set<ModalityTimeSlice>() on s.TimeSliceGuid equals t.TimeSliceGuid
                                       where
                                           t.Modality == modality && s.TargetType == 1 &&
                                           s.ShareTarget == site && t.DateType == dateTypeInt && t.AvailableDate == availableDate &&
                                           DbFunctions.DiffDays(s.Date, date) == 0
                                       select new { Share = s, Start = t.StartDt, End = t.EndDt }).ToListAsync();

                if (!baseQuery.Any()) return;
                var targetTimeSlices =
                    baseQuery.Where(
                        q => q.Start.HasValue && DateTime.Parse(q.Start.Value.ToString(timeFormat)) <= start.Value &&
                             q.End.HasValue && DateTime.Parse(q.End.Value.ToString(timeFormat)) >= end.Value).Select(q => q.Share).ToList();
                if (!targetTimeSlices.Any()) return;

                var privateTimeSlice =
                    targetTimeSlices.FirstOrDefault(
                        p => (p.GroupId == "Default_Hide" || p.GroupId == "") && p.AvailableCount > 0);
                if (privateTimeSlice != null)
                {
                    if (privateTimeSlice.MaxCount <= privateTimeSlice.AvailableCount) return;

                    privateTimeSlice.AvailableCount += 1;
                    await _risProContext.SaveChangesAsync();
                    return;
                }

                var sharedTimeSlices =
                    targetTimeSlices.Where(s => s.GroupId != "Default_Hide" && s.GroupId != "" && s.AvailableCount > 0)
                        .ToList();
                if (sharedTimeSlices.Any())
                {
                    var justNowShare = !sharedTimeSlices.Any(j => j.MaxCount > j.AvailableCount);
                    if (justNowShare)
                    {
                        var temp = targetTimeSlices.FirstOrDefault(
                            p => (p.GroupId == "Default_Hide" || p.GroupId == "") && p.AvailableCount == 0);
                        if (temp != null)
                        {
                            temp.AvailableCount += 1;
                            await _risProContext.SaveChangesAsync();
                        }

                    }
                    else
                    {
                        var tmpShare = sharedTimeSlices.FirstOrDefault(s => s.MaxCount > s.AvailableCount);
                        if (tmpShare != null)
                        {
                            tmpShare.AvailableCount += 1;
                            await _risProContext.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    var firstShare =
                        targetTimeSlices.FirstOrDefault(s => s.GroupId != "Default_Hide" && s.GroupId != "" &&
                                                        s.AvailableCount == 0);
                    if (firstShare != null && firstShare.MaxCount > 0)
                    {
                        firstShare.AvailableCount += 1;
                        await _risProContext.SaveChangesAsync();
                    }
                    else
                    {
                        var firstPrivate = targetTimeSlices.FirstOrDefault(s => (s.GroupId == "Default_Hide" || s.GroupId == "") &&
                                                        s.AvailableCount == 0);
                        if (firstPrivate != null && firstPrivate.MaxCount > 0)
                        {
                            firstPrivate.AvailableCount += 1;
                            await _risProContext.SaveChangesAsync();
                        }
                    }
                }
            }
            else
            {
                var reportDbService = new ReportDBService();
                await Task.Run(() =>
                {
                    reportDbService.UnlockModalityQuota(unlockGuid);
                });
            }
        }
        #endregion
    }
}
