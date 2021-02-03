using System.Threading.Tasks;

namespace Sdk.Interfaces
{
    public interface IEventStoreManager<TModel> where TModel : class, IDataModel
    {
        Task<TModel> SaveStateAsync(TModel dataModel);
    }
}