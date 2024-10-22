using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sdk.Persistence.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        // Task<PaginatedList<T>> GetAsync(PaginatedCriteria paginatedCriteria);
        Task<List<T>> ListAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetOneByParameterAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetManyByParameterAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> UpdatePassivelyAsync(T entity);
        Task<T> UpdateActivelyAsync(T entity);
        Task<T> DeleteAsync(Guid id);
    }
}