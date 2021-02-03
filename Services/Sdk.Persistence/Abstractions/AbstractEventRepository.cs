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
    public abstract class AbstractEventRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        public readonly TContext Context;
        private readonly ILogger<AbstractEventRepository<TContext, TEntity>> _logger;

        public AbstractEventRepository(
            ILogger<AbstractEventRepository<TContext, TEntity>> logger,
            TContext context
        )
        {
            _logger = logger;
            Context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.Created = DateTime.UtcNow;
            entity.Updated = entity.Created;
            var saved = await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return saved.Entity;
        }

        public Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).SingleOrDefaultAsync();
        }

        public Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (
                entity == null ||
                Guid.Empty.Equals(entity.Id) ||
                !await Context.Set<TEntity>()
                    .Where(acc => acc.Id.Equals(entity.Id))
                    .Where(acc => acc.Version.Equals(entity.Version - 1))
                    .AnyAsync()
            )
                return null;

            entity.Updated = DateTime.UtcNow;
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateOptimisticallyAsync(TEntity entity)
        {
            if (
                entity == null ||
                Guid.Empty.Equals(entity.Id) ||
                !await Context.Set<TEntity>()
                    .Where(acc => acc.Id.Equals(entity.Id))
                    .Where(acc => acc.Version.Equals(entity.Version))
                    .AnyAsync()
            )
                return null;

            entity.Updated = DateTime.UtcNow;
            entity.Version = entity.Version + 1;
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        public Task<List<TEntity>> GetManyLastVersionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>()
                .Where(
                    e => e.Version == Context.Set<TEntity>().Max(e2 => (int?) e2.Version)
                )
                .ToListAsync();
        }
    }
}