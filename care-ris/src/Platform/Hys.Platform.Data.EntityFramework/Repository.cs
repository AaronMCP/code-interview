#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Hys.Platform.Domain;
using log4net;
using Microsoft.Practices.Unity.Utility;
using System.Threading.Tasks;

#endregion

namespace Hys.Platform.Data.EntityFramework
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private static ILog logger = LogManager.GetLogger("Repository");
        private object lockObject = new object();
        public DbContext DbContext { get; private set; }
        public DbSet<TEntity> DbSet { get; private set; }

        public Repository(IDbContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            DbContext = (DbContext)context;
            DbSet = DbContext.Set<TEntity>();
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

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).CountAsync();
        }

        public void Update(TEntity instance)
        {
            Guard.ArgumentNotNull(instance, "instance");
            DbSet.Attach(instance);
            DbContext.Entry(instance).State = EntityState.Modified;
        }

        public void Add(TEntity instance)
        {
            Guard.ArgumentNotNull(instance, "instance");
            DbSet.Attach(instance);
            DbContext.Entry(instance).State = EntityState.Added;
        }

        public void Delete(TEntity instance)
        {
            Guard.ArgumentNotNull(instance, "instance");
            DbSet.Attach(instance);
            DbContext.Entry(instance).State = EntityState.Deleted;
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}