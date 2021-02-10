using System.Threading.Tasks;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Approvals.Services
{
    public class LicenseService : ILicenseService<AAccountState>
    {
        public Task<bool> EvaluatePendingAsync(AAccountState model)
        {
            return Task.FromResult(true);
        }

        public Task<bool> EvaluateNotPendingAsync(AAccountState model)
        {
            return Task.FromResult(true);
        }
    }
}