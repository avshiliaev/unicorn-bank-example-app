using System.Threading.Tasks;
using Sdk.Api.Events;
using Sdk.Extensions;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

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

        public override async Task HandleCheckLicense(ILicenseService<AAccountState> licenseService)
        {
            // Handle as pending.
            var isAllowed = await licenseService.EvaluatePendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new AccountApproved());
            else
                Context.TransitionTo(new AccountDenied());
        }

        public override async Task HandlePreserveState(IEventStoreService<AAccountState> eventStoreService)
        {
            var savedState = await eventStoreService.AppendStateOfEntity(this);
            if (savedState != null)
            {
                Id = savedState.Id;
                Version = savedState.Version;
            }
        }

        public override Task HandlePublishEvent(IPublishService<AAccountState> publishEndpoint)
        {
            return publishEndpoint.Publish<AccountCreatedEvent>(this);
        }
    }
}