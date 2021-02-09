using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Extensions;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Accounts.States.Account
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

        public override async Task HandleCheckLicense(ILicenseService<AAccountState> licenseService)
        {
            // Handle as blocked.

            // Check once more a blocked account.
            var isAllowed = await licenseService.EvaluateNotPendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new AccountApproved());
            // Otherwise stay.
        }

        public override async Task HandlePreserveState(IEventStoreService<AAccountState> eventStoreService)
        {
            var savedState = await eventStoreService.AppendStateOfEntity(this);
            if (savedState != null)
            {
                Context.Id = savedState.Id;
                Context.Version = savedState.Version;
            }
        }

        public override Task HandlePublishEvent(IPublishService<AAccountState> publishEndpoint)
        {
            throw new NotImplementedException();
        }
    }
}