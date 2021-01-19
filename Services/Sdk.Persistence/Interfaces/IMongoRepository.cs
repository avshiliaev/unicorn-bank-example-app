using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Sdk.Persistence.Interfaces
{
    public interface IMongoRepository<TEntity> where TEntity : class, IMongoEntity
    {
        public MongoClient Client { get; }

        List<TEntity> GetAll(string profileId);
        List<TEntity> GetManyByParameter(Expression<Func<TEntity, bool>> predicate);
        public TEntity GetSingleByParameter(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(string id);
        TEntity Create(TEntity entity);
        TEntity Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition);
        TEntity Remove(TEntity entityIn);
        bool Remove(string id);
        IEnumerator<ChangeStreamDocument<TEntity>> SubscribeToChangesStreamMany(BsonDocument[] pipeline);
    }
}