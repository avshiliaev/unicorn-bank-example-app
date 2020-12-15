using System.Collections.Generic;
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
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _mongoCollection = database.GetCollection<TEntity>(settings.CollectionName);
        }

        public List<TEntity> GetAll(string profileId)
        {
            return _mongoCollection.Find(e => e.ProfileId == profileId).ToList();
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

        // TODO implement the version check!

        public TEntity Update(string id, TEntity entityIn)
        {
            var result = _mongoCollection.ReplaceOne(e => e.Id == id, entityIn);
            if (result.IsAcknowledged)
                return entityIn;
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

        public IEnumerator<ChangeStreamDocument<TEntity>> SubscribeToChangesStream(string id)
        {
            var options = new ChangeStreamOptions
            {
                FullDocument = ChangeStreamFullDocumentOption.UpdateLookup
            };
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<TEntity>>()
                .Match("{ operationType: { $in: [ 'insert', 'update', 'replace' ] } }");
            var cursor = _mongoCollection.Watch(pipeline, options);
            var enumerator = cursor.ToEnumerable().GetEnumerator();
            return enumerator;
        }
    }
}