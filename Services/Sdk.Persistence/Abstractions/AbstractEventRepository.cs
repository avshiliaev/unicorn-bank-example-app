using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Interfaces;

namespace Sdk.Persistence.Abstractions
{
    public abstract class AbstractEventRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        private readonly TContext _context;
        private readonly ILogger<AbstractEventRepository<TContext, TEntity>> _logger;

        public AbstractEventRepository(
            ILogger<AbstractEventRepository<TContext, TEntity>> logger,
            TContext context
        )
        {
            _logger = logger;
            _context = context;
        }

        public async Task<TEntity> TransactionDecorator(Func<TEntity, Task<TEntity>> func, TEntity entity)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            var result = await func(entity);
            await transaction.CommitAsync();
            return result;
        }

        public async Task<TEntity> AppendState(TEntity entity)
        {
            entity.Created = DateTime.UtcNow;
            entity.Updated = entity.Created;
            var saved = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return saved.Entity;
        }
        
        public Task<TEntity> GetOneLastStateAsync(Expression<Func<TEntity, bool>> whereClause)
        {
            return _context.Set<TEntity>()
                .Where(whereClause)
                .OrderByDescending(x => x.Version)
                .FirstOrDefaultAsync();
        }
        
        public Task<List<TEntity>> GetManyLastStatesAsync(Expression<Func<TEntity, bool>> whereClause)
        {
            return _context.Set<TEntity>()
                .Where(whereClause)
                .Where(e => e.Version == _context.Set<TEntity>().Max(e2 => (int?) e2.Version))
                .ToListAsync();
        }
        
    }
}