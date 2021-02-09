using System.Threading.Tasks;

namespace Sdk.Interfaces
{
    public interface IPublishService<TEntity> where TEntity : class, IEntityState
    {
        Task<TEntity> Publish<TEvent>(TEntity entityState) where TEvent : class, IEvent;
    }
}