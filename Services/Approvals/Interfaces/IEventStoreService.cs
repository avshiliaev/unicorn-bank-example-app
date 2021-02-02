using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sdk.Persistence.Interfaces;

namespace Approvals.Interfaces
{
    public interface IEventStoreService<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity?> CreateApprovalAsync(TEntity approvalEntity);

        Task<List<TEntity?>> GetManyLastVersionAsync(Expression<Func<TEntity?, bool>> predicate);

        Task<TEntity?> GetOneAsync(Expression<Func<TEntity?, bool>> predicate);
    }
}