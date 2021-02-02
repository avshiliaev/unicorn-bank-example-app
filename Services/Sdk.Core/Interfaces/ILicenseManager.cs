using System.Threading.Tasks;

namespace Sdk.Interfaces
{
    public interface ILicenseManager<in TModel> where TModel : class, IDataModel
    {
        Task<bool> EvaluatePendingAsync(TModel model);
        Task<bool> EvaluateNotPendingAsync(TModel model);
    }
}