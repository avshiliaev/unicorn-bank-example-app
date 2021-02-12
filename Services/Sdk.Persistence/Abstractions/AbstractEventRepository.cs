using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Sdk.Persistence.Abstractions
{
    public abstract class AbstractEventRepository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class, IRecord
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

        public async Task<TEntity> AppendStateOfEntity(TEntity entity)
        {
            TEntity savedEntity = null;
            await using var transaction = await _context.Database.BeginTransactionAsync();

            var lastSavedState = await GetOneEntityLastStateAsync(
                e => e.EntityId == entity.EntityId &&
                     e.ProfileId == entity.ProfileId
            );

            if (lastSavedState == null)
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.Version = 1;
                savedEntity = await SaveState(entity);
                await transaction.CommitAsync();
            }

            // Check if the current version is the same as the last saved one, increment it and append.
            if (lastSavedState != null && lastSavedState.Version.Equals(entity.Version))
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.Version++;
                savedEntity = await SaveState(entity);
                await transaction.CommitAsync();
            }

            return savedEntity;
        }

        public Task<TEntity> GetOneEntityLastStateAsync(Expression<Func<TEntity, bool>> whereClause)
        {
            return _context.Set<TEntity>()
                .Where(whereClause)
                .OrderByDescending(x => x.Version)
                .FirstOrDefaultAsync();
        }

        public Task<List<TEntity>> GetAllEntitiesLastStatesAsync(Expression<Func<TEntity, bool>> whereClause)
        {
            return _context.Set<TEntity>()
                .Where(whereClause)
                .Where(e => e.Version == _context.Set<TEntity>().Max(e2 => (int?) e2.Version))
                .ToListAsync();
        }

        private async Task<TEntity> SaveState(TEntity entity)
        {
            entity.Created = DateTime.UtcNow;
            entity.Updated = entity.Created;
            var saved = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return saved.Entity;
        }
    }
}