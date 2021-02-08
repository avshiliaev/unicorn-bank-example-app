using System.Threading.Tasks;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Accounts.Services
{
    public class LicenseService: ILicenseService<AAccountState>
    {
        public Task<bool> EvaluatePendingAsync(AAccountState model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> EvaluateNotPendingAsync(AAccountState model)
        {
            throw new System.NotImplementedException();
        }
    }
}