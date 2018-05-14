#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Hys.Platform.Domain
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> Get<TOderKey>(Expression<Func<TEntity, bool>> filter, int pageIndex, int pageSize,
            Expression<Func<TEntity, TOderKey>> sortKeySelector, bool isAsc = true);

        int Count(Expression<Func<TEntity, bool>> predicate);

        void Update(TEntity instance);

        void Add(TEntity instance);

        void Delete(TEntity instance);

        void SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}