using System;
using System.Threading.Tasks;
using Accounts.Abstractions;
using Accounts.StateMachine;
using Sdk.Api.Interfaces;

namespace Accounts.Interfaces
{
    public interface IAccountContext : IAccountModel
    {
        IAccountContext InitializeState(AbstractState state, IAccountModel accountModel);
        Type GetCurrentState();
        void CheckBlocked();
        void CheckDenied();
        void CheckApproved();
        Task CheckLicense();
        Task PreserveStateAndPublishEvent();
    }
}