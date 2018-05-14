using Hys.Platform.Domain;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Applicatiton.Test.Mock
{
    public class MockRisProContext : IRisProContext
    {
        public MockRisProContext()
        {
            DbSetMapper = new Dictionary<Type, object>();

            this.Patients = new MockDbSet<Patient>();
            this.Orders = new MockDbSet<Order>();
            this.Procedures = new MockDbSet<Procedure>();
            this.Reports = new MockDbSet<Report>();
            this.ReportFiles = new MockDbSet<ReportFile>();
            this.PrintTemplates = new MockDbSet<PrintTemplate>();
            this.PrintTemplateFields = new MockDbSet<PrintTemplateFields>();
            this.ReportTemplates=new MockDbSet<ReportTemplate>();
            this.ReportTemplateDirecs=new MockDbSet<ReportTemplateDirec>();
            this.Modalities = new MockDbSet<Modality>();
            this.ApplyDepts = new MockDbSet<ApplyDept>();
            this.ApplyDoctors = new MockDbSet<ApplyDoctor>();
            this.Dictionaries = new MockDbSet<Dictionary>();
            this.DictionaryValues = new MockDbSet<DictionaryValue>();
            this.SystemProfiles = new MockDbSet<SystemProfile>();
            this.SiteProfiles = new MockDbSet<SiteProfile>();
            this.RoleProfiles = new MockDbSet<RoleProfile>();
            this.UserProfiles = new MockDbSet<UserProfile>();
            this.Users = new MockDbSet<User>();
            this.ReportList = new MockDbSet<ReportList>();
            this.ReportDelPools = new MockDbSet<ReportDelPool>();
            this.Syncs = new MockDbSet<Sync>();
            this.AccessionNumberLists = new MockDbSet<AccessionNumberList>();
            this.BodySystemMaps = new MockDbSet<BodySystemMap>();
            this.ProcedureCodes = new MockDbSet<Procedurecode>();
            this.ModalityTypes = new MockDbSet<ModalityType>();
            this.GWDataIndexs = new MockDbSet<GWDataIndex>();
            this.GWPatients = new MockDbSet<GWPatient>();
            this.GWOrders = new MockDbSet<GWOrder>();
            this.GWReports = new MockDbSet<GWReport>();
            this.Shortcuts = new MockDbSet<Shortcut>();
            this.Sites = new MockDbSet<Site>();
            this.ReportPrintLogs = new MockDbSet<ReportPrintLog>();
            this.OnlineClients = new MockDbSet<OnlineClient>();
            this.RequestLists = new MockDbSet<RequestList>();

            DbSetMapper.Add(typeof(Patient), this.Patients);
            DbSetMapper.Add(typeof(Order), this.Orders);
            DbSetMapper.Add(typeof(Procedure), this.Procedures);
            DbSetMapper.Add(typeof(Report), this.Reports);
            DbSetMapper.Add(typeof(ReportFile), this.ReportFiles);
            DbSetMapper.Add(typeof(PrintTemplate), this.PrintTemplates);
            DbSetMapper.Add(typeof(PrintTemplateFields), this.PrintTemplateFields);
            DbSetMapper.Add(typeof(ReportTemplate), this.ReportTemplates);
            DbSetMapper.Add(typeof(ReportTemplateDirec), this.ReportTemplateDirecs);
            DbSetMapper.Add(typeof(Modality), this.Modalities);
            DbSetMapper.Add(typeof(ApplyDept), this.ApplyDepts);
            DbSetMapper.Add(typeof(ApplyDoctor), this.ApplyDoctors);
            DbSetMapper.Add(typeof(Dictionary), this.Dictionaries);
            DbSetMapper.Add(typeof(DictionaryValue), this.DictionaryValues);
            DbSetMapper.Add(typeof(SystemProfile), this.SystemProfiles);
            DbSetMapper.Add(typeof(SiteProfile), this.SiteProfiles);
            DbSetMapper.Add(typeof(RoleProfile), this.RoleProfiles);
            DbSetMapper.Add(typeof(UserProfile), this.UserProfiles);
            DbSetMapper.Add(typeof(User), this.Users);
            DbSetMapper.Add(typeof(ReportList), this.ReportList);
            DbSetMapper.Add(typeof(ReportDelPool), this.ReportDelPools);
            DbSetMapper.Add(typeof(Sync), this.Syncs);
            DbSetMapper.Add(typeof(AccessionNumberList), this.AccessionNumberLists);
            DbSetMapper.Add(typeof(BodySystemMap), this.BodySystemMaps);
            DbSetMapper.Add(typeof(Procedurecode), this.ProcedureCodes);
            DbSetMapper.Add(typeof(ModalityType), this.ModalityTypes);
            DbSetMapper.Add(typeof(GWDataIndex), this.GWDataIndexs);
            DbSetMapper.Add(typeof(GWPatient), this.GWPatients);
            DbSetMapper.Add(typeof(GWOrder), this.GWOrders);
            DbSetMapper.Add(typeof(GWReport), this.GWReports);
            DbSetMapper.Add(typeof(Shortcut), this.Shortcuts);
            DbSetMapper.Add(typeof(Site), this.Sites);
            DbSetMapper.Add(typeof(ReportPrintLog), this.ReportPrintLogs);
            DbSetMapper.Add(typeof(OnlineClient), this.OnlineClients);
            DbSetMapper.Add(typeof(RequestList), this.RequestLists);
            DbSetMapper.Add(typeof(Requisition), this.Requisitions);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportFile> ReportFiles { get; set; }
        public DbSet<PrintTemplate> PrintTemplates { get; set; }
        public DbSet<PrintTemplateFields> PrintTemplateFields { get; set; }
        public DbSet<ReportTemplate> ReportTemplates { get; set; }
        public DbSet<ReportTemplateDirec> ReportTemplateDirecs { get; set; }
        public DbSet<Modality> Modalities { get; set; }
        public DbSet<ApplyDept> ApplyDepts { get; set; }
        public DbSet<ApplyDoctor> ApplyDoctors { get; set; }
        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<DictionaryValue> DictionaryValues { get; set; }
        public DbSet<SystemProfile> SystemProfiles { get; set; }
        public DbSet<SiteProfile> SiteProfiles { get; set; }
        public DbSet<RoleProfile> RoleProfiles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ReportList> ReportList { get; set; }
        public DbSet<ReportDelPool> ReportDelPools { get; set; }
        public DbSet<Sync> Syncs { get; set; }
        public DbSet<AccessionNumberList> AccessionNumberLists { get; set; }
        public DbSet<BodySystemMap> BodySystemMaps { get; set; }
        public DbSet<Procedurecode> ProcedureCodes { get; set; }
        public DbSet<ModalityType> ModalityTypes { get; set; }
        public DbSet<GWDataIndex> GWDataIndexs { get; set; }
        public DbSet<GWPatient> GWPatients { get; set; }
        public DbSet<GWOrder> GWOrders { get; set; }
        public DbSet<GWReport> GWReports { get; set; }
        public DbSet<Shortcut> Shortcuts { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<ReportPrintLog> ReportPrintLogs { get; set; }
        public DbSet<OnlineClient> OnlineClients { get; set; }
        public DbSet<RequestList> RequestLists { get; set; }
        public DbSet<Requisition> Requisitions { get; set; }
        public Dictionary<Type, object> DbSetMapper { get; set; }

        public void SaveChanges()  { }

        public DbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            return DbSetMapper[typeof(TEntity)] as DbSet<TEntity>;
        }


        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, System.Threading.CancellationToken cancellationToken, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, System.Threading.CancellationToken cancellationToken, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public System.Data.Entity.Infrastructure.DbRawSqlQuery SqlQuery(Type elementType, string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public System.Data.Entity.Infrastructure.DbRawSqlQuery<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void UseTransaction(System.Data.Common.DbTransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
