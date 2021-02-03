using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Sdk.Persistence.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> TransactionDecorator(Func<T, Task<T>> func, T entity);
        Task<T> AppendState(T entity);
        Task<T> GetOneLastStateAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetManyLastStatesAsync(Expression<Func<T, bool>> predicate);
    }
}