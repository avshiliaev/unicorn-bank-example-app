using System;
using System.Threading.Tasks;
using Sdk.Api.Abstractions;

namespace Sdk.Api.Interfaces
{
    public interface IAccountContext : IAccountModel
    {
        IAccountContext InitializeState(AAccountState state, IAccountModel accountModel);
        Type GetCurrentState();
        void CheckBlocked();
        void CheckDenied();
        void CheckApproved();
        Task CheckLicense();
        Task PreserveStateAndPublishEvent();
    }
}