using System.Threading.Tasks;
using Sdk.Api.Interfaces;

namespace Approvals.Interfaces
{
    public interface IApprovalsManager
    {
        Task<IAccountModel?> EvaluateAccountAsync(IAccountModel accountCreatedEvent);
    }
}