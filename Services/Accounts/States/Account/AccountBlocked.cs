using System.Threading.Tasks;
using Sdk.Api.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Interfaces;

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

        public override async Task HandleCheckLicense(ILicenseManager<IAccountModel> licenseManager)
        {
            // Handle as blocked.

            // Check once more a blocked account.
            var isAllowed = await licenseManager.EvaluateNotPendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new AccountApproved());
            // Otherwise stay.
        }

        public override async Task HandlePreserveStateAndPublishEvent(
            IEventStoreManager<IAccountModel> eventStoreManager)
        {
            await eventStoreManager.SaveStateAndNotifyAsync(this);
        }
    }
}