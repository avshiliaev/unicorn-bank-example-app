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

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.Created = DateTime.UtcNow;
            entity.Updated = entity.Created;
            var saved = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return saved.Entity;
        }

        public Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).SingleOrDefaultAsync();
        }

        public Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToListAsync();
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

        public async Task<TEntity> UpdateOptimisticallyAsync(TEntity entity)
        {
            if (
                entity == null ||
                Guid.Empty.Equals(entity.Id) ||
                !await _context.Set<TEntity>()
                    .Where(acc => acc.Id.Equals(entity.Id))
                    .Where(acc => acc.Version.Equals(entity.Version))
                    .AnyAsync()
            )
                return null;

            entity.Updated = DateTime.UtcNow;
            entity.Version = entity.Version + 1;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<List<TEntity>> GetManyLastVersionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>()
                .Where(
                    e => e.Version == _context.Set<TEntity>().Max(e2 => (int?) e2.Version)
                )
                .ToListAsync();
        }
    }
}