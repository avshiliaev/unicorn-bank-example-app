using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Interfaces;

namespace Approvals.States.Account
{
    public class AccountPending : AAccountState
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
            if (Context.IsApproved())
                Context.TransitionTo(new AccountApproved());
            // Remain in the current state.
        }

        public override async Task HandleCheckLicense(ILicenseService<IAccountModel> licenseManager)
        {
            // Handle as pending.
            var isAllowed = await licenseManager.EvaluatePendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new AccountApproved());
            else
                Context.TransitionTo(new AccountDenied());
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