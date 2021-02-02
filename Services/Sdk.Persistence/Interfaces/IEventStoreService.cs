using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sdk.Persistence.Interfaces
{
    public interface IEventStoreService<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> CreateRecordAsync(TEntity approvalEntity);

        Task<List<TEntity>> GetManyRecordsLastVersionAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetOneRecordAsync(Expression<Func<TEntity, bool>> predicate);
    }
}