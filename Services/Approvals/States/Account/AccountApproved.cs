using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Interfaces;

namespace Approvals.States.Account
{
    public class AccountApproved : AAccountState
    {
        public override void HandleCheckBlocked()
        {
            if (Context.IsBlocked())
                Context.TransitionTo(new AccountBlocked());
            // Otherwise stay.
        }

        public override void HandleCheckDenied()
        {
            if (Context.IsDenied())
                Context.TransitionTo(new AccountDenied());
            // Otherwise stay.
        }

        public override void HandleCheckApproved()
        {
            // Remain in the current state.
        }

        public override async Task HandleCheckLicense(ILicenseService<IAccountModel> licenseManager)
        {
            // Handle as approved.
            var isAllowed = await licenseManager.EvaluateNotPendingAsync(this);
            if (!isAllowed)
                Context.TransitionTo(new AccountBlocked());
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