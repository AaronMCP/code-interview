using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.Consultation.EntityFramework;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Applicatiton.Test.Mock
{
    public class MockConsultationContext : IConsultationContext
    {
        public MockConsultationContext()
        {
            DbSetMapper = new Dictionary<Type, object>();

            this.PatientCases = new MockDbSet<PatientCase>();
            this.EMRItems = new MockDbSet<EMRItem>();
            this.EMRItemDetails = new MockDbSet<EMRItemDetail>();
            this.ConsultationDictionarys = new MockDbSet<ConsultationDictionary>();
            this.ExamModules = new MockDbSet<ExamModule>();
            this.HospitalProfiles = new MockDbSet<HospitalProfile>();
            this.Persons = new MockDbSet<Person>();
            this.PersonPatientCases = new MockDbSet<PersonPatientCase>();
            this.UserExtentions = new MockDbSet<UserExtention>();
            this.ConsultationRequests = new MockDbSet<ConsultationRequest>();
            this.ServiceTypes = new MockDbSet<ServiceType>();
            this.ConsultationRequestHistorys = new MockDbSet<ConsultationRequestHistory>();
            this.ConsultationAssigns = new MockDbSet<ConsultationAssign>();

            DbSetMapper.Add(typeof(PatientCase), this.PatientCases);
            DbSetMapper.Add(typeof(EMRItem), this.EMRItems);
            DbSetMapper.Add(typeof(EMRItemDetail), this.EMRItemDetails);
            DbSetMapper.Add(typeof(ConsultationDictionary), this.ConsultationDictionarys);
            DbSetMapper.Add(typeof(ExamModule), this.ExamModules);
            DbSetMapper.Add(typeof(HospitalProfile), this.HospitalProfiles);
            DbSetMapper.Add(typeof(Person), this.Persons);
            DbSetMapper.Add(typeof(PersonPatientCase), this.PersonPatientCases);
            DbSetMapper.Add(typeof(UserExtention), this.UserExtentions);
            DbSetMapper.Add(typeof(ConsultationRequest), this.ConsultationRequests);
            DbSetMapper.Add(typeof(ServiceType), this.ServiceTypes);
            DbSetMapper.Add(typeof(ConsultationRequestHistory), this.ConsultationRequestHistorys);
            DbSetMapper.Add(typeof(ConsultationAssign), this.ConsultationAssigns);
            
        }

        public DbSet<PatientCase> PatientCases { get; set; }
        public DbSet<EMRItem> EMRItems { get; set; }
        public DbSet<EMRItemDetail> EMRItemDetails { get; set; }
        public DbSet<ConsultationDictionary> ConsultationDictionarys { get; set; }
        public DbSet<ExamModule> ExamModules { get; set; }
        public DbSet<HospitalProfile> HospitalProfiles { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonPatientCase> PersonPatientCases { get; set; }
        public DbSet<UserExtention> UserExtentions { get; set; }
        public DbSet<ConsultationRequest> ConsultationRequests { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<ConsultationRequestHistory> ConsultationRequestHistorys { get; set; }
        public DbSet<ConsultationAssign> ConsultationAssigns { get; set; }

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
