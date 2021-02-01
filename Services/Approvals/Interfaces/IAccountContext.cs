using System.Threading.Tasks;
using Approvals.Abstractions;
using Approvals.StateMachine;
using Sdk.Api.Interfaces;

namespace Approvals.Interfaces
{
    public interface IAccountContext : IAccountModel
    {
        AccountContext InitializeState(AbstractState state, IAccountModel accountModel);
        void CheckBlocked();
        void CheckPending();
        Task ProcessState();
    }
}