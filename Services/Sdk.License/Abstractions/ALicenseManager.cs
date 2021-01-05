using System.Threading.Tasks;
using Sdk.Interfaces;
using Sdk.License.Interfaces;

namespace Sdk.License.Abstractions
{
    public abstract class ALicenseManager<TModel> : ILicenseManager<TModel> where TModel : class, IDataModel
    {
        public abstract Task<bool> EvaluateNewEntityAsync(TModel model);

        public abstract Task<bool> EvaluateStateEntityAsync(TModel dataModel);
        
    }
}