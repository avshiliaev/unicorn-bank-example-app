using System.Threading.Tasks;
using Sdk.Api.Interfaces;

namespace Approvals.Interfaces
{
    public interface IApprovalsManager
    {
        Task<IAccountModel?> ApproveAccountAsync(IAccountModel accountEvent);
        Task<IAccountModel?> DenyAccountAsync(IAccountModel accountEvent);
        Task<IAccountModel?> BlockAccountAsync(IAccountModel accountEvent);
    }
}