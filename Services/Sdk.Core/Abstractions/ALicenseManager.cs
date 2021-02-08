using System.Threading.Tasks;
using Sdk.Interfaces;

namespace Sdk.Abstractions
{
    public abstract class ALicenseManager<TModel> : ILicenseService<TModel> where TModel : class, IEntityState
    {
        public abstract Task<bool> EvaluatePendingAsync(TModel model);

        public abstract Task<bool> EvaluateNotPendingAsync(TModel dataModel);
    }
}