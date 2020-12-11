using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sdk.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        // Task<PaginatedList<T>> GetAsync(PaginatedCriteria paginatedCriteria);
        Task<List<T>> ListAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByParameterAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(Guid id);
    }
}