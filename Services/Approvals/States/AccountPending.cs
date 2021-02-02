using System.Threading.Tasks;
using Sdk.Api.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Approvals.States
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

        public override async Task HandleCheckLicense(ILicenseManager<IAccountModel> licenseManager)
        {
            // Handle as pending.
            var isAllowed = await licenseManager.EvaluatePendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new AccountApproved());
            else
                Context.TransitionTo(new AccountDenied());
        }

        public override async Task HandlePreserveStateAndPublishEvent(
            IEventStoreManager<IAccountModel> eventStoreManager)
        {
            await eventStoreManager.SaveStateAndNotifyAsync(this);
        }
    }
}