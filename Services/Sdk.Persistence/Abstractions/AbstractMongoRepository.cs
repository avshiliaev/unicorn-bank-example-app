using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using Sdk.Persistence.Interfaces;

namespace Sdk.Persistence.Abstractions
{
    public abstract class AbstractMongoRepository<TEntity> : IMongoRepository<TEntity>
        where TEntity : class, IMongoEntity
    {
        private readonly IMongoCollection<TEntity> _mongoCollection;
        private readonly MongoClient _client;

        public AbstractMongoRepository(IMongoSettingsModel settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            var database = _client.GetDatabase(settings.DatabaseName);

            _mongoCollection = database.GetCollection<TEntity>(settings.CollectionName);
        }

        public List<TEntity> GetAll(string profileId)
        {
            return _mongoCollection.Find(e => e.ProfileId == profileId).ToList();
        }
        
        public List<TEntity> GetManyByParameter(Expression<Func<TEntity, bool>> predicate)
        {
            return _mongoCollection.Find(predicate).ToList();
        }
        
        public TEntity GetSingleByParameter(Expression<Func<TEntity, bool>> predicate)
        {
            return _mongoCollection.Find(predicate).Single();
        }

        public TEntity Get(string id)
        {
            return _mongoCollection.Find(e => e.Id == id).FirstOrDefault();
        }

        public TEntity Create(TEntity entity)
        {
            _mongoCollection.InsertOne(entity);
            return entity;
        }
        
        public TEntity UpdatePassively(string id, TEntity entityIn)
        {
            if (
                entityIn == null ||
                string.IsNullOrEmpty(id) ||
                !_mongoCollection
                    .Find(e => e.Id == id && e.Version.Equals(entityIn.Version - 1))
                    .Any()
            )
                return null;
            
            var result = _mongoCollection.ReplaceOne(e => e.Id == id, entityIn);
            if (result.IsAcknowledged)
                return entityIn;
            return null;
        }
        
        public TEntity UpdateIgnoreConcurrency(string id, UpdateDefinition<TEntity> updateDefinition)
        {
            var filter = new BsonDocument("_id", id);
            var result = _mongoCollection.FindOneAndUpdate<TEntity>(filter, updateDefinition);
            return result;
        }

        public TEntity Remove(TEntity entityIn)
        {
            var result = _mongoCollection.DeleteOne(e => e.Id == entityIn.Id);
            if (result.IsAcknowledged)
                return entityIn;
            return null;
        }

        public bool Remove(string id)
        {
            var result = _mongoCollection.DeleteOne(e => e.Id == id);
            return result.IsAcknowledged;
        }

        public IEnumerator<ChangeStreamDocument<TEntity>> SubscribeToChangesStreamMany(string pipeline)
        {
            if (string.IsNullOrEmpty(pipeline)) return null;
            
            var options = new ChangeStreamOptions
            {
                FullDocument = ChangeStreamFullDocumentOption.UpdateLookup
            };
            var fullPipeline = new EmptyPipelineDefinition<ChangeStreamDocument<TEntity>>()
                .Match("{ operationType: { $in: [ 'insert', 'update', 'replace' ] } }")
                .Match(pipeline);
            var cursor = _mongoCollection.Watch(fullPipeline, options);
            var enumerator = cursor.ToEnumerable().GetEnumerator();
            return enumerator;
        }
    }
}