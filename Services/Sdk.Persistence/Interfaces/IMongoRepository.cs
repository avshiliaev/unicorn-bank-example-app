using System.Collections.Generic;

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
    }
}