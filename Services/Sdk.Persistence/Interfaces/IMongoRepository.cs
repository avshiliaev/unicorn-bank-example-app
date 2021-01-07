using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Sdk.Persistence.Interfaces
{
    public interface IMongoRepository<TEntity> where TEntity : class, IMongoEntity
    {
        List<TEntity> GetAll(string profileId);
        List<TEntity> GetManyByParameter(Expression<Func<TEntity, bool>> predicate);
        public TEntity GetSingleByParameter(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(string id);
        TEntity Create(TEntity entity);
        TEntity UpdatePassively(string id, TEntity entityIn);
        TEntity UpdateIgnoreConcurrency(string id, UpdateDefinition<TEntity> updateDefinition);
        TEntity Remove(TEntity entityIn);
        bool Remove(string id);
        IEnumerator<ChangeStreamDocument<TEntity>> SubscribeToChangesStreamMany(string pipeline);
    }
}