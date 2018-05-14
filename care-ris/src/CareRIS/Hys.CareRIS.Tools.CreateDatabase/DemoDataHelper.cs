using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hys.Consultation.Application;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Services;
using Hys.Consultation.Application.Services.ServiceImpl;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Domain.Interface;
using Hys.Consultation.EntityFramework;
using Hys.Consultation.EntityFramework.Repositories;
using Hys.CrossCutting.Common.Utils;
using Hys.Platform.CrossCutting.LogContract;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.EntityFramework;
using Microsoft.Practices.Unity;

namespace Hys.CareRIS.Tools.CreateDatabase
{
    internal class DemoDataHelper
    {
        internal class UnitTestLog : ICommonLog
        {
            public void Log(LogLevel logLevel, object message)
            {
                Debug.WriteLine(message);
            }
        }

        public static UnityContainer Container { get; set; }

        public DemoDataHelper()
        {
            Container = new UnityContainer();
            Container.RegisterType<ICommonLog, UnitTestLog>();

            var risProConnectionString = ConfigurationManager.ConnectionStrings["RisProContext"].ConnectionString;
            var consultationConnectionString = ConfigurationManager.ConnectionStrings["ConsultationContext"].ConnectionString;
            Container.RegisterType<IRisProContext, RisProContext>(new InjectionConstructor(risProConnectionString, Container.Resolve<ICommonLog>()));
            Container.RegisterType<IConsultationContext, ConsultationContext>(new InjectionConstructor(consultationConnectionString, Container.Resolve<ICommonLog>()));
            AutoRegisterTypes(Container);

            var ctx = new ServiceContext("test", "test", "test", "test", "test", "test", false, "en-us");
            var loginUserServie = new LoginUserService(Container.Resolve<IConsultationContext>())
            {
                ServiceContext = ctx
            };
            Container.RegisterInstance(typeof(ILoginUserService), loginUserServie, new ContainerControlledLifetimeManager());
        }

        private void AutoRegisterTypes(UnityContainer container)
        {
            var types = typeof(IShortcutService).Assembly.GetTypes()
                .Concat(typeof(IShortcutRepository).Assembly.GetTypes())
                .Concat(typeof(ShortcutRepository).Assembly.GetTypes());

            var targetTypes = types.Where(t => !t.IsInterface && (t.Name.EndsWith("Service") || t.Name.EndsWith("Repository")) && !container.IsRegistered(t));
            foreach (var type in targetTypes)
            {
                var targetInterface = type.GetInterface("I" + type.Name, true);
                if (targetInterface != null)
                {
                    if (!container.IsRegistered(targetInterface))
                    {
                        container.RegisterType(targetInterface, type);
                    }
                }
            }
        }

        public void CreateConsultationConfigData()
        {
            Console.WriteLine("Create DemoData");

            CreateRoles();
            var hospital = CreateHospital();
            CreateUser(hospital);
        }

        private void CreateUser(HospitalProfile hospital)
        {
            Console.WriteLine("Create Users");

            var servie = Container.Resolve<IUserManagementService>();
            var configServie = Container.Resolve<IConsultationConfigurationService>();
            var risContext = Container.Resolve<RisProContext>();
            var users = risContext.Set<User>().AsQueryable().ProjectTo<UserDto>();
            foreach (var u in users)
            {
                u.Roles = configServie.GetRoles().ToList();
                u.HospitalID = hospital.UniqueID;
                servie.SaveUser(u);
            }
        }

        private void CreateRoles()
        {
            Console.WriteLine("Create Default Roles");
            var service = Container.Resolve<IConsultationConfigurationService>();
            var list = new List<RoleDto>
            {
                new RoleDto
                {
                    UniqueID = "2ee2fd0c-100d-b934-d0c2-f24ff16039e9",
                    RoleName = "超级管理员",
                    Permissions = ""
                },new RoleDto
                {
                    UniqueID = "4d1fa440-28b7-b5d2-976f-9db7f84b5d9d",
                    RoleName = "远程专家",
                    Permissions = "004ViewCase,010SearchCase,002ConsultationCenterViewInfo,015ViewConsultation,018PrintResult,004RefuseRequest,005StartMeeting,001ExpertViewInfo,002ExpertStartMeeting,003ExpertRefuseRequest"
                },new RoleDto
                {
                    UniqueID = "4dc3dbb2-27e0-9eb1-a106-003cae158b16",
                    RoleName = "远程申请医生",
                    Permissions = "001CreateNewCase,002CombineCase,003EditProcedure,004ViewCase,005EditCaseExceptCompleted,006EditPatientExceptCompleted,007EditHistoryAndDiagnosisExceptCompleted,008EditRequestAndRequirmentExceptCompleted,009EditReceiverNotAccepted,010SearchCase,011RequestConsultation,012SubmitRequest,013CancelRequest,014RemoteRequestForceEnd,015ViewConsultation,016RequestCancelConsultation,017RequestReconsideration,018PrintResult,002ConsultationCenterViewInfo,005StartMeeting"
                },new RoleDto
                {
                    UniqueID = "93321a30-891f-6c86-6b4d-46f17f13dfae",
                    RoleName = "会诊管理员",
                    Permissions = "003EditProcedure,004ViewCase,005EditCaseExceptCompleted,009EditReceiverNotAccepted,001EditReceiverAccept,010SearchCase,014RemoteRequestForceEnd,015ViewConsultation,018PrintResult,002ConsultationCenterViewInfo,004RefuseRequest,003AcceptRequest,005StartMeeting,001ExpertViewInfo,002ExpertStartMeeting,003ExpertRefuseRequest"
                },new RoleDto
                {
                    UniqueID = "d6e52828-6c4f-2efe-c2f0-700de9375e75",
                    RoleName = "Site管理员",
                    Permissions = ""
                }
            };

            list.ForEach(r =>
            {
                r.Description = r.RoleName;
                r.IsDeleted = false;
                r.IsSystem = true;
                r.LastEditTime = DateTime.Now;
                r.Status = true;
                service.SaveRole(r);
            });
        }

        private HospitalProfile CreateHospital()
        {
            Console.WriteLine("Create Hospital");

            var context = Container.Resolve<ConsultationContext>();
            var profile = new HospitalProfile
            {
                UniqueID = Guid.NewGuid().ToString(),
                LastEditUser = "Arbin",
                HospitalName = "重医",
                IsConsultation = true,
                DicomPrefix = "Arbin",
                LastEditTime = DateTime.Now
            };
            context.Set<HospitalProfile>().Add(profile);
            context.SaveChanges();
            return profile;
        }
    }
}