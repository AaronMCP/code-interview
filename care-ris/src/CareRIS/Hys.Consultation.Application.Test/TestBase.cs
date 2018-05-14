using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Hys.Consultation.Application.Services;
using Hys.Consultation.Application.Services.ServiceImpl;
using Hys.Consultation.Application.Test.Utils;
using Hys.Consultation.Domain.Interface;
using Hys.Consultation.EntityFramework;
using Hys.Consultation.EntityFramework.Repositories;
using Hys.CrossCutting.Common.Extensions;
using Hys.CrossCutting.Common.Utils;
using Hys.Platform.CrossCutting.LogContract;
using Hys.CareRIS.EntityFramework;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hys.Consultation.Application.Test
{
    [TestClass]
    public class TestBase
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Container = new UnityContainer();
            Container.RegisterType<ICommonLog, UnitTestLog>();
            Container.RegisterType<IInitialDataService, InitialDataService>();

            var risProConnectionString = ConfigurationManager.ConnectionStrings["RisProContext"].ConnectionString;
            var consultationConnectionString = ConfigurationManager.ConnectionStrings["ConsultationContext"].ConnectionString;
            Container.RegisterType<IRisProContext, RisProContext>(new InjectionConstructor(risProConnectionString, Container.Resolve<ICommonLog>()));
            Container.RegisterType<IConsultationContext, ConsultationContext>(new InjectionConstructor(consultationConnectionString, Container.Resolve<ICommonLog>(), Container.Resolve<IInitialDataService>()));
            AutoRegisterTypes(Container);


            var ctx = new ServiceContext("test", "test", "test", "test", "test", "test", false, "en-us");
            var loginUserServie = new LoginUserService(Container.Resolve<IConsultationContext>())
            {
                ServiceContext = ctx
            };
            Container.RegisterInstance(typeof(ILoginUserService), loginUserServie, new ContainerControlledLifetimeManager());


            // common out this method when you are doing performance testing.
            // check DB CompatibleWithModel have some cost may effect your testing result.
            var consultationContext = Container.Resolve<IConsultationContext>() as ConsultationContext;
            if (consultationContext != null)
            {
                consultationContext.Database.Log = null;
                InitDatabase(consultationContext.Database, "Scripts");
            }

            var risProContext = Container.Resolve<IRisProContext>() as RisProContext;
            if (risProContext != null)
            {
                //risProContext.Database.Log = null;
                //InitDatabase(risProContext.Database, "");
            }
        }

        public static UnityContainer Container { get; private set; }

        /// <summary>
        /// auto Register Types
        /// </summary>
        private static void AutoRegisterTypes(IUnityContainer container)
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

        private static void InitDatabase(Database database, string initScript)
        {
            if (database.CreateIfNotExists())
            {
                if (!string.IsNullOrWhiteSpace(initScript))
                {
                    InitData(database, initScript);
                }
            }
            else if (!database.CompatibleWithModel(true))
            {
                database.Delete();
                InitDatabase(database, initScript);
            }
        }

        private static void InitData(Database database, string initScript)
        {
            Directory.GetFiles(initScript, "*.sql", SearchOption.AllDirectories).ForEach(s =>
            {
                var sql = File.ReadAllText(s);
                if (!String.IsNullOrWhiteSpace(sql))
                {
                    try
                    {
                        database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, sql.Replace("GO", " ").Replace("Use GCConsultation", " "));
                    }
                    catch (SqlException ex)
                    {
                        //Ignore sql exception
                    }
                }
            });
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Container.Dispose();
        }
    }
}