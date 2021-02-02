using System.Threading.Tasks;
using Sdk.Interfaces;

namespace Sdk.License.Interfaces
{
    public interface ILicenseManager<in TModel> where TModel : class, IDataModel
    {
        Task<bool> EvaluatePendingAsync(TModel model);
        Task<bool> EvaluateNotPendingAsync(TModel model);
    }
}