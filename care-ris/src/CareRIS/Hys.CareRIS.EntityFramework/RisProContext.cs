using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Hys.Platform.CrossCutting.LogContract;
using Hys.Platform.Data.EntityFramework;
using Hys.Platform.Domain;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Entities.Referral;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Common;

namespace Hys.CareRIS.EntityFramework
{
    public interface IRisProContext : IDbContext { }

    public partial class RisProContext : DbContext, IRisProContext
    {
        private readonly ICommonLog _logger;

        public RisProContext(string connectionString, ICommonLog logger)
            : base(connectionString)
        {
            _logger = logger;
            _logger.Log(LogLevel.Debug, "RisProContext.ctor");
            Database.Log = log => Debug.WriteLine(log);
            this.Database.CommandTimeout = 0;
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : Entity { return base.Set<TEntity>(); }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Procedure> Procedures { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Procedurecode> ProcedureCodes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ModalityType> ModalityTypes { get; set; }
        public virtual DbSet<Modality> Modalities { get; set; }
        public virtual DbSet<ReportFile> ReportFiles { get; set; }
        public virtual DbSet<Dictionary> Dictionaries { get; set; }
        public virtual DbSet<DictionaryValue> DictionaryValues { get; set; }
        public virtual DbSet<PrintTemplate> PrintTemplates { get; set; }
        public virtual DbSet<PrintTemplateFields> PrintTemplateFields { get; set; }
        public virtual DbSet<ApplyDept> ApplyDepts { get; set; }
        public virtual DbSet<ApplyDoctor> ApplyDoctors { get; set; }
        public virtual DbSet<SystemProfile> SystemProfiles { get; set; }
        public virtual DbSet<SiteProfile> SiteProfiles { get; set; }
        public virtual DbSet<RoleProfile> RoleProfiles { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<User2Domain> User2Domains { get; set; }
        public virtual DbSet<ReportDelPool> ReportDelPools { get; set; }
        public virtual DbSet<ReportList> ReportLists { get; set; }
        public virtual DbSet<Sync> Syncs { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<RoleToUser> RoleToUsers { get; set; }
        public virtual DbSet<Shortcut> Shortcuts { get; set; }
        public virtual DbSet<ReportPrintLog> ReportPrintLogs { get; set; }
        public virtual DbSet<ReferralList> ReferralList { get; set; }

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

            modelBuilder.Entity<Order>()
                .Property(e => e.TotalFee)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Order>()
                .Property(e => e.HisID)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.CardNo)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.BodyWeight)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Procedure>()
                .Property(e => e.Deposit)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Procedure>()
                .Property(e => e.Charge)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Procedure>()
                .Property(e => e.QueueNo)
                .IsFixedLength();

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

        public DbEntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        protected override void Dispose(bool disposing) { _logger.Log(LogLevel.Debug, "RisProContext.Dispose"); base.Dispose(disposing); }


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
}
