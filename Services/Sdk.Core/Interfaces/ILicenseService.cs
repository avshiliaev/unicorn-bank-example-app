using System.Threading.Tasks;

namespace Sdk.Interfaces
{
    public interface ILicenseService<in TModel> where TModel : class, IEntityState
    {
        Task<bool> EvaluatePendingAsync(TModel model);
        Task<bool> EvaluateNotPendingAsync(TModel model);
    }
}