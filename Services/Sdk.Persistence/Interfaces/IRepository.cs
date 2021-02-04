using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Sdk.Persistence.Interfaces
{
    public interface IRepository<T> where T : class, IEventRecord
    {
        Task<T> TransactionDecorator(Func<T, Task<T>> func, T entity);
        Task<T> AppendStateOfEntity(T entity);
        Task<T> GetOneEntityLastStateAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllEntitiesLastStatesAsync(Expression<Func<T, bool>> predicate);
    }
}