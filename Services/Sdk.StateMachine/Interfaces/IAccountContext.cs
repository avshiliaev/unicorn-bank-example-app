using System;
using System.Threading.Tasks;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Sdk.StateMachine.Interfaces
{
    public interface IAccountContext : IAccountModel
    {
        IAccountContext InitializeState(AAccountState state, IAccountModel accountModel);
        Type GetCurrentState();
        IAccountContext CheckBlocked();
        IAccountContext CheckDenied();
        IAccountContext CheckApproved();
        Task CheckLicense(ILicenseService<AAccountState> licenseManager);
        Task PreserveState(IEventStoreService<AAccountState> eventStoreService);
        Task PublishEvent(IPublishService<AAccountState> publishEndpoint);
    }
}