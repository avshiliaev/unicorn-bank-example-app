using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Extensions;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Accounts.States.Account
{
    public class AccountDenied : AAccountState
    {
        public override void HandleCheckBlocked()
        {
            if (Context.IsBlocked())
                Context.TransitionTo(new AccountBlocked());
            // Otherwise stay.
        }

        public override void HandleCheckDenied()
        {
            // Remain in the current state.
        }

        public override void HandleCheckApproved()
        {
            if (Context.IsApproved())
                Context.TransitionTo(new AccountApproved());
            // Remain in the current state.
        }

        public override async Task HandleCheckLicense(ILicenseService<AAccountState> licenseService)
        {
            // Handle as denied.

            // Check once more a denied account.
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