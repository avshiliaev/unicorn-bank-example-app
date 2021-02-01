using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using Sdk.Persistence.Interfaces;

namespace Sdk.Persistence.Abstractions
{
    public abstract class AbstractMongoRepository<TEntity> : IMongoRepository<TEntity>
        where TEntity : class, IMongoEntity
    {
        private readonly IMongoCollection<TEntity> _mongoCollection;

        public AbstractMongoRepository(IMongoSettingsModel settings)
        {
            Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);

            _mongoCollection = database.GetCollection<TEntity>(settings.CollectionName);
        }

        public MongoClient Client { get; }

        public List<TEntity> GetAll(string profileId)
        {
            return _mongoCollection.Find(e => e.ProfileId == profileId).ToList();
        }

        public List<TEntity> GetManyByParameter(Expression<Func<TEntity, bool>> predicate, int count)
        {
            return _mongoCollection.Find(predicate).Limit(count).ToList();
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

        public TEntity Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> updateDefinition)
        {
            var result = _mongoCollection.FindOneAndUpdate<TEntity>(filter, updateDefinition);
            if (result != null && !string.IsNullOrEmpty(result.Id))
                return result;
            return null;
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

        public IEnumerator<ChangeStreamDocument<TEntity>> SubscribeToChangesStreamMany(
            PipelineDefinition<ChangeStreamDocument<TEntity>, ChangeStreamDocument<TEntity>> pipeline
        )
        {
            if (pipeline == null) return null;

            var options = new ChangeStreamOptions
            {
                FullDocument = ChangeStreamFullDocumentOption.UpdateLookup
            };

            var cursor = _mongoCollection.Watch(pipeline, options);
            var enumerator = cursor.ToEnumerable().GetEnumerator();
            return enumerator;
        }
    }
}