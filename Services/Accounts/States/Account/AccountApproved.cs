using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Extensions;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Accounts.States.Account
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

        public override async Task HandleCheckLicense(ILicenseService<AAccountState> licenseManager)
        {
            // Handle as approved.
            var isAllowed = await licenseManager.EvaluateNotPendingAsync(this);
            if (!isAllowed)
                Context.TransitionTo(new AccountBlocked());
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
            // await _publishEndpoint.Publish(newAccount?.ToAccountEvent<AccountCreatedEvent>());
            // await _publishEndpoint.Publish(newAccount?.ToAccountEvent<AccountCheckCommand>());
            throw new NotImplementedException();
        }
    }
}