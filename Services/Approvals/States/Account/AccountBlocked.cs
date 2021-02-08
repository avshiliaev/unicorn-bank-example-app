using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Interfaces;

namespace Approvals.States.Account
{
    public class AccountBlocked : AAccountState
    {
        public override void HandleCheckBlocked()
        {
            // Remain in the current state.
        }

        public override void HandleCheckDenied()
        {
            if (Context.IsDenied())
                Context.TransitionTo(new AccountDenied());
            // Otherwise stay.
        }

        public override void HandleCheckApproved()
        {
            if (Context.IsApproved())
                Context.TransitionTo(new AccountApproved());
            // Remain in the current state.
        }

        public override async Task HandleCheckLicense(ILicenseService<IAccountModel> licenseManager)
        {
            // Handle as blocked.

            // Check once more a blocked account.
            var isAllowed = await licenseManager.EvaluateNotPendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new AccountApproved());
            // Otherwise stay.
        }

        public override async Task HandlePreserveState(
            IEventStoreManager<AAccountState> eventStoreManager)
        {
            await eventStoreManager.SaveStateOptimisticallyAsync(this);
        }

        public override Task HandlePublishEvent(IPublishEndpoint publishEndpoint)
        {
            throw new NotImplementedException();
        }
    }
}