using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Interfaces;

namespace Sdk.Persistence.Abstractions
{
    public abstract class AbstractRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        private readonly TContext _context;
        private readonly ILogger<AbstractRepository<TContext, TEntity>> _logger;

        public AbstractRepository(
            ILogger<AbstractRepository<TContext, TEntity>> logger,
            TContext context
        )
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<TEntity>> ListAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context
                .Set<TEntity>()
                .FirstOrDefaultAsync(acc => acc.Id == id);
        }

        public async Task<TEntity> GetByParameterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).SingleAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.Created = DateTime.UtcNow;
            entity.Updated = entity.Created;
            var saved = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return saved.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (
                entity == null ||
                Guid.Empty.Equals(entity.Id) ||
                !await _context.Set<TEntity>()
                    .Where(acc => acc.Id.Equals(entity.Id))
                    .Where(acc => acc.Version.Equals(entity.Version - 1))
                    .AnyAsync()
            )
                return null;

            entity.Updated = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(Guid id)
        {
            var account = await _context.Set<TEntity>().FindAsync(id);
            if (account == null) return null;

            _context.Set<TEntity>().Remove(account);
            await _context.SaveChangesAsync();
            return account;
        }
    }
}