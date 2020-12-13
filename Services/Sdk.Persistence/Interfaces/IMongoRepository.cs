using System.Collections.Generic;
using MongoDB.Driver;

namespace Sdk.Persistence.Interfaces
{
    public interface IMongoRepository<TEntity> where TEntity : class, IMongoEntity
    {
        List<TEntity> GetAll();
        TEntity Get(string id);
        TEntity Create(TEntity entity);
        void Update(string id, TEntity entityIn);
        void Remove(TEntity entityIn);
        void Remove(string id);
        IEnumerator<ChangeStreamDocument<TEntity>> SubscribeToChangesStream(string id);
    }
}