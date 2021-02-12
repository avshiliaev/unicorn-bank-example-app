using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sdk.Interfaces;

namespace Sdk.Persistence.Interfaces
{
    public interface IRepository<T> where T : class, IRecord
    {
        Task<T> AppendStateOfEntity(T entity);
        Task<T> GetOneEntityLastStateAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllEntitiesLastStatesAsync(Expression<Func<T, bool>> predicate);
    }
}