using Hys.Platform.Data.EntityFramework;
using Hys.Platform.Domain;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.EntityFramework;
using Hys.Consultation.Domain.Interface;

namespace Hys.Consultation.Applicatiton.Test.Mock
{
    public class MockRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        public MockConsultationContext DbContext { get; private set; }
        public DbSet<TEntity> DbSet { get; private set; }

        public MockRepository(IDbContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            DbContext = (MockConsultationContext)context;
            DbSet = DbContext.Set<TEntity>() as DbSet<TEntity>;
        }

        public IEnumerable<TEntity> Get()
        {
            return DbSet.AsQueryable();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(filter).AsQueryable();
        }

        public IEnumerable<TEntity> Get<TOderKey>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize,
            Expression<Func<TEntity, TOderKey>> sortKeySelector, bool isAsc = true)
        {
            Guard.ArgumentNotNull(filter, "predicate");
            Guard.ArgumentNotNull(sortKeySelector, "sortKeySelector");
            if (isAsc)
            {
                return DbSet
                    .Where(filter)
                    .OrderBy(sortKeySelector)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
            }
            return DbSet
                .Where(filter)
                .OrderByDescending(sortKeySelector)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).AsQueryable();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).Count();
        }

        public void Update(TEntity instance)
        {
            DbSet.Remove(DbSet.FirstOrDefault(i => i.UniqueId.Equals(instance.UniqueId)));
            DbSet.Add(instance);
        }

        public void Add(TEntity instance)
        {
            DbSet.Add(instance);
        }

        public void Delete(TEntity instance)
        {
            DbSet.Remove(instance);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            
        }


        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }


        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class MockPatientCaseRepository : MockRepository<PatientCase>, IPatientCaseRepository
    {
        public MockPatientCaseRepository(IConsultationContext context)
            : base(context)
        {

        }
    }

    public class MockEMRItemRepository : MockRepository<EMRItem>, IEMRItemRepository
    {
        public MockEMRItemRepository(IConsultationContext context)
            : base(context)
        {

        }
    }

    public class MockEMRItemDetailRepository : MockRepository<EMRItemDetail>, IEMRItemDetailRepository
    {
        public MockEMRItemDetailRepository(IConsultationContext context)
            : base(context)
        {

        }
    }

    public class MockConsultationDictionaryRepository : MockRepository<ConsultationDictionary>, IConsultationDictionaryRepository
    {
        public MockConsultationDictionaryRepository(IConsultationContext context)
            : base(context)
        {

        }
    }

    public class MockExamModuleRepository : MockRepository<ExamModule>, IExamModuleRepository
    {
        public MockExamModuleRepository(IConsultationContext context)
            : base(context)
        {

        }
    }

}
