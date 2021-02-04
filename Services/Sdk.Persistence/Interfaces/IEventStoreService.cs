using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sdk.Persistence.Interfaces
{
    public interface IEventStoreService<TEntity> where TEntity : class, IEventRecord
    {
        Task<TEntity> TransactionDecorator(Func<TEntity, Task<TEntity>> func, TEntity entity);
        Task<TEntity> AppendStateOfEntity(TEntity approvalEntity);

        Task<List<TEntity>> GetAllEntitiesLastStatesAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetOneEntityLastStateAsync(Expression<Func<TEntity, bool>> predicate);
    }
}