using System.Threading.Tasks;

namespace Sdk.Interfaces
{
    public interface IEventStoreService<TEntity> where TEntity : class, IEntityState
    {
        Task<TEntity> AppendStateOfEntity(TEntity entityState);
    }
}