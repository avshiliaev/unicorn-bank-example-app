using System.Threading.Tasks;
using Sdk.Interfaces;
using Sdk.License.Interfaces;

namespace Sdk.License.Abstractions
{
    public abstract class ALicenseManager<TModel> : ILicenseManager<TModel> where TModel : class, IDataModel
    {
        public abstract Task<bool> EvaluatePendingAsync(TModel model);

        public abstract Task<bool> EvaluateNotPendingAsync(TModel dataModel);
    }
}