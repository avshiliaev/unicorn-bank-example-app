using System.Collections.Generic;
using MongoDB.Driver;
using Sdk.Persistence.Interfaces;

namespace Sdk.Persistence.Abstractions
{
    public abstract class AbstractMongoRepository<TEntity> where TEntity : class, IMongoEntity
    {
        private readonly IMongoCollection<TEntity> _mongoCollection;

        public AbstractMongoRepository(IMongoSettingsModel settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _mongoCollection = database.GetCollection<TEntity>(settings.CollectionName);
        }

        public List<TEntity> Get()
        {
            return _mongoCollection.Find(e => true).ToList();
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

        public void Update(string id, TEntity entityIn)
        {
            _mongoCollection.ReplaceOne(e => e.Id == id, entityIn);
        }

        public void Remove(TEntity entityIn)
        {
            _mongoCollection.DeleteOne(e => e.Id == entityIn.Id);
        }

        public void Remove(string id)
        {
            _mongoCollection.DeleteOne(e => e.Id == id);
        }
    }
}