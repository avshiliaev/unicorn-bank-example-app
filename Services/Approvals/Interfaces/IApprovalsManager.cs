using System.Threading.Tasks;
using Sdk.Api.Interfaces;

namespace Approvals.Interfaces
{
    public interface IApprovalsManager
    {
        Task<IAccountModel?> EvaluateAccountPendingAsync(IAccountModel accountCreatedEvent);

        Task<IAccountModel?> EvaluateAccountRunningAsync(IAccountModel accountCreatedEvent);
    }
}