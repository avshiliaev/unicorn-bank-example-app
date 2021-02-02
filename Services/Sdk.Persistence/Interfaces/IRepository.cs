using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sdk.Persistence.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> GetOneAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetManyLastVersionAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> UpdateOptimisticallyAsync(T entity);
    }
}