using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Hys.Consultation.Domain.Entities;
using Hys.Platform.CrossCutting.LogContract;
using Hys.Platform.Data.EntityFramework;
using Hys.Platform.Domain;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Common;

namespace Hys.Consultation.EntityFramework
{
    public interface IConsultationContext : IDbContext { }

    public class ConsultationContext : DbContext, IConsultationContext
    {
        private readonly ICommonLog _logger;
        private readonly IInitialDataService _InitialDataService;

        public ConsultationContext(string connectionString, ICommonLog logger)
            : base(connectionString)
        {
            _logger = logger;
            _logger.Log(LogLevel.Debug, "ConsultationContext.ctor");
            Database.Log = log => Debug.WriteLine(log);
            this.Database.CommandTimeout = 0;
        }

        public ConsultationContext(string connectionString, ICommonLog logger, IInitialDataService initialDataService)
            : this(connectionString, logger)
        {
            _InitialDataService = initialDataService;
        }

        public void InitialData(bool isV1Initialed)
        {
            if (_InitialDataService != null)
            {
                _InitialDataService.InitialData(this, isV1Initialed);
            }
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                    type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        public new void SaveChanges()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    var errorMessage = String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    Debug.WriteLine(errorMessage);
                    _logger.Log(LogLevel.Error, errorMessage);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        var propertyMessage = String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        _logger.Log(LogLevel.Error, propertyMessage);
                        Debug.WriteLine(propertyMessage);
                    }
                }
                throw;
            }
        }

        protected override void Dispose(bool disposing)
        {
            _logger.Log(LogLevel.Debug, "ConsultationContext.Dispose"); base.Dispose(disposing);
        }

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<PatientCase> PatientCases { get; set; }

        public virtual DbSet<HospitalProfile> HospitalProfiles { get; set; }

        public virtual DbSet<ConsultationRequest> ConsultationRequests { get; set; }

        public virtual DbSet<ConsultationRequestHistory> ConsultationRequestRequesties { get; set; }

        public virtual DbSet<ConsultationDictionary> ConsultationDictionary { get; set; }

        public virtual DbSet<InitialDataHistory> InitialDataHistory { get; set; }

        public virtual DbSet<ExamModule> ExamModule { get; set; }

        public virtual DbSet<ServiceType> ServiceType { get; set; }

        public virtual DbSet<Role> Role { get; set; }

        public virtual DbSet<UserExtention> UserExtention { get; set; }

        public virtual DbSet<NotificationConfig> NotificationConfig { get; set; }

        public virtual DbSet<SysConfig> SysConfig { get; set; }

        public virtual DbSet<ConsultationPatientNo> ConsultationPatientNo { get; set; }

        public int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sql, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, CancellationToken cancellationToken, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommandAsync(transactionalBehavior, sql, cancellationToken, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommandAsync(transactionalBehavior, sql, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommandAsync(sql, cancellationToken, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public DbRawSqlQuery SqlQuery(Type elementType, string sql, params object[] parameters)
        {
            return base.Database.SqlQuery(elementType, sql, parameters);
        }

        public DbRawSqlQuery<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return base.Database.SqlQuery<TElement>(sql, parameters);
        }

        public void UseTransaction(DbTransaction transaction)
        {
            base.Database.UseTransaction(transaction);
        }
    }

    public class MigrationsContextFactory : IDbContextFactory<ConsultationContext>
    {
        public ConsultationContext Create()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConsultationContext"].ConnectionString;
            return new ConsultationContext(connectionString, new Log4netImpl(), null);
        }
    }

    public class ConsultationDbMigrationsConfiguration : DbMigrationsConfiguration<ConsultationContext>
    {
        public ConsultationDbMigrationsConfiguration()
        {
            base.AutomaticMigrationsEnabled = true;
            base.AutomaticMigrationDataLossAllowed = true;
            base.ContextKey = "Hys.Consultation.EntityFramework.ConsultationDbMigrationsConfiguration";
        }

        /// <summary>
        /// Add initialize data
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ConsultationContext context)
        {
            var isV1Initialed = Convert.ToBoolean(ConfigurationManager.AppSettings["v1Initial"]);
            context.InitialData(isV1Initialed);

            base.Seed(context);
        }
    }
}