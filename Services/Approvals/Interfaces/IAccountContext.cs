using System;
using System.Threading.Tasks;
using Approvals.Abstractions;
using Approvals.StateMachine;
using Sdk.Api.Interfaces;

namespace Approvals.Interfaces
{
    public interface IAccountContext : IAccountModel
    {
        AccountContext InitializeState(AbstractState state, IAccountModel accountModel);
        Type GetCurrentState();
        void CheckBlocked();
        void CheckDenied();
        void CheckApproved();
        Task CheckLicense();
        Task PreserveStateAndPublishEvent();
    }
}