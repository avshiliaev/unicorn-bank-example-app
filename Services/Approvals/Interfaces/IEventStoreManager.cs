using System.Threading.Tasks;
using Sdk.Interfaces;

namespace Approvals.Interfaces
{
    public interface IEventStoreManager<TModel> where TModel : class, IDataModel
    {
        Task<TModel?> SaveStateAndNotifyAsync(TModel dataModel);
    }
}