using System.Threading.Tasks;

namespace Sdk.Interfaces
{
    public interface IEventStoreManager<TModel> where TModel : class, IDataModel
    {
        // Check if the current version is the same as the last saved one, increment it and append.
        Task SaveStateOptimisticallyAsync(TModel dataModel);
        
        // Check if the current version is -1 to the last saved one, and simply append.
        Task<TModel> SaveStateAsync(TModel dataModel);
    }
}