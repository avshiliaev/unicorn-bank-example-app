using System.Threading.Tasks;
using Sdk.Api.Interfaces;

namespace Approvals.Interfaces
{
    public interface ILicenseManager
    {
        public Task<bool> EvaluateCreationAllowedAsync(IAccountModel accountModel);
        public Task<bool> EvaluateStateAllowedAsync(IAccountModel accountModel);
    }
}