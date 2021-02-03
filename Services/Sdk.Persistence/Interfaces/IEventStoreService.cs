using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sdk.Persistence.Interfaces
{
    public interface IEventStoreService<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> TransactionDecorator(Func<TEntity, Task<TEntity>> func, TEntity entity);
        Task<TEntity> AppendState(TEntity approvalEntity);

        Task<List<TEntity>> GetManyLastStatesAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetOneLastStateAsync(Expression<Func<TEntity, bool>> predicate);
    }
}