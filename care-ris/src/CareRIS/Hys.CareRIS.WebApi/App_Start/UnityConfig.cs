using System;
using System.Configuration;
using System.Linq;
using Hys.Consultation.EntityFramework.Repositories;
using Hys.Consultation.Application.Services;
using Hys.Consultation.Application.Services.ServiceImpl;
using Hys.Consultation.Domain.Interface;
using Hys.Consultation.EntityFramework;
using Hys.CareRIS.Application.Services;
using Hys.CareRIS.Application.Services.ServiceImpl;
using Hys.CareRIS.WebApi.Security;
using Hys.CareRIS.WebApi.Services;
using Hys.Platform.CrossCutting.LogContract;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.EntityFramework;
using Hys.CareRIS.EntityFramework.Repositories;
using Microsoft.Practices.Unity;
using IShortcutRepository = Hys.CareRIS.Domain.Interface.IShortcutRepository;
using IUserManagementService = Hys.CareRIS.Application.Services.IUserManagementService;
using UserManagementService = Hys.CareRIS.Application.Services.ServiceImpl.UserManagementService;

namespace Hys.CareRIS.WebApi
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<ICommonLog, Log4netImpl>(new ContainerControlledLifetimeManager());

            var connectionString = ConfigurationManager.ConnectionStrings["RisProContext"].ConnectionString;
            container.RegisterType<IRisProContext, RisProContext>(new PerRequestLifetimeManager(), new InjectionConstructor(connectionString, container.Resolve<ICommonLog>()));

            container.RegisterType<IPatientRepository, PatientRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IProcedureRepository, ProcedureRepository>();
            container.RegisterType<IProcedureCodeRepository, ProcedureCodeRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IModalityTypeRepository, ModalityTypeRepository>();
            container.RegisterType<IModalityRepository, ModalityRepository>();
            container.RegisterType<IDictionaryRepository, DictionaryRepository>();
            container.RegisterType<IDictionaryValueRepository, DictionaryValueRepository>();
            container.RegisterType<CareRIS.Domain.Interface.IShortcutRepository, CareRIS.EntityFramework.Repositories.ShortcutRepository>();

            container.RegisterType<IReportPrintLogRepository, ReportPrintLogRepository>();

            container.RegisterType<IWorklistService, WorklistService>(new PerRequestLifetimeManager());
            container.RegisterType<ICommonService, CommonService>(new PerRequestLifetimeManager());
            container.RegisterType<IRegistrationService, RegistrationService>(new PerRequestLifetimeManager());
            container.RegisterType<IConfigurationService, ConfigurationService>(new PerRequestLifetimeManager());
            container.RegisterType<IReferralService, ReferralService>(new PerRequestLifetimeManager());

            container.RegisterType<IReportRepository, ReportRepository>();
            container.RegisterType<IReportService, ReportService>(new PerRequestLifetimeManager());
            container.RegisterType<IReportPrintService, ReportPrintService>(new PerRequestLifetimeManager());
            container.RegisterType<IReportLockService, ReportLockService>(new PerRequestLifetimeManager());
            container.RegisterType<IReportTemplateService, ReportTemplateService>(new PerRequestLifetimeManager());
            container.RegisterType<IReportFileRepository, ReportFileRepository>();
            container.RegisterType<IPrintTemplateRepository, PrintTemplateRepository>();
            container.RegisterType<IPrintTemplateFieldsRepository, PrintTemplateFieldsRepository>();

            container.RegisterType<IReportTemplateDirecRepository, ReportTemplateDirecRepository>();
            container.RegisterType<IReportTemplateRepository, ReportTemplateRepository>();
            container.RegisterType<IReportDelPoolRepository, ReportDelPoolRepository>();
            container.RegisterType<IReportListRepository, ReportListRepository>();
            container.RegisterType<ISyncRepository, SyncRepository>();
            container.RegisterType<ISiteRepository, SiteRepository>();
            container.RegisterType<IOnlineClientRepository, OnlineClientRepository>();

            container.RegisterType<IUserManagementService, UserManagementService>(new PerRequestLifetimeManager());
            container.RegisterType<IGWDataIndexRepository, GWDataIndexRepository>();
            container.RegisterType<IGWOrderRepository, GWOrderRepository>();
            container.RegisterType<IGWPatientRepository, GWPatientRepository>();
            container.RegisterType<IGWReportRepository, GWReportRepository>();

            container.RegisterType<IAccessionNumberListRepository, AccessionNumberListRepository>();
            container.RegisterType<IRoleToUserRepository, RoleToUserRepository>();
            container.RegisterType<IBodySystemMapRepository, BodySystemMapRepository>();

            container.RegisterType<IDomainListRepository, DomainListRepository>();
            container.RegisterType<IRequestRepository, RequestRepository>();
            container.RegisterType<IRequestItemRepository, RequestItemRepository>();
            container.RegisterType<IRequestChargeRepository, RequestChargeRepository>();
            container.RegisterType<IRequestListRepository, RequestListRepository>();
            container.RegisterType<IRequisitionRepository, RequisitionRepository>();


            //consultation
            container.RegisterType<IInitialDataService, InitialDataService>(new ContainerControlledLifetimeManager());
            var consultationConnStr = ConfigurationManager.ConnectionStrings["ConsultationContext"].ConnectionString;
            container.RegisterType<IConsultationContext, ConsultationContext>(new PerRequestLifetimeManager(), new InjectionConstructor(consultationConnStr, container.Resolve<ICommonLog>(), container.Resolve<IInitialDataService>()));
            container.RegisterType<IExamModuleRepository, ExamModuleRepository>();
            container.RegisterType<IPersonRepository, PersonRepository>();
            container.RegisterType<IConsultationService, ConsultationService>(new PerRequestLifetimeManager());
            container.RegisterType<IConsultationConfigurationService, ConsultationConfigurationService>(new PerRequestLifetimeManager());

            container.RegisterType<IPatientCaseRepository, PatientCaseRepository>();
            container.RegisterType<IHospitalProfileRepository, HospitalProfileRepository>();
            container.RegisterType<IConsultationRequestRepository, ConsultationRequestRepository>();
            container.RegisterType<IConsultationDictionaryRepository, ConsultationDictionaryRepository>();
            container.RegisterType<IConsultationAssignRepository, ConsultationAssignRepository>();
            container.RegisterType<IConsultationReportHistoryRepository, ConsultationReportHistoryRepository>();
            container.RegisterType<IConsultationReportRepository, ConsultationReportRepository>();
            container.RegisterType<IDAMHospitalRepository, DAMHospitalRepository>();
            container.RegisterType<IDAMInfoRepository, DAMInfoRepository>();
            container.RegisterType<IEMRItemDetailRepository, EMRItemDetailRepository>();
            container.RegisterType<IEMRItemRepository, EMRItemRepository>();
            container.RegisterType<IPersonPatientCaseRepository, PersonPatientCaseRepository>();
            container.RegisterType<ISysConfigRepository, SysConfigRepository>();
            container.RegisterType<Consultation.Domain.Interface.IShortcutRepository, Consultation.EntityFramework.Repositories.ShortcutRepository>();
            container.RegisterType<IConsultationPatientCaseService, ConsultationPatientCaseService>(new PerRequestLifetimeManager());
            container.RegisterType<IShortcutService, ShortcutService>(new PerRequestLifetimeManager());
            container.RegisterType<ILicenseService, LicenseService>(new PerRequestLifetimeManager());

            //structuredreport
            container.RegisterType<IStructuredReportService, StructuredReportService>(new PerRequestLifetimeManager());

            AutoRegisterTypes(container);
        }

        /// <summary>
        /// auto Register Types
        /// </summary>
        public static void AutoRegisterTypes(IUnityContainer container)
        {
            var types = typeof(Consultation.Application.Services.IUserManagementService).Assembly.GetTypes();
            var careTypes = typeof(CareRIS.Application.Services.ICommonService).Assembly.GetTypes();

            RegisterTypes(types, container);
            RegisterTypes(careTypes, container);
        }

        public static void RegisterTypes(Type[] types, IUnityContainer container)
        {
            var services = types.Where(t => !t.IsInterface && t.Name.EndsWith("Service") && !container.IsRegistered(t));
            foreach (var service in services)
            {
                var type = service.GetInterface("I" + service.Name, true);
                if (type != null)
                {
                    container.RegisterType(type, service, new PerRequestLifetimeManager());
                }
            }
        }
    }
}
