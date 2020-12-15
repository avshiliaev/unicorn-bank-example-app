using System.Collections.Generic;
using MongoDB.Driver;

namespace Sdk.Persistence.Interfaces
{
    public interface IMongoRepository<TEntity> where TEntity : class, IMongoEntity
    {
        List<TEntity> GetAll(string profileId);
        TEntity Get(string id);
        TEntity Create(TEntity entity);
        TEntity Update(string id, TEntity entityIn);
        TEntity Remove(TEntity entityIn);
        bool Remove(string id);
        IEnumerator<ChangeStreamDocument<TEntity>> SubscribeToChangesStream(string id);
    }
}