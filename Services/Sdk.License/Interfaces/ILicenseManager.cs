using System.Threading.Tasks;
using Sdk.Interfaces;

namespace Sdk.License.Interfaces
{
    public interface ILicenseManager<TModel> where TModel : class, IDataModel
    {
        Task<bool> EvaluateNewEntityAsync(TModel model);
        Task<bool> EvaluateStateEntityAsync(TModel model);
    }
}