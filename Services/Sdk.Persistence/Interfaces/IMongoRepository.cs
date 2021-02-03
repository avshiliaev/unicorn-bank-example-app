using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace Sdk.Persistence.Interfaces
{
    public interface IMongoRepository<TEntity> where TEntity : class, IMongoEntity
    {
        public MongoClient Client { get; }

        List<TEntity> GetAll(string profileId);
        List<TEntity> GetManyByParameter(Expression<Func<TEntity, bool>> predicate, int count);
        public TEntity GetSingleByParameter(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(string id);
        TEntity Create(TEntity entity);
        TEntity Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        TEntity Remove(TEntity entityIn);
        bool RemoveById(string id);

        IEnumerator<ChangeStreamDocument<TEntity>> SubscribeToChangesStreamMany(
            PipelineDefinition<ChangeStreamDocument<TEntity>, ChangeStreamDocument<TEntity>> pipeline
        );
    }
}